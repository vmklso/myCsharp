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
                S21AT[len] = Math.Sqrt(S21R * S21R + S21I * S21I);
                S21PT[len] = Math.Atan2(S21I, S21R);
                //.WriteLine("{0},{1}", S21AT[len], S21PT[len]);
                //S21AT[len] = S21R;
                //S21PT[len] = S21I / 180 * Math.PI;
                len++; 
            }
            double[] S21A = new double[len];
            double[] S21A_N = new double[len];
            double[] S21P = new double[len];
            double[] S21P_N = new double[len];
            long[] freqs = new long[len];
            for (int i = 0; i < len; i++)
            {
                S21A[i] = S21AT[i];
                S21A_N[i] = 0;
                S21P[i] = S21PT[i];
                S21P_N[i] = 0;
                freqs[i] = freqsT[i];
                
            }

#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

            double [] WFdata = new double[len];
            addwin.window mydll  = new addwin.window();
            Console.WriteLine("输入加窗方法(0代表凯泽窗，1代表高斯窗)：");
            int method = 0;
            method = Convert.ToInt32(Console.ReadLine());
            while(method != 0 && method != 1)
            {
                Console.WriteLine("输入错误!请重新输入：");
                method = Convert.ToInt32(Console.ReadLine());

            }
            Console.WriteLine("输入窗参数：");
            double alpha = 0;
            alpha = Convert.ToDouble(Console.ReadLine());
            while(alpha < 0)
            {
                Console.WriteLine("输入错误!请重新输入：");
                alpha = Convert.ToDouble(Console.ReadLine());
            }
            mydll.Wins(S21A,S21P,ref S21A_N, ref S21P_N,method,alpha);
           
            for (int i = 0; i < S21A_N.Length; i++)
            {
                Console.WriteLine("{0},{1}", S21A_N[i], S21P_N[i]);
            }
            Console.ReadKey();

        }
    }
}