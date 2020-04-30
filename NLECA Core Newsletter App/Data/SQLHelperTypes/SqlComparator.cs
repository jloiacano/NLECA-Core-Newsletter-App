using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Data.SQLHelperTypes
{

    /// <summary>
    /// For Converting to "=", "<>", ">", ">=", "<", "<=", or "LIKE", 
    /// </summary>
    public enum SqlComparator
    {
        IsEqualTo = 0,
        IsNotEqualTo = 1,
        IsGreaterThan = 2,
        IsGreaterThanOrEqualTo = 3,
        IsLessThan = 4,
        IsLessThanOrEqualTo = 5,
        IsLike = 6
    }
}
