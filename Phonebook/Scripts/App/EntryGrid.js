var EntryGridModel = function (model) {
    var self = this;
    var $createModal = $('#createModal');
    var $editModal = $('#editModal');
    var $deleteModal = $('#deleteModal');
    this.phonebookId = model.phonebookId;
    this.items = ko.mapping.fromJS(model.entries);
    this.currentItem = ko.observable({ id: ko.observable(0), name: ko.observable(""), phoneNumber: ko.observable(null) });
    this.isActionSuccessful = ko.observable(false);
    this.errorMessages = ko.observable([]);
    this.hasErrorMessages = ko.observable(false);

    this.showCreateItemWindow = function () {
        self.currentItem({ id: ko.observable(0), name: ko.observable(""), phoneNumber: ko.observable(null) });
        $createModal.modal('show');
    };

    this.createItem = function () {
        showAjaxLoader($createModal);
        var currentItemJSON = ko.toJS(self.currentItem);
        var data = { PhonebookId: self.phonebookId, Name: currentItemJSON.name, PhoneNumber: currentItemJSON.phoneNumber };
        $.post("/Entry/Create", data, function (returnedData) {
            hideAjaxLoader($createModal);
            if (returnedData.isActionSuccessful === true) {
                $createModal.modal('hide');
                location.reload();
            }
            updateValidationMessages(returnedData);
        });
    };

    this.showEditItemWindow = function (item) {
        setCurrentItem(item);
        $editModal.modal('show');
    };

    this.editItem = function () {
        showAjaxLoader($editModal);
        var currentItemJSON = ko.toJS(self.currentItem);
        var data = { PhonebookId: self.phonebookId, Id: currentItemJSON.id, Name: currentItemJSON.name, PhoneNumber: currentItemJSON.phoneNumber };
        $.post("/Entry/Update", data, function (returnedData) {
            hideAjaxLoader($editModal);
            if (returnedData.isActionSuccessful === true) {
                $editModal.modal('hide');
                var item = getItemById(currentItemJSON.id);
                item.name(currentItemJSON.name);
                item.phoneNumber(currentItemJSON.phoneNumber);
            }
            updateValidationMessages(returnedData);
        });
    };

    this.showDeleteItemConfirmationWindow = function (item) {
        setCurrentItem(item);
        $deleteModal.modal('show');
    };

    this.deleteItem = function () {
        showAjaxLoader($deleteModal);
        var currentItemJSON = ko.toJS(self.currentItem);
        var data = { PhonebookId: self.phonebookId, Id: currentItemJSON.id };
        $.post("/Entry/Delete/", data, function (returnedData) {
            hideAjaxLoader($deleteModal);
            if (returnedData.isActionSuccessful === true) {
                $deleteModal.modal('hide');
                location.reload();
            }
            updateValidationMessages(returnedData);
        });
    };

    this.resetAlertMessages = function () {
        self.isActionSuccessful(false);
        self.errorMessages([]);
        self.hasErrorMessages(false);
    };

    this.closeCreateEditDelete = function () {
        self.currentItem({ id: ko.observable(0), name: ko.observable(""), phoneNumber: ko.observable(null) });
        self.resetAlertMessages();
    };

    ko.bindingHandlers.numeric = {
        init: function (element, valueAccessor) {
            $(element).on("keydown", function (event) {
                if ($.inArray(event.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                }
                if (event.shiftKey == 190 || (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57)) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            });
        }
    };

    function setCurrentItem(item) {
        var currentItemJSON = ko.toJS(item);
        self.currentItem().id(currentItemJSON.id);
        self.currentItem().name(currentItemJSON.name);
        self.currentItem().phoneNumber(currentItemJSON.phoneNumber);
    }

    function getItemById(id) {
        return ko.utils.arrayFirst(self.items(), function (child) {
            return ko.toJS(child.id) === id;
        });
    }

    function EntryModel(id, name, phoneNumber) {
        var self = this;
        self.id = ko.observable(id);
        self.name = ko.observable(name);
        self.phoneNumber = ko.observable(phoneNumber);
    }

    function updateValidationMessages(returnedData) {
        self.isActionSuccessful(returnedData.isActionSuccessful);
        self.errorMessages(returnedData.errorMessages);
        self.hasErrorMessages(returnedData.hasErrorMessages);
    }

    function showAjaxLoader($modal) {
        $modal.find(".ajax-loader").removeClass("invisible");
        $modal.find("button").attr("disabled", "disabled");
    }

    function hideAjaxLoader($modal) {
        $modal.find(".ajax-loader").addClass("invisible");
        $modal.find("button").removeAttr("disabled");
    }
};


