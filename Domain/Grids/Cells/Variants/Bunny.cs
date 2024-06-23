using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record Bunny : Cell
{
    private const int JUMP_RANGE = 1;
    private const float SPLIT_CHANCE = 0.2f;

    public Bunny(int x, int y) : base(x, y)
    {
        Occupied = true;
    }

    public override void Tick(Grid grid)
    {
        Ticked = true;

        var area = grid.AreaAround(X, Y, JUMP_RANGE);     
        
        if (TrySplit(area))
            return;

        if (TryJump(area))
            return;
    }

    private bool TrySplit(GridSpan area)
    {
        if (Random.Shared.NextSingle() > SPLIT_CHANCE)
            return false;

        var spawnCell = area.PeekRandom();

        if (spawnCell.Occupied)
            return false;

        var bunny = new Bunny(spawnCell.X, spawnCell.Y);
        bunny.Ticked = true;
        area.Place(bunny);

        area.Grid.Logs.AppendLine($"{nameof(Bunny)} is splitted");

        return true;
    }

    private bool TryJump(GridSpan area)
    {
        var destination = area.PeekRandom();

        if (destination.Occupied)
        {
            area.Grid.Logs.AppendLine($"{nameof(Bunny)} is waiting");
            return false;
        }

        area.Grid.Swap(this, destination);
        
        if (destination.Ticked is false)
            destination.Tick(area.Grid);
        
        area.Grid.Logs.AppendLine($"{nameof(Bunny)} jumped to ({destination.X}, {destination.Y})");

        return true;
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override Cell Copy()
    {
        return new Bunny(X, Y);
    }
}
