angular.module('fagdagCqrsHotel').service('RoomTypeService', ['$http', 'Settings', function ($http, settings) {
    this.baseUrl = settings.apiUrl+ '/api/roomtypes';

    this.getRoomTypes = function () {
        return $http.get(this.baseUrl).then(function (result) {
            var roomTypesList = result.data;

            return roomTypesList;
        });
    };

    this.mapRoomTypesToMap = function(roomTypesList) {
        var roomTypesMap = {};
        _.each(roomTypesList, function (roomType) {
            roomTypesMap[roomType.id] = {
                title: roomType.title,
                pricePerNight: roomType.pricePerNight
            }
        });

        return roomTypesMap;
    }
}]);