namespace HarryPorter;

public partial class MainPage : TabbedPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = HRViewModel.Instance;
    }
}