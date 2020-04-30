using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Data.SQLHelperTypes
{
    public class SqlHelperComparableDictionary
    {
        public string ComparisonType { get; private set; }
        public Dictionary<string, string> Comparables { get; set; }

        private readonly ILogger<SqlHelperComparableDictionary> _logger;

        public SqlHelperComparableDictionary(ILogger<SqlHelperComparableDictionary> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Prepares a dictionary specifically for SQLHelperService Methods
        /// </summary>
        /// <param name="comparisonType">An enum of each possible comparison operators</param>
        public SqlHelperComparableDictionary(SqlComparator comparisonType)
        {
            ComparisonType = ConvertComparator(comparisonType);
            Comparables = new Dictionary<string, string>();
        }

        /// <summary>
        /// Prepares a dictionary specifically for SQLHelperService Methods
        /// </summary>
        /// <param name="comparisonType">An enum of each possible comparison operators</param>
        /// <param name="comparables">The dictionary of key value pairs of columns and values</param>
        public SqlHelperComparableDictionary(SqlComparator comparisonType, Dictionary<string, string> comparables)
        {
            ComparisonType = ConvertComparator(comparisonType);
            Comparables = comparables;
        }

        //sets the string value of ComparisonType by SqlComparator
        private string ConvertComparator(SqlComparator comparisonType)
        {
            string toReturn = ((int)comparisonType) switch
            {
                0 => " = ",
                1 => " <> ",
                2 => " > ",
                3 => " >= ",
                4 => " < ",
                5 => " <= ",
                6 => " LIKE ",
                _ => string.Empty,
            };

            if (string.IsNullOrEmpty(toReturn))
            {
                _logger.LogError("There was an Error converting comparator to string value", 
                    "No Value was assigned for " + ((int)comparisonType).ToString() + " " + comparisonType.ToString());
            }

            return toReturn;
        }
    }
}
