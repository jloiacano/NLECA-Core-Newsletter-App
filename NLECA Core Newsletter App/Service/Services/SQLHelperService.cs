using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data.SQLHelperTypes;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

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

        #region // Create Statements
        /// <summary>
        /// Creates a full SQL INSERT statement
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the Insert Statement</param>
        /// <param name="columnName_rowData">Dictionary of Key(column name)/Value(row data to be inserted)</param>
        /// <returns>valid SQL INSERT statement</returns>
        public string CreateInsertStatement(string tableName, Dictionary<string, string> columnName_rowData)
        {
            string tableColumns = string.Join(", ", columnName_rowData.Select(entry => entry.Key).ToArray());
            string rowdata = string.Join(", ", columnName_rowData.Select(entry => "'" + entry.Value + "'").ToArray());

            string insertStatement = string.Format(
                "INSERT INTO {0} ({1}) VALUES ({2});"
                , tableName
                , tableColumns
                , rowdata
                );

            return insertStatement;
        }

        /// <summary>
        /// Creates a full SQL INSERT statement
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the Insert Statement</param>
        /// <param name="columnName_rowData">Dictionary of Key(column name)/Value(row data to be inserted)</param>
        /// <param name="output">
        /// (OPTIONAL) will add an OUTPUT value into the statement; example entry "TableId" would add "OUTPUT Inserted.TableId"
        /// </param>
        /// <returns>valid SQL INSERT statement</returns>
        public string CreateInsertStatement(string tableName, Dictionary<string, string> columnName_rowData, string output)
        {
            string tableColumns = string.Join(", ", columnName_rowData.Select(entry => entry.Key).ToArray());
            string rowdata = string.Join(", ", columnName_rowData.Select(entry => "'" + entry.Value + "'").ToArray());
            string modifiedOutput = "";

            if (!string.IsNullOrWhiteSpace(output))
            {
                modifiedOutput = string.Format("OUTPUT Inserted.{0}", output.Trim());
            }

            string insertStatement = string.Format(
                "INSERT INTO {0} ({1}) {2} VALUES ({3});"
                , tableName
                , tableColumns
                , modifiedOutput
                , rowdata
                );

            return insertStatement;
        }

        /// <summary>
        /// Creates a basic SQL SELECT statement with equals WHERE conditionals
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the SELECT statement</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL SELECT statement</returns>
        public string CreateSelectStatement(string tableName, WhereClause whereClause)
        {
            string whereClauseText = (whereClause is null) ? string.Empty : whereClause.Text;
            string selectStatement = string.Format(
                "SELECT * FROM {0} {1};"
                , tableName
                , whereClauseText
                );

            return selectStatement;
        }

        /// <summary>
        /// Creates a full SQL UPDATE statement with equals WHERE conditionals
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the UPDATE statement</param>
        /// <param name="columnName_rowData">Dictionary where the key is the column name, and the value is the rowdata to SET</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL UPDATE statement</returns>
        public string CreateUpdateStatement(string tableName, Dictionary<string, string> columnName_rowData, WhereClause whereClause)
        {
            string tableColumnsToRowdata = string.Join(", ", columnName_rowData.Select(entry => entry.Key + " = '" + entry.Value + "'").ToArray());

            string whereClauseText = (whereClause is null) ? string.Empty : whereClause.Text;
            string updateStatement = string.Format(
                "UPDATE {0} SET {1} {2};"
                , tableName
                , tableColumnsToRowdata
                , whereClauseText
                );

            return updateStatement;
        }

        /// <summary>
        /// Creates basic SQL DELETE statement
        /// </summary>
        /// <param name="tableName">Name of the table from which you will delete entries</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL DELETE statement</returns>
        public string CreateDeleteStatement(string tableName, WhereClause whereClause)
        {
            string whereClauseText = (whereClause is null) ? string.Empty : whereClause.Text;
            string deleteStatement = string.Format(
                "DELETE FROM {0} {1};"
                , tableName
                , whereClauseText
                );

            return deleteStatement;
        }
        #endregion

        #region // Execute Statements
        /// <summary>
        /// Executes basic ADO.NET INSERT statement with custom error
        /// </summary>
        /// <param name="createStatement">valid SQL INSERT statement</param>
        /// <returns>true (if no exceptions)</returns>
        public bool ExecuteInsertStatement(string createStatement)
        {
            bool success = false;
            try
            {
                success = ExecuteSQLStatement(createStatement);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error creating to the database using: " + createStatement, ex);
            }

            return success;
        }

        /// <summary>
        /// Executes basic ADO.NET INSERT statement with custom error
        /// </summary>
        /// <param name="createStatement">valid SQL INSERT statement</param>
        /// <returns>output of INSERT statement</returns>
        public int ExecuteInsertStatementWithReturn(string insertStatement)
        {
            SqlConnection connection = new SqlConnection(dbConnectionString);
            int returnedValue = -1;

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertStatement, connection);
                returnedValue = (int)command.ExecuteScalar();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error inserting into the database using: " + insertStatement, ex);
            }

            return returnedValue;
        }

        /// <summary>
        /// Executes basic ADO.NET UPDATE statement with custom error
        /// </summary>
        /// <param name="updateStatement">valid SQL UPDATE statement</param>
        /// <returns>true (if no exceptions)</returns>
        public bool ExecuteUpdateStatement(string updateStatement)
        {
            bool success = false;
            try
            {
                success = ExecuteSQLStatement(updateStatement);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error updating to the database using: " + updateStatement, ex);
            }

            return success;
        }

        /// <summary>
        /// Executes basic ADO.NET DELETE statement with custom error
        /// </summary>
        /// <param name="deleteStatement">valid SQL DELETE statement</param>
        /// <returns>true (if no exceptions)</returns>
        public bool ExecuteDeleteStatement(string deleteStatement)
        {
            bool success = false;
            try
            {
                success = ExecuteSQLStatement(deleteStatement);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error deleting from the database using: " + deleteStatement, ex);
            }

            return success;
        }
        #endregion

        private bool ExecuteSQLStatement(string statement)
        {
            SqlConnection connection = new SqlConnection(dbConnectionString);
            int rowsEffected;

            connection.Open();
            SqlCommand command = new SqlCommand(statement, connection);
            rowsEffected = command.ExecuteNonQuery();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

            if (rowsEffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
