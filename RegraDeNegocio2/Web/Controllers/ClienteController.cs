using Aplicacao;
using Dominio;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteAplicacao aplicacao;

        public ClienteController()
        {
            aplicacao = new ClienteAplicacao();
        }

        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index","Login");
            }

            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Especiais()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ClientesEspeciais();
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = aplicacao.ListarPorId(id);

            if (listaId == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaId);
            }       
        }

        public ActionResult Cadastrar()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Inserir(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }

        public ActionResult Editar(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = aplicacao.ListarPorId(id);

            if (listaId == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaId);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Alterar(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }

        public ActionResult Excluir(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = aplicacao.ListarPorId(id);

            if (listaId == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaId);
            }
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
