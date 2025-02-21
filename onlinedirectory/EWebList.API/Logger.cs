﻿using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace EWebList.API
{
    public static class Logger
    {

        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        private static readonly log4net.ILog _log = GetLogger(typeof(Logger));

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static void Info(object message)
        {
            SetLog4NetConfiguration();
            _log.Info(message);
        }

        public static void Debug(object message)
        {
            SetLog4NetConfiguration();
            _log.Debug(message);
        }
        public static void Error(object message, Exception ex)
        {
            SetLog4NetConfiguration();
            _log.Error(message + Environment.NewLine + "Exception" + Environment.NewLine + ex.ToString());
        }
        public static void AngularError(string message, string error)
        {
            SetLog4NetConfiguration();
            _log.Error(message + Environment.NewLine + "Exception" + Environment.NewLine + error);
        }

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}
