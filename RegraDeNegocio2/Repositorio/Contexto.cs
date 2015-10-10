using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repositorio
{
    public class Contexto : IDisposable
    {
        private SqlConnection minhaConexao;

        public Contexto()
        {
            minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["RegraDeNegocio2"].ConnectionString);
            minhaConexao.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaConexao);
            cmd.ExecuteNonQuery();
        }

        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaConexao);
            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
            {
                minhaConexao.Close();
            }
        }
    }
}
