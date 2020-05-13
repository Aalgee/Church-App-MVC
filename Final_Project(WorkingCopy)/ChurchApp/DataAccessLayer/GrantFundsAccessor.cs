using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GrantFundsAccessor : IGrantFundsAccessor
    {
        /// <summary>
        /// Inserts grant funds into the grant funds table
        /// </summary>
        /// <param name="grantFunds"></param>
        /// <returns></returns>
        public int InsertGrantFunds(GrantFunds grantFunds)
        {
            int grantFundsID = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_grant_funds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Amount", grantFunds.Amount);
            try
            {
                conn.Open();
                grantFundsID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return grantFundsID;
        }

        /// <summary>
        /// Selects grant funds from the grant funds table
        /// </summary>
        /// <returns></returns>
        public GrantFunds SelectGrantFunds()
        {
            GrantFunds grantFunds = new GrantFunds();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_grant_funds", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    grantFunds.GrantFundsID = reader.GetInt32(0);
                    grantFunds.Amount = reader.GetDecimal(1);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return grantFunds;
        }

        /// <summary>
        /// Updates grant funds in the grant funds table
        /// </summary>
        /// <param name="oldGrantFunds"></param>
        /// <param name="newGrantFunds"></param>
        /// <returns></returns>
        public int UpdateGrantFunds(GrantFunds oldGrantFunds, GrantFunds newGrantFunds)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_grant_funds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantFundsID", oldGrantFunds.GrantFundsID);

            cmd.Parameters.AddWithValue("@OldAmount", oldGrantFunds.Amount);

            cmd.Parameters.AddWithValue("@NewAmount", newGrantFunds.Amount);
           
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
