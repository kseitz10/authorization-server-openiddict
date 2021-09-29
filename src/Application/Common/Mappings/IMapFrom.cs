using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

namespace AuthorizationServer.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}