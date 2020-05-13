using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PersonGroupAccessor : IPersonGroupAccessor
    {

        /// <summary>
        /// deletes person groups by group id
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int DeletePersonGroupsByGroupID(string groupID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_person_groups_by_group_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
