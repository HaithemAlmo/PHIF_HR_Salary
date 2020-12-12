using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Almotkaml.HR.EntityCore.Migrations
{
    public partial class employeeTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalaryTest",
                table: "Salaries",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryTest",
                table: "JobInfos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "EmployeeTest",
                columns: table => new
                {
                    EmployeeTestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    DateEndJob = table.Column<DateTime>(nullable: false),
                    DateStartJob = table.Column<DateTime>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    GrandfatherName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NationalNumber = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    StateEmployee = table.Column<bool>(nullable: false),
                    _CreatedBy = table.Column<int>(nullable: false),
                    _DateCreated = table.Column<DateTime>(nullable: false),
                    _DateModified = table.Column<DateTime>(nullable: false),
                    _ModifiedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTest", x => x.EmployeeTestId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTestSalary",
                columns: table => new
                {
                    EmployeeTestSalaryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    DateEndJob = table.Column<DateTime>(nullable: false),
                    DateStartJob = table.Column<DateTime>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmployeeTestId = table.Column<int>(nullable: false),
                    FatherName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    GrandfatherName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MonthDate = table.Column<DateTime>(nullable: false),
                    NationalNumber = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    StateEmployee = table.Column<bool>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTestSalary", x => x.EmployeeTestSalaryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTest");

            migrationBuilder.DropTable(
                name: "EmployeeTestSalary");

            migrationBuilder.DropColumn(
                name: "TotalSalaryTest",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "SalaryTest",
                table: "JobInfos");
        }
    }
}
