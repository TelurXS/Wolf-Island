using Domain.Contracts;
using Domain.Grids.Cells;
using System.Text;

namespace Domain.Grids;

public sealed record Grid : ITickable
{
    public Grid(int width, int height)
    {
        Width = width; 
        Height = height;
        Cells = new Cell[Width, Height];
        ApplyForEach((x, y) => new EmptyCell(x, y));
    }

    public Grid(int size) : this(size, size) { }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public bool Ticked { get; set; }

    public Cell[,] Cells { get; private set; }

    public StringBuilder Builder { get; private set; } = new();

    public bool IsValidCoordinates(int x, int y)
    {
        if (x < 0 || x >= Width)
            return false;

        if (y < 0 || y >= Height)
            return false;

        return true;
    }

    public void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || x >= Width)
            throw new ArgumentOutOfRangeException(nameof(x));

        if (y < 0 || y >= Height)
            throw new ArgumentOutOfRangeException(nameof(y));
    }

    public Cell At(int x, int y)
    {
        ValidateCoordinates(x, y);
        return Cells[x, y];
    }

    public void PlaceAt(Cell cell, int x, int y)
    {
        ValidateCoordinates(x, y);
        Cells[x, y] = cell;
        cell.X = x;
        cell.Y = y;
    }

    public void Place(Cell cell)
    {
        ValidateCoordinates(cell.X, cell.Y);
        Cells[cell.X, cell.Y] = cell;
    }

    public void ForEach(Action<Cell, int, int> action)
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                var cell = At(x, y);
                action.Invoke(cell, x, y);
            }
        }
    }

    public void ApplyForEach(Func<int, int, Cell> action)
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                Cells[x, y] = action.Invoke(x, y);
            }
        }
    }

    public void Swap(Cell from, Cell to)
    {
        ValidateCoordinates(from.X, from.Y);
        ValidateCoordinates(to.X, to.Y);

        var x = from.X;
        var y = from.Y;

        PlaceAt(from, to.X, to.Y);
        PlaceAt(to, x, y);
    }

    public void Tick(Grid grid)
    {
        Ticked = true;
        ForEach((cell, x, y) => { if (cell.Ticked is false) cell.Tick(grid); });
        Ticked = false;
        ForEach((cell, x, y) => cell.Ticked = false);
    }
}
