<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Innoview_Adduser.aspx.cs" Inherits="T0_T1_Innoview_Adduser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Innoview_Adduser_Aspx</title>
  
<script src="~/js/jquery-1.4.2.min.js"></script> 
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
//$('#example-placeholder').load("/examples/ajax-loaded.html"); 
$('#example-placeholder').html("<p>Page Loading Data...</p>"); 
} 
</script> 


</head>
<body onload="example_ajax_request()" >
    <form id="form1" runat="server">
    <div id="example-placeholder"> 
     
    
    </div>
    </form>
</body>
</html>
