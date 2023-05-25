using Newtonsoft.Json;
using Var_WebCrawler_CRUD;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace var.WebCrawler.CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to the Best Json browser in the world! :)))))))))))\n");
            Console.WriteLine("Please Insert the path of the json file: "); 
            string filePath = string.Empty;
            do
            {
                filePath = Console.ReadLine();
            } while (string.IsNullOrEmpty(filePath));
            List<FoodGeneralInfo> listOfFood = GetJson(filePath);
            MainMenu(listOfFood, filePath);
            MakeJsonFile(listOfFood, filePath);
        }
        #region selection Method
        public static List<FoodGeneralInfo> SelectElement(List<FoodGeneralInfo> list)
        {
            Console.WriteLine("\nPlease select from the menu the Argument you want find a specific food:\n1-Italian Name\n2-English Name\n3-Scientific Name\n4-Category\n5-Food Code\n6-All above Arguments\n");
            string command = string.Empty;
            do
            {
                Console.WriteLine("Please Choose from the Menu (1-6): ");
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
        // Welcome Menu and return the path of the json file
        // retrun 
        public static List<FoodGeneralInfo> GetJson(string path)
        {

            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<FoodGeneralInfo>>(jsonString);
        }
        //print the information of an element of the list
        public static void PrintData(List<FoodGeneralInfo> list)
        {
            string op = string.Empty;
            do
            {
                Console.WriteLine($"\n\n 1- Print names of the food\n 2- Print Site Url of the food\n 3- Print Category of the food\n" +
                     $" 4- print information of the food\n 5- Print Nutritions of the food\n 6- Print Langual code of the food\n 7-Exit");
                op = Console.ReadLine();
                foreach (var item in list)
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
                            foreach (var ele in item.Nutritions)
                            {
                                Console.WriteLine($" food Name: {item.ItalianName} ==>  \n Category: {ele.Category} \n Description: {ele.Description}" +
                                    $" \n ValueFor100g: {ele.ValueFor100g} \n Procedures: {ele.Procedures} \n DataSource: {ele.DataSource} \n Reference:  {ele.Reference}");
                                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                            }; break;
                        case "6":
                            foreach (var ele in item.LangualCodes)
                            {
                                Console.WriteLine($"\nLangual Id: {ele.Id} \n Langual Info: {ele.Info}");
                            }; break;
                        case "7": break;
                        default: Console.WriteLine("You did not choose [1-7]"); break;
                    }
                }

            } while (op != "7" || (Convert.ToInt32(op) < 1 && Convert.ToInt32(op) > 8));

            Console.WriteLine("\nThe search result has " + list.Count + " Elements");

        }
        #region AddMethod
        public static void AddElement(List<FoodGeneralInfo> list, string path)
        {

            Console.WriteLine("\nWelcome to the addition Section : \n");
            string italianName = string.Empty;
            do
            {
                Console.WriteLine("Please Insert the Italian name of the food:");
                italianName = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(italianName));

            Console.WriteLine("Please Insert the Category of the food:");
            string category = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the Scientific Name of the food:");
            string scientificName = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the English Name of the food:");
            string englishName = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the Information of the food:");
            string information = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the Number Of Samples of the food:");
            string numberOfSamples = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the Eatable Partpercentage part of the food:");
            string eatablePartpercentage = Console.ReadLine().Trim();
            Console.WriteLine("Please Insert the Portion of the food:");
            string portion = Console.ReadLine().Trim();

            // a way to find the max index of the list
            List<string> FoodCodeProjectAttributeSTR = list.Where(x => x.FoodCode.All(Char.IsDigit)).Select(x => x.FoodCode).ToList();

            List<int> FoodCodeProjectAttributeINT = FoodCodeProjectAttributeSTR.Select(int.Parse).ToList();
            int maxFoodCode = FoodCodeProjectAttributeINT.Max();
            int newFoodCode = ++maxFoodCode;
            string foodCode = newFoodCode.ToString();

            list.Add(new FoodGeneralInfo()
            {
                Url = "This Item is Added by the user!",
                ItalianName = italianName,
                Category = category,
                FoodCode = foodCode,
                ScientificName = scientificName,
                EnglishName = englishName,
                Information = information,
                NumberOfSamples = numberOfSamples,
                EatablePartpercentage = eatablePartpercentage,
                Portion = portion
            });
            //Add the new item to the json file in the directory


            List<FoodGeneralInfo> AddedItem = new List<FoodGeneralInfo>();
            AddedItem = list.Where(x => x.FoodCode == foodCode).ToList();


            Console.WriteLine("\nPlease Choose the operation:\n 1-See the item Added: \n 2-Back To The Main Menu\n 3-Exit");
            string operation = string.Empty;
            do
            {
                operation = Console.ReadLine();

            } while (string.IsNullOrEmpty(operation));

            switch (operation)
            {
                case "1": PrintData(AddedItem); break;
                case "2": MainMenu(list, path); break;
                case "3": break;
            }

        }
        #endregion
        public static void MakeJsonFile(List<FoodGeneralInfo> list, string path)
        {
            var option = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(list, option);
            File.WriteAllText(path, jsonString);
        }

        public static void MainMenu(List<FoodGeneralInfo> list, string jsonpathFile)
        {
            
            string operation = string.Empty;
            do
            {
                Console.WriteLine("\nPlease Choose From the Menu Your Operation:\n\n\n 1-Select\n 2-Add\n 3-Delete\n 4-Update\n 5-DownLoad\n 6-Exit");
                operation = Console.ReadLine();
                List<FoodGeneralInfo> SelectedItems = new List<FoodGeneralInfo>();
                switch (operation)
                {
                    case "1": SelectedItems = SelectElement(list);
                        PrintSelectedItems(SelectedItems);
                        break;
                    case "2": AddElement(list, jsonpathFile); break;
                    case "3": break;
                    case "4": break;
                    case "5": break;
                    case "6": break;
                    default: Console.WriteLine("The operator should be between [1-6]"); break;
                }

            } while (Convert.ToInt32(operation) < 0 || Convert.ToInt32(operation) > 6 || operation != "6");
        }

        public static void PrintSelectedItems(List<FoodGeneralInfo> list)
        {
            PrintData(list);
        }
    }
}