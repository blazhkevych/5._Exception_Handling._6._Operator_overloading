using System;
using System.Threading;
using System.Xml.Linq;

namespace task;

internal class Date
{
    private int m_Day;              // День.
    private int m_Month;            // Месяц.
    private int m_Year;             // Год.
    private string m_DayOfWeek;     // День недели.

    public int Day
    {
        get { return m_Day; }
        set
        {
            if (m_Year > 0)
            {
                if (m_Month > 0 && m_Month < 13)
                {
                    if (
                        (IsItALeapYear(m_Year) & (m_Month == (int)Months.February) & (value == 29))                 // Если 29 февраля в високосном году.
                        | (IsItALeapYear(m_Year) & (m_Month == (int)Months.February) & (value > 0 & value < 29))    // Если 1-28 февраля, високосный год.
                        | (!IsItALeapYear(m_Year) & (m_Month == (int)Months.February) & (value > 0 & value < 29))    // Если 1-28 февраля, обычный год.
                        | ((m_Month == (int)Months.January
                            | m_Month == (int)Months.March
                            | m_Month == (int)Months.May
                            | m_Month == (int)Months.July
                            | m_Month == (int)Months.August
                            | m_Month == (int)Months.October
                            | m_Month == (int)Months.December) & (value > 0 & value < 32))    // Все месяца, в которых 31 день.
                        | ((m_Month == (int)Months.April
                             | m_Month == (int)Months.June
                             | m_Month == (int)Months.September
                             | m_Month == (int)Months.November) & (value > 0 & value < 31))    // Все месяца, в которых 30 дней.
                    )
                    {
                        // Все значения корректны.
                        m_Day = value;
                    }
                    else
                        throw new Exception($"Введен не корректный день.");
                }
                else
                    throw new Exception($"Введен не корректный месяц.");
            }
            else
                throw new Exception($"Введен не корректный год.");
        }
    }

    public int Month
    {
        get { return m_Month; }
        set
        {
            if (value >= 1 & value <= 12)
                m_Month = value;
            else
                throw new Exception("Месяц должен быть >= 1 & <= 12");
        }
    }

    public int Year
    {
        get
        {
            return m_Year;
        }
        set
        {
            if (value >= 1)
                m_Year = value;
            else
                throw new Exception("Год должен быть >= 1");
        }
    }

    public string DayOfWeek
    {
        get
        {
            return m_DayOfWeek = WhatDayOfTheWeek();
        }
    }

    // Месяца.
    enum Months
    {
        January = 1,    // Январь, 31 день
        February = 2,   // Февраль, 28 дней (В високосные годы вводится дополнительный день — 29 февраля.)
        March = 3,      // Март, 31 день 
        April = 4,      // Апрель, 30 дней 
        May = 5,        // Май, 31 день 
        June = 6,       // Июнь, 30 дней 
        July = 7,       // Июль, 31 день 
        August = 8,     // Август, 31 день 
        September = 9,  // Сентябрь, 30 дней 
        October = 10,   // Октябрь, 31 день 
        November = 11,  // Ноябрь, 30 дней 
        December = 12   // Декабрь, 31 день 
    };

    // Конструктор по умолчанию.
    public Date()
    {
        m_Day = 1;
        m_Month = 1;
        m_Year = 1;
        m_DayOfWeek = "default";
    }

