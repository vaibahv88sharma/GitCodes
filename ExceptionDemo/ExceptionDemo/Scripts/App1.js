'use strict';

var BasicJSOM = window.BasicJSOM ||{};
//BasicJSOM.Exception = BasicJSOM.Exception || {};
BasicJSOM.Utilities = BasicJSOM.Utilities || {};
BasicJSOM.Batching = BasicJSOM.Batching || {};
BasicJSOM.Demo = BasicJSOM.Demo || {};


BasicJSOM.Utilities.LogResult =function(msg){
	$("logResultsDiv").append("<p class='highlighted'>"+msg +"</p>");
}

//CRUD
    //#region 1
    var context = SP.ClientContext.get_current();
    function fail() { alert("Request to data failed")};
    var hostWebUrl;
    var appWebUrl;

    hostWebUrl  = decodeURIComponent(manageQueryStringParameter('SPHostUrl'));
    appWebUrl   = decodeURIComponent(manageQueryStringParameter('SPAppWebUrl'));

    function manageQueryStringParameter(paramToRetrieve) {
        var params =
        document.URL.split("?")[1].split("&");
        var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }
    //#endregion 1
    //#region 2
    function listAllCategories() {
        var ctx = new SP.ClientContext(appWebUrl);
        var appCtxSite = new SP.AppContextSite(ctx, hostWebUrl);

        var web = appCtxSite.get_web();
        var list = web.get_lists().getByTitle("CategoryList");
        var query = new SP.CamlQuery();
        query.set_viewXml('<View><RowLimit></RowLimit>10</View>');
        var items = list.getItems(query);
        ctx.load(list);
        ctx.load(items);

        var table = $("#tblcategories");
        var innerHtml = "<tr><td>ID</td><td>Category Id</td><td>Category Name</td></tr>";

        ctx.executeQueryAsync(
            Function.createDelegate(this,function(){
                var itemInfo = '';
                var enumerator = items.getEnumerator();
                while(enumerator.moveNext()){
                    var currentListItem = enumerator.get_current();
                    innerHtml = "<tr>"
                                      + "<td>" + currentListItem.get_item('ID') + "</td>"
                                      + "<td>" + currentListItem.get_item('Title') + "</td>"
                                      + "<td>" + currentListItem.get_item('CategoryName') + "</td>" +
                                "</tr>";
                    table.html(innerHtml);
                }
            }),
            Function.createDelegate(this, fail)
            );
    }
    //#endregion 2
