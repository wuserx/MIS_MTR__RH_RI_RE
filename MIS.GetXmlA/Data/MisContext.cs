using MIS.Models.I;
using MIS_MTR_2GISOMS.GetXmlA.Models.A;
using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;

public class MisContext : DbContext
{
    public MisContext()
    { 
    }

    public MisContext(DbContextOptions<MisContext> options) : base(options)
    {       
        this.Database.SetCommandTimeout(TimeSpan.FromHours(2).Seconds);
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
    public DbSet<DK_Storn_VMP> DK_Storn_VMPs { get; set; }
    

    //E    
    public DbSet<ExpertiseSMO> ExpertiseSMOs { get; set; }
    public DbSet<TENP> EnpByEnpAndZSluchDate2 { get; set; }
    public DbSet<TENP> GetEnpByPidAndZSluchDate2 { get; set; }
    

    //G
    // Виртуальный DbSet для результата TVF
    public DbSet<GetENPv1Result> GetENPv1Results { get; set; }

    public DbSet<DS3N> DS3Ns { get; set; }

    //I
    public DbSet<INJ> INJs { get; set; }

    [NotMapped]
    public DbSet<IDENT> iDENTs { get; set; }

    //K
    public DbSet<KsgKpg> KsgKpgs { get; set; }//[Table("KsgKpg", Schema = "Reestr")]

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
    public DbSet<OnkSl> OnkSls { get; set; }//[Table("OnkSl", Schema = "Reestr")]
    public DbSet<OnkUsl> OnkUsls { get; set; }
    public DbSet<OplOtk> OplOtks { get; set; }

    //R
    public DbSet<RepeatedMEK> RepeatedMEKs { get; set; }

    //S   
    public DbSet<SankZ> SankZs { get; set; }//[Table("SankZ", Schema = "Reestr")]
    public DbSet<Schet> Schets { get; set; }
    public DbSet<Sluch> Sluchs { get; set; }//[Table("Sluch", Schema = "Reestr")]
    public DbSet<SluchSum> SluchSums { get; set; }
    public DbSet<SchetDirective> SchetDirectives { get; set; }
    public DbSet<SchetZakSluch> SchetZakSluchs { get; set; }

    //T
    public DbSet<TopicExpertise> TopicExpertises { get; set; }

    //U
    public DbSet<Usl> Usls { get; set; }

    //Z
    public DbSet<ZakSluch> ZakSluchs { get; set; }//[Table("ZakSluch", Schema = "Reestr")]

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TENP>()
        .HasNoKey()
        .ToFunction("GetEnpByEnpAndZSluchDate2");

        modelBuilder.Entity<TENP>()
        .HasNoKey()
        .ToFunction("GetEnpByPidAndZSluchDate2");

        // Указываем, что эта сущность не мапится на таблицу
        modelBuilder.Entity<GetENPv1Result>().HasNoKey();
        modelBuilder.Entity<GetENPv1Result>().ToTable((string?)null); // Не сопоставлять с таблицей

        modelBuilder.Entity<KsgKpg>().HasKey(x => x.Id);

        modelBuilder.Entity<KsgKpg>().ToTable("KsgKpg", schema: "Reestr");

        modelBuilder.Entity<OnkSl>().ToTable("OnkSl", schema: "Reestr");

        modelBuilder.Entity<Schet>().ToTable("Schet", schema: "Reestr");

        modelBuilder.Entity<ZakSluch>().ToTable("ZakSluch", schema: "Reestr");

        modelBuilder.Entity<Sluch>().ToTable("Sluch", schema: "Reestr");

        modelBuilder.Entity<Usl>().ToTable("Usl", schema: "Reestr"); //[Table("Usl", Schema = "Reestr")]

        modelBuilder.Entity<SankZ>().ToTable("SankZ", schema: "Reestr");

        modelBuilder.Entity<Expertise>().ToTable("Expertise", schema: "Reestr"); //[Table("Expertise", Schema = "Reestr")]

        //modelBuilder.Entity<Sluch>()
        //        .Property(s => s._CodeMes1).HasColumnName("CODE_MES1s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._Ds2).HasColumnName("DS2s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._Ds3).HasColumnName("DS3s");

        //modelBuilder.Entity<Sluch>()
        //    .Property(s => s._MekErrors).HasColumnName("MekErrors");

        //modelBuilder.Entity<Sluch>()
        //            .HasOne(x => x.KsgKpg)
        //            .WithOne(x => x.Sluch)
        //            .HasForeignKey<KsgKpg>(p => p.Id)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            ;

        //modelBuilder.Entity<Sluch>()
        //            .HasOne(x => x.OnkSls)
        //            .WithOne(x => x.Sluch)
        //            .HasForeignKey<OnkSl>(p => p.Id)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            ;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string constr = RepositorySettings.GetConnectionString("DefaultMisConnection").ToString();
      
        optionsBuilder.UseSqlServer(constr, o => o.UseCompatibilityLevel(120));
    }
}