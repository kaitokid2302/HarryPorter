using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPorter;

public partial class House : ContentPage
{
    private HRViewModel myViewModel = HRViewModel.Instance;
    public House()
    {
        InitializeComponent();
        BindingContext = myViewModel;
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DetailPage());
    }


    void ImageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        myViewModel.Display(textSearch.Text);
    }

    void textSearch_Completed(System.Object sender, System.EventArgs e)
    {
        myViewModel.Display(textSearch.Text);
    }

    void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
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