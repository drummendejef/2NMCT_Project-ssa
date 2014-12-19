using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssa.cashlesspayment.it.Controllers
{
    [Authorize]
    public class OrganisatieController : Controller
    {
        //Index pagina
        //Lijstje met organisaties en knoppen
        public ActionResult Index()
        {
            return View();
        }

        //Register organisatie
        //Registreer nieuwe vereniging
        public ActionResult Register()
        {
            return View();
        }

    }
}
