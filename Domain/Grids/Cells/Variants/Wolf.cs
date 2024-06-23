using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public abstract record Wolf : Cell
{
    protected const int JUMP_RANGE = 1;
    protected const int CHASE_RANGE = 1;

    protected const int POINTS_PER_CHASE = 10;
    protected const int POINTS_COST_PER_TICK = 1;

    protected Wolf(int x, int y) : base(x, y)
    {
        Occupied = true;
        Points = 10;
    }

    public int Points { get; set; }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void Tick(Grid grid)
    {
        Ticked = true;
        Points -= POINTS_COST_PER_TICK;

        if (Points <= 0) 
        {
            grid.Place(new EmptyCell(X, Y));
            grid.Logs.AppendLine($"{nameof(Wolf)} died");
            return;
        }

        var chaseArea = grid.AreaAround(X, Y, CHASE_RANGE);

        if (TryChase(chaseArea))
            return;

        var jumpArea = grid.AreaAround(X, Y, JUMP_RANGE);

        if (TryJump(jumpArea))
            return;
    }

    protected bool TryJump(GridSpan area)
    {
        var destination = area.PeekRandom();

        if (destination.Occupied)
        {
            area.Grid.Logs.AppendLine($"{nameof(Wolf)} is waiting");
            return false;
        }

        area.Grid.Swap(this, destination);

        if (destination.Ticked is false)
            destination.Tick(area.Grid);

        area.Grid.Logs.AppendLine($"{nameof(Wolf)} jumped to ({destination.X}, {destination.Y})");

        return true;
    }

    protected bool TryChase(GridSpan area)
    {
        var bunny = area.First((cell, x, y) => cell is Bunny);

        if (bunny is null)
            return false;

        var x = X;
        var y = Y;

        area.PlaceAt(this, bunny.X, bunny.Y);
        area.Place(new EmptyCell(x, y));
        Points += POINTS_PER_CHASE;
        area.Grid.Logs.AppendLine($"{nameof(Wolf)} ate a {nameof(Bunny)}");

        return true;
    }
}
