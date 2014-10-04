using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Jacket
    {
        public static bool heavy;
        public static bool light;
        public static bool windBreaker;
        public static int cold, freeze, breeze, hot, humid, rainy, windy, snow;
        public Jacket()
        {
        }
        public static void isHeavy(bool a)
        {
            heavy = a;
            if (a)
            {
                light = false;
                windBreaker = false;
            }
        }
        public static void isLight(bool a)
        {
            light = a;
            if (a)
            {
                heavy = false;
                windBreaker = false;
            }
        }
        public static void isWindBreaker(bool a)
        {
            windBreaker = a;
            if (a)
            {
                light = false;
                heavy = false;
            }
        }
        public static void setValues()
        {
            if (heavy)
            {
                cold = 8;
                freeze = 10;
                breeze = 0;
                hot = 0;
                humid = 0;
                rainy = 5;
                windy = 0;
                snow = 10;
            }
            else if (light)
            {
                cold = 10;
                freeze = 7;
                breeze = 8;
                hot = 2;
                humid = 0;
                rainy = 10;
                windy = 7;
                snow = 2;
            }
            else
            {
                cold = 2;
                freeze = 0;
                breeze = 10;
                hot = 5;
                humid = 5;
                rainy = 6;
                windy = 10;
                snow = 0;
            }
        }
    }
    class Sweater
    {
        public static int cold = 10, freeze = 6, breeze = 4, hot = 0, humid = 0, rainy = 0, windy = 6, snow = 0;
        public Sweater()
        {
        }
    }
    class RegularTop
    {
        public static bool longSleeve = false, shortSleeve = false, tankTop = false;
        public static int cold, freeze, breeze, hot, humid, rainy, windy, snow;
        public RegularTop()
        {
        }
        public static void isLong(bool a)
        {
            longSleeve = a;
        }
        public static void isShort(bool a)
        {
            shortSleeve = a;
        }
        public static void isTankTop(bool a)
        {
            tankTop = a;
        }
        public static void setValues()
        {
            if (longSleeve)
            {
                cold = 8;
                freeze = 10;
                breeze = 6;
                hot = 0;
                humid = 0;
                rainy = 5;
                windy = 10;
                snow = 10;
            }
            else if (shortSleeve)
            {
                cold = 7;
                freeze = 8;
                breeze = 7;
                hot = 9;
                humid = 8;
                rainy = 5;
                windy = 0;
                snow = 4;
            }
            else
            {
                cold = 0;
                freeze = 0;
                breeze = 5;
                hot = 10;
                humid = 10;
                rainy = 5;
                windy = 0;
                snow = 0;
            }
        }

        class Program
        {
            const int REFERENCETEMP = 72;
            static bool setCold(int temp, int wc)
            {
                bool cold;
                int feltTemp = Convert.ToInt32(0.3 * wc + 0.7 * temp);
                if ((REFERENCETEMP - feltTemp > 10 && REFERENCETEMP - feltTemp < 25) && feltTemp > 40)
                {
                    cold = true;
                }
                else
                {
                    cold = false;
                }
                return cold;
            }
            static bool setFreeze(int temp, int wc)
            {
                bool freeze = false;
                int feltTemp = Convert.ToInt32(0.3 * wc + 0.7 * temp);
                if (temp < 40 || REFERENCETEMP - feltTemp > 25)
                {
                    freeze = true;
                }
                return freeze;
            }
            static bool setHot(int temp, int wc)
            {
                bool hot = false;
                int feltTemp = Convert.ToInt32(0.3 * wc + 0.7 * temp);
                if (temp > 80 || feltTemp - REFERENCETEMP > 5)
                {
                    hot = true;
                }
                return hot;
            }
            static bool setRainy(int chanceRain)
            {
                bool rainChance = false;
                if (chanceRain > 50)
                {
                    rainChance = true;
                }
                return rainChance;
            }
            static void setWind(ref bool nowind, ref bool breeze, ref bool wind, int windSpeed)
            {
                nowind = false;
                breeze = false;
                wind = false;
                if (windSpeed < 2)
                {
                    nowind = true;
                }
                else if (windSpeed < 10)
                {
                    breeze = true;
                }
                else
                {
                    wind = true;
                }
            }
            static bool setHumid(int humidity)
            {
                bool humid = false;
                if (humidity > 30)
                {
                    humid = true;
                }
                return humid;
            }

            /* static void Main(string[] args)
             {
                 int temp = 70, windChill = 65, windSpeed = 10, humidity = 20, chancePres = 40;
                 bool cold = false, freeze = false, breeze = false, hot = false, humid = false, rainy = false, windy = false, snow, normal = false, noWind = false;
                 cold = setCold(temp, windChill);
                 if (cold)
                 {
                     rainy = setRainy(chancePres);
                 }
                 else
                 {
                     freeze = setFreeze(temp, windChill);
                     if (freeze)
                     {
                         snow = setRainy(chancePres);
                     }
                     else
                     {
                         hot = setHot(temp, windChill);
                         if (hot)
                         {
                             rainy = setRainy(chancePres);
                         }
                         else
                         {
                             normal = true;
                             rainy = setRainy(chancePres);
                         }
                     }
                 }
                 setWind(ref noWind,ref breeze,ref windy, windSpeed);
                 humid = setHumid(humidity); 
             }
         }
             */
        }
    }
}