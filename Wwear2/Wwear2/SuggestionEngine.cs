using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wwear2
{
    class SuggestionEngine
    {
        private int REFERENCETEMP = 72;
        private int REFERENCEHUMIDITY = 10;
        private int REFERENCEFREEZING = 35;
        private int REFERENCECOLD = 10;
        private int REFERENCESPEED = 15;

        /*
         * @param temp
         *      actual temperature
         * @param wc
         *      wind chill
         * @return
         *      0 if hot 1 if normal 2 if cold 3 if freezing
         */

        public SuggestionEngine(int RT, int RH, int RF, int RC, int RS)
        {
            REFERENCETEMP = RT;
            REFERENCEHUMIDITY = RH;
            REFERENCEFREEZING = RF;
            REFERENCECOLD = RC;
            REFERENCESPEED = RS;
        }

        private int setGeneral(int temp, int wc)
        {
            int feltTemp = (int)(0.3 * wc + 0.7 * temp);
            int returnVal;
            if (REFERENCETEMP - feltTemp > REFERENCECOLD && feltTemp > REFERENCEFREEZING) //cold
            {
                returnVal = 2;
            }
            else if (REFERENCETEMP - feltTemp > 2 * REFERENCECOLD || feltTemp <= REFERENCEFREEZING) //freeze
            {
                returnVal = 3;
            }
            else if (REFERENCETEMP - feltTemp < REFERENCECOLD || feltTemp - REFERENCETEMP < REFERENCECOLD/2) //normal
            {
                returnVal = 1;
            }
            else //hot
            {
                returnVal = 0;
            }
            return returnVal;
        }


        /*
         * @param windspeed
         *          windspeed
         * @return
         *          0 if no wind, 1 if breeze, 2 if windy
         */
        
        private int setWind(int windSpeed)
        {
            int returnVal;
            if (windSpeed < 1)
            {
                returnVal = 0;
            }
            else if (windSpeed < REFERENCESPEED)
            {
                returnVal = 1;
            }
            else
            {
                returnVal = 2;
            }
            return returnVal;
        }

        //returns 0 if no precipitation, 1 if chance of rain > 50% 2 if chance of snow > 50%
        private int setPrecipitation(int chanceOfPrecipitation, int temp)
        {
            int returnVal;
            if (chanceOfPrecipitation > 50)
            {
                if (temp > REFERENCEFREEZING)
                {
                    returnVal = 1;
                }
                else
                {
                    returnVal = 2;
                }
            }
            else
            {
                returnVal = 0;
            }
            return returnVal;
        }

        //0 if humidity < 10% 1 if 10 <=humdity < 50 2 if humidity >= 50
        private int setHumid(int humidity)
        {
            int returnvalue;
            if (humidity < REFERENCEHUMIDITY)
            {
                returnvalue = 0;
            }
            else if (humidity < 5 * REFERENCEHUMIDITY)
            {
                returnvalue = 1;
            }
            else
            {
                returnvalue = 2;
            }
            return returnvalue;
        }

        public char[] setValue(int temp, int windChill, int chancePrecipitation, int windSpeed, int humidity)
        {
            char[] charString = new char[4];
            charString[0] = setGeneral(temp, windChill).ToString().ToCharArray()[0];
            charString[1] = setPrecipitation(chancePrecipitation, temp).ToString().ToCharArray()[0];
            charString[2] = setWind(windSpeed).ToString().ToCharArray()[0];
            charString[3] = setHumid(humidity).ToString().ToCharArray()[0];
            return charString;
        }
    }
}
