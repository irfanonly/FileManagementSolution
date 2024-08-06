using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManagement.Web.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {

            Response.Cookies.Delete("JwtToken");
            return RedirectToPage("/Index");
        }

        
    }
}
