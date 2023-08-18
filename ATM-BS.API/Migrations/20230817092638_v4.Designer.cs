﻿// <auto-generated />
using System;
using ATM_BS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATM_BS.API.Migrations
{
    [DbContext(typeof(ATMBSDbContext))]
    [Migration("20230817092638_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ATM_BS.API.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<long>("Contact")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ATM_BS.API.Entities.Balance", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("float");

                    b.HasKey("AccountNumber");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("ATM_BS.API.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("BalanceAccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionAccountNumber")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("BalanceAccountNumber");

                    b.HasIndex("TransactionAccountNumber");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ATM_BS.API.Entities.Transaction", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<DateTime>("TransactionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.HasKey("AccountNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ATM_BS.API.Entities.Customer", b =>
                {
                    b.HasOne("ATM_BS.API.Entities.Balance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceAccountNumber");

                    b.HasOne("ATM_BS.API.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionAccountNumber");

                    b.Navigation("Balance");

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
