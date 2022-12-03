using ClientSide.Features;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace ClientSide.Services
{
    public class ProductHttpService : IProductHttpService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;


        public ProductHttpService(HttpClient client)
        {
            _client = client;
            _options=new JsonSerializerOptions { PropertyNameCaseInsensitive= true };
        }

        public async Task<Product> CreateProduct(Product newProduct)
        {
            var content= JsonSerializer.Serialize(newProduct);
            var ContentBody=new StringContent(content,Encoding.UTF8,"application/json");

            var postResult = await _client.PostAsync("Create", ContentBody);
            var postContent= await postResult.Content.ReadAsStringAsync();

            if(!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            return newProduct;
        }

        public async Task<PagingResponse<Product>> GetAllProducts(ProductParameters productParameters)
        {
            var queryStringParameters= new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"]=productParameters.SearchTerm==null? "":productParameters.SearchTerm,
                ["orderBy"]=productParameters.OrderBy
                   
            };    
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("GetAll",queryStringParameters));
            var content = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Product>
            {
                Items = JsonSerializer.Deserialize<List<Product>>(content, _options),
                MetaData=JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),_options)
            };

            return pagingResponse;
        }

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("https://localhost:5011/Upload", content);
            var postContent= await postResult.Content.ReadAsStringAsync();  
            if(!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);    
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return imgUrl;
            }
        }
    }
}
