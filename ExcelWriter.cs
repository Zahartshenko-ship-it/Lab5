using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Lab5
{
    public class ExcelWriter
    {
        public void SaveWorkbook(
            string path,
            List<Car> cars,
            List<Driver> drivers,
            List<Trip> trips)
        {
            var workbook = new HSSFWorkbook();

            WriteCars(workbook, cars);
            WriteDrivers(workbook, drivers);
            WriteTrips(workbook, trips);

            using var fs = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write);

            workbook.Write(fs);
        }

        private void WriteCars(IWorkbook wb, List<Car> cars)
        {
            ISheet sheet = wb.CreateSheet("Автомобили");
            IRow header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("Id");
            header.CreateCell(1).SetCellValue("Brand");
            header.CreateCell(2).SetCellValue("Model");
            header.CreateCell(3).SetCellValue("Year");

            for (int i = 0; i < cars.Count; i++)
            {
                Car c = cars[i];
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(c.Id);
                row.CreateCell(1).SetCellValue(c.Brand);
                row.CreateCell(2).SetCellValue(c.Model);
                row.CreateCell(3).SetCellValue(c.Year);
            }
        }

        private void WriteDrivers(
            IWorkbook wb,
            List<Driver> drivers)
        {
            ISheet sheet = wb.CreateSheet("Водители");
            IRow header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("Id");
            header.CreateCell(1).SetCellValue("Name");
            header.CreateCell(2).SetCellValue("Age");
            header.CreateCell(3).SetCellValue("Exp");

            for (int i = 0; i < drivers.Count; i++)
            {
                Driver d = drivers[i];
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(d.Id);
                row.CreateCell(1).SetCellValue(d.Name);
                row.CreateCell(2).SetCellValue(d.Age);
                row.CreateCell(3).SetCellValue(d.Exp);
            }
        }

        private void WriteTrips(IWorkbook wb, List<Trip> trips)
        {
            ISheet sheet = wb.CreateSheet("Рейсы");
            IRow header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("TripId");
            header.CreateCell(1).SetCellValue("CarId");
            header.CreateCell(2).SetCellValue("DriverId");
            header.CreateCell(3).SetCellValue("StartDate");
            header.CreateCell(4).SetCellValue("EndDate");
            header.CreateCell(5).SetCellValue("Distance");
            header.CreateCell(6).SetCellValue("Price");

            for (int i = 0; i < trips.Count; i++)
            {
                Trip t = trips[i];
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(t.TripId);
                row.CreateCell(1).SetCellValue(t.CarId);
                row.CreateCell(2).SetCellValue(t.DriverId);
                row.CreateCell(3).SetCellValue(t.StartDate);
                row.CreateCell(4).SetCellValue(t.EndDate);
                row.CreateCell(5).SetCellValue(t.Distance);
                row.CreateCell(6).SetCellValue(t.Price);
            }
        }
    }
}