using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessEfCore.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Apprentices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apprentices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Apprentices_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyReport",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprenticeID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    CalenderWeek = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false),
                    StatusApprentice = table.Column<int>(type: "int", nullable: false),
                    StatusInstructor = table.Column<int>(type: "int", nullable: false),
                    SigningApprentice = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SigningInstructor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SigningDateApprentice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SigningDateInstructor = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyReport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeeklyReport_Apprentices_ApprenticeID",
                        column: x => x.ApprenticeID,
                        principalTable: "Apprentices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeeklyReport_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WeeklyReportPositions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyReportID = table.Column<int>(type: "int", nullable: false),
                    ApprenticeID = table.Column<int>(type: "int", nullable: false),
                    NoteID = table.Column<int>(type: "int", nullable: false),
                    DailyReport = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    DailyHours = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyReportPositions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeeklyReportPositions_Apprentices_ApprenticeID",
                        column: x => x.ApprenticeID,
                        principalTable: "Apprentices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeeklyReportPositions_WeeklyReport_WeeklyReportID",
                        column: x => x.WeeklyReportID,
                        principalTable: "WeeklyReport",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyReportPositionsID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Notes_WeeklyReportPositions_WeeklyReportPositionsID",
                        column: x => x.WeeklyReportPositionsID,
                        principalTable: "WeeklyReportPositions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "DateEntry", "Email", "FirstName", "LastName", "Password", "Role", "Token", "UserName" },
                values: new object[] { 1, new DateTime(2010, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Max", "Mustermann", "12345678910!aA!", 0, "", "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "ID", "DateEntry", "Email", "FirstName", "LastName", "Password", "Role", "Token", "UserName" },
                values: new object[] { 1, new DateTime(2011, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ausbilder@gmail.com", "Uwe", "Meier", "12345678910!aA!", 1, "", "ausbilder@gmail.com" });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "ID", "DateEntry", "Email", "FirstName", "LastName", "Password", "Role", "Token", "UserName" },
                values: new object[] { 2, new DateTime(2010, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "ausbilder2@gmail.com", "John", "Doe", "12345678910!aA!", 1, "", "ausbilder2@gmail.com" });

            migrationBuilder.InsertData(
                table: "Apprentices",
                columns: new[] { "ID", "DateEntry", "Email", "FirstName", "InstructorID", "LastName", "Password", "Role", "Token", "UserName" },
                values: new object[] { 1, new DateTime(20, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "auszubildender@gmail.com", "Ruwen", 1, "Müller", "12345678910!aA!", 2, "", "auszubildender@gmail.com" });

            migrationBuilder.InsertData(
                table: "Apprentices",
                columns: new[] { "ID", "DateEntry", "Email", "FirstName", "InstructorID", "LastName", "Password", "Role", "Token", "UserName" },
                values: new object[] { 2, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "auszubildender2@gmail.com", "Kevin", 2, "McCallister", "12345678910!aA!", 2, "", "auszubildender2@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apprentices_Email",
                table: "Apprentices",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apprentices_InstructorID",
                table: "Apprentices",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Email",
                table: "Instructors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_InstructorID",
                table: "Notes",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_WeeklyReportPositionsID",
                table: "Notes",
                column: "WeeklyReportPositionsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReport_ApprenticeID",
                table: "WeeklyReport",
                column: "ApprenticeID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReport_InstructorID",
                table: "WeeklyReport",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportPositions_ApprenticeID",
                table: "WeeklyReportPositions",
                column: "ApprenticeID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportPositions_WeeklyReportID",
                table: "WeeklyReportPositions",
                column: "WeeklyReportID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "WeeklyReportPositions");

            migrationBuilder.DropTable(
                name: "WeeklyReport");

            migrationBuilder.DropTable(
                name: "Apprentices");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
