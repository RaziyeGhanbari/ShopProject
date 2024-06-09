﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopProject.Data;

#nullable disable

namespace ShopProject.Migrations
{
    [DbContext(typeof(ShopProjectContext))]
    [Migration("20240529104815_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("ShopProject.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "پوشیدنی",
                            ParentId = 1
                        });
                });

            modelBuilder.Entity("ShopProject.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("ShopProject.Models.FieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FieldId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("ProductId");

                    b.ToTable("FieldValue");
                });

            modelBuilder.Entity("ShopProject.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShopProject.Models.Category", b =>
                {
                    b.HasOne("ShopProject.Models.Category", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ShopProject.Models.Field", b =>
                {
                    b.HasOne("ShopProject.Models.Category", "Category")
                        .WithMany("Fields")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShopProject.Models.FieldValue", b =>
                {
                    b.HasOne("ShopProject.Models.Field", "Field")
                        .WithMany("FieldValues")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopProject.Models.Product", "Product")
                        .WithMany("FieldValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopProject.Models.Product", b =>
                {
                    b.HasOne("ShopProject.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShopProject.Models.Category", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShopProject.Models.Field", b =>
                {
                    b.Navigation("FieldValues");
                });

            modelBuilder.Entity("ShopProject.Models.Product", b =>
                {
                    b.Navigation("FieldValues");
                });
#pragma warning restore 612, 618
        }
    }
}
