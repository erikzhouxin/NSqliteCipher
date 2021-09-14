﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWPFUI.SQLiteCipher.Repository;

namespace TestWPFUI.SQLiteCipher.Repository.LocalDatabase
{
    [DbContext(typeof(LocalDatabaseContext))]
    [Migration("20210909074621_AddTestKey")]
    partial class AddTestKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("TestWPFUI.SQLiteCipher.Models.LocalTestEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("local_test");
                });
#pragma warning restore 612, 618
        }
    }
}
