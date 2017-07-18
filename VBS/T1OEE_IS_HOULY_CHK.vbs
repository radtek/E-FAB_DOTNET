With CreateObject("InternetExplorer.Application") 

.Visible = True 
.Navigate"http://10.56.131.22/E-FAB_dotnet/Alarm/OEE_IS_Hourly_Check_alarm.aspx" 
End With 

With CreateObject("InternetExplorer.Application") 

.Visible = True 
.Navigate"http://10.56.131.22/E-FAB_dotnet/Alarm/ALCS_Hourly_Check_alarm.aspx" 
End With 

With CreateObject("InternetExplorer.Application") 

.Visible = True 
.Navigate"http://10.56.131.22/E-FAB_dotnet/Alarm/t1newalarm_self_check.aspx" 
End With 


With CreateObject("InternetExplorer.Application") 

.Visible = True 
.Navigate"http://10.56.131.22/E-FAB_dotnet/OEE/OEE_0ATPT100_monitor.aspx" 
End With 
