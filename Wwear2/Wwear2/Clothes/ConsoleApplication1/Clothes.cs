using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Clothes
    {
        public enum types { notype, hat, jacket, sweater, tanktop, tShirt, jeans, shorts, pants, sweatPants, coat, shoes, sandels, flipflops };
        public enum tags { notag, thick, light, longSleeved, shortSleeved, noSleeve, heavy, fullLength, halfLength, threeForthsLength, closedToe, openToe, waterProof, insulated };

        private types typeOfClothes;
        private tags[] clothesTags = new tags[5];
        private int numberOfTags = 0;
        private string p;

        public void setType(types typeOfCloth){
            typeOfClothes = typeOfCloth;
        }
        public void addTag(string sTag)
        {
            tags tag;
            switch (sTag)
            {
                case "thick":
                    tag = tags.thick;
                    break;
                case "light":
                    tag = tags.light;
                    break;
                case "long sleave":
                    tag = tags.longSleeved;
                    break;
                case "short sleave":
                    tag = tags.shortSleeved;
                    break;
                case "no sleave":
                    tag = tags.noSleeve;
                    break;
                case "heavy":
                    tag = tags.heavy;
                    break;
                case "full length":
                    tag = tags.fullLength;
                    break;
                case "half length":
                    tag = tags.halfLength;
                    break;
                case "three forths length":
                    tag = tags.threeForthsLength;
                    break;
                case "closed toe":
                    tag = tags.closedToe;
                    break;
                case "open toe":
                    tag = tags.openToe;
                    break;
                case "waterproof":
                    tag = tags.waterProof;
                    break;
                case "insulated":
                    tag = tags.insulated;
                    break;
                default:
                    tag = tags.notag;
                    Console.WriteLine("No such tag found!");
                    Environment.Exit(0);
                    break;
            }
            if (numberOfTags < 5)
            {

                clothesTags[numberOfTags] = tag;
                numberOfTags++;
            }
        }
        public void removeTag(string sTag)
        {
            tags tag;
            switch (sTag)
            {
                case "thick":
                    tag = tags.thick;
                    break;
                case "light":
                    tag = tags.light;
                    break;
                case "long sleave":
                    tag = tags.longSleeved;
                    break;
                case "short sleave":
                    tag = tags.shortSleeved;
                    break;
                case "no sleave":
                    tag = tags.noSleeve;
                    break;
                case "heavy":
                    tag = tags.heavy;
                    break;
                case "full length":
                    tag = tags.fullLength;
                    break;
                case "half length":
                    tag = tags.halfLength;
                    break;
                case "three forths length":
                    tag = tags.threeForthsLength;
                    break;
                case "closed toe":
                    tag = tags.closedToe;
                    break;
                case "open toe":
                    tag = tags.openToe;
                    break;
                case "waterproof":
                    tag = tags.waterProof;
                    break;
                case "insulated":
                    tag = tags.insulated;
                    break;
                default:
                    tag = tags.notag;
                    Console.WriteLine("No such tag found!");
                    Environment.Exit(0);
                    break;
            }
            for (int i = 0; i < numberOfTags; i++)
            {
                if (clothesTags[i] == tag)
                {
                    clothesTags[i] = tags.notag;
                }
                if (clothesTags[i] == tags.notag)
                {
                    clothesTags[i] = clothesTags[i + 1];
                    clothesTags[i + 1] = tags.notag;
                }
            }
            numberOfTags--;
        }

        public Clothes(types typeOfCloth)
        {
            this.typeOfClothes = typeOfCloth;
        }

        public Clothes(string p)
        {
            // TODO: Complete member initialization
            switch (p)
            {
                case "hat":
                    this.typeOfClothes = types.hat;
                    break;
                case "jacket":
                    typeOfClothes = types.jacket;
                    break;
                case "sweater":
                    typeOfClothes = types.sweater;
                    break;
                case "tanktop":
                    typeOfClothes = types.tanktop;
                    break;
                case "t-shirts":
                    typeOfClothes = types.tShirt;
                    break;
                case "jeans":
                    typeOfClothes = types.jeans;
                    break;
                case "shorts":
                    typeOfClothes = types.shorts;
                    break;
                case "pants":
                    typeOfClothes = types.pants;
                    break;
                case "sweat pants":
                    typeOfClothes = types.sweatPants;
                    break;
                case "coat":
                    typeOfClothes = types.coat;
                    break;
                case"shoes":
                    typeOfClothes = types.shoes;
                    break;
                case "sandles":
                    typeOfClothes = types.sandels;
                    break;
                case "flip flops":
                    typeOfClothes = types.flipflops;
                    break;
                default:
                    typeOfClothes = types.notype;
                    Console.WriteLine("No such type found!\nThe application will now terminate!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
            }
        }

        public static void printTags(Clothes a)
        {
            for (int i = 0; i < a.numberOfTags; i++)
            {
                Console.WriteLine(a.clothesTags[i]);
            }
        }
        public static void Main(string[] args)
        {
            Console.Write("Enter type of cloth: ");
            Clothes cloth1 = new Clothes(Console.ReadLine());
            Console.Write("Enter tag: ");
            string tag = tag = Console.ReadLine();
            int count = cloth1.numberOfTags;
            do
            {
                Console.Write("Enter tag: ");
                cloth1.addTag(tag);
                tag = Console.ReadLine();
            } while (!String.IsNullOrEmpty(tag) && count <= 5);
            Console.Write("Enter a tag to remove: ");
            cloth1.removeTag(Console.ReadLine());
            Console.WriteLine("Number of Tags: " + cloth1.numberOfTags);
            printTags(cloth1);

            Console.ReadLine();
        }

    }
}