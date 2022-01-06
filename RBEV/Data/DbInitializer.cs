using RBEV.Enums;
using RBEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Data
{
    public class DbInitializer

    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var members = new Member[]
            {
            new Member{FirstName="Mary",LastName="Shelley",DOB=DateTime.Parse("2005-09-01"), Address ="Tucker Street, Castlebar, Co.Mayo", Email = "mary@gmail.com", PhoneNumber = 0891234561,Gender = Gender.Female, AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-01-05")},
            new Member{FirstName="Margaret",LastName="Atwood",DOB=DateTime.Parse("1980-10-03"), Address ="Ottawa Street, Castlebar, Co.Mayo", Email = "margaret@gmail.com", PhoneNumber = 0891234562, Gender = Gender.Female,AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-01-05")},
            new Member{FirstName="Harper",LastName="Lee",DOB=DateTime.Parse("1955-11-01"), Address ="Monroe Street, Moycullen, Co.Galway", Email = "harper@gmail.com", PhoneNumber = 0881234561, Gender = Gender.Female,AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-09-09")},
            new Member{FirstName="Emily",LastName="Dickinson",DOB=DateTime.Parse("1965-09-07"), Address ="Amherst Street, Fermoy, Co.Cork", Email = "emily@gmail.com", PhoneNumber = 0861234561,Gender = Gender.Female, AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-02-05")},
            new Member{FirstName="Oscar",LastName="Wilde",DOB=DateTime.Parse("1987-01-08"), Address ="Westland Row, Rossmore, Co.Tipperary", Email = "oscar@gmail.com", PhoneNumber = 0851234567, Gender = Gender.Male, AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-11-05")},
            new Member{FirstName="Robert",LastName="Frost",DOB=DateTime.Parse("1991-12-23"), Address ="Boston Street, Tralee, Co.Kerry", Email = "mary@gmail.com", PhoneNumber = 0891234561, Gender = Gender.Male, AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-01-07")},
            new Member{FirstName="Leo",LastName="Tolstoy",DOB=DateTime.Parse("1970-05-15"), Address ="Caesar Street, Fermoy, Co.Cork", Email = "leo@gmail.com", PhoneNumber = 0871234567, Gender = Gender.Male, AccountType = AccountType.Member, RegistrationDate =DateTime.Parse("2021-01-06")}

            };
            foreach (Member s in members)
            {
                context.Members.Add(s);
            }
            context.SaveChanges();

            var eventcordinators = new EventCoordinator[]
            {
                new EventCoordinator { FirstName = "Jimi",LastName="Hendrix",DOB=DateTime.Parse("1971-07-15"), Address ="Seattle Street, Fermoy, Co.Cork", PhoneNumber = 0871784567, ClubRole="General Secretary", Email ="jimi@gmail.com", AccountType = AccountType.EventCoordinator},
                new EventCoordinator { FirstName = "James",LastName="Dean",DOB=DateTime.Parse("1982-11-15"), Address ="Marion Street,Castlebar, Co.Mayo",PhoneNumber = 0891237267, ClubRole="General Secretary", Email ="james@gmail.com",AccountType = AccountType.EventCoordinator},
                new EventCoordinator { FirstName = "Kelley",LastName="O'Hara", DOB=DateTime.Parse("1991-10-23"), Address ="Orlando Street, Tralee, Co.Kerry",PhoneNumber = 0861233267, ClubRole="Chairperson", Email ="kelley@gmail.com", AccountType = AccountType.EventCoordinator},
                new EventCoordinator { FirstName = "Rose",LastName="Lavelle", DOB=DateTime.Parse("1965-01-08"), Address ="Cincinnati st,  Rossmore, Co.Tipperary",PhoneNumber = 0871223345, ClubRole="PRO", Email ="rose@gmail.com", AccountType = AccountType.EventCoordinator},
                new EventCoordinator { FirstName = "Alex",LastName="Morgan", DOB=DateTime.Parse("1995-01-08"), Address ="PearseRow, Ballinrobe, Co.Mayo",PhoneNumber = 0871234123, ClubRole="Chairperson", Email ="alex@gmail.com", AccountType = AccountType.EventCoordinator},
                new EventCoordinator { FirstName = "Katie",LastName="McCabe",DOB=DateTime.Parse("1987-01-08"), Address ="Westland Row, Portmarnock, Co.Dublin",PhoneNumber = 0851336699, ClubRole="Chairperson", Email ="katie@gmail.com", AccountType = AccountType.EventCoordinator},
            };

            foreach (EventCoordinator i in eventcordinators)
            {
                context.EventCoordinators.Add(i);
            }
            context.SaveChanges();

            var clubs = new Club[]
            {
               new Club { Name = "Castlebar Racquetball Club", County = "Mayo", Province = Province.Connaught, NumberofCourts = 2, EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "Hendrix").ID },
               new Club { Name = "Ballinrobe Racquetball Club", County = "Mayo", Province = Province.Connaught, NumberofCourts = 2, EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "Dean").ID },
               new Club { Name = "Fermoy Racquetball Club", County = "Cork", Province = Province.Munster, NumberofCourts = 2,EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "O'Hara").ID },
               new Club { Name = "Rossmore Racquetball Club", County = "Tipperary", Province = Province.Munster, NumberofCourts = 1, EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "Lavelle").ID },
               new Club { Name = "Tralee Racquetball Club", County = "Kerry", Province = Province.Munster, NumberofCourts = 2, EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "Morgan").ID },
               new Club { Name = "Moycullen Racquetball Club", County = "Galway", Province = Province.Connaught, NumberofCourts = 2, EventCoordinatorID  = eventcordinators.Single( i => i.LastName == "McCabe").ID },
            };

            foreach (Club d in clubs)
            {
                context.Clubs.Add(d);
            }
            context.SaveChanges();

            var events = new Event[]
            {

            new Event{EventName="Tralee Open", EventDetails="Annual Tralee Open. Singles & Doubles Divisions", EventDate=DateTime.Parse("2021-12-11"), EventType="RAI Tournament", PostedDate=DateTime.Parse("2021-11-11"), ClubID = clubs.Single( s => s.ClubID == 5).ClubID},
            new Event{EventName="Moycullen Open", EventDetails="Annual Moycullen Open. Singles & Doubles Divisions", EventDate=DateTime.Parse("2021-12-17"), EventType="RAI Tournament", PostedDate=DateTime.Parse("2021-11-11"),ClubID = clubs.Single( s => s.ClubID == 1).ClubID},
            new Event{EventName="Autumn League", EventDetails="All Divisions", EventDate=DateTime.Parse("2021-11-15"), EventType="Club League", PostedDate=DateTime.Parse("2021-11-11"), ClubID = clubs.Single( s => s.ClubID == 1).ClubID },
            new Event{EventName="Fermoy Racquetball Club AGM", EventDetails="Annual General Meeting at Clubhouse", EventDate=DateTime.Parse("2021-10-11"), EventType="Meeting", PostedDate=DateTime.Parse("2021-11-11"),ClubID = clubs.Single( s => s.ClubID == 2).ClubID},
            };
            foreach (Event c in events)
            {
                context.Events.Add(c);
            }
            context.SaveChanges();

            var eventlocations = new EventLocation[]
            {
            new EventLocation {EventID =1, Address="Tralee Regional Sports & Leisure Centre, Cloonalour, Tralee, Co. Kerry", Latitude = 52.272038, Longitude= -9.692023, Description = "Tralee Open"},
            new EventLocation{EventID =2, Address="Moycullen Handball Club, Baile Doite, Co.Galway", Latitude = 53.338748, Longitude= -9.181478 , Description = "Moycullen Open"},
            new EventLocation{EventID = 3, Address="An Sportlann Castlebar, Co.Mayo", Latitude = 53.85, Longitude= -9.3 , Description = "Autumn League"}
            };
            foreach (EventLocation c in eventlocations)
            {
                context.EventLocations.Add(c);
            }
            context.SaveChanges();

            var eventcordassignment = new EventAssignment[]
            {
                new EventAssignment {EventID = events.Single(c => c.EventName == "Tralee Open" ).RacquetballEventID, EventCoordinatorID = eventcordinators.Single(i => i.LastName == "Morgan").ID},
                new EventAssignment {EventID = events.Single(c => c.EventName == "Moycullen Open" ).RacquetballEventID, EventCoordinatorID = eventcordinators.Single(i => i.LastName == "McCabe").ID},
                new EventAssignment {EventID = events.Single(c => c.EventName == "Autumn League" ).RacquetballEventID,EventCoordinatorID = eventcordinators.Single(i => i.LastName == "Hendrix").ID},
                new EventAssignment {EventID = events.Single(c => c.EventName == "Fermoy Racquetball Club AGM" ).RacquetballEventID,EventCoordinatorID = eventcordinators.Single(i => i.LastName == "Dean").ID},

            };

            foreach (EventAssignment ci in eventcordassignment)
            {
                context.EventAssignments.Add(ci);
            }
            context.SaveChanges();

            var registrations = new Registration[]
            {
                new Registration {MemberID = members.Single(s => s.LastName == "Shelley").ID, EventID = events.Single(c => c.EventName == "Tralee Open" ).RacquetballEventID, Division=Division.Novice},
                new Registration {MemberID = members.Single(s => s.LastName == "Shelley").ID, EventID = events.Single(c => c.EventName == "Moycullen Open" ).RacquetballEventID, Division=Division.Novice},
                new Registration {MemberID = members.Single(s => s.LastName == "Shelley").ID, EventID = events.Single(c => c.EventName == "Fermoy Racquetball Club AGM" ).RacquetballEventID, Division=Division.Novice},
                new Registration {MemberID = members.Single(s => s.LastName == "Lee").ID, EventID = events.Single(c => c.EventName == "Tralee Open" ).RacquetballEventID, Division=Division.Novice},
                new Registration {MemberID = members.Single(s => s.LastName == "Frost").ID, EventID = events.Single(c => c.EventName == "Tralee Open" ).RacquetballEventID, Division=Division.Open},
                new Registration {MemberID = members.Single(s => s.LastName == "Tolstoy").ID, EventID = events.Single(c => c.EventName == "Fermoy Racquetball Club AGM" ).RacquetballEventID,},
                new Registration {MemberID = members.Single(s => s.LastName == "Dickinson").ID, EventID = events.Single(c => c.EventName == "Autumn League" ).RacquetballEventID, Division=Division.C},
                new Registration {MemberID = members.Single(s => s.LastName == "Dickinson").ID,EventID = events.Single(c => c.EventName == "Fermoy Racquetball Club AGM" ).RacquetballEventID},
                new Registration {MemberID = members.Single(s => s.LastName == "Atwood").ID, EventID = events.Single(c => c.EventName == "Autumn League" ).RacquetballEventID, Division=Division.B},
                new Registration {MemberID = members.Single(s => s.LastName == "Wilde").ID, EventID = events.Single(c => c.EventName == "Autumn League" ).RacquetballEventID, Division=Division.D},
            };
            foreach (Registration e in registrations)
            {
                var registrationInDataBase = context.Registrations.Where(
                    s =>
                            s.Member.ID == e.MemberID &&
                            s.Event.RacquetballEventID == e.EventID).SingleOrDefault();
                if (registrationInDataBase == null)
                {
                    context.Registrations.Add(e);
                }
            }
            context.SaveChanges();


        }


    }
}
