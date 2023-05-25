using Newtonsoft.Json;
using Var_WebCrawler_CRUD;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace var.WebCrawler.CRUD
{

    public class Global
    {
        public static string OriginaljsonFile;
    }
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to the Best Json browser in the world! :)))))))))))\n");
            Global.OriginaljsonFile = CrudUtility.GetJsonPath();
            List<FoodGeneralInfo> listOfFood = CrudUtility.GetJsonData(Global.OriginaljsonFile);
            CrudUtility.MainMenu(listOfFood);
        }

    }
}