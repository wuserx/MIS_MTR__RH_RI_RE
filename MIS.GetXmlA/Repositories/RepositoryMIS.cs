using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MIS.Models.I;
using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;

public class RepositoryMIS
{
    protected readonly MisContext Context;

    public RepositoryMIS()
    {
    }

    public RepositoryMIS(MisContext context)
    {
        Context = context;

        Context.Database.SetCommandTimeout(120);
    }

    public async Task<TENP> GetEnpByPidAndZSluchDate2(int? pid, DateTime? searchDate)
    {
        var result = await Context.Set<TENP>()
            .FromSqlRaw("SELECT * FROM dbo.GetEnpByPidAndZSluchDate2({0}, {1})", pid, searchDate)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<TENP> GetEnpByEnpAndZSluchDate2(string enp, DateTime searchDate)
    {
        var result = await Context.Set<TENP>()
            .FromSqlRaw("SELECT * FROM dbo.GetEnpByEnpAndZSluchDate2({0}, {1})", enp, searchDate)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public IQueryable<GetENPv1Result> GetENPv1(
    string fam,
    string im,
    string ot,
    string dr,
    string date1,
    string date2)
    {
        //var sql = $@"SELECT * FROM dbo.GetENPv1({fam}, {im}, {ot}, {dr}, {date1}, {date2})";

        var resut = Context.GetENPv1Results.FromSqlRaw(
            "SELECT * FROM dbo.GetENPv1({0}, {1}, {2}, {3}, {4}, {5})",
            fam, im, ot, dr, date1, date2); 

        return resut;
    }


    public async Task<IEnumerable<DK_Storn_VMP>> GetAllDK_Storn_VMPs()
    {
        var x = await Context.DK_Storn_VMPs.ToListAsync();

        return x;
    }

    public async Task<IEnumerable<ZakSluch>> GetZSluchWithOutZsluchX()
    {
        return await Context.ZakSluchs
            .Where(a => !Context.RepeatedMEKs.Any(b => b.ZakSluchId == a.Id))
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

    //группируем номера счетов
    public async Task<IEnumerable<string?>> GetSchetsGroupNSCHET(int year, int month)
    {
        List<string?> result = [];

        var Schets = Context.Schets
                        .Where(s => s.YEAR_REPORT == year)
                        .Where(z => z.MONTH_REPORT == month)
                        .Where(s => s.CODE_MO != null)
                        ;

        if (Init.SCHET_NSCHET_MIS?.Count > 0)
            return await Schets.Where(s => Init.SCHET_NSCHET_MIS.Contains(s.NSCHET)).Select(s => s.NSCHET).Distinct().ToListAsync();
        
        if (Init.SCHET_ID_MIS?.Count > 0)
            return await Schets.Where(s => Init.SCHET_ID_MIS.Contains(s.Id)).Select(s => s.NSCHET).Distinct().ToListAsync();
        
        return await Schets.Select(s => s.NSCHET).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<Schet>> GetSchets(int year, int month)
    {
        //var NSCHET_list = GetSchetsGroupNSCHET(year, month).Result;

        var Schets_mis = Context.Schets
                        .Where(s => s.YEAR_REPORT == year)
                        .Where(z => z.MONTH_REPORT == month)
                        .Where(s => s.CODE_MO != null)
                        //.Where(z => NSCHET_list.ToList().Contains(z.NSCHET ?? ""))
                        ;

        if (Init.SCHET_NSCHET_MIS?.Count() > 0)
            return await Schets_mis.Where(s => Init.SCHET_NSCHET_MIS.Contains(s.NSCHET)).ToListAsync();

        if (Init.SCHET_ID_MIS?.Count() > 0)
            return await Schets_mis.Where(s => Init.SCHET_ID_MIS.Contains(s.Id)).ToListAsync();

        return await Schets_mis.ToListAsync();
    }

    public string GetZakSluchIDCASE(ZakSluch zakSluch)
    {
        return string.Concat(zakSluch.Id, "-", zakSluch.IDCASEL != null ? zakSluch.IDCASEL : zakSluch.IDCASE);
    }

    public string GetUslIDSERV(Usl usl)
    {
        return usl.IDSERV.Length < 20 ? string.Concat(usl.Id, "-", usl.IDSERV) : usl.IDSERV;
    }

    public string GetSluchSL_ID(Sluch _sluch)
    {
        return _sluch.SL_ID.Length < 20 ? string.Concat(_sluch.Id, "-", _sluch.SL_ID) : _sluch.SL_ID;
    }

    public string GetSchetCODE(Schet schet)
    {
        return string.Concat(schet.Id, "-", schet.CODE, "-", schet.YEAR_REPORT, schet.MONTH);
    }

    public string GetZapN_ZAP(ZakSluch zakSluch)
    {
        return string.Concat(zakSluch.Id, "-", zakSluch.N_ZAP);
    }

    public async Task<string> GetAMB_USL_PROFIL(ZakSluch zakSluch, Sluch sluch)
    {
        string result = "";

        if (zakSluch.USL_OK == 3)
        {
            if(sluch.P_CEL == "1.3")
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

            if(result.IsNullOrEmpty())
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

    public async Task<IEnumerable<Sluch>> GetAllSluchByZakSluchIdAsync(int ZakSluchId)
    {
        return await Context.Sluchs
            .Where(s => s.ZakSluchId == ZakSluchId)            
            .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch>> GetAllZakSluchByParamsAsync(int SchetId, string FileNameXml)
    {
        return await Context.ZakSluchs
            .Where(z => z.FILENAME == FileNameXml)
            .Where(z => z.SchetId == SchetId)
            .Where(b => !Context.RepeatedMEKs.Any(c => c.ZakSluchId == b.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch>> GetAllZakSluchBySchetIdAsync(int SchetId)
    {
        /*
         * 31-10-2025 Шаовым принято решение выгружать все зслучаи для RH и RI
         * для этого закомментирована строчка с RepeatedMEK
         * основание https://t.me/c/1534691133/1888
         */

        return await Context.ZakSluchs
            .Where(z => z.SchetId == SchetId)
            //.Where(b => !Context.RepeatedMEKs.Any(c => c.ZakSluchId == b.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<SankZ>> GetAllSankZByZakIdAsync(int ZakSluchId)
    {
        return await Context.SankZs.Where(s => s.ZakSluchId == ZakSluchId).ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch>> GetAllSankZByZakSluchsAsync(IEnumerable<ZakSluch> zakSluchs)
    {
        var zakSluchIds = zakSluchs.Select(z => z.Id).Distinct().ToList();

        if (!zakSluchIds.Any())
            return Enumerable.Empty<ZakSluch>();

        var oplatasOk = new List<int?> { 2, 3 };

        var result = await Context.ZakSluchs
            .Where(z => zakSluchIds.Contains(z.Id) &&
                        Context.SankZs.Any(s => s.ZakSluchId == z.Id) &&
                        oplatasOk.Contains(z.OPLATA))
            .ToListAsync();

        return result;

        //var oplatas_ok = new List<int?> { 2, 3 };
        //var ZakSluchId_List = ZakSluchs.Select(a => a.Id).Distinct().ToList();        

        //var SankZ_ZakSluchId_List = await Context.SankZs
        //    .Where(s => ZakSluchId_List.Contains(s.ZakSluchId))
        //    .Select(a => a.ZakSluchId)
        //    .Distinct()
        //    .ToListAsync();

        //return await Context.ZakSluchs
        //                            .Where(z => SankZ_ZakSluchId_List.Contains(z.Id))
        //                            .Where(z => oplatas_ok.Contains(z.OPLATA))
        //                            .ToListAsync();
    }
    

    public async Task<IEnumerable<ExpertiseSMO>> GetAllExpertiseSMOByZakIdAsync(int zakSluchId)
    {
        return await Context.ExpertiseSMOs
        .AsNoTracking() // ⚡ Ускоряет, если не планируете обновлять 
        .Where(e => e.ZakSluchId == zakSluchId &&
                   e.SANK_MEE  + e.SANK_EKMP > 0)
        .ToListAsync();

        //return await Context.ExpertiseSMOs
        //    .Where(s => s.ZakSluchId == ZakSluchId)
        //    .Where(d => d.SANK_MEE + d.SANK_EKMP > 0)
        //    .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch>> GetAllExpertiseSMOByZakSluchsAsync(IEnumerable<ZakSluch> zakSluchs)
    {
        var zakSluchIds = zakSluchs.Select(z => z.Id).Distinct().ToList();

        if (!zakSluchIds.Any())
            return Enumerable.Empty<ZakSluch>();

        var idsWithExpertise = await Context.ExpertiseSMOs
            .Where(e => zakSluchIds.Contains(e.ZakSluchId!.Value))
            .Select(e => e.ZakSluchId.Value)
            .Distinct()
            .ToListAsync();

        var result = await Context.ZakSluchs
            .Where(z => idsWithExpertise.Contains(z.Id))
            .ToListAsync();

        return result;

        //var ZakSluchsIds = ZakSluchs.Select(a => a.Id).Distinct().ToList();

        //var ExpertiseSMO_IdList = await Context.ExpertiseSMOs
        //    .Where(s => ZakSluchsIds.Contains((int)s.ZakSluchId))
        //    .Select(a => a.ZakSluchId)
        //    .Distinct()
        //    .ToListAsync();

        //return await Context.ZakSluchs
        //       .Where(z => ExpertiseSMO_IdList.Contains(z.Id))
        //       .ToListAsync();
    }

    public async Task<IEnumerable<ZakSluch>> GetAllRepeatedMEKByZakSluchsAsync(IEnumerable<ZakSluch> zakSluchs)
    {
        var zakSluchIds = zakSluchs.Select(z => z.Id).Distinct().ToList();

        if (!zakSluchIds.Any())
            return Enumerable.Empty<ZakSluch>();

        var result = await Context.ZakSluchs
            .Where(z => zakSluchIds.Contains(z.Id) &&
                        Context.RepeatedMEKs.Any(r => r.ZakSluchId == z.Id))
            .ToListAsync();

        return result;

        //var ZakSluchsIds = ZakSluchs.Select(a => a.Id).Distinct().ToList();

        //var RepeatedMEK_IdList = await Context.RepeatedMEKs
        //    .Where(s => ZakSluchsIds.Contains((int)s.ZakSluchId))
        //    .Select(a => a.ZakSluchId)
        //    .Distinct()
        //    .ToListAsync();

        //return await Context.ZakSluchs
        //       .Where(z => RepeatedMEK_IdList.Contains(z.Id))
        //       .ToListAsync();
    }

    public async Task<IEnumerable<RepeatedMEK>> GetRepeatedMEKByZakSluchsAsync(int zakSluchId)
    {
        return await Context.RepeatedMEKs
            .AsNoTracking()
            .Where(s => s.ZakSluchId == zakSluchId)            
            .ToListAsync();
    }

    public async Task<IEnumerable<KsgKpg>> GetAllKsgKpgBySluchIdAsync(int SluchId)
    {
        return await Context.KsgKpgs.Where(s => s.Id == SluchId).ToListAsync();
    }
    
    public async Task<IEnumerable<LekPrSl>> GetAllLekPrSlBySluchIdAsync(int SluchId)
    {
        return await Context.LekPrSls.Where(s => s.Id == SluchId).ToListAsync();
    }

    public async Task<IEnumerable<INJ>> GetAllINJByLekPrIdAsync(int lekprId)
    {
        return await Context.INJs.Where(s => s.LEkPrId == lekprId).ToListAsync();
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

    public async Task<IEnumerable<Usl>> GetAllUslBySluchIdAsync(int SluchId)
    {
        var usls = Context.Usls.Where(s => s.SluchId == SluchId);

        if (!string.IsNullOrEmpty(Init.FILT_USL_NOTAKE_CODE_USL))
            return await usls.Where(s => !s.CODE_USL.StartsWith( Init.FILT_USL_NOTAKE_CODE_USL )).ToListAsync();


        return await usls.ToListAsync();
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

    public async Task<IEnumerable<OnkSl>> GetAllOnkSlBySluchIdAsync(int SluchId)
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

    public async Task<IEnumerable<OnkUsl>> GetAllOnkUslByOnkSlIdAsync(int OnkSlId)
    {
        return await Context.OnkUsls.Where(s => s.OnkSlId == OnkSlId).ToListAsync();
    }
}