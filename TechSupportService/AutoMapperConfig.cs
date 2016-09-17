using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DbAndRepository;
using TechSupportService.DataContract;

namespace TechSupportService
{

    static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cgf => cgf.CreateMap<DbAndRepository.NewTechWorks, TechSupportService.DataContract.NewTechWork>());
        }
    }
}