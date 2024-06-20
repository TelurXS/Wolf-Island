using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record FemaleWolf : Cell
{
    public FemaleWolf(int x, int y) : base(x, y)
    {
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }
}
