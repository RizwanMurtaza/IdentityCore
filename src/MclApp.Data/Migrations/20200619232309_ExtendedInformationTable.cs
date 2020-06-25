using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MclApp.Data.Migrations
{
    public partial class ExtendedInformationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserExtendedInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ExternalWebsiteToScan = table.Column<string>(nullable: true),
                    ExternalEndPointsToScan = table.Column<string>(nullable: true),
                    DomainNameForScan = table.Column<string>(nullable: true),
                    OfficeTenant = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExtendedInformation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExtendedInformation");
        }
    }
}
