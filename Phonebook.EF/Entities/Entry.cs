using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.EF.Entities
{
    public class Entry
    {
        #region Properties
        public long Id { get; set; }
        public EntityState State { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long PhonebookId { get; set; }

        [ForeignKey("PhonebookId")]
        public Phonebook Phonebook { get; set; }
        #endregion
    }
}
