using NewsApplication.Models;

namespace NewsApplication.Pages;

public partial class NewsDetailsPage : ContentPage
{
    private string uri;
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
                Uri = uri,
                Title = "Share"
            });
        }


           

    }
}