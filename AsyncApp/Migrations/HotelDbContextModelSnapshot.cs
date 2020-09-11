﻿// <auto-generated />
using AsyncApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AsyncApp.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    partial class HotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AsyncApp.Models.Amenities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "WiFi"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Air Conditioning"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Coffee Maker"
                        });
                });

            modelBuilder.Entity("AsyncApp.Models.Hotel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAdress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotel");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            City = "Des Moines",
                            Name = "Async Inn",
                            Phone = "515-555-1234",
                            State = "Iowa",
                            StreetAdress = "123 Sync Street"
                        },
                        new
                        {
                            Id = 2L,
                            City = "Ames",
                            Name = "Async Motel",
                            Phone = "515-294-1111",
                            State = "Iowa",
                            StreetAdress = "1858 Cyclone Lane"
                        },
                        new
                        {
                            Id = 3L,
                            City = "Pleasant Hill",
                            Name = "Async Suites",
                            Phone = "515-299-1234",
                            State = "Iowa",
                            StreetAdress = "5050 Morning Star Court"
                        });
                });

            modelBuilder.Entity("AsyncApp.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Room");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 0,
                            Name = "Studio"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 1,
                            Name = "OneBedroom"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 2,
                            Name = "TwoBedroom"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}