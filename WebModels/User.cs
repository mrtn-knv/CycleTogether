using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebModels
{
    public class User
    {       
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        public List<Route> Routes { get; set; }
        public List<Guid> Equipments { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