    // Конструктор с параметрами.
    public Date(in int day, in int month, in int year)
    {
        if (year > 0)
        {
            if (month > 0 && month < 13)
            {
                if (
                    ((IsItALeapYear(year))
                     && (month == (int)Months.February) && (day == 29))                 // Если 29 февраля в високосном году.
                    || ((IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, високосный год.
                    || (!(IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, обычный год.
                    || ((month == (int)Months.January
                         || month == (int)Months.March
                         || month == (int)Months.May
                         || month == (int)Months.July
                         || month == (int)Months.August
                         || month == (int)Months.October
                         || month == (int)Months.December) && (day > 0 && day < 32))    // Все месяца, в которых 31 день.
                    || ((month == (int)Months.April
                         || month == (int)Months.June
                         || month == (int)Months.September
                         || month == (int)Months.November) && (day > 0 && day < 31))    // Все месяца, в которых 30 дней.
                )
                {
                    // Все введённые значения корректны.
                    m_Day = day;
                    m_Month = month;
                    m_Year = year;
                }
                else
                    throw new Exception($"Введен не корректный день.");
            }
            else
                throw new Exception($"Введен не корректный месяц.");
        }
        else
            throw new Exception($"Введен не корректный год.");
    }

    private string WhatDayOfTheWeek()
    {
        string? str = null;
        int a = (14 - m_Month) / 12;
        int y = m_Year - a;
        int m = m_Month + 12 * a - 2;
        int dow = (7000 + (m_Day + y + y / 4 - y / 100 + y / 400 + (31 * m) / 12)) % 7;

        switch (dow)
        {
            case 1:
                str = "Понедельник";
                break;
            case 2:
                str = "Вторник";
                break;
            case 3:
                str = "Среда";
                break;
            case 4:
                str = "Четверг";
                break;
            case 5:
                str = "Пятница";
                break;
            case 6:
                str = "Суббота";
                break;
            case 0:
                str = "Воскресенье";
                break;
        }

        return str;
    }

    // Ввод даты.
    public void Input()
    {
        int d, m, y;

        Console.WriteLine("День:");
        string day = Console.ReadLine();
        if (int.TryParse(day, out int numberDay) && day != "0" && Convert.ToInt32(day) > 0)
            d = Convert.ToInt32(day);
        else
            throw new Exception("День должен быть >= 1 & <= 31 !");

        Console.WriteLine("Месяц:");
        string month = Console.ReadLine();
        if (int.TryParse(month, out int numberMonth) && month != "0" && Convert.ToInt32(month) > 0)
            m = Convert.ToInt32(month);
        else
            throw new Exception("Месяц должен быть >= 1 & <= 12 !");

        Console.WriteLine("Год:");
        string year = Console.ReadLine();
        if (int.TryParse(year, out int numberYear) && year != "0" && Convert.ToInt32(year) > 0)
            y = Convert.ToInt32(year);
        else
            throw new Exception("Год должен быть >= 1 !");

        // Существует ли такая дата ?
        try
        {
            DateValidationCheck(d, m, y);

            m_Day = d;
            m_Month = m;
            m_Year = y;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Environment.Exit(0); // Досрочное завершение программы, из-за несуществующей даты.
        }
    }

    // Проверяет является ли год високосным.
    private bool IsItALeapYear(int year)
    {
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            return true;
        else
            return false;
    }

    private static bool IsItALeapYearStatic(int year)
    {
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            return true;
        else
            return false;
    }

    // Проверяет корректность введенных значений даты.
    private void DateValidationCheck(int day, int month, int year)
    {
        if (year > 0)
        {
            if (month > 0 && month < 13)
            {
                if (
                    ((IsItALeapYear(year))
                     && (month == (int)Months.February) && (day == 29))                 // Если 29 февраля в високосном году.
                    || ((IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, високосный год.
                    || (!(IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, обычный год.
                    || ((month == (int)Months.January
                         || month == (int)Months.March
                         || month == (int)Months.May
                         || month == (int)Months.July
                         || month == (int)Months.August
                         || month == (int)Months.October
                         || month == (int)Months.December) && (day > 0 && day < 32))    // Все месяца, в которых 31 день.
                    || ((month == (int)Months.April
                         || month == (int)Months.June
                         || month == (int)Months.September
                         || month == (int)Months.November) && (day > 0 && day < 31))    // Все месяца, в которых 30 дней.
                )
                {
                    // Все введённые значения корректны.
                }
                else
                    throw new Exception($"Введен не корректный день.");
            }
            else
                throw new Exception($"Введен не корректный месяц.");
        }
        else
            throw new Exception($"Введен не корректный год.");
    }

    // Вывод даты.
    public void Show()
    {
        Console.Write($"{m_Day}.{m_Month}.{m_Year} ({m_DayOfWeek = DayOfWeek})");
    }

    // Деконструкторы позволяют выполнить декомпозицию объекта на отдельные части.
    private void Deconstruct(out int day, out int month, out int year, out string? dayOfWeek)
    {
        day = m_Day;
        month = m_Month;
        year = m_Year;
        dayOfWeek = m_DayOfWeek;
    }

    // Перегруженный бинарный оператор «-», возвращающий 
    // разницу между двумя датами в днях.
    public static int operator -(Date d1, Date d2)
    {
        (int day1, int month1, int year1, _) = d1;
        (int day2, int month2, int year2, _) = d2;

        int differenceIs = 0; // Счетчик разности в днях между этими датами.

        if (year1 == year2 && month1 == month2 && day1 == day2) // Если даты равны.
        {
            return differenceIs = 0;
        }
        else
        {
            // Пока даты не равны, вычисляем следующую дату.
            while (!(year1 == year2 && month1 == month2 && day1 == day2))
            {
                switch ((Months)month1)
                {
                    case Months.April: // Расчет следующей даты за введенной, для месяцев c 30 днями.
                    case Months.June:
                    case Months.September:
                    case Months.November:
                        day1++;
                        if (day1 > 30)
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;

                    case Months.January: // Расчет следующей даты за введенной, для месяцев c 31 днем.
                    case Months.March:
                    case Months.May:
                    case Months.July:
                    case Months.August:
                    case Months.October:
                    case Months.December:
                        day1++;
                        if (day1 > 31)
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;

                    case Months.February: // Расчет следующей даты за введенной, для февраля (как высокосного так и не високосного).
                        day1++;
                        if (!(year1 % 400 == 0 || (year1 % 100 != 0 && year1 % 4 == 0)) && (day1 > 28)) // Не високосный.
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        else if ((year1 % 400 == 0 || (year1 % 100 != 0 && year1 % 4 == 0)) && (day1 > 29)) // Високосный.
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;
                }
                differenceIs++;
            }
        }
        return differenceIs;
    }

    // Изменение даты на заданное количество дней.
    public static Date operator +(Date d, int days)
    {
        while (days > 0) // Пока даты не равны, вычисляем следующую дату.
        {
            switch (d.m_Month)
            {
                case (int)Months.April: // Расчет следующей даты за введенной, для месяцев c 30 днями.
                case (int)Months.June:
                case (int)Months.September:
                case (int)Months.November:
                    d.m_Day++;
                    if (d.m_Day > 30)
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;

                case (int)Months.January: // Расчет следующей даты за введенной, для месяцев c 31 днем.
                case (int)Months.March:
                case (int)Months.May:
                case (int)Months.July:
                case (int)Months.August:
                case (int)Months.October:
                case (int)Months.December:
                    d.m_Day++;
                    if (d.m_Day > 31)
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;

                case (int)Months.February: // Расчет следующей даты за введенной, для февраля (как высокосного так и не високосного).
                    d.m_Day++;
                    if (!(d.m_Year % 400 == 0 || (d.m_Year % 100 != 0 && d.m_Year % 4 == 0)) && (d.m_Day > 28)) // Не високосный.
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    else if ((d.m_Year % 400 == 0 || (d.m_Year % 100 != 0 && d.m_Year % 4 == 0)) && (d.m_Day > 29)) // Високосный.
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;
            }
            days--;
        }

        return d;
    }

    // Изменение даты на заданное количество дней.
    public static Date operator +(int days, Date d)
    {
        while (days > 0) // Пока даты не равны, вычисляем следующую дату.
        {
            switch (d.m_Month)
            {
                case (int)Months.April: // Расчет следующей даты за введенной, для месяцев c 30 днями.
                case (int)Months.June:
                case (int)Months.September:
                case (int)Months.November:
                    d.m_Day++;
                    if (d.m_Day > 30)
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;

                case (int)Months.January: // Расчет следующей даты за введенной, для месяцев c 31 днем.
                case (int)Months.March:
                case (int)Months.May:
                case (int)Months.July:
                case (int)Months.August:
                case (int)Months.October:
                case (int)Months.December:
                    d.m_Day++;
                    if (d.m_Day > 31)
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;

                case (int)Months.February: // Расчет следующей даты за введенной, для февраля (как высокосного так и не високосного).
                    d.m_Day++;
                    if (!(d.m_Year % 400 == 0 || (d.m_Year % 100 != 0 && d.m_Year % 4 == 0)) && (d.m_Day > 28)) // Не високосный.
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    else if ((d.m_Year % 400 == 0 || (d.m_Year % 100 != 0 && d.m_Year % 4 == 0)) && (d.m_Day > 29)) // Високосный.
                    {
                        d.m_Day = 1;
                        d.m_Month++;
                        if (d.m_Month > 12)
                        {
                            d.m_Month = 1;
                            d.m_Year++;
                        }
                    }
                    break;
            }
            days--;
        }

        return d;
    }

    // Перегрузка унарного оператора "++"
    public static Date operator ++(Date d)
    {
        d = d + 1;
        return d;
    }

    // Функция, проверяющая корректность введенных значений даты.
    private static bool DateValidationCheckBool(int day, int month, int year)
    {
        if (year > 0)
        {
            if (month > 0 && month < 13)
            {
                if (
                    ((IsItALeapYearStatic(year))
                && (month == (int)Months.February) && (day == 29))                 // Если 29 февраля в високосном году.
                    || ((IsItALeapYearStatic(year))
                && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, високосный год.
                    || (!(IsItALeapYearStatic(year))
                && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, обычный год.
                || ((month == (int)Months.January
                || month == (int)Months.March
                || month == (int)Months.May
                         || month == (int)Months.July
                         || month == (int)Months.August
                         || month == (int)Months.October
                         || month == (int)Months.December) && (day > 0 && day < 32))    // Все месяца, в которых 31 день.
                    || ((month == (int)Months.April
                         || month == (int)Months.June
                         || month == (int)Months.September
                         || month == (int)Months.November) && (day > 0 && day < 31))    // Все месяца, в которых 30 дней.
                )
                {
                    return true; // Все введённые значения корректны.
                }
                else
                    return false; // Введен не корректный день.
            }
            else
                return false; // Введен не корректный месяц. 
        }
        else
            return false; // Введен не корректный год. 
    }

    // Перегрузка унарного оператора "--"
    public static Date operator --(Date d)
    {
        (int day, int month, int year, _) = d;

        do
        {
            day--;
            if (day < 1)
            {
                day = 31;
                month--;
                if (month < 1)
                {
                    month = 12;
                    year--;
                    if (year < 1)
                    {
                        day = 1;
                        month = 1;
                        year = 1;
                        Date res1 = new(day, month, year);
                        return res1;
                    }
                }
            }
        } while (DateValidationCheckBool(day, month, year) == false);

        Date res2 = new(day, month, year);
        return res2;
    }

    // Перегрузка оператора ">".
    public static bool operator >(in Date d1, in Date d2)
    {
        (int day1, int month1, int year1, _) = d1;
        (int day2, int month2, int year2, _) = d2;

        if (year1 > year2)
            return true;
        else
        {
            if (month1 > month2)
                return true;
            else
            {
                if (day1 > day2)
                    return true;
                else
                    return false;
            }
        }
    }

    // Перегрузка оператора "<".
    public static bool operator <(in Date d1, in Date d2)
    {
        (int day1, int month1, int year1, _) = d1;
        (int day2, int month2, int year2, _) = d2;

        if (year1 < year2)
            return true;
        else
        {
            if (month1 < month2)
                return true;
            else
            {
                if (day1 < day2)
                    return true;
                else
                    return false;
            }
        }
    }

    // Перегрузка оператора "<".
    public static bool operator ==(in Date d1, in Date d2)
    {
        (int day1, int month1, int year1, _) = d1;
        (int day2, int month2, int year2, _) = d2;

        if (year1 == year2 && month1 == month2 && day1 == day2)
            return true;
        else
            return false;
    }

    // Перегрузка оператора "!=".
    public static bool operator !=(in Date d1, in Date d2)
    {
        (int day1, int month1, int year1, _) = d1;
        (int day2, int month2, int year2, _) = d2;

        if (year1 != year2 || month1 != month2 || day1 != day2)
            return true;
        else
            return false;
    }
}