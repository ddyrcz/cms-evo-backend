﻿// <auto-generated />
using System;
using CMS.Cars.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CMS.Cars.Infrastructure.Migrations
{
    [DbContext(typeof(CarsDbContext))]
    [Migration("20181128212930_Change_Oc_Column_Type")]
    partial class Change_Oc_Column_Type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMS.Cars.Domain.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AcExpiry");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LiftUdtExpiry");

                    b.Property<DateTime>("OcExpiry")
                        .HasColumnType("date");

                    b.Property<string>("RegistrationNumber");

                    b.Property<DateTime?>("TachoLegalizationExpiry");

                    b.Property<DateTime>("TermTechnicalResearch");

                    b.Property<string>("VinNumber");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
