using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoServiceMongoDB.Model;
using PromoServiceMongoDB.DataAccess;
using PromoServiceMongoDB.DataAccess.Utility;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;


namespace PromoServiceMongoDB.Controllers
{

    [Route("/v1/promotions")]
    [ApiController]
    public class CosmosController : ControllerBase
    {
        ICosmosDataAdapter _adapter;




        /* public CosmosController()
         {
         }*/

        public CosmosController(ICosmosDataAdapter adapter)
        {
            _adapter = adapter;
        }

       

       

        [HttpPost]
        [Route("action")]
        public async Task<IActionResult> CreateAction([FromBody] ProductPromoAction productpromoaction)
        {

            var result = await _adapter.CreateAction("PromoDatabase", "ProductPromoAction", productpromoaction);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] ProductPromo productpromo)
        {
            var result = await _adapter.CreateDocument("PromoDatabase", "ProductPromo", productpromo);
            return Ok(result);
        }

       /* [HttpPut]
        [Route("id/action")]
        public async Task<IActionResult> UpdateAction([FromBody] ProductPromoAction productpromoaction)
        {
            var result = await _adapter.updateDocumentAsyncAction("PromoDatabase", "ProductPromoAction", productpromoaction);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adapter.DeleteUserAsync("PromoDatabase", "ProductPromo", id);
            return Ok(result);
        }*/


        // [HttpGet]
        [FunctionName(nameof(Get))]
        public async Task<IActionResult> Get([HttpTrigger("get")] HttpRequest request)
        {
            var result = await _adapter.GetDataAsync("PromoDatabase", "ProductPromo");
            return Ok(result);
        }

       /* [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _adapter.GetBy("PromoDatabase", "ProductPromo",id);
            return Ok(result);
        }*/

    }

    internal class ApiControllerAttribute : Attribute
    {
    }
}
