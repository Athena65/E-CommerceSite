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

        public async Task DeleteProduct(Guid id)
        {
            var url = Path.Combine("Delete",id.ToString());
            var deleteResult= await _client.DeleteAsync(url);
            var deleteContent= await deleteResult.Content.ReadAsStringAsync();
            if(!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
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

        public async Task<Product> GetProductById(string id)
        {
            var url = Path.Combine("GetById", id);
            var response= await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var product= JsonSerializer.Deserialize<Product>(content,_options);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent= new StringContent(content,Encoding.UTF8,"application/json");
            var url = Path.Combine("Update", product.Id.ToString());

            var putResult= await _client.PutAsync(url, bodyContent);    
            var putContent=await putResult.Content.ReadAsStringAsync(); 

            if(!putResult.IsSuccessStatusCode) 
            {
                throw new ApplicationException(putContent);
            }
            return product;
        

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
