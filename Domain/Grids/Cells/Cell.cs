using Domain.Contracts;
using System.Runtime.CompilerServices;

namespace Domain.Grids.Cells;

public abstract record Cell : ITickable, IVisitable<ICellVisitor>
{
    protected Cell(int x, int y)
    {
        X = x; 
        Y = y;
        Occupied = false;
        Seed = Random.Shared.Next();
    }

    public int X { get; set; }
    public int Y { get; set; }

    public bool Occupied { get; set; }
    public bool Ticked { get; set; }
    public int Seed { get; private set; }

    public virtual void Tick(Grid grid) { }

    public virtual void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }
}
