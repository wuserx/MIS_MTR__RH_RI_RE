public class RepositorySettings
{ 
    protected static readonly ConfigurationRoot Configuration = Builder();

    public RepositorySettings()
    {
        
    }
   
    public static ConfigurationRoot Builder()
    {
        var builder = new ConfigurationBuilder();
        // установка пути к текущему каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());
        // получаем конфигурацию из файла appsettings.json
        builder.AddJsonFile("appsettings.json");
        // создаем конфигурацию
        return (ConfigurationRoot)builder.Build();
    }


    public static string GetConnectionString(string json_key)
    {
        string? constr = Configuration.GetConnectionString(json_key);

        if (string.IsNullOrEmpty(constr))
        {
            throw new Exception($"проверить {json_key} в настройках строку подключения");
        }   

        return constr;
    }

    public static DirectoryInfo GetSaveXmlFileFolder()
    {
        var path = Configuration.GetSection("SaveXmlFileFolder").Value;
        if (!string.IsNullOrEmpty(path))
            return Directory.CreateDirectory(path);
        return Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XML");
    }

    public static string GetSection(string section)
    {
        return Configuration.GetSection(section).Value;
    }
}