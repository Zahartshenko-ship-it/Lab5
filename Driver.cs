namespace Lab5
{
    public class Driver
    {
        private int _id;
        private string _name;
        private int _age;
        private int _exp;

        public Driver() {}

        public Driver(int id, string name, int age, int exp)
        {
            Id = id;
            Name = name;
            Age = age;
            Exp = exp;
        }

        public int Id
        {
            get{return _id;}
            set{_id = value;}
        }

        public string Name
        {
            get{return _name;}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя" +
                    " не может быть пустым");
                }
                _name = value.Trim();
            }
        }

        public int Age
        {
            get{return _age;}
            set
            {
                if (value < 18 || value > 120)
                {
                    throw new ArgumentException("Ошибка: " + 
                    "возраст водителя введен не корректно");
                }
                _age = value;
            }
        }

        public int Exp
        {
            get{return _exp;}
            set
            {
                int maxExperience = Age - 18;
                if (value < 0 || value > maxExperience)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Exp),
                        $"Стаж должен быть от 0 "+
                        $"до {maxExperience} лет "+
                        $"при возрасте {Age}"
                    );
                }
                _exp = value;
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} | "+
            $"Возраст: {Age} | Стаж: {Exp} лет";
        }
    }
}