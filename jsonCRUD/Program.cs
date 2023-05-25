using Newtonsoft.Json;
using Var_WebCrawler_CRUD;
using System;
using System.Threading.Channels;

namespace var.WebCrawler.CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //listOfFood = new List<FoodGeneralInfo>();
            Console.WriteLine("Please Insert the path of the json file: ");
            string fileName = Console.ReadLine();
            List<FoodGeneralInfo> listOfFood = GetDataFromJson(fileName);
            //

            PrinciplMenu();
            string operation = Console.ReadLine();
            List<FoodGeneralInfo> SelectedItems = new List<FoodGeneralInfo>();
            switch (operation)
            {
                case "1": SelectedItems = SelectElement(listOfFood); break;
                case "2": break;
                case "3": break;
                case "4": break;
                case "5": break;
                default: break;
            }
            PrintData(SelectedItems);

            Console.WriteLine("The search result has " + SelectedItems.Count + " Elements");
        }
        #region selection Method
        public static List<FoodGeneralInfo> SelectElement(List<FoodGeneralInfo> list)
        {
            Console.WriteLine("Please select from the menu the Argument you want find a specific food:\n1-Italian Name\n2-English Name\n3-Scientific Name\n4-Category\n5-Food Code\n6-All above Arguments");
            string command = string.Empty;
            do
            {
                Console.WriteLine("Please Choose from the Menu (1-5): ");
                command = Console.ReadLine().Trim();

            } while (Convert.ToInt32(command) < 0 || Convert.ToInt32(command) > 6);
            Console.WriteLine("Please Insert The searchKey: ");
            string searchKey = Console.ReadLine();

            switch (command)
            {
                case "1": return list.Where(x => x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList();
                case "2": return list.Where(x => x.EnglishName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList();
                case "3": return list.Where(x => x.ScientificName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList();
                case "4": return list.Where(x => x.Category.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList();
                case "5": return list.Where(x => x.FoodCode == searchKey).ToList();
                case "6":
                    return list.Where(x => x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || (!string.IsNullOrEmpty(x.ScientificName) && x.ScientificName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase))
                || x.Category.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || x.FoodCode == searchKey).ToList();
                default:
                    Console.WriteLine("Please insert a number between 1 - 6 ");
                    return new List<FoodGeneralInfo>();
            }

        }

        #endregion

        public static void PrinciplMenu()
        {
            Console.WriteLine("Welcome to the Best Json browser in the world! :)))))))))))\n\n");
            Console.WriteLine("Please Choose From the Menu Your Operation:\n\n\n1-Select\n2-Add\n3-Delete\n4-Update\n5-DownLoad");
        }

        public static List<FoodGeneralInfo> GetDataFromJson(string path)
        {

            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<FoodGeneralInfo>>(jsonString);
        }

        public static void PrintData(List<FoodGeneralInfo> list)
        {
            string op = string.Empty;
            do
            {
                Console.WriteLine($"\n\n1- Print names of the food\n2- Print Site Url of the food\n3- Print Category of the food\n" +
                                     $"4- print information of the food\n5- Print Nutritions of the food\n6- Print Langual code of the fodd");
                op = Console.ReadLine();
            } while (Convert.ToInt32(op) < 1 && Convert.ToInt32(op) > 7);
            Parallel.ForEach<FoodGeneralInfo>(list, item =>
            {
                switch (op)
                {
                    case "1":
                        Console.WriteLine($"The Italian and English name and SCIENTIFIC name of the food are: {item.ItalianName} , {item.EnglishName} , {item.ScientificName}"); break;
                    case "2":
                        Console.WriteLine($"The site URL of the food is : {item.Url}"); break;
                    case "3":
                        Console.WriteLine($"The main Category of the food is : {item.Category}"); break;
                    case "4":
                        Console.WriteLine($"The Food information: {item.Information}"); break;
                    case "5":
                        Parallel.ForEach<Nutrition>(item.Nutritions, items =>
                    {
                        Console.WriteLine($"Category: {items.Category} | Description: {items.Description}" +
                            $" |ValueFor100g: {items.ValueFor100g} | Procedures: {items.Procedures} | DataSource: {items.DataSource} | Reference:  {items.Reference}");
                           Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                    }); break;
                    case "6":
                        Parallel.ForEach<Langual>(item.LangualCodes, ele =>
                        {
                        Console.WriteLine($"Langual Id: {ele.Id} | Langual Info: {ele.Info}");
                        }); break;
                    default:
                        Console.WriteLine("Pleasse Insert a number between 1-7");
                        break;
                }
            }
            );
        }
    }
}