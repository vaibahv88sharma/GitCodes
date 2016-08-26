var app = angular.module('ContactsApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.when('/all-contacts',
        {
            controller: 'ctrlContacts',
            templateUrl: '../template/allContacts.html'
        })
        .when('/view-contacts/:contactId',
        {
            controller: 'ctrlViewContacts',
            templateUrl: '../template/viewContact.html'
        })
        .when('/add-contacts',
        {
            controller: 'ctrlAddContacts',
            templateUrl: '../template/manageContact.html'
        })
        .when('/edit-contacts/:contactId',
        {
            controller: 'ctrlEditContacts',
            templateUrl: '../template/manageContact.html'
        })
        .otherwise({ redirectTo: '/all-contacts' });
});

app.controller('ctrlContacts', function ($scope, ContactService) {

    ContactService.getContact().success(function (contacts) {
        $scope.contacts = contacts.d.results;
        //alert('Contact received successfully.');
    })
    .error(function (data, status) {
        console.error('Repos error', status, data);
        alert(status);
        alert(JSON.stringify(data));
    });
	
	$scope.setOrder = function(orderby){
		if(orderby === $scope.orderby){
			$scope.reverse = !$scope.reverse;
		}
		$scope.orderby = orderby;
	};
	
	$scope.confirmDel = function(id){
		if(confirm("Do you really want to delete this contact?")){
	        ContactService.delContact(id).success(function (contacts) {
	            ContactService.getContact().success(function (contacts) {
	                $scope.contacts = contacts.d.results;
	                //alert('Contact Deleted Successfully');
	            })
	            .error(function (data, status) {
	                console.error('Repos error', status, data);
	                alert(status);
	                alert(JSON.stringify(data));
	            });
	        })
	        .error(function (data, status) {
	            console.error('Repos error', status, data);
	            alert(status);
	            alert(JSON.stringify(data));
	        });					
		}
	};
			
});

app.controller('NavbarController', function ($scope, $location) {
    $scope.getClass = function (path) {
        if ($location.path().substr(0, path.length) == path) {
            return true;
        } else {
            return false;
        }
    }
});

app.controller('ctrlViewContacts', function ($scope, $routeParams, ContactService) {
	ContactService.getSelContact($routeParams.contactId)
	.success(function (contact) {
		$scope.contact = contact.d.results;
	})
	.error(function (data, status) {
	    console.error('Repos error', status, data);
	    alert(status);
	    alert(JSON.stringify(data));
	});
});

app.controller('ctrlAddContacts', function ($scope, ContactService) {
    $scope.submitForm = function (contact) {
		debugger;
        if ($scope.ContactForm.$valid) {
            ContactService.addContact(contact)
			.success(function () {
                $scope.ContactForm.$setPristine();
                $scope.contact = null;
            })
            .error(function (data, status) {
                console.error('Repos error', status, data);
                alert(status);
                alert(JSON.stringify(data));
            });
        }
    };
});

app.controller('ctrlEditContacts', ["$scope", "$location", "$routeParams","ContactService",function ($scope, $location, $routeParams, ContactService) {
	$scope.contact={};
	ContactService.getSelContact($routeParams.contactId)
	.success(function (contacts) {
		$scope.contact.name = contacts.d.results[0].name;
		$scope.contact.email = contacts.d.results[0].email;
		$scope.contact.phone = contacts.d.results[0].phone;
		$scope.contact.city = contacts.d.results[0].city;
		$scope.contact.state = contacts.d.results[0].state;
		$scope.contact.country = contacts.d.results[0].country;
		$scope.contact.zip = contacts.d.results[0].zip;
		$scope.contact.addrs1 = contacts.d.results[0].addrs1;
		$scope.contact.addrs2 = contacts.d.results[0].addrs2;
	})
	.error(function (data, status) {
	    console.error('Repos error', status, data);
	    alert(status);
	    alert(JSON.stringify(data));
	});	
	
    $scope.submitForm = function (contact) {
        if ($scope.ContactForm.$valid) {
            ContactService.editContact(contact,$routeParams.contactId)
			.success(function () {
                $scope.ContactForm.$setPristine();
                $scope.contact = null;
				debugger;
				$location.path("#/all-contacts");
            })
            .error(function (data, status) {
                console.error('Repos error', status, data);
                alert(status);
                alert(JSON.stringify(data));
            });
        }
    };	
	
}]);

