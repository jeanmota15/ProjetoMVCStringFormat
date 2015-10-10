using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class LoginAplicacao
    {
        private Contexto contexto;

        public Login Logar(string usuario, string senha)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM LOGIN WHERE Usuario = '{0}' AND Senha = '{1}' ", 
                    usuario, senha);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Login> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var logins = new List<Login>();

            while (reader.Read())
            {
                Login login = new Login();
                login.LoginId = int.Parse(reader["LoginId"].ToString());
                login.Usuario = reader["Usuario"].ToString();
                login.Senha = reader["Senha"].ToString();

                logins.Add(login);
            }
            reader.Close();
            return logins;
        }
    }
}
