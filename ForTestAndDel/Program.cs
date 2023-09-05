using System;

namespace ForTestAndDel
{
    class Program
    {
        static void Main(string[] args)
        {

            string c1 = "III";
            string c2 = "LVIII"; // 58
            string c3 = "MCMXCIV"; // M = 1000 CM = 900 XC = 90 IV = 4 ->  1994 // 1000 100 1000 10 100 1 5 
            string c4 = "MMMCMXCIX"; // 3999
            // I   1
            // IV  4
            // V   5
            // IX  9
            // X   10
            // XL  40
            // L   50
            // LX  60
            // XC  90
            // C   100
            // CD  400
            // D   500
            // CM  900
            //
            // M   1000
            // MC  1100 
            for(var i = 0; i < c1.Length; i++)
            {
                switch (c1[i])
                {
                    case 'M':
                        if (i + 1 <= c1.Length)
                        {
                            //if ()
                        }
                        break;

                }
            }
        }
    }
}
