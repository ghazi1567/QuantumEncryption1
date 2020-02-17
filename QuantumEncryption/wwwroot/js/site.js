// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var quantumModule;
(function () {
    if (typeof jQuery !== 'undefined') {

        quantumModule = (function () {
            var userBase = null;
            var confirmedBases = new Array();
            var currentIndex = 0;
            var init = function () {
                $frmLogin = $('#login-form');
                $tblUserBases = $('#tbl-user-bases thead');
                $btnKeyRequest = $('#btn-key-request');
                $btnSendData = $('#btn-send-data');
                $divLogin = $('#login-div');
                $divBase = $('#base-div');
                $divCIABase = $('#cia-base-div');
                $divBase.hide();
                $btnKeyRequest.hide();
                $divLogin.show();
                $divCIABase.empty();

                $txtUserId = $('#txt-UserId');
                $txtUserName = $('#txt-UserName');
                $txtUserKey = $('#txt-UserKey');
            };

            var bindEvents = function () {
               
                $frmLogin.submit(function (event) {
                    event.preventDefault(); //prevent default action 
                    var post_url = $(this).attr("action"); //get form action url
                    var request_method = $(this).attr("method"); //get form GET/POST method
                    //var form_data = $(this).serialize(); //Encode form elements for submission
                    var form_data = JSON.stringify(objectifyForm($(this).serializeArray()));
                    $.ajax({
                        contentType: "application/json",
                        url: post_url,
                        type: request_method,
                        data: form_data
                    }).done(function (response) { //
                        $btnKeyRequest.show();
                        $txtUserId.val(response.userId);
                        $txtUserName.val(response.name);
                        showUserBases(response);
                    }).fail(function (xhr, status, error) {
                        //Ajax request failed.
                        var errorMessage = xhr.status + ': ' + xhr.statusText
                        alert('Error - ' + errorMessage);
                    })
                });
                $btnKeyRequest.click(function () {
                    $divBase.show();
                    getQKBit();
                });
            }
            function objectifyForm(formArray) {//serialize data function

                var returnArray = {};
                for (var i = 0; i < formArray.length; i++) {
                    returnArray[formArray[i]['name']] = formArray[i]['value'];
                }
                return returnArray;
            }
            function showUserBases(obj) {
                
                $divLogin.hide();
                userBase = obj.userBases;
                $divBase.show();
                getQKBit();
              


               
            }

            function showConfirmedBases() {
                var head = '<tr>';
                var row = '<tr>';
                for (var i = 0; i < confirmedBases.length; i++) {
                    head += '<th scope="col">' + getHeadValue(confirmedBases[i].degree) + '</th>';
                    row += '<th scope="col">' + confirmedBases[i].degree + ' &#xb0;</th>';
                }
                head += '</tr>';
                row += '</tr>';
                $tblUserBases.empty().append(head).append(row);
            }
            function getHeadValue(index, isMatched) {
                var color = '';
                if (isMatched == true) {
                    color = 'color: green;'
                }
                else if (isMatched == false) {
                    color = 'color: red;'
                }

                if (index == 0) {
                    return '<span style="' + color + '" title="' + index +'" class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>';
                }
                return '<span style="transform: rotate(-' + index + 'deg); ' + color + '" title="' + index +'" class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>';
            }


            function RequestBit(bit) {
                
                var matched = isMatched(bit);
                $divCIABase.append(getHeadValue(bit, matched));
                if (matched) {
                    confirmedBases.push(userBase[currentIndex]);
                    currentIndex = currentIndex + 1;
                  
                    showConfirmedBases();
                    getQKBit();
                } else {
                    getQKBit();
                }
            }


            function getQKBit() {
                if (currentIndex > userBase.length - 1) {
                    $btnSendData.show();
                    return;
                }
                // ajax call here
                $.ajax({
                    contentType: "application/json",
                    url: 'https://quantumapi2.azurewebsites.net/api/Common',
                    type: 'get'
                }).done(function (response) { //
                    RequestBit(response);
                }).fail(function (xhr, status, error) {
                    //Ajax request failed.
                    var errorMessage = xhr.status + ': ' + xhr.statusText
                    //alert('Error - ' + errorMessage);
                    return 0;
                })
                return 0;
            }

            function isMatched(bit) {
                if (bit === userBase[currentIndex].degree) {
                    var _val = $txtUserKey.val();
                    _val = userBase[currentIndex].sin +','+ userBase[currentIndex].cos;
                    $txtUserKey.val(_val);
                    return true;
                }
                return false;
            }

            var setup = function () {
                init();
                bindEvents();
            };

            return {
                setup: setup
            };

        })();
        quantumModule.setup();
    }
}());

