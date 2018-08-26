using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phonebook.App.Controllers
{
    public class BaseController : Controller
    {
        #region Properties
        public IPhonebookService _phonebookService { get; }
        #endregion

        #region Constructors
        public BaseController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }
        #endregion

        #region Protected Methods
        protected IList<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
           return  modelState
                .Keys
                .SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                .ToList();
        }
        #endregion
    }
}