using Domain.Contracts;
using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;
using Domain.Extensions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Presentation.Components.Visitors;

public sealed class ApplyTextureToElementByCellVisitor : ICellVisitor
{
    private readonly List<string> _grassVariations = 
        [
            "Grass1.png",
            "Grass2.png",
            "Grass3.png",
            "Grass4.png",
            "Grass5.png",
            "Grass6.png",
        ];

    public ApplyTextureToElementByCellVisitor(Image control)
    {
        Control = control;
    }

    public Image Control { get; }

    public void Visit(Cell cell)
    {
    }

    public void Visit(EmptyCell cell)
    {
        var variation = _grassVariations.PeekRandom(cell.Seed);
        var source = new BitmapImage(new Uri("/Resources/Images/Grass/" + variation, UriKind.Relative));
        Control.Source = source;
    }

    public void Visit(Wolf cell)
    {
        var source = new BitmapImage(new Uri("/Resources/Images/Wolfs/WolfMale.png", UriKind.Relative));
        Control.Source = source;
    }

    public void Visit(MaleWolf cell)
    {
        var source = new BitmapImage(new Uri("/Resources/Images/Wolfs/WolfMale.png", UriKind.Relative));
        Control.Source = source;
    }

    public void Visit(FemaleWolf cell)
    {
        var source = new BitmapImage(new Uri("/Resources/Images/Wolfs/WolfFemale.png", UriKind.Relative));
        Control.Source = source;
    }

    public void Visit(Bunny cell)
    {
        var source = new BitmapImage(new Uri("/Resources/Images/Bunnies/Bunny.png", UriKind.Relative));
        Control.Source = source;
    }
}
