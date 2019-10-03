﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Data.Migrations
{
    [DbContext(typeof(CycleTogetherDbContext))]
    [Migration("20191003065219_update-constraints")]
    partial class updateconstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.EquipmentEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("DAL.Models.PictureEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path");

                    b.Property<string>("PublicId")
                        .IsRequired();

                    b.Property<Guid>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("DAL.Models.RouteEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Destination")
                        .IsRequired();

                    b.Property<int>("Difficulty");

                    b.Property<int>("Endurance");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("StartPoint")
                        .IsRequired();

                    b.Property<DateTime>("StartTime");

                    b.Property<bool>("SuitableForKids");

                    b.Property<int>("Terrain");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("DAL.Models.RouteEquipmentEntry", b =>
                {
                    b.Property<Guid>("EquipmentId");

                    b.Property<Guid>("RouteId");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("EquipmentId", "RouteId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteEquipments");
                });

            modelBuilder.Entity("DAL.Models.UserEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Difficulty");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("Endurance");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Level");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Terrain");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserEquipmentEntry", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("EquipmentId");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("UserId", "EquipmentId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("EquipmentId");

                    b.ToTable("UserEquipments");
                });

            modelBuilder.Entity("DAL.Models.UserRouteEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RouteId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoutes");
                });

            modelBuilder.Entity("DAL.Models.PictureEntry", b =>
                {
                    b.HasOne("DAL.Models.RouteEntry", "Route")
                        .WithMany("Pictures")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.RouteEntry", b =>
                {
                    b.HasOne("DAL.Models.UserEntry", "User")
                        .WithMany("Routes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.RouteEquipmentEntry", b =>
                {
                    b.HasOne("DAL.Models.EquipmentEntry", "Equipment")
                        .WithMany("RouteEquipments")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.RouteEntry", "Route")
                        .WithMany("RouteEquipments")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.UserEquipmentEntry", b =>
                {
                    b.HasOne("DAL.Models.EquipmentEntry", "Equipment")
                        .WithMany("UserEquipments")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.UserEntry", "User")
                        .WithMany("UserEquipments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.UserRouteEntry", b =>
                {
                    b.HasOne("DAL.Models.RouteEntry", "Route")
                        .WithMany("UserRoutes")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.UserEntry", "User")
                        .WithMany("UserRoutes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
