using Domain.Contracts;
using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;
using System.Text;

namespace Presentation.Components.Visitors;

public sealed class AddNameToStringBuilderByCellVisitor : ICellVisitor
{
    public AddNameToStringBuilderByCellVisitor(StringBuilder builder)
    {
        Builder = builder;
    }

    public StringBuilder Builder { get; }

    public void Visit(Cell cell)
    {
        Builder.Append(cell.GetType().Name);
    }

    public void Visit(EmptyCell cell)
    {
        Builder.Append("Grass");
    }

    public void Visit(Wolf cell)
    {
        Builder.Append("Wolf");
    }

    public void Visit(MaleWolf cell)
    {
        Builder.Append("Wolf (M)");
    }

    public void Visit(FemaleWolf cell)
    {
        Builder.Append("Wolf (F)");
    }

    public void Visit(Bunny cell)
    {
        Builder.Append("Bnuuy");
    }
}
