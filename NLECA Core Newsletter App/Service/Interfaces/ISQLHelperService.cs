using NLECA_Core_Newsletter_App.Data.SQLHelperTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using static NLECA_Core_Newsletter_App.Service.Services.SQLHelperService;

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
        /// Creates a full SQL INSERT statement
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the Insert Statement</param>
        /// <param name="columnName_rowData">Dictionary of Key(column name)/Value(row data to be inserted)</param>
        /// <returns>valid SQL INSERT statement</returns>
        string CreateInsertStatement(string tableName, Dictionary<string, string> columnName_rowData);

        /// <summary>
        /// Creates a full SQL INSERT statement
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the Insert Statement</param>
        /// <param name="columnName_rowData">Dictionary of Key(column name)/Value(row data to be inserted)</param>
        /// <param name="output">
        /// (OPTIONAL) will add an OUTPUT value into the statement; example entry "TableId" would add "OUTPUT Inserted.TableId"
        /// </param>
        /// <returns>valid SQL INSERT statement</returns>
        string CreateInsertStatement(string tableName, Dictionary<string, string> columnName_rowData, string output);

        /// <summary>
        /// Creates a basic SQL SELECT statement with equals WHERE conditionals
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the SELECT statement</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL SELECT statement</returns>
        string CreateSelectStatement(string tableName, WhereClause whereClause);

        /// <summary>
        /// Creates a full SQL UPDATE statement with equals WHERE conditionals
        /// </summary>
        /// <param name="tableName">Name of the table as it would appear in the UPDATE statement</param>
        /// <param name="columnName_rowData">Dictionary where the key is the column name, and the value is the rowdata to SET</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL UPDATE statement</returns>
        string CreateUpdateStatement(string tableName, Dictionary<string, string> columnName_rowData, WhereClause whereClause);

        /// <summary>
        /// Creates basic SQL DELETE statement
        /// </summary>
        /// <param name="tableName">Name of the table from which you will delete entries</param>
        /// <param name="whereClause">Uses <see cref="WhereClause"/> to create a useable SQL WHERE clause</param>
        /// <returns>valid SQL DELETE statement</returns>
        string CreateDeleteStatement(string tableName, WhereClause whereClause);

        /// <summary>
        /// Executes basic ADO.NET INSERT statement with custom error
        /// </summary>
        /// <param name="createStatement">valid SQL INSERT statement</param>
        /// <returns>true (if no exceptions)</returns>
        bool ExecuteInsertStatement(string insertStatement);

        /// <summary>
        /// Executes basic ADO.NET INSERT statement with custom error
        /// </summary>
        /// <param name="createStatement">valid SQL INSERT statement</param>
        /// <returns>output of INSERT statement</returns>
        int ExecuteInsertStatementWithReturn(string insertStatement);

        /// <summary>
        /// Executes basic ADO.NET UPDATE statement with custom error
        /// </summary>
        /// <param name="updateStatement">valid SQL UPDATE statement</param>
        /// <returns>true (if no exceptions)</returns>
        bool ExecuteUpdateStatement(string updateStatement);

        /// <summary>
        /// Executes basic ADO.NET DELETE statement with custom error
        /// </summary>
        /// <param name="deleteStatement">valid SQL DELETE statement</param>
        /// <returns>true (if no exceptions)</returns>
        bool ExecuteDeleteStatement(string deleteStatement);
    }
}
