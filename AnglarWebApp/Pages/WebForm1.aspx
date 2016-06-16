<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AnglarWebApp.Pages.WebForm1" %>

<!DOCTYPE html>

<%--<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="myapp">--%>
<html data-ng-app="myapp">
    <head runat="server">
        <title>Angluar Page</title>
        <script src="../Scripts/jquery-1.9.1.js"></script>
        <script src="../Scripts/bootstrap.min.js"></script>
        <script src="../Scripts/angular.min.js"></script>
        <script src="../Scripts/angular-route.js"></script>
        <script src="../Scripts/App.js"></script>
        <script src="../Scripts/Controllers.js"></script>
        <link href="../Content/bootstrap.min.css" rel="stylesheet" />
        <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
        <link href="http://bootswatch.com/slate/bootstrap.min.css" rel="stylesheet" />

    </head>
    <body>

        <%--<div data-ng-app="myapp">
            <div data-ng-controller="listController">--%>
                <div class="container-fluid">
                    <%--                <div class="well">--%>
                    <div class="jumbotron">
                        <div class="row">
                            <div class="col-xs-12">
                                <h3><span class="glyphicon glyphicon-music"></span>This is heading</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div data-ng-view></div>
<%--                                <div class="form-group">
                                    <input type="text" class="form-control" placeholder="Enter Here" data-ng-model="itemName" />
                                        <p>Hello {{itemName}}!</p>
                                </div>
                                <table class="table table-stripped table-hover">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Genre</th>
                                            <th>Rating</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr data-ng-repeat="item in data | filter:itemName">
                                            <td>{{item.name}}</td>
                                            <td>{{item.genre}}</td>
                                            <td>{{item.rating}}</td>
                                        </tr>
                                    </tbody>
                                </table>--%>
                            </div>
                        </div>
                    </div>
                </div>


    </body>
</html>
