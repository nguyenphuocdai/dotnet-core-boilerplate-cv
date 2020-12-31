using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderPartialToStringAsync<T>(string viewName, T model);
    }
}
