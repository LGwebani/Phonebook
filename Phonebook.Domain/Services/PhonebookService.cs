using System;
using System.Linq;
using Phonebook.EF.Contexts;
using System.Collections.Generic;
using Phonebook.Shared.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Domain.Services
{
    public class PhonebookService: IPhonebookService
    {
        #region Properties
        private IDatabaseContext _dbContext;
        private readonly IErrorLogger _logger;
        #endregion

        #region Constructors
        public PhonebookService(IDatabaseContext dbContext, IErrorLogger logger)
        {
            _logger = logger;
            _dbContext = dbContext;

        }
        #endregion

        #region Public Methods
        public Phonebook FindById(long id)
        {
            var entity = _dbContext.FindByIdAsync(id).Result;
            var phonebook = new Phonebook(entity);
            return phonebook;
        }
        public void Delete(long id)
        {
           _dbContext.Delete(id);
        }
        public void Save(Phonebook phonebook)
        {
             _dbContext.SaveAsync(phonebook.ConvertToEntity());
        }
        public void Update(Phonebook phonebook)
        {
            try
            {
                if (phonebook.State == State.Modified)
                {
                    _dbContext.Update(phonebook.ConvertToEntity());
                    return;
                }

                if (phonebook.Entries.Any())
                {
                    var entry = phonebook.Entries.First();
                    var entity = _dbContext.FindByIdAsync(phonebook.Id).Result;
                    switch (entry.State)
                    {
                        case State.Added:
                            entity.Entries.Add(entry.ConvertToEntity());
                            _dbContext.Update(entity);
                            return;
                        case State.Modified:
                            var modifiedEntry = entity.Entries.First(x => x.Id == entry.Id);
                            modifiedEntry.State = EntityState.Modified;
                            modifiedEntry.Name = entry.Name;
                            modifiedEntry.PhoneNumber = entry.PhoneNumber;
                            _dbContext.Update(entity);
                            return;
                        case State.Deleted:
                            var deletedEntry = entity.Entries.First(x => x.Id == entry.Id);
                            entity.Entries.Remove(deletedEntry);
                            _dbContext.Update(entity);
                            return;
                        default:
                            throw new Exception("State unknown.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex);
                throw new Exception(ex.Message);
            }
           
          
        }
        public IList<Phonebook> GetAll()
        {
            var entities =  _dbContext.GetAllAsync().Result;
            var phonebooks = entities?.Select(x => new Phonebook(x)).ToList() ?? new List<Phonebook>();
            return phonebooks;
        }
        #endregion
    }
}
