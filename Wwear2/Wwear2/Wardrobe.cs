﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wwear2
{
    class Wardrobe
    {
        private static Dictionary<string, Clothes[]> head = new Dictionary<string, Clothes[]>();
        private static Dictionary<string, Clothes[]> body = new Dictionary<string, Clothes[]>();
        private static Dictionary<string, Clothes[]> legs = new Dictionary<string, Clothes[]>();
        private static Dictionary<string, Clothes[]> feet = new Dictionary<string, Clothes[]>();
        private static Random random = new Random();
        private static int maxHats = 0, maxShirts = 0, maxPants = 0, maxShoes = 0;

        /* Key codes generated as 4 character strings */
        /* Codes are (General Weather, precipitation, wind, humidity) */

        public static void WardrobeSet()
        {
            Clothes cloth0 = new Clothes("Hat", "head", "3xxx", "head");
            Clothes cloth1 = new Clothes("shirt", "body", "xxxx", "head");
            Clothes cloth2 = new Clothes("pants", "legs", "xxxx", "head");
            Clothes cloth3 = new Clothes("shoes", "feet", "xxxx", "head");

            addClothes("head",cloth0);
            addClothes("body",cloth1);
            addClothes("legs",cloth2);
            addClothes("feet",cloth3);
        }

        public static void addClothes(string typeOfClothes, Clothes cloth){

            if(typeOfClothes == "head"){
                if(head.ContainsKey(cloth.TagString)){
                    Debug.WriteLine("head addition success!");
                    head[cloth.TagString][head[cloth.TagString].Length-1] = cloth;
                }
                else
                {
                    Clothes[] clothArr = new Clothes[1];
                    clothArr[0] = cloth;
                    head.Add(cloth.TagString, clothArr);
                    Debug.WriteLine("head addition success!else statement");
                }

                maxHats++;
            }

            if (typeOfClothes == "body")
            {
                if (body.ContainsKey(cloth.TagString))
                {
                    body[cloth.TagString][body[cloth.TagString].Length-1] = cloth;
                }
                else
                {
                    Clothes[] clothArr = new Clothes[1];
                    clothArr[0] = cloth;
                    body.Add(cloth.TagString, clothArr);
                }

                maxShirts++;
            }

            if (typeOfClothes == "legs")
            {
                if (legs.ContainsKey(cloth.TagString))
                {
                    legs[cloth.TagString][legs[cloth.TagString].Length-1] = cloth;
                }
                else
                {
                    Clothes[] clothArr = new Clothes[1];
                    clothArr[0] = cloth;
                    legs.Add(cloth.TagString, clothArr);
                }

                maxPants++;
            }

            if (typeOfClothes == "feet")
            {
                if (feet.ContainsKey(cloth.TagString))
                {
                    feet[cloth.TagString][feet[cloth.TagString].Length-1] = cloth;
                }
                else
                {
                    Clothes[] clothArr = new Clothes[1];
                    clothArr[0] = cloth;
                    feet.Add(cloth.TagString, clothArr);
                }

                maxShoes++; 
            }
        }

        public static Clothes pickHat(string generatedCode){
            string[] permutations = { "" + generatedCode[0] + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + 'x',
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + 'x'+ 'x' + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + 'x' + 'x',
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + generatedCode[3],
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + 'x' + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + 'x'+ generatedCode[1] + 'x' + 'x',
                                      "" + 'x' + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + 'x'+'x' + generatedCode[2] + 'x',
                                      "" + 'x' + 'x' + 'x' + generatedCode[3],
                                      "" + 'x' + 'x' + 'x' + 'x',
                                      generatedCode};

            Clothes[] hopefulClothes = new Clothes[50];

            string correctedKey;

            int a = 0; //initial value count
            for(int i = 0; i < permutations.Length; i++)
            {
                Debug.WriteLine("Entering for loop for permutation key checks");
                correctedKey = fixX(permutations[i], generatedCode);
                Debug.WriteLine("cprrected key: " + correctedKey);
                if (head.ContainsKey(correctedKey))
                {
                    Debug.WriteLine(permutations[i]);       
                    hopefulClothes[a] = head[permutations[i]][random.Next(0,head[permutations[i]].Length)];
                    a++;
                }
            }

            if (hopefulClothes.Length <= 0)
            {
                Clothes close = new Clothes("No Clothes", "", "", "");
                Debug.WriteLine("Closing Cloth :(");
                return close;
            }
            else
            {
                Debug.WriteLine(maxHats);
                return hopefulClothes[random.Next(0,a)];
            }

        }

        public static Clothes pickTop(string generatedCode)
        {
            string[] permutations = { "" + generatedCode[0] + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + 'x',
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + 'x'+ 'x' + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + 'x' + 'x',
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + generatedCode[3],
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + 'x' + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + 'x'+ generatedCode[1] + 'x' + 'x',
                                      "" + 'x' + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + 'x'+'x' + generatedCode[2] + 'x',
                                      "" + 'x' + 'x' + 'x' + generatedCode[3],
                                      "" + 'x' + 'x' + 'x' + 'x',
                                      generatedCode};

            Clothes[] hopefulClothes = new Clothes[50];

            string correctedKey;

            int a = 0; //initial value count
            for (int i = 0; i < permutations.Length; i++)
            {
                Debug.WriteLine("Entering for loop for permutation key checks");
                correctedKey = fixX(permutations[i], generatedCode);
                Debug.WriteLine("cprrected key: " + correctedKey);
                if (body.ContainsKey(correctedKey))
                {
                    Debug.WriteLine(permutations[i]);
                    hopefulClothes[a] = body[permutations[i]][random.Next(0, body[permutations[i]].Length)];
                    a++;
                }
            }

            if (hopefulClothes.Length <= 0)
            {
                Clothes close = new Clothes("No Clothes", "", "", "");
                Debug.WriteLine("Closing Cloth :(");
                return close;
            }
            else
            {
                Debug.WriteLine(maxHats);
                return hopefulClothes[random.Next(0, a)];
            }

        }


        public static Clothes pickBottoms(string generatedCode)
        {
            string[] permutations = { "" + generatedCode[0] + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + 'x',
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + 'x'+ 'x' + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + 'x' + 'x',
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + generatedCode[3],
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + 'x' + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + 'x'+ generatedCode[1] + 'x' + 'x',
                                      "" + 'x' + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + 'x'+'x' + generatedCode[2] + 'x',
                                      "" + 'x' + 'x' + 'x' + generatedCode[3],
                                      "" + 'x' + 'x' + 'x' + 'x',
                                      generatedCode};

            Clothes[] hopefulClothes = new Clothes[50];

            string correctedKey;

            int a = 0; //initial value count
            for (int i = 0; i < permutations.Length; i++)
            {
                Debug.WriteLine("Entering for loop for permutation key checks");
                correctedKey = fixX(permutations[i], generatedCode);
                Debug.WriteLine("cprrected key bottoms: " + correctedKey);
                if (legs.ContainsKey(correctedKey))
                {
                    Debug.WriteLine(permutations[i]);
                    hopefulClothes[a] = legs[permutations[i]][random.Next(0, legs[permutations[i]].Length)];
                    a++;
                }
            }

            if (hopefulClothes.Length <= 0)
            {
                Clothes close = new Clothes("No Clothes", "", "", "");
                Debug.WriteLine("Closing Cloth :(");
                return close;
            }
            else
            {
                Debug.WriteLine(maxHats);
                return hopefulClothes[random.Next(0, a)];
            }

        }


        public static Clothes pickFeet(string generatedCode)
        {
            string[] permutations = { "" + generatedCode[0] + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + generatedCode[0] + generatedCode[1] + 'x' + 'x',
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + generatedCode[2] + 'x',
                                      "" + generatedCode[0] + 'x'+ 'x' + generatedCode[3],
                                      "" + generatedCode[0] + 'x' + 'x' + 'x',
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + generatedCode[3],
                                      "" + 'x' + generatedCode[1] + generatedCode[2] + 'x',
                                      "" + 'x' + generatedCode[1] + 'x' + generatedCode[3],
                                      "" + 'x'+ generatedCode[1] + 'x' + 'x',
                                      "" + 'x' + 'x' + generatedCode[2] + generatedCode[3],
                                      "" + 'x'+'x' + generatedCode[2] + 'x',
                                      "" + 'x' + 'x' + 'x' + generatedCode[3],
                                      "" + 'x' + 'x' + 'x' + 'x',
                                      generatedCode};

            Clothes[] hopefulClothes = new Clothes[50];

            string correctedKey;

            int a = 0; //initial value count
            for (int i = 0; i < permutations.Length; i++)
            {
                Debug.WriteLine("Entering for loop for permutation key checks");
                correctedKey = fixX(permutations[i], generatedCode);
                Debug.WriteLine("cprrected key: " + correctedKey);
                if (feet.ContainsKey(correctedKey))
                {
                    Debug.WriteLine(permutations[i]);
                    hopefulClothes[a] = feet[permutations[i]][random.Next(0, feet[permutations[i]].Length)];
                    a++;
                }
            }

            if (hopefulClothes.Length <= 0)
            {
                Clothes close = new Clothes("No Clothes", "", "", "");
                Debug.WriteLine("Closing Cloth :(");
                return close;
            }
            else
            {
                Debug.WriteLine(maxHats);
                return hopefulClothes[random.Next(0, a)];
            }

        }


        private static string fixX(string tag, string generatedCode)
        {
            char[] returnTag = generatedCode.ToCharArray();
            for (int i = 0; i < tag.Length; i++)
            {
                if (tag[i] == 'x' && generatedCode[i] != 'x')
                {
                    returnTag[i] = tag[i];
                }
            }

            return "" + returnTag[0] + returnTag[1] + returnTag[2] + returnTag[3];
        }

    }

}
