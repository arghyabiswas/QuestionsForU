var app = angular.module('ocr', []);
/*
(function(){
   app.config(['$routeProvider','$locationProvider',function($routeProvider,$locationProvider){
       $routeProvider
        .when('/',{controller='OCRController as ocr'})
        .otherwise('/',{redirectTo=-'/',controller='OCRController'});
   }]); 
}());
*/


app.factory('OCRServices', OCRServices);
OCRServices.$inject = ['$http'];

app.controller('OCRController', OCRController);
OCRController.$inject = ['$scope', 'OCRServices'];