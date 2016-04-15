using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using AuthorizationServer.Models;

namespace AuthorizationServer.Migrations.Operational
{
    [DbContext(typeof(OperationalContext))]
    partial class OperationalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.Consent", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("ClientId")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Scopes")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 2000);

                    b.HasKey("SubjectId", "ClientId");

                    b.HasAnnotation("Relational:TableName", "Consents");
                });

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.Token", b =>
                {
                    b.Property<string>("Key");

                    b.Property<short>("TokenType");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<DateTime>("Expiry");

                    b.Property<string>("JsonCode")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "varchar(max)");

                    b.Property<string>("SubjectId")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("Key", "TokenType");

                    b.HasAnnotation("Relational:TableName", "Tokens");
                });
        }
    }
}
