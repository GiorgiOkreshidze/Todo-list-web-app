﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    [DbContext(typeof(TodoListDbContext))]
    [Migration("20240121193610_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TodoListApp.Services.Database.TodoListEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TodoListEntities");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("todoListEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("todoListEntityId");

                    b.ToTable("TodoTaskEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskEntity", b =>
                {
                    b.HasOne("TodoListApp.Services.Database.TodoListEntity", "todoListEntity")
                        .WithMany("todoTaskEntities")
                        .HasForeignKey("todoListEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("todoListEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoListEntity", b =>
                {
                    b.Navigation("todoTaskEntities");
                });
#pragma warning restore 612, 618
        }
    }
}