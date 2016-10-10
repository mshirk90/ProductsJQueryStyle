<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductsJQueryStyle._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div ng-app="MyApp" ng-controller="myController">

        <table border="1" class="margintop" >
            <tr>
                <td>
                    <div class="dropdown-pull-left">
                        <select id="ddlPreferences">
                            <option ng-repeat="x in categories" value="{{x.Id}}">{{x.Name}}</option>
                        </select>
                    </div>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr ng-repeat="x in products">
                <td> {{x.Description}}</td>
                <td>
                    <img src="{{x.ImageName}}" />
                </td>
            </tr>
        </table>
    </div>



    <script>
        var app = angular.module("MyApp", []);
        app.controller("myController", function ($scope) {
            $scope.categories = [];
            $scope.products = [];
            $("#ddlPreferences").on('change', function (event) {
                var selectedItem = document.getElementById("ddlPreferences");
                var selectedValue = ddlPreferences.options[selectedItem.selectedIndex].value;
                GetProducts(selectedValue);
            });
            $.ajax({
                type: 'POST',
                url: 'ProductWS.asmx/CategoryGetAll',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    $scope.categories = response.d;
                    $scope.$apply();
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            function GetProducts(id) {
                $.ajax({
                    type: 'POST',
                    url: 'ProductWS.asmx/GetProductsByCategoryId',
                    dataType: 'json',
                    processData: false,
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        $scope.products = response.d;
                        $scope.$apply();
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            }
        });
    </script>
</asp:Content>
