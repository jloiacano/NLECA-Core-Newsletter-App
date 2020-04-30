using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Data.SQLHelperTypes
{
    public class WhereClause
    {
        private readonly ILogger<WhereClause> _logger;
        public WhereClause(ILogger<WhereClause> logger)
        {
            _logger = logger;
        }

        public string Text { get; private set; }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary,
                dictionary2
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary3">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2, SqlHelperComparableDictionary dictionary3)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary,
                dictionary2,
                dictionary3
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary3">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary4">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2, SqlHelperComparableDictionary dictionary3, SqlHelperComparableDictionary dictionary4)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary,
                dictionary2,
                dictionary3,
                dictionary4
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary3">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary4">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary5">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2, SqlHelperComparableDictionary dictionary3, SqlHelperComparableDictionary dictionary4, SqlHelperComparableDictionary dictionary5)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary,
                dictionary2,
                dictionary3,
                dictionary4,
                dictionary5
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary3">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary4">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary5">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary6">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2, SqlHelperComparableDictionary dictionary3, SqlHelperComparableDictionary dictionary4, SqlHelperComparableDictionary dictionary5, SqlHelperComparableDictionary dictionary6)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {

                dictionary,
                dictionary2,
                dictionary3,
                dictionary4,
                dictionary5,
                dictionary6,
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        /// <summary>
        /// Creates a WHERE clause to be used in a SQL statement
        /// </summary>
        /// <param name="dictionary">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary2">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary3">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary4">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary5">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary6">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        /// <param name="dictionary7">Uses the <see cref="SqlHelperComparableDictionary" /> comparable column,datavalues, and the comparison type</param>
        public WhereClause(SqlHelperComparableDictionary dictionary, SqlHelperComparableDictionary dictionary2, SqlHelperComparableDictionary dictionary3, SqlHelperComparableDictionary dictionary4, SqlHelperComparableDictionary dictionary5, SqlHelperComparableDictionary dictionary6, SqlHelperComparableDictionary dictionary7)
        {
            List<SqlHelperComparableDictionary> dictionaries = new List<SqlHelperComparableDictionary>()
            {
                dictionary,
                dictionary2,
                dictionary3,
                dictionary4,
                dictionary5,
                dictionary6,
                dictionary7
            };
            Text = CreateWhereClauseText(dictionaries);
        }

        private string CreateWhereClauseText(IEnumerable<SqlHelperComparableDictionary> dictionaries)
        {
            string conditions = string.Empty;

            List<string> modifiedConditions = new List<string>();

            foreach (SqlHelperComparableDictionary dictionary in dictionaries)
            {
                if (dictionary.Comparables != null && dictionary.Comparables.Count > 0)
                {
                    string currentDictionaryString = string.Join(" AND ", dictionary.Comparables.Select(entry => entry.Key + dictionary.ComparisonType + "'" + entry.Value + "'").ToArray());
                    modifiedConditions.Add(currentDictionaryString);
                }
            }

            if (modifiedConditions.Count > 0)
            {
                string conditionsToUse = string.Join(" AND ", modifiedConditions.Where(condition => !string.IsNullOrEmpty(condition)).ToArray());
                conditions = string.Format("WHERE {0} ", conditionsToUse);
            }

            return conditions;
        }
    }
}
