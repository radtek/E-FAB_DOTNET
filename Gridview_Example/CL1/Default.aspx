<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CL1_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    
    <script src="~/js/jquery-1.4.1.js"></script> 
<style type="text/css"> 
#example-placeholder { 
border: 1px solid #ccc; 
padding: 0 10px; 
} 
</style> 
<script language="javascript"> 
function example_ajax_request() { 
$('#example-placeholder').html('<p><img src="~/images/ajax-loader.gif" width="220" height="19" /></p>'); 
setTimeout('example_ajax_request_go()', 2000); 
} 
function example_ajax_request_go() { 
$('#example-placeholder').load("/examples/ajax-loaded.html"); 
$('#example-placeholder').html("<p>this is a sample</p>"); 
} 
</script> 

    
    
</head>

<body>
    <form id="form1" runat="server">
   <div id="example-placeholder"> 

<p><input type="button" value="Click Me!" onclick="example_ajax_request()" /></p> 

<p>Loading...</p> </div> 

 
    </form>
    
    </body> 

</html>
