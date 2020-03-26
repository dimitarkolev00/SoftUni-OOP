namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var vehicle = new Vehicle(150, 50);
            System.Console.WriteLine(vehicle.FuelConsumption);
        }
    }
}
