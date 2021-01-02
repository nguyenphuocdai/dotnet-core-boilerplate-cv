using System;

namespace AspNetCoreHero.Application.Features.CurriculumVitae.Queries.GetAll
{
    public class GetAllCurriculumVitaeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
