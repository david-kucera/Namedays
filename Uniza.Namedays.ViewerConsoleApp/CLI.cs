using System.Globalization;
using System.Text;

namespace Uniza.Namedays.ViewerConsoleApp
{
    public class CLI
    {
        public static int Main(string[] args)
        {
            NamedayCalendar calendar = new();
            FileInfo def = new(args[0]);
            calendar.Load(def);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("KALENDÁR MIEN");
                var celebrators = calendar[DateTime.Now.Day, DateTime.Now.Month];
                string mena;
                if (celebrators.Length == 0)
                {
                    mena = "nemá nikto meniny.";
                }
                else
                {
                    mena = celebrators[0];
                    for (var i = 1; i < celebrators.Length; i++)
                    {
                        mena += ", ";
                        mena += celebrators[i];
                    }
                }

                Console.WriteLine("Dnes " + DateTime.Now.ToString("dd/MM/yyyy") + " " + mena);
                Console.WriteLine("Zajtra má meniny: " + calendar[DateTime.Now.AddDays(1).Day, DateTime.Now.AddDays(1).Month][0]);
                Console.WriteLine("");

                Console.WriteLine("Menu");
                Console.WriteLine("1 - načítať kalendár");
                Console.WriteLine("2 - zobraziť štatistiku");
                Console.WriteLine("3 - vyhľadať mená");
                Console.WriteLine("4 - vyhľadať mená podľa dátumu");
                Console.WriteLine("5 - zobraziť kalendár mien v mesiaci");
                Console.WriteLine("6 | Escape - koniec");
                Console.Write("Vaša voľba ");
                var volba = Console.ReadKey();

                switch (volba.Key)
                {
                    case ConsoleKey.D1 or ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.WriteLine("OTVORENIE");
                        Console.WriteLine("Zadajte cestu k súboru kalendára mien alebo stlačte Enter pre ukončenie.");
                        while (true)
                        {
                            Console.WriteLine("Zadajte cestu k CSV súboru: ");
                            var input = Console.ReadLine();
                            if (input == "")
                            {
                                Environment.Exit(0);
                            }

                            var indexDot = input!.IndexOf('.');
                            var type = input.Substring(indexDot, 4);

                            if (type != ".csv")
                            {
                                Console.WriteLine("Zadaný súbor " + input + " nie je typu CSV!");
                                continue;
                            }

                            FileInfo info = new(input);

                            if (!info.Exists)
                            {
                                Console.WriteLine("Zadaný súbor " + input + " neexistuje!");
                                continue;
                            }

                            calendar.Load(info);
                            Console.WriteLine("Súbor kalendára bol načítaný.");
                            Console.WriteLine("Pre pokračovanie stlačte Enter.");
                            Console.ReadKey();
                            break;
                        }
                        continue;

                    case ConsoleKey.D2 or ConsoleKey.NumPad2:
                        Console.Clear();
                        Console.WriteLine("ŠTATISTIKA");
                        Console.WriteLine("Celkový počet mien v kalendári: " + calendar.NameCount);
                        Console.WriteLine("Celkový počet dní obsahujúcich mená v kalendári: " + calendar.DayCount);
                        Console.WriteLine("Celkový počet mien v jednotlivých mesiacoch: ");
                        var months = new string[] { "január", "február", "marec", "apríl", "máj", "jún", "júl", "august", "september", "október", "november", "december" };
                        for (var i = 0; i < months.Length; i++)
                        {
                            var count = calendar.GetNamedays(i + 1);
                            Console.Write(" " + months[i] + ": " + count.Count() + "\n");
                        }

                        Console.WriteLine("Počet mien podľa začiatočných písmen: ");
                        var pismena = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "Ľ", "M", "N", "O", "P", "R", "S", "Š", "T", "U", "V", "X", "Z", "Ž" };
                        for (var i = 0; i < pismena.Length; i++)
                        {
                            var count = calendar.GetNamedays(pismena[i]);
                            Console.Write(" " + pismena[i] + ": " + count.Count() + "\n");
                        }

                        Console.WriteLine("Počet mien podľa dĺžky znakov:");
                        for (var i = 0; i < 12; i++)
                        {
                            var count = calendar.GetNamedays(i, true);
                            if (count.Count() == 0)
                            {
                                continue;
                            }

                            Console.WriteLine(" " + i + ": " + count.Count());
                        }

                        Console.WriteLine("Pre skončenie stlačte Enter.");
                        Console.ReadKey();
                        continue;

                    case ConsoleKey.D3 or ConsoleKey.NumPad3:
                        Console.Clear();

                        Console.WriteLine("VYHĽADÁVANIE MIEN");
                        Console.WriteLine("Pre ukončenie stlačte Enter.");

                        while (true)
                        {
                            Console.Write("Zadajte meno (regulárny výraz): ");
                            var input = Console.ReadLine();
                            if (input == "")
                            {
                                break;
                            }

                            var count = calendar.GetNamedays(input!).ToList().Count;

                            if (count == 0)
                            {
                                Console.WriteLine("Neboli nájdené žiadne mená.");
                                continue;
                            }

                            var names = calendar.GetNamedays(input!).ToArray();
                            for (int i = 1; i <= count; i++)
                            {
                                var meno = names[i-1];
                                Console.Write(i + ". " + meno.Name + " (" + meno.DayMonth.Day + "." + meno.DayMonth.Month + ")\n");
                            }
                        }
                        continue;

                    case ConsoleKey.D4 or ConsoleKey.NumPad4:
                        Console.Clear();
                        Console.WriteLine("VYHĽADÁVANIE MIEN PODĽA DÁTUMU");
                        Console.WriteLine("Pre ukončenie stlačte Enter.");

                        while (true)
                        {
                            Console.Write("Zadajte deň a mesiac: ");
                            var input = Console.ReadLine();
                            if (input == "")
                            {
                                break;
                            }

                            var data = input!.Split(".");
                            var day = int.Parse(data[0]);
                            var month = int.Parse(data[1]);
                            var names = calendar[day, month];

                            if (names.Length == 0)
                            {
                                Console.WriteLine("Neboli nájdené žiadne mená.");
                                continue;
                            }

                            var i = 1;
                            foreach (var name in names)
                            {
                                Console.WriteLine(i + ". " + name);
                                i++;
                            }
                        }
                        continue;

                    case ConsoleKey.D5 or ConsoleKey.NumPad5:
                        Console.Clear();
                        var aktual = DateTime.Now;

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("KALENDÁR MENÍN");
                            Console.WriteLine(aktual.ToString("MMM") + " " + aktual.Year + ":");

                            for (var i = 1; i <= DateTime.DaysInMonth(aktual.Year, aktual.Month); i++)
                            {
                                var date = new DateTime(aktual.Year, aktual.Month, i);

                                if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                if (date.Month == DateTime.Now.Month && date.Day == DateTime.Now.Day && date.Year == DateTime.Now.Year)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }

                                var names = calendar[date];
                                var output = "";
                                if (names.Length == 0)
                                {
                                    output = "";
                                }
                                else
                                {
                                    output += names[0];
                                    for (var j = 1; j < names.Length; j++)
                                    {
                                        output += ", ";
                                        output += names[j];
                                    }
                                }
                                Console.WriteLine(" " + date.Day + "." + date.Month + " " +
                                                  new DateTime(aktual.Year, date.Month, date.Day)
                                                      .ToString("ddd", new CultureInfo("sk-SK")) + " " + output);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine();
                            Console.WriteLine("Šípka doľava / doprava - mesiac dozadu / dopredu.");
                            Console.WriteLine("Šípka dole / hore - rok dozadu / dopredu.");
                            Console.WriteLine("Kláves Home alebo D - aktuálny deň.");
                            Console.WriteLine("Pre ukončenie stlačte Enter.");
                            var input = Console.ReadKey();

                            if (input.Key.Equals(ConsoleKey.Enter))
                            {
                                break;
                            }
                            switch (input.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    aktual = aktual.AddYears(1);
                                    break;
                                case ConsoleKey.DownArrow:
                                    aktual = aktual.AddYears(-1);
                                    break;
                                case ConsoleKey.LeftArrow:
                                    aktual = aktual.AddMonths(-1);
                                    break;
                                case ConsoleKey.RightArrow:
                                    aktual = aktual.AddMonths(1);
                                    break;
                                case ConsoleKey.Home:
                                case ConsoleKey.D:
                                    aktual = DateTime.Now;
                                    break;
                            }
                        }
                        continue;

                    case ConsoleKey.D6 or ConsoleKey.NumPad6 or ConsoleKey.Escape:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default: // If clicked other option, wait for right input.
                        continue;
                }
                break;
            }
            return 0;
        }
        
    }
}
