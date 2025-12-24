using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pulse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGuildLoggingSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildLoggingSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuildId = table.Column<string>(type: "text", nullable: false),
                    LogChannelId = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildLoggingSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildLoggingSettings");
        }
    }
}
