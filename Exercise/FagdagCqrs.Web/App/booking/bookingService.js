angular.module('fagdagCqrsHotel').service('BookingService', ['$http', 'Settings', function ($http, settings) {
    this.baseUrl = this.baseUrl = settings.apiUrl + '/api/booking';

    this.getBookings = function () {
        return $http.get(this.baseUrl).then(function (result) {
            return result.data;
        });
    };
    
    //this.updateUnion = function (companyId, union) {
    //    return $http.put(settings.apiBaseUrl + 'api/payroll/company/' + companyId + '/union/' + union.id, union);
    //};

    this.createBooking = function (newBooking) {
        return $http.post(this.baseUrl, newBooking);
    };
}]);