using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models
{
    public class BirthdaySplashScreenModel : SplashScreenModel
    {
        public string Name { get; set; }


        public BirthdaySplashScreenModel(bool showSplashScreen, string name) : base(showSplashScreen)
        {
            Duration = 180000; // 3 minutes?
            Name = name;
        }
    }
}
