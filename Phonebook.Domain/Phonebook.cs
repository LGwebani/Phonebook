using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain
{
    public class Phonebook
    {
        #region Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public IList<Entry> Entries { get; set; }
        #endregion

        #region Constructors
        public Phonebook()
        {
            Entries = new List<Entry>();
        }
        public Phonebook(EF.Entities.Phonebook phonebook)
        {
            Id = phonebook.Id;
            Name = phonebook.Name;
            Entries = phonebook.Entries.Select(x => new Entry(x)).ToList();
        }
        #endregion
    }

    #region Extensions
    public static class PhonebookExtensions
    {
        public static EF.Entities.Phonebook ConvertToEntity(this Phonebook phonebook)
        {
            return new EF.Entities.Phonebook()
            {
                Id = phonebook.Id,
                Name = phonebook.Name,
                Entries = phonebook.Entries.Select(x => x.ConvertToEntity()).ToList()
            };
        }
    }
    #endregion
}