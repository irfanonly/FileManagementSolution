using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace FileManagement.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public string? Token { get; private set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void OnGet()
        {
            Token = Request.Cookies["JwtToken"];
        }

        public async Task<IActionResult> OnGetDownloadFileAsync()
        {
            var client = _clientFactory.CreateClient("BackendAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["JwtToken"]);

            var response = await client.GetAsync($"/api/file/getfile");

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                return File(fileBytes, "application/octet-stream", "example.txt");
            }

            return RedirectToPage("/Error");
        }
    }
}
