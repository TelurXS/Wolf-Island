namespace Domain.Contracts;

public interface IVisitable<in T>
{
    void Accept(T visitor);
}
