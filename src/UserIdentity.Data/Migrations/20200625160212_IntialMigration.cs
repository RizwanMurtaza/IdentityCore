using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentity.Data.Migrations
{
    public partial class IntialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MclApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationKey = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MclAppPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    MclApplicationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclAppPermissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "MclAppGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclAppGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MclAppGroups_MclApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MclApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MclAppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    MclApplicationId = table.Column<int>(nullable: false),
                    MclUserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActiveLanguageId = table.Column<int>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclAppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MclAppUsers_MclApplications_MclApplicationId",
                        column: x => x.MclApplicationId,
                        principalTable: "MclApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_MclAppPermissions_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MclAppPermissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MclGroupPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MclGroupPermissions_MclAppPermissions_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MclAppPermissions",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK_MclGroupPermissions_MclAppGroups_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "MclAppGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_MclAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MclAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_MclAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MclAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MclGroupUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MclGroupUsers_MclAppGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MclAppGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MclGroupUsers_MclAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MclAppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MclLoginDetail",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclLoginDetail", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_MclLoginDetail_MclAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MclAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MclUserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MclUserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MclUserPermissions_MclAppPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "MclAppPermissions",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK_MclUserPermissions_MclAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MclAppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MclAppGroups_ApplicationId",
                table: "MclAppGroups",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "MclAppPermissions",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MclAppUsers_MclApplicationId",
                table: "MclAppUsers",
                column: "MclApplicationId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "MclAppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "MclAppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MclGroupPermissions_GroupId",
                table: "MclGroupPermissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MclGroupPermissions_PermissionId",
                table: "MclGroupPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MclGroupUsers_GroupId",
                table: "MclGroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MclGroupUsers_UserId",
                table: "MclGroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MclLoginDetail_UserId",
                table: "MclLoginDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MclUserPermissions_PermissionId",
                table: "MclUserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MclUserPermissions_UserId",
                table: "MclUserPermissions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MclGroupPermissions");

            migrationBuilder.DropTable(
                name: "MclGroupUsers");

            migrationBuilder.DropTable(
                name: "MclLoginDetail");

            migrationBuilder.DropTable(
                name: "MclUserPermissions");

            migrationBuilder.DropTable(
                name: "MclAppGroups");

            migrationBuilder.DropTable(
                name: "MclAppPermissions");

            migrationBuilder.DropTable(
                name: "MclAppUsers");

            migrationBuilder.DropTable(
                name: "MclApplications");
        }
    }
}
