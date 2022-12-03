using ClientSide.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace ClientSide.Shared
{
    public partial class ImageUpload
    {
        [Parameter]
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> Onchange { get; set; }
        [Inject]
        public IProductHttpService Repo { get; set; }   
        private async Task HandleSelected(InputFileChangeEventArgs e)
        {
            var imageFiles=e.GetMultipleFiles();    
            foreach (var file in imageFiles)
            {
                if (file != null)
                {
                    var resizedFile = await file.RequestImageFileAsync("image/png", 300, 500);
                    using (var ms =  resizedFile.OpenReadStream(resizedFile.Size))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms,Convert.ToInt32(resizedFile.Size)),"image",file.Name);

                        ImgUrl = await Repo.UploadProductImage(content);
                        await Onchange.InvokeAsync(ImgUrl); 
                    }
                }
            }
        }
    }
}
