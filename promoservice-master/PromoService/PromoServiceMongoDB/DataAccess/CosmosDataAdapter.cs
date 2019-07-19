using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using PromoServiceMongoDB.Model;

namespace PromoServiceMongoDB.DataAccess.Utility
{
    public class CosmosDataAdapter : ICosmosDataAdapter
    {


        private readonly DocumentClient _client;
        private readonly string _accountUrl;
        private readonly string _primarykey;

        public CosmosDataAdapter(ICosmosConnection connection, IConfiguration config)
        {

            _accountUrl = config.GetValue<string>("Cosmos:AccountURL");
            _primarykey = config.GetValue<string>("Cosmos:AuthKey");
            _client = new DocumentClient(new Uri(_accountUrl), _primarykey);
        }

        public async Task<bool> CreateDocument(string dbName, string name, ProductPromo productpromo)
        {
            try
            {
               // productpromo.Id = "d9e51c1e-1474-41d1-8f32-96deedd8f36a";
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productpromo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateAction(string dbName, string name, ProductPromoAction productpromoaction)
        {
            try
            {
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productpromoaction);
                return true;
            }
            catch
            {
                return false;
            }
        }

       /* public Task updateDocumentAsync(string v1, string v2, ProductPromo productpromo)
        {
            try
            {
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productPromoCondition);
                return true;
            }
            catch
            {
                return false;
            }
        }*/

       public async Task<bool> updateDocumentAsyncAction(string dbName, string name, ProductPromoAction productpromoaction)
        {
            try
            {
                var result = await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productpromoaction);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<ProductPromo> DeleteUserAsync(string dbName, string name, string id)
        {
            var collectionUri = UriFactory.CreateDocumentUri(dbName, name, id);

            var result = await _client.DeleteDocumentAsync(collectionUri);

            return (dynamic)result.Resource;
        }

       

       /* public async Task<FeedResponse<dynamic>> GetBy(string dbName, string name,string id)
        {
            var document = UriFactory.CreateDocumentUri(dbName, name, id);
           
            var result = await _client.ReadDocumentFeedAsync(document, new FeedOptions { MaxItemCount = 1 });

            return result;
        }*/



        async Task<dynamic> ICosmosDataAdapter.GetDataAsync(string dbName, string name)
        {
            var result = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name),
                    new FeedOptions { MaxItemCount = 10 });

            return result;
        }

        public Task<FeedResponse<dynamic>> GetBy(string dbName, string name, string id)
        {
            throw new NotImplementedException();
        }

       

      



        /* public async Task<dynamic> GetDataAsync(string dbName, string name)
         {
             *//*try
             {

                 var result = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name),
                     new FeedOptions { MaxItemCount = 10 });

                 return result;
             }
             catch (Exception ex)
             {
                 return false;
             }*//*
         }*/





        /*  public Task<IEnumerable<ProductPromo>> Get()
          {
              throw new NotImplementedException();
          }

          public Task<ProductPromo> Get(string id)
          {
              throw new NotImplementedException();
          }
  */





    }
}
