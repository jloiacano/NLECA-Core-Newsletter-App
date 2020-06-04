using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class SQLHelperService : ISQLHelperService
    {
        private readonly string dbConnectionString;
        private readonly ILogger<SQLHelperService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SQLHelperService(IConfiguration config, ILogger<SQLHelperService> logger, IHttpContextAccessor httpContextAccessor)
        {
            dbConnectionString = config["ConnectionStrings:DefaultConnection"];
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Converts the C# DateTime to a string which is acceptable by SQL Server
        /// </summary>
        /// <param name="dateTime">The DateTime object you would like to convert</param>
        /// <returns>A string representation of the DateTime object which is acceptable to a SQL Server database</returns>
        public string ConvertDateTimeForSQL(DateTime dateTime)
        {
            string formattedDateTime = string.Empty;

            if (dateTime == DateTime.MinValue)
            {
                return SqlDateTime.MinValue.ToString();
            }
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss tt");
        }

        /// <summary>
        /// Calls the named stored procedure to retrieve a dataset
        /// </summary>
        /// <param name="storedProcedureName">String value of the name of the stored procedure to execute</param>
        /// <returns>DataSet from the stored procedure</returns>
        public DataSet GetDatasetFromStoredProcedure(string storedProcedureName)
        {
            CheckIfStoredProcedureExists(storedProcedureName);

            DataSet dataSet = new DataSet();

            SqlConnection connection = new SqlConnection(dbConnectionString);

            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Connection = connection;

                // add parameters to command here....

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;

                if (dataSet.Tables.Count != 0)
                {
                    dataAdapter.Fill(dataSet);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error running stored procedure: {0} from method {1}"
                    , storedProcedureName
                    , "GetDatasetFromStoredProcedure without parameters");
                _logger.LogError(error, ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dataSet;
        }

        /// <summary>
        /// Calls the named stored procedure using the passed in parameters to retrieve a dataset
        /// </summary>
        /// <param name="storedProcedureName">String value of the name of the stored procedure to execute</param>
        /// <param name="parameters">Array of <see cref="SqlParameter"/>s for the stored procedure</param>
        /// <returns>DataSet from the stored procedure</returns>
        public DataSet GetDatasetFromStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            CheckIfStoredProcedureExists(storedProcedureName);

            DataSet dataSet = new DataSet();

            SqlConnection connection = new SqlConnection(dbConnectionString);

            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Connection = connection;

                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;

                if (dataSet.Tables.Count != 0)
                {
                    dataAdapter.Fill(dataSet);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error running stored procedure: {0} from method {1}"
                    , storedProcedureName
                    , "GetDatasetFromStoredProcedure with parameters");
                _logger.LogError(error, ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dataSet;
        }

        /// <summary>
        /// Calls the named stored procedure to retrieve an integer result
        /// </summary>
        /// <param name="storedProcedureName">String value of the name of the stored procedure to execute</param>
        /// <returns>integer result of the stored procedure</returns>
        public int GetReturnValueFromStoredProcedure(string storedProcedureName)
        {
            CheckIfStoredProcedureExists(storedProcedureName);

            int returnValue = 0;

            SqlConnection connection = new SqlConnection(dbConnectionString);

            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Connection = connection;

                returnValue = Int32.Parse(command.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error running stored procedure: {0} from method {1}"
                    , storedProcedureName
                    , "GetReturnValueFromStoredProcedure without parameters");
                _logger.LogError(error, ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Calls the named stored procedure using the passed in parameters to retrieve an integer result
        /// </summary>
        /// <param name="storedProcedureName">String value of the name of the stored procedure to execute</param>
        /// <param name="parameters">Array of <see cref="SqlParameter"/>s for the stored procedure</param>
        /// <returns>integer result of the stored procedure</returns>
        public int GetReturnValueFromStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            CheckIfStoredProcedureExists(storedProcedureName);

            int returnValue = 0;

            SqlConnection connection = new SqlConnection(dbConnectionString);

            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Connection = connection;

                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }

                var result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }

                returnValue = Int32.Parse(result.ToString());

                connection.Close();
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error running stored procedure: {0} from method {1}"
                    , storedProcedureName
                    , "GetReturnValueFromStoredProcedure with parameters");
                _logger.LogError(error, ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return returnValue;
        }

        private void CheckIfStoredProcedureExists(string storedProcedureName)
        {
            SqlConnection connection = new SqlConnection(dbConnectionString);
            try
            {
                string checkStatemnt = string.Format("SELECT * FROM sys.objects WHERE type = 'P' AND name = '{0}'", storedProcedureName);

                connection.Open();
                SqlCommand command = new SqlCommand(checkStatemnt, connection);


                if (command.ExecuteScalar() == null)
                {
                    string error = string.Format("Stored Procedure \"{0}\" attempted execution but was not found", storedProcedureName);
                    throw new NotImplementedException(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Stored Procedure Error", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
