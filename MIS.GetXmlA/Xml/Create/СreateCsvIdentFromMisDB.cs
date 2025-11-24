using CsvHelper;
using CsvHelper.Configuration;
using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net.Sockets;

public class СreateCsvIdentFromMisDB
{
    public СreateCsvIdentFromMisDB()
    {
    }

    public void Run(Schet schet, IEnumerable<ZakSluch> zakSluchs, string fileNameXml)
    {
        var ischet = GetSCHET(schet, zakSluchs);

        var ident = new IDENT()
        {
            Schet = ischet
        };

        var records = new List<MIS_MTR_RH_RI_RE.GetXmlA.Models.I.IDENT> { ident };

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            Encoding = Encoding.ASCII,
            
            // Add more configuration options as needed
        };

        using (var writer = new StreamWriter($"MisIdentOutput{DateTime.Now.ToString("HHmmss_fff")}.csv"))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(records);
        }
    }


    private MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SCHET GetSCHET(Schet schet, IEnumerable<ZakSluch> zakSluchs) 
    {
        var ischet = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SCHET()
        {
            CODE = new RepositoryMIS(new MisContext()).GetSchetCODE(schet),
            YEAR = schet.YEAR_REPORT,
            MONTH = schet.MONTH_REPORT
        };

        foreach (var zakSluch in zakSluchs)
        {

            Console.WriteLine(zakSluch.Id);

            var zap = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.ZAP()
            {
                N_ZAP = new RepositoryMIS(new MisContext()).GetZapN_ZAP(zakSluch)
            };

            var z_sl = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.Z_SL()
            {
                IDCASE = new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)
            };

            var sluchs = new RepositoryMIS(new MisContext()).GetAllSluchByZakSluchIdAsync(zakSluch.Id).Result;

            foreach (var sluch in sluchs)
            {
                var sl = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SL()
                {
                    SL_ID = new RepositoryMIS(new MisContext()).GetSluchSL_ID(sluch)
                };

                z_sl.SL.Add(sl);
            }

            zap.Z_SL.Add(z_sl);

            ischet.ZAP.Add(zap);
        }

        return ischet;
    }


}