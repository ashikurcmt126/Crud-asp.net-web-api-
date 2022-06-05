using System;
using System.Net;
using ApiCrudCore.Data;
using ApiCrudCore.Dtos;
using ApiCrudCore.Enums;
using ApiCrudCore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiCrudCore.Controllers
{
	[Route("api/todos/[controller]")]
	public class TodoController: ControllerBase
	{
		private readonly ITodoRepo _repo;
		private readonly IMapper _mapper;

		public TodoController(ITodoRepo repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetTodos([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
		{
			var todos = await _repo.FetchMany(TodoShow.All);
			return new OkObjectResult(todos);
		}

		[HttpGet("/get-all")]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> GetAllTodos()
        {
            var todos = await _repo.GetAllTodos();
            return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todos));
        }

		[HttpGet]
		[Route("pending")]
		public async Task<IActionResult> GetPending([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
			var todos = await _repo.FetchMany(TodoShow.Pending);
			return new OkObjectResult(todos);
        }

		[HttpGet]
		[Route("completed")]
		public async Task<IActionResult> GetCompleted([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
			var todos = await _repo.FetchMany(TodoShow.Completed);
			return new OkObjectResult(todos);
        }

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetTodoDetails(int id)
        {
			var todo = await _repo.GetById(id);
			return new OkObjectResult(todo);
        }

		[HttpPost]
		[Route("create-todo")]
		public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
			await _repo.CreateTodo(todo);
			var response = new ObjectResult(todo);
			response.StatusCode = (int)HttpStatusCode.Created;
			return response;
        }

		[HttpPut]
		[Route("update-todo")]
		public async Task<IActionResult> UpdateTodo(int id, [FromBody] Todo todo)
        {
			var todoFromDb = await _repo.GetById(id);

			if(todoFromDb == null)
            {
				return NotFound(Newtonsoft.Json.JsonConvert.SerializeObject(new
				{
					Success = false,
					FullMessage = new string[]
                    {
						"Not Found"
                    }
				}));
            }
            else
            {
				return new OkObjectResult(await _repo.UpdateTodo(todoFromDb, todo));
            }
        }

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteTodo(int id)
        {
			var todoFromDb = await _repo.GetById(id);
			if (todoFromDb == null)
			{
				return NotFound(Newtonsoft.Json.JsonConvert.SerializeObject(new
				{
					Success = false,
					FullMessage = new string[]
					{
						"Not Found"
					}
				}));
            }
            else
            {
				EntityEntry<Todo> result = _repo.DeleteTodo(id);
				Console.WriteLine(result);
            }

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAllTodo()
        {
			await _repo.DeleteAllTodo();
			return new NoContentResult();
        }
	}
}
