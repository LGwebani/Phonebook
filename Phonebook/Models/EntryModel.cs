using Phonebook.App.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.App.Models
{
    public class EntryModel: ValidationResultModel
    {
        #region Properties
        public long Id { get; set; }
        public long PhonebookId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters.")]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "Phone number must not exceed 10 digits.")]
        [RegularExpression(@"^0[0-9]*$", ErrorMessage = "Phone number start with zero.")]
        public string PhoneNumber { get; set; }
        #endregion

        #region Constructors
        public EntryModel()
        {
        }
        public EntryModel(Domain.Entry entry)
        {
            Id = entry.Id;
            Name = entry.Name;
            PhoneNumber = entry.PhoneNumber;
        }
        #endregion
    }

    #region Extensions
    public static class EntryModelExtensions
    {
        public static Domain.Entry ConvertToDomain(this EntryModel entryModel, Domain.State state)
        {
            return new Domain.Entry()
            {
                State = state,
                Id = entryModel.Id,
                Name = entryModel.Name,
                PhoneNumber = entryModel.PhoneNumber,
                PhonebookId = entryModel.PhonebookId
            };
        }
    }
    #endregion

}