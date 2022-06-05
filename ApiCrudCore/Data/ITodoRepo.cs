using System;
using ApiCrudCore.Enums;
using ApiCrudCore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiCrudCore.Data
{
	public interface ITodoRepo
	{
		Task<IEnumerable<Todo>> GetAllTodos();
		Task<List<Todo>> FetchMany(TodoShow show = TodoShow.All);
		Task<Todo> GetById(int id);
		Task CreateTodo(Todo todo);
		Task<Todo> UpdateTodo(Todo currentTodo, Todo todoFromUser);
		EntityEntry<Todo> DeleteTodo(int todoId);
		Task DeleteAllTodo();
	}
}

