using nmct.ba.cashlessproject.model;
using ssa.cashlesspayment.it.DataAcces;
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
            return View(DAOrganisatie.GetOrganisaties());
        }

        //Register organisatie
        //Registreer nieuwe vereniging
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Op de registratieknop geklikt
        [HttpPost]
        public ActionResult Create(string Login, string Password, string DbName, string DbLogin, string DbPassword, string OrganisatieNaam, string Adres, string Email, string Telefoonnr)
        {
            //Organisatie opslaan in DB en aanmaken DB
            DAOrganisatie.SaveOrganisatie(Login, Password, DbName, DbLogin, DbPassword, OrganisatieNaam, Adres, Email, Telefoonnr);

            return RedirectToAction("Index");
        }

        //TODO
        //Bewerken
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Organisatie organisatie = DAOrganisatie.GetOrganisatie(id.Value);
            if (organisatie == null)
                return RedirectToAction("Index");

            return View(organisatie);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteById(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            DAOrganisatie.DeleteOrganisatie(id.Value);

            return RedirectToAction("Index");
        }


    }
}
