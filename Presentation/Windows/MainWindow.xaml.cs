using Domain.Grids;
using Domain.Grids.Cells;
using Domain.Grids.Cells.Variants;
using Presentation.Components.Visitors;
using System;
using System.Collections.ObjectModel;
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
        DataContext = this;

        Grid = new Grid(20);

        Grid.Place(new Bunny(9, 9));
        Grid.Place(new Bunny(7, 8));
        Grid.Place(new Bunny(12, 11));
        Grid.Place(new Bunny(14, 12));
        Grid.Place(new MaleWolf(11, 11));
        Grid.Place(new FemaleWolf(10, 10));

        History.Add(Grid.Copy());
        InitializeGrid();
        InitializeButtons();
        InfoWindow = infoWindow;
    }

    public InfoWindow InfoWindow { get; }
    private Grid Grid { get; set; }

    public List<Grid> History { get; set; } = new();
    private int Selected { get; set; }

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

        TextBlock_Logs.Text = Grid.Logs.ToString();
    }

    private void InitializeButtons()
    {
        Panel_Buttons.Children.Clear();

        for (int i = 0; i < History.Count; i++)
        {
            var button = new Button
            {
                Content = i.ToString(),
            };

            if (Selected == i)
                button.Background = Brushes.Gray;

            button.Click += (sender, args) => Button_Select_Click(Panel_Buttons.Children.IndexOf(button));

            Panel_Buttons.Children.Add(button);
        }
    }

    private void OnCellClick(Cell cell)
    {
        InfoWindow.Owner = this;
        InfoWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        InfoWindow.Show(cell);
    }

    private void Button_Select_Click(int index)
    {
        if (index < 0 || index >= History.Count)
            return;

        Selected = index;
        Grid = History[Selected];
        InitializeGrid();
        InitializeButtons();
    }

    private void Button_Next_Click(object sender, RoutedEventArgs e)
    {
        if (Selected == History.Count - 1) 
        {
            Grid = Grid.Copy();
            Grid.Logs.Clear();
            Grid.Tick(Grid);
            Grid.Logs.AppendLine("==================");
            History.Add(Grid.Copy());
        }

        Selected++;
        Grid = History[Selected];
        InitializeGrid();
        InitializeButtons();
    }

    private void Button_Back_Click(object sender, RoutedEventArgs e)
    {
        if (Selected == 0)
            return;

        Selected--;
        Grid = History[Selected];
        InitializeGrid();
        InitializeButtons();
    }
}