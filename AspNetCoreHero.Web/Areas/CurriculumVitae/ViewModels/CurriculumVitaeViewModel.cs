using AspNetCoreHero.Library.Enum;
using System;

namespace AspNetCoreHero.Web.Areas.CurriculumVitae.ViewModels
{
    public class CurriculumVitaeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int TemplatePreviewID { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string CreatedDisplay => Created.ToString(PatternEnum.DateTimeDisplay);
    }
}
