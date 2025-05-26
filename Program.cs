namespace sorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the file location: ");
            string fileLocation = Console.ReadLine();
            var sorted = LogAnalyzer(fileLocation);
            PrintSortedReport(sorted);

            Console.ReadLine();
        }

        public static void PrintSortedReport(Dictionary<string, List<string>> logs)
        {
            foreach (var log in logs)
            {
                Console.WriteLine($"\n {log.Key} ");
                foreach (var line in log.Value)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static Dictionary<string, List<string>> LogAnalyzer(string path)
        {
            var result = new Dictionary<string, List<string>>()
            {
                { "ERROR", new List<string>() },
                { "WARNING", new List<string> () },
                { "INFO", new List<string> () }
            };

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("The file does not exist");
                    return result;
                }

                if (Path.GetExtension(path).ToLower() != ".txt")
                {
                    Console.WriteLine("File format incorrect. The file must be .txt");
                    return result;
                }

                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    if (line.Contains("[ERROR]"))
                    {
                        result["ERROR"].Add(line);
                    }
                    else if (line.Contains("[WARNING]"))
                    {
                        result["WARNING"].Add(line);
                    }
                    else if (line.Contains("[INFO]"))
                    {
                        result["INFO"].Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            return result;
        }

    }
}
