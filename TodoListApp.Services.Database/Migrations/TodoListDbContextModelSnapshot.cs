﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoListApp.Services.Database;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    [DbContext(typeof(TodoListDbContext))]
    partial class TodoListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TagEntityTodoTaskEntity", b =>
                {
                    b.Property<long>("TagEntitiesId")
                        .HasColumnType("bigint");

                    b.Property<long>("todoTaskEntitiesId")
                        .HasColumnType("bigint");

                    b.HasKey("TagEntitiesId", "todoTaskEntitiesId");

                    b.HasIndex("todoTaskEntitiesId");

                    b.ToTable("TagEntityTodoTaskEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.EditorEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("TodoListEntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TodoListEntityId");

                    b.ToTable("Editors");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TagEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

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

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskCommentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<long>("todoTaskEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("todoTaskEntityId");

                    b.ToTable("TodoTaskCommentEntities");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Assignee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<long>("TodoListEntityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TodoListEntityId");

                    b.ToTable("TodoTaskEntities");
                });

            modelBuilder.Entity("TagEntityTodoTaskEntity", b =>
                {
                    b.HasOne("TodoListApp.Services.Database.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("TagEntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TodoListApp.Services.Database.TodoTaskEntity", null)
                        .WithMany()
                        .HasForeignKey("todoTaskEntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoListApp.Services.Database.EditorEntity", b =>
                {
                    b.HasOne("TodoListApp.Services.Database.TodoListEntity", "TodoListEntity")
                        .WithMany("Editors")
                        .HasForeignKey("TodoListEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoListEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskCommentEntity", b =>
                {
                    b.HasOne("TodoListApp.Services.Database.TodoTaskEntity", "todoTaskEntity")
                        .WithMany("TodoTaskCommentEntities")
                        .HasForeignKey("todoTaskEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("todoTaskEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskEntity", b =>
                {
                    b.HasOne("TodoListApp.Services.Database.TodoListEntity", "TodoListEntity")
                        .WithMany("TodoTaskEntities")
                        .HasForeignKey("TodoListEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoListEntity");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoListEntity", b =>
                {
                    b.Navigation("Editors");

                    b.Navigation("TodoTaskEntities");
                });

            modelBuilder.Entity("TodoListApp.Services.Database.TodoTaskEntity", b =>
                {
                    b.Navigation("TodoTaskCommentEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
