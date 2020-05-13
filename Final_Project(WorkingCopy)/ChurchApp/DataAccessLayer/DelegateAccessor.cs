using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class DelegateAccessor : IDelegateAccessor
    {
        /// <summary>
        /// activates a delegate
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public int ActivateDelegate(int delegateID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_activate_delegate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DelegateID", delegateID);

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
        /// Deactivates a delegate
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public int DeactivateDelegate(int delegateID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            
            var cmd = new SqlCommand("sp_deactivate_delegate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DelegateID", delegateID);

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
        /// inserts a delegate into the delegate table
        /// </summary>
        /// <param name="theDelegate"></param>
        /// <returns></returns>
        public int InsertDelegate(ElectionDelegate theDelegate)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_delegate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", theDelegate.FirstName);
            cmd.Parameters.AddWithValue("@LastName", theDelegate.LastName);
            try
            {
                conn.Open();
                rows = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// selects a delegate by id
        /// </summary>
        /// <param name="delegateID"></param>
        /// <returns></returns>
        public ElectionDelegate SelectDelegateByID(int delegateID)
        {
            ElectionDelegate theDelegate = new ElectionDelegate();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_delegate_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DelegateID", delegateID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    theDelegate.DelegateID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    theDelegate.FirstName = reader.IsDBNull(1) ? null : reader.GetString(1);
                    theDelegate.LastName = reader.IsDBNull(2) ? null : reader.GetString(2);
                    theDelegate.HasVotedForGrants = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                    theDelegate.HasVotedForElections = reader.IsDBNull(4) ? false : reader.GetBoolean(4);
                    theDelegate.Active = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
                    //theDelegate.Pin = reader.IsDBNull(6) ? null : reader.GetString(6);
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
            return theDelegate;
        }

        /// <summary>
        /// selects a delegate by their pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public ElectionDelegate SelectDelegateByPin(string pin)
        {
            ElectionDelegate theDelegate = new ElectionDelegate();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_delegate_by_pin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Pin", pin);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    theDelegate.DelegateID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    theDelegate.FirstName = reader.IsDBNull(1) ? null : reader.GetString(1);
                    theDelegate.LastName = reader.IsDBNull(2) ? null : reader.GetString(2);
                    theDelegate.HasVotedForGrants = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                    theDelegate.HasVotedForElections = reader.IsDBNull(4) ? false : reader.GetBoolean(4);
                    theDelegate.Active = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
                    //theDelegate.Pin = reader.IsDBNull(6) ? null : reader.GetString(6);
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
            return theDelegate;
        }

        /// <summary>
        /// selects a list of delegates by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<ElectionDelegate> SelectDelegatesByActive(bool active)
        {
            List<ElectionDelegate> theDelegates = new List<ElectionDelegate>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_delegates_by_active", conn);
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
                        ElectionDelegate theDelegate = new ElectionDelegate();

                        theDelegate.DelegateID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        theDelegate.FirstName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        theDelegate.LastName = reader.IsDBNull(2) ? null : reader.GetString(2);
                        theDelegate.HasVotedForGrants = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                        theDelegate.HasVotedForElections = reader.IsDBNull(4) ? false : reader.GetBoolean(4);
                        theDelegate.Active = reader.IsDBNull(5) ? false : reader.GetBoolean(5);

                        theDelegates.Add(theDelegate);
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
            return theDelegates;
        }

        /// <summary>
        /// updates a delegate
        /// </summary>
        /// <param name="oldDelegate"></param>
        /// <param name="newDelegate"></param>
        /// <returns></returns>
        public int UpdateDelegate(ElectionDelegate oldDelegate, ElectionDelegate newDelegate)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_delegate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DelegateID", oldDelegate.DelegateID);

            cmd.Parameters.AddWithValue("@OldFirstName", oldDelegate.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldDelegate.LastName);
            cmd.Parameters.AddWithValue("@OldHasVotedForGrants", oldDelegate.HasVotedForGrants);
            cmd.Parameters.AddWithValue("@OldHasVotedForElections", oldDelegate.HasVotedForElections);
            cmd.Parameters.AddWithValue("@OldActive", oldDelegate.Active);

            cmd.Parameters.AddWithValue("@NewFirstName", newDelegate.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newDelegate.LastName);
            cmd.Parameters.AddWithValue("@NewHasVotedForGrants", newDelegate.HasVotedForGrants);
            cmd.Parameters.AddWithValue("@NewHasVotedForElections", newDelegate.HasVotedForElections);
            cmd.Parameters.AddWithValue("@NewActive", newDelegate.Active);
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
        /// updates a delegates pin
        /// </summary>
        /// <param name="delegateID"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public int UpdateDelegatePin(int delegateID, string pin)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_update_delegate_pin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DelegateID", delegateID);
            cmd.Parameters.AddWithValue("@Pin", pin);

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
