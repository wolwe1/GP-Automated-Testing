namespace lib.common
{
    public class UserExperience
    {
        public static string GetString()
        {
            return Console.ReadLine();
        }

        public static bool Ask(string question)
        {
            Output($"{question}? Y/N");
            var answer = GetString().ToLower();

            return answer.Equals("y") || answer.Equals("yes");
        }

        public static void Output(string message)
        {
            Console.WriteLine(message);
        }

        public static string Get(string message)
        {
            Output(message);
            return GetString();
        }

        public static IEnumerable<string> GetList(string message)
        {
            return Get(message).Split(",").ToList();
        }
    }
}