using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Xam.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of the application.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The environment of the application.
        /// </summary>
        public AppEnvironment Environment { get; set; }

        /// <summary>
        /// The full name of the application.
        /// </summary>
        public string FullName => $"{Name} v{Version}-{Environment}";

        /// <summary>
        /// 
        /// </summary>
        public string DocNocApiBaseAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OpenPayMerchantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OpenPayApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool OpenPayIsProductionMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SyncfusionLicenseKey { get; set; }
    }
}
