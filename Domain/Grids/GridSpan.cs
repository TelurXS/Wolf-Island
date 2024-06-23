using Domain.Contracts;
using Domain.Extensions;
using Domain.Grids.Cells;

namespace Domain.Grids;

public sealed record GridSpan : ICopyable<GridSpan>
{
    public GridSpan(Grid grid, int xFrom, int yFrom, int xTo, int yTo)
    {
        Grid = grid;
        XFrom = xFrom;
        YFrom = yFrom;
        XTo = xTo;
        YTo = yTo;
    }

    public GridSpan(Grid grid, int x, int y, int range) 
        : this(grid, x - range, y - range, x + range, y + range) 
    { 
    }

    public Grid Grid { get; }

    public int XFrom { get; }
    public int YFrom { get; }
    public int XTo { get; }
    public int YTo { get; }


    public Cell At(int x, int y)
    {
        return Grid.At(x, y);
    }

    public void PlaceAt(Cell cell, int x, int y)
    {
        Grid.PlaceAt(cell, x, y);
    }

    public void Place(Cell cell)
    {
        Grid.Place(cell);
    }

    public void ForEach(Action<Cell, int, int> action)
    {
        for (int x = XFrom; x <= XTo; x++)
        {
            for (int y = YFrom; y <= YTo; y++)
            {
                var cell = At(x, y);
                action.Invoke(cell, x, y);
            }
        }
    }

    public void ApplyForEach(Func<int, int, Cell> action)
    {
        for (int x = XFrom; x <= XTo; x++)
        {
            for (int y = YFrom; y <= YTo; y++)
            {
                Grid.PlaceAt(action.Invoke(x, y), x, y);
            }
        }
    }

    public bool All(Func<Cell, int, int, bool> action)
    {
        for (int x = XFrom; x <= XTo; x++)
        {
            for (int y = YFrom; y <= YTo; y++)
            {
                var cell = At(x, y);

                if (action.Invoke(cell, x, y) is false)
                    return false;
            }
        }

        return true;
    }

    public bool Any(Func<Cell, int, int, bool> action)
    {
        for (int x = XFrom; x <= XTo; x++)
        {
            for (int y = YFrom; y <= YTo; y++)
            {
                var cell = At(x, y);

                if (action.Invoke(cell, x, y))
                    return true;
            }
        }

        return false;
    }

    public Cell? First(Func<Cell, int, int, bool> action)
    {
        for (int x = XFrom; x <= XTo; x++)
        {
            for (int y = YFrom; y <= YTo; y++)
            {
                var cell = At(x, y);

                if (action.Invoke(cell, x, y))
                    return cell;
            }
        }

        return null;
    }

    public Cell PeekRandom()
    {
        int x = Random.Shared.Next(XFrom, XTo + 1);
        int y = Random.Shared.Next(YFrom, YTo + 1);
        return At(x, y);
    }

    public GridSpan Copy()
    {
        return new GridSpan(this);
    }
}
