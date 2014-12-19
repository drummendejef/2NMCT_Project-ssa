using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ssa.cashlesspayment.it.Models
{
    public class Organisatie
    {
        //Properties
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [StringLength(100, ErrorMessage= "The {0} moet minstens {2} karakters lang zijn.", MinimumLength= 6)]
        public string Password { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        public string DbLogin { get; set; }
        [Required]
        public string DbPassword { get; set; }
        [Required]
        public string OrganisatieNaam { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Telefoonnr { get; set; }
    }
}