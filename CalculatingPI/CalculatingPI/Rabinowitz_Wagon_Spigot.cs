using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatingPI
{
    class Rabinowitz_Wagon_Spigot
    {
        private uint m_NrOfDigits;

        public Rabinowitz_Wagon_Spigot(uint set_NrOfDigits) 
        {
            m_NrOfDigits = set_NrOfDigits;
        }

        // berekening van de RabinoWitz_Wagon_Spigot_Formula
        private string Rabinowitz_Wagon_Spigot_Formula(uint BeginDigits, uint EndDigits)
        {
            EndDigits++;
            uint[] x = new uint[EndDigits * 10 / 3 + 2];
            uint[] r = new uint[EndDigits * 10 / 3 + 2];

            uint[] pi = new uint[EndDigits - BeginDigits];

            // sets everything to 20
            for (uint j = 0; j < x.Length; j++)
                x[j] = 20;

            for (uint i = 0; i < EndDigits - BeginDigits ; i++)
            {
                // begin calculation digit
                uint carry = 0;
                for (uint j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;

                    x[j] += carry;

                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;

                    carry = q * num;
                }


                pi[i] = (x[x.Length - 1] / 10);


                r[x.Length - 1] = x[x.Length - 1] % 10; 

                for (uint j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }

            // preparing to convert to string to show on console
            var result = "";

            uint c = 0;
            // >= begindigits als je de 3 erbij wilt hebben van 3,14
            for (int i = pi.Length - 1; i >= 1; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;

                result = (pi[i] % 10).ToString() + result;
            }

            return result;
        }


        public Tuple<string, Int32> PiRabinowitz_Wagon_SpigotThreadOne()
        {
            // 1 thread => main thread
            DateTime start = DateTime.UtcNow;
            string pi = "3." + Rabinowitz_Wagon_Spigot_Formula(0, m_NrOfDigits);
            DateTime end = DateTime.UtcNow;

            TimeSpan timediff = end - start;
            Console.WriteLine("Rabinowitz Wagon Spigot: calculating " + m_NrOfDigits + " of Pi");

            Console.WriteLine("Pi: ");
            Console.WriteLine(pi);
            Console.WriteLine();
            return Tuple.Create(pi, Convert.ToInt32(timediff.TotalMilliseconds));
        }

        public void PiRabinowitz_Wagon_SpigotThreadTwo()
        {
            /********NIET MOGELIJK OM TE MULTITHREADING**************/
            //// two threads => main thread and one extra task
            //uint part1Begin = 0;
            //uint part1End = m_NrOfDigits / 2;

            //uint part2Begin = (m_NrOfDigits/2);
            //uint part2End = m_NrOfDigits;

            //string valuePart1 = null;
            //string valuePart2 = null;


            //Task t1 = new Task(() => valuePart1 = Rabinowitz_Wagon_Spigot_Formula(part1Begin, part1End));
            //Task t2 = new Task(() => valuePart2 = Rabinowitz_Wagon_Spigot_Formula(part2Begin, part2End));

            //DateTime start = DateTime.UtcNow;

            //t1.Start();
            //t2.Start();

            //Task.WaitAll(t1, t2);

            //string pi = "3." + valuePart1 + valuePart2;

            //DateTime end = DateTime.UtcNow;

            //TimeSpan timediff = end - start;
            //Console.WriteLine("Time needed to calculate pi: ");
            //Console.WriteLine(Convert.ToInt32(timediff.TotalMilliseconds) + " ms");

            //Console.WriteLine("pi: ");
            //Console.WriteLine(pi);
            //Console.WriteLine("error: ");
            //Console.WriteLine("no error");
            //Console.WriteLine();
        }

        public void PiRabinowitz_Wagon_SpigotThreadFour()
        {
            /********NIET MOGELIJK OM TE MULTITHREADING**************/

        }

        public void PiRabinowitz_Wagon_SpigotThreadEight()
        {
            /********NIET MOGELIJK OM TE MULTITHREADING**************/

        }

    }
}
