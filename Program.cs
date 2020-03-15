using System;
using System.IO;
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
            int cont = 0;
            do
            {
                Console.Write("Digite um ID .: ");
                int idPlayer = int.Parse(Console.ReadLine());
                CallWebAPIAsync(idPlayer).Wait();
                Console.Write("Continuar (S/N): ");
                cont = Console.ReadLine() == "S" ? 1 : 0;
            } while (cont == 1);
            
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
                    writeJsonToCsv(resultJson);                   
                }
                else
                {
                    Console.WriteLine("Erro - Sem sucesso na requisição com código " + response.StatusCode);
                }
            }
        }

        private static void writeJsonToCsv(dynamic resultJson)
        {
            string fileName = @"C:\Users\alexa\Documents\nba3.csv";
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

            try
            {
                //Teste usado para definir se é necessário escrever o cabeçalho ou não
                if (!File.Exists(fileName))
                {                    
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.WriteLine("Id;FIRST NAME;LAST NAME;POSITION;WEIGHT POUNDS;TEAM ID;TEAM ABBREVIATION;TEAM CITY;TEAM CONF;TEAM DIVISION;TEAM FULL NAME;TEAM NAME;");
                    }
                }

                using (StreamWriter sw = File.AppendText(fileName))
                {                    
                    sw.WriteLine(id + ";" + firstName + ";" + lastName + ";" + position + ";" + weightPounds + ";" + teamId + ";" + teamAbbrev + ";" + teamCity + ";" + teamConf + ";" + teamDivision + ";" + teamFullName + ";" + teamName + ";");
                }
            }    
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
   
}
