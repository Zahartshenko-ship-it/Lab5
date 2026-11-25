using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    public class FleetService
    {
        public List<Car> Cars { get; }
        public List<Driver> Drivers { get; }
        public List<Trip> Trips { get; }

        public FleetService(
            List<Car> cars,
            List<Driver> drivers,
            List<Trip> trips)
        {
            Cars = cars;
            Drivers = drivers;
            Trips = trips;
        }

        public void PrintCars()
        {
            foreach (var car in Cars)
            {
                Console.WriteLine(car);
            }
        }

        public void PrintDrivers()
        {
            foreach (var driver in Drivers)
            {
                Console.WriteLine(driver);
            }
        }

        public void PrintTrips()
        {
            foreach (var trip in Trips)
            {
                Console.WriteLine(trip);
            }
        }

        public bool RemoveCarById(int id)
        {
            Car? target = Cars.FirstOrDefault(
                c => c.Id == id);
            if (target == null)
            {
                return false;
            }

            return Cars.Remove(target);
        }

        public bool RemoveDriverById(int id)
        {
            Driver? target = Drivers.FirstOrDefault(
                d => d.Id == id);
            if (target == null)
            {
                return false;
            }

            return Drivers.Remove(target);
        }

        public bool RemoveTripById(int id)
        {
            Trip? target = Trips.FirstOrDefault(
                t => t.TripId == id);
            if (target == null)
            {
                return false;
            }

            return Trips.Remove(target);
        }

        public bool AddCar(Car car)
        {
            if (Cars.Any(c => c.Id == car.Id))
            {
                return false;
            }

            Cars.Add(car);
            return true;
        }

        public bool AddDriver(Driver driver)
        {
            if (Drivers.Any(d => d.Id == driver.Id))
            {
                return false;
            }

            Drivers.Add(driver);
            return true;
        }

        public bool AddTrip(Trip trip)
        {
            bool idExists = Trips.Any(
                t => t.TripId == trip.TripId);
            bool carExists = Cars.Any(
                c => c.Id == trip.CarId);
            bool driverExists = Drivers.Any(
                d => d.Id == trip.DriverId);

            if (idExists || !carExists || !driverExists)
            {
                return false;
            }

            Trips.Add(trip);
            return true;
        }

        public List<Car> GetCarsFromYear(int minYear)
        {
            var query =
                from car in Cars
                where car.Year >= minYear
                orderby car.Brand, car.Model
                select car;

            return query.ToList();
        }

        public int GetTotalDistanceByBrand(string brand)
        {
            var query =
                from trip in Trips
                join car in Cars on trip.CarId equals car.Id
                where car.Brand.Equals(
                    brand,
                    StringComparison.OrdinalIgnoreCase)
                select trip.Distance;

            return query.Sum();
        }

        public List<string> GetTripsByExpAndYear(
            int minExp,
            int minYear)
        {
            var query =
                from trip in Trips
                join driver in Drivers
                    on trip.DriverId equals driver.Id
                join car in Cars
                    on trip.CarId equals car.Id
                where driver.Exp >= minExp && car.Year >= minYear
                select $"Рейс #{trip.TripId}: {driver.Name}, " +
                    $"{car.Brand} {car.Model}, {trip.Distance} км";

            return query.ToList();
        }

        public int CountTripsByDriverAgeAndBrand(
            int minAge,
            string brand)
        {
            var query =
                from trip in Trips
                join driver in Drivers
                    on trip.DriverId equals driver.Id
                join car in Cars
                    on trip.CarId equals car.Id
                where driver.Age >= minAge
                    && car.Brand.Equals(
                        brand,
                        StringComparison.OrdinalIgnoreCase)
                select trip.TripId;

            return query.Count();
        }

        public int GetNextCarId()
        {
            if (Cars.Count == 0)
            {
                return 1;
            }

            return Cars.Max(c => c.Id) + 1;
        }
    }
}