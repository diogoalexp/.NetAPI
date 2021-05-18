using AutoMapper;
using SampleDAL;
using SampleDAL.Repositories;
using SampleDAL.Repositories.Base;
using SampleDAL.Repositories.Interfaces;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Services;
using SampleService.Services.Base;
using SampleService.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SampleService
{
    public static class MapperControl
    {
        private static Dictionary<Type, Type> GetMapperTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(AuthDTO), typeof(Auth) },
                { typeof(AuthRequestDTO), typeof(Auth) },
                { typeof(AuthResponseDTO), typeof(Auth) },
                { typeof(RegisterResponseDTO), typeof(Auth) },
                { typeof(PersonDTO), typeof(Person) },
                { typeof(CreatePersonDTO), typeof(Person) },
                { typeof(UpdatePersonDTO), typeof(Person) },
            };
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                foreach (var item in GetMapperTypes())
                {
                    cfg.CreateMap(item.Key, item.Value);
                    cfg.CreateMap(item.Value, item.Key);
                }
            });
            return config;
        }


    }
}
