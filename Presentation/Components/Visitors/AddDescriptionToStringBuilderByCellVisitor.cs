using Domain.Contracts;
using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;
using System.Text;

namespace Presentation.Components.Visitors;

public sealed class AddDescriptionToStringBuilderByCellVisitor : ICellVisitor
{
    public AddDescriptionToStringBuilderByCellVisitor(StringBuilder builder)
    {
        Builder = builder;
    }

    public StringBuilder Builder { get; }

    public void Visit(Cell cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
    }

    public void Visit(EmptyCell cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
    }

    public void Visit(Wolf cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
        Builder.AppendLine($"• Points: {cell.Points / 10f}");
    }

    public void Visit(MaleWolf cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
        Builder.AppendLine($"• Points: {cell.Points / 10f}");
        Builder.AppendLine($"• Cooldown: {cell.Cooldown}");
        Builder.AppendLine($"• Gender: Male");
    }

    public void Visit(FemaleWolf cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
        Builder.AppendLine($"• Points: {cell.Points / 10f}");
        Builder.AppendLine($"• Gender: Female");
    }

    public void Visit(Bunny cell)
    {
        Builder.AppendLine($"• X: {cell.X}");
        Builder.AppendLine($"• Y: {cell.Y}");
        Builder.AppendLine($"• Gender: Non Binary");
    }
}
