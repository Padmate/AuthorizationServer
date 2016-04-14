using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using AuthorizationServer.Models;

namespace AuthorizationServer.Migrations.ScopeConfiguration
{
    [DbContext(typeof(ScopeConfigurationContext))]
    [Migration("20160413093746_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.Scope<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowUnrestrictedIntrospection");

                    b.Property<string>("ClaimsRule")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("Emphasize");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("IncludeAllClaimsForUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("Required");

                    b.Property<bool>("ShowInDiscoveryDocument");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Scopes");
                });

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.ScopeClaim<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AlwaysIncludeInIdToken");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<Guid?>("ScopeId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "ScopeClaims");
                });

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.ScopeSecret<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<Guid?>("ScopeId");

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 250);

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "ScopeSecrets");
                });

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.ScopeClaim<System.Guid>", b =>
                {
                    b.HasOne("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.Scope<System.Guid>")
                        .WithMany()
                        .HasForeignKey("ScopeId");
                });

            modelBuilder.Entity("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.ScopeSecret<System.Guid>", b =>
                {
                    b.HasOne("TwentyTwenty.IdentityServer4.EntityFramework7.Entities.Scope<System.Guid>")
                        .WithMany()
                        .HasForeignKey("ScopeId");
                });
        }
    }
}
