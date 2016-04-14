using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace AuthorizationServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    AllowAccessToAllGrantTypes = table.Column<bool>(nullable: false),
                    AllowAccessToAllScopes = table.Column<bool>(nullable: false),
                    AllowClientCredentialsOnly = table.Column<bool>(nullable: false),
                    AllowPromptNone = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    ClientName = table.Column<string>(nullable: false),
                    ClientUri = table.Column<string>(nullable: true),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Flow = table.Column<int>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    LogoUri = table.Column<string>(nullable: true),
                    LogoutSessionRequired = table.Column<bool>(nullable: false),
                    LogoutUri = table.Column<string>(nullable: true),
                    PrefixClientClaims = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    RequireConsent = table.Column<bool>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    UpdateAccessTokenOnRefresh = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client<Guid>", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "ClientClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaim<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientClaim<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ClientCorsOrigins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Origin = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigin<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCorsOrigin<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientCustomGrantTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    GrantType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCustomGrantType<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCustomGrantType<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUris",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Uri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUri<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPostLogoutRedirectUri<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientProviderRestrictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Provider = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProviderRestriction<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProviderRestriction<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientRedirectUris",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Uri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUri<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRedirectUri<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Scope = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScope<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ClientSecrets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecret<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSecret<Guid>_Client<Guid>_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Client<Guid>_ClientId",
                table: "Clients",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ClientClaims");
            migrationBuilder.DropTable("ClientCorsOrigins");
            migrationBuilder.DropTable("ClientCustomGrantTypes");
            migrationBuilder.DropTable("ClientPostLogoutRedirectUris");
            migrationBuilder.DropTable("ClientProviderRestrictions");
            migrationBuilder.DropTable("ClientRedirectUris");
            migrationBuilder.DropTable("ClientScopes");
            migrationBuilder.DropTable("ClientSecrets");
            migrationBuilder.DropTable("Clients");
        }
    }
}
