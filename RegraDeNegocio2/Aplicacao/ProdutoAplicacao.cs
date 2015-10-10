using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorio;
using System.Data.SqlClient;
using System.Data;

namespace Aplicacao
{
    public class ProdutoAplicacao
    {
        private Contexto contexto;

        public void Inserir(Produto produto)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " INSERT INTO PRODUTO(ProdutoNome, Valor, Disponivel, ClienteId) ";
                strQuery += string.Format(" VALUES('{0}', {1}, '{2}', {3}) ", produto.ProdutoNome,
                    produto.Valor, produto.Disponivel, produto.ClienteId);

                contexto.ExecutaComando(strQuery);
            }
        }

        public void Alterar(Produto produto)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE PRODUTO SET ";
                strQuery += string.Format(" ProdutoNome = '{0}', ", produto.ProdutoNome);
                strQuery += string.Format(" Valor = {0}, ", produto.Valor);
                strQuery += string.Format(" Disponivel = '{0}', ", produto.Disponivel);
                strQuery += string.Format(" ClienteId = {0} ", produto.ClienteId);
                strQuery += string.Format(" WHERE ProdutoId = {0} ", produto.ProdutoId);

                contexto.ExecutaComando(strQuery);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM PRODUTO WHERE ProdutoId = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Produto> ListarTodos()
        {
            using (contexto = new Contexto())
            {

                string strQuery = " SELECT * FROM PRODUTO AS P LEFT JOIN CLIENTE C ON(P.ClienteId = C.ClienteId) ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);   
            }
        }

        public Produto ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM PRODUTO AS P LEFT JOIN CLIENTE C ON(P.ClienteId = C.ClienteId) ";
                strQuery += string.Format(" WHERE P.ProdutoId = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Produto> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var produtos = new List<Produto>();

            while (reader.Read())
            {
                Produto produto = new Produto();
                produto.ProdutoId = int.Parse(reader["ProdutoId"].ToString());
                produto.ProdutoNome = reader["ProdutoNome"].ToString();
                produto.Valor = Decimal.Parse(reader["Valor"].ToString());
                produto.Disponivel = Boolean.Parse(reader["Disponivel"].ToString());
                produto.ClienteId = int.Parse(reader["ClienteId"].ToString());

                produto.Cliente = new Cliente();
                produto.Cliente.Nome = reader["Nome"].ToString();
                produto.Cliente.Sobrenome = reader["Sobrenome"].ToString();
                produto.Cliente.Email = reader["Email"].ToString();
                produto.Cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                produto.Cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());

                produtos.Add(produto);
            }
            reader.Close();
            return produtos;
        }
    }
}
