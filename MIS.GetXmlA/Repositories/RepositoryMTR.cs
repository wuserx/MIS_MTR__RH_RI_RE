using Microsoft.IdentityModel.Tokens;
using MIS.Models.I;

public class RepositoryMTR
{
    protected readonly MtrContext Context;

    public RepositoryMTR(MtrContext context)
    {
        Context = context;
    }

    public IQueryable<GetENPv1Result> GetENPv1(
    string fam,
    string im,
    string ot,
    string dr,
    string date1,
    string date2)
    {
        var sql = $@"SELECT * FROM dbo.GetENPv1({fam}, {im}, {ot}, {dr}, {date1}, {date2})";

        return Context.GetENPv1Results.FromSqlRaw(
            "SELECT * FROM dbo.GetENPv1({0}, {1}, {2}, {3}, {4}, {5})",
            fam, im, ot, dr, date1, date2);
    }

    public async Task<IEnumerable<RepeatedMEK>> GetRepeatedMEKByZakSluchsAsync(int zakSluchId)
    {
        return await Context.RepeatedMEKs
            .Where(s => s.ZakSluchId == zakSluchId)
            .ToListAsync();
    }

    public string GetSchetCODE(Schet_mtr schet)
    {
        return string.Concat(schet.Id, "-", schet.CODE, "-", schet.YEAR, schet.MONTH);
    }

    public string GetZapN_ZAP(ZakSluch_mtr zakSluch)
    {
        return string.Concat(zakSluch.Id, "-", zakSluch.N_ZAP);
    }

    public string GetSluchSL_ID(Sluch_mtr _sluch)
    {
        return _sluch.SL_ID.Length < 20 ? string.Concat(_sluch.Id, "-", _sluch.SL_ID) : _sluch.SL_ID;
    }

