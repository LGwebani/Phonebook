using System.Collections.Generic;

namespace Phonebook.Domain.Services
{
    public interface IPhonebookService
    {
        Phonebook FindById(long id);
        void Delete(long id);
        void Save(Phonebook entity);
        void Update(Phonebook entity);
        IList<Phonebook> GetAll();
    }
}
