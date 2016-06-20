using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace ConsoleWepApi
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunAsync().Wait();
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:40784/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //MyAPIGetById(cons).Wait();
            //MyAPIDelete(cons).Wait();
            //MyAPIPut(cons).Wait();
            Console.Write("product name giriniz:");
            string productName = Console.ReadLine();             
            MyAPIPost(cons, productName).Wait();
        }
        static async Task MyAPIGetById(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Product/2");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    ProductEntity tag = await res.Content.ReadAsAsync<ProductEntity>();
                    Console.WriteLine("\n");
                    Console.WriteLine("---------------------Calling Get Operation------------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("productId    productName         ");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("{0}\t{1}\t\t", tag.ProductId, tag.ProductName);
                    Console.ReadLine();
                }
            }
        }
        static async Task MyAPIDelete(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Product/1007");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    res = await cons.DeleteAsync("api/Product/1007");
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Delete Operation--------------------");
                    Console.WriteLine("------------------Deleted-------------------");
                    Console.ReadLine();
                }
            }
        }
        static async Task MyAPIPut(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Product/1006");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    ProductEntity tag = await res.Content.ReadAsAsync<ProductEntity>();
                    tag.ProductName = "New Tag";
                    res = await cons.PutAsJsonAsync("api/Product/1006", tag);
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Put Operation--------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("tagId    tagName     ");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("{0}\t{1}\t", tag.ProductId, tag.ProductName);
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// httpclient ve value alır.
        /// </summary>
        /// <param name="cons"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        static async Task MyAPIPost(HttpClient cons, string productName)
        {
            using (cons)
            {
                var tag = new ProductEntity() { ProductName = productName };
                HttpResponseMessage res = await cons.PostAsJsonAsync("api/Product", tag);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Post Operation--------------------");
                    Console.WriteLine("------------------Created Successfully--------------------");
                    Console.ReadLine();
                }
            }
        }
    }
}
