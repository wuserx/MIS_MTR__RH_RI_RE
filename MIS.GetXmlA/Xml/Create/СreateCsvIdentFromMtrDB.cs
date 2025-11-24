using CsvHelper;
using CsvHelper.Configuration;
using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net.Sockets;

public class СreateCsvIdentFromMtrDB
{
    public СreateCsvIdentFromMtrDB()
    {
    }

    public void Run(Schet_mtr schet, IEnumerable<ZakSluch_mtr> zakSluchs, string fileNameXml)
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

        using (var writer = new StreamWriter($"MtrIdentOutput{DateTime.Now.ToString("HHmmss_fff")}.csv"))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(records);
        }
    }


    private MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SCHET GetSCHET(Schet_mtr schet, IEnumerable<ZakSluch_mtr> zakSluchs) 
    {
        var ischet = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SCHET()
        {
            CODE = new RepositoryMTR(new MtrContext()).GetSchetCODE(schet),
            YEAR = (int)schet.YEAR,
            MONTH = (int)schet.MONTH
        };

        foreach (var zakSluch in zakSluchs)
        {

            Console.WriteLine(zakSluch.Id);

            var zap = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.ZAP()
            {
                N_ZAP = new RepositoryMTR(new MtrContext()).GetZapN_ZAP(zakSluch)
            };

            var z_sl = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.Z_SL()
            {
                IDCASE = new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)
            };

            var sluchs = new RepositoryMTR(new MtrContext()).GetAllSluchByZakSluchIdAsync(zakSluch.Id).Result;

            foreach (var sluch in sluchs)
            {
                var sl = new MIS_MTR_RH_RI_RE.GetXmlA.Models.I.SL()
                {
                    SL_ID = new RepositoryMTR(new MtrContext()).GetSluchSL_ID(sluch)
                };

                z_sl.SL.Add(sl);
            }

            zap.Z_SL.Add(z_sl);

            ischet.ZAP.Add(zap);
        }

        return ischet;
    }


}