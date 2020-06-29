using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace CalculatingPI
{
    class Nilakantha_Series : CalculatePI
    {

        public Nilakantha_Series(uint set_NrOfIterations) : base (set_NrOfIterations)
        {

        }

        // berekening van de Nilakantha Series
        private decimal Nilakantha_Series_Formula(uint begin, uint end)
        {
            decimal sum = 0;
            decimal temp = 0;
            decimal a = 2, b = 3, c = 4;
            for (uint i = begin; i <= end; i++)
            {
                temp = 4 / ( (a + (i * 2)) * (b + (i * 2)) * (c + (i * 2)) );
                sum += i % 2 == 0 ? temp : -temp;
            }
            return sum;
        }

        public Tuple<decimal, decimal, Int32> PiNilakanthaThreadOne()
        {
            DateTime start = DateTime.UtcNow;

            // 1 thread => main thread
            decimal pi = 3 + Nilakantha_Series_Formula(0, m_NrOfIterations);
            DateTime end = DateTime.UtcNow;

            //bereken het verschil tussen de berekende pi en de correct PI waarde
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

        public Tuple<decimal, decimal, Int32> PiNilakanthaThreadTwo()
        {

            DateTime start = DateTime.UtcNow;

            // 2 threads 
            // partXBegin is op welke integer waarde de for lus moet beginnen en partXEnd is wanneer de for lus moet eindigen
            uint part1Begin = 0;
            uint part1End = m_NrOfIterations/2;

            uint part2Begin = (m_NrOfIterations / 2) + 1;
            uint part2End = m_NrOfIterations;

            decimal valuePart1 = 0;
            decimal valuePart2 = 0;


            Task t1 = new Task(() => valuePart1 = Nilakantha_Series_Formula(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = Nilakantha_Series_Formula(part2Begin, part2End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();

            Task.WaitAll(t1,t2);
            DateTime endpar = DateTime.UtcNow;

            // + 3 door de formule van Nilakantha Series, indien de 3 in de formule van Nilakantha_series_formula staat dan doet hij 2x +3 en niet 1x
            decimal pi = 3 + valuePart1 + valuePart2;

            DateTime end = DateTime.UtcNow;

            //bereken het verschil tussen de berekende pi en de correct PI waarde
            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiNilakanthaThreadFour()
        {
            DateTime start = DateTime.UtcNow;

            // 4 threads 
            // partXBegin is op welke integer waarde de for lus moet beginnen en partXEnd is wanneer de for lus moet eindigen
            uint part1Begin = 0;
            uint part1End = m_NrOfIterations / 4;

            uint part2Begin = (m_NrOfIterations / 4) + 1;
            uint part2End = m_NrOfIterations/2;

            uint part3Begin = (m_NrOfIterations / 2) + 1;
            uint part3End = m_NrOfIterations * 3 / 4;

            uint part4Begin = (m_NrOfIterations * 3 / 4) + 1;
            uint part4End = m_NrOfIterations;


            decimal valuePart1 = 0;
            decimal valuePart2 = 0;
            decimal valuePart3 = 0;
            decimal valuePart4 = 0;



            Task t1 = new Task(() => valuePart1 = Nilakantha_Series_Formula(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = Nilakantha_Series_Formula(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = Nilakantha_Series_Formula(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = Nilakantha_Series_Formula(part4Begin, part4End));

            DateTime startpar = DateTime.UtcNow;

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Task.WaitAll(t1, t2, t3, t4);
            DateTime endpar = DateTime.UtcNow;

            // + 3 door de formule, indien de 3 in de Nilakantha_series_formula staat dan doet hij 2x +3 en niet 1x
            decimal pi = 3 + valuePart1 + valuePart2 + valuePart3 + valuePart4;

            DateTime end = DateTime.UtcNow;

            //bereken het verschil tussen de berekende pi en de correct PI waarde
            decimal error = ErrorDifferencePi(pi);

            TimeSpan timediff = end - start;
            TimeSpan timeparralleldiff = endpar - startpar;

            Console.WriteLine("Time needed to calculate pi: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();
            Console.WriteLine("Time needed for the parrallel computation: ");
            Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");
            Console.WriteLine();

            Console.WriteLine("pi: ");
            Console.WriteLine(pi);
            Console.WriteLine("error: ");
            Console.WriteLine(error);
            Console.WriteLine();
            return Tuple.Create(pi, error, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public Tuple<decimal, decimal, Int32> PiNilakanthaThreadEight()
        {

            DateTime start = DateTime.UtcNow;

            // 8 threads
            // partXBegin is op welke integer waarde de for lus moet beginnen en partXEnd is wanneer de for lus moet eindigen
            uint part1Begin = 0;
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

            Task t1 = new Task(() => valuePart1 = Nilakantha_Series_Formula(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = Nilakantha_Series_Formula(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = Nilakantha_Series_Formula(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = Nilakantha_Series_Formula(part4Begin, part4End));
            Task t5 = new Task(() => valuePart5 = Nilakantha_Series_Formula(part5Begin, part5End));
            Task t6 = new Task(() => valuePart6 = Nilakantha_Series_Formula(part6Begin, part6End));
            Task t7 = new Task(() => valuePart7 = Nilakantha_Series_Formula(part7Begin, part7End));
            Task t8 = new Task(() => valuePart8 = Nilakantha_Series_Formula(part8Begin, part8End));

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
            decimal pi = 3 + valuePart1 + valuePart2 + valuePart3 + valuePart4 + valuePart5 + valuePart6 + valuePart7 + valuePart8;

            DateTime end = DateTime.UtcNow;

            //bereken het verschil tussen de berekende pi en de correct PI waarde
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

        public Tuple<decimal, decimal, Int32> PiNilakanthaThreadSixteen()
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



            Task t1 = new Task(() => valuePart1 = Nilakantha_Series_Formula(part1Begin, part1End));
            Task t2 = new Task(() => valuePart2 = Nilakantha_Series_Formula(part2Begin, part2End));
            Task t3 = new Task(() => valuePart3 = Nilakantha_Series_Formula(part3Begin, part3End));
            Task t4 = new Task(() => valuePart4 = Nilakantha_Series_Formula(part4Begin, part4End));
            Task t5 = new Task(() => valuePart5 = Nilakantha_Series_Formula(part5Begin, part5End));
            Task t6 = new Task(() => valuePart6 = Nilakantha_Series_Formula(part6Begin, part6End));
            Task t7 = new Task(() => valuePart7 = Nilakantha_Series_Formula(part7Begin, part7End));
            Task t8 = new Task(() => valuePart8 = Nilakantha_Series_Formula(part8Begin, part8End));
            Task t9 = new Task(() => valuePart9 = Nilakantha_Series_Formula(part9Begin, part9End));
            Task t10 = new Task(() => valuePart10 = Nilakantha_Series_Formula(part10Begin, part10End));
            Task t11 = new Task(() => valuePart11 = Nilakantha_Series_Formula(part11Begin, part11End));
            Task t12 = new Task(() => valuePart12 = Nilakantha_Series_Formula(part12Begin, part12End));
            Task t13 = new Task(() => valuePart13 = Nilakantha_Series_Formula(part13Begin, part13End));
            Task t14 = new Task(() => valuePart14 = Nilakantha_Series_Formula(part14Begin, part14End));
            Task t15 = new Task(() => valuePart15 = Nilakantha_Series_Formula(part15Begin, part15End));
            Task t16 = new Task(() => valuePart16 = Nilakantha_Series_Formula(part16Begin, part16End));

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

            // + 3 door de formule, indien de 3 in de Nilakantha_series_formula staat dan doet hij 2x +3 en niet 1x
            decimal pi = 3 + valuePart1 + valuePart2 + valuePart3 + valuePart4 + valuePart5 + valuePart6 + valuePart7 + valuePart8 + valuePart9 + valuePart10 + valuePart11 + valuePart12 + valuePart13 + valuePart14 + valuePart15 + valuePart16;

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
