using System;
using System.Collections.Generic;
using System.Text;



namespace AnalyzerObfuscator
{
    class LCSAnalyzer : TextAnalyzer
    {
        static string LCS(String s1, String s2)
        {
            string X = s1;
            string Y = s2;
            int m = s1.Length;
            int n = s2.Length;

            int[,] l = new int[m + 1, n + 1];

            int len = 0;
            int row = 0, col = 0;

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        l[i, j] = 0;

                    else if (X[i - 1] == Y[j - 1])
                    {
                        l[i, j] = l[i - 1, j - 1] + 1;
                        if (len < l[i, j])
                        {
                            len = l[i, j];
                            row = i;
                            col = j;
                        }
                    }
                    else
                        l[i, j] = 0;
                }
            }

            String resultStr = "";
            if (len == 0)
            {
                return resultStr;
            }

            while (l[row, col] != 0)
            {
                resultStr = X[row - 1] + resultStr; // or Y[col-1]
                --len;

                // move diagonally up to previous cell
                row--;
                col--;
            }
            return resultStr;
        }
        public string AnalyzeText(string text, string obfuscated)
        {
            return LCS(text, obfuscated);
        }
    }
}
