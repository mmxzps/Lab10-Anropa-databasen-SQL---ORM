using Lab10_Anropa_databasen_SQL___ORM.Data;
using Lab10_Anropa_databasen_SQL___ORM.Models;
using System.Security.Cryptography.X509Certificates;

namespace Lab10_Anropa_databasen_SQL___ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //calling the mainmenu method.
            MainMenu();
        }
        public static void Sorting()
        {
            Console.WriteLine("----------------------Sort the customer in the table----------------------");
            do
            {
                using NorthContext context = new NorthContext();
                {
                    //Asking the user how they want their info presented.
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Do you want to get your info ordered by [asc] or [desc]? ");
                    string input = Console.ReadLine().ToLower();
                    Console.Write("Enter the amount of Customers you want to see: ");
                    int limit = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                    if (input == "desc")
                    {
                        var allcustomers = context.Customers
                        .Select(c => new { c.CompanyName, c.Country, c.Region, c.Phone, Orders = c.Orders.Count })
                        .OrderByDescending(c => c.CompanyName)
                        .Take(limit)
                        .ToList();
                        foreach (var customer in allcustomers)
                        {
                            Console.WriteLine(customer.CompanyName);
                        }
                    }
                    else if (input == "asc")
                    {
                        var allcustomers = context.Customers
                       .Select(c => new { c.CompanyName, c.Country, c.Region, c.Phone, Orders = c.Orders.Count })
                       .OrderBy(c => c.CompanyName)
                       .Take(limit)
                       .ToList();
                        foreach (var customer in allcustomers)
                        {
                            Console.WriteLine(customer.CompanyName);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input.");
                        Console.ResetColor();
                    }
                }
                //Gives an option if we want to sort again.
                //if no we go back to menu.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Sort it again [yes/no]");
                string sortAgain = Console.ReadLine().ToLower();
                Console.ResetColor();
                if (sortAgain == "no")
                {
                    MainMenu();
                }
                else if (sortAgain == "yes")
                {
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    break;
                }

            } while (true); 
        }

        public static void BringInfo()
        {
            Console.WriteLine("--------------------------Get company information--------------------------");
            do
            {
                using NorthContext context = new NorthContext();
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Enter the company name to get more of its information: ");
                    Console.ResetColor();
                    string input = Console.ReadLine().ToLower();

                    var userChoice = context.Customers
                    .Where(o => o.CompanyName == input)
                    .Select(c => new { c.CompanyName, c.Country, c.Region, c.Phone, Orders = c.Orders.Count })
                    .ToList();

                    foreach (var order in userChoice)
                    {
                        //Console.WriteLine(order);
                        Console.WriteLine($"Company:{order.CompanyName} \nCountry:{order.Country} \nRegion:{order.Region} \nPhone:{order.Phone} \nOrders:{order.Orders}");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Check another company? [yes/no]");
                string checkC = Console.ReadLine().ToLower();
                Console.ResetColor();
                if (checkC == "no")
                {
                    MainMenu();
                }
                else if (checkC == "yes")
                {
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    break;
                }
            } while (true);
           
        }

        public static void AddCustomer()
        {
            Console.WriteLine("----------------------Add a Customer into the table----------------------");
            
            //here we use 2 Do-while loops. One is if the user wants to begin addin customers.
            //the second is if the user wants to add another customer.
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Do you want to add customer? [yes/no]");
                Console.ResetColor();
                string add = Console.ReadLine().ToLower();
                if (add == "no")
                {
                    MainMenu();
                }
                else if (add == "yes")
                {
                    do
                    {
                        using (NorthContext context = new NorthContext())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Enter Company Name: ");
                            Console.ResetColor();
                            string input = Console.ReadLine();

                            //Creating a random object
                            Random rand = new Random();
                            //The letters that randomises.
                            String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            //the size of random letters.
                            int fiveL = 5;
                            //storing the random letters
                            String randomID = "";
                            //using a for-loop to generate random letter and storing
                            //them into "randomID" after each iteration. one by one.
                            for (int i = 0; i < fiveL; i++)
                            {
                                int indexX = rand.Next(26);
                                randomID = randomID + letters.ElementAt(indexX);
                            }

                            Customer newCustomer = new Customer()
                            {
                                CustomerId = randomID
                            };

                            while (true)
                            {
                                //forcing user to enter companyname.
                                if (input == "")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("You MUST enter a company name");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                }
                                else
                                {
                                    string companyName = input;
                                    newCustomer.CompanyName = companyName;
                                    context.Customers.Add(newCustomer);
                                    context.SaveChanges();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{input} added to the table!");
                                    Console.ResetColor();
                                    break;
                                }

                            }
                            //Option to fill in the informations.
                            //if the user dont enter anything. It'll be null.
                            Console.Write("Enter Contact Name:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.ContactName = null;
                            }
                            else
                            {
                                string contactName = input;
                                newCustomer.ContactName = contactName;
                            }

                            Console.Write("Enter Contact Title:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.ContactTitle = null;
                            }
                            else
                            {
                                string ContactTitle = input;
                                newCustomer.ContactTitle = ContactTitle;
                            }

                            Console.Write("Enter Contact Address:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.Address = null;
                            }
                            else
                            {
                                string Address = input;
                                newCustomer.Address = Address;
                            }

                            Console.Write("Enter Contact City:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.City = null;
                            }
                            else
                            {
                                string City = input;
                                newCustomer.City = City;
                            }

                            Console.Write("Enter Contact Region:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.Region = null;
                            }
                            else
                            {
                                string Region = input;
                                newCustomer.Region = Region;
                            }

                            Console.Write("Enter Contact PostalCode:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.PostalCode = null;
                            }
                            else
                            {
                                string PostalCode = input;
                                newCustomer.PostalCode = PostalCode;
                            }

                            Console.Write("Enter Contact Country:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.Country = null;
                            }
                            else
                            {
                                string Country = input;
                                newCustomer.Country = Country;
                            }

                            Console.Write("Enter Contact Phone:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.Phone = null;
                            }
                            else
                            {
                                string Phone = input;
                                newCustomer.Phone = Phone;
                            }

                            Console.Write("Enter Contact Fax:");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                newCustomer.Fax = null;
                            }
                            else
                            {
                                string Fax = input;
                                newCustomer.Fax = Fax;
                            }

                            context.SaveChanges();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Customer updated with contact informaton!");
                            Console.ResetColor();

                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Do you want to add another customer? [yes/no]");
                        Console.ResetColor();
                        string addMore = Console.ReadLine().ToLower();
                        if (addMore == "no")
                        {
                            MainMenu();
                        }
                        else if (addMore == "yes")
                        {
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid input.");
                            Console.ResetColor();
                            break;
                        }
                    } while (true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    break;
                }

            } while (true);
            
        }

        public static void DeleteCustomer()
        {
            Console.WriteLine("----------------------Delete customer from the table----------------------");
            //here we use 2 Do-while loops. One is if the user wants to begin delete customers.
            //the second is if the user wants to delete another customer.
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Do you want to delete a customer? [yes/no]");
                string delete = Console.ReadLine().ToLower();
                Console.ResetColor();
                if (delete == "no")
                {
                    MainMenu();
                }
                else if (delete == "yes")
                {
                    do
                    {
                        using NorthContext context = new NorthContext();
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Enter the company name: ");
                            string input = Console.ReadLine();
                            Console.ResetColor();

                            var allcustomers = context.Customers
                                            .Where(c => c.CompanyName == input)
                                            .FirstOrDefault();
                            if (allcustomers != null)
                            {
                                context.Customers.Remove(allcustomers);
                                context.SaveChanges();
                                Console.WriteLine($"{input} deleted!");
                            }
                            Console.WriteLine($"{input} doesnt exists!");
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Delete more? [yes/no]");
                        string deletAgain = Console.ReadLine().ToLower();
                        Console.ResetColor();
                        if (deletAgain == "no")
                        {
                            MainMenu();
                        }
                        else if (deletAgain == "yes")
                        {
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid input.");
                            Console.ResetColor();
                            break;
                        }
                    }
                    while (true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    break;
                }
               
            } while (true);  
        }
        public static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine(" ----------------------------------------------- ");
            Console.WriteLine("|\tEnter the following option (0-4):\t|\n|\t0.To exit.\t\t\t\t| \n|\t1.Sorting & viewing the company names\t| " +
                "\n|\t2.Get more info of desired company\t| \n|\t3.Add a customer to the table.\t\t| \n|\t4.Delete a customer from the table.\t|");
            Console.WriteLine(" ----------------------------------------------- ");
            Console.ResetColor();
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Sorting();
                    break;
                case 2:
                    BringInfo();
                    break;
                case 3:
                    AddCustomer();
                    break;
                case 4:
                    DeleteCustomer();
                    break;
                case 0:
                    Console.WriteLine("Hasta La Vista Baby!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}