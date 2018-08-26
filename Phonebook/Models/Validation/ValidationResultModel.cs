using System.Linq;
using System.Collections.Generic;

namespace Phonebook.App.Models.Validation
{
    public class ValidationResultModel
    {
        #region Properties
        public bool IsActionSuccessful { get; set; }
        public bool HasErrorMessages => ErrorMessages.Any();
        public IList<string> ErrorMessages { get; set; }
        #endregion

        #region Constructors
        public ValidationResultModel()
        {
            ErrorMessages = new List<string>();
        }
        #endregion
    }
}
