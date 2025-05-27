using NewsApplication.Data;
using NewsApplication.Service;

namespace NewsApplication.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }


    async void BtnLogin_Clicked(System.Object sender, System.EventArgs e)
    {
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        var email = EntEmail.Text;
        //var phone = EntPhone.Text;
        var password = EntPassword.Text;

        try 
        {
            using (var context = new AppDbContext())
            {
                var response = await ApiService.Login(context, email, password);

                if (response != null)
                {

                    await DisplayAlert("Success", "User logged in successfully!", "Alright");

                    Application.Current.MainPage = new NavigationPage(new NewsPage());


                }

                else
                {
                    await DisplayAlert("", "Oops something went wrong.", "Cancel");
                }
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
        finally
        {
            // Stop loading
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }



    }

    async void TapJoinNow_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }

}

