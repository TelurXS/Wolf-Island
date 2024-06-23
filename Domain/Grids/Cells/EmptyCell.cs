using Domain.Contracts;

namespace Domain.Grids.Cells;

public sealed record EmptyCell : Cell
{
    public EmptyCell(int x, int y) : base(x, y)
    {
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override Cell Copy()
    {
        return new EmptyCell(X, Y) { Seed = Seed };
    }
}
