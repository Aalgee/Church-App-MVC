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

    /// <summary>
    /// contains the data access methods for grants
    /// </summary>
    public class GrantAccessor : IGrantAccessor
    {

        /// <summary>
        /// Deactivates a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public int DeactivateGrant(int grantID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_grant", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantID", grantID);
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

        /// <summary>
        /// Inserts a Grant
        /// </summary>
        /// <param name="grant"></param>
        /// <returns></returns>
        public int InsertGrant(Grant grant)
        {
            int grantID = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_grant", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantName", grant.GrantName);
            cmd.Parameters.AddWithValue("@Description", grant.Description);
            cmd.Parameters.AddWithValue("@AmountAskedFor", grant.AmountAskedFor);
            try
            {
                conn.Open();
                grantID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return grantID;
        }

        /// <summary>
        /// reactivates a grant
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public int ReactivateGrant(int grantID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_reactivate_grant", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantID", grantID);
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

        /// <summary>
        /// selects all grants
        /// </summary>
        /// <returns></returns>
        public List<Grant> SelectAllGrants()
        {
            List<Grant> grants = new List<Grant>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_grants", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var grant = new Grant();

                        grant.GrantID = reader.GetInt32(0);
                        grant.GrantName = reader.GetString(1);
                        grant.Points = reader.GetInt32(2);
                        grant.Description = !reader.IsDBNull(3) ? reader.GetString(3) : grant.Description = null;
                        grant.AmountAskedFor = reader.GetDecimal(4);
                        grant.AmountRecieved = reader.GetDecimal(5);
                        grant.Active = reader.GetBoolean(6);
                    }
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
            return grants;
        }

        /// <summary>
        /// selects a grant by id
        /// </summary>
        /// <param name="grantID"></param>
        /// <returns></returns>
        public Grant SelectGrantByGrantID(int grantID)
        {
            Grant grant = new Grant();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_grant_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantID", grantID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    grant.GrantID = reader.GetInt32(0);
                    grant.GrantName = reader.GetString(1);
                    grant.Points = reader.GetInt32(2);
                    grant.Description = reader.IsDBNull(3) ?null : reader.GetString(3) ;
                    grant.AmountAskedFor = reader.GetDecimal(4);
                    grant.AmountRecieved = reader.GetDecimal(5);
                    grant.Active = reader.GetBoolean(6);
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
            return grant;
        }

        /// <summary>
        /// Selects active grants
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Grant> SelectGrantsByActive(bool active)
        {
            List<Grant> grants = new List<Grant>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_grants_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var grant = new Grant();

                        grant.GrantID = reader.GetInt32(0);
                        grant.GrantName = reader.GetString(1);
                        grant.Points = reader.GetInt32(2);
                        grant.Description = !reader.IsDBNull(3) ? reader.GetString(3) : grant.Description = null;
                        grant.AmountAskedFor = reader.GetDecimal(4);
                        grant.AmountRecieved = reader.GetDecimal(5);
                        grant.Active = reader.GetBoolean(6);

                        grants.Add(grant);
                    }
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
            return grants;
        }

        /// <summary>
        /// updates a grant
        /// </summary>
        /// <param name="oldGrant"></param>
        /// <param name="newGrant"></param>
        /// <returns></returns>
        public int UpdateGrant(Grant oldGrant, Grant newGrant)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_grant", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantID", oldGrant.GrantID);

            cmd.Parameters.AddWithValue("@OldGrantName", oldGrant.GrantName);
            cmd.Parameters.AddWithValue("@OldPoints", oldGrant.Points);
            cmd.Parameters.AddWithValue("@OldDescription", oldGrant.Description);
            cmd.Parameters.AddWithValue("@OldAmountAskedFor", oldGrant.AmountAskedFor);
            cmd.Parameters.AddWithValue("@OldAmountAwarded", oldGrant.AmountRecieved);

            cmd.Parameters.AddWithValue("@NewGrantName", newGrant.GrantName);
            cmd.Parameters.AddWithValue("@NewPoints", newGrant.Points);
            cmd.Parameters.AddWithValue("@NewDescription", newGrant.Description);
            cmd.Parameters.AddWithValue("@NewAmountAskedFor", newGrant.AmountAskedFor);
            cmd.Parameters.AddWithValue("@NewAmountAwarded", newGrant.AmountRecieved);
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

        /// <summary>
        /// Updates the points a grant has
        /// </summary>
        /// <param name="grantID"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public int UpdateGrantPointsByID(int grantID, int points)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_grant_points_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GrantID", grantID);

            cmd.Parameters.AddWithValue("@Points", points);

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
