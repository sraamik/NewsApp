using NewsApplication.Models;
using NewsApplication.Service;
using System.Threading.Tasks;

namespace NewsApplication.Pages;

public partial class NewsPage : ContentPage
{
    public NewsPage()
    {
        InitializeComponent();
        ArticlesList = new List<Article>();
        FilteredCountryList = CountryList;
        CvCategories.ItemsSource = CategoryList;
        CvCountries.ItemsSource = CountryList;
    }

    private bool IsNextPage = false;
    //private string SelectedCountryCode = "us";
    //private string SelectedCountryCode = "us";
    public List<Article> ArticlesList { get; set; }
    public List<Category> CategoryList = new List<Category>()
    {
        new Category() { Name = "Breaking-news"},
        new Category() { Name = "World" },
        new Category() { Name = "Nation" },
        new Category() { Name = "Business" },
        new Category() { Name = "Technology" },
        new Category() { Name = "Entertainment" },
        new Category() { Name = "Sports" },
        new Category() { Name = "Science" },
        new Category() { Name = "Health" },

    };

    public List<Country> CountryList = new List<Country>()
{
    new Country() { Name = "Italy", Code = "it", Language = "it"},
    new Country() { Name = "France", Code = "fr" , Language = "fr"},
    new Country() { Name = "India", Code = "in" , Language = "te"},
    new Country() { Name = "Spain", Code = "es" , Language = "es"},
    new Country() { Name = "China", Code = "cn" , Language = "zh"},
    new Country() { Name = "United States", Code = "us", Language = "en" },
    new Country() { Name = "Germany", Code = "de" , Language = "de"},
    new Country() { Name = "Egypt", Code = "eg" , Language = "ar"},
    new Country() { Name = "Australia", Code = "au" , Language = "en"},
    new Country() { Name = "Brazil", Code = "br" , Language = "pt"},
    new Country() { Name = "Canada", Code = "ca" , Language = "en"},
    new Country() { Name = "Greece", Code = "gr" , Language = "el"},
    new Country() { Name = "Hong Kong", Code = "hk" , Language = "zh"},
    new Country() { Name = "Ireland", Code = "ie" , Language = "en"},
    new Country() { Name = "Israel", Code = "il" , Language = "he"},
    new Country() { Name = "Japan", Code = "jp" , Language = "ja"},
    new Country() { Name = "Netherlands", Code = "nl" , Language = "nl"},
    new Country() { Name = "Norway", Code = "no" , Language = "no"},
    new Country() { Name = "Peru", Code = "pe" , Language = "es"},
    new Country() { Name = "Pakistan", Code = "pk" , Language = "hi"},
    new Country() { Name = "Philippines", Code = "ph" , Language = "zh"},
    new Country() { Name = "Portugal", Code = "pt" , Language = "pt"},
    new Country() { Name = "Romania", Code = "ro" , Language = "ro"},
    new Country() { Name = "Russian Federation", Code = "ru" , Language = "ru"},
    new Country() { Name = "Singapore", Code = "sg" , Language = "ta"},
    new Country() { Name = "Sweden", Code = "se" , Language = "es"},
    new Country() { Name = "Switzerland", Code = "Taiwan" , Language = "zh"},
    new Country() { Name = "Taiwan", Code = "br" , Language = "pt"},
    new Country() { Name = "Ukraine", Code = "ua" , Language = "ru"},
    new Country() { Name = "United Kingdom", Code = "gb" , Language = "en"},
    
    


};

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    PassCategory("breaking-news");


    //}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //if (!IsNextPage == false)
        if (!IsNextPage)
         

