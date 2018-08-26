using System.Linq;
using System.Collections.Generic;

namespace Phonebook.App.Models
{
    public class EntryListModel
    {
        #region Properties
        public long PhonebookId { get; set; }
        public string PhonebookName { get; set; }
        public IList<EntryModel> Entries { get; set; }
        #endregion

        #region Constructors
        public EntryListModel(Domain.Phonebook phonebook)
        {
            PhonebookName = phonebook.Name;
            Entries = phonebook.Entries.Select(x => new EntryModel(x)).ToList();
        }
        #endregion
    }
}
