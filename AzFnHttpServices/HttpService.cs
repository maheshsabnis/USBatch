using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
 
using AzFnHttpServices.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

namespace AzFnHttpServices
{
    /// <summary>
    /// Function App having Multiple Functions
    /// </summary>
    public  class HttpService
    {
        MyCompanyContext ctx;

        public HttpService()
        {
            ctx = new MyCompanyContext ();
        }
        [FunctionName("Get")]
        public  async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get",Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Product> products = await ctx.Products.ToListAsync();

            return new OkObjectResult(products);
        }

        [FunctionName("GetById")]
        public   IActionResult GetById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/{id:int}")] HttpRequest req,int id,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                Product product =  ctx.Products.Find(id);

                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                 return new BadRequestObjectResult($"{ex.Message} {ex.InnerException}");
            }
        }

        [FunctionName("Post")]
        public  async Task<IActionResult> Post(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Read data from Body which is stream using StreamReader

            string bodyData = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the Data into the Product Object

            Product product = JsonSerializer.Deserialize<Product>(bodyData);

            // Perform Insert

            var result = await ctx.Products.AddAsync(product);
            // Commit Transaction to Db
            await ctx.SaveChangesAsync();

            return new OkObjectResult(result.Entity);
        }
        [FunctionName("Put")]
        public async Task<IActionResult> Put(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Products/{id:int}")] HttpRequest req,int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Search Data based on id

            Product product = await ctx.Products.FindAsync(id);
            if (product == null)
                return new NotFoundObjectResult($"Record you are looking for is not found");


            // Else update
            // Read data from Body which is stream using StreamReader

            string bodyData = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the Data into the Product Object

            Product productUpd = JsonSerializer.Deserialize<Product>(bodyData);

            product.ProductName = productUpd.ProductName;
            product.Manufacturere = productUpd.Manufacturere;
            product.Price = productUpd.Price;
            await ctx.SaveChangesAsync();

            return new OkObjectResult(product);
        }

        [FunctionName("Delete")]
        public  async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Products/{id:int}")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            Product product = await ctx.Products.FindAsync(id);
            if (product == null)
                return new NotFoundObjectResult($"Record you are looking for is not found");

            // else delete
            ctx.Products.Remove(product);
            await ctx.SaveChangesAsync();


            return new OkObjectResult("Record is deleted Successfully");
        }
    }
}
