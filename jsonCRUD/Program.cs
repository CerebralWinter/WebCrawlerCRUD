using Newtonsoft.Json;
using Var_WebCrawler_CRUD;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace var.WebCrawler.CRUD
{
    public class Path
    {
        public static string JsonPathOrginal = GetPath();
        public static string NewJsonPath = GetPath();

        public static string GetPath()
        {
            Console.WriteLine("Please Insert the path of the json file: ");
            string filePath = string.Empty;
            do
            {
                filePath = Console.ReadLine();
            } while (string.IsNullOrEmpty(filePath));
            return filePath;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to the Best Json browser in the world! :)))))))))))\n");
            List<FoodGeneralInfo> listOfFood = GetJsonData(Path.JsonPathOrginal);
            MainMenu(listOfFood);
        }

        #region MainMenu(List<FoodGeneralInfo> list) function void
        public static void MainMenu(List<FoodGeneralInfo> list)
        {

            string operation = string.Empty;
            do
            {
                Console.WriteLine("\nPlease Choose From the Menu Your Operation:\n\n\n 1-Select\n 2-Add\n 3-Delete\n 4-Update\n 5-DownLoad\n 6-Exit\n");
                operation = Console.ReadLine();
                List<FoodGeneralInfo> SelectedItems = new List<FoodGeneralInfo>();
                switch (operation)
                {
                    case "1": SelectElement(list); break;
                    case "2": AddElement(list); break;
                    case "3": DeleteElements(list); break;
                    case "4": UpdateElement(list); break;
                    case "5": MakeJsonFile(list); break;
                    case "6":; break;
                    default: Console.WriteLine("The operator should be between [1-6]"); break;
                }

            } while (Convert.ToInt32(operation) < 0 || Convert.ToInt32(operation) > 6 || operation != "6");
        }
        #endregion 
        #region SelectElement(List<FoodGeneralInfo> list) function ---> returns a List<FoodGeneralInfo>
        public static List<FoodGeneralInfo> SelectElement(List<FoodGeneralInfo> list)
        {
            Console.WriteLine("\nPlease select from the menu the Argument you want find :\n 1-Italian Name\n 2-English Name\n 3-Scientific Name\n 4-Category\n 5-Food Code\n 6-All above Arguments\n");
            string command = string.Empty;
            do
            {
                Console.WriteLine("Please Choose from the Menu (1-6): ");
                command = Console.ReadLine().Trim();

            } while (Convert.ToInt32(command) < 0 || Convert.ToInt32(command) > 6);
            Console.WriteLine("Please Insert The searchKey: ");
            string searchKey = Console.ReadLine();
            List<FoodGeneralInfo> SelectedItems = new List<FoodGeneralInfo>();
            switch (command)
            {
                case "1": SelectedItems = list.Where(x => x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList(); break;
                case "2": SelectedItems = list.Where(x => x.EnglishName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList(); break;
                case "3": SelectedItems = list.Where(x => x.ScientificName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList(); break;
                case "4": SelectedItems = list.Where(x => x.Category.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)).ToList(); break;
                case "5": SelectedItems = list.Where(x => x.FoodCode == searchKey).ToList(); break;
                case "6":
                    SelectedItems = list.Where(x => x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || x.ItalianName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || (!string.IsNullOrEmpty(x.ScientificName) && x.ScientificName.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase))
                || x.Category.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase)
                || x.FoodCode == searchKey).ToList(); break;
                default:
                    Console.WriteLine("Please insert a number between 1 - 6 ");
                    SelectedItems = new List<FoodGeneralInfo>(); break;


            }

            PrintData(SelectedItems);
            Console.WriteLine("Do You like to Download the result of select search:  [y][n]?");
            if (Console.ReadLine().Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please Insert the path of the json file you want to save (just the path!!): ");
                string filePath = Path.NewJsonPath;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                Console.WriteLine("Please Insert the name of the file without .json: ");
                string path = $"{filePath}\\{Console.ReadLine()}.json";
                MakeJsonFile(SelectedItems);
                Console.WriteLine($"\nThe file Created successfully in the address : {path}");
            }
            Console.WriteLine("\nYou Are transfering to the Main Menu ...");
            return SelectedItems;
        }
        #endregion
        #region AddElement(List<FoodGeneralInfo> list) function void
        public static void AddElement(List<FoodGeneralInfo> list)
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
                case "2": MainMenu(list); break;
                case "3": break;
            }

        }
        #endregion
        #region UpdateElement(List<FoodGeneralInfo> list) function void
        public static void UpdateElement(List<FoodGeneralInfo> list)
        {
            List<FoodGeneralInfo> selectedItems = SelectElement(list);
        }
        #endregion
        #region DeleteElements(List<FoodGeneralInfo> list) function
        public static void DeleteElements(List<FoodGeneralInfo> list)
        {
            List<FoodGeneralInfo> desiredItemsToDelete = SelectElement(list);
            PrintData(desiredItemsToDelete);
            list = list.Except(desiredItemsToDelete).ToList();
            Console.WriteLine("Do you want to turn back to the main menu? [Y][N] ");
            string op = Console.ReadLine();
            if (op.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            {
                MainMenu(list);
            }
        }
        #endregion
        #region string GetJsonPath() function ---> returns a string
        public static string GetJsonPath()
        {
            Console.WriteLine("Please Insert the path of the json file: ");
            string filePath = string.Empty;
            do
            {
                filePath = Console.ReadLine();
            } while (string.IsNullOrEmpty(filePath));
            return filePath;
        }
        #endregion
        #region GetJsonData(string path)  ---> returns a List<FoodGeneralInfo>
        public static List<FoodGeneralInfo> GetJsonData(string path)
        {

            string jsonString = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(jsonString))
            {
                return JsonConvert.DeserializeObject<List<FoodGeneralInfo>>(jsonString);
            }
            else
            {
                Console.WriteLine("The file does not loaded correctly");
                return new List<FoodGeneralInfo>();
            }

        }
        #endregion
        #region PrintData(List<FoodGeneralInfo> list) function void
        public static void PrintData(List<FoodGeneralInfo> list)
        {

            Console.WriteLine("\nThe search result has " + list.Count + " Elements\n");

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
                                Console.WriteLine($"\n food Name: {item.ItalianName} \nLangual Id: {ele.Id} \n Langual Info: {ele.Info}");
                            }; break;
                        case "7": break;
                        default: Console.WriteLine("You did not choose [1-7]"); break;
                    }
                }

            } while (op != "7" || (Convert.ToInt32(op) < 1 && Convert.ToInt32(op) > 8));

            

        }
        #endregion
        #region MakeJsonFileList<FoodGeneralInfo> list, string path) function void
        public static void MakeJsonFile(List<FoodGeneralInfo> list)
        {

            List<FoodGeneralInfo> listOfFoodOrginal = GetJsonData(Path.JsonPathOrginal);
            if (list.Count != listOfFoodOrginal.Count)
            {
                Console.WriteLine($" this json file is defferent rispect To original file\n Do you want to the file? [y][n] ");
                string op = Console.ReadLine();
                if (op.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    var option = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(list, option);
                    File.WriteAllText(Path.NewJsonPath,jsonString);
                }
                else { Console.WriteLine("No modification to the source json file\n Best wishes for you\n Hope to see you Again!"); }
            }
        }
        #endregion
    }
}