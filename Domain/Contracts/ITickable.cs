using Domain.Grids;

namespace Domain.Contracts;

public interface ITickable
{
    bool Ticked { get; set; }

    void Tick(Grid grid);
}
