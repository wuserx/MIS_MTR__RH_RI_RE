public class Percents
{
    public Percents()
    {
        Kol1 = 0;
        Kol2 = 0;
        Kol3 = 0;
        Kol4 = 0;
        ProcMee1 = 0;
        ProcMee2 = 0;
        ProcMee3 = 0;
        ProcMee4 = 0;
        ProcEkmp1 = 0;
        ProcEkmp2 = 0;
        ProcEkmp3 = 0;
        ProcEkmp4 = 0;
        PrecentMeePurpose1 = 0;
        PrecentMeePurpose2 = 0;
        PrecentMeePurpose3 = 0;
        PrecentMeePurpose4 = 0;
        PrecentMeePlanRandom1 = 0;
        PrecentMeePlanRandom2 = 0;
        PrecentMeePlanRandom3 = 0;
        PrecentMeePlanRandom4 = 0;
        PrecentMeeThematicRandom1 = 0;
        PrecentMeeThematicRandom2 = 0;
        PrecentMeeThematicRandom3 = 0;
        PrecentMeeThematicRandom4 = 0;
        PrecentEkmpPurpose1 = 0;
        PrecentEkmpPurpose2 = 0;
        PrecentEkmpPurpose3 = 0;
        PrecentEkmpPurpose4 = 0;
        PrecentEkmpPlanRandom1 = 0;
        PrecentEkmpPlanRandom2 = 0;
        PrecentEkmpPlanRandom3 = 0;
        PrecentEkmpPlanRandom4 = 0;
        PrecentEkmpThematicRandom1 = 0;
        PrecentEkmpThematicRandom2 = 0;
        PrecentEkmpThematicRandom3 = 0;
        PrecentEkmpThematicRandom4 = 0;
    }
    public int Id { get; set; }
    public int Kol1 { get; set; } // Количество случаев
    public int Kol2 { get; set; }
    public int Kol3 { get; set; }
    public int Kol4 { get; set; }



    public decimal ProcMee1 { get; set; }
    public decimal ProcMee2 { get; set; }
    public decimal ProcMee3 { get; set; }
    public decimal ProcMee4 { get; set; }

    public decimal ProcEkmp1 { get; set; }
    public decimal ProcEkmp2 { get; set; }
    public decimal ProcEkmp3 { get; set; }
    public decimal ProcEkmp4 { get; set; }

    // ---- МЭЭ
    public decimal PrecentMeePurpose1 { get; set; } //Проценты целевой проверки МЭЭ (код 110)
    public decimal PrecentMeePurpose2 { get; set; }
    public decimal PrecentMeePurpose3 { get; set; }
    public decimal PrecentMeePurpose4 { get; set; }



    public decimal PrecentMeePlanRandom1 { get; set; } //Проценты плановой проверки МЭЭ (случайная выборка) (код 121)
    public decimal PrecentMeePlanRandom2 { get; set; }
    public decimal PrecentMeePlanRandom3 { get; set; }
    public decimal PrecentMeePlanRandom4 { get; set; }


    public decimal PrecentMeeThematicRandom1 { get; set; } //Проценты плановой проверки МЭЭ (тематическая) (код 122)
    public decimal PrecentMeeThematicRandom2 { get; set; }
    public decimal PrecentMeeThematicRandom3 { get; set; }
    public decimal PrecentMeeThematicRandom4 { get; set; }
        

    // ------------ ЭКМП
    public decimal PrecentEkmpPurpose1 { get; set; } //Проценты целевой проверки ЭКМП (код 210)
    public decimal PrecentEkmpPurpose2 { get; set; }
    public decimal PrecentEkmpPurpose3 { get; set; }
    public decimal PrecentEkmpPurpose4 { get; set; }

    public decimal PrecentEkmpPlanRandom1 { get; set; } //Проценты плановой проверки ЭКМП (случайная выборка) (код 221)
    public decimal PrecentEkmpPlanRandom2 { get; set; }
    public decimal PrecentEkmpPlanRandom3 { get; set; }
    public decimal PrecentEkmpPlanRandom4 { get; set; }


    public decimal PrecentEkmpThematicRandom1 { get; set; } //Проценты плановой проверки ЭКМП (тематическая) (код 222)
    public decimal PrecentEkmpThematicRandom2 { get; set; }
    public decimal PrecentEkmpThematicRandom3 { get; set; }
    public decimal PrecentEkmpThematicRandom4 { get; set; }

}
