function deep1(no,deep)
{
  if(deep == 10)
    return;
  for(var i=0;i<5;i++)
  {
	switch(no)
	{
      case 2:
        eval("pmL"+no+""+deep+""+i+"=new TPopMenu('Menu "+i+"','0','','',' Menu "+deep+" "+i+"');"); 
	    break;
 	  default:
		eval("pmL"+no+""+deep+""+i+"=new TPopMenu('Menu "+i+"','','','',' Menu "+deep+" "+i+"');"); 
	}
       
  }
  deep1(no,++deep);
}
function deep2(par,no,deep)
{ 
  if(deep == 10)
    return;
  var j = Math.round(Math.random()*4);
  for(var i=0;i<5;i++)
  {
    eval(par+".Add(pmL"+no+""+deep+""+i+");");
    if(i== j)
    {  
      var d = deep+1;  
      deep2("pmL"+no+""+deep+""+i,no,d);
    }
  }
}
//xp style
function deep1(no,deep)
{
  if(deep == 10)
    return;
  for(var i=0;i<5;i++)
  {
    switch(no)
    {
      case 2:
        eval("pmL"+no+""+deep+""+i+"=new TPopMenu('Menu "+i+"','0','','',' Menu "+deep+" "+i+"');"); 
        break;
      default:
            eval("pmL"+no+""+deep+""+i+"=new TPopMenu('Menu "+i+"','','','',' Menu "+deep+" "+i+"');"); 
    }
       
  }
  deep1(no,++deep);
}        
        
var mm0 = new TMainMenu('mm0','horizontal');
//mm0.SetWidth(500);

var pmFileSharing = new TPopMenu('查詢功能   ','5','','','查詢功能   ');

var pmIcon00 = new TPopMenu('節能減廢專案','','f',"targetWindow('ConserveList_Add.aspx','mainiframe')",'節能減廢專案');//節能減廢專案-新增
//var pmFav00 = new TPopMenu('Favorites','../img/favorites.gif','','','favorites'); 
//var pmHist00 = new TPopMenu('History','../img/history.gif','','','History');
//var pmHome00 = new TPopMenu('Home','../img/home.gif','','','Home');
//var pmSep00 = new TPopMenu('-','','','','');
//var pmRef00 = new TPopMenu('Refresh','../img/refresh.gif','','','Refresh');
//var pmStop00 = new TPopMenu('Stop','../img/stop.gif','','','Stop');
var pmOpen00 = new TPopMenu('節能減廢專案','','f',"",'節能減廢專案');
var pmOpen001 = new TPopMenu('節能減廢專案','','f',"targetWindow('ConserveList_Query.aspx','mainiframe')",'節能減廢專案');
var pmOpen_project = new TPopMenu('節能減廢專案統計','','f',"targetWindow('ConserveList_Summary_1.aspx','mainiframe')",'節能減廢專案統計');

//var pmSame00 = new TPopMenu('in same window','','a','index.html','Open page in same window');
//var pmDiv00 = new TPopMenu('in new window','','f',"targetWindow('index.html','mainiframe')",'Open page in new window');
//var pmL00 = new TPopMenu('Long popup menu','','','','Popup menu demo');
var pmGoToMainPage = new TPopMenu('生產效率','','f',"targetWindow('pp_output_efficiency_add.aspx','mainiframe')",'生產效率');//生產效率-新增
var pmGoToMainPage_project_add = new TPopMenu('專案簽呈管控','','f',"targetWindow('Conserve_project_add.aspx','mainiframe')",'專案簽呈管控');//專案簽呈管控新增-新增
var pmGoToMainPage_project_query = new TPopMenu('專案簽呈管控','','f',"targetWindow('Conserve_project_query.aspx','mainiframe')",'專案簽呈管控');//專案簽呈管控新增-搜尋

