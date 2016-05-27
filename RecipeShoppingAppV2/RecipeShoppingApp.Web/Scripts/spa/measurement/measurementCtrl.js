(function (app) {
    'use strict';
    app.controller('measurementCtrl', measurementCtrl);

    measurementCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService',];

    function measurementCtrl($scope, $location, $routeParams, apiService, notificationService)
    {
        $scope.pageClass = 'page-measurements';
        $scope.measurement = {};
        $scope.measurements = [];
        $scope.isReadOnly = false;
        $scope.AddMeasurement = AddMeasurement;
        $scope.DeleteMeasurement = DeleteMeasurement;
        $scope.UpdateMeasurement = UpdateMeasurement;
        $scope.edit = 'edit';

        function AddMeasurement()
        {
            AddMeasurementModel();
        }

        function DeleteMeasurement(id) {
            apiService.post('api/measurement/delete/' + id,null,
                DeleteMeasurementSucceded,
                DeleteMeasurementFailed);
        }

        function UpdateMeasurement(id, name) {
            var measurement = { ID: id, Name: name };
            apiService.post('/api/measurement/update', measurement,
                UpdateMeasurementSucceded,
                UpdateMeasurementFailed);
        }

        function UpdateMeasurementSucceded() {
            notificationService.displaySuccess('Measurement updated.')
            LoadMeasurements();
        }

        function UpdateMeasurementFailed(response) {
            notificationService.displayError(response.statusText);
        }

        function DeleteMeasurementSucceded(response) {
            LoadMeasurements();
            notificationService.displaySuccess("Measurement deleted");
        }

        function DeleteMeasurementFailed(response) {
            notificationService.displayError("Bad Request : Some recipes are using this measurement.");
        }


        function AddMeasurementModel()
        {
            apiService.post('api/measurement/add', $scope.measurement,
                AddMeasurementSucceded,
                AddMeasurementFailed)
        }

        function AddMeasurementSucceded(response)
        {
            notificationService.displaySuccess($scope.measurement.Name + ' has been submitted');
            LoadMeasurements();
        }
        function AddMeasurementFailed(response)
        {
            
            console.log(response.data);
            notificationService.displayError(response.statusText);
        }

        function LoadMeasurements()
        {
            apiService.get('/api/measurement/measurementsAdmin/', null,
                measurementsLoadCompleted,
                measurementsLoadFailed
                );
        }

        function measurementsLoadCompleted(result)
        {
            $scope.measurement = null;
            $scope.measurements = result.data;
        }
        function measurementsLoadFailed()
        {
            notificationService.displayError(response.data);
        }

        function clearSearch()
        {
            $scope.filterMeasurements = '';
            search();
        }

        LoadMeasurements();
    }
})(angular.module('RecipeApp'));