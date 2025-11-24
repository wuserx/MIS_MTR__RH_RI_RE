public class PathFileNameHelper
{
    private readonly string PacketName;
    private readonly string XmlName;

    public PathFileNameHelper()
    {
    }

    public PathFileNameHelper(Schet schet, string filenamexml)
    {
        new RouteFileNameHerper(schet, filenamexml).GetAllName(out string packetName, out string xmlName);
        PacketName = packetName.ToUpper().Replace(".ZIP","").Replace(".OMS","");
        XmlName = xmlName;
    }    

    public string GetPathSmoLFileXmlSave()
    {
        string path = GetPathForSmo();

        return string.Concat(path, "\\", PacketName, "\\", GetFilePrefixNameFromL(XmlName), ".", Extension.XML);
    }

    public string GetPathSmoNoLFileXmlSave()
    {
        string path = string.Concat(GetPathForSmo(), "\\", PacketName);
        
        Directory.CreateDirectory(path);

        return string.Concat(path, "\\", XmlName, ".", Extension.XML);
    }

    public string GetPathSmoNoLFileXmlSave(string filename)
    {
        string path = string.Concat(GetPathForSmo(), "\\", filename);

        Directory.CreateDirectory(path);

        return string.Concat(path, "\\", filename, ".", Extension.XML);
    }

    public string GetPathFfomsNoLFileXmlSave(string filename, string folder)
    {
        string path = string.Concat(GetPathForFfoms(), "\\", folder, "\\", filename.Replace("H", "").Replace("I", "").Replace("E", "").Substring(0,8));

        Directory.CreateDirectory(path);

        return string.Concat(path, "\\", filename, ".", Extension.XML);
    }

    public string GetPathFfomsNoLSchetDeleteFileXmlSave(string filename)
    {
        string path = string.Concat(GetPathForFfoms(), "\\", filename.Replace("H", "").Replace("I", "").Replace("E", "").Substring(0, 8), "_SCHET_DELETE");

        Directory.CreateDirectory(path);

        return string.Concat(path, "\\", filename, ".", Extension.XML);
    }
    

    public string GetPathMoFileXmlSave()
    {
        string path = string.Concat(GetPathForMo(), "\\S", PacketName);

        Directory.CreateDirectory(path);

        return string.Concat(path, "\\S", XmlName, ".", Extension.XML);
    }

    public string GetPathForMo()
    {
        return string.Concat(RepositorySettings.GetSaveXmlFileFolder(), $"\\MO");
    }

    public string GetPathForSmo()
    {
        return string.Concat(RepositorySettings.GetSaveXmlFileFolder(), "\\SMO");
    }

    public string GetPathForFfoms()
    {
        return string.Concat(RepositorySettings.GetSaveXmlFileFolder(), "\\FFOMS");
    }

    public string GetFilePrefixNameFromL(string FileName)
    {
        StringBuilder sb = new StringBuilder(FileName);

        if (Char.ToUpper(FileName[0]) == 'H')
            return string.Concat("L", sb.Remove(0, 1));
        if (Char.ToUpper(FileName[0]) == 'T')
            return string.Concat("L", FileName);
        if (Char.ToUpper(FileName[0]) == 'D')
            return string.Concat("L", sb.Remove(0, 1));
        if (Char.ToUpper(FileName[0]) == 'C')
            return string.Concat("L", FileName);
        return FileName;
    }
}