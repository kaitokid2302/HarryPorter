using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPorter;

public partial class Setting : ContentPage
{
    public Setting()
    {
        InitializeComponent();
        BindingContext = HRViewModel.Instance;
    }

    void ScrollView_Scrolled(System.Object sender, Microsoft.Maui.Controls.ScrolledEventArgs e)
    {
        // if user scroll to the most top, the reload, checking by e.ScrollY
        
    }

    void Switch_Toggled(System.Object sender, Microsoft.Maui.Controls.ToggledEventArgs e)
    {
        // check if sender is Switch, then dark, else light
        var cur = sender as Switch;
        if (cur.IsToggled)
        {
            // dark mode
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            // light mode
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var button = (Button)sender;
        var question = (Question)button.BindingContext;

        // ClassId được sử dụng để lưu trữ chỉ số của câu trả lời
        int selectedOption = int.Parse(button.ClassId);

        if (selectedOption == question.CorrectAnswer)
        {
            button.BackgroundColor = Colors.Green;
        }
        else
        {
            button.BackgroundColor = Colors.Red;
        }
    }
}