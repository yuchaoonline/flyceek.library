using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic.Lib
{
    public class KMP
    {
        public int Search(string mainStr, string subStr)
        {
            int i = 0;
            int j = 0;
 
            //计算“前缀串”和“后缀串“的next
            int[] next = GetNextVal(subStr);

            while (i < mainStr.Length && j < subStr.Length)
            {
                if (j == -1 || mainStr[i] == subStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }

            if (j == subStr.Length)
                return i - subStr.Length;
 
            return -1;
        }

        static int[] GetNextVal(string smallstr)
        {
            //前缀串起始位置("-1"是方便计算）
            int k = -1;
 
            //后缀串起始位置（"-1"是方便计算）
            int j = 0;
 
            int[] next = new int[smallstr.Length];
 
            //根据公式： j=0时，next[j]=-1
            next[j] = -1;
 
            while (j < smallstr.Length - 1)
            {
                if (k == -1 || smallstr[k] == smallstr[j])
                {
                    //pk=pj的情况: next[j+1]=k+1 => next[j+1]=next[j]+1
                    next[++j] = ++k;
                }
                else
                {
                    //pk != pj 的情况:我们递推 k=next[k];
                    //要么找到，要么k=-1中止
                    k = next[k];
                }
            }
 
            return next;
        }
    }
}
