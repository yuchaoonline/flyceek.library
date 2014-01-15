using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic.Lib
{
    public class BF
    {
        public int Search(string mainStr, string subStr, int pos)
        {
            int i = pos, j = 0;
            while (i < mainStr.Length && j < subStr.Length)
            {
                if (mainStr[i] == subStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    i = i - j + 1;
                    j = 0;
                }
            }
            if (j == subStr.Length) return i - subStr.Length + 1;
            else return 0;
        }
    }
}
