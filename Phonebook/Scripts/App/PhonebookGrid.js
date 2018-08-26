var PhonebookGridModel = function (items) {
    var self = this;
    var $createModal = $('#createModal');
    var $editModal = $('#editModal');
    var $deleteModal = $('#deleteModal');
    this.items = ko.mapping.fromJS(items);
    this.currentItem = ko.observable({ id: ko.observable(0), name: ko.observable("") });
    this.isActionSuccessful = ko.observable(false);
    this.errorMessages = ko.observable([]);
    this.hasErrorMessages = ko.observable(false);

    this.showCreateItemWindow = function () {
        self.currentItem({ id: ko.observable(0), name: ko.observable("") });
        $createModal.modal('show');
    };

    this.createItem = function () {
        showAjaxLoader($createModal);
        var currentItemJSON = ko.toJS(self.currentItem);
        var data = { Name: currentItemJSON.name };
        $.post("/Phonebook/Create", data, function (returnedData) {
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
        var data = { Id: currentItemJSON.id, Name: currentItemJSON.name };
        $.post("/Phonebook/Update", data, function (returnedData) {
            hideAjaxLoader($editModal);
            if (returnedData.isActionSuccessful === true) {
                $editModal.modal('hide');
                var item = getItemById(currentItemJSON.id);
                item.name(currentItemJSON.name);
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
        $.post("/Phonebook/Delete/" + currentItemJSON.id, function (returnedData) {
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
        self.currentItem({ id: ko.observable(0), name: ko.observable("") });
        self.resetAlertMessages();
    };

    function setCurrentItem(item) {
        var currentItemJSON = ko.toJS(item);
        self.currentItem().id(currentItemJSON.id);
        self.currentItem().name(currentItemJSON.name);
    }

    function getItemById(id) {
        return ko.utils.arrayFirst(self.items(), function (child) {
            return ko.toJS(child.id) === id;
        });
    }

    function PhonebookModel(id, name) {
        var self = this;
        self.id = ko.observable(id);
        self.name = ko.observable(name);
    };

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

 
