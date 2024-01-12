using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading.Maui;

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
        var cachedImage = sender as CachedImage; // Cast sender thành CachedImage
        if (cachedImage != null)
        {
            var character = cachedImage.BindingContext as Root; // Sử dụng BindingContext từ CachedImage
            if (character != null)
            {
                HRViewModel.Instance.SelectedCharacter = character;
                Navigation.PushAsync(new DetailPage()); // Đảm bảo DetailPage được khởi tạo đúng
            }
        }
    }

    private void Button_OnClickedPrevious(object sender, EventArgs e)
    {
        myViewModel.PageNumber -= 1;
    }

    private void Button_OnClickedNext(object sender, EventArgs e)
    {
        myViewModel.PageNumber += 1;
    }
}