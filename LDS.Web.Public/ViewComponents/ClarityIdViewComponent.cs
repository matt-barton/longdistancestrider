using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.ViewComponents;

public class ClarityIdViewComponent (IConfiguration config) : ViewComponent
{
    public async Task<string> InvokeAsync()
    {
        return config["LDS_CLARITY_ID"]!;
    }
}