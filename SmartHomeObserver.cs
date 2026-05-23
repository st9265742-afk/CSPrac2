using System;

namespace SmartHomeObserver
{
    class TemperatureSensor
    {
        private int temperature;

        public event Action<int> TemperatureChanged;

        public void SetTemperature(int newTemperature)
        {
            temperature = newTemperature;

            Console.WriteLine($"\nТемпература змінена: {temperature}°C");

            TemperatureChanged?.Invoke(temperature);
        }
    }

    class Display
    {
        public void ShowTemperature(int temperature)
        {
            Console.WriteLine($"[Display] Поточна температура: {temperature}°C");
        }
    }

    class AirConditioner
    {
        public void ControlTemperature(int temperature)
        {
            if (temperature < 17)
            {
                Console.WriteLine("[AirConditioner] Увімкнено обігрів");
            }
            else if (temperature > 25)
            {
                Console.WriteLine("[AirConditioner] Увімкнено охолодження");
            }
            else
            {
                Console.WriteLine("[AirConditioner] Кондиціонер вимкнений");
            }
        }
    }

    class SecuritySystem
    {
        public void CheckTemperature(int temperature)
        {
            if (temperature > 40)
            {
                Console.WriteLine("[SecuritySystem] УВАГА! Перегрів системи!");
            }

            if (temperature < 5)
            {
                Console.WriteLine("[SecuritySystem] Попередження! Ризик замерзання систем!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TemperatureSensor sensor = new TemperatureSensor();

            Display display = new Display();
            AirConditioner airConditioner = new AirConditioner();
            SecuritySystem securitySystem = new SecuritySystem();

            sensor.TemperatureChanged += display.ShowTemperature;
            sensor.TemperatureChanged += airConditioner.ControlTemperature;
            sensor.TemperatureChanged += securitySystem.CheckTemperature;

            sensor.SetTemperature(10);
            sensor.SetTemperature(20);
            sensor.SetTemperature(30);
            sensor.SetTemperature(45);
            sensor.SetTemperature(2);
        }
    }
}