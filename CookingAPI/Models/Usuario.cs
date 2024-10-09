﻿using System.ComponentModel.DataAnnotations;

namespace CookingAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
