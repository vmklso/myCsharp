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
            Complex[] Ttdata = new Complex[len];
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
            int ca = 0;
            Console.WriteLine("选择加窗类型");
            ca = Convert.ToInt32(Console.ReadLine());
            int N =0;
            double[] window = new double[len];
            Kaiser mykaiser = new Kaiser();
            switch (ca)
            {
                case 0://凯泽窗
                    
                    int alpha = 6;
                    Console.WriteLine("输入凯泽窗参数：");
                    alpha = Convert.ToInt32(Console.ReadLine());
                    if (maxPoint >= len / 2)
                    {
                        N = 2 * maxPoint;
                        double[] windowK = new double[N];
                        mykaiser.BulidWindow(windowK, alpha);
                        for (int i = 0; i < len; i++)
                        {
                            window[i] = windowK[i];
                        }
                    }
                    else
                    {
                        N = 2 * (len - maxPoint);
                        double[] windowK = new double[N];
                        mykaiser.BulidWindow(windowK, alpha);
                        for (int j = len; j > 0; --j, --N)
                        {
                            window[j-1] = windowK[N-1];
                        }
                    }
                    break;
                case 1:
                    {
                        double alpha1 = 100;
                        Console.WriteLine("输入高斯窗参数：");
                        alpha1 = Convert.ToDouble(Console.ReadLine());
                        if (maxPoint >= len / 2)
                        {
                            N = 2 * maxPoint;
                            double[] windowK = new double[N];
                            mykaiser.Gausswin(windowK, alpha1);
                            for (int i = 0; i < len; i++)
                            {
                                window[i] = windowK[i];
                            }
                        }
                        else
                        {
                            N = 2 * (len - maxPoint);
                            double[] windowK = new double[N];
                            mykaiser.Gausswin(windowK, alpha1);
                            for (int j = len; j > 0; --j, --N)
                            {
                                window[j-1] = windowK[N-1];
                            }
                        }
                        break;

                    }
            }
            
            for(int i =0;i<len;i++)
            {
                Ttdata[i] =new Complex(Tdata[i].Real * window[i], Tdata[i].Imaginary * window[i]);
                //Console.WriteLine(Ttdata[i].Real.ToString() + "," + Ttdata[i].Imaginary.ToString());
                WFdata[i] = Ttdata[i];
            }
            //WFdata = Ttdata;
            myfft.InitW(W, len);
            myfft.FFTfrequency(len, WFdata, W);
            //for (int i = 0; i < len; i++)
            //{
            //    Console.WriteLine(WFdata[i].Real.ToString() + "," + WFdata[i].Imaginary.ToString());
            //}
            for (int i = 0;i<S21A.Length;i++)
            {
                S21A[i] = WFdata[i].GetModul();
                S21P[i] = WFdata[i].GetAngle();
            }


        }
    }
}
