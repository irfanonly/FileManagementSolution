using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FileManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public string ErrorMessage { get; set; }

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            var client = _clientFactory.CreateClient("BackendAPI");
            var loginData = new { Username = username, Password = password };
            var response = await client.PostAsJsonAsync($"/api/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var token = JsonSerializer
                    .Deserialize<TokenResponse>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?
                    .Token;

                if (token != null)
                {
                    Response.Cookies.Append("JwtToken", token,
                        new CookieOptions { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddMinutes(30) });
                    return RedirectToPage("/Index");
                }
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
