angular.module('fagdagCqrsHotel').service('BookingService', ['$http', 'Settings', function ($http, settings) {
    this.baseUrl = this.baseUrl = settings.apiUrl + '/api/booking';

    this.getBookings = function () {
        return $http.get(this.baseUrl).then(function (result) {
            return result.data;
        });
    };

    this.getBooking = function (bookingId) {
        var url = this.baseUrl + '/' + bookingId;

        return $http.get(url).then(function (result) {
            return result.data;
        });
    };

    this.createBooking = function (newBooking) {
        return $http.post(this.baseUrl, newBooking);
    };

    this.confirmBooking = function (bookingId) {
        var url = this.baseUrl + '/' + bookingId + '/confirm';
        return $http.post(url);
    }
}]);