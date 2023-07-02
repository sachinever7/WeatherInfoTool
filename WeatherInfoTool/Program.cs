using Newtonsoft.Json;

public class WeatherInfo {
    public static async Task Main(string[] args) {

        decimal latitude = 0.0M;
        decimal longitude = 0.0M;

        // Read the JSON file
        string jsonFilePath = "data.json";
        string jsonString = File.ReadAllText(jsonFilePath);
        dynamic citylist = JsonConvert.DeserializeObject(jsonString);

        //Read user input
        Console.WriteLine("Enter the city name: ");
        string city = Console.ReadLine();

        //find the city latitude and longitude from the list
        foreach (dynamic var in citylist)
        {
            if (var.city.ToString().ToLower().Contains(city.ToLower()))
            {
                latitude = var.lat;
                longitude = var.lng;
                break;
            }
        }

        if (latitude == 0.0M && longitude == 0.0M)
        {
            Console.WriteLine("Invalid City.");
            return;
        }
        string result= await FetchData(latitude,longitude);
    }
    public static async Task<String> FetchData(decimal latitude,decimal longitude)
    {
        try
        {
            // api call
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            string content = "";
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Failed To fetch data. Status Code: {response.StatusCode}";
            }

            // parse the json
            dynamic weatherData = JsonConvert.DeserializeObject(content);

            // getting the values from json
            decimal temperature = weatherData.current_weather.temperature;
            decimal windSpeed = weatherData.current_weather.windspeed;
            DateTime time = weatherData.current_weather.time;

            Console.WriteLine($"Temperature: {temperature}°C");
            Console.WriteLine($"WindSpeed: {windSpeed} m/s");
            Console.WriteLine($"Time: {time}");
            return weatherData.current_weather.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed To fetch data: {ex.Message}");
            return $"Failed To fetch data. {ex.Message}";
        }
    }
}
