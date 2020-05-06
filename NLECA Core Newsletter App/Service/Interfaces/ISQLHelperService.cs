using System;
using System.Data;
using System.Data.SqlClient;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface ISQLHelperService
    {
        /// <summary>
        /// Converts the C# DateTime to a string which is acceptable by SQL Server
        /// </summary>
        /// <param name="dateTime">The DateTime object you would like to convert</param>
        /// <returns>A string representation of the DateTime object which is acceptable to a SQL Server database</returns>
        string ConvertDateTimeForSQL(DateTime dateTime);

        /// <summary>
        /// Calls the named stored procedure to retrieve a dataset
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns>DataSet from the stored procedure</returns>
        DataSet GetDatasetFromStoredProcedure(string storedProcedureName);

        /// <summary>
        /// Calls the named stored procedure using the passed in parameters to retrieve a dataset
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>DataSet from the stored procedure</returns>
        DataSet GetDatasetFromStoredProcedure(string storedProcedureName, SqlParameter[] parameters);

        /// <summary>
        /// Calls the named stored procedure to retrieve an integer result
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns>integer result of the stored procedure</returns>
        int GetReturnValueFromStoredProcedure(string storedProcedureName);

        /// <summary>
        /// Calls the named stored procedure using the passed in parameters to retrieve an integer result
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>integer result of the stored procedure</returns>
        int GetReturnValueFromStoredProcedure(string storedProcedureName, SqlParameter[] parameters);
    }
}