        {
            //CountryPicker.SelectedIndex = 1;
            await PassCategory("Technology");
            

        }
       


    }

    public Country SelectedCountry { get; set; } = new Country { Code = "us", Language = "en" };
    public async Task PassCategory(string categoryName) 
    {
        CvNews.ItemsSource = null;
        ArticlesList.Clear();
        ApiService apiService = new ApiService();
        
        var newsRequest = await apiService.GetNews(categoryName, SelectedCountry.Language);
        foreach (var item in newsRequest.Articles)
        {

            ArticlesList.Add(item);
        }

        FilteredArticlesList = new List<Article>(ArticlesList);
        CvNews.ItemsSource = ArticlesList;

    }

    //public async Task PassCategory(string countryCode)
    //{
    //    CvNews.ItemsSource = null;
    //    ArticlesList.Clear();
    //    ApiService apiService = new ApiService();

    //    var newsRequest = await apiService.GetNews(countryCode);
    //    foreach (var item in newsRequest.Articles)
    //    {

    //        ArticlesList.Add(item);
    //    }
    //    CvNews.ItemsSource = ArticlesList;

    //}

    private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Category;
        if (selectedItem != null)
        {
            await PassCategory(selectedItem.Name);
            CvCategories.SelectedItem = null;
        }
    }

    //private async void OnHamburgerMenuClicked(object sender, EventArgs e)
    //{
    //    var categoryNames = CategoryList.Select(c => c.Name).ToArray();
    //    var selected = await DisplayActionSheet("Select Category", "Cancel", null, categoryNames);

    //    if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
    //    {
    //        await PassCategory(selected);
    //    }
    //}





    private async void CvCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Country;
        if (selectedItem != null)
        {
            SelectedCountry = selectedItem;

            var selectedCategory = (CvCategories.SelectedItem as Category)?.Name ?? "Technology";
            await PassCategory(selectedCategory);
        }
    }


    //private List<Country> FilteredCountryList;

    //private void OnCountrySearchTextChanged(object sender, TextChangedEventArgs e)
    //{
    //    var searchText = e.NewTextValue.ToLower();

    //    if (string.IsNullOrWhiteSpace(searchText))
    //    {
    //        FilteredCountryList = CountryList;
    //    }
    //    else
    //    {
    //        FilteredCountryList = CountryList
    //            .Where(c => c.Name.ToLower().Contains(searchText))
    //            .ToList();
    //    }

    //    CvCountries.ItemsSource = FilteredCountryList;
    //}


    //private List<Article> FilteredArticlesList = new List<Article>();
    //private Article article;

    //private void OnArticleSearchTextChanged(object sender, TextChangedEventArgs e)
    //{
    //    var searchText = e.NewTextValue?.ToLower();

    //    if (string.IsNullOrWhiteSpace(searchText))
    //    {
    //        FilteredArticlesList = ArticlesList;
    //    }
    //    else
    //    {
    //        FilteredArticlesList = ArticlesList
    //            .Where(a =>
    //                (a.Title?.ToLower().Contains(searchText) ?? false) ||
    //                (a.Description?.ToLower().Contains(searchText) ?? false))
    //            .ToList();
    //    }

    //    CvNews.ItemsSource = FilteredArticlesList;
    //}

    private List<Country> FilteredCountryList = new List<Country>();
    private List<Article> FilteredArticlesList = new List<Article>();

    private void OnUnifiedSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower();

        // Filter countries
        if (string.IsNullOrWhiteSpace(searchText))
        {
            FilteredCountryList = CountryList;
        }
        else
        {
            FilteredCountryList = CountryList
                .Where(c => c.Name.ToLower().Contains(searchText))
                .ToList();
        }
        CvCountries.ItemsSource = FilteredCountryList;

        // Filter articles
        if (string.IsNullOrWhiteSpace(searchText))
        {
            FilteredArticlesList = ArticlesList;
        }
        else
        {
            FilteredArticlesList = ArticlesList
                .Where(a =>
                    (a.Title?.ToLower().Contains(searchText) ?? false) ||
                    (a.Description?.ToLower().Contains(searchText) ?? false))
                .ToList();
        }
        CvNews.ItemsSource = FilteredArticlesList;
    }



    private async void CvNews_SelectedChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Article;
        if (selectedItem != null)
        {
            IsNextPage = true;
            //await Navigation.PushAsync(new NewsDetailsPage(selectedItem));

            await Navigation.PushAsync(new NewsDetailsPage(selectedItem));

            //Application.Current.MainPage = new NewsDetailsPage(selectedItem);
            CvNews.SelectedItem = null;
        }
        //IsNextPage = true;
        //await Navigation.PushAsync(new NewsDetailsPage(selectedItem));
    }


    




    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }




    //private async void CountryPicker_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var picker = sender as Picker;
    //    SelectedCountryCode = picker.SelectedItem?.ToString() ?? "us";
    //    var selectedCategory = (CvCategories.SelectedItem as Category)?.Name ?? "breaking-news";
    //    await PassCategory(selectedCategory);
    //}


}