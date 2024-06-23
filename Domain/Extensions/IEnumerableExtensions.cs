namespace Domain.Extensions;

public static class IEnumerableExtensions
{
    public static T PeekRandom<T>(this IEnumerable<T> values, int? seed = null)
    {
        var random = Random.Shared;

        if (seed is not null)
            random = new Random(seed.Value);

        var index = random.Next(0, values.Count());
        return values.ElementAt(index);
    }
}
