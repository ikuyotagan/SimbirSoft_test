using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
 
namespace SimbirSoftTest
{
    public static class stringWords
    {
        private static string GetHtmlPageText(string url)
        {
            WebClient client = new WebClient();
            using (Stream data = client.OpenRead(url))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static Dictionary<string, int> GetDictionary(string url)
        {
            string data = GetHtmlPageText(url);
            data = Regex.Replace(data, @"<style[^>]*>[\s\S]*?</style>", string.Empty);
            data = Regex.Replace(data, @"<script[^>]*>[\s\S]*?</script>", string.Empty);
            data = Regex.Replace(data, @"\[[\s\S]*?\]", string.Empty);
            data = Regex.Replace(data, @"\-[\s\S]*?\-", string.Empty);
            data = Regex.Replace(data, "<[^>]+>", string.Empty);
            data = Regex.Replace(data, @"[\(\.\,\:\;\-\!\?\'\$\&\%\№\#\<\>\«\»\)]", " ");
            data = Regex.Replace(data, "\"", string.Empty);
            data = Regex.Replace(data, @"\d", string.Empty);
            data = Regex.Replace(data, @"\s+", " ");
            string[] word = data.Split(' ');
            Dictionary<string, int> words = new Dictionary<string, int>();
            foreach (var wordd in word)
            {
                if (words.ContainsKey(wordd))
                    words[wordd]++;
                else
                    words.Add(wordd, 1);
            }

            return words;
        }
    }
    public static class Program
    {
        private static void Main()
        {
            Dictionary<string, int> words = stringWords.GetDictionary("https://www.simbirsoft.com/");

            foreach (var pair in words)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        }
    }
}