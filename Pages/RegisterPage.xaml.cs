using NewsApplication.Data;
using NewsApplication.Service;

namespace NewsApplication.Pages;

public partial class RegisterPage : ContentPage
{
    
	public RegisterPage()
	{
		InitializeComponent();
        
	}

    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        var username = EntFullName.Text;
        var email = EntEmail.Text;
        //var phone = EntPhone.Text;
        var password = EntPassword.Text;


        try 
        {
            using (var context = new AppDbContext())
            {
                var response = await ApiService.RegisterUser(context, username, email, password);

                if (response)
                {
                    await DisplayAlert("Success", "User registered successfully!", "Alright");
                    // Navigate to LoginPage or HomePage after registration
                    //await Navigation.PushAsync(new LoginPage());
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Error", "Email already exists.", "Cancel");
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

    async void TapLogin_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e) 
    {

        await Navigation.PushModalAsync(new LoginPage());

    }


}