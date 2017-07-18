 		   function CallBackFunction(radWindow, returnValue)
				{
				    var ReturnElementID=radWindow._offsetElementID;
                    var oArea =document.getElementById(ReturnElementID);                   
					if (returnValue) oArea.value = returnValue;
					else alert ("No text was returned");
				}
			function CallBackFunctionUC(radWindow, returnValue)
			{
			    var ReturnElementID=radWindow._offsetElementID;
			    var ClientID=radWindow._name;
                var oArea =document.getElementById(ClientID+'_' + ReturnElementID);

				if (returnValue) oArea.value = returnValue;
				else alert ("No text was returned");
			}			
			function GetRadWindow()
			    {
				    var oWindow = null;
				    if (window.radWindow) oWindow = window.radWindow;
				    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
				    return oWindow;
			    }						
			function OK_Clicked(iCount)
		    {
			    var oWindow = GetRadWindow();
				
			    //Get current content of text area   
			    var oNewText = document.getElementById("listBoxR") ;
			    var TextSum="";
			    var selectText="";
			    if (oNewText.length>0)
			    {
			        for(i=0; i<oNewText.length; i++)  
			        {
                       selectText = oNewText.options[i].text;
                       TextSum +=selectText +"," ;
                    }
			        oNewText=TextSum.substr(0,TextSum.length-1);
			        oWindow.close(oNewText);
			    }
			    else
			    {
			        oWindow.close("");
			    }								
		    }
			
			function Cancel_Clicked()
		    {
			    var oWindow = GetRadWindow();			
			    oWindow.close();
		    }