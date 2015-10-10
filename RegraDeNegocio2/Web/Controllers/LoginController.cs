using Aplicacao;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private LoginAplicacao aplicacao;

        public LoginController()
        {
            aplicacao = new LoginAplicacao();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string senha)
        {
            var acesso = aplicacao.Logar(usuario, senha);

            if (acesso != null)
            {
                FormsAuthentication.SetAuthCookie(usuario, false);
                Session["Usuario"] = acesso;
                return RedirectToAction("Index","Home");
            }
            ViewBag.Mensagem = "Usuário e/ou Senha Inválidos!";
            return View(acesso);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
