var collListItem;

$( document ).ready(function() {
//window.onload = function () {


    var hostweburl = _spPageContextInfo.webAbsoluteUrl;
    var scriptbase = hostweburl + "/_catalogs/masterpage/Watersun/";
    $.getScript(scriptbase + "jquery-2.2.4.js",
        execOperation
    );


//}
});


function execOperation() {
    //alert("reached execOperation");	 

    if ($("[name='CalledBest']").length) {

        //$("[id='scriptWPQ1']").css(
        //                       //     "border", "4px solid black"
        //                            );
        $("[id='scriptWPQ1']").addClass('callForward1');

        $("td:nth-child(5)").css("color", "red");
        $("td:nth-child(7)").addClass('columns');

        //$( 'span.ms-noWrap' )
    }
}


