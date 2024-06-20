using Domain.Grids;
using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;
using Presentation.Components.Visitors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Grid = Domain.Grids.Grid;

namespace Presentation.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(InfoWindow infoWindow)
    {
        InitializeComponent();

        Grid = new Grid(20);

        Grid.Place(new Bunny(3, 3));
        Grid.Place(new MaleWolf(16, 16));

        InitializeGrid();
        InfoWindow = infoWindow;
    }

    public InfoWindow InfoWindow { get; }
    private Grid Grid { get; set; }

    private void InitializeGrid()
    {
        CellPresenters.Columns = Grid.Width;
        CellPresenters.Rows = Grid.Height;

        CellPresenters.Children.Clear();

        Grid.ForEach((cell, x, y) =>
        {
            var image = new Image();
            var visitor = new ApplyTextureToElementByCellVisitor(image);
            cell.Accept(visitor);
            image.MouseDown += (sender, args) => OnCellClick(cell);

            CellPresenters.Children.Add(image);
        });

        TextBlock_Logs.Text = Grid.Builder.ToString();
        Grid.Builder.Clear();
    }

    private void OnCellClick(Cell cell)
    {
        InfoWindow.Owner = this;
        InfoWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        InfoWindow.Show(cell);
    }

    private void Button_Next_Click(object sender, RoutedEventArgs e)
    {
        Grid.Tick(Grid);
        InitializeGrid();
    }
}