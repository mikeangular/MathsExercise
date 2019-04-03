﻿// <auto-generated />
using System;
using MathsExercise.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MathsExercise.Migrations
{
    [DbContext(typeof(MEDBContext))]
    partial class MEDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MathsExercise.Models.MathsExercises", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Formula")
                        .HasMaxLength(500);

                    b.Property<string>("RightAnswer")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("SaveTime");

                    b.Property<int>("SettingId");

                    b.Property<string>("UserAnswer")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.HasIndex("SettingId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("MathsExercise.Models.Setting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUIDValue")
                        .HasMaxLength(36);

                    b.Property<int>("MaxValue");

                    b.Property<string>("Operations")
                        .HasMaxLength(4);

                    b.Property<int>("QuantityOfOperation");

                    b.HasKey("ID");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("MathsExercise.Models.MathsExercises", b =>
                {
                    b.HasOne("MathsExercise.Models.Setting", "_setting")
                        .WithMany("_mathsExercises")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
