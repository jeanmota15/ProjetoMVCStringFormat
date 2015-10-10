using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;
using Dominio.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ClienteAplicacao aplicacao;

        public HomeController()
        {
            aplicacao = new ClienteAplicacao();
        }

        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public ActionResult About()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //ViewBag.Message = "Your application description page.";
            var cliente = aplicacao.ListarTodos();

            var model = from p in cliente
                        group p by p.DataCadastro into grupo
                        select new ClienteEstatistica()
                        {
                            Data = grupo.Key,
                            Contador = grupo.Count()
                        };

            return View(model);
        }

        public ActionResult Contact()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}