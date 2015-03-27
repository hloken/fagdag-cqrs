angular.module('fagdagCqrsHotel').controller('BookingsController', ['$scope', '$location', '$routeParams', function ($scope, $location, $routeParams) {
    $scope.hello = 'Hello!';
    //$scope.isLoading = true;
    //$scope.companyId = $routeParams.companyId;
    //$scope.backUrl = links.getLocation('unions', { companyId: $scope.companyId });

    //companyService.get({ companyId: $scope.companyId }).$promise.then(function (company) {
    //    paycodeService.getUnionPaycodes(company.id).then(function (paycodes) {
    //        $scope.paycodes = paycodes;
    //        $scope.union = { useAmount: true };
    //        if ($scope.paycodes.length > 0) {
    //            $scope.union.paycodeId = $scope.paycodes[0].id;
    //        }
    //        $scope.isLoading = false;
    //    });
    //});
    //$scope.isDuesDeductionRateValid = function (value) {
    //    return unionValidationRules.isDuesDeductionRateValid(value, $scope.union);
    //};

    //$scope.isDuesDeductionAmountValid = function (value) {
    //    return unionValidationRules.isDuesDeductionAmountValid(value, $scope.union);
    //};

    //$scope.isDuesMinAmountValid = function (value) {
    //    return unionValidationRules.isDuesMinAmountValid(value, $scope.union);
    //};

    //$scope.isDuesMaxAmountValid = function (value) {
    //    return unionValidationRules.isDuesMaxAmountValid(value, $scope.union);
    //};

    //$scope.save = function () {
    //    if ($scope.editUnionForm.$invalid) {
    //        return;
    //    }
    //    $scope.isSaving = true;
    //    unionService.createUnion($scope.companyId, $scope.union).then(function () {
    //        $scope.isSaving = false;
    //        $location.url($scope.backUrl);
    //    });
    //};
}]);