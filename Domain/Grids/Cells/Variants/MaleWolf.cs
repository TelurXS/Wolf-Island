using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record MaleWolf : Cell
{
    public MaleWolf(int x, int y) : base(x, y)
    {
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }
}
