using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseApiController
    {
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrEmpty(term)) return null;
            var categories = await Mediator.Send(new GetAllProductCategoriesQuery());
            var data = categories.Data.Where(a => a.Id.ToString().Contains(term, StringComparison.OrdinalIgnoreCase) 
                                               || a.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
            return Ok(data);
        }
    }
}
