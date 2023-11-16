﻿// <auto-generated />
using System;
using HeroisApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HeroisApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HeroisApi.Models.Heroi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Altura")
                        .HasColumnType("double");

                    b.Property<DateOnly?>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<string>("NomeHeroi")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<double>("Peso")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("NomeHeroi")
                        .IsUnique();

                    b.ToTable("Herois");
                });

            modelBuilder.Entity("HeroisApi.Models.HeroisSuperPoderes", b =>
                {
                    b.Property<int>("SuperPoderesId")
                        .HasColumnType("int");

                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.HasKey("SuperPoderesId", "HeroiId");

                    b.HasIndex("HeroiId");

                    b.ToTable("HeroisSuperPoderes");
                });

            modelBuilder.Entity("HeroisApi.Models.SuperPoderes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("SuperPoder")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SuperPoderes");
                });

            modelBuilder.Entity("HeroisApi.Models.HeroisSuperPoderes", b =>
                {
                    b.HasOne("HeroisApi.Models.Heroi", "Heroi")
                        .WithMany("HeroiSuperPoderes")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroisApi.Models.SuperPoderes", "SuperPoderes")
                        .WithMany("HeroiSuperPoderes")
                        .HasForeignKey("SuperPoderesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");

                    b.Navigation("SuperPoderes");
                });

            modelBuilder.Entity("HeroisApi.Models.Heroi", b =>
                {
                    b.Navigation("HeroiSuperPoderes");
                });

            modelBuilder.Entity("HeroisApi.Models.SuperPoderes", b =>
                {
                    b.Navigation("HeroiSuperPoderes");
                });
#pragma warning restore 612, 618
        }
    }
}
