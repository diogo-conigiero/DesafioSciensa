var app = angular.module('SciensaApp', ['ui.bootstrap']);
app.run(function () { });

app.controller('SciensaAppController', ['$rootScope', '$scope', '$http', '$timeout', function ($rootScope, $scope, $http, $timeout) {

    $scope.refresh = function () {
        $http.get('api/Votes?c=' + new Date().getTime())
            .then(function (data, status) {
                $scope.votes = data;
            }, function (data, status) {
                $scope.votes = undefined;
            });
    };    

    $scope.addCliente = function (nome, cpf, endereco) {
        var fd = new FormData();
        fd.append('nome', nome);
        fd.append('cpf', cpf);
        fd.append('endereco', endereco);

        $http.put('api/Votes/' + nome + '/' + cpf + '/' + endereco, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .then(function (data, status) {
                $scope.refresh();
                $scope.item = undefined;
            })
    };

    $scope.addConta = function (NomeCliente, NumeroConta, TipoConta, SaldoConta) {
        var fd = new FormData();
        fd.append('NomeCliente', NomeCliente);
        fd.append('NumeroConta', NumeroConta);
        fd.append('TipoConta', TipoConta);
        fd.append('SaldoConta', SaldoConta);

        $http.put('api/Votes/' + NomeCliente + '/' + NumeroConta + '/' + TipoConta + '/' + SaldoConta, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .then(function (data, status) {
                $scope.refresh();
                $scope.item = undefined;
            })
    };

    $scope.listConta = function (NomeCliente) {
        var fd = new FormData();
        fd.append('NomeCliente', NomeCliente);

        $http.get('api/Votes/' + NomeCliente + '?c=' + new Date().getTime())
            .then(function (data, status) {
                $scope.contas = data;
            }, function (data, status) {
                $scope.contas = undefined;
            });
    };
}]);