using System.Threading.Tasks;
using System.Collections.Generic;

namespace Phonebook.EF.Contexts
{
    public interface IDatabaseContext
    {
        Task<Entities.Phonebook> FindByIdAsync(long id);
        Task Delete(long id);
        Task SaveAsync(Entities.Phonebook entity);
        Task Update(Entities.Phonebook entity);
        Task<IList<Entities.Phonebook>> GetAllAsync();
    }
}
