using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace addwin
{
    public class Class1
    {
     
        public void  Wins(double[] S21A, double[] S21P)
        {
            //使得数据长度为2的n次方
            int len = S21A.Length;
            for (int i = 0; i < 30; i++)
            {
                if(len<=Math.Pow(2,i))
                {
                    len = (int)Math.Pow(2, i);
                    break;
                }
            }
            //Console.WriteLine(len);
            Complex[] S21 = new Complex[len];
            for(int i = 0; i < S21A.Length; i++)
            {
                S21[i] =  new Complex(S21A[i] * Math.Cos(S21P[i]), S21A[i] * Math.Sin(S21P[i]));
                //Console.WriteLine("{0},{1}", S21[i].Real, S21[i].Imaginary);
            }
            for(int j = S21A.Length-1; j < len; j++)
            {
                S21[j] = new Complex(0, 0);
                //Console.WriteLine("{0},{1}", S21[j].Real, S21[j].Imaginary);
            }

            FFT myfft = new FFT();
            Complex[] Tdata = new Complex[len];
            Complex[] W = new Complex[len];
            Complex[] Fdata = new Complex[len];
            Fdata =S21;
            Complex[] WTdata = new Complex[len];
            Complex[] WFdata = new Complex[len];
            myfft.InitW(W, len);
            Complex myComplex = new Complex();
            myComplex.Copy(Fdata, Tdata, len) ;
            myfft.IFFTfrequency(len, Tdata, W);
            //此时Tdata 中包含了2048个时域数据点
            //下面对时域数据进行加窗
            //找到最大值
            int maxPoint = 0;// store the location of maxvalue
            double maxValue = Tdata[0].GetModul();// store the maxValure;
            for(int i = 0; i < len; i++)
            {
                if (Tdata[i].GetModul() > maxValue)
                {
                    maxValue = Tdata[i].GetModul();
                    maxPoint = i;
                }
            }
            //Console.WriteLine("{0},{1}",maxValue,maxPoint);
            //N 代表窗的长度；
            int N =0;
            if(maxPoint >= len/2)
            {
                N = 2 * maxPoint;
                double[] windowK = new double[len];
                Kaiser(6, windowK);
            }
            else
            {
                N = 2*(len-maxPoint);
            }

        }
        public int N_jiecheng(int n)
        {
            int sum = 1;
            for(int i =1;i <= n;i++)
            {
                sum *= i;
                //Console.WriteLine(sum);
            }
            return sum;
        }
        //零阶第一类修正贝塞尔函数，n一般选择20
        public double I0(int n, double x)
        {
            double I0_x = 1.0;

            for (int i =1;i<=n;i++)
            {
                I0_x += Math.Pow((Math.Pow(x / 2, i) / N_jiecheng(i)), 2);
                //Console.WriteLine(I0_x);
            }
            return I0_x; ;
        }
        public void Kaiser(double beta,double[] KaiserWin)
        {
            int length = KaiserWin.Length;
            double temp = I0(20, beta);
            for (int j =0; j < length; j++)
            {

                KaiserWin[j] = I0(20, (double)(beta * Math.Sqrt(1 - Math.Pow(2 * (double)j / (length - 1) - 1, 2)))) / temp;
                Console.WriteLine(KaiserWin[j]);
            }
        }
    }
}
