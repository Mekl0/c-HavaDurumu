using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherData
{
    public string Temperature { get; set; }
    public ForecastData[] Forecast { get; set; }
}

public class ForecastData
{
    public string Date { get; set; }
    public string Temperature { get; set; }
    // Diğer özellikleri ekleyin
}

class Program
{
    static async Task Main()
    {
        await GetWeatherData("İstanbul", "https://goweather.herokuapp.com/weather/istanbul");
        await GetWeatherData("İzmir", "https://goweather.herokuapp.com/weather/izmir");
        await GetWeatherData("Ankara", "https://goweather.herokuapp.com/weather/ankara");

        Console.ReadLine();
    }

    static async Task GetWeatherData(string city, string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string jsonData = await client.GetStringAsync(apiUrl);

                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonData);

                Console.WriteLine($"{city} Hava Durumu:");
                Console.WriteLine($"Sıcaklık: {weatherData.Temperature}");

                if (weatherData.Forecast != null && weatherData.Forecast.Length > 0)
                {
                    Console.WriteLine("Tahminler:");
                    foreach (var forecast in weatherData.Forecast)
                    {
                        Console.WriteLine($"- {forecast.Date}: {forecast.Temperature}");
                        // Diğer tahmin bilgilerini ekleyin
                    }
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata alındı: {ex.Message}");
            }
        }
    }
}
