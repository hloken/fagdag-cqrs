angular.module('fagdagCqrsHotel').controller('NewBookingController', ['$scope', '$location', '$routeParams', 'RoomTypeService', 'BookingService', function ($scope, $location, $routeParams, roomTypeService, bookingService) {
    $scope.isLoading = true;
    $scope.backUrl = '/booking';

    roomTypeService.getRoomTypes().then(function(roomTypesList) {
        $scope.roomTypes = roomTypesList;

        $scope.newBooking = {
            roomType: undefined,
            fromDate : new Date(),
            duration : 1
        };

        $scope.isLoading = false;
    });

    $scope.send = function() {
        if ($scope.roomBookingForm.$invalid ||
            $scope.newBooking.duration < 1) {
            $scope.errorMessage = 'Bad booking, fix it';
        } else {
            bookingService.createBooking($scope.newBooking);
            $location.url($scope.backUrl);
        }
    }
}]);