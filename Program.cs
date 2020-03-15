using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;  //Necessário para usar o Deserialize

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
                    var player = await response.Content.ReadAsStringAsync();
                    var resultJson = JsonConvert.DeserializeObject<dynamic>(player);

                    var id = resultJson.id;
                    var firstName = resultJson.first_name;
                    var lastName = resultJson.last_name;
                    var position = resultJson.position;
                    var weightPounds = resultJson.weight_pounds;
                    var teamId = resultJson.team.id;
                    var teamAbbrev = resultJson.team.abbreviation;
                    var teamCity = resultJson.team.city;
                    var teamConf = resultJson.team.conference;
                    var teamDivision = resultJson.team.division;
                    var teamFullName = resultJson.team.full_name;
                    var teamName = resultJson.team.name;
                    
                }
                else
                {
                    Console.WriteLine("Erro - Sem sucesso na requisição com código " + response.StatusCode);
                }
            }
        }
    }
   
}
