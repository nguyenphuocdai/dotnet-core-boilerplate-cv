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
        public long DateTimeCreate { get; set; }
        public long DateTimeModify { get; set; }
        public int UserIdCreate { get; set; }
        public int UserIdModify { get; set; }
    }
}
