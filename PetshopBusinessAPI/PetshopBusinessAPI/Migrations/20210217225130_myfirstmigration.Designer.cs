﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetshopDB.Models;

namespace PetshopBusinessAPI.Migrations
{
    [DbContext(typeof(PetshopDbContext))]
    [Migration("20210217225130_myfirstmigration")]
    partial class myfirstmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PetshopDB.Models.ClientUser", b =>
                {
                    b.Property<int>("ClientUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfirmationCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Login")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("ClientUserId");

                    b.ToTable("ClientUser");
                });
#pragma warning restore 612, 618
        }
    }
}
