using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wwear2
{
    class Clothes
    {
        private string clothesName = "noName";
        private string typeOfClothes = "notype";
        private string tagString = "xxxx";
        private string clothesSection = "nosection";

        public string ClothesName { get { return clothesName; } set { clothesName = value; } }
        public string TypeOfClothes { get { return typeOfClothes; } set { typeOfClothes = value; } }
        public string TagString { get { return tagString; } set { tagString = value; } }
        public string ClothesSection { get { return clothesSection; } set { clothesSection = value; } }

        public Clothes(string name, string typeOfC, string tagS, string clothesSec){
            clothesName = name;
            typeOfClothes = typeOfC;
            tagString = tagS;
            clothesSection = clothesSec;
        }
    }
}
