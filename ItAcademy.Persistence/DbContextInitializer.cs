using AutoFixture;

namespace ItAcademy.Persistence;

public static class DbContextInitializer
{
    private static readonly Fixture _fixture;

    static DbContextInitializer()
    {
        _fixture = new Fixture();
    }
    
    public static List<T> Seed<T>(int count)
    {
        return _fixture.Build<T>().CreateMany(count).ToList();
    }
}