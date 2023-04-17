using System.Text.RegularExpressions;

namespace Практическая3
{
    struct PersonFromFile
    {
        public string Name;
        public string Surname;
        public string Telephon;
        public int Age;
        public string Pol;
        public PersonFromFile(string[] Line)
        {
            Name = Line[1];
            Surname = Line[0];
            Telephon = Line[2];
            Age = Convert.ToInt32(Line[3]);
            Pol = Line[4];
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //OutputInConsole();
            //InputInFile();
            //OutputInConsole();
            PersonWithMaxAge(InputDataInList());
            AllWoman(InputDataInList());
            FirstManWithNameBeginningA(InputDataInList());
            HumansWithFamily6len(InputDataInList());
            FirstHumanWithTelEndOn9(InputDataInList());
            OrderBY(InputDataInList());

        }
        public static void OutputInConsole()
        {
            using (StreamReader file = new StreamReader("Data.csv"))
            {
                while(!file.EndOfStream)
                {
                    PersonFromFile line = new PersonFromFile(file.ReadLine().Split(";"));
                    Console.WriteLine($"|{line.Surname}\t | {line.Name}\t | {line.Telephon}\t | {line.Age}\t | {line.Pol}\t");
                }
            }
        }
        public static void InputInFile()
        {
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            while (true)
            {
                if (Regex.IsMatch(name, @"(?=^[А-Я])(?=[А-я]+)") is true)
                {
                    break;
                }
                Console.WriteLine("Имя введенно неверно, повторите ввод:");
                name = Console.ReadLine();
            }
            Console.WriteLine("Введите фамилию:");
            string surname = Console.ReadLine();
            while (true)
            {
                if (Regex.IsMatch(surname, @"(?=^[А-Я])(?=[А-я]{2,})") is true)
                {
                    break;
                }
                Console.WriteLine("Фамимлия введенна неверно, повторите ввод:");
                surname = Console.ReadLine();
            }
            Console.WriteLine("Введите телефон:");
            string telephone = Console.ReadLine();
            while(true)
            {
                if (Regex.IsMatch(telephone, @"^8\(\d{3}\)\d{3}-\d{2}-\d{2}$"))
                {
                    break;
                }
                Console.WriteLine("Телефон введен неверно, повторите ввод:");
                telephone = Console.ReadLine();
            }
            Console.WriteLine("Введите возраст:");
            int age = Convert.ToInt32(Console.ReadLine());
            while(true)
            {
                if(age >= 18)
                {
                    break;
                }
                Console.WriteLine("Возраст введен неверно, повторите ввод:");
                age = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Введите пол:");
            string pol = Console.ReadLine();
            while(true)
            {
                if (Regex.IsMatch(pol, @"^(мужской|женский)$") is true)
                {
                    break;
                }
                Console.WriteLine("Пол введен неверно, повторите ввод:");
                pol = Console.ReadLine();
            }
                File.AppendAllText("Data.csv",$"{surname};{name};{telephone};{age};{pol}");
        }
        public static List<PersonFromFile> InputDataInList()
        {
            List<PersonFromFile> list = new List<PersonFromFile>();
            using (StreamReader file = new StreamReader("Data.csv"))
            {
                while (!file.EndOfStream)
                {
                    list.Add(new PersonFromFile(file.ReadLine().Split(";")));
                }
            }
            return list;
        }
        public static void PersonWithMaxAge(List<PersonFromFile> data)
        {
            PersonFromFile Res = data.MaxBy(a => a.Age);
            Console.WriteLine("Человек с максимальным возрастом:");
            Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
            File.AppendAllText("PersonWithMaxAge.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
        }
        public static void AllWoman(List<PersonFromFile> data)
        {
            List<PersonFromFile> Resuslt = data.Where(a => a.Pol == "женский").ToList();
            Console.WriteLine("Все женщины:");
            foreach(var Res in Resuslt)
            {
                Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
                File.AppendAllText("AllWoman.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
            }
        }
        public static void FirstManWithNameBeginningA(List<PersonFromFile> data)
        {
            PersonFromFile Res = data.Where(a => a.Pol == "мужской" && a.Name[0] == 'А').First();
            Console.WriteLine("Первый мужчина с именем на А:");
            Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
            File.AppendAllText("FirstManWithNameBeginningA.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
        }
        public static void HumansWithFamily6len(List<PersonFromFile> data)
        {
            List<PersonFromFile> Resuslt = data.Where(a => a.Surname.Length == 6).ToList();
            Console.WriteLine("Все люди с фамилией на 6 букв:");
            foreach (var Res in Resuslt)
            {
                Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
                File.AppendAllText("HumansWithFamily6len.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
            }
        }
        public static void FirstHumanWithTelEndOn9(List<PersonFromFile> data)
        {
            PersonFromFile Res = data.FindAll(a => a.Telephon[14] == '9').First();
            Console.WriteLine("Первый человек с телефон заканчивающимся на 9:");
            Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
            File.AppendAllText("FirstHumanWithTelEndOn9.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
        }
        public static void OrderBY(List<PersonFromFile> data)
        {
            List<PersonFromFile> Resuslt = data.OrderBy(a => a.Age).ToList();
            Console.WriteLine("Сортировка по возрасту:");
            foreach (var Res in Resuslt)
            {
                Console.WriteLine($"|{Res.Surname}\t | {Res.Name}\t | {Res.Telephon}\t | {Res.Age}\t | {Res.Pol}\t");
                File.AppendAllText("Sorting.csv", $"{Res.Surname};{Res.Name};{Res.Telephon};{Res.Age};{Res.Pol}\n");
            }
        }
    }
}