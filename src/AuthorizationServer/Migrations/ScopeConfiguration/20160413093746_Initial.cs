using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace AuthorizationServer.Migrations.ScopeConfiguration
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AllowUnrestrictedIntrospection = table.Column<bool>(nullable: false),
                    ClaimsRule = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Emphasize = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    IncludeAllClaimsForUser = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope<Guid>", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "ScopeClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlwaysIncludeInIdToken = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopeClaim<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScopeClaim<Guid>_Scope<Guid>_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ScopeSecrets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true),
                    ScopeId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopeSecret<Guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScopeSecret<Guid>_Scope<Guid>_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ScopeClaims");
            migrationBuilder.DropTable("ScopeSecrets");
            migrationBuilder.DropTable("Scopes");
        }
    }
}
