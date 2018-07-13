'use strict';

var app = angular.module('FinApp', []);

document.addEventListener('DOMContentLoaded', function () {
    angular.bootstrap(document, ['FinApp']);
});

app.controller('AccountsController', function ($scope, AccountsService) {
    var ctrl = this;

    $scope.Accounts = [];
    $scope.TotalBalance = 0;

    $scope.newAccount = {};
    $scope.newAccount.Name = "";
    $scope.newAccount.Description = "";
    $scope.newAccount.Balance = 0;
    $scope.newAccount.Tags = [];

    LoadAccounts();

    function LoadAccounts() {
        AccountsService.Get()
            .then(function (accounts) {
                $scope.Accounts = accounts;
                UpdateBalance();
            }, function (error) {
                $scope.ErrorMessage = error;
            });
    }

    function UpdateBalance() {
        $scope.TotalBalance = 0;
        for (var i = 0; i < $scope.Accounts.length; i++) {
            $scope.TotalBalance += $scope.Accounts[i].Balance;
        }
    }

    $scope.Edit = function(account) {
        account.OldName = account.Name;
        account.OldDescription = account.Description;
        account.OldBalance = account.Balance;
        account.editing = true;        
    };

    $scope.CancelEdit = function(account) {
        account.Name = account.OldName;
        account.Description = account.OldDescription;
        account.Balance = account.OldBalance;
        account.editing = false;
    };

    $scope.Save = function(account) {
        AccountsService.Update(account).then(function(data) {
            account.Id = data.Id;
            account.Name = data.Name;
            account.Description = data.Description;
            account.Balance = data.Balance;
            account.Tags = data.Tags;
            account.editing = false;
            UpdateBalance();
        });
    };

    $scope.Delete = function(account) {
        $scope.accountToDelete = account;
        $("#deleteAccountModal").modal("show");
    };

    $scope.DeleteAccount = function() {
        AccountsService.Delete($scope.accountToDelete).then(function(data) {
            $scope.Accounts = $scope.Accounts.filter(function(a) {
                return a.Id != $scope.accountToDelete.Id;
            });
            UpdateBalance();
        });
        $("#deleteAccountModal").modal("hide");
    }

    $scope.Add = function(account) {
        AccountsService.Add(account).then(function(data) {
            $scope.Accounts.push(data);
            UpdateBalance();
            $scope.newAccount.Name = "";
            $scope.newAccount.Description = "";
            $scope.newAccount.Balance = 0;
            $scope.newAccount.Tags = [];
        });
    };
});

app.service('AccountsService', function ($http) {
    var svc = this;
    var apiUrl = 'http://localhost:5000/api';

    svc.Get = function () {
        return $http.get(apiUrl + '/accounts')
            .then(function success(response) {
                return response.data;
            });
    };

    svc.Update = function (account) {
        return $http.put(apiUrl + "/accounts/" + account.Id, {            
            Name: account.Name,
            Description: account.Description,
            Balance: account.Balance,
            Tags: account.Tags
        }).then(function success(response) {
            return response.data;
        });
    };

    svc.Delete = function (account) {
        return $http.delete(apiUrl + "/accounts/" + account.Id)
                    .then(function success(response) {
                        return true;
                    });
    };

    svc.Add = function (account) {
        return $http.post(apiUrl + "/accounts", {
            Name: account.Name,
            Description: account.Description,
            Balance: account.Balance,
            Tags: account.Tags
        }).then(function sucess(response) {
            return response.data;
        });
    };
});