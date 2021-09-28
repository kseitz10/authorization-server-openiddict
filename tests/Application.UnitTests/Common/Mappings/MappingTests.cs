﻿using AutoMapper;
using AuthorizationServer.Application.Common.Mappings;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace AuthorizationServer.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        //[Test]
        //[TestCase(typeof(TodoList), typeof(TodoListDto))]
        //[TestCase(typeof(TodoItem), typeof(TodoItemDto))]
        //public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        //{
        //    var instance = GetInstanceOf(source);

        //    _mapper.Map(instance, source, destination);
        //}

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
