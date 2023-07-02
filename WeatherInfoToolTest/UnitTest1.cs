namespace WeatherInfoToolTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task ChecksApiCall_Is_Working_Or_Not()
    {
        //Arrange
        decimal latitude = 18.9667M;
        decimal longitude = 72.8333M;

        var WeatherInfo = new WeatherInfo();

        //Act
        var OutputData = await WeatherInfo.FetchData(latitude, longitude);

        //Assert
        Assert.IsNotNull(OutputData);
        //StringAssert.Contains($"\"temperature\": {expectedTempertaure}", OutputData.ToString());
        //StringAssert.Contains($"\"windspeed\": {expectedWindSpeed}", OutputData.ToString());
        //"{\r\n  \"temperature\": 28.8,\r\n  \"windspeed\": 7.6,\r\n  \"winddirection\": 172.0,\r\n  \"weathercode\": 3,\r\n  \"is_day\": 0,\r\n  \"time\": \"2023-07-02T18:00\"\r\n}"
    }
}