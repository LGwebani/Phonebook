using System.Collections.Generic;

namespace Phonebook.EF.Entities
{ 
    public class Phonebook
    {
        #region Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<Entry> Entries { get; set; }
        #endregion

        #region Properties
        public Phonebook()
        {
            Entries = new List<Entry>();
        }
        #endregion
    }
}
