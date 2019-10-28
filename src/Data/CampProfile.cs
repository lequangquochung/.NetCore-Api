using AutoMapper;
using CoreCodeCamp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModels>()
                .ForMember(c => c.VenueName, o => o.MapFrom(m => m.Location.VenueName));
        }
    }
}
