﻿// <auto-generated />
using System;
using Database.Entity.AnimalFiveHead;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Entity.AnimalFiveHead.Migrations
{
    [DbContext(typeof(AnimalFiveHeadContext))]
    partial class AnimalFiveHeadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Entity.AnimalFiveHead.Models.GameSessionState", b =>
                {
                    b.Property<int>("GameStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GameStateId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameStateId"), 1L, 1);

                    b.Property<string>("GameStateName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("GameStateName");

                    b.HasKey("GameStateId");

                    b.ToTable("tb_GameSessionState", (string)null);
                });

            modelBuilder.Entity("Database.Entity.AnimalFiveHead.Models.PlayerSessionInformation", b =>
                {
                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SessionId");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("PlayerId");

                    b.Property<string>("CardIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("CardIds");

                    b.Property<string>("Cards")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("Cards");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasMaxLength(10)
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("DateTimeAdded");

                    b.Property<DateTime?>("DateTimeUpdated")
                        .HasMaxLength(10)
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("DateTimeUpdated");

                    b.Property<string>("GameResult")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("GameResult");

                    b.Property<string>("GameSession")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("GameSession");

                    b.Property<int>("Score")
                        .HasColumnType("int")
                        .HasColumnName("Score");

                    b.HasKey("SessionId", "PlayerId")
                        .HasName("PK_PlayerSessionInformation");

                    b.ToTable("tb_PlayerSessionInformation", (string)null);
                });

            modelBuilder.Entity("Database.Entity.AnimalFiveHead.Models.PlayerType", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"), 1L, 1);

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("PlayerName");

                    b.HasKey("PlayerId");

                    b.ToTable("tb_PlayerType", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
