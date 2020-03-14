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
                    Player p = await response.Content.ReadAsAsync<Player>();

                    Console.WriteLine("ID: {0}\nFirst Name: {1}\nLast Name: {2}\nHeight Ft.: {3}\nHeight Inc.: {4}\nPosition: {5}", 
                                        p.iD, p.first_name, p.last_name, p.height_feet, p.height_inches, p.position);
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }

    public class Player
    {
        
        public int iD { get; set; }
        public String first_name { get; set; }        
        public String last_name { get; set; }
        public String height_feet { get; set; }
        public String height_inches { get; set; }
        public String position { get; set; }
      
    }

   
}
