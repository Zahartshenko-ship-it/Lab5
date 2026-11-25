namespace Lab5
{
    public class Car
    {
        private int _id;
        private string _brand;
        private string _model;
        private int _year;
        public Car(int id, string brand, string model, int year)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
        }
        
        public Car() {}

        public int Id
        {
            get{return _id;}
            set{_id = value;}
        }

        public string Brand
        {
            get{return _brand;}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Марка" +
                    " автомобиля не может быть пустой");
                }
                else
                {
                    _brand = value.Trim();
                }
            }
        }

        public string Model
        {
            get{return _model;}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Модель" +
                    " автомобиля не может быть пустой");
                }
                else
                {
                    _model = value.Trim();
                }
            }
        }

        public int Year
        {
            get{return _year;}
            set
            {
                if (value < 1886 || value > 2026)
                {
                    throw new ArgumentException("Ошибка: " + 
                    "год машины введен не корректно");
                }
                else
                {
                    _year = value;
                }
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Brand} {Model} ({Year})";
        }
    }
}