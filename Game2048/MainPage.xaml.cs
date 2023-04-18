using Game2048.ViewModel;

namespace Game2048;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		this.BindingContext = new BoardViewModel();
	}


}

