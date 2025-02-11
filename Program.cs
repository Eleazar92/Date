///Простейший класс, реализующий работу с датой. Независим от системных классов типа DateTime (за исключением одного конструктора, сделанного для удобства).
///Поддерживает год в Георгианском календаре от 9999 до нашей эры вплоть до 9999 нашей эры. Месяц хранит в виде 0-11 (0 = январь).
///Сама программа демонстрирует создание и вывод сегодняшней даты, а также любого числа производных, введенных с экрана.
using Microsoft.VisualBasic;

namespace Date
{
    public class Date
    {
        public int Year { get; set { field = (value < -9999 || value > 9999) ? 0 : value; } } = 0;
        public int Month { get; set { field = (value < 0 || value > 11) ? 0 : value; } } = 0;
        public int Day { get; set { field = (value < 0 || value > (CalcMonthDays(this.Year, this.Month))) ? 1 : value; } }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public Date(string date)
        {
            string[] dateElems = date.Split('.');
            if (dateElems.Length >= 3)
            {
                Month = int.Parse(dateElems[1]) - 1;
                Year = int.Parse(dateElems[2]);
                Day = int.Parse(dateElems[0]);
            }
            else throw new ArgumentException("Некорректный формат строки с датой");

        }

        public Date() : this(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day) { }

        public bool LeapYear() => LeapYear(this.Year);
        public static bool LeapYear(int year) => (year >= -9999 && year <= 9999 && ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0));
        public int CalcMonthDays() => CalcMonthDays(this.Year, this.Month);
        public static int CalcMonthDays(int year, int month) =>
            (LeapYear(year), month) switch
            {
                (true, 1) => 29,
                (false, 1) => 28,
                (_, 0 or 2 or 4 or 6 or 7 or 9 or 11) => 31,
                (_, 3 or 5 or 8 or 10) => 30,
                _ => throw new Exception("Некорректные месяц или год")
            };
        public override string ToString()
        {
            return $"The date is {this.Day:00}.{this.Month + 1:00}.{Math.Abs(this.Year)} {(this.Year < 0 ? "b.c" : "a.d.")}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var today = new Date();
            Console.WriteLine("Today: " + today.ToString());
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите новую дату в формате DD.MM.YYYY");
                    var date = new Date(Console.ReadLine() ?? "");
                    Console.WriteLine(date.ToString());
                }
                catch (Exception e) { Console.WriteLine(e.Message); continue; }
                Console.WriteLine("Прервать выполнение программы? - Y/n");
                if (Console.ReadLine()?.ToUpper() == "Y") break;
            }

        }
    }
}
