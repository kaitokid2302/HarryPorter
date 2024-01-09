
namespace HarryPorter;

public partial class DetailPage : ContentPage
{
    public DetailPage()
    {
        InitializeComponent();
        BindingContext = HRViewModel.Instance.SelectedCharacter;
        // get current theme
        var currentTheme = Application.Current.UserAppTheme;
        // if current theme is dark, then set the background color to black
        if (currentTheme == AppTheme.Dark)
        {
            BackgroundColor = Colors.Gray;
        }
        else
        {
            BackgroundColor = Colors.LightBlue;
        }
    }
}