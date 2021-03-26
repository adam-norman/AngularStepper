using App.Models;
using AutoMapper;
using ParentDetailsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParentDetailsApp.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
             
            CreateMap<StepVM, Step>().ForMember(s => s.Items, opt => opt.Ignore()).ReverseMap();
            CreateMap<Step, StepVM>().ReverseMap();
            
        }
    }
}
