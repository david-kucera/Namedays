﻿using Microsoft.VisualBasic;

namespace Uniza.Namedays.ViewerConsoleApp
{
    public class CLI
    {
        public static int Main()
        {
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
            var volba = Console.Read();

            switch (volba)
            {
                case 1:
                    Console.Clear();
                    // TODO implement
                    break;
                case 2:
                    Console.Clear();
                    // TODO implement
                    break;
                case 3:
                    Console.Clear();
                    // TODO implement
                    break;
                case 4:
                    Console.Clear();
                    // TODO implement
                    break;
                case 5:
                    Console.Clear();
                    // TODO implement
                    break;
                case 6:
                    Console.Clear();
                    // TODO implement
                    return 0;
            }

            return 0;
        }
        
    }
}