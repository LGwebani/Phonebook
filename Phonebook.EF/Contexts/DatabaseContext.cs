using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Phonebook.EF.Entities;
using Phonebook.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace Phonebook.EF.Contexts
{
    public class DatabaseContext : BaseContext, IDatabaseContext
    {
        #region Properties
        private readonly IErrorLogger _logger;
        #endregion

        #region Constructors
        public DatabaseContext(IHostingEnvironment appHost, IErrorLogger logger) : base(appHost, logger)
        {
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<Entities.Phonebook> FindByIdAsync(long id)
        {
            try
            {
                var result = await base.Phonebook.FindAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(long id)
        {
            try
            {
                var item = FindByIdAsync(id);
                base.Phonebook.Remove(item.Result);
                await  base.SaveChangesAsync(true);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task SaveAsync(Entities.Phonebook phonebook)
        {
            try
            {
                var result = await base.Phonebook.AddAsync(phonebook);
                await base.SaveChangesAsync(true);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task Update(Entities.Phonebook phonebook)
        {
            try
            {

                Phonebook.Include(x => x.Entries);
                Phonebook.Update(phonebook);
                await base.SaveChangesAsync(true);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task<IList<Entities.Phonebook>> GetAllAsync()
        {
            try
            {
                var result = await base.Phonebook.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Entry>()
                .HasOne(p => p.Phonebook)
                .WithMany(b => b.Entries)
                .HasForeignKey(p => p.PhonebookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion
    }
}
