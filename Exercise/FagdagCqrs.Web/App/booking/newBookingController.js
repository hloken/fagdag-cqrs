angular.module('fagdagCqrsHotel').controller('NewBookingController', ['$scope', '$location', 'RoomTypeService', 'BookingService', function ($scope, $location, roomTypeService, bookingService) {
    $scope.isLoading = true;
    $scope.confirmationUrl = '/booking';

    roomTypeService.getRoomTypes().then(function(roomTypesList) {
        $scope.roomTypes = roomTypesList;

        $scope.newBooking = {
            roomType: undefined,
            fromDate : new Date(),
            duration : 1
        };

        $scope.isLoading = false;
    });

    $scope.save = function() {
        if ($scope.roomBookingForm.$invalid ||
            $scope.newBooking.duration < 1) {
            $scope.errorMessage = 'Bad booking, fix it';
        } else {
            bookingService.createBooking($scope.newBooking)
                .then(function (result) {
                    var bookingId = result.data.id;
                    var url = '/booking/new/' + bookingId + '/confirmation';
                    $location.url(url);
                });
            
        }
    }
}]);