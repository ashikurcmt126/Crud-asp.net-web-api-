using System;
namespace ApiCrudCore.Dtos
{
	public class TodoReadDto
	{
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool Completed { get; set; }
    }
}

