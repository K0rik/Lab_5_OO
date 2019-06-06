using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{

    class MatX
    {
        public int n = 4;
        public double K1 = 0.05;
        public double K2 = 0.05;

        public double[,] B2 = { { 2, 3, 3, 4 }, { 4, 3, 2, 3 }, { 1, 3, 3, 3 }, { 2, 2, 1, 2 } },
            A2 = { { 1, 1, 1, 1 }, { 3, 4, 1, 4 }, { 4, 4, 3, 2 }, { 1, 3, 2, 2 } },
            A1 = { { 2, 4, 4, 3 }, { 3, 4, 4, 1 }, { 2, 3, 4, 4 }, { 2, 2, 3, 4 } },
            A  = { { 2, 4, 4, 3 }, { 1, 3, 4, 1 }, { 3, 3, 1, 4 }, { 3, 3, 4, 4 } },
            b1 = { { 2, 0, 0, 0 }, { 2, 0, 0, 0 }, { 1, 0, 0, 0 }, { 4, 0, 0, 0 } },
            c1 = { { 1, 0, 0, 0 }, { 4, 0, 0, 0 }, { 3, 0, 0, 0 }, { 1, 0, 0, 0 } };

        public double[,] C2, Y3;
        public double[,] b, y1;
        public double[,] y2;
        public double[,] first;
        public double[,] second;
        public double[,] third;
        public double[,] X;

        public MatX()
        {
            this.b = new double[n, n];
            this.C2 = new double[n, n];
            this.y1 = new double[n, n];
            this.y2 = new double[n, n];
            this.Y3 = new double[n, n];

            this.first = new double[n, n];
            this.second = new double[n, n];
            this.third = new double[n, n];
            this.X = new double[n, n];
        }

        public void m_Y3()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = 1 / Math.Pow((i + 1 +j + 1),3);  
                    Y3[i, j] = A2[i, j] * (10*B2[i, j] + C2[i, j]);
                }
            }
        }
        public void m_b()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                        b[i, j] = 6/Math.Pow(i+1, 2);
                }
            }
        }
        public void m_y1()
        {
            for (int i = 0; i < n; i++)
            {
                    y1[i, 0] = A[i, 0] * b[i, 0];
            }
        }
        public void m_y2()
        {
            for (int i = 0; i < n; i++)
            {
                    y2[i, 0] = A1[i, 0] * (6 * b1[i, 0] - c1[i, 0]);
            }
        }

        public void f()
        {
            for (int i = 0; i < n; i++)
            {
                first[i, 0] = y2[i, 0];
            }
        }
        public void s()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    second[i, j] = K2 * y1[i, 0] * y2[i, 0]*Y3[i,j];
                }
            }
        } 

        public void t()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    third[i, j] = K2 * y2[i,0]*Y3[i,j]*y1[i,0]*Y3[i,j];
                }
            }
        }
        
        public void x()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    X[i, j] = first[i, 0] + second[i, j] + third[i,j];
                }
            }
        }
        public void Output()
        {
            Console.WriteLine("B2: ");
            Print(B2);
            Console.WriteLine("A2: ");
            Print(A2);
            Console.WriteLine("A1: ");
            Print(A1);
            Console.WriteLine("A: ");
            Print(A);
            Console.WriteLine("b1: ");
            Print(b1);
            Console.WriteLine("c1: ");
            Print(c1);

            Console.WriteLine("Result: ");

            Console.WriteLine("Y3: ");
            Print(Y3);
            Console.WriteLine("b: ");
            Print(b);
            Console.WriteLine("y1: ");
            Print(y1);
            Console.WriteLine("y2: ");
            Print(y2);
            Console.WriteLine("f: ");
            Print(first);
            Console.WriteLine("s: ");
            Print(second);
            
            
            Console.WriteLine("x: ");
            Print(X);
            void Print(double[,] arr)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(arr[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            MatX matrix = new MatX();

            Task t_m_Y3 = new Task(matrix.m_Y3);
            Task t_m_b = new Task(matrix.m_b);
            Task t_m_y1 = new Task(matrix.m_y1);
            Task t_m_y2 = new Task(matrix.m_y2);
            Task t_m_f = new Task(matrix.f);
            Task t_m_s = new Task(matrix.s);
            Task t_m_t = new Task(matrix.t);
            Task t_m_x = new Task(matrix.x);

            t_m_Y3.Start();
            t_m_b.Start();
            t_m_y1.Start();
            t_m_y2.Start();

            Task.WaitAny(t_m_Y3);
            Task.WaitAny(t_m_b);
            Task.WaitAny(t_m_y1);
            Task.WaitAny(t_m_y2);
  
   
            t_m_f.Start();
            t_m_s.Start();
            t_m_t.Start();

            Task.WaitAny(t_m_f);
            Task.WaitAny(t_m_s);
            Task.WaitAny(t_m_t);


            t_m_x.Start();



            Task.WaitAny(t_m_x);



            matrix.Output();

            Console.ReadKey();
        }
    }
}
