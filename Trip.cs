namespace Lab5
{
    public class Trip
    {
        private int _tripId;
        private int _carId;
        private int _driverId;
        private string _startDate;
        private string _endDate;
        private int _distance;
        private string _price;

        public Trip() {}

        public Trip(
            int tripId, 
            int carId, 
            int driverId, 
            string startDate, 
            string endDate, 
            int distance, 
            string price)
        {
            TripId = tripId;
            CarId = carId;
            DriverId = driverId;
            StartDate = startDate;
            EndDate = endDate;
            Distance = distance;
            Price = price;
        }

        public int TripId
        {
            get{return _tripId;}
            set{_tripId = value;}
        }

        public int CarId
        {
            get{return _carId;}
            set{_carId = value;}
        }

        public int DriverId
        {
            get{return _driverId;}
            set{_driverId = value;}
        }

        public string StartDate
        {
            get { return _startDate; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Дата начала рейса не может быть пустой"
                    );
                }
                _startDate = value.Trim();
            }
        }

        public string EndDate
        {
            get { return _endDate; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Дата конца рейса не может быть пустой"
                    );
                }
                _endDate = value.Trim();
            }
        }

        public int Distance
        {
            get { return _distance; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Distance),
                        "Дистанция не может быть отрицательной"
                    );
                }
                _distance = value;
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Стоимость рейса не может быть пустой"
                    );
                }
                _price = value.Trim();
            }
        }

        public override string ToString()
        {
            return $"Рейс #{TripId} | Авто #{CarId}, " +
                   $"Водитель #{DriverId} | " +
                   $"{StartDate} - {EndDate} | " +
                   $"{Distance} км | {Price}";
        }
    }
}