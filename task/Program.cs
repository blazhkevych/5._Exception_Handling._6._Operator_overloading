using System.Xml.Linq;

namespace task
{
    internal class Program
    {
        /*
         *Для класса Date из предыдущего задания предусмотреть в 
           свойствах и методах проверку допустимости передаваемых
           значений. В случае недопустимых значений необходимо 
           генерировать исключения.
         */
        /*
         * В классе Date из предыдущего задания реализовать:
           • перегруженный бинарный оператор «-», возвращающий 
           разницу между двумя датами в днях; 
           • перегруженный оператор «+», изменяющий дату на заданное 
           количество дней; 
           • операторные методы «++», «--», «>», «<», «==», «!=».
         */

        static void Main(string[] args)
        {
            Date date1 = new(), date2 = new();

            try
            {
                date1.Year = 2022;
                date1.Month = 2;
                date1.Day = 29;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            Console.WriteLine("Первая дата должна быть меньше второй даты.");
            Console.WriteLine("Введите первую дату.");
            try
            {
                date1.Input();
                Console.WriteLine($"{date1.Day}.{date1.Month}.{date1.Year} ( {date1.DayOfWeek} )");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("Введите вторую дату. ");
                date2.Input();
                Console.WriteLine($"{date2.Day}.{date2.Month}.{date2.Year} ( {date2.DayOfWeek} )");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            int difference = date1 - date2;
            Console.WriteLine($"{date2.Day}.{date2.Month}.{date2.Year} ( {date2.DayOfWeek} ) - " +
                              $"{date1.Day}.{date1.Month}.{date1.Year} ( {date1.DayOfWeek} ) = {difference} days.");

            try
            {
                Console.WriteLine($"date 1 до изменения {date1.Day}.{date1.Month}.{date1.Year} ( {date1.DayOfWeek} )");
                Console.WriteLine("Введите кол-во дней для изменения date1:");
                int d;
                string days = Console.ReadLine();
                if (int.TryParse(days, out int numberDay) && days != "0" && Convert.ToInt32(days) > 0)
                    d = Convert.ToInt32(days);
                else
                    throw new Exception("Дней должно быть >= 1 !");
                date1 += d;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine($"date1 после изменения {date1.Day}.{date1.Month}.{date1.Year} ( {date1.DayOfWeek} )");

            Console.WriteLine();
            Date date3 = new(29, 02, 2000);
            date3.Show();
            date3++;
            Console.WriteLine();
            Console.WriteLine("date3 после ++");
            date3.Show();

            Console.WriteLine();
            Date date4 = new(1, 3, 2020);
            date4.Show();
            date4--;
            Console.WriteLine();
            Console.WriteLine("date4 после --");
            date4.Show();

            Console.WriteLine();
            Date date5 = new(29, 2, 2020);
            Console.WriteLine("date5");
            date5.Show();
            Console.WriteLine();
            Date date6 = new(1, 3, 2020);
            Console.WriteLine("date6");
            date6.Show();

            if (date6 > date5)
            {
                Console.WriteLine();
                Console.WriteLine("date6 > date5 ? true");
            }

            if (date6 < date5)
            {
                Console.WriteLine();
                Console.WriteLine("date6 < date5 ? false");
            }

            Console.WriteLine();
            Date date7 = new(29, 2, 2020);
            Console.WriteLine("date7");
            date7.Show();

            Console.WriteLine();
            Date date8 = new(1, 3, 2020);
            Console.WriteLine("date8");
            date8.Show();

            Console.WriteLine();
            Date date9 = new(1, 3, 2020);
            Console.WriteLine("date9");
            date9.Show();

            Console.WriteLine();
            Console.WriteLine();
            if (date9 == date8)
                Console.WriteLine("date9 == date8 ? true");

            Console.WriteLine();
            if (date9 != date7)
                Console.WriteLine("date9 != date7 ? true");
        }
    }
}