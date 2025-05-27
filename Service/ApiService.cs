using Microsoft.EntityFrameworkCore;
using NewsApplication.Data;
using NewsApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsApplication.Service
{
    public class ApiService
    {

        private readonly AppDbContext _context;

        public ApiService(AppDbContext context)
        {
            _context = context;
        }

        public ApiService()
        {
        }

        //public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
        //{

        //    var register = new Register() 
        //    {

        //    }

        //}

        public async Task<Root> GetNews(string categoryName, string languageCode) 
        {
            //var httpClient = new HttpClient();
            //var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?category=general&apikey=2f8a4946c370cc36df5700cbdbabc4c9&lang=en&topic= +categoryName.ToLower()");
            //return JsonConvert.DeserializeObject<Root>(response);

            var httpClient = new HttpClient();
            string category = categoryName.ToLower();
            string lang = languageCode.ToLower();

            string url = $"https://gnews.io/api/v4/top-headlines?category={category}&lang={lang}&apikey=2f8a4946c370cc36df5700cbdbabc4c9";
            var response = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Root>(response);

        }

        public static async Task<bool> RegisterUser(AppDbContext context, string username, string email, string password)
        {
            if (await context.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                Fullname = username,
                Email = email,
                Password = HashPassword(password) // HashPassword must also be static
            };

            context.Users.Add(user);
            //await context.SaveChangesAsync();
            //return true;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                await Application.Current.MainPage.DisplayAlert("Database Error", detailedError, "OK");
                throw; // rethrow or handle as needed
            }
        }




        //private static string HashPassword(string password)
        //{
            
        //    return password; // Replace with real hash
        //}



        public static async Task<User> Login(AppDbContext context, string email, string password)
        {
            var hashedPassword = HashPassword(password);
            return await context.Users.FirstOrDefaultAsync(
                u => u.Email == email && u.Password == hashedPassword);
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        //public async Task<Root> GetNewsByCountry(string countryCode)
        //{
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?category=general&apikey=2f8a4946c370cc36df5700cbdbabc4c9&lang=en&topic= +countryCode.ToLower()");
        //    return JsonConvert.DeserializeObject<Root>(response);

        //}
    }
}
