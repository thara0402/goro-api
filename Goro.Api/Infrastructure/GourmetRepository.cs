using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goro.Api.Infrastructure.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Spatial;

namespace Goro.Api.Infrastructure
{
    public class GourmetRepository : RepositoryBase<GourmetEntity>
    {
        public GourmetRepository(DocumentClient documentClient)
            : base(documentClient, "GourmetDatabase", "GourmetCollection")
        {
        }

        public async Task<IEnumerable<GourmetEntity>> GetAllAsync()
        {
            var documentQuery = Client.CreateDocumentQuery<GourmetEntity>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                                        //.Where(x => x.Season == 3)
                                        .OrderBy(x => x.Episode)
                                        .AsDocumentQuery();

            var results = new List<GourmetEntity>();
            while (documentQuery.HasMoreResults)
            {
                results.AddRange(await documentQuery.ExecuteNextAsync<GourmetEntity>());
            }

            return results;
        }

    }
}