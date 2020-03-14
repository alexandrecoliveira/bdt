using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite um ID .: ");
            int idPlayer = int.Parse(Console.ReadLine());

            CallWebAPIAsync(idPlayer).Wait();
            Console.WriteLine("FIM");
        }

        static async Task CallWebAPIAsync(int idPlayer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.balldontlie.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //Get Method                
                HttpResponseMessage response = await client.GetAsync("api/v1/players/" +  idPlayer);
                if (response.IsSuccessStatusCode)
                {
                    var p = await response.Content.ReadAsStringAsync();
                    var j = JsonConvert.DeserializeObject<object>(p);
                    Console.WriteLine(j);
                    
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
   
}
