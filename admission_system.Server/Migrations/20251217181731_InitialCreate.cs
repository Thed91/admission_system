using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admission_system.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    VisitorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    VisitorType = table.Column<string>(type: "text", nullable: true),
                    IsPass = table.Column<bool>(type: "boolean", nullable: false),
                    PassLevel = table.Column<int>(type: "integer", nullable: false),
                    IsWeapons = table.Column<bool>(type: "boolean", nullable: false),
                    IsAggressive = table.Column<bool>(type: "boolean", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.VisitorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
