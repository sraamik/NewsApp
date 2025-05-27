using NewsApplication.Models;

namespace NewsApplication.Pages;

public partial class NewsDetailsPage : ContentPage
{
    private string uri;
    private Article article;

    public NewsDetailsPage(Article article)
	{
		InitializeComponent();
        ImgNews.Source = article.Image;
		LblTitle.Text = article.Title;
		LblContent.Text = article.Content;
        uri = article.Url;
    }

    private async void TbShare_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(uri))
        {

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = uri,
                Title = "Share"
            });
            //await Navigation.PushAsync(new NewsDetailsPage(article));

            await DisplayAlert("Shared", "The article was shared successfully.", "OK");


            await Navigation.PopAsync();

            
        }

       




    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}