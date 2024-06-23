namespace Domain.Contracts;

public interface ICopyable<out T> where T : class
{
    /// <summary>
    /// Creates a copy of this object
    /// </summary>
    /// <returns>A copy of this object</returns>
    T Copy();
}
