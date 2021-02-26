using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;


namespace FrontEndMVC.Adapters
{
    public class BarcodeAdapter
    {
        public class Store
        {
            public String store_name { get; set; }
            public String store_price { get; set; }
            public String product_url { get; set; }
            public String currency_code { get; set; }
            public String currency_symbol { get; set; }
        }

        public class Review
        {
            public String name { get; set; }
            public String rating { get; set; }
            public String title { get; set; }
            public String review { get; set; }
            public String datetime { get; set; }
        }

        public class Product
                    {
                    public String barcode_number { get; set; }
                    public String barcode_type { get; set; }
                    public String barcode_formats { get; set; }
                    public String mpn { get; set; }
                    public String model { get; set; }
                    public String asin { get; set; }
                    public String product_name { get; set; }
                    public String title { get; set; }
                    public String category { get; set; }
                    public String manufacturer { get; set; }
                    public String brand { get; set; }
                    public String label { get; set; }
                    
                    public String size { get; set; }
                    
                    public String description { get; set; }
                    
                    public IList<string> images { get; set; }
                    public IList<Store> stores { get; set; }
                    public IList<Review> reviews { get; set; }
                }

        public class RootObject
                {
                    public IList<Product> products { get; set; }
                }
                class Program
                {
                    static void Main(string[] args)
                    {
                        using (WebClient webClient = new System.Net.WebClient())
                        {
                            WebClient n = new WebClient();
                            string api_key = "ENTER_YOUR_API_KEY_HERE";
                            string url = "https://api.barcodelookup.com/v2/products?barcode=077341125112&formatted=y&key=" + api_key;
                            var data = n.DownloadString(url);
                            var name = JsonConvert.DeserializeObject<RootObject>(data).products[0].product_name;
                            System.Console.Write("Product Name: ");
                            System.Console.WriteLine(name);

                            var barcode = JsonConvert.DeserializeObject<RootObject>(data).products[0].barcode_number;
                            System.Console.Write("Barcode Number: ");
                            System.Console.WriteLine(barcode);

                            System.Console.WriteLine("Entire Response: ");
                            System.Console.WriteLine(data);
                        }
                    }
                }

            }
        }
    
