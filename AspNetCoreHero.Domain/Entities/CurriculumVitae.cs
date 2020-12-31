using AspNetCoreHero.Domain.Common;
namespace AspNetCoreHero.Domain.Entities
{
    public class CurriculumVitae : AuditableEntityBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TemplatePreviewID { get; set; }
        public bool IsPublished { get; set; }
    }

}
