using System;
using Phonebook.EF.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Phonebook.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace Phonebook.EF.Contexts
{
    public class BaseContext : DbContext
    {
        #region Properties
        private readonly IErrorLogger _logger;
        private IHostingEnvironment _appHost { get; }
        protected new DbSet<Entry> Entry { get; set; }
        protected DbSet<Entities.Phonebook> Phonebook { get; set; }
        #endregion

        #region Constructors
        public BaseContext(IHostingEnvironment appHost, IErrorLogger logger)
        {
            _logger = logger;
            _appHost = appHost;
        }
        #endregion

        #region Protected Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                var connectionString = $"Data Source ={ _appHost.ContentRootPath }/Phonebook.db";
                optionsBuilder.UseSqlite(connectionString, x => x.SuppressForeignKeyEnforcement()).EnableSensitiveDataLogging(true);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
