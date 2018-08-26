using System.Linq;
using System.Collections.Generic;

namespace Phonebook.App.Models
{
    public class PhonebookListModel
    {
        #region Properties
        public IList<PhonebookModel> Phonebooks { get; set; }
        #endregion

        #region Constructors
        public PhonebookListModel(IList<Domain.Phonebook> phonebooks)
        {
            Phonebooks = phonebooks.Select(x => new PhonebookModel(x)).ToList();
        }
        #endregion
    }
}
