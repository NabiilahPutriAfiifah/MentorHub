using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MentorHub.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_learning_goals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    target_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_learning_goals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    otp = table.Column<long>(type: "bigint", nullable: true),
                    eppired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_used = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_accounts_tbl_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    experience = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mentor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_employees_tbl_accounts_id",
                        column: x => x.id,
                        principalTable: "tbl_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_employees_tbl_employees_mentor_id",
                        column: x => x.mentor_id,
                        principalTable: "tbl_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_mentee_goals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mentee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    learning_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LearningGoalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_mentee_goals", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_mentee_goals_tbl_accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "tbl_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_mentee_goals_tbl_accounts_mentee_id",
                        column: x => x.mentee_id,
                        principalTable: "tbl_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_mentee_goals_tbl_learning_goals_LearningGoalsId",
                        column: x => x.LearningGoalsId,
                        principalTable: "tbl_learning_goals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_mentee_goals_tbl_learning_goals_learning_id",
                        column: x => x.learning_id,
                        principalTable: "tbl_learning_goals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_mentor_skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mentor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    skill_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_mentor_skills", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_mentor_skills_tbl_accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "tbl_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_mentor_skills_tbl_accounts_mentor_id",
                        column: x => x.mentor_id,
                        principalTable: "tbl_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_mentor_skills_tbl_skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "tbl_skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_mentor_skills_tbl_skills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "tbl_skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tbl_learning_goals",
                columns: new[] { "id", "description", "status", "target_date", "title" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), "Mampu mengimplementasikan CRUD dengan Entity Framework Core di ASP.NET.", "InProgress", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "Menguasai EF Core" });

            migrationBuilder.InsertData(
                table: "tbl_roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Admin" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Mentor" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Mentee" }
                });

            migrationBuilder.InsertData(
                table: "tbl_skills",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Programming language for backend development.", "C#" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Frontend and full-stack language.", "JavaScript" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Database query and management skill.", "SQL" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "User experience and interface design.", "UX/UI Design" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Scripting and data analysis language.", "Python" }
                });

            migrationBuilder.InsertData(
                table: "tbl_accounts",
                columns: new[] { "id", "eppired", "is_used", "otp", "password", "RoleId", "username" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), null, false, null, "Pa$$word123", new Guid("10000000-0000-0000-0000-000000000001"), "admin" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), null, false, null, "Pa$$word123", new Guid("10000000-0000-0000-0000-000000000002"), "mentor" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), null, false, null, "Pa$$word123", new Guid("10000000-0000-0000-0000-000000000003"), "mentee" }
                });

            migrationBuilder.InsertData(
                table: "tbl_employees",
                columns: new[] { "id", "bio", "email", "experience", "first_name", "last_name", "mentor_id", "position" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "System Administrator.", "admin@hub.com", "Senior", "Super", "Admin", null, "System Analyst" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Full-stack developer with 5 years experience.", "budi@hub.com", "Expert", "Budi", "Prasetyo", null, "Tech Lead" }
                });

            migrationBuilder.InsertData(
                table: "tbl_mentee_goals",
                columns: new[] { "id", "AccountsId", "LearningGoalsId", "learning_id", "mentee_id", "Status" },
                values: new object[] { new Guid("cec21414-9509-4898-9587-ef8306781d7b"), null, null, new Guid("10000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000003"), 1 });

            migrationBuilder.InsertData(
                table: "tbl_mentor_skills",
                columns: new[] { "id", "AccountsId", "level", "mentor_id", "skill_id", "SkillsId" },
                values: new object[,]
                {
                    { new Guid("ee74eb17-ada5-4fc3-8604-48768fb97765"), null, 2, new Guid("10000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000001"), null },
                    { new Guid("fc94bbef-b663-421b-9343-0c7744233826"), null, 1, new Guid("10000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), null }
                });

            migrationBuilder.InsertData(
                table: "tbl_employees",
                columns: new[] { "id", "bio", "email", "experience", "first_name", "last_name", "mentor_id", "position" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), "Junior developer learning C#.", "siti@hub.com", "Junior", "Siti", "Aisyah", new Guid("10000000-0000-0000-0000-000000000002"), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_accounts_RoleId",
                table: "tbl_accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_employees_mentor_id",
                table: "tbl_employees",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentee_goals_AccountsId",
                table: "tbl_mentee_goals",
                column: "AccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentee_goals_learning_id",
                table: "tbl_mentee_goals",
                column: "learning_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentee_goals_LearningGoalsId",
                table: "tbl_mentee_goals",
                column: "LearningGoalsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentee_goals_mentee_id",
                table: "tbl_mentee_goals",
                column: "mentee_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentor_skills_AccountsId",
                table: "tbl_mentor_skills",
                column: "AccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentor_skills_mentor_id",
                table: "tbl_mentor_skills",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentor_skills_skill_id",
                table: "tbl_mentor_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_mentor_skills_SkillsId",
                table: "tbl_mentor_skills",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_employees");

            migrationBuilder.DropTable(
                name: "tbl_mentee_goals");

            migrationBuilder.DropTable(
                name: "tbl_mentor_skills");

            migrationBuilder.DropTable(
                name: "tbl_learning_goals");

            migrationBuilder.DropTable(
                name: "tbl_accounts");

            migrationBuilder.DropTable(
                name: "tbl_skills");

            migrationBuilder.DropTable(
                name: "tbl_roles");
        }
    }
}
