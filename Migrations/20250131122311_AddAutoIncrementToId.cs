using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAutoIncrementToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ABMaterialMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ABCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherRate = table.Column<double>(type: "float", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABMaterialMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode = table.Column<int>(type: "int", nullable: true),
                    Prefecture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PipeAccessoryRate = table.Column<int>(type: "int", nullable: false),
                    PipeSupportRate = table.Column<int>(type: "int", nullable: false),
                    CableRackAccessoryRate = table.Column<int>(type: "int", nullable: false),
                    CableRackSupportRate = table.Column<int>(type: "int", nullable: false),
                    RacewayAccessoryRate = table.Column<int>(type: "int", nullable: false),
                    RacewaySupportRate = table.Column<int>(type: "int", nullable: false),
                    CableAdditionalRate = table.Column<int>(type: "int", nullable: false),
                    LightingAdditionalRate = table.Column<int>(type: "int", nullable: false),
                    PanelAdditionalRate = table.Column<int>(type: "int", nullable: false),
                    PerformAuxiliaryWorks = table.Column<bool>(type: "bit", nullable: false),
                    AuxiliaryWorkRate = table.Column<int>(type: "int", nullable: false),
                    OverheadRate = table.Column<int>(type: "int", nullable: false),
                    CostRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaterialMaster_1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaterialMaster_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaterialMaster_2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category1 = table.Column<int>(type: "int", nullable: true),
                    Category2 = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaterialMaster_2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaterialMaster_3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category1 = table.Column<int>(type: "int", nullable: true),
                    Category2 = table.Column<int>(type: "int", nullable: true),
                    Category3 = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaterialMaster_3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaterialMaster_4",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category1 = table.Column<int>(type: "int", nullable: true),
                    Category2 = table.Column<int>(type: "int", nullable: true),
                    Category3 = table.Column<int>(type: "int", nullable: true),
                    Category4 = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaterialMaster_4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    SiteMiscell = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseingDat = table.Column<int>(type: "int", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category1 = table.Column<int>(type: "int", nullable: true),
                    Category2 = table.Column<int>(type: "int", nullable: true),
                    Category3 = table.Column<int>(type: "int", nullable: true),
                    CategoryNam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalCos = table.Column<double>(type: "float", nullable: true),
                    InternalCos = table.Column<double>(type: "float", nullable: true),
                    BuildingCos = table.Column<double>(type: "float", nullable: true),
                    Accumulated = table.Column<double>(type: "float", nullable: true),
                    CompositeCo = table.Column<double>(type: "float", nullable: true),
                    ElectricalI = table.Column<double>(type: "float", nullable: true),
                    SupplyRate = table.Column<double>(type: "float", nullable: true),
                    AccessoryRa = table.Column<double>(type: "float", nullable: true),
                    Miscellaneo = table.Column<double>(type: "float", nullable: true),
                    LaborCostA = table.Column<int>(type: "int", nullable: true),
                    StepRateA = table.Column<double>(type: "float", nullable: true),
                    RemovalRate = table.Column<double>(type: "float", nullable: true),
                    LaborCostB = table.Column<int>(type: "int", nullable: true),
                    StepRateB = table.Column<double>(type: "float", nullable: true),
                    RemovalRate1 = table.Column<double>(type: "float", nullable: true),
                    Other = table.Column<double>(type: "float", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialCat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialCat1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ABC_Materia = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeilingOpen = table.Column<double>(type: "float", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrefectureMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefectureMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationCalc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TubeNetRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TubeReplenishmentRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CableNetRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CableReplenishmentRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborRateA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaborRateB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SiteMiscellRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiscellRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Minority = table.Column<bool>(type: "bit", nullable: false),
                    LaborBasisRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ABMethod = table.Column<bool>(type: "bit", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationCalc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationMain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Export = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Import = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Square = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationMain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Category1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Divide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemovalRate = table.Column<double>(type: "float", nullable: true),
                    Calculate = table.Column<bool>(type: "bit", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborCostA = table.Column<double>(type: "float", nullable: true),
                    LaborCostB = table.Column<double>(type: "float", nullable: true),
                    SiteMiscell = table.Column<double>(type: "float", nullable: true),
                    OtherExpens = table.Column<double>(type: "float", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearMaster", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ABMaterialMaster");

            migrationBuilder.DropTable(
                name: "AddressMaster");

            migrationBuilder.DropTable(
                name: "CalculationData");

            migrationBuilder.DropTable(
                name: "CategoryMaterialMaster_1");

            migrationBuilder.DropTable(
                name: "CategoryMaterialMaster_2");

            migrationBuilder.DropTable(
                name: "CategoryMaterialMaster_3");

            migrationBuilder.DropTable(
                name: "CategoryMaterialMaster_4");

            migrationBuilder.DropTable(
                name: "ConstructionMaster");

            migrationBuilder.DropTable(
                name: "CustomerMaster");

            migrationBuilder.DropTable(
                name: "MaterialMaster");

            migrationBuilder.DropTable(
                name: "PrefectureMaster");

            migrationBuilder.DropTable(
                name: "QuotationCalc");

            migrationBuilder.DropTable(
                name: "QuotationMain");

            migrationBuilder.DropTable(
                name: "QuotationMaterial");

            migrationBuilder.DropTable(
                name: "QuotationType");

            migrationBuilder.DropTable(
                name: "RankMaster");

            migrationBuilder.DropTable(
                name: "UnitMaster");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "YearMaster");
        }
    }
}
