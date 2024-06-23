
namespace Domain.Extensions;

public static class RandomExtensions
{
    public static int InRange(this Random random, int range)
    {
        return random.Next(-range, range + 1);
    }
}
