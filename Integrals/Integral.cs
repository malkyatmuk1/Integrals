using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrals
{
    class Integral
    {
        
        public  int a { get; set; }
        public  int b { get; set; }
        public  int c { get; set; }
        public  int d { get; set; }
        public  int n { get; set; }
        public  int m { get; set; }
        public double D { get { return (double) (b - a) / n;}  }


        public Integral( int a, int b, int c, int d,int n, int m)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.n = n;
            this.m = m;
        }
        public  double G(double s, double t, double r)
        {
           
            
                double g = 0.75d * (2.0d + r) * s * t - 0.125d * Math.Pow((2.0d + r), 2) * s * t;
            
            return g;
        }
        public double GPlus(double s, double t, double r)
        {
            double g = 0.75d * (4.0d - r) * s * t - 0.125d * Math.Pow((4.0d - r), 2) * s * t;
            return g;
        }
      
      
        public  double f(double s, double t, double r, double F)
        {
            double f = F / 4.0d + 0.0625d * Math.Pow((2.0d + r), 2) * s * t;
            return f;
        }
        public double fPlus(double s, double t, double r, double F)
        {
            double f = F / 4.0d + 0.0625d * Math.Pow((4.0d- r), 2) * s * t;
            return f;
        }
         public  double A(double s,double t,  double[,,] F,int k)
        {
            double Asum = 0.0d;
            for (int i = 0; i <= n; i++)
            {
                double eta = c + (2 * i - 1) * D / 2.0d;
                for (int j = 0; j <= n ; j++)
			    {
                    double ksi = a + (2 * j - 1) * D / 2.0d;
                    Asum = Asum +  ksi * eta * Math.Pow(F[j, i, k], 2);		 
			    }
            }
            return Asum*s*t;
        }
     

    }
}
