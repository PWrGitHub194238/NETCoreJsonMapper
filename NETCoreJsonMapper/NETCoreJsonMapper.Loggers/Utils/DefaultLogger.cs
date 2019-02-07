using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace NETCoreJsonMapper.Loggers.Utils
{
    public static class DefaultLogger
    {
        private static readonly LoggerAdapter logger = new LoggerAdapter();

        public static bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
        {
            return logger.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);
        }

        public static bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            return logger.BindProperty(propertyName, value, destructureObjects, out property);
        }

        public static void Debug(string messageTemplate)
        {
            logger.Debug(messageTemplate);
        }

        public static void Debug<T>(string messageTemplate, T propertyValue)
        {
            logger.Debug(messageTemplate, propertyValue);
        }

        public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Debug(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            logger.Debug(messageTemplate, propertyValues);
        }

        public static void Debug(Exception exception, string messageTemplate)
        {
            logger.Debug(exception, messageTemplate);
        }

        public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Debug(exception, messageTemplate, propertyValue);
        }

        public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Debug(exception, messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate)
        {
            logger.Error(messageTemplate);
        }

        public static void Error<T>(string messageTemplate, T propertyValue)
        {
            logger.Error(messageTemplate, propertyValue);
        }

        public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Error(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            logger.Error(messageTemplate, propertyValues);
        }

        public static void Error(Exception exception, string messageTemplate)
        {
            logger.Error(exception, messageTemplate);
        }

        public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Error(exception, messageTemplate, propertyValue);
        }

        public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Error(exception, messageTemplate, propertyValues);
        }

        public static void Fatal(string messageTemplate)
        {
            logger.Fatal(messageTemplate);
        }

        public static void Fatal<T>(string messageTemplate, T propertyValue)
        {
            logger.Fatal(messageTemplate, propertyValue);
        }

        public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Fatal(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(messageTemplate, propertyValues);
        }

        public static void Fatal(Exception exception, string messageTemplate)
        {
            logger.Fatal(exception, messageTemplate);
        }

        public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Fatal(exception, messageTemplate, propertyValue);
        }

        public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(exception, messageTemplate, propertyValues);
        }

        public static ILogger ForContext(ILogEventEnricher enricher)
        {
            return logger.ForContext(enricher);
        }

        public static ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return logger.ForContext(enrichers);
        }

        public static ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return logger.ForContext(propertyName, value, destructureObjects);
        }

        public static ILogger ForContext<TSource>()
        {
            return logger.ForContext<TSource>();
        }

        public static ILogger ForContext(Type source)
        {
            return logger.ForContext(source);
        }

        public static void Information(string messageTemplate)
        {
            logger.Information(messageTemplate);
        }

        public static void Information<T>(string messageTemplate, T propertyValue)
        {
            logger.Information(messageTemplate, propertyValue);
        }

        public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Information(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            logger.Information(messageTemplate, propertyValues);
        }

        public static void Information(Exception exception, string messageTemplate)
        {
            logger.Information(exception, messageTemplate);
        }

        public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Information(exception, messageTemplate, propertyValue);
        }

        public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Information(exception, messageTemplate, propertyValues);
        }

        public static bool IsEnabled(LogEventLevel level)
        {
            return logger.IsEnabled(level);
        }

        public static void Verbose(string messageTemplate)
        {
            logger.Verbose(messageTemplate);
        }

        public static void Verbose<T>(string messageTemplate, T propertyValue)
        {
            logger.Verbose(messageTemplate, propertyValue);
        }

        public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Verbose(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(messageTemplate, propertyValues);
        }

        public static void Verbose(Exception exception, string messageTemplate)
        {
            logger.Verbose(exception, messageTemplate);
        }

        public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Verbose(exception, messageTemplate, propertyValue);
        }

        public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public static void Warning(string messageTemplate)
        {
            logger.Warning(messageTemplate);
        }

        public static void Warning<T>(string messageTemplate, T propertyValue)
        {
            logger.Warning(messageTemplate, propertyValue);
        }

        public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Warning(messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(messageTemplate, propertyValues);
        }

        public static void Warning(Exception exception, string messageTemplate)
        {
            logger.Warning(exception, messageTemplate);
        }

        public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Warning(exception, messageTemplate, propertyValue);
        }

        public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(exception, messageTemplate, propertyValues);
        }

        public static void Write(LogEvent logEvent)
        {
            logger.Write(logEvent);
        }

        public static void Write(LogEventLevel level, string messageTemplate)
        {
            logger.Write(level, messageTemplate);
        }

        public static void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            logger.Write(level, messageTemplate, propertyValue);
        }

        public static void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Write(level, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(level, messageTemplate, propertyValues);
        }

        public static void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            logger.Write(level, exception, messageTemplate);
        }

        public static void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Write(level, exception, messageTemplate, propertyValue);
        }

        public static void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public static void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(level, exception, messageTemplate, propertyValues);
        }
    }
}