using System;

namespace Uniza.Namedays.ViewerConsoleApp
{
    public class CLI
    {
        public static int Main()
        {

            NamedayCalendar calendar = new();
            FileInfo vv = new FileInfo("names.csv");
            calendar.Load(vv);

            Console.WriteLine("KALENDÁR MIEN");

            var kto = "";
            try
            {
                kto = calendar[DateTime.Now.Day, DateTime.Now.Month][0];
            }
            catch (Exception e)
            {
                kto = "nemá nikto meniny.";
            }
            
            Console.WriteLine("Dnes " + DateTime.Now.ToString("dd/MM/yyyy") + " " + kto);
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
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    Console.WriteLine("OTVORENIE");
                    Console.WriteLine("Zadajte cestu k súboru kalendára mien alebo stlačte Enter pre ukončenie.");
                    var input_je_zly = true;
                    while (input_je_zly)
                    {
                        Console.WriteLine("Zadajte cestu k CSV súboru: ");
                        var input = Console.ReadLine();
                        if (input == "")
                        {
                            Environment.Exit(0);
                        }

                        var index_dot = input.IndexOf('.');
                        var type = input.Substring(index_dot, 4);

                        if (type != ".csv")
                        {
                            Console.WriteLine("Zadaný súbor " + input + " nie je typu CSV!");
                            continue;
                        }

                        FileInfo info = new FileInfo(input);

                        if (!info.Exists)
                        {
                            Console.WriteLine("Zadaný súbor " + input + " neexistuje!");
                            continue;
                        }

                        calendar.Load(info);
                        Console.WriteLine("Súbor kalendára bol načítaný.");
                        input_je_zly = false;
                        Console.WriteLine("Pre pokračovanie stlačte Enter.");
                        Console.ReadKey();
                    }
                    // TODO navrat na menu
                    break;
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Console.WriteLine("ŠTATISTIKA");
                    Console.WriteLine("Celkový počet mien v kalendári: " + calendar.NameCount);
                    Console.WriteLine("Celkový počet dní obsahujúcich mená v kalendári: " + calendar.DayCount);
                    Console.WriteLine("Celkový počet mien v jednotlivých mesiacoch: ");
                    var months = new string[] { "január", "február", "marec", "apríl", "máj", "jún", "júl", "august", "september", "október", "november", "december"};
                    for (int i = 0; i < months.Length; i++)
                    {
                        var count = calendar.GetNamedays(i + 1);
                        Console.Write(months[i] + ": " + count.Count() + "\n");
                    }

                    Console.WriteLine("Počet mien podľa začiatočných písmen: ");
                    var pismena = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "Ľ", "M", "N", "O", "P", "R", "S", "Š", "T", "U", "V", "X", "Z", "Ž"};
                    // TODO does not work with slovak diacritis
                    for (int i = 0; i < pismena.Length; i++)
                    {
                        var count = calendar.GetNamedays(pismena[i]);
                        Console.Write(pismena[i] + ": " + count.Count() + "\n");
                    }

                    Console.WriteLine("Pre skončenie stlačte Enter.");
                    Console.ReadKey();
                    break;
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    // TODO implement
                    break;
                case ConsoleKey.NumPad4:
                    Console.Clear();
                    // TODO implement
                    break;
                case ConsoleKey.NumPad5:
                    Console.Clear();
                    // TODO implement
                    break;
                case ConsoleKey.NumPad6:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            return 0;
        }
        
    }
}
