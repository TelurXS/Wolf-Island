using Domain.Contracts;

namespace Domain.Grids.Cells.Variants;

public sealed record MaleWolf : Wolf
{
    private const int BREED_RANGE = 1;
    private const int BREED_COOLDOWN = 3;
    private const float FEMALE_BORN_CHANCE = 0.5f;

    public MaleWolf(int x, int y) : base(x, y)
    {
        Occupied = true;
    }

    public int Cooldown { get; set; } = BREED_COOLDOWN;

    public override void Tick(Grid grid)
    {
        Ticked = true;

        if (Cooldown > 0)
            Cooldown--;

        var breedArea = grid.AreaAround(X, Y, BREED_RANGE);

        if (TryBreed(breedArea))
            return;

        base.Tick(grid);
    }

    private bool TryBreed(GridSpan area)
    {
        if (Cooldown > 0)
            return false;

        if (area.Any((cell, x, y) => cell is FemaleWolf) is false)
            return false;

        var destinatrion = area.First((cell, x, y) => cell.Occupied is false);

        if (destinatrion is null)
            return false;

        Wolf wolf;

        if (Random.Shared.NextSingle() >= FEMALE_BORN_CHANCE)
            wolf = new FemaleWolf(destinatrion.X, destinatrion.Y);

        else
            wolf = new MaleWolf(destinatrion.X, destinatrion.Y);

        Cooldown = BREED_COOLDOWN;
        wolf.Ticked = true;
        area.Place(wolf);
        area.Grid.Logs.AppendLine($"{nameof(Wolf)} was born");

        return true;
    }

    public override void Accept(ICellVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override Cell Copy()
    {
        return new MaleWolf(X, Y) { Points = Points, Cooldown = Cooldown };
    }
}
