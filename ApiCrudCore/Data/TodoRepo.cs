using System;
using ApiCrudCore.Enums;
using ApiCrudCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiCrudCore.Data
{
	public class TodoRepo: ITodoRepo
	{
        private readonly AppDbContext _context;

        public TodoRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTodo(Todo todo)
        {
            await _context.todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Todo>> FetchMany(TodoShow show = TodoShow.All)
        {
            IQueryable<Todo> queryable = null;

            if (show == TodoShow.Completed)
            {
                queryable = _context.todos.Where(t => t.Completed);
            }
            else if(show == TodoShow.Pending)
            {
                queryable = _context.todos.Where(t => !t.Completed);
            }

            List<Todo> todos;
            if(queryable != null)
            {
                todos = await queryable.Select(t => new Todo
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Completed = t.Completed
                }).ToListAsync();
            }
            else
            {
                todos = await _context.todos.Select(t => new Todo
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Completed = t.Completed
                }).ToListAsync();
            }

            return todos;
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _context.todos!.ToListAsync();
        }

        public async Task<Todo> GetById(int id)
        {
           return await _context.todos.FirstOrDefaultAsync(t=> t.Id == id);
        }

        public async Task<Todo> UpdateTodo(Todo currentTodo, Todo todoFromUser)
        {
            currentTodo.Title = todoFromUser.Title;
            currentTodo.Completed = todoFromUser.Completed;

            if(todoFromUser.Description != null)
            {
                currentTodo.Description = todoFromUser.Description;
            }

            _context.Entry(currentTodo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return currentTodo;
        }

        public async Task<Todo> Get(int todoId) => await _context.todos.FindAsync(todoId);

        public EntityEntry<Todo> DeleteTodo(int todoId)
        {
            EntityEntry<Todo> result = _context.todos.Remove(Get(todoId).Result);
            _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAllTodo()
        {
            _context.todos.RemoveRange(_context.todos);
            await _context.SaveChangesAsync();
        }
    }
}

