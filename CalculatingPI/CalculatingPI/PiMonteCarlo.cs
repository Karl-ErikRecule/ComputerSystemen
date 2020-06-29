using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatingPI
{
    class PiMonteCarlo : CalculatePI
    {

        public PiMonteCarlo(uint set_Iterations) : base(set_Iterations)
        {

        }

        private double euclideanDistance(double x1, double y1, double x2, double y2)
        {
            double dX = x2 - x1;
            double dY = y2 - y1;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        public decimal CalculatePiMonteCarlo(uint begin, uint end)
        {

            decimal c = 0;
            double x = 0.0, y = 0.0;
            decimal result = 0;

            Random rand = new Random();
            for (uint i = begin; i <= end; i++)
            {
                x = rand.NextDouble();
                y = rand.NextDouble();
                if (euclideanDistance(x, y, 0, 0) <= 1)
                    c++;
                result = (decimal)4.0 * (c / m_NrOfIterations);
            }

            return result;
        }


        public Tuple<decimal, decimal, Int32> PiMonteCarloThreadOne()
        {
            // one thread => main thread
            DateTime start = DateTime.UtcNow;
            decimal pi = CalculatePiMonteCarlo(1, m_NrOfIterations);
            DateTime end = DateTime.UtcNow;

            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiMonteCarloThreadTwo()
        {
            DateTime start = DateTime.UtcNow;

            // two threads => main thread and one extra task
            uint part1Begin = 1;
            uint part1End = m_NrOfIterations / 2;

            uint part2Begin = (m_NrOfIterations / 2) + 1;
            uint part2End = m_NrOfIterations;

            decimal valuePart1 = 0;
            decimal valuePart2 = 0;


            Task t1 = new Task(() => valuePart1 = CalculatePiMonteCarlo(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = CalculatePiMonteCarlo(part2Begin, part2End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);
            DateTime endpar = DateTime.UtcNow;

            // 
            decimal pi = valuePart1 + valuePart2;

            DateTime end = DateTime.UtcNow;

            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timeparralleldiff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiMonteCarloThreadFour()
        {

            DateTime start = DateTime.UtcNow;

            // two threads => main thread and one extra task
            uint part1Begin = 1;
            uint part1End = m_NrOfIterations / 4;

            uint part2Begin = (m_NrOfIterations / 4) + 1;
            uint part2End = m_NrOfIterations / 2;

            uint part3Begin = (m_NrOfIterations / 2) + 1;
            uint part3End = m_NrOfIterations * 3 / 4;

            uint part4Begin = (m_NrOfIterations * 3 / 4) + 1;
            uint part4End = m_NrOfIterations;


            decimal valuePart1 = 0;
            decimal valuePart2 = 0;
            decimal valuePart3 = 0;
            decimal valuePart4 = 0;



            Task t1 = new Task(() => valuePart1 = CalculatePiMonteCarlo(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = CalculatePiMonteCarlo(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = CalculatePiMonteCarlo(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = CalculatePiMonteCarlo(part4Begin, part4End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Task.WaitAll(t1, t2, t3, t4);
            DateTime endpar = DateTime.UtcNow;

            // + 3 door de formule, indien de 3 in de Nilakantha_series_formula staat dan doet hij 2x +3 en niet 1x
            decimal pi = valuePart1 + valuePart2 + valuePart3 + valuePart4;

            DateTime end = DateTime.UtcNow;

            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;

            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timeparralleldiff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiMonteCarloThreadEight()
        {

            DateTime start = DateTime.UtcNow;

            // two threads => main thread and one extra task
            uint part1Begin = 1;
            uint part1End = m_NrOfIterations / 8;

            uint part2Begin = (m_NrOfIterations / 8) + 1;
            uint part2End = m_NrOfIterations / 4;

            uint part3Begin = (m_NrOfIterations / 4) + 1;
            uint part3End = m_NrOfIterations * 3 / 8;

            uint part4Begin = (m_NrOfIterations * 3 / 8) + 1;
            uint part4End = m_NrOfIterations / 2;

            uint part5Begin = m_NrOfIterations / 2 + 1;
            uint part5End = m_NrOfIterations * 5 / 8;

            uint part6Begin = (m_NrOfIterations * 5 / 8) + 1;
            uint part6End = m_NrOfIterations * 3 / 4;

            uint part7Begin = (m_NrOfIterations * 3 / 4) + 1;
            uint part7End = m_NrOfIterations * 7 / 8;

            uint part8Begin = (m_NrOfIterations * 7 / 8) + 1;
            uint part8End = m_NrOfIterations;


            decimal valuePart1 = 0;
            decimal valuePart2 = 0;
            decimal valuePart3 = 0;
            decimal valuePart4 = 0;
            decimal valuePart5 = 0;
            decimal valuePart6 = 0;
            decimal valuePart7 = 0;
            decimal valuePart8 = 0;



            Task t1 = new Task(() => valuePart1 = CalculatePiMonteCarlo(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = CalculatePiMonteCarlo(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = CalculatePiMonteCarlo(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = CalculatePiMonteCarlo(part4Begin, part4End));
            Task t5 = new Task(() => valuePart5 = CalculatePiMonteCarlo(part5Begin, part5End));
            Task t6 = new Task(() => valuePart6 = CalculatePiMonteCarlo(part6Begin, part6End));
            Task t7 = new Task(() => valuePart7 = CalculatePiMonteCarlo(part7Begin, part7End));
            Task t8 = new Task(() => valuePart8 = CalculatePiMonteCarlo(part8Begin, part8End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();

            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7, t8);
            DateTime endpar = DateTime.UtcNow;

            // + 3 door de formule, indien de 3 in de Nilakantha_series_formula staat dan doet hij 2x +3 en niet 1x
            decimal pi = valuePart1 + valuePart2 + valuePart3 + valuePart4 + valuePart5 + valuePart6 + valuePart7 + valuePart8;

            DateTime end = DateTime.UtcNow;

            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timeparralleldiff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiMonteCarloThreadSixteen()
        {
            DateTime start = DateTime.UtcNow;

            uint part1Begin = 0;
            uint part1End = m_NrOfIterations / 16;

            uint part2Begin = (m_NrOfIterations / 16) + 1;
            uint part2End = m_NrOfIterations * 2 / 16;

            uint part3Begin = (m_NrOfIterations * 2 / 16) + 1;
            uint part3End = m_NrOfIterations * 3 / 16;

            uint part4Begin = (m_NrOfIterations * 3 / 16) + 1;
            uint part4End = m_NrOfIterations * 4 / 16;

            uint part5Begin = (m_NrOfIterations * 4 / 16) + 1;
            uint part5End = m_NrOfIterations * 5 / 16;

            uint part6Begin = (m_NrOfIterations * 5 / 16) + 1;
            uint part6End = m_NrOfIterations * 6 / 16;

            uint part7Begin = (m_NrOfIterations * 6 / 16) + 1;
            uint part7End = m_NrOfIterations * 7 / 16;

            uint part8Begin = (m_NrOfIterations * 7 / 16) + 1;
            uint part8End = m_NrOfIterations * 8 / 16;

            uint part9Begin = (m_NrOfIterations * 8 / 16) + 1; ;
            uint part9End = m_NrOfIterations * 9 / 16;

            uint part10Begin = (m_NrOfIterations * 9 / 16) + 1;
            uint part10End = m_NrOfIterations * 10 / 16;

            uint part11Begin = (m_NrOfIterations * 10 / 16) + 1;
            uint part11End = m_NrOfIterations * 11 / 16;

            uint part12Begin = (m_NrOfIterations * 11 / 16) + 1;
            uint part12End = m_NrOfIterations * 12 / 16;

            uint part13Begin = (m_NrOfIterations * 12 / 16) + 1;
            uint part13End = m_NrOfIterations * 13 / 16;

            uint part14Begin = (m_NrOfIterations * 13 / 16) + 1;
            uint part14End = m_NrOfIterations * 14 / 16;

            uint part15Begin = (m_NrOfIterations * 14 / 16) + 1;
            uint part15End = m_NrOfIterations * 15 / 16;

            uint part16Begin = (m_NrOfIterations * 15 / 16) + 1;
            uint part16End = m_NrOfIterations;

            decimal valuePart1 = 0;
            decimal valuePart2 = 0;
            decimal valuePart3 = 0;
            decimal valuePart4 = 0;
            decimal valuePart5 = 0;
            decimal valuePart6 = 0;
            decimal valuePart7 = 0;
            decimal valuePart8 = 0;
            decimal valuePart9 = 0;
            decimal valuePart10 = 0;
            decimal valuePart11 = 0;
            decimal valuePart12 = 0;
            decimal valuePart13 = 0;
            decimal valuePart14 = 0;
            decimal valuePart15 = 0;
            decimal valuePart16 = 0;



            Task t1 = new Task(() => valuePart1 = CalculatePiMonteCarlo(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = CalculatePiMonteCarlo(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = CalculatePiMonteCarlo(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = CalculatePiMonteCarlo(part4Begin, part4End));
            Task t5 = new Task(() => valuePart5 = CalculatePiMonteCarlo(part5Begin, part5End));
            Task t6 = new Task(() => valuePart6 = CalculatePiMonteCarlo(part6Begin, part6End));
            Task t7 = new Task(() => valuePart7 = CalculatePiMonteCarlo(part7Begin, part7End));
            Task t8 = new Task(() => valuePart8 = CalculatePiMonteCarlo(part8Begin, part8End));
            Task t9 = new Task(() => valuePart9 = CalculatePiMonteCarlo(part9Begin, part9End));
            Task t10 = new Task(() => valuePart10 = CalculatePiMonteCarlo(part10Begin, part10End));
            Task t11 = new Task(() => valuePart11 = CalculatePiMonteCarlo(part11Begin, part11End));
            Task t12 = new Task(() => valuePart12 = CalculatePiMonteCarlo(part12Begin, part12End));
            Task t13 = new Task(() => valuePart13 = CalculatePiMonteCarlo(part13Begin, part13End));
            Task t14 = new Task(() => valuePart14 = CalculatePiMonteCarlo(part14Begin, part14End));
            Task t15 = new Task(() => valuePart15 = CalculatePiMonteCarlo(part15Begin, part15End));
            Task t16 = new Task(() => valuePart16 = CalculatePiMonteCarlo(part16Begin, part16End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();
            t9.Start();
            t10.Start();
            t11.Start();
            t12.Start();
            t13.Start();
            t14.Start();
            t15.Start();
            t16.Start();

            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
            DateTime endpar = DateTime.UtcNow;

            decimal pi = valuePart1 + valuePart2 + valuePart3 + valuePart4 + valuePart5 + valuePart6 + valuePart7 + valuePart8 + valuePart9 + valuePart10 + valuePart11 + valuePart12 + valuePart13 + valuePart14 + valuePart15 + valuePart16;

            DateTime end = DateTime.UtcNow;

            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timeparralleldiff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }
    }
}