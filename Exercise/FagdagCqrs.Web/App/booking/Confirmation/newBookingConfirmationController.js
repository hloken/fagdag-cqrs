angular.module('fagdagCqrsHotel').controller('NewBookingConfirmationController', [
    '$scope', '$q', '$routeParams', '$location', 'RoomTypeService', 'BookingService', function($scope, $q, $routeParams, $location, roomTypeService, bookingService) {
        $scope.bookingId = $routeParams.bookingId;
        $scope.isLoading = true;
        $scope.backUrl = '/booking';

        var roomTypePromise = roomTypeService.getRoomTypes(),
            bookingPromise = bookingService.getBooking($scope.bookingId);

        $q.all([roomTypePromise, bookingPromise]).then(function(results) {
            var roomTypesMap = roomTypeService.mapRoomTypesToMap(results[0]);
            $scope.booking = results[1];
            $scope.booking.roomTypeName = roomTypesMap[$scope.booking.roomType].title;

            $scope.isLoading = false;
        });

        $scope.save = function () {
            bookingService.confirmBooking($scope.booking.id).then(
                $location.url($scope.backUrl)
            );
        };
    }
]);