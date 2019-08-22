﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VegankoService.Data;

namespace VegankoService.Migrations
{
    [DbContext(typeof(VegankoContext))]
    [Migration("20190813222858_product-img-length")]
    partial class productimglength
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VegankoService.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProductId");

                    b.Property<int>("Rating");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("UtcDatePosted");

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("VegankoService.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode");

                    b.Property<string>("Brand");

                    b.Property<string>("Description");

                    b.Property<string>("ImageName");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(16777215);

                    b.Property<string>("Name");

                    b.Property<int>("ProductClassifiers");

                    b.Property<int>("Rating");

                    b.Property<int>("State");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });
#pragma warning restore 612, 618
        }
    }
}