    public async Task<IEnumerable<Expertise_mtr>> GetAllExpertiseSMOByZakIdAsync(IEnumerable<Sluch_mtr> sluchs)
    {
        List<int> sluchs_id = sluchs.Select(s => s.Id).ToList();

        return await Context.Expertise
            .Where(d => sluchs_id.Contains(d.Id))
            .Where(d => d.SumExpertise + d.SumExpertisePenalty > 0)
        .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch_mtr>> GetAllExpertiseSMOByZakSluchIdAsync(IEnumerable<ZakSluch_mtr> ZakSluchs)
    {
        var ZakSluchId_MTR_List = ZakSluchs.Select(a => a.Id).Distinct().ToList();       

        var ExpertiseSMO_ZakSluchId_MTR_List = await Context.Expertise
            .Where(d => ZakSluchId_MTR_List.Contains((int)d.ZakSluchId))
            .Select(s => s.ZakSluchId)
            .Distinct()
            .ToListAsync();

        return await Context.ZakSluchs
            .Where(s => ExpertiseSMO_ZakSluchId_MTR_List.Contains(s.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch_mtr>> GetAllRepeatedMEKByZakSluchIdAsync(IEnumerable<ZakSluch_mtr> ZakSluchs)
    {
        var ZakSluchId_MTR_List = ZakSluchs.Select(a => a.Id).Distinct().ToList();

        var RepeatedMEK_ZakSluchId_MTR_List = await Context.RepeatedMEKs
            .Where(d => ZakSluchId_MTR_List.Contains((int)d.ZakSluchId))
            .Select(s => s.ZakSluchId)
            .Distinct()
            .ToListAsync();

        return await Context.ZakSluchs
            .Where(s => RepeatedMEK_ZakSluchId_MTR_List.Contains(s.Id))
            .ToListAsync();
    }


    public async Task<IEnumerable<ZakSluch_mtr>> GetAllSankZByZakSluchsAsync(IEnumerable<ZakSluch_mtr> ZakSluchs)
    {
        var oplatas = new List<int> { 2, 3 };
        var ZakSluchId_List = ZakSluchs.Select(p => p.Id).Distinct().ToList();

        var SankZ_ZakSluchId_List = await Context.SankZs
            .Where(s => ZakSluchId_List.Contains((int)s.ZakId))
            .Select(d => d.ZakId)
            .Distinct()
            .ToListAsync();

        return await Context.ZakSluchs
            .Where(s => SankZ_ZakSluchId_List.Contains(s.Id))
            .ToListAsync();
    }

    public SchetZakSluch? GetSchetZakSluchByFileNameXml(string? FileNameXml)
    {
        return Context.SchetZakSluchs
            .Where(z => z.FILENAME == FileNameXml)
            .FirstOrDefault();
    }

    public async Task<IEnumerable<OplOtk>> GetAllOplOtkByZakIdAsync(int? sankS_OSN)
    {
        return await Context.OplOtks.Where(s => s.Kod == sankS_OSN).ToListAsync();
    }

    public async Task<List<GroupFilenameZakSluch>> GetGroupFilenameZakSluchBySchetId(int SchetId)
    {
        return await Context.ZakSluchs            
            .Where(s => s.SchetId == SchetId)
            .Where(f => f.FILENAME != null)
            .GroupBy(g => new { g.FILENAME, g.SchetId })
            .Select(p => new GroupFilenameZakSluch()
                { 
                    FILENAME = p.Key.FILENAME,
                    SchetId = p.Key.SchetId
                })
            .ToListAsync();
    }

    public async Task<List<Pers>> GetGroupPersZakSluchBySchetId(int SchetId, string FileNameXml)
    {
        return await Context.ZakSluchs
            .Where(z => z.FILENAME == FileNameXml)
            .Where(s => s.SchetId == SchetId)
            .Where(f => f.FILENAME != null)
            .GroupBy(g => new {
                g.ID_PAC,
                g.FAM,
                g.IM,
                g.OT,
                g.W,
                g.DR,
                g.DOSTs,
                g.TEL,
                g.FAM_P,
                g.IM_P,
                g.OT_P,
                g.W_P,
                g.DR_P,
                g.DOST_Ps,
                g.MR,
                g.DOCTYPE,
                g.DOCSER,
                g.DOCNUM,
                g.DOCDATE,
                g.DOCORG,
                g.SNILS,
                g.OKATOG,
                g.OKATOP
            })
            .Select(p => new Pers()
            {
                ID_PAC = p.Key.ID_PAC,
                FAM = p.Key.FAM,
                IM = p.Key.IM,
                OT = p.Key.OT,
                W = p.Key.W,
                DR = p.Key.DR,
                DOSTs = p.Key.DOSTs,
                TEL = p.Key.TEL,
                FAM_P = p.Key.FAM_P,
                IM_P = p.Key.IM_P,
                OT_P = p.Key.OT_P,
                W_P = p.Key.W_P,
                DR_P = p.Key.DR_P,
                DOST_Ps = p.Key.DOST_Ps,
                MR = p.Key.MR,
                DOCTYPE = p.Key.DOCTYPE,
                DOCSER = p.Key.DOCSER,
                DOCNUM = p.Key.DOCNUM,
                DOCDATE = p.Key.DOCDATE,
                DOCORG = p.Key.DOCORG,
                SNILS = p.Key.SNILS,
                OKATOG = p.Key.OKATOG,
                OKATOP = p.Key.OKATOP
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<Schet_mtr>> GetSchets(int year, int month)
    {
        var Schets_mtr = Context.Schets
                            .Where(s => s.YEAR == year)
                            .Where(z => z.MONTH == month)
                            .Where(s => s.RegionId == 7)
                            .Where(s => s.CODE_MO != null)
                            .Where(s => s.CODE_MO != "999999");

        if (Init.SCHET_NSCHET_MTR?.Count() > 0)
            return await Schets_mtr.Where(s => Init.SCHET_NSCHET_MTR.Contains(s.NSCHET)).ToListAsync();

        if (Init.SCHET_ID_MTR?.Count() > 0)
            return await Schets_mtr.Where(s => Init.SCHET_ID_MTR.Contains(s.Id)).ToListAsync();

        return await Schets_mtr.ToListAsync();
    }

    public async Task<IEnumerable<Sluch_mtr>> GetAllSluchByZakSluchIdAsync(int ZakSluchId)
    {
        return await Context.Sluchs.Where(s => s.ZakSluchId == ZakSluchId).ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch_mtr>> GetAllZakSluchByParamsAsync(int SchetId, string FileNameXml)
    {
        return await Context.ZakSluchs
            .Where(z => z.FILENAME == FileNameXml)
            .Where(z => z.SchetId == SchetId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch_mtr>> GetAllZakSluchBySchetIdAsync(int SchetId)
    {
        return await Context.ZakSluchs
            .Where(z => z.SchetId == SchetId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SankZ_mtr>> GetAllSankZByZakIdAsync(int ZakSluchId)
    {
        return await Context.SankZs.Where(s => s.ZakId == ZakSluchId).ToListAsync();
    }

    public async Task<IEnumerable<KsgKpg_mtr>> GetAllKsgKpgBySluchIdAsync(int SluchId)
    {
        return await Context.KsgKpgs.Where(s => s.Id == SluchId).ToListAsync();
    }
    
    public async Task<IEnumerable<LekPrSl>> GetAllLekPrSlBySluchIdAsync(int SluchId)
    {
        return await Context.LekPrSls.Where(s => s.Id == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<LekDose>> GetAllLekDoseByLekPrSlIdAsync(int LekPrSlId)
    {
        return await Context.LekDoses.Where(s => s.Id == LekPrSlId).ToListAsync();
    }

    public async Task<IEnumerable<MedDev>> GetAllMedDevByUslIdAsync(int UslId)
    {
        return await Context.MedDevs.Where(s => s.UslId == UslId).ToListAsync();
    }

    public async Task<IEnumerable<MrUslN>> GetAllMrUslNByUslIdAsync(int UslId)
    {
        return await Context.MrUslNs.Where(s => s.UslId == UslId).ToListAsync();
    }

    public async Task<IEnumerable<Usl_mtr>> GetAllUslBySluchIdAsync(int SluchId)
    {
        return await Context.Usls.Where(s => s.SluchId == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<NApr>> GetAllNAprBySluchIdAsync(int SluchId)
    {
        return await Context.NAprs.Where(s => s.SluchId == SluchId).ToListAsync();
    }
    public async Task<IEnumerable<COns>> GetAllCOnsBySluchIdAsync(int SluchId)
    {
        return await Context.COnss.Where(s => s.SluchId == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<DS2N>> GetAllDS2NBySluchIdAsync(int SluchId)
    {
        return await Context.DS2Ns.Where(s => s.SluchId == SluchId).ToListAsync();
    }
    public async Task<IEnumerable<DS3N>> GetAllDS3NBySluchIdAsync(int SluchId)
    {
        return await Context.DS3Ns.Where(s => s.SluchId == SluchId).ToListAsync();
    }
    public async Task<IEnumerable<Naz>> GetAllNazBySluchIdAsync(int SluchId)
    {
        return await Context.Nazs.Where(s => s.SluchId == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<OnkSl_mtr>> GetAllOnkSlBySluchIdAsync(int SluchId)
    {
        return await Context.OnkSls.Where(s => s.Id == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<BDiag>> GetAllBDiagByOnkSlIdAsync(int OnkSlId)
    {
        return await Context.BDiags.Where(s => s.OnkSlId == OnkSlId).ToListAsync();
    }

    public async Task<IEnumerable<BProt>> GetAllBProtByOnkSlIdAsync(int OnkSlId)
    {
        return await Context.BProts.Where(s => s.OnkSlId == OnkSlId).ToListAsync();
    }

    public async Task<IEnumerable<LEkPr>> GetAllLEkPrByOnkUslIdAsync(int OnkUslId)
    {
        return await Context.LEkPrs.Where(s => s.OnkUslId == OnkUslId).ToListAsync();
    }

    public async Task<IEnumerable<INJ>> GetAllINJByLekPrIdAsync(int lekprId)
    {
        return await Context.INJs.Where(s => s.LEkPrId == lekprId).ToListAsync();
    }

    public async Task<IEnumerable<OnkUsl>> GetAllOnkUslByOnkSlIdAsync(int OnkSlId)
    {
        return await Context.OnkUsls.Where(s => s.OnkSlId == OnkSlId).ToListAsync();
    }

    public string GetZakSluchIDCASE(ZakSluch_mtr zakSluch)
    {
        return string.Concat(zakSluch.Id, "-", zakSluch.IDCASEL != null ? zakSluch.IDCASEL : zakSluch.IDCASE);
    }

    public string GetUslIDSERV(Usl usl)
    {
        return usl.IDSERV.Length < 20 ? string.Concat(usl.Id, "-", usl.IDSERV) : usl.IDSERV;
    }

    public async Task<string> GetAMB_USL_PROFIL(ZakSluch_mtr zakSluch, Sluch_mtr sluch)
    {
        string result = "";

        if (zakSluch.USL_OK == 3)
        {
            if (sluch.P_CEL == "1.3")
            {
                result = Context.AMB_USL_PROFIL
                     .Where(s => s.PROFIL == sluch.PROFIL)
                     .FirstOrDefault()?.CODE_USL_DN;
            }
            else
            {
                result = Context.AMB_USL_PROFIL
                     .Where(s => s.PROFIL == sluch.PROFIL)
                     .FirstOrDefault()?.CODE_USL;
            }

            if (result.IsNullOrEmpty())
            {
                if (sluch.DET == 1)
                    result = "B01.031.001";
                else
                    result = "B01.047.001";
            }

        }

        if (zakSluch.USL_OK == 4)
        {
            result = "B01.044.001";
        }

        return result;
    }
}