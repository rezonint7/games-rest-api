﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEB_API.Data;

#nullable disable

namespace WEB_API.Migrations
{
    [DbContext(typeof(GamesDbContext))]
    partial class GamesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("WEB_API.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WEB_API.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("WEB_API.Models.Info", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("Info");
                });

            modelBuilder.Entity("WEB_API.Models.SystemRequirements", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GPU")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HDD")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OS")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RAM")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("SystemRequirements");
                });

            modelBuilder.Entity("WEB_API.Models.Game", b =>
                {
                    b.HasOne("WEB_API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WEB_API.Models.Info", b =>
                {
                    b.HasOne("WEB_API.Models.Game", "Game")
                        .WithOne("Info")
                        .HasForeignKey("WEB_API.Models.Info", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("WEB_API.Models.SystemRequirements", b =>
                {
                    b.HasOne("WEB_API.Models.Game", "Game")
                        .WithOne("SystemRequirements")
                        .HasForeignKey("WEB_API.Models.SystemRequirements", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("WEB_API.Models.Game", b =>
                {
                    b.Navigation("Info");

                    b.Navigation("SystemRequirements");
                });
#pragma warning restore 612, 618
        }
    }
}
