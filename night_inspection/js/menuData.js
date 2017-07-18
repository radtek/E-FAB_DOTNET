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
//****已經 Mark by Oscar 2009/05/21 ******//
//var pmselect_area = new TPopMenu('類別','5','','','會議記錄E化平台');
//var area_ary = new TPopMenu('Array會議','','a',"../ary/main.aspx",'Array會議');
//var area_cell = new TPopMenu('Cell會議','','a',"../cell/main.aspx",'Cell會議');
//var area_cf = new TPopMenu('CF會議','','a',"../cf/main.aspx",'CF會議');
//var area_Yield = new TPopMenu('Yield會議','','a',"../Yield/main.aspx",'Yield會議');
//var area_Productivity = new TPopMenu('PP會議','','a',"../Productivity/main.aspx",'PP會議');
//var area_general = new TPopMenu('一般會議','','a',"../general/main.aspx",'一般會議');





var pmFileSharing = new TPopMenu('主管夜巡紀錄平台','5','','','主管夜巡紀錄平台');


var pmIcon00 = new TPopMenu('查詢夜巡記錄','','f',"targetWindow('night_inspection_query.aspx','mainiframe')",'查詢夜巡記錄');
//var pmFav00 = new TPopMenu('Favorites','../img/favorites.gif','','','favorites'); 
//var pmHist00 = new TPopMenu('History','../img/history.gif','','','History');
//var pmHome00 = new TPopMenu('Home','../img/home.gif','','','Home');
//var pmSep00 = new TPopMenu('-','','','','');
//var pmRef00 = new TPopMenu('Refresh','../img/refresh.gif','','','Refresh');
//var pmStop00 = new TPopMenu('Stop','../img/stop.gif','','','Stop');

var pmOpen00 = new TPopMenu('新增夜巡記錄','','f',"targetWindow('night_inspection_add.aspx','mainiframe')",'新增夜巡記錄');
//var pmSame00 = new TPopMenu('in same window','','a','index.html','Open page in same window');
//var pmDiv00 = new TPopMenu('in new window','','f',"targetWindow('index.html','mainiframe')",'Open page in new window');
//var pmL00 = new TPopMenu('Long popup menu','','','','Popup menu demo');



var E_Meeting_function_list = new TPopMenu('相關文件','','','','相關文件');

var E_Meeting_function_homepage = new TPopMenu('教學文件下載','','f',"newWindow('LH_Night_Inspect_management_Web_intro_v2.0.ppt','mainiframe')",'教學文件下載');


//var E_Meeting_function_Query = new TPopMenu('檔案搜尋','','f',"targetWindow('e_meeting_query.aspx','mainiframe')",'E-Meeting 上傳搜尋');
//var E_Meeting_function_upload= new TPopMenu('檔案上傳','','f',"targetWindow('e_meeting_upload.aspx','mainiframe')",'E-Meeting 檔案上傳');


//var pmDocument_List = new TPopMenu('教學文件','','f',"targetWindow('teaching_document.aspx','mainiframe')",'教學文件');


//var pmDocument = new TPopMenu('教學相關資訊','','',"targetWindow('mail_meeting.aspx','mainiframe')",'教學相關資訊');
//var pmDocument_List = new TPopMenu('教學文件','','f',"targetWindow('sop.doc','mainiframe')",'教學文件');

//var login_history = new TPopMenu('統計','','','','統計');
//var login_history_web = new TPopMenu('登入統計','','f',"targetWindow('statistic_login.aspx','mainiframe')",'登入統計');
//var Meeting_type_statistic = new TPopMenu('Meeting_Type統計','','f',"targetWindow('statistic_meeting_type.aspx','mainiframe')",'登入統計');




//var pmDocument_NetMeetingConfig = new TPopMenu('NetMeeting安裝設定'Add,'','f',"targetWindow('/Distance/Document_Link/NETMEETING_INST/netmeet_config.htm','mainiframe')",'NetMeeting設定');

//var pmTrustView = new TPopMenu('TrustView System','','f',"newWindow('http://inlcndrm01/innolux/')",'加密網站');
//var pmDistance_Intro = new TPopMenu('遠距教學平台介紹','','f',"targetWindow('/Distance/Document_Link/Distance_Intro/Distance_Introduction.htm','mainiframe')",'遠距教學平台介紹');


//var pmInnoWiki = new TPopMenu('InnoWiki群創百科','','','','InnoWiki群創百科');

//var pmInnoWiki_Main = new TPopMenu('InnoWiki首頁','','f',"newWindow('InnoWiki.aspx','mainiframe')",'InnoWiki首頁');
//var pmInnoWiki_Intro = new TPopMenu('InnoWiki系統介紹','','f',"newWindow('InnoWiki_Intro.aspx','mainiframe')",'InnoWiki系統介紹');

//var pmAbout00 = new TPopMenu('About','','f',"alert('Implement by Bunny')",'About this program');
//var pmAbout00 = new TPopMenu('About','','f',"alert('Implement by Bunny')",'About this program');


//新增登入類別****已經 整合 mark 掉  oscar 2009/05/21 ******//
//mm0.Add(pmselect_area);
//pmselect_area.Add(area_ary);
//pmselect_area.Add(area_cell);
//pmselect_area.Add(area_cf);
//pmselect_area.Add(area_Yield);
//pmselect_area.Add(area_Productivity);
//pmselect_area.Add(area_general);




//Meeting System Function (Meeting System)
mm0.Add(pmFileSharing);
pmFileSharing.Add(pmIcon00);
pmFileSharing.Add(pmOpen00);

//pmFileSharing.Add(pmDocument_List);




//E-Meeting Combine(E-Meeting Combine)
mm0.Add( E_Meeting_function_list);
E_Meeting_function_list.Add(E_Meeting_function_homepage);
//E_Meeting_function_list.Add(E_Meeting_function_Query);
//E_Meeting_function_list.Add(E_Meeting_function_upload);

//mm0.Add(login_history);
//login_history.Add(login_history_web);
//login_history.Add(Meeting_type_statistic);







//會議記錄教學文件(pmDocument)
//mm0.Add(pmDocument);
//pmDocument.Add(pmDocument_List);
//pmDocument_List.Add(pmDocument_NetMeetingConfig);


//pmDocument_List.Add(pmDistance_Intro);
//pmDocument.Add(pmDocument_WebSite);

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
//pmAlert00.Add(pmAbout00);
//mm0.Add(pmGoToMainPage);

//pmHelp00.Add(pmAbout00);
//end of xp style      