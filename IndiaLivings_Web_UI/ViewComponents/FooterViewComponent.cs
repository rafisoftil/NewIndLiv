using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class FooterViewComponent : ViewComponent
{
    private readonly IMemoryCache _cache;

    public FooterViewComponent(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Get footer data from cache or API
        var footerData = await _cache.GetOrCreateAsync("FOOTER_DATA", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);

            // API / DB call (ONLY once)
            return await new CompanySetupViewModel().GetCompanySetupById(1);
        });

        // Reuse your existing partial view
        return View("_Footer", footerData);
    }
}