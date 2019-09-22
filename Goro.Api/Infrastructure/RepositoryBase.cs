using System.Net;
using System.Threading.Tasks;
using Goro.Api.Infrastructure.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Goro.Api.Infrastructure
{
    public abstract class RepositoryBase<TDocument> where TDocument : DocumentBase
    {
        protected string DatabaseId { get; }
        protected string CollectionId { get; }
        protected DocumentClient Client { get; }

        protected RepositoryBase(DocumentClient documentClient, string databaseId, string collectionId)
        {
            DatabaseId = databaseId;
            CollectionId = collectionId;
            Client = documentClient;

            // _φ(･_･
            // パーティショニング、同時実行制御、ページングを考慮する必要あり
            //  https://github.com/Azure-Samples/documentdb-dotnet-todo-app/blob/master/src/DocumentDBRepository.cs
        }

        public async Task CreateAsync(TDocument document)
        {
            var response = await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), document);
            document.Id = response.Resource.Id;
        }

        public async Task<TDocument> GetAsync(string id)
        {
            try
            {
                var response = await Client.ReadDocumentAsync<TDocument>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                return response.Document;
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return default(TDocument);
            }
        }

        public async Task UpdateAsync(TDocument document)
        {
            try
            {
                await Client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, document.Id), document);
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                // optimistic concurrency に失敗
                throw;
            }
        }

        public async Task DeleteAsync(TDocument document)
        {
            try
            {
                await Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, document.Id));
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                // optimistic concurrency に失敗
                throw;
            }
        }        

    }
}