namespace ParkingAPI.models
{
    public class carSetting: IcarSetting
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    
    public interface IcarSetting
    {
        string Server { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
}