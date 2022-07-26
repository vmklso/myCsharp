using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addwin
{
    internal class Kaiser
    {
        public void BulidWindow(double[] win,double shape)
        {
            double oneOverDenom = 1.0 / ZeroethOrderBessel(shape);
            UInt32 N = (UInt32)(win.Length - 1);
            double oneOverN = 1.0 / N;
            for (UInt32 n = 0; n <= N; ++n)
            {
                double K = (2.0 * n * oneOverN) - 1.0;
                double arg = Math.Sqrt(1 - (K * K));
                
                win[n] = ZeroethOrderBessel(shape * arg) *oneOverDenom;
            }
        }
        

        private static double ZeroethOrderBessel(double x)
        {
            const double eps = 0.000001;
            double besselValue = 0;
            double term = 1;
            double m = 0;
            while(term>eps*besselValue)
            {
                besselValue += term;
                ++m;
                term *=(x*x)/(4*m*m);
            }
            return besselValue;
        }
        public void Gausswin(double[] win, double alpha)
        {
            int halfLen = win.Length / 2;
            for (int i = halfLen, j = 1; i < win.Length; i++,j++)
            {
                win[i] = Math.Exp(-0.5 * Math.Pow(2 * alpha * j / (win.Length - 1), 2));
                win[halfLen - j] = win[i];
            }
            //for(int i = 0; i < win.Length; i++)
            //{
            //    Console.WriteLine(win[i]);
            //}
        }
    }
}
