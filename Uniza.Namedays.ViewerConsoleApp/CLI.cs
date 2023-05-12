namespace Uniza.Namedays.ViewerConsoleApp
{
    public class CLI
    {
        public static int Main()
        {
            NamedayCalendar calendar = new NamedayCalendar();
            Console.WriteLine("KALENDÁR MIEN");
            // TODO set right values to dnes, kto, zajtra
            var dnes = DateTime.Now;
            var kto = "Emil";
            Console.WriteLine("Dnes " + dnes.ToString("dd/MM/yyyy") + " " + kto);
            var zajtra = "Stefan";
            Console.WriteLine("Zajtra má meniny: " + zajtra);
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
                    // TODO implement
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
