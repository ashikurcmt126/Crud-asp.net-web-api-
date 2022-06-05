using System;
using ApiCrudCore.Dtos;
using ApiCrudCore.Models;
using AutoMapper;

namespace ApiCrudCore.Profiles
{
	public class TodoProfiles: Profile
	{
		public TodoProfiles()
		{
			// Source -> Target
			CreateMap<Todo, TodoReadDto>();
			CreateMap<TodoCreateDto, Todo>();
			CreateMap<TodoUpdateDto, Todo>();
			CreateMap<Todo, TodoUpdateDto>();
		}
	}
}

