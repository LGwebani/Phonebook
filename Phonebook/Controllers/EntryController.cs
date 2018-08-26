using Phonebook.App.Models;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Services;

namespace Phonebook.App.Controllers
{
    public class EntryController : BaseController
    {
        #region Properties
        #endregion

        #region Constructors
        public EntryController(IPhonebookService phonebookService): base(phonebookService)
        {
        }
        #endregion

        #region Public Methods
        public IActionResult Index(long id)
        {
            var phonebook = _phonebookService.FindById(id);
            var model = new EntryListModel(phonebook);
            model.PhonebookId = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EntryModel model)
        {
            if (ModelState.IsValid)
            {
                var phonebook = new Domain.Phonebook() { Id = model.PhonebookId };
                var entry = model.ConvertToDomain(Domain.State.Added);
                phonebook.Entries.Add(entry);
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
        public IActionResult Update(EntryModel model)
        {
            if (ModelState.IsValid)
            {
                var phonebook = new Domain.Phonebook() { Id = model.PhonebookId };
                var entry = model.ConvertToDomain(Domain.State.Modified);
                phonebook.Entries.Add(entry);
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
        public IActionResult Delete(EntryModel model)
        {
            var phonebook = new Domain.Phonebook() { Id = model.PhonebookId };
            var entry = model.ConvertToDomain(Domain.State.Deleted);
            phonebook.Entries.Add(entry);
            _phonebookService.Update(phonebook);
            model.IsActionSuccessful = true;
            return Json(model);
        }
        #endregion
    }
}