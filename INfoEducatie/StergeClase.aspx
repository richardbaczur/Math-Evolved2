﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StergeClase.aspx.cs" Inherits="INfoEducatie.StergeClase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            background-color:transparent;
        }
    </style>
     <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript --> 
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> 
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="jquery-ui.css" rel="stylesheet" />
    <script src="jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br />
        <label>Selectectati clasa pe care doriti sa o stergeti!</label><br />
        <asp:DropDownList runat="server" CssClass="form-control" ID="del"></asp:DropDownList>
        <br /><br />
        <asp:Button runat="server" class="btn btn-danger" data-toggle="modal" OnClick="conf_Click" data-target="#confirm" Text="Stergeti" />
        
    </form>
</body>
</html>
