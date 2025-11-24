public class RepositoryTest<T> where T : DbContext
{
    public IEnumerable<Schet> GetTestSchets(int year, int month)
    {
        return new RepositoryMIS(new MisContext()).GetSchets(year, month).Result;
    }

    public IEnumerable<Schet_mtr> GetTestMtrSchets(int year, int month)
    {
        return new RepositoryMTR(new MtrContext()).GetSchets(year, month).Result;
    }

    public IEnumerable<LekPrSl> GetTestLEKPRSL()
    {
        return new RepositoryMIS(new MisContext()).GetAllLekPrSlBySluchIdAsync(18405830).Result;
    }
}