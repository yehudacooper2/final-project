using System;
using System.Data.SqlClient;


// DAL = Data Access Layer
namespace DAL
{
    public static class DbManager
    {
        private static string GetSqlConnection()
        {
            string connectionString = " Data Source = .\\sqlExpress; Initial Catalog = FileDb; Integrated Security = True";

            return connectionString;
        }

      #region UpdateDbResults
       
        /// <summary>
        /// this function updates the db with the results given from the file searcher function.
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="filePath"></param>
        /// <param name="choosenDirectory"></param>

        public static void UpdateDbResults(int searchId, string filePath, string choosenDirectory)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(GetSqlConnection()))
                {

                    sql.Open();
                    SqlCommand query = new SqlCommand($"INSERT INTO [dbo].[FileRes] ([SearchId],[PathRes],[ChoosenDirectory]) VALUES ('{searchId}','{filePath}','{choosenDirectory}') ", sql);

                    query.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion 

        #region UpdateDbSearchId
        
        /// <summary>
        /// this function returns the lastest searchId which is a Pk in the FileQuery table and FK in the FileRes table.
        /// </summary>
        /// <returns></returns>
        public static int UpdateDbSearchId()
        {
            int searchId = 0;
            try
            {
                using (SqlConnection sql = new SqlConnection(GetSqlConnection()))
                {

                    sql.Open();


                    SqlCommand query = new SqlCommand($"SELECT TOP 1 [SearchId] FROM [dbo].[FileQuery] ORDER BY [SearchId] DESC", sql);

                    object res = query.ExecuteScalar();

                    searchId = (int)res;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return searchId;

        }
        #endregion

        #region UpdateDbSearchQuery

        /// <summary>
        /// this function updates the db with the file name which the program is searching for. 
        /// </summary>
        /// <param name="searchQuery"></param>
        public static void UpdateDbSearchQuery(string searchQuery)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(GetSqlConnection()))
                {

                    sql.Open();

                    SqlCommand query = new SqlCommand($"INSERT INTO [dbo].[FileQuery] ([SearchQuery]) VALUES ('{searchQuery}') ", sql);

                    query.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        #endregion
    }
}
