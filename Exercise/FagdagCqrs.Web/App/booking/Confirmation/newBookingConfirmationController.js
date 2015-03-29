angular.module('fagdagCqrsHotel').controller('NewBookingConfirmationController', [
    '$scope', '$routeParams', '$location', 'RoomTypeService', 'BookingService', function($scope, $routeParams, $location, roomTypeService, bookingService) {
        $scope.bookingId = $routeParams.bookingId;
        $scope.isLoading = true;
        $scope.backUrl = '/booking';

        bookingService.getBooking($scope.bookingId).then(function (result) {
            $scope.isLoading = false;
            $scope.booking = result.data;
        });
        
        $scope.save = function () {
            bookingService.confirmBooking($scope.booking.id);
            $location.url($scope.backUrl);
        };
    }
]);