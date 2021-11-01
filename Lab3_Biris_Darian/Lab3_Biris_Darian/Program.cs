using System;
using System.Collections.Generic;
using System.IO;

namespace Lab3_Biris_Darian
{
    class Program
    {
        private static string ID_DRIVE = "14Hlgnw6tB3Ka1cO_PyXvpouJfNSV3zVD";
        [Obsolete]
        private static DriveApiService driveApiService = driveApiService = new DriveApiService();
        private static IList<Google.Apis.Drive.v3.Data.File> files;

        [Obsolete]
        static void Main(string[] args)
        {
            string opt;
            do
            {
                Console.WriteLine("1.Afisare fisiere");
                Console.WriteLine("2.Incarcare fisiere");
                Console.WriteLine("0.Iesire");
                Console.WriteLine("Alege optiune:");

                opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        afisare_fisiere();
                        break;
                    case "2":
                        uploadFile();
                        break;

                }

            } while (opt != "0");
        }

        [Obsolete]
        private static void afisare_fisiere()
        {
            files = driveApiService.ListEntities(ID_DRIVE);
            Console.WriteLine("--------------------------");
            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
            }

            Console.WriteLine("--------------------------");
        }

        [Obsolete]
        private static async void uploadFile()
        {
            Console.WriteLine("Dati path-ul fisierului pentru a urca pe server:");
            string path = Console.ReadLine();
            if (path != "")
            {
                var stream = new FileStream(path, FileMode.Open);

                await driveApiService.Upload(stream, ID_DRIVE);
                Console.WriteLine("Incarcat cu succes!");
            }

        }
    }
}
