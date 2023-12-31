﻿// <auto-generated />
using System;
using Hjc.TranslatorDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hjc.TranslatorDomain.Migrations
{
    [DbContext(typeof(TranslatorDbContext))]
    partial class TranslatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hjc.TranslatorDomain.ChineseWord", b =>
                {
                    b.Property<long>("ChineseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ChineseId"));

                    b.Property<long?>("EnglishWordEnglishId")
                        .HasColumnType("bigint");

                    b.Property<string>("Word")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ChineseId");

                    b.HasIndex("EnglishWordEnglishId");

                    b.ToTable("ChineseWords", (string)null);
                });

            modelBuilder.Entity("Hjc.TranslatorDomain.EnglishWord", b =>
                {
                    b.Property<long>("EnglishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EnglishId"));

                    b.Property<string>("Explain")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Word")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("EnglishId");

                    b.ToTable("EnglishWords", (string)null);
                });

            modelBuilder.Entity("Hjc.TranslatorDomain.ChineseWord", b =>
                {
                    b.HasOne("Hjc.TranslatorDomain.EnglishWord", "EnglishWord")
                        .WithMany()
                        .HasForeignKey("EnglishWordEnglishId");

                    b.Navigation("EnglishWord");
                });
#pragma warning restore 612, 618
        }
    }
}
