﻿// <auto-generated />
using System;
using ChatMe.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatMe.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20210806060024_SeedingUsers")]
    partial class SeedingUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ChatMe")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChatMe.Infrastructure.Data.Configurations.DomainMessagePersistence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(178)
                        .HasColumnType("nvarchar(178)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatMe.Infrastructure.Data.Configurations.DomainUserPersistence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("26f40fdf-8f92-4c4f-80c1-71090d86aef4"),
                            Password = "10edaa2ccab4a337894a84fae672038702658567",
                            Username = "Will"
                        },
                        new
                        {
                            Id = new Guid("1b0f8308-feb0-4d55-93ec-0765971e0bb7"),
                            Password = "10edaa2ccab4a337894a84fae672038702658567",
                            Username = "Test"
                        });
                });

            modelBuilder.Entity("ChatMe.Infrastructure.Data.Configurations.DomainMessagePersistence", b =>
                {
                    b.HasOne("ChatMe.Infrastructure.Data.Configurations.DomainUserPersistence", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
