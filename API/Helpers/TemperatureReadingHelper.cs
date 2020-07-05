namespace API.Helpers
{
    public class TemperatureReadingHelper
    {
        public static string GetTemperatureString(double temperature)
        {
            var result = string.Empty;
            if (temperature >= 20)
            {
                result = "It's hot outside. Don't forget your sunglasses! ";
            }
            else if (temperature >= 19)
            {
                result = "It's not too hot nor too cold. ";
            }
            else if (temperature < 19)
            {
                result = "It is rather chilly outside. Wearing a coat is recommended. ";
            }
            result = result + ($"The temperature is {temperature:n2} °C.");
            return result;
        }
    }
}