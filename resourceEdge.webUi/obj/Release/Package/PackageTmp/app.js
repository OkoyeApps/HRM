/// <reference path="C:\Users\Emmanuel\documents\visual studio 2015\Projects\ResourceEdge\resourceEdge.webUi\Scripts/angular/angular.js" />

var app = angular.module('appModule', [])
        .config(function ($httpProvider) {
                //$httpProvider.defaults.headers['CORS']
                $httpProvider.defaults.useXDomain = true;
                delete $httpProvider.defaults.headers.common['X-Requested-With']
  });