﻿// <auto-generated />
using lab1_ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace lab4_igi.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20180515190047_HotelMigration")]
    partial class HotelMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("lab1_ef.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DepartureDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("OccupancyDate");

                    b.Property<string>("Passport");

                    b.Property<int?>("RoomId");

                    b.HasKey("ClientId");

                    b.HasIndex("RoomId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("lab1_ef.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientId");

                    b.Property<string>("Name");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ClientId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("lab1_ef.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<double>("Cost");

                    b.Property<DateTime>("CostDate");

                    b.Property<string>("Description");

                    b.Property<string>("RoomNo");

                    b.Property<int?>("RoomTypeId");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("lab1_ef.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("lab1_ef.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientId");

                    b.Property<int?>("EmployeeId");

                    b.Property<int?>("ServiceTypeId");

                    b.HasKey("ServiceId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("lab1_ef.ServiceType", b =>
                {
                    b.Property<int>("ServiceTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ServiceTypeId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("lab1_ef.Client", b =>
                {
                    b.HasOne("lab1_ef.Room", "Room")
                        .WithMany("Clients")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("lab1_ef.Employee", b =>
                {
                    b.HasOne("lab1_ef.Client")
                        .WithMany("ServeEmployees")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("lab1_ef.Room", b =>
                {
                    b.HasOne("lab1_ef.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId");
                });

            modelBuilder.Entity("lab1_ef.Service", b =>
                {
                    b.HasOne("lab1_ef.Client", "Client")
                        .WithMany("Services")
                        .HasForeignKey("ClientId");

                    b.HasOne("lab1_ef.Employee", "Employee")
                        .WithMany("Services")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("lab1_ef.ServiceType", "ServiceType")
                        .WithMany("Services")
                        .HasForeignKey("ServiceTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
