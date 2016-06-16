app.factory("musicService", ["$rootScope", function ($rootScope) {
    var svc = {};
    var data = [
    { name: "Vaibhav", genre: "Rock", rating: 4 },
    { name: "Ajay", genre: "Alternative", rating: 5 },
    { name: "Animesh", genre: "Rap", rating: 5 },
    { name: "Arun", genre: "SciFi", rating: 2 },
    { name: "Vijay", genre: "Comedy", rating: 1 },
    ];
    svc.getArtists = function () {
        return data;
    };
    svc.addArtists = function (artist) {
        data.push(artist);
    };
    svc.editArtists = function (index, artist) {
        data[index] = artist;
    };
    return svc;
}]);