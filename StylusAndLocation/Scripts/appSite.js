/// <reference path="_references.js" />
//'use strict';

(function () {

    //App Module
    var app = angular.module('displayLinks', ['ngRoute', 'ngResource', 'ng-q-timeout']);

    //App Provider
    angular.module('displayLinks').provider('bkiService', function () {
        var usrNm = '';
        this.userName = function (name) {
            usrNm = name;
        };
        //this.$get = ['$q', function ($q) {
        this.$get = function () {

            var calcNameService = {};

            // Dependency Injector
            //angular.module('app').controller('Avengers', Avengers);
           // Avengers.$inject = ['moviesPrepService'];

            var myInjector = angular.injector(["ng"]);
            var $q = myInjector.get("$q");

            var context = SP.ClientContext.get_current();
            var user = context.get_web().get_currentUser();
            var printableName = "";
            calcNameService.getUserName = function () {
                var deferred = $q.defer();
                context.load(user);
                context.executeQueryAsync(
                    function () {
                        printableName = user.get_title().split("\\")[usrNm];
                        $('#message').text('Hello ' + printableName + ", ");
                        deferred.resolve(printableName);
                    },
                    function () {
                        console.log('Failed to get user name. Error:' + args.get_message());
                        deferred.reject(args.get_message());
                    }
                );
                return deferred.promise;
            };

            return calcNameService;
            //}];
        };
    });

    //App config
    angular.module('displayLinks').config(['bkiServiceProvider', '$routeProvider', 'detailsProvider', '$httpProvider','$provide',
    function (bkiServiceProvider, $routeProvider, detailsProvider, $httpProvider, $provide) {

        //Decorator for $log service using Provider
        $provide.decorator('$log', ['$delegate', 'bkiService', function ($delegate, bkiService) {
            var usr = '';
            bkiService.getUserName().then(function (response) {
                console.log(response);
                usr = response;
            });

            function log(message) {
                message = ' A log has been created for - ' + usr+ ', on '+ new Date(); + 'The log is: ' + message;
                $delegate.log(message);
            }
            function info(message) {
                message = ' An info log has been created for - ' + usr+ ', on '+ new Date(); + 'The log is: ' + message;
                $delegate.info(message);
            }
            //function warn(message) {
            //    message = ' A warning log has been created for - ' + usr + ', on ' + new Date(); + 'The log is: ' + message;
            //    $delegate.warn(message);
            //}
            //function error(message) {
            //    message = ' An error log has been created for - ' + usr + ', on ' + new Date(); + 'The log is: ' + message;
            //    $delegate.error(message);
            //}
            function debug(message) {
                message = ' A debug log has been created for - ' + usr + ', on ' + new Date(); + 'The log is: ' + message;
                $delegate.debug(message);
            }
            function awesome(message) {
                message = 'Awesome!!! - ' + ' A log has been created for - ' + usr + ', on ' + new Date(); + 'The log is: ' + message;
                //message = 'Awesome!!! - ' + message;
                $delegate.debug(message);
            }

            return {
                log: log,
                info: info,
                //warn: warn,
                //error: error,
                debug: debug,
                awesome: awesome
            }

        }]);

        //Register Interceptor
        $httpProvider.interceptors.push('CustomHttpLoggerInterceptor');

        //Provider Call from Config
        bkiServiceProvider.userName('0');

        $routeProvider
            .when('/', {
                redirectTo: '/links2'
            })
            //.when('/links', {
            //    templateUrl: "../HTML/links.html",
            //    controller: "HomeController"
            //})
            .when('/links2', {
                templateUrl: "../HTML/links2.html",
                controller: "HomeController4"//,
                //resolve: {
                //        locationDetails: function (details) {
                //                debugger;
                //                var data = '';

                //                // Dependency Injector
                //                //angular.module('app').controller('Avengers', Avengers);
                //                // Avengers.$inject = ['moviesPrepService'];

                //                //var myInjector = angular.injector(["ng"]);
                //                //var details = myInjector.get("details");
                //                //debugger;
                //                //detailsProvider.getLocationRequest();
                //                //debugger;
                //                details.getLocationRequest().then(function (data) {
                //                    debugger;
                //                    //$log.debug(data);
                //                    data = data;
                //                });
                //                return data;
                //                //return "100";
                //            },
                //}
            })
            .when('/links02', {
                templateUrl: "../HTML/links2.html",
                controller: "HomeController4",
                resolve: {
                    //"resolveCtrl": ['details', function (details) {
                    "resolveCtrl": function () {
                        return {
                            //locationDetails: ['details', function (details) {
                            //locationDetails: ['details', '$q', '$http', function (details, $q, $http) {
                            //locationDetails: function (details) {


                            //myData: ['$http', '$q', function($http, $q) {
                            //    var defer = $q.defer();

                            locationDetails: function (detailsProvider) {
                                debugger;
                                var data = '';

                                // Dependency Injector
                                //angular.module('app').controller('Avengers', Avengers);
                                // Avengers.$inject = ['moviesPrepService'];

                                var myInjector = angular.injector(["ng"]);
                                var details = myInjector.get("detailsProvider");
                                debugger;
                                detailsProvider.getLocationRequest();
                                debugger;
                                details.getLocationRequest().then(function (data) {
                                    debugger;
                                    //$log.debug(data);
                                    data = data;
                                });
                                return data;
                                //return "100";
                            },//}],
                            roomNo: function( ) {
                                return "100";
                            }
                        };
                    }//}]
                }
            })
            .when('/links3', {
                templateUrl: "../HTML/links3.html",
                controller: "HomeController3"
            })
            .otherwise({
                redirectTo: '/links'
            });

    }]);

    //App Directive for Forms Data
    angular.module('displayLinks').directive('ngAppFrame', ['$timeout', '$window', function ($timeout, $window) {

        return {
            restrict: 'A',
            link: function (scope, element, attrs) {

                element.css("display", "block");

                function getQsParam(name) {
                    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                    return results === null ? "" : results[1].replace(/\+/g, " ");
                }

                scope.$watch
                (
                    function () {
                        return element[0].offsetHeight;
                    },
                    function (newHeight, oldHeight) {

                        if (newHeight != oldHeight) {
                            $timeout(function () {
                                var height = attrs.minheight ? newHeight + parseInt(attrs.minheight) : newHeight;
                                var id = getQsParam("SenderId")
                                var message = "<message senderId=" + id + ">resize(100%," + height + ")</message>";
                                $window.parent.postMessage(message, "*");
                            }, 0);// timeout needed to wait for DOM to update
                        }
                    }
                );
            }
        };

    }]);

    //App Directive for Forms Data Category
    angular.module('displayLinks').directive('nwCategorySelector', function (details) {
        return {
            restrict: "E",
            require: "?ngModel",
            templateUrl: "../HTML/nwCategorySelector.html",
            link: function (scope, element, attrs, ngModelCtrl) {
                //var activeCategory = {};
                //details.getCategoryData().then(function (data) {
                //    scope.categories = data.d.results;
                //});
                //scope.isActive = function (category) {
                //    return activeCategory && activeCategory.ID === category.ID;
                //}
                //scope.toggleCategory = function (category) {
                //    if (category === activeCategory) {
                //        activeCategory = {};
                //    }
                //    else {
                //        activeCategory = category;
                //    }
                //    ngModelCtrl.$setViewValue(activeCategory);
                //}
                //ngModelCtrl.$render = function () {
                //    activeCategory = ngModelCtrl.$viewState;
                //}
            }
        };
    });

    // Controllers
    //Controller 1
    angular.module('displayLinks').controller('HomeController', ['details', 'bkiService', 'constants', '$q', '$log', '$scope',
                                                    function (details, bkiService, constants, $q, $log, $scope) {
                                                        // $scope.title = constants.APP_HEADTITLE;
                                                        //$scope.hyperlink = constants.APP_LIST;

                                                        $scope.search = {};

                                                        debugger;
                                                        //$scope.formsData = resolvedInfo.data;

                                                        var _sample = "data:image/png;base64,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"

                                                        //$scope.formsData = resolvedInfo.formLinks().filter(function (item) {
                                                        //    debugger;
                                                        //    console.log(item);
                                                        //})[0];

                                                        //details.getRequestDetails()
                                                        //    .then(function (data) {
                                                        //        $scope.formsData = data.d.results;
                                                        //        $log.info(data.d.results);
                                                        //    })
                                                        //    .catch(function (data) {
                                                        //        $log.error(data);
                                                        //    });

                                                        $scope.item = [];
                                                        //$scope.loginName = bkiService.getUserName().then(function (response) {
                                                        //    console.log(response);
                                                        //    //debugger;
                                                        //    $scope.item.userName = response;
                                                        //});

                                                    }]);
    //Controller 2
    angular.module('displayLinks').controller('HomeController2', ['details', 'constants', '$q', '$log', '$scope', function (details, constants, $q, $log, $scope) {


        var wrapper = document.getElementById("signature-pad"),
            clearButton = wrapper.querySelector("[data-action=clear]"),
            saveButton = wrapper.querySelector("[data-action=save]"),
            canvas = wrapper.querySelector("canvas"),
            signaturePad;

        // Adjust canvas coordinate space taking into account pixel ratio,
        // to make it look crisp on mobile devices.
        // This also causes canvas to be cleared.
        function resizeCanvas() {
            // When zoomed out to less than 100%, for some very strange reason,
            // some browsers report devicePixelRatio as less than 1
            // and only part of the canvas is cleared then.
            var ratio = Math.max(window.devicePixelRatio || 1, 1);
            canvas.width = canvas.offsetWidth * ratio;
            canvas.height = canvas.offsetHeight * ratio;
            canvas.getContext("2d").scale(ratio, ratio);
        }

        window.onresize = resizeCanvas;
        resizeCanvas();

        signaturePad = new SignaturePad(canvas);

        clearButton.addEventListener("click", function (event) {
            signaturePad.clear();
        });

        saveButton.addEventListener("click", function (event) {

            if (signaturePad.isEmpty()) {
                alert("Please provide signature first.");
            } else {
                //debugger;

                var image1 = signaturePad.toDataURL("image/png").replace("image/png", "image/octet-stream");  // here is the most important part because if you dont replace you will get a DOM 18 exception.
                var image2 = signaturePad.toDataURL("image/png", 1);
                var image = new Image();
                image.src = signaturePad.toDataURL("image/png", 1);

                var dataURL = signaturePad.toDataURL("image/png", 1);
                var a = dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
                var aa = dataURL.replace(/^data:/, "");
                //image/png;base64,
                debugger;
                localStorage.setItem("signature", dataURL);

                var signature = localStorage.getItem("signature");


                //canvas.toDataURL("image/jpeg")
                window.location.href = image;
                //document.write('<img src="' + image2 + '"/>');
                $("#img1").attr("src", image2);


                //$('#input1').attr('type', 'file').attr('file', image2);
                //document.getElementById('canvasImg').src = dataURL;
                //<img id="canvasImg" alt="Right click to save me!">

                debugger;


                var decodeBase64b = function (s) {
                    var e = {}, i, b = 0, c, x, l = 0, a, r = '', w = String.fromCharCode, L = s.length;
                    var A = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
                    for (i = 0; i < 64; i++) { e[A.charAt(i)] = i; }
                    for (x = 0; x < L; x++) {
                        c = e[s.charAt(x)]; b = (b << 6) + c; l += 6;
                        while (l >= 8) { ((a = (b >>> (l -= 8)) & 0xff) || (x < (L - 2))) && (r += w(a)); }
                    }
                    return r;
                };

                var decodeBase64a = function (s) {
                    var b = l = 0, r = '',
                    m = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'.split('');
                    s.split('').forEach(function (v) {
                        b = (b << 6) + m.indexOf(v); l += 6;
                        while (l >= 8) r += String.fromCharCode((b >>> (l -= 8)) & 0xff);
                    });
                    return r;
                }

                var decodeBase64 = function (s) {
                    var e = {}, i, k, v = [], r = '', w = String.fromCharCode;
                    var n = [[65, 91], [97, 123], [48, 58], [43, 44], [47, 48]];

                    for (z in n) {
                        for (i = n[z][0]; i < n[z][1]; i++) {
                            v.push(w(i));
                        }
                    }
                    for (i = 0; i < 64; i++) {
                        e[v[i]] = i;
                    }

                    for (i = 0; i < s.length; i += 72) {
                        var b = 0, c, x, l = 0, o = s.substring(i, i + 72);
                        for (x = 0; x < o.length; x++) {
                            c = e[o.charAt(x)];
                            b = (b << 6) + c;
                            l += 6;
                            while (l >= 8) {
                                r += w((b >>> (l -= 8)) % 256);
                            }
                        }
                    }
                    return r;
                }

                //image1 = image1.split(",")[1];
                a = decodeBase64b(a);

                var image3 = signaturePad.toDataURL("image/png");
                image3 = image.replace('data:image/png;base64,', '');

                debugger;
                $.ajax({
                    url: constants.IMAGE_LIB,
                    //method: "POST",
                    // processData: false,
                    type: "POST",
                    // async: false,
                    //data: JSON.stringify(dataUpdate),
                    //transformRequest: angular.identity,
                    // binaryStringRequestBody: true,
                    //contentType: 'application/json; charset=utf-8',
                    // dataType: 'json',
                    data: image3,// '{ "imageData" : "' + data + '" }',//JSON.stringify(data),
                    headers: {
                        "Accept": "application/json; odata=verbose",
                        //"content-type": "application/json; odata=verbose",
                        "content-type": "application/json; charset=utf-8",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val()
                        // , "content-length": image2.length,
                        //"X-HTTP-Method": "MERGE",
                        //"IF-MATCH": "*"
                    },
                    complete: function (data) {
                        //uploaded pic url
                        debugger;
                        console.log(data);
                        //console.log(data.responseJSON.d.ServerRelativeUrl);
                        //$route.reload();
                    },
                    error: function (err) {
                        debugger;
                        console.log(err);
                    }
                });


                //details.postRequest(image2)
                //	.then(function (response) {
                //	    console.log(response);
                //	});

                //window.open(signaturePad.toDataURL());
            }
        });

    }]);
    //Controller 3
    angular.module('displayLinks').controller('HomeController3', ['details', 'constants', '$q', '$log', '$scope', function (details, constants, $q, $log, $scope) {

        var appWebUrl = "";
        var targetSiteUrl = "";
        var digest = "";
        var ready = true;//false;


        //function UploadFile() {
        $scope.UploadFile = function () {
            debugger;
            appWebUrl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0];

            $.getScript(appWebUrl + "/_layouts/15/SP.RequestExecutor.js", function () {
                ready = true;
            });



            // if we have not finished loading scripts then display an alert
            if (!ready) {
                alert("Oooooops... Please wait for the page to finish loading before attempting to upload a file");
                return;
            }

            // get the library name and file reference
            var docLibrary = $('#docLibraryNameInput').val();
            var fileInput = $('#fileSelectorInput');

            // if there is no document library name alert the user and return
            if (!docLibrary || docLibrary == '') {
                alert("Oooooops... It doesn't look like you entered the name of the library you would like to upload your file to");
                return;
            }

            // if we couldnt get a reference to the file input then alert the user and return
            if (!fileInput || fileInput.length != 1) {
                alert('Oooooops... An error occured processing your input.');
                return;
            }
            else if (!fileInput[0].files) {
                alert("Oooooops... It doesn't look like your browse supports uploading files in this way");
                return;
            }
            else if (fileInput[0].files.length <= 0) {
                alert("Oooooops it doesn't look like you selected a file. Please select a file and try again.");
                return;
            }

            // for each file in the list of files process the upload
            for (var i = 0; i < fileInput[0].files.length; i++) {
                var file = fileInput[0].files[i];
                ProcessUpload(file, docLibrary, '');
            }
        }

        function ProcessUpload(fileInput, docLibraryName, folderName) {
            debugger;
            var reader = new FileReader();
            reader.onload = function (result) {
                var fileName = '',
                 libraryName = '',
                 fileData = '';

                var byteArray = new Uint8Array(result.target.result)
                for (var i = 0; i < byteArray.byteLength; i++) {
                    fileData += String.fromCharCode(byteArray[i])
                }

                // once we have the file perform the actual upload
                PerformUpload(docLibraryName, fileInput.name, folderName, fileData);

            };
            reader.readAsArrayBuffer(fileInput);
        }

        function PerformUpload(libraryName, fileName, folderName, fileData) {
            var url;

            // if there is no folder name then just upload to the root folder
            //if (folderName == "") {
            //    url = appWebUrl + "/_api/SP.AppContextSite(@TargetSite)/web/lists/getByTitle(@TargetLibrary)/RootFolder/Files/add(url=@TargetFileName,overwrite='true')?" +
            //        "@TargetSite='" + targetSiteUrl + "'" +
            //        "&@TargetLibrary='" + libraryName + "'" +
            //        "&@TargetFileName='" + fileName + "'";
            //}
            //else {
            //    // if there is a folder name then upload into this folder
            //    url = appWebUrl + "/_api/SP.AppContextSite(@TargetSite)/web/lists/getByTitle(@TargetLibrary)/RootFolder/folders(@TargetFolderName)/files/add(url=@TargetFileName,overwrite='true')?" +
            //       "@TargetSite='" + targetSiteUrl + "'" +
            //       "&@TargetLibrary='" + libraryName + "'" +
            //       "&@TargetFolderName='" + folderName + "'" +
            //       "&@TargetFileName='" + fileName + "'";
            //}



            // use the request executor (cross domain library) to perform the upload
            debugger;
            //var reqExecutor = new SP.RequestExecutor(appWebUrl);
            var reqExecutor = new SP.RequestExecutor(appWebUrl);
            reqExecutor.executeAsync({
                url: constants.IMAGE_LIB,// url,
                method: "POST",
                headers: {
                    "Accept": "application/json; odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()//digest
                },
                contentType: "application/json;odata=verbose",
                binaryStringRequestBody: true,
                body: fileData,
                success: function (x, y, z) {
                    debugger;
                    alert("Success! Your file was uploaded to SharePoint.");
                },
                error: function (x, y, z) {
                    debugger;
                    alert("Oooooops... it looks like something went wrong uploading your file.");
                }
            });
            debugger;
            //$.ajax({
            //    url: constants.IMAGE_LIB,
            //    //method: "POST",
            //    // processData: false,
            //    type: "POST",
            //    // async: false,
            //    //data: JSON.stringify(dataUpdate),
            //    //transformRequest: angular.identity,
            //    binaryStringRequestBody: true,
            //    body: fileData,
            //    //contentType: 'application/json; charset=utf-8',
            //    // dataType: 'json',
            //    //data: image3,// '{ "imageData" : "' + data + '" }',//JSON.stringify(data),
            //    headers: {
            //        "Accept": "application/json; odata=verbose",
            //        //"content-type": "application/json; odata=verbose",
            //        "content-type": "application/json; charset=utf-8",
            //        "X-RequestDigest": $("#__REQUESTDIGEST").val()
            //        // , "content-length": image2.length,
            //        //"X-HTTP-Method": "MERGE",
            //        //"IF-MATCH": "*"
            //    },
            //    complete: function (data) {
            //        //uploaded pic url
            //        debugger;
            //        console.log(data);
            //        //console.log(data.responseJSON.d.ServerRelativeUrl);
            //        //$route.reload();
            //    },
            //    error: function (err) {
            //        debugger;
            //        console.log(err);
            //    }
            //});


            //// use the request executor (cross domain library) to perform the upload
            //appWebUrl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0];
            //var reqExecutor = new SP.RequestExecutor(appWebUrl);
            //reqExecutor.executeAsync({
            //    url: url,
            //    method: "POST",
            //    headers: {
            //        "Accept": "application/json; odata=verbose",
            //        "X-RequestDigest": digest
            //    },
            //    contentType: "application/json;odata=verbose",
            //    binaryStringRequestBody: true,
            //    body: fileData,
            //    success: function (x, y, z) {
            //        alert("Success! Your file was uploaded to SharePoint.");
            //    },
            //    error: function (x, y, z) {
            //        alert("Oooooops... it looks like something went wrong uploading your file.");
            //    }
            //});
        }

    }]);
    //Controller 4
    //angular.module('displayLinks').controller('HomeController4', ['details', 'constants', '$q', '$log', '$scope', 'locationDetails', function (details, constants, $q, $log, $scope, locationDetails) {
    angular.module('displayLinks').controller('HomeController4', ['details', 'constants', '$q', '$log', '$scope', function (details, constants, $q, $log, $scope) {
        var appWebUrl = "";
        var targetSiteUrl = "";
        var digest = "";
        var ready = true;//false;

        appWebUrl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0];

        $.getScript(appWebUrl + "/_layouts/15/SP.RequestExecutor.js", function () {
            ready = true;
        });

        var wrapper = document.getElementById("signature-pad"),
            clearButton = wrapper.querySelector("[data-action=clear]"),
            saveButton = wrapper.querySelector("[data-action=save]"),
            canvas = wrapper.querySelector("canvas"),
            signaturePad;

        // Adjust canvas coordinate space taking into account pixel ratio,
        // to make it look crisp on mobile devices.
        // This also causes canvas to be cleared.
        function resizeCanvas() {
            // When zoomed out to less than 100%, for some very strange reason,
            // some browsers report devicePixelRatio as less than 1
            // and only part of the canvas is cleared then.
            //debugger;
            var ratio = Math.max(window.devicePixelRatio || 1, 1);
            canvas.width = canvas.offsetWidth * ratio;
            canvas.height = canvas.offsetHeight * ratio;
            canvas.getContext("2d").scale(ratio, ratio);
        }

        window.onresize = resizeCanvas;
        resizeCanvas();
        //resizeCanvas();

        signaturePad = new SignaturePad(canvas);

        clearButton.addEventListener("click", function (event) {
            signaturePad.clear();
        });

        saveButton.addEventListener("click", function (event) {

            if (signaturePad.isEmpty()) {
                alert("Please provide signature first.");
            } else {
                //var image1 = signaturePad.toDataURL("image/png").replace("image/png", "image/octet-stream");  // here is the most important part because if you dont replace you will get a DOM 18 exception.
                var image2 = signaturePad.toDataURL("image/png", 1);

                var fileInput = dataURItoBlob(image2, 'image/png');

                function dataURItoBlob(dataURI, type) {
                    // convert base64 to raw binary data held in a string
                    var byteString = atob(dataURI.split(',')[1]);

                    // separate out the mime component
                    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0]

                    // write the bytes of the string to an ArrayBuffer
                    var ab = new ArrayBuffer(byteString.length);
                    var ia = new Uint8Array(ab);
                    for (var i = 0; i < byteString.length; i++) {
                        ia[i] = byteString.charCodeAt(i);
                    }

                    // write the ArrayBuffer to a blob, and you're done
                    var bb = new Blob([ab], { type: type });
                    return bb;
                }

                ProcessUpload(fileInput, '', '');

                function ProcessUpload(fileInput, docLibraryName, folderName) {
                    //debugger;
                    var reader = new FileReader();
                    reader.onload = function (result) {
                        var fileName = '',
                         libraryName = '',
                         fileData = '';

                        var byteArray = new Uint8Array(result.target.result)
                        for (var i = 0; i < byteArray.byteLength; i++) {
                            fileData += String.fromCharCode(byteArray[i])
                        }

                        // once we have the file perform the actual upload
                        //PerformUpload(docLibraryName, fileInput.name, folderName, fileData);
                        //debugger;
                        details.postDocImageData(fileData, appWebUrl)
                            .then(function (data) {
                                //$scope.formsData = data.d.results;
                                //debugger;
                                $log.info(data);
                                signaturePad.clear();
                            })
                            .catch(function (data) {
                                debugger;
                                $log.error(data);
                            });

                    };
                    reader.readAsArrayBuffer(fileInput);
                }
            }
        });



        // Get Location
        //debugger;
        //$scope.location1 = locationDetails.d;//resolveCtrl.locationDetails();
        //    .then(function (data) {
        //        debugger;
        //        $log.debug(data);
        //        $scope.locationName = data;
        //});
        //debugger;


        details.getLocationRequest()
            .then(function (data) {
                $log.debug(data);
                $scope.locationName = data;
            })
            .finally(function (data) {
                $log.info(data);
            });


    }]);



    // Factory Service
    angular.module('displayLinks').factory("details", ["constants", "$http", "$q", "$resource", '$log', function (constants, $http, $q, $resource, $log) {

        //JSOM Post Canvas Stylus image
        var postDocImageData = function (fileData, appWebUrl) {
            var deferred = $q.defer();
           // debugger;
            var reqExecutor = new SP.RequestExecutor(appWebUrl);
            reqExecutor.executeAsync({
                url: constants.IMAGE_LIB,
                method: "POST",
                headers: {
                    "Accept": "application/json; odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()
                },
                contentType: "application/json;odata=verbose",
                binaryStringRequestBody: true,
                body: fileData,
                success: function (result) {
                    //debugger;
                    deferred.resolve(result);
                },
                error: function (result, status) {
                    //debugger;
                    deferred.reject(result);
                }
            });
            return deferred.promise;
        }
        // HTTP post Canvas Stylus image
        var postDocImageData1 = function (fileData, appWebUrl) {
            var deferred = $q.defer();
            debugger;
            $http({
                url: constants.IMAGE_LIB,
                method: "POST",
                //processData: false,
                //data: JSON.stringify(dataUpdate),
                data: fileData,//body
                // transformRequest: angular.identity,
                binaryStringRequestBody: true,
                headers: {
                    "Accept": "application/json; odata=verbose",
                    "content-type": "application/json; odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                    //"content-length": dataUpdate.length,
                    // "X-HTTP-Method": "MERGE",
                    // "IF-MATCH": "*"
                }
            })
                .success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (result, status) {
                    deferred.reject(status);
                });
            return deferred.promise;
        }

        //Get Location HTTP :- method: "GET"
        var getLocationRequest = function () {
            var deferred = $q.defer();
            //debugger;
            $http({
                url: "http://ipinfo.io",
                method: "GET",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "content-Type": "application/json;odata=verbose"
                }
            })
                .success(function (result) {
                    var a = $.parseHTML(result);
                    //$log.info('ip: '+$.trim($($(a).find('h1#heading')[0]).text()));
                    //$log.info('location: ' + $.trim($($(a).find('img.map')[0]).attr("alt").split(',')[0]));

                    //Location Name from table
                    //$($($(a).find("table:first")[0]).find("tr")[2]).find("td")[1]
                    //$($($(a).find("table:first")[0]).find("tr")[2]).find("a[href$='/countries/au']")[0]
                    //$($(a).find("table:first")[0]).find("a[href$='/countries/au']")[0]
                    var d = $.trim($($($($(a).find("table:first")[0]).find("tr")[2]).find("td")[1]).text());
                    //debugger;
                    deferred.resolve(d);
                })
                .error(function (result, status) {
                    //debugger;
                    deferred.reject(status);
                });
            return deferred.promise;
        };


        function getRequestDetails() {
            var resource = getdetails();
            var deferred = $q.defer();
            //debugger;.
            resource.query({},
                function (data) {
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        function getdetails(id) {
            //var urlSite = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
            //            "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
            //            decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'";
            var url = constants.HYPERLINK_URL;
            if (id) {
                return $resource(
                    //urlSite,
                    url,
                    { id: id },
                    {
                        query: {
                            method: 'GET', //isArray: false, 
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.HYPERLINK_SELECT,
                                '$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
            else {
                return $resource(
                    url,
                    //urlSite,
                    {},
                    {
                        query: {
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                //'$select': "'" + constants.HYPERLINK_SELECT + "'",
                                //'$expand': "'" + constants.HYPERLINK_EXPAND + "'"
                                '$select': constants.HYPERLINK_SELECT,
                                '$expand': constants.HYPERLINK_EXPAND
                            },
                            transformResponse: function (data, headersGetter) {
                                //debugger;
                                var transformed = angular.fromJson(data);
                                transformed.d.results.forEach(function (currentValue, index, array) {
                                    currentValue.dateDownloaded = new Date();
                                });
                                $log.log(transformed.d.results);
                                return transformed;
                            },
                        }
                    }
                );
            }
        }

        //Forms Data Category
        function getCategoryData() {
            var resource = getCategoryDetails();
            var deferred = $q.defer();

            resource.query({},
                function (data) {
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        // HTTP Post
        var postRequest = function (data) {
            debugger;

            /////
            var reader = new FileReader();
            reader.onload = function (result) {
                var fileName = '',
                 libraryName = '',
                 fileData = '';

                var byteArray = new Uint8Array(result.target.result)
                for (var i = 0; i < byteArray.byteLength; i++) {
                    fileData += String.fromCharCode(byteArray[i])
                }

                // once we have the file perform the actual upload
                //PerformUpload(docLibraryName, fileInput.name, folderName, fileData);

            };
            //reader.readAsArrayBuffer(data);

            //////
            //dataURL = signaturePad.toDataURL().replace('data:image/png;base64,', '');
            var dataImg = JSON.stringify(
                               {
                                   value: data
                               });

            //'{ "imageData" : "' + Pic + '" }',

            var dataMeta = {
                "__metadata": { "type": constants.ITEM_TYPE }
            };
            var dataUpdate = $.extend({}, dataMeta, data);
            //console.log(dataUpdate);													
            var deferred = $q.defer();

            $http({
                url: constants.IMAGE_LIB,
                method: "POST",
                //processData: false,
                //data: JSON.stringify(dataUpdate),
                transformRequest: angular.identity,
                binaryStringRequestBody: true,
                body: data,// '{ "imageData" : "' + data + '" }',//JSON.stringify(data),
                headers: {
                    "Accept": "application/json; odata=verbose",
                    "content-type": "application/json; odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()
                    //"content-length": dataUpdate.length,
                    //"X-HTTP-Method": "MERGE",
                    //"IF-MATCH": "*"
                }
            })
                .success(function (result) {
                    debugger;
                    deferred.resolve(result);
                })
                //.error(function (result, status) {
                .error(function (error) {
                    debugger;
                    deferred.reject(error);
                });
            return deferred.promise;
        };


        function getCategoryDetails(id) {
            if (id) {
                return $resource(
                    constants.CATEGORY_URL,
                    { id: id },
                    {
                        query: {
                            method: 'GET', //isArray: false, 
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.CATEGORY_SELECT,
                                //'$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
            else {
                return $resource(
                    constants.CATEGORY_URL,
                    {},
                    {
                        query: {
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.CATEGORY_SELECT,
                                //'$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
        }
        return {
            getRequestDetails: getRequestDetails,
            getCategoryData: getCategoryData,
            postRequest: postRequest,
            postDocImageData: postDocImageData,
            getLocationRequest: getLocationRequest
        };
    }]);
    // HTTP Interceptors
    angular.module('displayLinks').factory('CustomHttpLoggerInterceptor', ['$q', '$log', function ($q, $log) {
        return {
            request: requestInterseptor,
            requestError: requestErrorInterseptor,
            response : responseInterseptor,
            responseError: responseErrorInterseptor
        };

        function requestInterseptor(config) {
            $log.debug('HTTP ' + config.method + ' request - ' + config.url);
            //return config;
            // Return the config or wrap it in a promise if blank.
            return config || $q.when(config);
        }

        function requestErrorInterseptor(config) {
            $log.debug('HTTP Response Error: ' + rejection);
            return $q.reject(rejection);
        }

        function responseInterseptor(response) {
            if (response.status === 401) {
                //  Redirect user to login page / signup Page.
            }
            $log.debug('HTTP response: ' +response);
            // Return the response or promise.
            return response || $q.when(response);
        }

        function responseErrorInterseptor(response) {
            $log.debug('HTTP ' + response.config.method + 'response error - ' + response.config);
            return $q.reject(response);
        }

    }]);

    //Constant Service
    angular.module('displayLinks').constant('constants', {
        APP_TITLE: 'Book Logger',
        APP_HEADTITLE: decodeURIComponent(getQueryStringParameter("AppTitle")),
        APP_LIST: decodeURIComponent(getQueryStringParameter("ListName")),
        HYPERLINK_SELECT: 'ID,Title,hyperlink,hyperlinkTitle,hyperlinkTarget,DepartmentCategoryId,DepartmentCategory/DepartmentCategory,DepartmentCategory/Id',
        HYPERLINK_EXPAND: 'DepartmentCategory',
        HYPERLINK_URL: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("ListName")) + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",

        CATEGORY_SELECT: 'ID,DepartmentCategory',
        CATEGORY_URL: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("CategoryListName")) + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
        ITEM_TYPE: getItemTypeForListName(decodeURIComponent(getQueryStringParameter("CategoryListName"))),
        IMAGE_LIB: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0]
                        + "/_api/SP.AppContextSite(@TargetSite)/web/lists/getByTitle(@TargetLibrary)/RootFolder/Files/add(url=@TargetFileName,overwrite='true')?" +
                        "@TargetSite='" + decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'" +
                        //"&@TargetLibrary='" + decodeURIComponent(getQueryStringParameter("CategoryListName")) + "'" +
                        "&@TargetLibrary='DemoPictureLibrary'" +
                        "&@TargetFileName='" + '111.png' + "'"
        //"&@TargetFileName='" + fileName + "'"

    });

    //General
    //General -- Get App Part Preperties
    function getQueryStringParameter(paramToRetrieve) {
        var params;
        var strParams;

        params = document.URL.split("?")[1].split("&");
        strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve)
                return singleParam[1];
        }
    }
    //General -- Get APP URL
    function manageQueryStringParameter(paramToRetrieve) {
        var params =
        document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }
    //General -- Item Type for post call
    function getItemTypeForListName(name) {
        return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
    }

})();
