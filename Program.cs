using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace Lab5
{
    internal class Program
    {
        private const string FilePath = "LR5-var4.xls";

        private static void Main()
        {
            var reader = new ExcelReader();
            IWorkbook workbook = reader.OpenWorkbook(FilePath);

            List<Car> cars = reader.ReadCars(workbook);
            List<Driver> drivers = reader.ReadDrivers(workbook);
            List<Trip> trips = reader.ReadTrips(workbook);

            var service = new FleetService(cars, drivers, trips);
            var writer = new ExcelWriter();

            bool running = true;
            while (running)
            {
                PrintMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowAll(service);
                        break;
                    case "2":
                        AddCarMenu(service);
                        break;
                    case "3":
                        RemoveCarMenu(service);
                        break;
                    case "4":
                        RunQueries(service);
                        break;
                    case "5":
                        writer.SaveWorkbook(
                            FilePath,
                            service.Cars,
                            service.Drivers,
                            service.Trips);
                        Console.WriteLine("Сохранено.");
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный пункт.");
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n--- АВТОПАРК ---");
            Console.WriteLine("1. Просмотр базы");
            Console.WriteLine("2. Добавить авто");
            Console.WriteLine("3. Удалить авто по ID");
            Console.WriteLine("4. LINQ-запросы");
            Console.WriteLine("5. Сохранить в Excel");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
        }

        private static void ShowAll(FleetService service)
        {
            Console.WriteLine("\nАвтомобили:");
            service.PrintCars();
            Console.WriteLine("\nВодители:");
            service.PrintDrivers();
            Console.WriteLine("\nРейсы:");
            service.PrintTrips();
        }

        private static void AddCarMenu(FleetService service)
        {
            try
            {
                Console.Write("Марка: ");
                string brand = Console.ReadLine() ?? "";
                Console.Write("Модель: ");
                string model = Console.ReadLine() ?? "";
                Console.Write("Год: ");
                int year = int.Parse(Console.ReadLine() ?? "");

                int id = service.GetNextCarId();
                var car = new Car(id, brand, model, year);
                bool added = service.AddCar(car);
                Console.WriteLine(added
                    ? "Авто добавлено."
                    : "Такой ID уже есть.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void RemoveCarMenu(FleetService service)
        {
            if (service.Cars.Count == 0)
            {
                Console.WriteLine("Список авто пуст.");
                return;
            }

            int minId = service.Cars.Min(c => c.Id);
            int maxId = service.Cars.Max(c => c.Id);

            Console.WriteLine(
                $"Доступные ID: от {minId} "+
                $"до {service.Cars.Count}");

            Console.Write("ID авто для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool ok = service.RemoveCarById(id);
                Console.WriteLine(ok ? "Удалено." : "Не найдено.");
            }
        }

        private static void RunQueries(FleetService service)
        {
            int minYear = ReadIntWithDefault(
                "\n1) Минимальный год авто (по умолч. 2022): ",
                2022);
            Console.WriteLine($"Авто с {minYear} года:");
            foreach (var c in service.GetCarsFromYear(minYear))
            {
                Console.WriteLine(c);
            }

            string brandDist = ReadStringWithDefault(
                "\n2) Марка для суммарного пробега " +
                "(по умолч. Opel): ",
                "Opel");
            int dist = service.GetTotalDistanceByBrand(brandDist);
            Console.WriteLine($"Пробег {brandDist}: {dist} км");

            int minExp = ReadIntWithDefault(
                "\n3) Мин. стаж водителя (по умолч. 8): ",
                8);
            int minTripCarYear = ReadIntWithDefault(
                "Мин. год выпуска авто (по умолч. 2020): ",
                2020);
            Console.WriteLine(
                $"Рейсы (стаж >= {minExp}, " +
                $"год >= {minTripCarYear}):");
            foreach (var line in service.GetTripsByExpAndYear(
                minExp,
                minTripCarYear))
            {
                Console.WriteLine(line);
            }

            int minAge = ReadIntWithDefault(
                "\n4) Мин. возраст водителя (по умолч. 45): ",
                45);
            string brandCnt = ReadStringWithDefault(
                "Марка авто (по умолч. Opel): ",
                "Opel");
            int cnt = service.CountTripsByDriverAgeAndBrand(
                minAge,
                brandCnt);
            Console.WriteLine(
                $"Рейсов (возраст >= {minAge}, {brandCnt}): {cnt}");
        }

        private static int ReadIntWithDefault(
            string prompt,
            int defaultValue)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                return defaultValue;
            }

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine(
                $"Некорректное число, взято {defaultValue}.");
            return defaultValue;
        }

        private static string ReadStringWithDefault(
            string prompt,
            string defaultValue)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }
    }
}
