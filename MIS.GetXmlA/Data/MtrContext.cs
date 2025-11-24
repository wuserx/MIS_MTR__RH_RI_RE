
using MIS.Models.I;
using MIS_MTR_2GISOMS.GetXmlA.Models.A;

public class MtrContext: DbContext
{
    
    public MtrContext()
    {
    }

    public MtrContext(DbContextOptions<MtrContext> options) : base(options)
    {
        this.Database.SetCommandTimeout(TimeSpan.FromHours(1).Seconds);
    }

    //A
    public DbSet<AMB_USL> AMB_USL { get; set; }
    public DbSet<AMB_USL_PROFIL> AMB_USL_PROFIL { get; set; }

    //B
    public DbSet<BDiag> BDiags { get; set; }
    public DbSet<BProt> BProts { get; set; }

    //C
    public DbSet<COns> COnss { get; set; }

    //D
    public DbSet<Directive> Directives { get; set; }
    public DbSet<DS2N> DS2Ns { get; set; }

    //E    
    public DbSet<Expertise_mtr> Expertise { get; set; } 

    //G
    // Виртуальный DbSet для результата TVF
    public DbSet<GetENPv1Result> GetENPv1Results { get; set; }

    //I
    public DbSet<INJ> INJs { get; set; }

    public DbSet<DS3N> DS3Ns { get; set; }

    //K
    public DbSet<KsgKpg_mtr> KsgKpgs { get; set; }

    //L
    public DbSet<LekPrSl> LekPrSls { get; set; }
    public DbSet<LekDose> LekDoses { get; set; }
    public DbSet<LEkPr> LEkPrs { get; set; }

    //M
    public DbSet<MedDev> MedDevs { get; set; }
    public DbSet<MrUslN> MrUslNs { get; set; }

    //N
    public DbSet<NApr> NAprs { get; set; }
    public DbSet<Naz> Nazs { get; set; }

    //O
    public DbSet<OnkSl_mtr> OnkSls { get; set; }//[Table("OnkSl", Schema = "Reestr")]
    public DbSet<OnkUsl> OnkUsls { get; set; }
    public DbSet<OplOtk> OplOtks { get; set; }

    //R
    public DbSet<RepeatedMEK> RepeatedMEKs { get; set; }

    //S
    //public DbSet<Sank> Sanks { get; set; }
    public DbSet<SankZ_mtr> SankZs { get; set; }
    public DbSet<Schet_mtr> Schets { get; set; }
    public DbSet<Sluch_mtr> Sluchs { get; set; }//[Table("Sluch", Schema = "Reestr")]
    public DbSet<SluchSum> SluchSums { get; set; }
    public DbSet<SchetDirective> SchetDirectives { get; set; }
    public DbSet<SchetZakSluch> SchetZakSluchs { get; set; }

    //T
    public DbSet<TopicExpertise> TopicExpertises { get; set; }

    //U
    public DbSet<Usl_mtr> Usls { get; set; }

    //Z
    public DbSet<ZakSluch_mtr> ZakSluchs { get; set; }//[Table("ZakSluch", Schema = "Reestr")]

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);       

        // Указываем, что эта сущность не мапится на таблицу
        modelBuilder.Entity<GetENPv1Result>().HasNoKey();
        modelBuilder.Entity<GetENPv1Result>().ToTable((string?)null); // Не сопоставлять с таблицей

        modelBuilder.Entity<KsgKpg_mtr>().HasKey(x => x.Id);
        modelBuilder.Entity<KsgKpg_mtr>().ToTable("KsgKpg", schema: "Reestr");

        modelBuilder.Entity<OnkSl_mtr>().ToTable("OnkSl", schema: "Reestr");

        modelBuilder.Entity<Schet_mtr>().ToTable("Schet", schema: "Reestr");

        modelBuilder.Entity<ZakSluch_mtr>().ToTable("ZakSluch", schema: "Reestr");

        modelBuilder.Entity<Sluch_mtr>().ToTable("Sluch", schema: "Reestr");

        modelBuilder.Entity<Usl_mtr>().ToTable("Usl", schema: "Reestr"); //[Table("Usl", Schema = "Reestr")]

        modelBuilder.Entity<SankZ_mtr>().ToTable("SankZ", schema: "Reestr");

        modelBuilder.Entity<Expertise_mtr>().ToTable("Expertise", schema: "Reestr"); //[Table("Expertise", Schema = "Reestr")]

        //modelBuilder.Entity<Sluch>()
        //        .Property(s => s._CodeMes1).HasColumnName("CODE_MES1s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._Ds2).HasColumnName("DS2s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._Ds3).HasColumnName("DS3s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._MekErrors).HasColumnName("MekErrors");

        //modelBuilder.Entity<Sluch_mtr>()
        //.HasOne(x => x.KsgKpg)
        //.WithOne(x => x.Sluch)
        //.HasForeignKey<KsgKpg_mtr>(p => p.Id)
        //.OnDelete(DeleteBehavior.Cascade)
        //;

        //modelBuilder.Entity<Sluch_mtr>()
        //.HasOne(x => x.OnkSl)
        //.WithOne(x => x.Sluch)
        //.HasForeignKey<OnkSl_mtr>(p => p.Id)
        //.OnDelete(DeleteBehavior.Cascade)
        //;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(RepositorySettings.GetConnectionString("DefaultMtrConnection"), o => o.UseCompatibilityLevel(120));
    }
}
