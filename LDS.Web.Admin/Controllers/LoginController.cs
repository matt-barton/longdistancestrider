using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task Login()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = result
            .Principal
            .Identities
            .FirstOrDefault()
            .Claims
            .Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    public async Task Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "/Home"
        };
        // after signout this will redirect to your provided target
        await HttpContext.SignOutAsync("Cookies", prop);
        
    }
    
}