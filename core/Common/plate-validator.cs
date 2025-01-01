namespace OrdersMicroservice.core.Common
{
    public class PlateValidator
    {
        public static bool IsValid(string plate)
        {
            return (!System.Text.RegularExpressions.Regex.IsMatch(plate, @"^[A-Z]{2}[0-9]{3}[A-Z]{2}$"));
        }
    }
}