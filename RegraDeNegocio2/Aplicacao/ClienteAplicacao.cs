using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorio;
using System.Data;
using System.Data.SqlClient;

namespace Aplicacao
{
    public class ClienteAplicacao
    {
        private Contexto contexto;

        public void Inserir(Cliente cliente)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " INSERT INTO CLIENTE(Nome, Sobrenome, Email, DataCadastro, Ativo) ";                  
                strQuery += string.Format(" VALUES('{0}', '{1}', '{2}', '{3}', '{4}') ", cliente.Nome, 
                    cliente.Sobrenome, cliente.Email, cliente.DataCadastro, cliente.Ativo);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Alterar(Cliente cliente)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE CLIENTE SET ";
                strQuery += string.Format(" Nome = '{0}', ", cliente.Nome);
                strQuery += string.Format(" Sobrenome = '{0}', ", cliente.Sobrenome);
                strQuery += string.Format(" Email = '{0}', ", cliente.Email);
                strQuery += string.Format(" DataCadastro = '{0}', ", cliente.DataCadastro);
                strQuery += string.Format(" Ativo = '{0}' ", cliente.Ativo);
                strQuery += string.Format(" WHERE ClienteId = {0} ", cliente.ClienteId);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM CLIENTE WHERE ClienteId = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Cliente> ListarTodos()
        {
            using (contexto = new Contexto())
            {
              
                    string strQuery = " SELECT * FROM CLIENTE ";
                    var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                    return TransformaDataReaderEmLista(retorno); 
            }
        }

        public Cliente ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM CLIENTE WHERE ClienteId = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public List<Cliente> ClientesEspeciais()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM CLIENTE WHERE Ativo = 1 AND GetDate() - DataCadastro >= 5 ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        private List<Cliente> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var clientes = new List<Cliente>();

            while (reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.ClienteId = int.Parse(reader["ClienteId"].ToString());
                cliente.Nome = reader["Nome"].ToString();
                cliente.Sobrenome = reader["Sobrenome"].ToString();
                cliente.Email = reader["Email"].ToString();
                cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());

                clientes.Add(cliente);
            }
            reader.Close();
            return clientes;
        }
    }
}
