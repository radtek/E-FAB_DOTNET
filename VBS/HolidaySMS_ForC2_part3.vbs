Set IE = CreateObject("InternetExplorer.Application") 
URL="http://10.56.131.22/E-FAB_dotnet/Alarm/Holiday_inout_SMS_FOR_C2_R3.aspx" 
IE.Visible = True 
IE.navigate URL 
Set IE=nothing 
