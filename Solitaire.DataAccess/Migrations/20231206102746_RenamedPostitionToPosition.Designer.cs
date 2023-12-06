﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Solitaire.DataAccess.Context;

#nullable disable

namespace Solitaire.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231206102746_RenamedPostitionToPosition")]
    partial class RenamedPostitionToPosition
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Solitaire.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Base64Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int?>("SolitaireSessionId")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SolitaireSessionId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Solitaire.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Flipped")
                        .HasColumnType("boolean");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SolitaireSessionId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SolitaireSessionId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Solitaire.Models.Draw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FromPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SolitaireSessionId")
                        .HasColumnType("integer");

                    b.Property<int>("Sort")
                        .HasColumnType("integer");

                    b.Property<string>("ToPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("WasFlipped")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("SolitaireSessionId");

                    b.ToTable("Draws");
                });

            modelBuilder.Entity("Solitaire.Models.SolitaireSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("SessionStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("SolitaireSessions");
                });

            modelBuilder.Entity("Solitaire.Models.ApplicationUser", b =>
                {
                    b.HasOne("Solitaire.Models.SolitaireSession", "SolitaireSession")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("Solitaire.Models.ApplicationUser", "SolitaireSessionId");

                    b.Navigation("SolitaireSession");
                });

            modelBuilder.Entity("Solitaire.Models.Card", b =>
                {
                    b.HasOne("Solitaire.Models.SolitaireSession", "SolitaireSession")
                        .WithMany("Cards")
                        .HasForeignKey("SolitaireSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SolitaireSession");
                });

            modelBuilder.Entity("Solitaire.Models.Draw", b =>
                {
                    b.HasOne("Solitaire.Models.SolitaireSession", "SolitaireSession")
                        .WithMany("Draws")
                        .HasForeignKey("SolitaireSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SolitaireSession");
                });

            modelBuilder.Entity("Solitaire.Models.SolitaireSession", b =>
                {
                    b.Navigation("ApplicationUser");

                    b.Navigation("Cards");

                    b.Navigation("Draws");
                });
#pragma warning restore 612, 618
        }
    }
}
