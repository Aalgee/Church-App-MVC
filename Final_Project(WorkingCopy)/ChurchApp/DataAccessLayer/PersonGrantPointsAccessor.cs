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
    public class PersonGrantPointsAccessor : IPersonGrantPointsAccessor
    {
        /// <summary>
        /// Deletes a record out of the person grant points table
        /// </summary>
        /// <param name="personGrantPointsID"></param>
        /// <returns></returns>
        public int DeletePersonGrantPoints(int personGrantPointsID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_person_grant_points", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonGrantPointsID", personGrantPointsID);

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
        /// Inserts a person grant points intop the database
        /// </summary>
        /// <param name="personGrantPoints"></param>
        /// <returns></returns>
        public int InsertPersonGrantPoints(PersonGrantPoints personGrantPoints)
        {
            int personGrantPointsID = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_person_grant_points", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonID", personGrantPoints.PersonID);
            cmd.Parameters.AddWithValue("@ElectionID", personGrantPoints.Points);
            try
            {
                conn.Open();
                personGrantPointsID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return personGrantPointsID;
        }

        /// <summary>
        /// Selects all available person grant points
        /// </summary>
        /// <returns></returns>
        public List<PersonGrantPointsVM> SelectAllPersonGrantPoints()
        {
            List<PersonGrantPointsVM> personGrantPoints = new List<PersonGrantPointsVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_person_grant_points", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var personGrantPoint = new PersonGrantPointsVM();

                        personGrantPoint.PersonGrantPointsID = reader.GetInt32(0);
                        personGrantPoint.PersonID = reader.GetInt32(1);
                        personGrantPoint.Points = reader.GetInt32(2);
                        personGrantPoint.FirstName = reader.GetString(3);
                        personGrantPoint.LastName = reader.GetString(4);

                        personGrantPoints.Add(personGrantPoint);
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
            return personGrantPoints;
        }

        /// <summary>
        /// Selects person grant points by id
        /// </summary>
        /// <param name="personGrantPointsID"></param>
        /// <returns></returns>
        public PersonGrantPointsVM SelectPersonGrantPointsByPersonGrantPointsID(int personGrantPointsID)
        {
            PersonGrantPointsVM personGrantPoint = new PersonGrantPointsVM();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_person_grant_points_by_person_grant_points_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonGrantPointsID", personGrantPointsID);

            try
            {
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    personGrantPoint.PersonGrantPointsID = reader.GetInt32(0);
                    personGrantPoint.PersonID = reader.GetInt32(1);
                    personGrantPoint.Points = reader.GetInt32(2);
                    personGrantPoint.FirstName = reader.GetString(3);
                    personGrantPoint.LastName = reader.GetString(4);



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
            return personGrantPoint;
        }

        /// <summary>
        /// updates person grant points
        /// </summary>
        /// <param name="oldPersonGrantPoints"></param>
        /// <param name="newPersonGrantPoints"></param>
        /// <returns></returns>
        public int UpdatePersonGrantPoints(PersonGrantPoints oldPersonGrantPoints, PersonGrantPoints newPersonGrantPoints)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_person_grant_points", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonGrantPointsID", oldPersonGrantPoints.PersonGrantPointsID);

            cmd.Parameters.AddWithValue("@OldPersonID", oldPersonGrantPoints.PersonID);
            cmd.Parameters.AddWithValue("@OldPoints", oldPersonGrantPoints.Points);

            cmd.Parameters.AddWithValue("@NewPersonID", newPersonGrantPoints.PersonID);
            cmd.Parameters.AddWithValue("@NewPoints", newPersonGrantPoints.Points);
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
    }
}
