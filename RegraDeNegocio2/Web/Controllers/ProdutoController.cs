using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;


namespace Web.Controllers
{
    public class ProdutoController : Controller
    {
        private ClienteAplicacao appCliente;
        private ProdutoAplicacao appProduto;

        public ProdutoController()
        {
            appCliente = new ClienteAplicacao();
            appProduto = new ProdutoAplicacao();
        }

        public ActionResult Index(string pesquisa, string ordem)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appProduto.ListarTodos();

            ViewBag.Nome = string.IsNullOrEmpty(ordem) ? "Nome_Desc" : "Nome";
            ViewBag.Data = ordem == "Date" ? "Data_Desc" : "Date";

            if (!string.IsNullOrEmpty(pesquisa))
            {
                lista = lista.Where(x => x.Cliente.Nome.ToUpper().Contains(pesquisa.ToUpper()) ||
                x.ProdutoNome.ToUpper().Contains(pesquisa.ToUpper())).ToList();
            }

            switch (ordem)
            {
                case "Nome_Desc":
                    lista = lista.OrderByDescending(x => x.Cliente.Nome).ToList();
                    break;
                case "Nome":
                    lista = lista.OrderBy(x => x.Cliente.Nome).ToList();
                    break;
                case "Data_Desc":
                    lista = lista.OrderByDescending(x => x.Cliente.DataCadastro).ToList();
                    break;
                case "Date":
                    lista = lista.OrderBy(x => x.Cliente.DataCadastro).ToList();
                    break;
                default:
                    lista = lista.OrderByDescending(x => x.Cliente.DataCadastro).ToList();
                    break;
            }

            return View(lista);
        }

        public ActionResult Index2(string pesquisa)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appProduto.ListarTodos();

            ViewBag.Nome = (from c in lista
                           select c.Cliente.Nome).Distinct();

            var model = from c in lista
                        orderby c.Cliente.Nome
                        where c.Cliente.Nome == pesquisa
                        select c;

            return View(model);
        }

     
        public ActionResult Detalhes(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = appProduto.ListarPorId(id);

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

            ViewBag.ClienteId = new SelectList(appCliente.ListarTodos(), "ClienteId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                appProduto.Inserir(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ClienteId = new SelectList(appCliente.ListarTodos(), "ClienteId", "Nome");
                return View(produto);
            }
        }

        public ActionResult Editar(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = appProduto.ListarPorId(id);

            if (listaId == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.ClienteId = new SelectList(appCliente.ListarTodos(), "ClienteId", "Nome");
                return View(listaId);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                appProduto.Alterar(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ClienteId = new SelectList(appCliente.ListarTodos(), "ClienteId", "Nome");
                return View(produto);
            }
        }

        public ActionResult Excluir(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaId = appProduto.ListarPorId(id);

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
            appProduto.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
