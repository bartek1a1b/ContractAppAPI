﻿// <auto-generated />
using System;
using ContractAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContractAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240130111310_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContractAppAPI.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContractNumber")
                        .HasColumnType("int");

                    b.Property<int>("ContractTypeOneId")
                        .HasColumnType("int");

                    b.Property<int>("ContractTypeTwoId")
                        .HasColumnType("int");

                    b.Property<string>("Contractor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfConclusion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pdf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signatory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ContractTypeOneId");

                    b.HasIndex("ContractTypeTwoId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ContractAppAPI.Models.ContractTypeOne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContractTypeOnes");
                });

            modelBuilder.Entity("ContractAppAPI.Models.ContractTypeTwo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContractTypeTwos");
                });

            modelBuilder.Entity("ContractAppAPI.Models.Contract", b =>
                {
                    b.HasOne("ContractAppAPI.Models.ContractTypeOne", "ContractTypeOne")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractTypeOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContractAppAPI.Models.ContractTypeTwo", "ContractTypeTwo")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractTypeTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContractTypeOne");

                    b.Navigation("ContractTypeTwo");
                });

            modelBuilder.Entity("ContractAppAPI.Models.ContractTypeOne", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("ContractAppAPI.Models.ContractTypeTwo", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
