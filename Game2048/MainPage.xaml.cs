using Game2048.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Game2048;

public partial class MainPage : ContentPage
{
	int count = 0;
    private readonly BoardViewModel _viewModel;
    private GraphicsView graphicView;

    public MainPage()
	{
		InitializeComponent();
        _viewModel = new BoardViewModel();
         graphicView = this.FindByName<GraphicsView>("BoardView");
        this.BindingContext = _viewModel;

    }

    private void GraphicsView_StartInteraction(object sender, TouchEventArgs e)
    {
       _viewModel.StartInteraction(sender,e);
    
    }

    private void GraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {

        if (_viewModel.EndInteraction(sender, e, null)) 
        {
            graphicView.Invalidate();

        }
        //CounterLabel.Text += msg;
    }
}

