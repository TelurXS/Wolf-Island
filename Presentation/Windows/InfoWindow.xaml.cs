using Domain.Grids.Cells;
using Presentation.Components.Visitors;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Presentation.Windows;

/// <summary>
/// Interaction logic for InfoWindow.xaml
/// </summary>
public partial class InfoWindow : Window
{
    public InfoWindow()
    {
        InitializeComponent();
    }

    public void Show(Cell cell)
    {
        cell.Accept(new ApplyTextureToElementByCellVisitor(Image_Icon));

        var builder = new StringBuilder();

        cell.Accept(new AddNameToStringBuilderByCellVisitor(builder));

        Label_Name.Content = builder.ToString();
        builder.Clear();

        cell.Accept(new AddDescriptionToStringBuilderByCellVisitor(builder));
        TextBlock_Description.Text = builder.ToString();

        ShowDialog();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}
