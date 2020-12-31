using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Create;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Update;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Web.Areas.CurriculumVitae.ViewModels;
using AspNetCoreHero.Web.Helpers;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.CurriculumVitae.Pages
{
    public class IndexModel : HeroPageModel<IndexModel>
    {
        public void OnGet()
        {
        }

        public IEnumerable<CurriculumVitaeViewModel> CurriculumVitaes { get; set; }

        public async Task<PartialViewResult> OnGetViewAll()
        {
            //CurriculumVitaes = new List<CurriculumVitaeViewModel>();

            var response = await Mediator.Send(new GetAllProductCategoriesQuery());
            CurriculumVitaes = new List<CurriculumVitaeViewModel>();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<CurriculumVitaeViewModel>>(ViewData, CurriculumVitaes)
            };
        }

        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            var viewModel = new CurriculumVitaeViewModel();
            return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync("_CreateOrEdit", viewModel) });
        }


        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, CurriculumVitaeViewModel curriculumVitae)
        {
            string html;

            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        User.HasRequiredClaims(new List<string> { MasterPermissions.Create, CurriculumVitaePermissions.Create });
                        var createProductCommand = Mapper.Map<CreateProductCategoryCommand>(curriculumVitae);
                        var result = await Mediator.Send(createProductCommand);
                        if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Created.");
                    }
                    else
                    {
                        User.HasRequiredClaims(new List<string> { MasterPermissions.Update, CurriculumVitaePermissions.Update });
                        var updateProductCommand = Mapper.Map<UpdateProductCategoryCommand>(curriculumVitae);

                        try
                        {
                            var result = await Mediator.Send(updateProductCommand);
                            if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Updated.");
                        }
                        catch (Exception ex)
                        {
                            Logger.LogInformation(ex.Message, ex);
                            throw;
                        }

                    }
                    var response = await Mediator.Send(new GetAllProductCategoriesQuery());
                    if (response.Succeeded)
                    {
                        var data = response.Data;
                        CurriculumVitaes = Mapper.Map<IEnumerable<CurriculumVitaeViewModel>>(data);
                    }
                    html = await Renderer.RenderPartialToStringAsync("_ViewAll", CurriculumVitaes);
                    return new JsonResult(new { isValid = true, html = html });
                }
                catch (Exception ex)
                {

                    Notify.AddErrorToastMessage(ex.Message);
                    throw;
                }

            }

            html = await Renderer.RenderPartialToStringAsync<CurriculumVitaeViewModel>("_CreateOrEdit", curriculumVitae);
            return new JsonResult(new { isValid = false, html = html });
        }
    }
}
