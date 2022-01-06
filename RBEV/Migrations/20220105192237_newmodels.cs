using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RBEV.Migrations
{
    public partial class newmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventCoordinator",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ClubRole = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCoordinator", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventCoordinator_Account_ID",
                        column: x => x.ID,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Member_Account_ID",
                        column: x => x.ID,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                schema: "Identity",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<int>(type: "int", nullable: false),
                    NumberofCourts = table.Column<int>(type: "int", nullable: false),
                    EventCoordinatorID = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.ClubID);
                    table.ForeignKey(
                        name: "FK_Club_EventCoordinator_EventCoordinatorID",
                        column: x => x.EventCoordinatorID,
                        principalSchema: "Identity",
                        principalTable: "EventCoordinator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "Identity",
                columns: table => new
                {
                    RacquetballEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EventDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.RacquetballEventID);
                    table.ForeignKey(
                        name: "FK_Event_Club_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "Identity",
                        principalTable: "Club",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAssignment",
                schema: "Identity",
                columns: table => new
                {
                    EventCoordinatorID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAssignment", x => new { x.EventID, x.EventCoordinatorID });
                    table.ForeignKey(
                        name: "FK_EventAssignment_Event_EventID",
                        column: x => x.EventID,
                        principalSchema: "Identity",
                        principalTable: "Event",
                        principalColumn: "RacquetballEventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAssignment_EventCoordinator_EventCoordinatorID",
                        column: x => x.EventCoordinatorID,
                        principalSchema: "Identity",
                        principalTable: "EventCoordinator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLocation",
                schema: "Identity",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocation", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_EventLocation_Event_EventID",
                        column: x => x.EventID,
                        principalSchema: "Identity",
                        principalTable: "Event",
                        principalColumn: "RacquetballEventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                schema: "Identity",
                columns: table => new
                {
                    RegistrationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationID);
                    table.ForeignKey(
                        name: "FK_Registration_Event_EventID",
                        column: x => x.EventID,
                        principalSchema: "Identity",
                        principalTable: "Event",
                        principalColumn: "RacquetballEventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Member_MemberID",
                        column: x => x.MemberID,
                        principalSchema: "Identity",
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_EventCoordinatorID",
                schema: "Identity",
                table: "Club",
                column: "EventCoordinatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ClubID",
                schema: "Identity",
                table: "Event",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_EventAssignment_EventCoordinatorID",
                schema: "Identity",
                table: "EventAssignment",
                column: "EventCoordinatorID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_EventID",
                schema: "Identity",
                table: "EventLocation",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_EventID",
                schema: "Identity",
                table: "Registration",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_MemberID",
                schema: "Identity",
                table: "Registration",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAssignment",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "EventLocation",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Registration",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Member",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Club",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "EventCoordinator",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Identity");
        }
    }
}
