﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuotationWritingSystem.Data;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250131122311_AddAutoIncrementToId")]
    partial class AddAutoIncrementToId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuotationWritingSystem.Models.ABMaterialMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ABCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Cost")
                        .HasColumnType("float");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("OtherRate")
                        .HasColumnType("float");

                    b.Property<double?>("Rate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ABMaterialMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.AddressMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Prefecture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AddressMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CalculationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuxiliaryWorkRate")
                        .HasColumnType("int");

                    b.Property<int>("CableAdditionalRate")
                        .HasColumnType("int");

                    b.Property<int>("CableRackAccessoryRate")
                        .HasColumnType("int");

                    b.Property<int>("CableRackSupportRate")
                        .HasColumnType("int");

                    b.Property<int>("CostRate")
                        .HasColumnType("int");

                    b.Property<int>("LightingAdditionalRate")
                        .HasColumnType("int");

                    b.Property<int>("OverheadRate")
                        .HasColumnType("int");

                    b.Property<int>("PanelAdditionalRate")
                        .HasColumnType("int");

                    b.Property<bool>("PerformAuxiliaryWorks")
                        .HasColumnType("bit");

                    b.Property<int>("PipeAccessoryRate")
                        .HasColumnType("int");

                    b.Property<int>("PipeSupportRate")
                        .HasColumnType("int");

                    b.Property<int>("RacewayAccessoryRate")
                        .HasColumnType("int");

                    b.Property<int>("RacewaySupportRate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CalculationData", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CategoryMaterialMaster1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryMaterialMaster_1", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CategoryMaterialMaster2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Category1")
                        .HasColumnType("int");

                    b.Property<int?>("Category2")
                        .HasColumnType("int");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryMaterialMaster_2", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CategoryMaterialMaster3", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Category1")
                        .HasColumnType("int");

                    b.Property<int?>("Category2")
                        .HasColumnType("int");

                    b.Property<int?>("Category3")
                        .HasColumnType("int");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryMaterialMaster_3", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CategoryMaterialMaster4", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Category1")
                        .HasColumnType("int");

                    b.Property<int?>("Category2")
                        .HasColumnType("int");

                    b.Property<int?>("Category3")
                        .HasColumnType("int");

                    b.Property<int?>("Category4")
                        .HasColumnType("int");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryMaterialMaster_4", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.ConstructionMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SiteMiscell")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ConstructionMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.CustomerMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CloseingDat")
                        .HasColumnType("int");

                    b.Property<string>("Creater")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.MaterialMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ABC_Materia")
                        .HasColumnType("int");

                    b.Property<double?>("AccessoryRa")
                        .HasColumnType("float");

                    b.Property<double?>("Accumulated")
                        .HasColumnType("float");

                    b.Property<double?>("BuildingCos")
                        .HasColumnType("float");

                    b.Property<int?>("Category1")
                        .HasColumnType("int");

                    b.Property<int?>("Category2")
                        .HasColumnType("int");

                    b.Property<int?>("Category3")
                        .HasColumnType("int");

                    b.Property<string>("CategoryNam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CeilingOpen")
                        .HasColumnType("float");

                    b.Property<double?>("CompositeCo")
                        .HasColumnType("float");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<double?>("ElectricalI")
                        .HasColumnType("float");

                    b.Property<double?>("ExternalCos")
                        .HasColumnType("float");

                    b.Property<double?>("InternalCos")
                        .HasColumnType("float");

                    b.Property<int?>("LaborCostA")
                        .HasColumnType("int");

                    b.Property<int?>("LaborCostB")
                        .HasColumnType("int");

                    b.Property<string>("MaterialCat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialCat1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Miscellaneo")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Other")
                        .HasColumnType("float");

                    b.Property<double?>("RemovalRate")
                        .HasColumnType("float");

                    b.Property<double?>("RemovalRate1")
                        .HasColumnType("float");

                    b.Property<double?>("StepRateA")
                        .HasColumnType("float");

                    b.Property<double?>("StepRateB")
                        .HasColumnType("float");

                    b.Property<double?>("SupplyRate")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updater")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MaterialMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.PrefectureMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrefectureMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.QuotationCalc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ABMethod")
                        .HasColumnType("bit");

                    b.Property<decimal>("CableNetRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CableReplenishmentRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LaborBasisRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LaborRateA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LaborRateB")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Minority")
                        .HasColumnType("bit");

                    b.Property<decimal>("MiscellRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SiteMiscellRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TubeNetRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TubeReplenishmentRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("QuotationCalc", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.QuotationMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creater")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Export")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Import")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Square")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QuotationMain", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.QuotationMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Divide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StepRate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QuotationMaterial", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.QuotationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Calculate")
                        .HasColumnType("bit");

                    b.Property<string>("Category1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("RemovalRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("QuotationType", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.RankMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<double?>("LaborCostA")
                        .HasColumnType("float");

                    b.Property<double?>("LaborCostB")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("OtherExpens")
                        .HasColumnType("float");

                    b.Property<double?>("SiteMiscell")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RankMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.UnitMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UnitMaster", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("QuotationWritingSystem.Models.YearMaster", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StartYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("YearMaster", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
