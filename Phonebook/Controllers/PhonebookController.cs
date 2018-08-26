using Phonebook.App.Models;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Services;
using Phonebook.App.Models.Validation;

namespace Phonebook.App.Controllers
{
    public class PhonebookController : BaseController
    {
        #region Constructors
        public PhonebookController(IPhonebookService phonebookService) : base(phonebookService)
        {
        }
        #endregion

        #region Public Methods
        public IActionResult Index()
        {
            var phoneBooks = _phonebookService.GetAll();
            var model = new PhonebookListModel(phoneBooks);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PhonebookModel model)
        {
            if (ModelState.IsValid)
            {
                var phonebook =  model.ConvertToDomain(Domain.State.Added);
                _phonebookService.Save(phonebook);
                model.IsActionSuccessful = true;
            }
            else
            {
                model.ErrorMessages = GetModelStateErrors(ModelState);
                model.IsActionSuccessful = false;
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult Update(PhonebookModel model)
        {
            if (ModelState.IsValid)
            {
                var phonebook = model.ConvertToDomain(Domain.State.Modified);
                _phonebookService.Update(phonebook);
                model.IsActionSuccessful = true;
            }
            else
            {
                model.ErrorMessages = GetModelStateErrors(ModelState);
                model.IsActionSuccessful = false;
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            _phonebookService.Delete(id);
            var phonebook = new Domain.Phonebook() { Id = id };
            var model = new ValidationResultModel();
            model.IsActionSuccessful = true;
            return Json(model);
        }
        #endregion
    }
}