using EvernoteClone.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel.Helper
{
    public class FirebaseAuthHelper
    {
        private static string AuthKey = "AIzaSyCCuX5RogxHuMMk71vYll1X6K-zwadgIX4";

        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        public static async Task<bool> Register(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, encoding: Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={AuthKey}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();

                    var error = JsonConvert.DeserializeObject<Error>(errorJson);

                    MessageBox.Show(error.error.message);

                    return false;
                }


            }
        }

        public static async Task<bool> Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, encoding: Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={AuthKey}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();

                    var error = JsonConvert.DeserializeObject<Error>(errorJson);

                    MessageBox.Show(error.error.message);

                    return false;
                }


            }
        }

    }
}

