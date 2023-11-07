// Cool features to implement:
//** Currency exchange - Make it so different accounts may have different currencies, Currency changes as you transfer money between accounts.


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading; // Add this using directive for Thread.Sleep
using System.Xml;

namespace Sums_of_Anarchy
{
    class Program
    {
        static List<User> users = new List<User>();
        static User currentUser;

        static void Main(string[] args)
        {
            while (true)
            {
                string welcomeText = "Welcome to \"Sums of Anarchy\" - The #1 leading bank in the world, your money is in the right hands, until it isn't.";
                foreach (char letter in welcomeText)
                {
                    Console.Write(letter);
                    Thread.Sleep(50); // Make the text flow on rather than getting printed all at once.
                }

                Console.WriteLine(); //moves the code along to the next string

                //Add here



                if (currentUser == null)
                {
                    Console.Write("Ange ditt användarnamn: ");
                    string username = Console.ReadLine();
                    Console.Write("Ange din PIN-kod: ");
                    string pin = Console.ReadLine();

                    if (username == "admin" && pin == "password") // Admin-inloggning
                    {
                        AdminMenu();
                    }
                    else
                    {
                        currentUser = AuthenticateUser(username, pin);
                        if (currentUser == null)
                        {
                            Console.WriteLine("Ogiltigt användarnamn eller PIN-kod.");
                        }
                        else
                        {
                            UserMenu();
                        }
                    }
                }
                else
                {
                    UserMenu();
                }
            }
        }
        // GetCurrencyListFromWeb https://stackoverflow.com/questions/11384541/getting-exchange-rates-from-the-internet
        public static List<KeyValuePair<string, decimal>> GetCurrencyListFromWeb(out DateTime currencyDate)
        {
            List<KeyValuePair<string, decimal>> returnList = new List<KeyValuePair<string, decimal>>();
            string date = string.Empty;
            using (XmlReader xmlr = XmlReader.Create(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"))
            {
                xmlr.ReadToFollowing("Cube");
                while (xmlr.Read())
                {
                    if (xmlr.NodeType != XmlNodeType.Element) continue;
                    if (xmlr.GetAttribute("time") != null)
                    {
                        date = xmlr.GetAttribute("time");
                    }
                    else returnList.Add(new KeyValuePair<string, decimal>(xmlr.GetAttribute("currency"), decimal.Parse(xmlr.GetAttribute("rate"), CultureInfo.InvariantCulture)));
                }
                currencyDate = DateTime.Parse(date);
            }
            returnList.Add(new KeyValuePair<string, decimal>("EUR", 1));
            return returnList;
        }

        static User AuthenticateUser(string username, string pin)
        {
            // Tip for future fady, make it so the database info gets pulled from here so you can authenticate the user.
            // IF the user exists in the database return the user as a User object in the database, otherwise NULL.
            return null; // <---- Change this to database logic
        }

        static void AdminMenu()
        {
            // Admin menu 
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine("1. View all of your acounts and balance");
                Console.WriteLine("2. Transfer Balance");
                Console.WriteLine("3. Withdrawal");
                Console.WriteLine("4. Deposit");
                Console.WriteLine("5. Open a new account");
                Console.WriteLine("6. Currency Exchanger");
                Console.WriteLine("7. Log out.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAccountInfo();
                        break;
                    case "2":
                        TransferMoney();
                        break;
                    case "3":
                        WithdrawMoney();
                        break;
                    case "4":
                        DepositMoney();
                        break;
                    case "5":
                        OpenNewAccount();
                        break;
                    case "6":
                        currentUser = null;
                        return;
                    default:
                        Console.WriteLine("Error! Please try again.");
                        break;
                }
            }
        }

        static void FindCurrencies()
        {
            Console.WriteLine("10 Most Popular Currencies:");

            List<KeyValuePair<string, decimal>> currencyList = GetCurrencyListFromWeb(out DateTime currencyDate);

            // Sort top 10 currencies Descending
            var popularCurrencies = currencyList.OrderByDescending(x => x.Value).Take(10);

            foreach (var currency in popularCurrencies)
            {
                Console.WriteLine($"{currency.Key}: {currency.Value}");
            }

        }

        static void ViewAccountInfo()
        {
            // Show the users bank account info
        }

        static void TransferMoney()
        {
            // Transfer money
        }

        static void WithdrawMoney()
        {
            // Withdraw money
        }

        static void DepositMoney()
        {
            // Deposit money
        }

        static void OpenNewAccount()
        {
            // Open a new account here
        }
    }
}
