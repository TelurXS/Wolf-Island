using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;

namespace Domain.Contracts;

public interface ICellVisitor
{
    void Visit(Cell cell);
    void Visit(EmptyCell cell);
    void Visit(Wolf cell);
    void Visit(MaleWolf cell);
    void Visit(FemaleWolf cell);
    void Visit(Bunny cell);
}
