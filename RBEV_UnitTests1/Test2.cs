using Microsoft.AspNetCore.Mvc;
using Moq;
using RBEV.Controllers;
using RBEV.Data;
using RBEV.Enums;
using RBEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RBEV_UnitTests1
{

    public class Test2
    {
        private readonly EventsController sut;
        private readonly Mock<ApplicationDbContext> mockset;

        [Theory]
        [InlineData(3, "Ladder")]
        public async Task Details_ReturnsAViewResult_WithEventModel(int id, string title)
        {
            var result = await sut.Details(id);
            var model = (Event)((ViewResult)result).Model;

            Assert.NotNull(result);
            Assert.Equal(title, model.EventName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async Task Details_ReturnsANotFoundResult(int? id)
        {
            var result = await sut.Details(id);

            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public void Create_ReturnsAViewResult()
        {
            var result = (ViewResult)sut.Create();

            Assert.IsType<ViewResult>(result);

            var viewData = ((ViewResult)result).ViewData;
            Assert.True(viewData.ContainsKey("ClubID"));
        }


        [Fact]
        public async Task CreatePost_ReturnsAViewResult_WithInvalidCourseCreateViewModel()
        {
            Event eventToAdd = new Event { EventName = "Competition", EventDate = DateTime.Parse("2022-05-05"), EventDetails = "New Event", PostedDate = DateTime.Parse("2022-01-01"), EventType = "Competition", ClubID = 1 };
            sut.ModelState.AddModelError("MyErrorMessage", "error message");

            var result = await sut.Create(eventToAdd);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewData = ((ViewResult)result).ViewData;
            Event model = (Event)viewData.Model;
            Assert.True(viewData.ContainsKey("ClubID"));
            Assert.True(viewData.ModelState.ContainsKey("MyErrorMessage"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async Task Edit_ReturnsANotFoundResult(int? id)
        {
            var result = await sut.Edit(id);

            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Theory]
        [InlineData(3, "League", "League", 4)]
        public async Task Edit_ReturnsAViewResult_WithEventEditViewModel(int id, string name, string number, long clubid)
        {
            var result = await sut.Edit(id);

            Assert.IsType<ViewResult>(result);

            var viewData = ((ViewResult)result).ViewData;
            Event model = (Event)viewData.Model;
            Assert.Equal(name, model.EventName);
            Assert.Equal(number, model.EventType);
            Assert.Equal(clubid, model.ClubID);
            Assert.True(viewData.ContainsKey("ClubID"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public async Task EditPost_ReturnsANotFoundResult(int? id)
        {
            var result = await sut.EditPost(id);

            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public async Task Delete_ReturnsANotFoundResult(int? id)
        {
            var result = await sut.Delete(id);

            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }


        [Theory]
        [InlineData(1, "Mayo Open")]
        public async Task Delete_ReturnsAViewResult_WithCourseModel(int id, string title)
        {
            var result = await sut.Delete(id);

            Assert.IsType<ViewResult>(result);

            var viewData = ((ViewResult)result).ViewData;
            Event model = (Event)viewData.Model;
            Assert.Equal(title, model.EventName);
        }

        [Theory]
        [InlineData(5)]
        public async Task DeletePost_ReturnsARedirectToAction_Index(int? id)
        {
            var result = await sut.DeleteConfirmed(id.Value);

            Assert.IsType<RedirectToActionResult>(result);
            var actionName = ((RedirectToActionResult)result).ActionName;
            Assert.Equal("Index", actionName);
        }


        private List<Club> Clubs()
        {
            return new List<Club>
            {
               new Club { ClubID = 1, Name = "Carnacon Racquetball Club", County = "Mayo", Province = Province.Connaught, NumberofCourts = 2, EventCoordinatorID = 1 },
               new Club { ClubID = 2,Name = "Newport Racquetball Club", County = "Mayo", Province = Province.Connaught, NumberofCourts = 2, EventCoordinatorID = 2 },
               new Club { ClubID = 3,Name = "Araglen Racquetball Club", County = "Cork", Province = Province.Munster, NumberofCourts = 2, EventCoordinatorID = 3 },
               new Club { ClubID = 4, Name = "Fethard Racquetball Club", County = "Tipperary", Province = Province.Munster, NumberofCourts = 1, EventCoordinatorID = 4 }
               };
           }
        private List<Event> Events()
        {
            return new List<Event>
            {
            new Event { EventName = "Mayo Open", EventDetails = "Annual Mayo Open. Singles & Doubles Divisions", EventDate = DateTime.Parse("2021-12-11"), EventType = "RAI Tournament", PostedDate = DateTime.Parse("2021-11-11"), ClubID = 1 },
            new Event { EventName = "Newport Open", EventDetails = "Annual Newport Open. Singles & Doubles Divisions", EventDate = DateTime.Parse("2021-12-17"), EventType = "RAI Tournament", PostedDate = DateTime.Parse("2021-11-11"), ClubID = 2 },
            new Event { EventName = "Ladder", EventDetails = "All Divisions", EventDate = DateTime.Parse("2021-11-15"), EventType = "Club League", PostedDate = DateTime.Parse("2021-11-11"), ClubID = 3 },
            new Event { EventName = "AGM", EventDetails = "Annual General Meeting at Clubhouse", EventDate = DateTime.Parse("2021-10-11"), EventType = "Meeting", PostedDate = DateTime.Parse("2021-11-11"), ClubID = 4 }
            };
        }
    }
}
