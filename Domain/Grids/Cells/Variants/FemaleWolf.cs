using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record FemaleWolf : Wolf
{
    public FemaleWolf(int x, int y) : base(x, y)
    {
        Occupied = true;
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override Cell Copy()
    {
        return new FemaleWolf(X, Y) { Points = Points };
    }
}
