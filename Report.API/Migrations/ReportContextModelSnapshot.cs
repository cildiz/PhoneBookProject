﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Report.API.Contexts;

#nullable disable

namespace Report.API.Migrations
{
    [DbContext(typeof(ReportContext))]
    partial class ReportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReportStatus")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Report.API.Entities.ReportDetail", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonCount")
                        .HasColumnType("integer");

                    b.Property<int>("PhoneNumberCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReportUUID")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("ReportUUID");

                    b.ToTable("ReportDetails");
                });

            modelBuilder.Entity("Report.API.Entities.ReportDetail", b =>
                {
                    b.HasOne("Report.API.Entities.Report", null)
                        .WithMany("ReportDetails")
                        .HasForeignKey("ReportUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Navigation("ReportDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
