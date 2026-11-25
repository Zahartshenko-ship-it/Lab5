using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Lab5
{
    public class ExcelReader
    {
        public IWorkbook OpenWorkbook(string path)
        {
            using var fs = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read);

            return new HSSFWorkbook(fs);
        }

        public List<Car> ReadCars(IWorkbook workbook)
        {
            var cars = new List<Car>();
            ISheet? sheet = workbook.GetSheetAt(0);

            if (sheet == null)
            {
                return cars;
            }

            for (int rowIdx = 1;
                 rowIdx <= sheet.LastRowNum;
                 rowIdx++)
            {
                IRow? row = sheet.GetRow(rowIdx);
                if (row == null)
                {
                    continue;
                }
                try
                {
                    int id = ReadInt(row, 0);
                    string brand = ReadText(row, 1);
                    string model = ReadText(row, 2);
                    int year = ReadInt(row, 3);

                    cars.Add(new Car(id, brand, model, year));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Строка {rowIdx + 1}: {ex.Message}");
                }
            }

            return cars;
        }

        public List<Driver> ReadDrivers(IWorkbook workbook)
        {
            var drivers = new List<Driver>();
            ISheet? sheet = workbook.GetSheetAt(1);

            if (sheet == null)
            {
                return drivers;
            }

            for (int rowIdx = 1;
                 rowIdx <= sheet.LastRowNum;
                 rowIdx++)
            {
                IRow? row = sheet.GetRow(rowIdx);
                if (row == null)
                {
                    continue;
                }
                try
                {
                    int id = ReadInt(row, 0);
                    string name = ReadText(row, 1);
                    int age = ReadInt(row, 2);
                    int exp = ReadInt(row, 3);

                    drivers.Add(new Driver(id, name, age, exp));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Строка {rowIdx + 1}: {ex.Message}");
                }
            }

            return drivers;

        }

        public List<Trip> ReadTrips(IWorkbook workbook)
        {
            var trips = new List<Trip>();
            ISheet? sheet = workbook.GetSheetAt(2);

            if (sheet == null)
            {
                return trips;
            }

            for (int rowIdx = 1;
                 rowIdx <= sheet.LastRowNum;
                 rowIdx++)
            {
                IRow? row = sheet.GetRow(rowIdx);
                if (row == null)
                {
                    continue;
                }
                try
                {
                    int tripId = ReadInt(row, 0);
                    int carId = ReadInt(row, 1);
                    int driverId = ReadInt(row, 2);
                    string startDate = ReadText(row, 3);
                    string endDate = ReadText(row, 4);
                    int distance = ReadInt(row, 5);
                    string price = ReadText(row, 6);

                    trips.Add(new Trip(
                        tripId, 
                        carId, 
                        driverId, 
                        startDate, 
                        endDate, 
                        distance, 
                        price));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Строка {rowIdx + 1}: {ex.Message}");
                }
            }

            return trips;
        }

        private int ReadInt(IRow row, int col)
        {
            ICell? cell = row.GetCell(col);
            if (cell == null)
            {
                throw new FormatException($"Пустая ячейка в {col}");
            }

            if (cell.CellType == CellType.Numeric)
            {
                return Convert.ToInt32(cell.NumericCellValue);
            }

            string text = cell.ToString()?.Trim() ?? string.Empty;
            return int.Parse(text);
        }

        private string ReadText(IRow row, int col)
        {
            ICell? cell = row.GetCell(col);
            if (cell == null)
            {
                return string.Empty;
            }

            string? text = cell.ToString();
            if (text == null)
            {
                return string.Empty;
            }

            return text.Trim();
        }
    }
}