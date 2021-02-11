<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFrontEnd._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function getStorages() {

            HttpClient client = new HttpClient();

            //$.ajax({
            //    type: "GET",
            //    url: "web.socem.plymouth.ac.uk/FYP/SSharpe/api/Storages",
            //    contentType: "json",
            //    dataType: "json",
            //    success: function (data) {
            //        $.each(data, function (key, value) {

            //            var jsonData = JSON.stringify(value);

            //            var objData = $.parseJSON(jsonData);

            //            var itemID = objData.itemID;
            //            var itemName = objData.itemName;
            //            var QTY = objData.QTY;
            //            var Cupboard = objData.Cupboard;
            //            var Room = objData.Room;

            //            $('<tr><td>' + itemID + '</td><td>' + itemName +
            //                '</td><td>' + QTY + '</td></tr>' + Cupboard + '</td></tr>' + Room ).appendTo('#storages');

            //        });
            //    },
            //    error: function (xhr) {
            //        alert(xhr.responseText);
            //    }
            //});
                        
                }






        //$.getJSON("web.socem.plymouth.ac.uk/FYP/SSharpe/api/Storages",
        //    function (data) {
        //        $('#Storages').empty(); // Clear the table body.

        //        // Loop through the list of products.
        //        $.each(data, function (key, val) {
        //            // Add a table row for the product.
        //            var row = '<td>' + val.ItemID + '</td><td>' + val.ItemName + '</td><td>' + val.QTY + '</td><td>' + val.Cupboard + '</td><td>' + val.Room + '</td><td>' + val.Weight'</td>';
        //            $('<tr/>', { html: row })  // Append the name.
        //                .appendTo($('#Storages'));
        //        });
        //    });
       // }

        $(document).ready(getProducts);
    </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Storages</h2>
    <table>
    <thead>
        <tr><th>ItemID</th><th>ItemName</th><th>Quantity</th><th>Cupboard</th><th>Room</th><th>Weight</th></tr>
    </thead>
    <tbody id="Storages">
    </tbody>
    </table>
</asp:Content>