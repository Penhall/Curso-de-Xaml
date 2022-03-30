using EvernoteClone.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helper
{
    public class FirebaseDataHelper
    {

        private static string dbPath = "https://notebookcloneapp-default-rtdb.firebaseio.com/";

        public static async Task<bool> Insert<T>(T item)
        {

            string jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync($"{dbPath}{item.GetType().Name.ToLower()}.json", content);

                if (result.IsSuccessStatusCode) { return true; } else { return false; }
            }

        }
        public static async Task<bool> Delete<T>(T item) where T : IHasId
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json");

                if (result.IsSuccessStatusCode) { return true; } else { return false; }
            }

        }
        public static async Task<bool> Update<T>(T item) where T : IHasId
        {

            string jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PatchAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json", content);

                if (result.IsSuccessStatusCode) { return true; } else { return false; }
            }
        }
        public static async Task<List<T>> Read<T>() where T : IHasId
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");

                string? jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    Dictionary<string, T>? objects = new Dictionary<string, T>();

                    objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                    List<T> list = new List<T>();

                    foreach (var o in objects)
                    {
                        o.Value.Id = o.Key;
                        list.Add(o.Value);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }





        }


    }
}
