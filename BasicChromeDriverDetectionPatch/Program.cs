using System.Text;

namespace BasicChromeDriverDetectionPatch
{
    internal class Program
    {
        static byte[]? Data;

        static void Main(string[] args)
        {
            Console.Title =
                "Basic ChromeDriver Detection Patch - Plot <github.com/Plot1337>";

            if (args.Length == 0)
                ErrorExit(
                    "Usage: [current exe] [path to chromedriver.exe]"
                    );

            string path = args[0];

            try
            {
                Console.WriteLine($"Attempting to load... ({path})");
                Data = File.ReadAllBytes(path);

                Console.WriteLine("Loaded EXE bin into memory!");

                var ids = GetCdcIds();

                if (ids.Count == 0)
                    ErrorExit("No IDs found! Perhaps the EXE is already patched?");

                Console.WriteLine($"Found {ids.Count} IDs! Replacing with alts...");

                byte[] AsciiStr(string str) =>
                    Encoding.ASCII.GetBytes(str);

                foreach (var id in ids)
                {
                    string alt = RandomString();

                    Data = BinHelper.Replace(
                        Data,
                        AsciiStr(id),
                        AsciiStr(alt)
                        );

                    Console.WriteLine($"{id} -> {alt}");
                }

                string patchedFilePath = path.Replace(".exe", "_patched.exe");

                File.WriteAllBytes(patchedFilePath, Data);
                Console.WriteLine("Saved to: " + patchedFilePath);
            }
            catch (FileNotFoundException) { ErrorExit("Chromedriver EXE not found!"); }
            catch (Exception ex) { ErrorExit(ex.ToString()); }

            Exit();
        }

        static List<string> GetCdcIds()
        {
            Console.WriteLine("Finding CDC ids...");

            if (Data == null)
                throw new ArgumentNullException(nameof(Data));

            int idLen = 26;
            var result = new List<string>();

            // "cdc_" as bytes
            byte[] searchArr = {
                0x63, 0x64, 0x63, 0x5F
            };

            var offsets = BinHelper.FindOffsets(Data, searchArr);

            foreach (var offset in offsets)
            {
                var idBytes = Data.Skip(offset).Take(idLen).ToArray();
                string id = Encoding.UTF8.GetString(idBytes);

                result.Add(id);
                Console.WriteLine($"{offset}: {id}");
            }

            return result.Distinct().ToList();
        }

        static string RandomString(int length = 26)
        {
            var random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(
                Enumerable.Repeat(chars, length).Select(
                    i => i[random.Next(i.Length)]
                    ).ToArray()
                );
        }

        static void ErrorExit(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;

            Exit();
        }

        static void Exit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
