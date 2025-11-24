public class RouteFileNameHerper
{
    private readonly string local_MO;
    private readonly string local_SMO;
    private readonly string local_TFOMS;
    private readonly string local_packetName;
    private readonly string local_filenameXml;

    public RouteFileNameHerper(Schet schet, string filenameZakSluch)
    {
        local_MO = GetNormalCodeMO(schet.FILENAME); //string.Concat("M", schet.CODE_MO);
        local_SMO = string.Concat("S", schet.PLAT);
        local_TFOMS = "T07";
        local_packetName = schet.FILENAME_PACKET;
        local_filenameXml = filenameZakSluch;
    }

    public void GetAllName(out string PacketName, out string XmlName)
    {
        PacketName = GetNewFileNamePacketByRoute();
        XmlName = GetNewFileNameXmlByRoute();
    }

    public string GetNewFileNameXmlByRoute()
    {  
        var local_filenameXmlArr = local_filenameXml.Split("_");

        var filenameXmlChapter1 = local_filenameXmlArr[0].Replace(local_MO, "").Replace(local_SMO, "").Replace(local_TFOMS, "");
        var filenameXmlChapter2 = local_filenameXmlArr[1][..4];
        var filenameXmlChapter3 = local_filenameXmlArr[1][4..];

        if (GetSectionFrom() == "T")
        {
            //XX+T07+S+код смо_период+М+кодМО+номер пакета
            return string.Concat(filenameXmlChapter1,
                                    local_TFOMS,
                                    local_SMO,
                                    "_",
                                    filenameXmlChapter2,
                                    local_MO,
                                    filenameXmlChapter3
                                    );
        }

        return string.Concat(filenameXmlChapter1, GetStringRoutePacketFrom(), GetStringRoutePacketTo(), "_", filenameXmlChapter2, filenameXmlChapter3);
    }

    public string GetNewFileNamePacketByRoute()
    {
        var local_packetNameArr = local_packetName.Split("_");

        var packetNameChapter1 = local_packetNameArr[0].Replace(local_MO, "").Replace(local_SMO, "").Replace(local_TFOMS, "");
        var packetNameChapter2 = local_packetNameArr[1][..4];
        var packetNameChapter3 = local_packetNameArr[1][4..];

        if (GetSectionFrom() == "T")
        {
            //XX+T07+S+код смо_период+М+кодМО+номер пакета
            return string.Concat(packetNameChapter1,
                                    local_TFOMS,
                                    local_SMO,
                                    "_",
                                    packetNameChapter2,
                                    local_MO,
                                    packetNameChapter3
                                    );
        }

        return string.Concat(packetNameChapter1, GetStringRoutePacketFrom(), GetStringRoutePacketTo(), "_", packetNameChapter2, packetNameChapter3);
    }

    //получаем строку - от куда
    private string GetStringRoutePacketFrom()
    {
        return GetPrefixRoute(GetSectionFrom());
    }

    private string GetSectionFrom()
    {
        return RepositorySettings.GetSection("PACKET_ROUTE:FROM");
    }

    //получаем строку - от куда
    private string GetStringRoutePacketTo()
    {
        return GetPrefixRoute(RepositorySettings.GetSection("PACKET_ROUTE:TO"));
    }

    //вернуть префикс маршрута, например: T07, S07004 и т.п.
    private string GetPrefixRoute(string route)
    {
        if (route == "T")
            return string.Concat(local_TFOMS);

        if (route == "M")
            return local_MO;

        if (route == "S")
            return local_SMO;

        return string.Empty;
    }

    private string GetNormalCodeMO(string schetFILENAME)
    {
        string _code_mo = schetFILENAME.Substring(schetFILENAME.IndexOf("M07"), 7);

        string pattern = @"\S07\d\d\d\d";

        Regex regex = new Regex(pattern);

        var result = regex.IsMatch(_code_mo);

        if (!result)
            throw new Exception("Не верный код МО в счете");

        return _code_mo;
    } 
}