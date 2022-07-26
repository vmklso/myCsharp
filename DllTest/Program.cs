using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using addwin;


namespace DLLtest0
{

    public class Myclass
    {
        static void Main()
        {
            string line;
            int nn = 5000;
            double[] S21AT = new double[nn];
            double[] S21PT = new double[nn];
            long[] freqsT = new long[nn];
            using var reader = new StreamReader(@"1.s2p");
            
            for(int i = 0; i < 8; i++)
            {
                reader.ReadLine();
            }
            int len =0;
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(' ');
                long freq = long.Parse(items[0]);
                double S21R = double.Parse(items[3]);
                double S21I = double.Parse(items[4]);
                //Console.WriteLine("{0},{1}",S21R,S21I);
                //freqsT[len]=freq;
                //S21AT[len]=Math.Sqrt(S21R*S21R+S21I*S21I);
                //S21PT[len]= Math.Atan2(S21I,S21R);
                //.WriteLine("{0},{1}", S21AT[len], S21PT[len]);
                S21AT[len] = S21R;
                S21PT[len] = S21I / 180 * Math.PI;
                len++; 
            }
            double[] S21A = new double[len];
            double[] S21P = new double[len];
            long[] freqs = new long[len];
            for (int i = 0; i < len; i++)
            {
                S21A[i] = S21AT[i];
                S21P[i] = S21PT[i];
                freqs[i] = freqsT[i];
                
            }
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

            double [] WFdata = new double[len];
            addwin.window mydll  = new addwin.window();
            mydll.Wins(S21A, S21P);
            for (int i = 0; i < S21A.Length; i++)
            {
                Console.WriteLine("{0},{1}", S21A[i], S21P[i]);
            }

        }
    }
}