using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Application.Commands.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Domain.Category>();

            CreateMap<RenameCategoryCommand, Domain.Category>()
                .ForMember(x => x.Id, m => m.Ignore())
                .ForMember(x => x.Label, m => m.MapFrom(x => x.NewLabel));
        }
    }
}