using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.Models
{
    public class GlobalSettings
    {
        public GlobalSettings()
        {
            if (instance == null)
            {
                instance = new GlobalSettings();
            }
        }

        private static GlobalSettings instance;
        public static int GetSettings()
        {
            return FontSize;
        }

        public static void SetSettings(int fontSize)
        {
            FontSize = fontSize;
        }

        private static int FontSize { get; set; } = 15;
    }
}
