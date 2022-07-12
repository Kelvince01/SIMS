using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Service
{
    public class SQLDAL
    {
        private SqlConnection connection;

        public SQLDAL() => this.connection = new SqlConnection(this.GlobalConnection());

        public SQLDAL(string serverConnection) => this.connection = new SqlConnection(this.GetServerConnection());

        public string GlobalConnection() => ConfigurationManager.ConnectionStrings["SIMS.Properties.Settings.SIMS_WebConnectionString"].ConnectionString;

        public string GetServerConnection() => new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SIMS_HO"].ConnectionString).ProviderConnectionString;

        public Result ExecuteQuery(string SQL)
        {
            Result result = new Result();
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    new SqlCommand(SQL, this.connection).ExecuteNonQuery();
                    result.ExecutionState = true;
                }
            }
            catch (Exception ex)
            {
                result.ExecutionState = false;
                result.Error = ex.Message;
            }
            finally
            {
                this.connection.Close();
            }
            return result;
        }

        public Result ExecuteQuery(List<string> SQL)
        {
            Result result = new Result();
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    foreach (string cmdText in SQL)
                    {
                        new SqlCommand(cmdText, this.connection)
                        {
                            Transaction = sqlTransaction
                        }.ExecuteNonQuery();
                        result.ExecutionState = true;
                    }
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                result.ExecutionState = false;
                result.Error = ex.Message;
                sqlTransaction.Rollback();
            }
            finally
            {
                this.connection.Close();
            }
            return result;
        }

        public Result Select(string SQL)
        {
            Result result = new Result();
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    SqlCommand selectCommand = new SqlCommand(SQL, this.connection);
                    selectCommand.CommandTimeout = 0;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    result.ExecutionState = true;
                    result.Data = dataTable;
                }
            }
            catch (Exception ex)
            {
                result.ExecutionState = false;
                result.Error = ex.Message;
            }
            finally
            {
                this.connection.Close();
            }
            return result;
        }
    }
}
