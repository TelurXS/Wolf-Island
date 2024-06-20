using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record Bunny : Cell
{
    public Bunny(int x, int y) : base(x, y)
    {
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void Tick(Grid grid)
    {
        int randX = Random.Shared.Next(-1, 2);
        int randY = Random.Shared.Next(-1, 2);

        int x = X + randX;
        int y = Y + randY;

        if (X == x && Y == y)
        {
            Ticked = true;
            grid.Builder.AppendLine($"{nameof(Bunny)} is waiting");
            return;
        }

        if (grid.IsValidCoordinates(x, y))
        {
            var destination = grid.At(x, y);

            if (destination.Occupied)
            {
                Ticked = true;
                grid.Builder.AppendLine($"{nameof(Bunny)} is waiting");
                return;
            }

            grid.Swap(this, destination);
            Ticked = true;
            grid.Builder.AppendLine($"{nameof(Bunny)} jumped to ({x}, {y}) ({randX}, {randY})");
        }

    }
}
