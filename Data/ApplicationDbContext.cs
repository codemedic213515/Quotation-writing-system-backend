using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Models;

namespace QuotationWritingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Add DbSet for each entity
        public DbSet<ABMaterialMaster> ABMaterialMaster { get; set; }
        public DbSet<AddressMaster> AddressMaster { get; set; }
        public DbSet<CategoryMaterialMaster1> CategoryMaterialMasters1 { get; set; }
        public DbSet<CategoryMaterialMaster2> CategoryMaterialMasters2 { get; set; }
        public DbSet<CategoryMaterialMaster3> CategoryMaterialMasters3 { get; set; }
        public DbSet<CategoryMaterialMaster4> CategoryMaterialMasters4 { get; set; }
        public DbSet<ConstructionMaster> ConstructionMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<MaterialMaster> MaterialMasters { get; set; }
        public DbSet<PrefectureMaster> PrefectureMasters { get; set; }
        public DbSet<QuotationMain> QuotationMains { get; set; }
        public DbSet<QuotationMaterial> QuotationMaterials { get; set; }
        public DbSet<QuotationType> QuotationTypes { get; set; }
        public DbSet<RankMaster> RankMasters { get; set; }
        public DbSet<UnitMaster> UnitMasters { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<YearMaster> YearMasters { get; set; }
        public DbSet<CalculationData> CalculationDatas {get; set;}
public DbSet<QuotationCalc> QuotationCalcs { get; set; }
        // Constructor that accepts DbContextOptions and passes it to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        // Optionally, override OnConfiguring if needed (not necessary if you use AddDbContext)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourConnectionStringHere"); 
            }
        }

        // Customize model creation if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<ABMaterialMaster>().ToTable("ABMaterialMaster");
            modelBuilder.Entity<AddressMaster>().ToTable("AddressMaster");
            modelBuilder.Entity<CategoryMaterialMaster1>().ToTable("CategoryMaterialMaster_1");
            modelBuilder.Entity<CategoryMaterialMaster2>().ToTable("CategoryMaterialMaster_2");
            modelBuilder.Entity<CategoryMaterialMaster3>().ToTable("CategoryMaterialMaster_3");
            modelBuilder.Entity<CategoryMaterialMaster4>().ToTable("CategoryMaterialMaster_4");
            modelBuilder.Entity<ConstructionMaster>().ToTable("ConstructionMaster");
            modelBuilder.Entity<CustomerMaster>().ToTable("CustomerMaster");
            modelBuilder.Entity<MaterialMaster>().ToTable("MaterialMaster");
            modelBuilder.Entity<PrefectureMaster>().ToTable("PrefectureMaster");
            modelBuilder.Entity<QuotationMain>().ToTable("QuotationMain");
            modelBuilder.Entity<QuotationMaterial>().ToTable("QuotationMaterial");
            modelBuilder.Entity<QuotationType>().ToTable("QuotationType");
            modelBuilder.Entity<RankMaster>().ToTable("RankMaster");
            modelBuilder.Entity<UnitMaster>().ToTable("UnitMaster");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<YearMaster>().ToTable("YearMaster");
            modelBuilder.Entity<CalculationData>().ToTable("CalculationData");
            modelBuilder.Entity<QuotationCalc>().ToTable("QuotationCalc");
        
        }
    }
}
