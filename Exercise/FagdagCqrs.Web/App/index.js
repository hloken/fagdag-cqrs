angular.module('fagdagCqrsHotel', [
    'ngRoute'
])
    .config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/main', { controller: 'MainController', templateUrl: 'App/Main/main.html' }).
        when('/booking', { controller: 'BookingsController', templateUrl: 'App/Booking/bookings.html' }).
        when('/booking/new', { controller: 'NewBookingController', templateUrl: 'App/Booking/newBooking.html' }).
        when('/booking/new/:bookingId/confirmation', { controller: 'NewBookingConfirmationController', templateUrl: 'App/Booking/Confirmation/newBookingConfirmation.html' }).
        otherwise({ redirectTo: '/main' });
    }]);


