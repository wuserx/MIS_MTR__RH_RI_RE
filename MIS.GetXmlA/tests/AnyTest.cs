public class AnyTest
{
    public void Get()
    {
        var p1 = new RepositoryTest<MtrContext>().GetTestLEKPRSL();
        var p2 = JsonConvert.DeserializeObject<List<SluchKoef>>("[{IDSL:1, Z_SL:1},{IDSL:1, Z_SL:1},{IDSL:1, Z_SL:1}]");
    }
}