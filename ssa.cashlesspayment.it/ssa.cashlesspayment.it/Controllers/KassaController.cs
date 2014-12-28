using ssa.cashlesspayment.it.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssa.cashlesspayment.it.Controllers
{
    public class KassaController : Controller
    {
        // GET: Kassa
        public ActionResult Index()
        {
            return View(DAKassa.GetKassas());
        }

        //register Kassa
        //Registreet nieuwe kassa
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //Op de registratieknop geklikt
        [HttpPost]
        public ActionResult Create(string kassanaam, string toestel, string aankoopdatum, string vervaldatum)
        {
            DAKassa.SaveKassa(kassanaam, toestel, aankoopdatum, vervaldatum);

            return RedirectToAction("Index");
        }
    }
}