<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AngularWebApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/angular.js"></script>
    <script type="text/javascript" src="Scripts/MyScripts/myModule.js"></script>
    <script type="text/javascript" src="Scripts/MyScripts/newFile.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div ng-app="myApp">
            <div class="container jumbotron">
                <%--<div ng-controller="ProductControllerOld">
                    {{name}}                   
                </div>--%>

                <div ng-controller="StudentController">
                    hi
                    <my-first-directive-Old1></my-first-directive-Old1>
                    <my-first-directive-Old1></my-first-directive-Old1>
                    <%--<my-first-directive-Old1></my-first-directive-Old1>--%>
                </div>

                <div ng-controller="ProductController">
                    <inventory-Product-Old></inventory-Product-Old>                   
                </div>
                <div ng-controller="ProductController">                    
                    <inventory-Product name="{{product1.name}}" price="{{product2.price}}"></inventory-Product>                   
                    <inventory-Product name="{{product1.name}}" price="{{product2.price}}"></inventory-Product>                   
                    <inventory-Product name="{{product1.name}}" price="{{product2.price}}"></inventory-Product>
                    Next Directive
                    <inventory-Product-New data="product1"></inventory-Product-New>
                    <inventory-Product-New data="product2"></inventory-Product-New>
                    <inventory-Product-New data="product3"></inventory-Product-New>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
