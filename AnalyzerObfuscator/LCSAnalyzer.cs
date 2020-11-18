using System;
using System.Collections.Generic;
using System.Text;



namespace AnalyzerObfuscator
{
    class LCSAnalyzer : TextAnalyzer
    {
        static int LCS(string str1, string str2)
        {
            int[,] l = new int[str1.Length, str2.Length];
            int lcs = -1;
            int end = -1;

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] == str2[j])
                    {
                        if (i == 0 || j == 0)
                        {
                            l[i, j] = 1;
                        }
                        else
                            l[i, j] = l[i - 1, j - 1] + 1;
                        if (l[i, j] > lcs)
                        {
                            lcs = l[i, j];
                            end = i;
                        }

                    }
                    else
                        l[i, j] = 0;
                }
            }

            return lcs;
        }
        public List<(string, string)> AnalyzeText(string text, string obfuscated)
        {
            return new List<(string, string)>() { ("LCS length", LCS(text, obfuscated).ToString()) };
        }
    }
}
