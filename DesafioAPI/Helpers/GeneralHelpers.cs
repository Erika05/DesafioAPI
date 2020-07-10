using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Helpers
{
    public class GeneralHelpers
    {
        public static string FormatJson(string str)
        {
            string INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            string x = sb.ToString();
            return sb.ToString();
        }

        public static void EnsureDirectoryExists(string fullReportFilePath)
        {
            var directory = Path.GetDirectoryName(fullReportFilePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        public static string ReturnProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
        }

        public static IEnumerable ReturnCSVData(string csvPath)
        {
            using (StreamReader sr = new StreamReader(csvPath, System.Text.Encoding.GetEncoding(1252)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ArrayList result = new ArrayList();
                    result.AddRange(line.Split(';'));
                    yield return result;
                }
            }
        }
        public static bool VerificaSeStringEstaContidaNaLista(List<string> lista, string p_string)
        {
            foreach (string item in lista)
            {
                if (item.Equals(p_string))
                {
                    return true;
                }
            }
            return false;
        }

        public static int RetornaNumeroDeObjetosDoJson(JArray json)
        {
            return json.Count;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodNameByLevel(int level)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(level);
            return sf.GetMethod().Name;
        }

        public static bool IsAJsonArray(string json)
        {
            if (json.StartsWith("["))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> ObterListaResponse(IRestResponse response, string nomeVetorPai, string nomeVetorFilho, string valorChave)
        {
            var jsonString = response.Content;

            var twitterObject = JToken.Parse(jsonString);
            var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == nomeVetorPai).Value;

            List<string> listaResponse = new List<string>();

            foreach (var item in trendsArray.Children())
            {
                var itemProperties = item.Children<JProperty>();

                var tracks = itemProperties.FirstOrDefault(x => x.Name == nomeVetorFilho).Value;
                var itemTracks = tracks.Children<JProperty>();
                listaResponse.Add(itemTracks.FirstOrDefault(x => x.Name == valorChave).Value.ToString());
            }
            return listaResponse;
        }

        public static List<string> ObterListaResponse(IRestResponse response, string nomeVetorPai, string valorChave)
        {
            var jsonString = response.Content;

            var twitterObject = JToken.Parse(jsonString);
            var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == nomeVetorPai).Value;

            List<string> listaResponse = new List<string>();

            foreach (var item in trendsArray.Children())
            {
                var itemProperties = item.Children<JProperty>();

                listaResponse.Add(itemProperties.FirstOrDefault(x => x.Name == valorChave).Value.ToString());
            }
            return listaResponse;
        }

        public static String ReturnEmailRandomico(int tamanho)
        {
            return ReturnStringWithRandomCharacters(tamanho) + "@testdata.com";
        }

        public static string ReturnStringWithRandomCharacters(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}

static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
    {
        foreach (var i in ie)
        {
            action(i);
        }
    }
}