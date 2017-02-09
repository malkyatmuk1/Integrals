using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrals
{
    class Program
    {
        static void Main(string[] args)
        {
            double r;
            int m , n , a = 0, b = 1, c = 0, d = 1;
            int Rcnt;
 
            
                Console.WriteLine("Write m, n, delta, deltaR, so, to");
               m=int.Parse( Console.ReadLine());
               n = int.Parse(Console.ReadLine());
               double delta = double.Parse(Console.ReadLine());
               double deltaR = double.Parse(Console.ReadLine());
               double so = double.Parse(Console.ReadLine());
               double to = double.Parse(Console.ReadLine());
               Rcnt = Convert.ToInt32(1.0d / deltaR)+1;
               Integral integral = new Integral( a, b, c, d, n, m);
               
               double[,,] F = new double[n+1 , n+1, 2];
               double[,,] F_2 = new double[n+1, n+1, 2];
               double[,,] F_Plus = new double[n+1, n+1, 2];
               double[,,] F_2Plus = new double[n+1, n+1, 2];
               double[] Fst=new double[2];
               double[] Fst2 = new double[2];
               double[] FstPlus = new double[2];
               double[] Fst2Plus = new double[2];

               double[] A = new double[Rcnt], B = new double[Rcnt];
               double[] APlus = new double[Rcnt], BPlus = new double[Rcnt];
               int count = 0; 

                for (int v = 0; v < Rcnt ; v++)
                {
                    r = v * deltaR;
                    for (int i = 0; i <= n; i++)
                    {
                        double t = c + (2 * i - 1) * integral.D / 2.0d; 
                        for (int j = 0; j <= n; j++)
                        {
                            double s = a + (2 * j - 1) * integral.D / 2.0d;
                            F[j, i, 0] = integral.G(s, t, r);
                            F_2[j, i, 0] = integral.G(s, t, r)+delta;

                            F_Plus[j, i, 0] = integral.GPlus(s, t, r);
                            F_2Plus[j, i, 0] = integral.GPlus(s, t, r)+delta;
                        }
                    }
                    Fst[0] = integral.G(so, to, r);
                    Fst2[0] = integral.G(so, to, r)+delta;
                    FstPlus[0] = integral.GPlus(so, to, r);
                    Fst2Plus[0] = integral.GPlus(so, to, r)+delta;
                    for (int k = 0; k < m; k++)
                    {
                        int k1 = k % 2; 
                        int k2 = (k + 1) % 2;
                        
                        for (int i = 0; i <= n; i++)
                        {
                            double t = c + (2 * i - 1) * integral.D / 2.0d;
                            for (int j = 0; j <= n; j++)
                            {
                                double s = a + (2 * j - 1) * integral.D / 2.0d;
                                F[j, i, k2] = integral.G(s, t, r) + integral.f(s, t, r, F[j,i,k1])  + Math.Pow(integral.D, 2) * integral.A(s, t, F,k1);
                                F_2[j, i, k2] = integral.G(s, t, r) + integral.f(s, t, r, F_2[j, i, k1]) + Math.Pow(integral.D, 2) * integral.A(s, t, F_2, k1);

                                F_Plus[j, i, k2] = integral.GPlus(s, t, r) + integral.fPlus(s, t, r, F_Plus[j, i, k1]) + Math.Pow(integral.D, 2) * integral.A(s, t, F_Plus, k1);
                                F_2Plus[j, i, k2] = integral.GPlus(s, t, r) + integral.fPlus(s, t, r, F_2Plus[j, i, k1]) + Math.Pow(integral.D, 2) * integral.A(s, t, F_2Plus, k1);
                            }
                        }
                        Fst[k2] = integral.G(so, to, r) + integral.f(so, to, r, Fst[k1]) + Math.Pow(integral.D, 2) * integral.A(so, to, F, k1);
                        Fst2[k2] = integral.G(so, to, r) + integral.f(so, to, r, Fst2[k1]) + Math.Pow(integral.D, 2) * integral.A(so, to, F_2, k1);
                        FstPlus[k2] = integral.G(so, to, r) + integral.fPlus(so, to, r, FstPlus[k1]) + Math.Pow(integral.D, 2) * integral.A(so, to, F_Plus, k1);
                        Fst2Plus[k2] = integral.G(so, to, r) + integral.fPlus(so, to, r, Fst2Plus[k1]) + Math.Pow(integral.D, 2) * integral.A(so, to, F_2Plus, k1);
                    }
                   
                   int k3 = m % 2;
                   
                   A[count] = Math.Abs((2.0d+r)*so*to-Fst[k3]);
                   B[count] = Math.Abs(Fst[k3] - Fst2[k3]);
                   APlus[count] = Math.Abs((4.0d - r) * so * to - FstPlus[ k3]);
                   BPlus[count] = Math.Abs(FstPlus[k3] - Fst2Plus[k3]);
                   count++;
                }


                double r1=0;
                Console.WriteLine("+----+-----------+-------+-----------+-------+");
                Console.WriteLine("| r  |    F-     |  F'-  |    F+     |  F'+  |");
                Console.WriteLine("+----+-----------+-------+-----------+-------+");
                for (int i = 0; i < Rcnt; i++)
                {
                    Console.WriteLine("|" + r1.ToString("0.00") + "|" + A[i].ToString("0.000000000") + "|" + B[i].ToString("0.00E0") + "|" + APlus[i].ToString("0.000000000") + "|" + BPlus[i].ToString("0.00E0") + "|");
                    r1 = r1 + deltaR;
                }
                Console.WriteLine("+----+-----------+-------+-----------+-------+");
           
                
            }

            }
        }


