using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    FieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.FieldID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GivenNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Project_Field_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Field",
                        principalColumn: "FieldID");
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeEstimation = table.Column<double>(type: "float", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                    table.ForeignKey(
                        name: "FK_Job_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JobComponent",
                columns: table => new
                {
                    JobComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobComponent", x => x.JobComponentID);
                    table.ForeignKey(
                        name: "FK_JobComponent_Job_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobComponent_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JobPerson",
                columns: table => new
                {
                    JobPersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPerson", x => x.JobPersonID);
                    table.ForeignKey(
                        name: "FK_JobPerson_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobPerson_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JobRequirement",
                columns: table => new
                {
                    JobRequirementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    RequirementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequirement", x => x.JobRequirementID);
                    table.ForeignKey(
                        name: "FK_JobRequirement_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobRequirement_Job_RequirementID",
                        column: x => x.RequirementID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkSection",
                columns: table => new
                {
                    WorkSectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSection", x => x.WorkSectionID);
                    table.ForeignKey(
                        name: "FK_WorkSection_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkSection_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_ProjectID",
                table: "Job",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_JobComponent_ComponentID",
                table: "JobComponent",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_JobComponent_JobID",
                table: "JobComponent",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPerson_JobID",
                table: "JobPerson",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPerson_PersonID",
                table: "JobPerson",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirement_JobID",
                table: "JobRequirement",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirement_RequirementID",
                table: "JobRequirement",
                column: "RequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_FieldID",
                table: "Project",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSection_JobID",
                table: "WorkSection",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSection_PersonID",
                table: "WorkSection",
                column: "PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobComponent");

            migrationBuilder.DropTable(
                name: "JobPerson");

            migrationBuilder.DropTable(
                name: "JobRequirement");

            migrationBuilder.DropTable(
                name: "WorkSection");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Field");
        }
    }
}
