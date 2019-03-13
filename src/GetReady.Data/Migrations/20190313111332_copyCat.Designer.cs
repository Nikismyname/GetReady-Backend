﻿// <auto-generated />
using System;
using GetReady.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GetReady.Data.Migrations
{
    [DbContext(typeof(GetReadyDbContext))]
    [Migration("20190313111332_copyCat")]
    partial class copyCat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.GlobalQuestionPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<bool>("Approved");

                    b.Property<int>("Column");

                    b.Property<string>("Comment");

                    b.Property<int?>("DerivedFromId");

                    b.Property<int>("Difficulty");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<string>("Question");

                    b.Property<int>("QuestionSheetId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionSheetId");

                    b.ToTable("GlobalQuestionPackages");
                });

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.PersonalQuestionPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<float>("AnswerRate");

                    b.Property<int>("Column");

                    b.Property<string>("Comment");

                    b.Property<int?>("DerivedFromId");

                    b.Property<int>("Difficulty");

                    b.Property<string>("LatestScores");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<string>("Question");

                    b.Property<int>("QuestionSheetId");

                    b.Property<int>("TimesBeingAnswered");

                    b.Property<string>("YourBestAnswer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionSheetId");

                    b.ToTable("PersonalQuestionPackages");
                });

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.QuestionSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("Difficulty");

                    b.Property<int>("Importance");

                    b.Property<bool>("IsGlobal");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<int?>("QuestionSheetId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionSheetId");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionSheets");
                });

            modelBuilder.Entity("GetReady.Data.Models.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("HashedPassword");

                    b.Property<string>("LastName");

                    b.Property<string>("Role");

                    b.Property<string>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.GlobalQuestionPackage", b =>
                {
                    b.HasOne("GetReady.Data.Models.QuestionModels.QuestionSheet", "QuestionSheet")
                        .WithMany("GlobalQuestions")
                        .HasForeignKey("QuestionSheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.PersonalQuestionPackage", b =>
                {
                    b.HasOne("GetReady.Data.Models.QuestionModels.QuestionSheet", "QuestionSheet")
                        .WithMany("PersonalQuestions")
                        .HasForeignKey("QuestionSheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GetReady.Data.Models.QuestionModels.QuestionSheet", b =>
                {
                    b.HasOne("GetReady.Data.Models.QuestionModels.QuestionSheet", "QestionSheet")
                        .WithMany("Children")
                        .HasForeignKey("QuestionSheetId");

                    b.HasOne("GetReady.Data.Models.UserModels.User", "User")
                        .WithMany("QuestionSheets")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
