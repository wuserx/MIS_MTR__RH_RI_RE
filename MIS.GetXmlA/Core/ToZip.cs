public class ToZip
{
    readonly string pathMO = new PathFileNameHelper().GetPathForMo();
    readonly string pathSMO = new PathFileNameHelper().GetPathForSmo();   

    public void SMODirectories()
    {
        string PathDirectory = string.Concat(pathSMO, "\\");
        Compress(PathDirectory);
    }

    public void MODirectories()
    {
        string PathDirectory = string.Concat(pathMO, "\\");
        Compress(PathDirectory);
    }

    private void Compress(string PathDirectory)
    {
        var paths = Directory.GetDirectories(PathDirectory);

        foreach (var path in paths)
        {
            //фильтр - имя файла
            if (Directory.Exists(path))
            {
                string fileZip = string.Concat(path, ".", Extension.ZIP);
                ZipFile.CreateFromDirectory(path, fileZip);
                Directory.Delete(path, true);
            }
        }
    }
}