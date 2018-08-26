namespace Phonebook.Domain
{
    public class Entry
    {
        #region Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public long PhonebookId { get; set; }
        public string PhoneNumber { get; set; }
        #endregion

        #region Constructors
        public Entry()
        {
        }
        public Entry(EF.Entities.Entry entry)
        {
            Id = entry.Id;
            Name = entry.Name;
            PhoneNumber = entry.PhoneNumber;
            PhonebookId = entry.PhonebookId;
        }
        #endregion
    }

    #region Extensions
    public static class  EntryExtensions
    {
        public static EF.Entities.Entry ConvertToEntity(this Entry entry)
        {
            return  new EF.Entities.Entry()
            {
                Id = entry.Id,
                Name = entry.Name,
                PhoneNumber = entry.PhoneNumber,
                PhonebookId = entry.PhonebookId,
                Phonebook = new EF.Entities.Phonebook() { Id = entry.PhonebookId }
            };
        }
    }
    #endregion
}
