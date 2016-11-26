using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchAlgorithms
{
    class StringSearchAlgorithms
    {
        public static IEnumerable<int> Naive(string filepath, string textToFind)
        {
       
            using (var sr = File.OpenText(filepath))
            {
                var index = 0;
                var text = string.Empty;
                while ((text = sr.ReadLine()) != null)
                {
                    for (var i = 0; i < text.Length - textToFind.Length + 1; i++)
                    {
                        if (textToFind == text.Substring(i, textToFind.Length))
                            yield return index;
                        index++;
                    }

                    index += textToFind.Length;
                }
            }
        }

        public static IEnumerable<int> KnuthMorrisPratt(string filepath, string textToFind)
        {
            var index = 0;
            var lps = new int[textToFind.Length];

            var length = 0;
            var pos = 1;

            lps[0] = 0;

            while (pos < textToFind.Length)
            {
                if (textToFind[pos] == textToFind[length])
                {
                    length++;
                    lps[pos] = length;
                    pos++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[pos] = 0;
                        pos++;
                    }
                }
            }

            using (var sr = File.OpenText(filepath))
            {
                var text = string.Empty;
                while ((text = sr.ReadLine()) != null)
                {
                    var i = 0;
                    var j = 0;

                    while (i < text.Length)
                    {
                        if (textToFind[j] == text[i])
                        {
                            j++;
                            i++;
                        }
                        if (j == textToFind.Length)
                        {
                            yield return index + i - j;
                            j = lps[j - 1];
                        }
                        else if (i < text.Length && textToFind[j] != text[i])
                        {
                            if (j != 0)
                                j = lps[j - 1];
                            else
                                i = i + 1;
                        }
                    }
                    index += text.Length;
                }
            }
        }


        public static IEnumerable<int> RabinKarp(string filepath, string textToFind)
        {
            //using (var sr = File.OpenText(filepath))
            //{

            var text = File.ReadAllText(filepath);
            //while ((text = sr.ReadLine()) != null)
            //{

            if (text.Length < textToFind.Length) yield break;

            ulong siga = 0;
            ulong sigb = 0;
            ulong Q = 100007;
            ulong D = 256;
            ulong power = 1;

            for (int i = 0; i < textToFind.Length; i++)
            {
                siga = (siga * D + (ulong)text[i]) % Q;
                sigb = (sigb * D + (ulong)textToFind[i]) % Q;
            }

            if (siga == sigb)
                yield return 0;

            for (int i = 1; i <= textToFind.Length - 1; i++)
            {
                power = (power * D) % Q;
            }

            for (int i = 1; i <= text.Length - textToFind.Length; i++)
            {
                siga = (siga + Q - power * (ulong)text[i - 1] % Q) % Q;
                siga = (siga * D + (ulong)text[i + textToFind.Length - 1]) % Q;

                if (siga == sigb)
                {
                    if (text.Substring(i, textToFind.Length) == textToFind)
                        yield return i;
                }
            }
            //}
            //}

        }
    }
}
