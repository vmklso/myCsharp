using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addwin
{
    public class FFT
    {
        public void InitW(Complex[] W,int len)
        {
            for (int i = 0; i < len; i++)
            {
                W[i] = new Complex(0, 0);
                W[i].Real = Math.Cos(2*Math.PI*i/len);
                W[i].Imaginary = -Math.Sin(2*Math.PI*i/len);
            }
        }
        public void Change(int len, Complex[] Sdata)
        {
            Complex temp;
            uint i=0,j=0,k=0;
            double t;
            for(i=0; i<len; i++)
            {
                k = i;
                j = 0;
                t = (Math.Log(len)/Math.Log(2));
                while((t--)>0)
                {
                    j = j << 1;
                    j |= (k & 1);
                    k =k >> 1;
                }
                if(j>i)
                {
                    temp = Sdata[i];
                    Sdata[i] = Sdata[j];
                    Sdata[j] = temp;
                }
            }
        }
        public void FFTfrequency(int len, Complex[] Sdata, Complex[] W)
        {
            int i = 0, j = 0, k = 0, l = 0;
            Complex up, down, product;
            Change(len,Sdata);

            for(i =0; i<(int)(Math.Log10(len)/Math.Log10(2)); i++)
            {
                l = 1 << i;
                for(j = 0; j<len; j += 2 * l)
                {
                    for(k =0;k<l;k++)
                    {
                        product = Sdata[j + k + l].Divi(W[len * k / 2 / l]);
                        up = Sdata[j + k].Add(product);
                        down = Sdata[j + k].Substract(product);

                        Sdata[j + k] = up;
                        Sdata[j + k + l] = down;
                    }
                }
            }
        }
        public void IFFTfrequency(int len , Complex[] Sdata, Complex[] W)
        {
            
            int i = 0, j = 0, k = 0, l = len;
            Complex up, down;
            for(i =0; i<(int)(Math.Log(len)/Math.Log(2));i++)
            {
                l /= 2;
                for (j = 0; j < len; j += 2 * l)
                {
                    for(k=0; k<l;k++)
                    {
                        up = Sdata[(j + k)].Add(Sdata[j + k + l]);
                        //Console.WriteLine("{0:G8},{1:G8},{2}", Sdata[j+k].Real, Sdata[j+k].Imaginary, i);
                        up.Real /= 2;
                        up.Imaginary /= 2;
                        down = Sdata[(j + k)].Substract(Sdata[j + k + l]);
                        down.Real /= 2;
                        down.Imaginary /= 2;
                        down = down.Divi(W[len*k/2/l]);
                        Sdata[j + k] = up;
                        Sdata[(j + k + l)] = down;
                        //Console.WriteLine("{0:G8},{1:G8},{2}", up.Real, up.Imaginary, i);

                    }
                }
            }
            Change(len, Sdata);
        }
    }
}
