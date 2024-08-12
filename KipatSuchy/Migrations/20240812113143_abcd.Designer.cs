﻿// <auto-generated />
using System;
using KipatSuchy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KipatSuchy.Migrations
{
    [DbContext(typeof(KipatSuchyContext))]
    [Migration("20240812113143_abcd")]
    partial class abcd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KipatSuchy.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LeftMissile")
                        .HasColumnType("int");

                    b.Property<string>("Protector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("KipatSuchy.Models.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ThreatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("ThreatId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("KipatSuchy.Models.Threat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LaunchTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weapon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("puzaz")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Threats");
                });

            modelBuilder.Entity("KipatSuchy.Models.Response", b =>
                {
                    b.HasOne("KipatSuchy.Models.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KipatSuchy.Models.Threat", "Threat")
                        .WithMany()
                        .HasForeignKey("ThreatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("Threat");
                });
#pragma warning restore 612, 618
        }
    }
}
