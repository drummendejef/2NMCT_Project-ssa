using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssa.cashlesspayment.it.Models
{
    public class Organisatie
    {
        //Properties
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
        public string DbLogin { get; set; }
        public string OrganisatieNaam { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Telefoonnr { get; set; }
    }
}