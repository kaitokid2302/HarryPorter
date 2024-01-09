namespace HarryPorter;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
		BindingContext = HRViewModel.Instance;
	}

	 void Button_OnClicked(object sender, EventArgs e)
	{
		 Navigation.PushAsync(new DetailPage());
	}

	private void ImageButton_OnClicked(object sender, EventArgs e)
	{
		var imageButton = sender as ImageButton;
		if (imageButton != null)
		{
			var character = imageButton.BindingContext as Root;
			if (character != null)
			{
				HRViewModel.Instance.SelectedCharacter = character;
				Navigation.PushAsync(new DetailPage());
			}
		}
	}
}
