﻿using System;
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

        public static GlobalSettings GetSettings()
        {
            return instance;
        }

        private static GlobalSettings instance;
        public static int GetFontSize()
        {
            return FontSize;
        }

        public static void SetFontSize(int fontSize)
        {
            FontSize = fontSize;
        }

        /// <summary>
        /// Размер шрифта.
        /// </summary>
        private static int FontSize { get; set; } = 15;

        /// <summary>
        /// Получить размеры дочерних окон.
        /// </summary>
        /// <returns></returns>
        public static ChildWindowSettings GetChildWindowSettings()
        {
            return new ChildWindowSettings();
        }

        public static void SetChildWindowSettings(ChildWindowSettings settings)
        {
            ChildWindowSettings = settings;
        }

        private static ChildWindowSettings ChildWindowSettings { get; set; }
    }
}
