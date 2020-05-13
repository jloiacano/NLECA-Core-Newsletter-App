using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models
{
    public class SpinnerModel
    {
        public bool ShowSpinner { get; set; }

        public SpinnerModel(bool useSpinner)
        {
            ShowSpinner = useSpinner;
        }
    }
}
