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
            int Rcnt = 0 ;
            int l=0;
           
            double deltaR = 0.1;
            double[,] A, B, APlus, BPlus;
            Rcnt = Convert.ToInt32(1.0d / deltaR) + 1;
            A = new double[Rcnt, 4];
            B = new double[Rcnt, 4];

            APlus = new double[Rcnt, 4];
            BPlus = new double[Rcnt, 4];
            while(l!=4)
            {
                Console.WriteLine("Write m, n, delta, deltaR, so, to");
               m=int.Parse( Console.ReadLine());
               n = int.Parse(Console.ReadLine());
               double delta = double.Parse(Console.ReadLine());
                deltaR = double.Parse(Console.ReadLine());
               double so = double.Parse(Console.ReadLine());
               double to = double.Parse(Console.ReadLine());
               
               Integral integral = new Integral( a, b, c, d, n, m);
               
               double[,,] F = new double[n+1 , n+1, 2];
               double[,,] F_2 = new double[n+1, n+1, 2];
               double[,,] F_Plus = new double[n+1, n+1, 2];
               double[,,] F_2Plus = new double[n+1, n+1, 2];
               double[] Fst=new double[2];
               double[] Fst2 = new double[2];
               double[] FstPlus = new double[2];
               double[] Fst2Plus = new double[2];

               
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
                        //tuk se pravqt sumite
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
                   
                   A[count,l] = Math.Abs((2.0d+r)*so*to-Fst[k3]);
                   B[count,l] = Math.Abs(Fst[k3] - Fst2[k3]);
                   APlus[count,l] = Math.Abs((4.0d - r) * so * to - FstPlus[ k3]);
                   BPlus[count,l] = Math.Abs(FstPlus[k3] - Fst2Plus[k3]);
                   count++;
                }
                l++;
            }
            using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@"..\..\tables.txt"))
            {
                
                file.WriteLine(@"\begin{table}");
                file.WriteLine(@"\caption{Numerical results}");
                file.WriteLine(@"\label{tab:1}       % Give a unique label");
                file.WriteLine("%");
                file.WriteLine("% Follow this input for your own table layout");
                file.WriteLine("%");
                file.WriteLine("\begin{tabular}{p{2cm}p{2.4cm}p{2cm}p{4.9cm}}");
                file.WriteLine(@"\hline\noalign{\smallskip}");
                file.WriteLine(@" r & \multicolumn{2}{c}{m=10 n=10} & \multicolumn{2}{c}{m=20 n=10} & \ multicolumn{2}{c}{m=10 n=30} & multicolumn{2}{c}{m=20 n=30} &   \midrule");
                file.WriteLine(@"{} & e+ e-  & e+ e-    & e+ e-   & e+ e-\");
                file.WriteLine(@"\noalign{\smallskip}\svhline\noalign{\smallskip}");
                double r2 = 0;

                for (int i = 0; i < Rcnt; i++)
                {
                    file.WriteLine(r2.ToString("0.00") + " & " + A[i, 0].ToString("0.000000000") + " & " + APlus[i, 0].ToString("0.000000000") + @"\ " + A[i, 1].ToString("0.000000000") + " & " + APlus[i, 1].ToString("0.000000000") + @"\ " + A[i, 2].ToString("0.000000000") + " & " + APlus[i, 2].ToString("0.000000000") + @"\ " + A[i, 3].ToString("0.000000000") + " & " + APlus[i, 3].ToString("0.000000000") + @"\");
                    r2 = r2 + deltaR;
                }


                file.WriteLine(@"\noalign{\smallskip}\hline\noalign{\smallskip}");
                file.WriteLine(@"\end{tabular}");
                file.WriteLine("$^a$ Table foot note (with superscript)");
                file.WriteLine(@"\end{table}");
                file.WriteLine();
                //for d
                file.WriteLine(@"\begin{table}");
                file.WriteLine(@"\caption{Numerical results}");
                file.WriteLine(@"\label{tab:1}       % Give a unique label");
                file.WriteLine("%");
                file.WriteLine("% Follow this input for your own table layout");
                file.WriteLine("%");
                file.WriteLine("\begin{tabular}{p{2cm}p{2.4cm}p{2cm}p{4.9cm}}");
                file.WriteLine(@"\hline\noalign{\smallskip}");
                file.WriteLine(@" r & \multicolumn{2}{c}{m=10 n=10} & \multicolumn{2}{c}{m=20 n=10} & \ multicolumn{2}{c}{m=10 n=30} & multicolumn{2}{c}{m=20 n=30} &   \midrule");
                file.WriteLine(@"{} & d+ d-  & d+ d-    & d+ d-   & d+ d-\");
                file.WriteLine(@"\noalign{\smallskip}\svhline\noalign{\smallskip}");
                double r3 = 0;

                for (int i = 0; i < Rcnt; i++)
                {
                    file.WriteLine(r2.ToString("0.00") + " & " + B[i, 0].ToString("0.000000000") + " & " + BPlus[i, 0].ToString("0.000000000") + @"\ " + B[i, 1].ToString("0.000000000") + " & " + BPlus[i, 1].ToString("0.000000000") + @"\ " + B[i, 2].ToString("0.000000000") + " & " + BPlus[i, 2].ToString("0.000000000") + @"\ " + B[i, 3].ToString("0.000000000") + " & " + BPlus[i, 3].ToString("0.000000000") + @"\");
                    r3 = r3 + deltaR;
                }


                file.WriteLine(@"\noalign{\smallskip}\hline\noalign{\smallskip}");
                file.WriteLine(@"\end{tabular}");
                file.WriteLine("$^a$ Table foot note (with superscript)");
                file.WriteLine(@"\end{table}");
            } 
            }

            }
        }



/*
4
4
0.1
0.1
0.5
0.5
10
10
0.1
0.1
0.5
0.5
20
10
0.1
0.1
0.5
0.5
10
30
0.1
0.1
0.5
0.5
20
30
0.1
0.1
0.5
0.5
  */










