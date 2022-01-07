using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using RBEV.Controllers;
using RBEV.Data;
using RBEV.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RBEV_UnitTests1
{



    /*
        private readonly ITestOutputHelper output;
        private readonly Mock<ApplicationDbContext> mockcontext;
        private readonly EventsController sut;

        public EventsControllerTests(ITestOutputHelper testOutput)
        {
            this.output = testOutput;
            var mockSet = new Mock<DbSet<Member>>();
            mockcontext = new Mock<ApplicationDbContext>();

            sut = new EventsController(mockcontext.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfOfEvents()
        {
            var result = await sut.Index();

            Assert.IsType<ViewResult>(result);
            var model = ((ViewResult)result).Model;
            Assert.Equal(Event().Count, ((List<Event>)model).Count);
        }*/


    //var mockSet = new Mock<DbSet<Member>>();

}
