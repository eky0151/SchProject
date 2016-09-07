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
            Mapper.Initialize(cgf => cgf.CreateMap<DbAndRepository.Technician, TechSupportService.Entities.Technician>());
            Mapper.Initialize(cgf => cgf.CreateMap<DbAndRepository.Worker, TechSupportService.Entities.Worker>());
            Mapper.Initialize(cgf => cgf.CreateMap<DbAndRepository.LoginData, TechSupportService.Entities.LoginData>());
        }
    }
}