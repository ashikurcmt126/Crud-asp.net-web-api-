using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCrudCore.Dtos
{
	public class TodoCreateDto
	{
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}

