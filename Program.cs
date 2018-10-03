using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace card
{
    public class Card
    {
        public string nameOwner;
        public string nameCompany;
        public string idCard;
        public bool checkSum;

        public Card(string id, string owner)
        {
            this.nameOwner = owner;
            this.nameCompany = "None";
            this.idCard = id;
            chekLegit();
        }
        public void setCompany()
        {
            if (idCard.Length == 15 && (idCard.Substring(0, 2) == "34" || idCard.Substring(0, 2) == "37"))  // 370000000000000
                this.nameCompany = "American Express";
            else if ((idCard.Length == 13 || idCard.Length == 16) && idCard[0] == '4')    // 4000000000000
                this.nameCompany = "Visa";
            else if (idCard.Length == 16 && (idCard[0] == '5' || idCard[1] == '1' || idCard[1] == '2' || idCard[1] == '3' || idCard[1] == '4' || idCard[1] == '5')) //5400000000000000
                this.nameCompany = "MasterCard";
            else
                this.nameCompany = "Invalid";
        }
        public bool setCompanyOne(string nameCompany, int numberAmount, string numberId)
        {
            if (idCard.Length == numberAmount && idCard.Substring(0, numberId.Length) == numberId)
            {
                this.nameCompany = nameCompany;
                return true;
            }
            return false;
        }
        public void chekLegit()
        {
            int sum = 0;
            int len = idCard.Length;
            for (int i = 0; i < len; i++)
            {
                int add = (idCard[i] - '0') * (2 - (i + len) % 2);
                add -= add > 9 ? 9 : 0;
                sum += add;
            }
            checkSum = sum % 10 == 0;
        }
        public string toString()
        {
            string str = "Owner: " + nameOwner + "\nCard id: " + idCard + "\n" + nameCompany;
            if (checkSum == true)
                str += "\nValid card";
            else
                str += "\nInvalid card";
            return str;
        }

    }
    class Program
    {
        static bool readData(string nameFile, Card CreditCard)
        {
            string line;
            StreamReader read = new StreamReader(nameFile + ".txt");
            while ((line = read.ReadLine()) != null)
            {
                string nameCompany = string.Empty;
                int i = 0;
                while (line[i] != ':')             //read namecompany
                {
                    nameCompany += line[i];
                    i++;
                }
                i++;
                while (i != line.Length)
                {
                    string amountID = string.Empty;
                    string numberID = string.Empty;

                    while (line[i] != ',')      //read amount ID
                    {
                        amountID += line[i];
                        i++;
                    }
                    i++;
                    while (line[i] != ':')   //read numver ID
                    {
                        numberID += line[i];
                        i++;
                    }
                    i++;
                    if (CreditCard.setCompanyOne(nameCompany, int.Parse(amountID), numberID) == true)
                    {
                        return true;
                    }
                }
            }
            CreditCard.nameCompany = "Invalid";
            return false;
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello\nPlease write name of card owner");
            //string owner = Console.ReadLine();
            string owner = "Vlad";
            Console.WriteLine("Write id card");
            string cardId = Console.ReadLine();
            //Console.WriteLine("Write name data file");
            //string nameFile = Console.ReadLine();
            string nameFile = "data";
            Card CreditCard = new Card(cardId, owner);
            Console.Clear();
            readData(nameFile, CreditCard);
            Console.WriteLine(CreditCard.toString());
            Console.ReadKey();
        }
    }
}