var pmGoToMainPage_query = new TPopMenu('生產效率','','f',"targetWindow('pp_output_efficiency_query.aspx','mainiframe')",'生產效率');//生產效率-查詢
var pmGoToMainPage_fee = new TPopMenu('修繕維護費','','f',"targetWindow('http://cnpc0185:7777/pls/apex/f?p=201:1:1130278435474525','mainiframe')",'修繕維護費');
//var pmDiv20 = new TPopMenu('in new window','0','f',"newWindow('index.html')",'Open page in new window');
var pmGoToMainPage_productivity = new TPopMenu('投入產出資訊','','f',"targetWindow('input_output.aspx','mainiframe')",'投入產出資訊');    

var pmFileAdd = new TPopMenu('維護功能    ','5','','','維護功能    ');


var pmDocument = new TPopMenu('相關資訊','','','','相關資訊');

var pmDocument_List =  new TPopMenu('教學文件','','f',"targetWindow('images/PP_CONSERVE_TEACHING.doc','mainiframe')",'教學文件'); 


//var pmInnoWiki = new TPopMenu('InnoWiki群創百科','','','','InnoWiki群創百科');

//var pmInnoWiki_Main = new TPopMenu('InnoWiki登錄首頁','','f',"newWindow('InnoWiki.aspx','mainiframe')",'InnoWiki首頁');
//var pmInnoWiki_Intro = new TPopMenu('InnoWiki系統介紹','','f',"newWindow('InnoWiki_Intro.aspx','mainiframe')",'InnoWiki系統介紹');
//var pmInnoWiki_FAQ = new TPopMenu('InnoWiki發文說明','','f',"newWindow('InnoWiki_FAQ.aspx','mainiframe')",'InnoWiki發文說明');

//var pmAbout00 = new TPopMenu('About','','f',"alert('Implement by Bunny')",'About this program');
//var pmAbout00 = new TPopMenu('About','','f',"alert('Implement by Bunny')",'About this program');

//檔案共享平臺(pmFileSharing)
mm0.Add(pmFileSharing);
//pmFileSharing.Add(pmIcon00);
pmFileSharing.Add(pmOpen00);
//pmFileSharing.Add(pmOpen_project);
//pmFileSharing.Add(pmGoToMainPage);
pmFileSharing.Add(pmGoToMainPage_query);
pmFileSharing.Add(pmGoToMainPage_fee);
pmFileSharing.Add(pmGoToMainPage_productivity);
pmFileSharing.Add(pmGoToMainPage_project_query);//專案簽呈管控新增-搜尋


pmOpen00.Add(pmOpen001);
pmOpen00.Add(pmOpen_project);


//檔案新增功能
mm0.Add(pmFileAdd);
pmFileAdd.Add(pmIcon00);
pmFileAdd.Add(pmGoToMainPage);
pmFileAdd.Add(pmGoToMainPage_project_add);



//遠距教學文件(pmDocument)
mm0.Add(pmDocument);
pmDocument.Add(pmDocument_List);
//pmDocument_List.Add(pmDocument_NetMeetingConfig);
//pmDocument_List.Add(pmDocument_NetMeetingHowToUse);
//pmDocument_List.Add(pmDocument_TrustView);
//pmDocument_List.Add(pmDistance_Intro);
//pmDocument.Add(pmDocument_WebSite);
//pmDocument_WebSite.Add(pmTrustView);
//pmIcon00.Add(pmFav00);
//pmIcon00.Add(pmHist00);
//pmIcon00.Add(pmHome00);
//pmIcon00.Add(pmSep00);
//pmIcon00.Add(pmRef00);
//pmIcon00.Add(pmStop00);
//pmDemo00.Add(pmSep00);

//pmOpen00.Add(pmSame00);
//pmOpen00.Add(pmDiv00);
//pmDemo00.Add(pmL00);

//InnoWiki群創百科
//mm0.Add(pmInnoWiki);
//pmInnoWiki.Add(pmInnoWiki_Main);
//pmInnoWiki.Add(pmInnoWiki_Intro);
//pmInnoWiki.Add(pmInnoWiki_FAQ);
//pmAlert00.Add(pmAbout00);
//mm0.Add(pmGoToMainPage);

//pmHelp00.Add(pmAbout00);
//end of xp style      