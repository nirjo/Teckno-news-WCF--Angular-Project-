var app = angular.module('myApp', []);
		app.controller('myCtrl', function($scope, $http) {
		
		   $http.get(serviceUrl+"/json/nt")
		  .then(function(response) {
			
			    $scope.items = JSON.parse(response.data.JSONDataResult);
	
			  
		  });
		   $http.get(serviceUrl+"/json/svd")
		  .then(function(response) {
			
			    $scope.svditems = JSON.parse(response.data.JSONDataResult);
	
			  
		  });
		   $http.get(serviceUrl+"/json/expressen")
		  .then(function(response) {
			
			    $scope.expressenitems = JSON.parse(response.data.JSONDataResult);
	
			  
		  });
		  
		  
		
		}
		
		).filter('renderHTMLCorrectly', function($sce)
{
	return function(stringToParse)
	{
	
	//return $sce.trustAsHtml( stringToParse);
		return $sce.trustAsHtml( ((stringToParse)));
	}
});