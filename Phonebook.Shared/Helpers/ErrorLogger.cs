using System;
using Microsoft.Extensions.Logging;

namespace Phonebook.Shared.Helpers
{
    public class ErrorLogger<T>: IErrorLogger
    {
        #region Properties
        private readonly ILogger _logger;
        private const string GenericError = "An error occurred and your request was unscuccessful.";
        #endregion

        #region Constructors
        public ErrorLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        #endregion

        #region Public Methods
        public void Log(LogLevel logLevel, Exception ex)
        {
            _logger.Log(logLevel, ex, GenericError);
        }
        #endregion
    }
}