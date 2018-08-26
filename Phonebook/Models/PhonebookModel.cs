using Phonebook.App.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.App.Models
{
    public class PhonebookModel: ValidationResultModel
    {
        #region Properties
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters.")]
        public string Name { get; set; }
        #endregion

        #region Constructors
        public PhonebookModel()
        {
        }
        public PhonebookModel(Domain.Phonebook phonebook)
        {
            Id = phonebook.Id;
            Name = phonebook.Name;
        }
        #endregion
    }

    #region Extensions
    public static class PhonebookExtensions
    {
        public static Domain.Phonebook ConvertToDomain(this PhonebookModel phonebookModel, Domain.State state)
        {
            return new Domain.Phonebook()
            {
                Id = phonebookModel.Id,
                Name = phonebookModel.Name,
                State = state
            };
        }
    }
    #endregion
}