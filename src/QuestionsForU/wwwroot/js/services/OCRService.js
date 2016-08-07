function OCRServices($http) {
    var self = this,
        Name = null,
        ints = null;

    this.$get = function() {
        $http({
            method: 'GET',
            url: 'api/ocr'
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            self.Name = response.data[0] + ' ' + response.data[1];
            //console.log(response)
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            alert(response);
        });
    }

    this.$get();

    this.$post = function(data) {
        $http({
            method: 'POST',
            url: 'api/ocr',
            data: data
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            self.ints = response.data;

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            alert(response);
        });
    }

    return this;
}