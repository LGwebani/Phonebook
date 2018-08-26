using System;
using Microsoft.Extensions.Logging;

namespace Phonebook.Shared.Helpers
{
    public interface IErrorLogger
    {
        void Log(LogLevel logLevel, Exception ex);
    }
}