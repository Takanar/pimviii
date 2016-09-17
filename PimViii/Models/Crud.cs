using System;
using System.Data.OleDb;
using System.Data;

namespace PimViii.Models
{
    public class Crud
    {
        //Caminho do Banco de dados deve ser exato observando-se as \\.
        string endBd = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = |DataDirectory|Gerenciador.mdb";
        OleDbConnection conectar = null;
        public void ConectarBd()
        {
            try
            {
                conectar = new OleDbConnection(endBd);
                if (conectar.State == ConnectionState.Closed)
                {
                    conectar.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Create(string tarefa, string discriminacao, string detalhes, DateTime data)
        {
            try
            {
                ConectarBd();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO [Table] (Tipo, Discriminacao, Detalhes, DataLimite) VALUES (@Tipo, @Discriminacao, @detalhes, @DataLimite)", conectar);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Tipo ", tarefa);
                cmd.Parameters.AddWithValue("@Discriminacao ", discriminacao);
                cmd.Parameters.AddWithValue("@Detalhes ", detalhes);
                cmd.Parameters.AddWithValue("@DataLimite ", data);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.Close();
            }
        }
        public DataSet Read()
        {
            string query = "SELECT * FROM [Table]";
            ConectarBd();
            DataSet ds = new DataSet();
            try
            {
                OleDbCommand cmd = new OleDbCommand(query, conectar);
                cmd.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.Close();
            }
            return ds;
        }
        public void Update(int id, string tipo, string discriminacao, string detalhes, DateTime data)
        {
            try
            {
                ConectarBd();
                OleDbCommand cmd = new OleDbCommand("UPDATE [Table] SET Tipo=@Tipo, Discriminacao = @Discriminacao, Detalhes = @detalhes, DataLimite = @DataLimite where Id ="+ id, conectar);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@Id ", id);
                cmd.Parameters.AddWithValue("@Tipo ", tipo);
                cmd.Parameters.AddWithValue("@Discriminacao ", discriminacao);
                cmd.Parameters.AddWithValue("@Detalhes ", detalhes);
                cmd.Parameters.AddWithValue("@DataLimite ", data);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.Close();
            }

        }
        public void Delete(int id)
        {
            try
            {
                ConectarBd();
                OleDbCommand cmd = new OleDbCommand("Delete from [Table] where Id = @Id", conectar);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id ", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.Close();
            }
        }

        public int VerificaData()
        {
            Default cadastro = new Default();
            DateTime hoje = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            try
            {
                ConectarBd();
                OleDbCommand cmd = new OleDbCommand("select DataLimite from [Table] where DataLimite = @DataLimite", conectar);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@DataLimite",hoje);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return 0;
                }
                else return 1;
    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.Close();
            }
        }
    }
}