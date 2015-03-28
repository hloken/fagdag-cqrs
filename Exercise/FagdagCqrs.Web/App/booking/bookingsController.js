angular.module('fagdagCqrsHotel').controller('BookingsController', ['$scope', '$location', '$routeParams', 'BookingService', 'RoomTypeService', '$q', function ($scope, $location, $routeParams, bookingService, roomTypeService, $q) {
    $scope.isLoading = true;
    $scope.backUrl = '/main';

    var roomTypePromise = roomTypeService.getRoomTypes(),
        bookingsPromise = bookingService.getBookings();
    
    $q.all([roomTypePromise, bookingsPromise]).then(function (results) {
        $scope.roomTypesMap = roomTypeService.mapRoomTypesToMap(results[0]);
        $scope.bookings = results[1];

        _.each($scope.bookings, function(booking) {
            booking.roomTypeName = $scope.roomTypesMap[booking.roomType];
        });

        $scope.isLoading = false;
    });
}]);