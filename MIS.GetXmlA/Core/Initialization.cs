using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS_MTR_RH_RI_RE.GetXmlA.Core
{
    public class Initialization
    {
        public static int YEAR_REPORT = 0;
        public static int MONTH_REPORT = 0;

        public static int TYPE_OUT_XML_RH;
        public static int TYPE_OUT_XML_RHE;
        public static int TYPE_OUT_XML_RI;
        public static int TYPE_OUT_XML_RIE;
        public static List<string>? SCHET_NSCHET_MIS;
        public static List<string>? SCHET_NSCHET_MTR;
        public static List<int>? SCHET_ID_MIS;
        public static List<int>? SCHET_ID_MTR;
        public static int GET_FROM_MISDB;
        public static int GET_FROM_MTRDB;
        public static string? FILT_USL_NOTAKE_CODE_USL;
        public static List<string>? NO_CREATE_DS_FIELD_IN_USL_BY_DATA;
        public static int GET_EMPTY_FROM_MISDB;
        public static int GET_EMPTY_FROM_MTRDB;
        public static int GET_FROM_MISDB_IDENT;
        public static int GET_FROM_MTRDB_IDENT;
        public static int PACKET_MTR_NUM_START = 0;
        public static int PACKET_MIS_NUM_START = 0;

        public static bool THREAD_ONE;

        public static void Get()
        {
            THREAD_ONE = int.TryParse(RepositorySettings.GetSection("THREAD_ONE"), out int value) && value == 1;

            TYPE_OUT_XML_RH = int.Parse(RepositorySettings.GetSection("TYPE_OUT_XML_RH"));
            TYPE_OUT_XML_RHE = int.Parse(RepositorySettings.GetSection("TYPE_OUT_XML_RHE"));
            TYPE_OUT_XML_RI = int.Parse(RepositorySettings.GetSection("TYPE_OUT_XML_RI"));
            TYPE_OUT_XML_RIE = int.Parse(RepositorySettings.GetSection("TYPE_OUT_XML_RIE"));

            SCHET_NSCHET_MIS = RepositorySettings.GetSection("SCHET_NSCHET_MIS").Split(',')
                ?.ToList()
                ?.Where(s => !string.IsNullOrWhiteSpace(s))
                ?.ToList();

            SCHET_NSCHET_MTR = RepositorySettings.GetSection("SCHET_NSCHET_MTR").Split(',')
                ?.ToList()
                ?.Where(s => !string.IsNullOrWhiteSpace(s))
                ?.ToList();

            SCHET_ID_MIS = RepositorySettings.GetSection("SCHET_ID_MIS").Split(',')
                ?.ToList()
                ?.Where(s => !string.IsNullOrWhiteSpace(s))
                ?.Where(s => int.TryParse(s, out _))
                ?.Select(int.Parse)
                ?.ToList(); 

            SCHET_ID_MTR = RepositorySettings.GetSection("SCHET_ID_MTR").Split(',')
                ?.ToList()
                ?.Where(s => !string.IsNullOrWhiteSpace(s))
                ?.Where(s => int.TryParse(s, out _))
                ?.Select(int.Parse)
                ?.ToList();

            GET_FROM_MISDB = int.Parse(RepositorySettings.GetSection("GET_FROM_MISDB"));
            GET_FROM_MTRDB = int.Parse(RepositorySettings.GetSection("GET_FROM_MTRDB"));

            GET_EMPTY_FROM_MISDB = int.Parse(RepositorySettings.GetSection("GET_EMPTY_FROM_MISDB"));
            GET_EMPTY_FROM_MTRDB = int.Parse(RepositorySettings.GetSection("GET_EMPTY_FROM_MTRDB"));

            GET_FROM_MISDB_IDENT = int.Parse(RepositorySettings.GetSection("GET_FROM_MISDB_IDENT"));
            GET_FROM_MTRDB_IDENT = int.Parse(RepositorySettings.GetSection("GET_FROM_MTRDB_IDENT"));

            PACKET_MTR_NUM_START = int.Parse(RepositorySettings.GetSection("PACKET_MTR_NUM_START"));
            PACKET_MIS_NUM_START = int.Parse(RepositorySettings.GetSection("PACKET_MIS_NUM_START"));

            FILT_USL_NOTAKE_CODE_USL = RepositorySettings.GetSection("FILT_USL_NOTAKE_CODE_USL");

            NO_CREATE_DS_FIELD_IN_USL_BY_DATA = RepositorySettings.GetSection("NO_CREATE_DS_FIELD_IN_USL_BY_DATA").Split(',').ToList();
        }
    }
}
