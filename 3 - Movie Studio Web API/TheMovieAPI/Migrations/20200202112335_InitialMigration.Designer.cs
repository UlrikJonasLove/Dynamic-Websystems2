﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheMovieAPI.Models;

namespace TheMovieAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20200202112335_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheMovieAPI.Models.Movies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentedOut")
                        .HasColumnType("int");

                    b.Property<string>("Trivia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Available = true,
                            Count = 3,
                            Description = "The Survivalist Hugh Glass is hunting the man who left him for dead, after a bear attack.",
                            Name = "The Revenant",
                            RatedBy = "",
                            RentedOut = 0,
                            Trivia = ""
                        },
                        new
                        {
                            Id = 2,
                            Available = true,
                            Count = 3,
                            Description = "A young man, called Amsterdam is looking for revange after his father was murded in a gang war by Bill The Butcher",
                            Name = "Gangs Of New York",
                            RatedBy = "",
                            RentedOut = 0,
                            Trivia = ""
                        },
                        new
                        {
                            Id = 3,
                            Available = true,
                            Count = 3,
                            Description = "A love story in ancient time. Achilles, Hector, Paris.",
                            Name = "Troy",
                            RatedBy = "",
                            RentedOut = 0,
                            Trivia = ""
                        },
                        new
                        {
                            Id = 4,
                            Available = true,
                            Count = 3,
                            Description = "The outlaw Peter Quill is the savior of the galaxy, along side the other guardians.",
                            Name = "Guardians Of The Galaxy",
                            RatedBy = "",
                            RentedOut = 0,
                            Trivia = ""
                        },
                        new
                        {
                            Id = 5,
                            Available = true,
                            Count = 3,
                            Description = "The next installment of the guardians saga. This time, Peter Quill learns more about his dangerous roots.",
                            Name = "Guardians Of The Galaxy Vol 2",
                            RatedBy = "",
                            RentedOut = 0,
                            Trivia = ""
                        });
                });

            modelBuilder.Entity("TheMovieAPI.Models.Ratings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("StudioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("StudioId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("TheMovieAPI.Models.Studios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MoviesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MoviesId");

                    b.ToTable("Studio");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Jönköping",
                            Name = "Filmstaden"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Stockholm",
                            Name = "Stockholms Skärgård filmstudio"
                        },
                        new
                        {
                            Id = 3,
                            Location = "Lund",
                            Name = "Skåne studio"
                        });
                });

            modelBuilder.Entity("TheMovieAPI.Models.Ratings", b =>
                {
                    b.HasOne("TheMovieAPI.Models.Movies", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheMovieAPI.Models.Studios", "Studio")
                        .WithMany("Ratings")
                        .HasForeignKey("StudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheMovieAPI.Models.Studios", b =>
                {
                    b.HasOne("TheMovieAPI.Models.Movies", null)
                        .WithMany("Studios")
                        .HasForeignKey("MoviesId");
                });
#pragma warning restore 612, 618
        }
    }
}