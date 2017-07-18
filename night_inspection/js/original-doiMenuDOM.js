/**
* Bismillaahirrohmaanirrohiim
* Dhtml MenuBar DOM
*
* @access   public
* @version  1.5
* @author   Donna Iwan Setiawan
* @email    pagi@donnaiwan.com
* @homepage http://www.donnaiwan.com
* @package  doiMenu
*
* History
* -------
* v.1.5
*   - tested on
*       - IE 6
*       - mozilla 1.4 on window
*       - mozilla 1.2.1 on linux
*       - konqueror 3.1-12 Red Hat
*         known problem:
*           - image icon
*         main menu shadow has been disabled
*       - opera 7.21 on linux
*         known problem:
*           - expand icon symbol
*   - minor bug fix
*   - automatically generate unique ID
*   - correct parameter naming convention
*   - add new parameters
*   - code optimizing
*   - ability of creating more than 1 menu in one page
*   - status on status bar
*   - and many more..
* v.1.4
*   - first public release
*
* Notes
*   If you have trouble building menu, please refer to demo files first.
*   If you find this code useful or if you have any comment please let me know
*   If you have time, please visit http://www.limabit.com. Just a click away :)
*
* This note must remain intact
*/
var _browser = new TBrowser();
var _arRegisterMenu = new Array();
var _arRegisterMenuIndex = -1;
var _arTriggerMenu = new Array();

var _arRegisterPopID = new Array(); //array menyimpan id popup menu yang muncul
var _arRegisterPopIndex = new Array(); //index terakhir dari id popup menu yang te-register di atas

/*
	menyimpan state item yang membuka pop up menu
*/
var _arRegisterTriggerPopID = new Array();
var _arRegisterTriggerPopIndex = new Array();

/*
	untuk menyimpan state click pada main menu
		false berarti harus diclick dulu baru muncul pop up
		true berarti hanya dgn mouse over, muncul pop up
*/
var _arMMClick = new Array();

function Initialize()
{  
  var byk = _arRegisterMenuIndex;
  
  for(var i=0;i<=byk;i++)
    _arRegisterMenu[i].Init();
  for(var i=0;i<=byk;i++)
    _arRegisterMenu[i].Init();    
}

//browser constructor
function TBrowser()
{
  this._name ='';
  this._version = '';
  this._os ='';
}


//start:function browser checking
// original browser detection source code go to http://www.xs4all.nl/~ppk/js/detect.html
var detect = navigator.userAgent.toLowerCase();
var total,thestring;

if (checkIt('konqueror'))
{
	_browser._name = "Konqueror";
	_browser._os = "Linux";
}
else if (checkIt('safari'))  _browser._name = "Safari";
else if (checkIt('omniweb')) _browser._name = "OmniWeb";
else if (checkIt('opera'))   _browser._name = "Opera";
else if (checkIt('webtv'))   _browser._name = "WebTV";
else if (checkIt('icab'))    _browser._name = "iCab";
else if (checkIt('msie'))    _browser._name = "IE";
else if (!checkIt('compatible'))
{
	_browser._name = "Netscape";
	_browser._version = detect.charAt(8);
}
else _browser._name = "none";

if (_browser._version == '') _browser._version = detect.charAt(place + thestring.length);

if (_browser._os == '')
{
	if (checkIt('linux'))    _browser._os = "Linux";
	else if (checkIt('x11')) _browser._os = "Unix";
	else if (checkIt('mac')) _browser._os = "Mac";
	else if (checkIt('win')) _browser._os = "Windows";
	else _browser._os = "none";
}

function checkIt(string)
{
	place = detect.indexOf(string) + 1;
	thestring = string;
	return place;
}
//end:function browser checking


//Main Menu Constructor
function TMainMenu(name,direction)
{
  _arRegisterMenuIndex++;
  _arRegisterMenu[_arRegisterMenuIndex] = this;
  
  _arRegisterPopID[_arRegisterMenuIndex] = new Array();
  _arRegisterPopIndex[_arRegisterMenuIndex] = -1;
  
  _arRegisterTriggerPopID[_arRegisterMenuIndex] = new Array();
  _arRegisterTriggerPopIndex[_arRegisterMenuIndex] = -1;
  
  _arMMClick[_arRegisterMenuIndex] = false;
  
  this._uniqueID = 0; //don't ever ever overwrite this!!!!
  //this._lastPop;
  this._name = name; //harus sama dengan nama variable
	this._id = '_'+ name+'ID';
	this._index = _arRegisterMenuIndex;  //agar nama class unik
	
	this._correction = new TCorrection();
	
	this._parent = null; //di set pada saat SetParent() method
	this._width ='auto';
	this._direction = direction; //direction = 'horizontal' atau 'vertical'
	this._position= 'absolute';	
	this._top = 0;
	this._left = 0;
	this._cellSpacing =0;
	this._itemHeight = 'auto'; //dengan satuan bisa 'auto' atau ''
	this._itemWidth = 'auto';	//dengan satuan bisa 'auto' atau ''
	//this._textAlign = 'center';
	this._background = new TBackground();
	this._background._color = 'buttonface';
	
	this._pop = new TPopParameter();
	this._pop._mmName = this._name;
	this._pop._index = this._index;
	
	this._shadow = new TShadow();
	
	this._font = new TFont();
	this._font._family = 'tahoma,verdana,sans-serif,arial';
	this._font._size = '8pt';
	
	this._itemIndex = -1;
	this._items = new Array();
	
	//this._itemTextColor = 'black';
	this._itemText = new TText();
	this._itemText._color = 'black';
	this._itemText._align = 'center';
	//this._itemBackColor = 'transparent';
	this._itemBack = new TBackground();
	//this._itemTextColorHL = 'white';
	this._itemTextHL = new TText();
	this._itemTextHL._color = 'white';
	this._itemTextHL._align = 'center';
	//this._itemBackColorHL = '#B6BDD2';
	this._itemBackHL = new TBackground();
	this._itemBackHL._color ='#B6BDD2';
	
	this._border = new TBorder();
	
	this._itemBorder = new TBorder(); //bug: harus diisi jangan none atau 0px	
	this._itemBorderHL = new TBorder();
	this._itemBorderHL._top    ='1px navy solid';
	this._itemBorderHL._right  ='1px navy solid';
	this._itemBorderHL._bottom ='1px navy solid';
	this._itemBorderHL._left   ='1px navy solid';

	//this._itemTextColorClick      = 'white';
	this._itemTextClick           = new TText();
	this._itemTextClick._color    = 'white';
	this._itemTextClick._align    = 'center';
	//this._itemBackColorClick      = '#B6BDD2';
	this._itemBackClick = new TBackground();
	this._itemBackClick._color = '#B6BDD2';
	
	this._itemBorderClick         = new TBorder();
	this._itemBorderClick._top    ='1px navy solid';
	this._itemBorderClick._right  ='1px navy solid';
	this._itemBorderClick._bottom ='1px navy solid';
	this._itemBorderClick._left   ='1px navy solid';
	
	//methods
	this.Add = AddItem;
	this.Build = BuildMenu;
	this.BuildStyle = BuildStyle;	
	this.Draw = DrawMenu;
	this.Init = InitMenu;
	this.SetParent = SetParent;	
	
	this.SetWidth = SetWidth;
	this.SetBorder = SetBorder;
	this.SetBorderTop=SetBorderTop;
	this.SetBorderRight=SetBorderRight;
	this.SetBorderBottom=SetBorderBottom;
	this.SetBorderLeft=SetBorderLeft;
	this.SetItemDimension = SetItemDimension;
	this.SetItemBorder=SetItemBorder;
	this.SetItemBorderTop=SetItemBorderTop;
	this.SetItemBorderRight=SetItemBorderRight;
	this.SetItemBorderBottom=SetItemBorderBottom;
	this.SetItemBorderLeft=SetItemBorderLeft;
	this.SetItemBorderHL=SetItemBorderHL;
	this.SetItemBorderTopHL=SetItemBorderTopHL;
	this.SetItemBorderRightHL=SetItemBorderRightHL;
	this.SetItemBorderBottomHL=SetItemBorderBottomHL;
	this.SetItemBorderLeftHL=SetItemBorderLeftHL;
	this.SetItemBorderClick=SetItemBorderClick;
	this.SetItemBorderTopClick=SetItemBorderTopClick;
	this.SetItemBorderRightClick=SetItemBorderRightClick;
	this.SetItemBorderBottomClick=SetItemBorderBottomClick;
	this.SetItemBorderLeftClick=SetItemBorderLeftClick;
	this.SetShadow=SetShadow;
	this.SetFont=SetFont;
	this.SetBackground=SetBackground;
	this.SetPosition=SetPosition;
	this.SetCorrection=SetCorrection;
	this.SetCellSpacing=SetCellSpacing;
	this.SetItemText=SetItemText;
	this.SetItemTextHL=SetItemTextHL;
	this.SetItemTextClick=SetItemTextClick;
	this.SetItemBackground=SetItemBackground;
	this.SetItemBackgroundHL=SetItemBackgroundHL;
	this.SetItemBackgroundClick=SetItemBackgroundClick;
}
//Pop Menu Constructor
function TPopMenu(label,icon,clickType,clickParam,status)
{
	//status  belum kepake
	//rencana buat status di statusbar
	this._id = '';
	this._parent = null; //di set pada saat SetParent() method. mengacu pada TMainMenu object
	this._parentPop = null;
	this._label = label;
	this._top = 0;
	this._left = 0;
	this._status = status;
	this._tmpIcon = icon;
	this._icon = "";
	this._itemIndex = -1;
	this._items = new Array();
	
	switch(clickType)
	{
		case 'function':
			this._eClick = clickParam;//' onclick="'+clickParam+'"';
			break;
		case 'f':
			this._eClick = clickParam;//' onclick="'+clickParam+'"';
			break;
	  case 'address':
	    this._eClick = "_openURL('"+clickParam+"')";
	    break;
	  case 'a':
	    this._eClick = "_openURL('"+clickParam+"')";
	    break;
		default:
			this._eClick = '';
	}
	this.Add = AddItem;
	this.Draw = DrawPopMenu;
	this.Init = InitPopMenu;
	this.SetParent = SetParent;
}
//new: Parameter buat Pop Menu
function TPopParameter()
{
  this._index = -1; //dioverwrite dgn TMainMenu._index
  this._mmName = ''; //dioverwrite dgn TMainMenu._name
  this._padding = '1px 1px 1px 1px';
	this._separator = new TSeparator();
	this._expandIcon = new TExpandIcon(); 

  this._correction = new TCorrection();
	this._font = new TFont();
	this._font._family = 'tahoma,verdana,sans-serif,arial';
	this._font._size = '8pt';	

  this._itemWidth =200;  //in pixel  
  this._itemHeight = 'auto';
	this._itemBorder = new TBorder();
	this._itemBorder._top    = '0px none solid'; 
	this._itemBorder._right  = '0px none solid';
	this._itemBorder._bottom = '0px none solid';
	this._itemBorder._left   = '0px none solid';
	
	this._itemPadding = '1px 1px 1px 1px'	;
	//this._itemTextColor = 'black';
	this._itemText = new TText();

	//this._itemBackColor = 'transparent';	
  this._itemBack = new TBackground();
  
	this._itemBorderHL = new TBorder();
	this._itemBorderHL._top    = '1px navy solid'; 
	this._itemBorderHL._right  = '1px navy solid';
	this._itemBorderHL._bottom = '1px navy solid';
	this._itemBorderHL._left   = '1px navy solid';
	
	this._itemPaddingHL = '0px 0px 0px 0px';	
	//this._itemTextColorHL = 'white';
	this._itemTextHL = new TText();
	this._itemTextHL._color = 'white';
	//this._itemBackColorHL = '#B6BDD2';
	this._itemBackHL = new TBackground();
	this._itemBackHL._color = '#B6BDD2';
	
	this._background = new TBackground();
	this._background._color  = 'whitesmoke';	
	//this._background._image  = 'xp.gif'; //with no url()
	//this._background._repeat = 'repeat-y';
	
	this._border = new TBorder();
	this._border._top    = '1px black solid';
	this._border._right  = '1px black solid';
	this._border._bottom = '1px black solid';
	this._border._left   = '1px black solid';
	
	this._shadow = new TShadow();
	
	this._timeOut = 2000;  //in milliseconds
	
	this.SetBorder = SetBorder;
	this.SetPadding = SetPadding;
	this.SetPaddings = SetPaddings;
	this.SetBorderTop=SetBorderTop;
	this.SetBorderRight=SetBorderRight;
	this.SetBorderBottom=SetBorderBottom;
	this.SetBorderLeft=SetBorderLeft;
	this.SetItemDimension = SetItemDimension;
	this.SetItemBorder=SetItemBorder;
	this.SetItemPadding=SetItemPadding;
	this.SetItemPaddingHL=SetItemPaddingHL;
	this.SetItemPaddings=SetItemPaddings;
	this.SetItemPaddingsHL=SetItemPaddingsHL;
	this.SetItemBorderTop=SetItemBorderTop;
	this.SetItemBorderRight=SetItemBorderRight;
	this.SetItemBorderBottom=SetItemBorderBottom;
	this.SetItemBorderLeft=SetItemBorderLeft;
	this.SetItemBorderHL=SetItemBorderHL;
	this.SetItemBorderTopHL=SetItemBorderTopHL;
	this.SetItemBorderRightHL=SetItemBorderRightHL;
	this.SetItemBorderBottomHL=SetItemBorderBottomHL;
	this.SetItemBorderLeftHL=SetItemBorderLeftHL;
	this.SetShadow=SetShadow;
	this.SetFont=SetFont;
	this.SetBackground=SetBackground;
	this.SetCorrection=SetCorrection;
	this.SetExpandIcon=SetExpandIcon;
	this.SetSeparator=SetSeparator;
	this.SetDelay=SetDelay;
	this.SetItemText=SetItemText;
	this.SetItemTextHL=SetItemTextHL;
	this.SetItemBackground=SetItemBackground;
	this.SetItemBackgroundHL=SetItemBackgroundHL;
}
//Koreksi jarak antara main menu dgn pop menu atau antar(pop menu)
function TCorrection()
{
  this._top = 0;
  this._left = 0;
}
function TText()
{
  this._color ='black';
  this._align = 'left';
  this._decoration = 'none';
  this._whiteSpace = 'normal';
  this._weight = 'normal';
}
function TShadow()
{
  this._create = false;
  this._color = 'black';
  this._distance = 3;  //in px
}
//Separator constructor
function TSeparator()
{
	this._align = 'center'  //available value: 'left','center','right'
	this._width = 200; //in pixel
	this._margin = "0px 0px 0px 0px";	//top right bottom left margin --> dioverwrite
	this._border = new TBorder();  //_left and _right --> tidak kepake
	this._border._top = '1px black solid';
	this._border._bottom = '1px white solid';
}
//Font constructor
function TFont()
{
	this._family = 'arial,times,sans-serif';
	this._size = '8pt';
}
//Background constructor
function TBackground()
{
	this._image = 'none';  //path and name of image
	this._repeat = 'no-repeat';
	this._color = 'transparent';
	this._position = 'top left';
}
//Expand Icon constructor
function TExpandIcon()
{
  this._create = true;
	this._symbol = '&#9658;';
	this._font = new TFont();
	this._font._size = '6pt';
}
//Border Constructor
function TBorder()
{
	this._top='1px gray solid';
	this._right ='1px gray solid';
	this._bottom ='1px gray solid';
	this._left='1px gray solid';	
}
//public
function BuildMenu()
{
	var result ="";
	var level = -1;
	
  this.SetParent(this);

	result += this.BuildStyle();
	result += this.Draw(level++);

	level++;
	for(var i=0;i<=this._itemIndex;i++)
	{
		result +=this._items[i].Draw(level);
		result += BuildPopUpMenu(this._items[i],level);
	}	
	document.write(result);
}

//public
function InitMenu()
{
  if(document.all)
    var el_menu = document.all(this._id);
  else if(document.getElementById)
	  var el_menu = document.getElementById(this._id);

  this._position = this._position.toLowerCase();
	if(this._position == 'absolute')
	{
		el_menu.style.top = this._top;
		el_menu.style.left = this._left;
	}else
	{
		this._top = findPosY(el_menu);
		this._left = findPosX(el_menu);//el_menu.offsetLeft
	}	
  el_menu.style.zIndex = 1;
	
	//main menu shadow	
	if(this._shadow._create)
	{
		if(document.all)
		  var sh_el = document.all('sh_'+this._id);
    else if(document.getElementById)
      var sh_el = document.getElementById('sh_'+this._id);
    if(_browser._name == 'IE')
    {
		  sh_el.style.top = this._top + 'px';
		  sh_el.style.left= this._left + 'px';

		  sh_el.style.width = el_menu.offsetWidth +10+'px';
		  sh_el.style.height = el_menu.offsetHeight+10+'px';
		  sh_el.childNodes[0].style.width =el_menu.offsetWidth +'px';
		  sh_el.childNodes[0].style.height = el_menu.offsetHeight + 'px';
		  sh_el.childNodes[0].style.backgroundColor = 'black';
		  sh_el.style.visibility = 'visible';
		}
	  else
	  {
	    if(_browser._name =='Konqueror' && this._position == 'relative')
	      sh_el.style.visibility = 'hidden';
	    else
	    {
		    sh_el.style.top = this._top + this._shadow._distance +'px';
		    sh_el.style.left= this._left + this._shadow._distance +'px';

		    sh_el.style.width = el_menu.offsetWidth + 'px';
		    sh_el.style.height = el_menu.offsetHeight + 'px';
		    sh_el.style.backgroundColor = this._shadow._color;
		    sh_el.style.visibility = 'visible';
		  }
	  }
	  sh_el.style.zIndex=0;
	}
	for(var i=0;i<=this._itemIndex;i++)
	{
	  if(document.all)
	    var el_menuitem = document.all('pr_'+this._items[i]._id);
	  else if(document.getElementById)
		  var el_menuitem = document.getElementById('pr_'+this._items[i]._id);
/*		
		var tyH = typeof(this._itemHeight);
		var tyW = typeof(this._itemWidth);
		if(tyH == 'string')
		{
		  if(this._itemHeight.toLowerCase() != 'auto' || this._itemHeight != '')
			  el_menuitem.style.height = this._itemHeight;
		}
		else
		  el_menuitem.style.height = this._itemHeight + 'px';
		if(tyW == 'string')
		{  
		  if(this._itemWidth.toLowerCase() != 'auto' || this._itemWidth != '')
			  el_menuitem.style.width = this._itemWidth;
    }
    else
      el_menuitem.style.width = this._itemWidth + 'px';
*/
		if(this._items[i]._itemIndex > -1)
		{				
		  if(document.all)
			  var el_pop = document.all(this._items[i]._id);
			else if(document.getElementById)
			  var el_pop = document.getElementById(this._items[i]._id);
			  
			if(this._direction == 'horizontal')
			{
				var leftPos=0;
				leftPos = el_menuitem.offsetLeft + this._left;
				if((leftPos - this._pop._itemWidth) < 0)
				{
				  this._items[i]._left = leftPos + this._correction._left - 1;
				}
			  else
			  {
			    if((leftPos + this._pop._itemWidth) > document.body.clientWidth)
			    {
			      if((leftPos + el_menuitem.offsetWidth) > document.body.clientWidth)
			      { 
			        this._items[i]._left = leftPos - this._pop._itemWidth - this._correction._left;
			      }
			      else
			      {
			        this._items[i]._left = leftPos  + el_menuitem.offsetWidth - this._pop._itemWidth - this._correction._left;
			      }
			    }
			    else
			      this._items[i]._left = leftPos + this._correction._left - 1;//el_menuitem.offsetLeft + this._left + this._correction._left - 1;
				}			  
        if(this._top - el_pop.offsetHeight <0)
				  this._items[i]._top = el_menuitem.offsetTop + el_menuitem.offsetHeight+this._top + 1 + this._correction._top;
				else
				{
				  if(this._top + el_menuitem.offsetHeight+ el_pop.offsetHeight < document.body.clientHeight)
				    this._items[i]._top = el_menuitem.offsetTop + el_menuitem.offsetHeight+this._top + 1 + this._correction._top;
				  else
				    this._items[i]._top = this._top - el_pop.offsetHeight - this._correction._top;
				}
			}
			else  //vertical
			{
			  var leftPos = 0
			  leftPos = this._left + el_menu.offsetWidth + this._correction._left;
			  /*
			  if(leftPos >= this._pop._itemWidth)
			  {
			    if(leftPos + this._pop._itemWidth <= document.body.clientWidth)
			    {
			      leftPos = this._left - this._pop._itemWidth - this._correction._left;
			    }
			  }
			  */
				this._items[i]._left = leftPos;//el_menuitem.offsetLeft + this._left +el_menu.offsetWidth + this._correction._left;				
				this._items[i]._top = el_menuitem.offsetTop +this._top + this._correction._top;
			}
			
			el_pop.style.top = this._items[i]._top + 'px';
			el_pop.style.left = this._items[i]._left  + 'px';
			el_pop.style.zIndex = 3;
			
			//shadow
			if(this._items[i]._parent._pop._shadow._create)
			{
			  if(document.all)
				  var sh_el_pop = document.all('sh_'+this._items[i]._id);
				else if(document.getElementById)
				  var sh_el_pop = document.getElementById('sh_'+this._items[i]._id);
				 
				if(_browser._name == 'IE')
				{  
				  sh_el_pop.style.top= this._items[i]._top + 'px';
				  sh_el_pop.style.left= this._items[i]._left + 'px';
				  sh_el_pop.style.width= el_pop.offsetWidth + 10 + 'px';
				  sh_el_pop.style.height= el_pop.offsetHeight + 10 + 'px';
				  sh_el_pop.childNodes[0].style.width = el_pop.offsetWidth + 'px';
				  sh_el_pop.childNodes[0].style.height = el_pop.offsetHeight + 'px';
				  //sh_el_pop.childNodes[0].style.backgroundColor = this._items[i]._parent._pop._shadow._color
				  sh_el_pop.childNodes[0].style.backgroundColor = 'black';
				}
			  else
			  {  
				  sh_el_pop.style.top= this._items[i]._top + this._items[i]._parent._pop._shadow._distance + 'px';
				  sh_el_pop.style.left= this._items[i]._left + this._items[i]._parent._pop._shadow._distance + 'px';
				  sh_el_pop.style.width= el_pop.offsetWidth +'px';
				  sh_el_pop.style.height= el_pop.offsetHeight +'px';
				  sh_el_pop.style.backgroundColor = this._items[i]._parent._pop._shadow._color;
				}
				sh_el_pop.style.zIndex = 2;
			}	
			this._items[i].Init(3);
		}
	}	
	el_menu.style.visibility="visible";
}
//private
function SetParent(parent)
{
  for(var i=0;i<=this._itemIndex;i++)
  {
    this._items[i]._parent = parent;
    this._items[i]._parentPop = this;
    //generate uniqueID
    this._items[i]._id ='_'+ parent._name + '-'+ parent._uniqueID+"ID";
    var iIcon = parseInt(this._items[i]._tmpIcon);
    if((iIcon > 0))
    {
      this._items[i]._icon ='<td style="padding-left:'+iIcon+'px;">';
      //<td class="TIcon'+this._items[i]._parent._index+'" style="width:'+iIcon+'px"></td>';
    }
    else
    {
      switch(this._items[i]._tmpIcon)
  	  {
  	    case "":
          this._items[i]._icon = '<td style="padding-left:24px;">';
          //'<td class="TIcon'+this._items[i]._parent._index+'">&nbsp;</td>';
          break;
        case '0':
          this._items[i]._icon = '<td>';
          break;
        default:
          this._items[i]._icon = '<td class="TIcon'+this._items[i]._parent._index+'"><img class="TIcon" src="'+this._items[i]._tmpIcon+'" width="16px" /></td><td>';
      }
    }
    parent._uniqueID++;
    this._items[i].SetParent(parent);
  }
}
//private
function DrawMenu(level)
{
	var result = "";
	//main menu shadow
	if(this._shadow._create)
	{	
	  if(_browser._name == 'IE') 
		  result +='<div style="position:absolute;visibility:hidden;filter: blur( direction=135, strength='+this._shadow._distance+', add=1);" id="sh_'+this._id+'" align="left"><div></div></div>';
		else
		  result +='<div style="position:absolute;visibility:hidden;" id="sh_'+this._id+'"></div>';
	}
		
	result +='<table class="TMenu'+this._index+'" id="'+this._id+'"';
	result +=' cellspacing="'+this._cellSpacing+'">';
	
	if(this._direction == 'horizontal')
		result +='<tr>';
	if(this._itemIndex > -1)
	{
		for(var i=0;i<=this._itemIndex;i++)
		{
			var result1 = '';
			result1 += '<td nowrap class="TMenuItem'+this._index+'" id="pr_'+this._items[i]._id+'"';
			if(this._items[i]._itemIndex > -1)
			{
				result1 += ' onmouseover="onMainMOver(event,this,\''+this._items[i]._id+'\','+level+','+this._name+',\''+this._items[i]._status+'\')"';
				result1 += 'onclick="onMainClick(event,this,\''+this._items[i]._id+'\','+this._name+')"';
				result1 += ' onmouseout="onMainMOut(event,this,\''+this._items[i]._id+'\','+this._name+')"';
			}
			else
			{
				result1 += ' onmouseover="onMainMOver(event,this,\'\','+level+','+this._name+',\''+this._items[i]._status+'\')"';
				result1 += ' onmouseout="onMainMOut(event,this,\'\','+this._name+')"';
				result1 += ' onclick="'+this._items[i]._eClick+'"';
			}
			result1 += '>'+this._items[i]._label+'</td>';
			
			if(this._direction == 'horizontal')
				result += result1;
			else
				result += '<tr>'+result1+'</tr>';
		}
	}
	else
	{
		var result1 = '';
		result1 +='<td>&nbsp;</td>';
		if(this._direction == 'horizontal')
			result += result1;
		else
			result +='<tr>'+result1+'</tr>';
	}
	if(this._direction == 'horizontal')
		result += '</tr>';
	result +='</table>';
	return result;
}
//private
function DrawPopMenu(level)
{
	var result ="";
	if(this._itemIndex > -1)
	{
		//shadow
		if(this._parent._pop._shadow._create)
		{	
		  if(_browser._name == 'IE')
			  result += '<div style="position:absolute;visibility:hidden;filter: blur( direction=135, strength='+this._parent._pop._shadow._distance+', add=1);" id="sh_'+this._id+'" align="left"><div></div></div>';
			else
			  result += '<div style="position:absolute;visibility:hidden;" id="sh_'+this._id+'" align="left"></div>';
		}		
		result += '<div class="TPopUp'+this._parent._index+'" id="'+this._id+'">';
		for(var i=0;i<=this._itemIndex;i++)
		{
			if(this._items[i]._label != '-')
			{
				//jika child punya child lagi
				if(this._items[i]._itemIndex > -1)
				{
				  result += '<div class="TPopUpItem'+this._items[i]._parent._index+'" id="di_'+this._items[i]._id+'">';
					result += '<table class="TPopUpItem'+this._items[i]._parent._index+'" cellspacing="0" cellpadding="0"';
					result += ' onmouseover="onPopItemMOver(event,this,\''+this._items[i]._id+'\','+level+','+this._items[i]._parent._name+',\''+this._items[i]._status+'\')"';
					result += ' onmouseout="onPopItemMOut(event,this,\''+this._items[i]._id+'\','+this._items[i]._parent._name+')"';
					result +=' id="pr_'+this._items[i]._id+'"><tr>'+this._items[i]._icon+this._items[i]._label+'</td>';
					if(this._items[i]._parent._pop._expandIcon._create)
					  result +='<td class="TExpand'+this._items[i]._parent._index+'">'+ this._items[i]._parent._pop._expandIcon._symbol+'</td>';
				}
				else
				{
				  result += '<div class="TPopUpItem'+this._items[i]._parent._index+'">';
					result += '<table class="TPopUpItem'+this._items[i]._parent._index+'" cellspacing="0" cellpadding="0"';
					result += ' onmouseover="onPopItemMOver(event,this,\'\','+level+','+this._items[i]._parent._name+',\''+this._items[i]._status+'\')"';
					result += ' onmouseout="onPopItemMOut(event,this,\'\','+this._items[i]._parent._name+')"';
					result += ' onclick="hideAll('+this._items[i]._parent._name+');'+this._items[i]._eClick+'"';
					result +='><tr>'+this._items[i]._icon+this._items[i]._label+'</td>';
				}
				result += '</tr></table>';
				result += '</div>';
			}
			else
			{			
				//ie butuh margin-right:-2px
				if(_browser._name == 'IE')
				  result +='<div style="margin-right:-2px;padding:4px 0px 4px 0px;background-color:'+this._parent._pop._itemBack._color+';"><div class="TSeparator'+this._parent._index+'"></div></div>';
				else
				  result +='<div style="margin-right:0px;padding:4px 0px 4px 0px;background-color:'+this._parent._pop._itemBack._color+';"><div class="TSeparator'+this._parent._index+'"></div></div>';   
			}
		}
		result +='</div>';
	}
	return result;
}
//global
function AddItem(popMenu)
{ 
  this._itemIndex++;
  this._items[this._itemIndex] = popMenu;
}
//private
function InitPopMenu(zIndex)
{
	if(this._itemIndex > -1)
	{
		for(var i=0;i<=this._itemIndex;i++)
		{
			if(this._items[i]._itemIndex > -1)
			{
			  if(document.all)
			  {
				  var pr_el = document.all('pr_'+this._items[i]._id);
				  var el = document.all(this._items[i]._id);
				}
			  else if(document.getElementById)
			  {
				  var pr_el = document.getElementById('pr_'+this._items[i]._id);
				  var el = document.getElementById(this._items[i]._id);
			  } 
			  var topPos =0;
			  var leftPos =0;
			  
			  topPos = this._top + pr_el.parentNode.offsetTop; //pr_el.offsetTop
			  leftPos = this._left + this._parent._pop._itemWidth;
			  
			  if((leftPos - (this._parent._pop._itemWidth * 2)) < 0)
			    this._items[i]._left = leftPos  - 3 + this._items[i]._parent._pop._correction._left;
			  else if((leftPos+this._parent._pop._itemWidth) > document.body.clientWidth)
			    this._items[i]._left = this._left - this._parent._pop._itemWidth - this._items[i]._parent._pop._correction._left + 3;
			  else
			    this._items[i]._left = leftPos + this._items[i]._parent._pop._correction._left  - 3;
				
				if(topPos - el.offsetHeight < 0)
				  this._items[i]._top = topPos + this._items[i]._parent._pop._correction._top;
				else
				{
				  if(topPos + el.offsetHeight < document.body.clientHeight)
				    this._items[i]._top = topPos + this._items[i]._parent._pop._correction._top;
				  else
				    this._items[i]._top = topPos - el.offsetHeight + pr_el.offsetHeight + this._items[i]._parent._pop._correction._top + 4;
				}
				el.style.top = this._items[i]._top +'px';
				el.style.left = this._items[i]._left + 'px';
				zIndex++;
				el.style.zIndex = zIndex + 1;
				
				//shadow
				if(this._items[i]._parent._pop._shadow._create)
				{
				  if(document.all)
					  var sh_el = document.all('sh_'+this._items[i]._id);
					else if(document.getElementById)
					  var sh_el = document.getElementById('sh_'+this._items[i]._id);
					
					if(_browser._name == 'IE')
					{
					  sh_el.style.top = this._items[i]._top + 'px';
					  sh_el.style.left = this._items[i]._left + 'px';
					  sh_el.style.width = el.offsetWidth  +10+ 'px';
					  sh_el.style.height = el.offsetHeight  +10+ 'px';
					  sh_el.childNodes[0].style.width = el.offsetWidth + 'px';
					  sh_el.childNodes[0].style.height = el.offsetHeight+ 'px';
					  //sh_el.childNodes[0].style.backgroundColor = this._items[i]._parent._pop._shadow._color
					  sh_el.childNodes[0].style.backgroundColor = 'black';
					}
				  else
				  {
					  sh_el.style.top= this._items[i]._top + this._items[i]._parent._pop._shadow._distance + 'px';
					  sh_el.style.left= this._items[i]._left + this._items[i]._parent._pop._shadow._distance + 'px';
					  sh_el.style.width= el.offsetWidth ;
					  sh_el.style.height= el.offsetHeight;
					  sh_el.style.backgroundColor = this._items[i]._parent._pop._shadow._color;
					}
					sh_el.style.zIndex = zIndex;
				}	
				this._items[i].Init(zIndex+1);
			}
		}
	}
}
//private
function BuildPopUpMenu(popMenu,level)
{
	var result = "";
	level++;
	for(var i=0;i<=popMenu._itemIndex;i++)
	{
		result += popMenu._items[i].Draw(level);
		result += BuildPopUpMenu(popMenu._items[i],level);
	}
	return result;
}
//private
function BuildStyle()
{
	var result = '';
	var tyH;
	var tyW;
	result +='<style type="text/css">';
	
	result +='table.TMenu'+this._index+'{';
	result +='cursor:default';
	result +=';visibility:hidden';
	result +=';position:'+this._position;
	tyW = typeof(this._width);
	if(tyW == 'string')
	{
	  this._width = this._width.toLowerCase();
	  if(this._width != 'auto' || this._width != '')
	    result +=';width:'+this._width+'px';	
	}
  else
    result +=';width:'+this._width+'px';  
	result +=';font-family:'+ this._font._family;
	result +=';font-size:'    + this._font._size;
	result +=';border-top:'   + this._border._top;
	result +=';border-right:' + this._border._right;
	result +=';border-bottom:'+ this._border._bottom;
	result +=';border-left:'  + this._border._left;
	result +=';background-color:' + this._background._color;
	result +=';background-image:'+ this._background._image;
	result +=';background-position:' + this._background._position;
	result +=';background-repeat:' + this._background._repeat;
	result +=';}';
	result +='td.TMenuItem'+this._index+'{';
	result +='padding: 0px 0px 0px 0px';
	tyH = typeof(this._itemHeight);
	tyW = typeof(this._itemWidth);
	if(tyH == 'string')
  {
	  if(this._itemHeight.toLowerCase() != 'auto' || this._itemHeight != '')
		  result +=';height:'+ this._itemHeight + 'px';
	}
	else
	  result +=';height:'+ this._itemHeight + 'px';
	if(tyW == 'string')
	{  
	  if(this._itemWidth.toLowerCase() != 'auto' || this._itemWidth != '')
		  result +=';width:'+ this._itemWidth + 'px';
  }
  else
    result +=';width:'+ this._itemWidth + 'px';	  	
	result +=';border-top:'   + this._itemBorder._top;
	result +=';border-right:' + this._itemBorder._right;
	result +=';border-bottom:'+ this._itemBorder._bottom;
	result +=';border-left:'  + this._itemBorder._left;
	result +=';text-align:'      + this._itemText._align;
	result +=';color:'      + this._itemText._color;
	result +=';text-decoration:' + this._itemText._decoration;
	result +=';white-space:'     + this._itemText._whiteSpace;
	result +=';font-weight:'     + this._itemText._weight;
	//result +=';background-color:' + this._itemBackColor;
	result +=';background-color:' + this._itemBack._color;
	result +=';background-repeat:'+ this._itemBack._repeat;
	result +=';background-image:'+this._itemBack._image;
	result +=';background-position:'+ this._itemBack._position;
	result +=';}';
	result +='div.TPopUp'+this._index+'{';
	result +='position:absolute';
	result +=';padding:'+ this._pop._padding;
	result +=';visibility:hidden';
	result +=';width:'+this._pop._itemWidth + 'px';
	result +=';border-top:'+    this._pop._border._top;
	result +=';border-right:'+  this._pop._border._right;
	result +=';border-bottom:'+ this._pop._border._bottom;
	result +=';border-left:'+   this._pop._border._left;
	result +=';background-color:'+     this._pop._background._color;
	result +=';background-image:'+     this._pop._background._image;
	result +=';background-position:' + this._pop._background._position;
	result +=';background-repeat:'+    this._pop._background._repeat;
	result +=';display:block';
	result +=';}';
	
	result +='table.TPopUpItem'+this._index+'{';
	result +='width:100%';
	result +=';height:'+ this._pop._itemHeight+'px';
	result +=';cursor:default';
	result +=';font-family:'+this._pop._font._family;
	result +=';font-size:'+this._pop._font._size;
	result +=';color:'+this._pop._itemText._color;
	result +=';text-align:'+this._pop._itemText._align;
	result +=';text-decoration:'+this._pop._itemText._decoration;
	result +=';white-space:'+this._pop._itemText._whiteSpace;
	result +=';font-weight:'+this._pop._itemText._weight;
	result +=';}';

	result +='div.TPopUpItem'+this._index+'{';
	result +='cursor:default';
	result +=';background-color:'+ this._pop._itemBack._color;
	result +=';background-image:'+this._pop._itemBack._image;
	result +=';background-position:'+this._pop._itemBack._position;
	result +=';background-repeat:'+this._pop._itemBack._repeat;
	result +=';border-top:'+   this._pop._itemBorder._top;
	result +=';border-right:'+ this._pop._itemBorder._right;
	result +=';border-bottom:'+this._pop._itemBorder._bottom;
	result +=';border-left:'+  this._pop._itemBorder._left;
	result +=';padding:'+ this._pop._itemPadding;
	result +=';}';
	result +='div.TPopUpItem'+this._index+'_1{';
	result +='cursor:default';
	result +=';background-color :'+this._pop._itemBackHL._color;
	result +=';background-image:'+this._pop._itemBackHL._image;
	result +=';background-position:'+this._pop._itemBackHL._position;
	result +=';background-repeat:'+this._pop._itemBackHL._repeat;
	result +=';border-top:'+   this._pop._itemBorderHL._top;
	result +=';border-right:'+ this._pop._itemBorderHL._right;
	result +=';border-bottom:'+this._pop._itemBorderHL._bottom;
	result +=';border-left:'+  this._pop._itemBorderHL._left;
	result +=';padding:'+ this._pop._itemPaddingHL;
	result +=';}';
	
	//separator
	this._pop._separator._width=((this._pop._itemWidth - this._pop._separator._width) < 0)?this._pop._itemWidth:this._pop._separator._width;
	
	var _div = Math.floor((this._pop._itemWidth - this._pop._separator._width)/2);
	switch(this._pop._separator._align)
	{
	  case 'left':
	    this._pop._separator._margin = '0px '+(_div*2)+'px 0px 0px';
	    break;
	  case 'right':
	    this._pop._separator._margin = '0px 0px 0px '+(_div*2)+'px';
	    break;
	  default:
	    this._pop._separator._margin = '0px '+_div+'px 0px '+_div+'px';
	}
	result +='div.TSeparator'+this._index+'{';
	result +='margin:'+this._pop._separator._margin;
	result +=';border-top:'+this._pop._separator._border._top;
	result +=';border-bottom:'+this._pop._separator._border._bottom;
	result +=';}';
	
	result +='td.TExpand'+this._index+'{';
	result +='text-align:right';
	result +=';padding-right:2px';
	result +=';font-family:'+this._pop._expandIcon._font._family;
	result +=';font-size:'+this._pop._expandIcon._font._size;
	result +=';font-weight:normal';
	result +=';text-decoration:none !important';
	result +=';white-space:nowrap !important';
	result +=';}';

	result +='td.TIcon'+this._index+'{';
	result +='width:24px';
	result +=';text-align:left';
	result +=';text-decoration:normal';
	result +=';white-space:nowrap';
	result +=';font-weight:normal';
	result +=';}';
	result +='img.'+this._index+'TIcon{';
	//result +='width:16px';
	result +='vertical-align:middle';
	result +=';}';

	result +='</style>';
	return result;
}
//methods
function SetCorrection(dLeft,dTop)
{
  dLeft = parseInt(dLeft);
  if(!dLeft)
    this._correction._left = 0;
  else
    this._correction._left = dLeft;
  dTop = parseInt(dTop);
  if(!dTop)
    this._correction._top = 0;
  else
    this._correction._top = dTop;
}
function SetPosition(dPosition,dLeft,dTop)
{
  switch(dPosition)
  {
    case 'absolute':
      this._position = dPosition;
      break;
    default:
      this._position = 'relative';
  }
  dLeft = parseInt(dLeft);
  if(!dLeft)
    this._left = 0;
  else
    this._left = dLeft;
  dTop = parseInt(dTop);
  if(!dTop)
    this._top = 0;
  else
    this._top = dTop;
}
function SetCellSpacing(dSpace)
{
  dSpace = parseInt(dSpace);
  if(!dSpace)
    this._cellSpacing = 0;
  else
    this._cellSpacing = dSpace;
}
function SetWidth(dWidth)
{
  dWidth = parseInt(dWidth);
  if(!dWidth)
    this._width = 'auto';
  else
    this._width = dWidth;
}
function SetItemDimension(dWidth,dHeight)
{
  dWidth = parseInt(dWidth);
  dHeight = parseInt(dHeight);
  if(!dWidth)
    this._itemWidth='auto';
  else
    this._itemWidth=dWidth;
  if(!dHeight)
    this._itemHeight='auto';
  else
    this._itemHeight=dHeight;
}
function SetBackground(dColor,dImage,dRepeat,dPos)
{
  (dColor == '')?this._background._color='transparent':this._background._color=dColor;
  (dImage == '')?this._background._image='none':this._background._image="url('"+dImage+"')";
  (dRepeat == '')?this._background._repeat='no-repeat':this._background._repeat=dRepeat;
  (dPos == '')?this._background._position='top left':this._background._position=dPos;

}
function SetItemBackground(dColor,dImage,dRepeat,dPos)
{
  (dColor == '')?this._itemBack._color='transparent':this._itemBack._color=dColor;
  (dImage == '')?this._itemBack._image='none':this._itemBack._image="url('"+dImage+"')";
  (dRepeat == '')?this._itemBack._repeat='no-repeat':this._itemBack._repeat=dRepeat;
  (dPos == '')?this._itemBack._position='top left':this._itemBack._position=dPos;
}
function SetItemBackgroundHL(dColor,dImage,dRepeat,dPos)
{
  (dColor == '')?this._itemBackHL._color='transparent':this._itemBackHL._color=dColor;
  (dImage == '')?this._itemBackHL._image='none':this._itemBackHL._image="url('"+dImage+"')";
  (dRepeat == '')?this._itemBackHL._repeat='no-repeat':this._itemBackHL._repeat=dRepeat;
  (dPos == '')?this._itemBackHL._position='top left':this._itemBackHL._position=dPos;
}
function SetItemBackgroundClick(dColor,dImage,dRepeat,dPos)
{
  (dColor == '')?this._itemBackClick._color='transparent':this._itemBackClick._color=dColor;
  (dImage == '')?this._itemBackClick._image='none':this._itemBackClick._image="url('"+dImage+"')";
  (dRepeat == '')?this._itemBackClick._repeat='no-repeat':this._itemBackClick._repeat=dRepeat;
  (dPos == '')?this._itemBackClick._position='top left':this._itemBackClick._position=dPos;
}
function SetShadow(dCreate,dColor,dDistance)
{
  if(dCreate)
  {
    this._shadow._create = dCreate;
    this._shadow._color = dColor;
    this._shadow._distance = dDistance;
  }
}
function SetFont(dFamily,dSize)
{
  this._font._family=dFamily;
  this._font._size=dSize;
}
function SetBorder(dSize,dColor,dType)
{
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._border._top = dBorder;
  this._border._right = dBorder;
  this._border._bottom = dBorder;
  this._border._left = dBorder;
}
function SetItemBorder(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorder._top = dBorder;
  this._itemBorder._right = dBorder;
  this._itemBorder._bottom = dBorder;
  this._itemBorder._left = dBorder;
}
function SetItemBorderHL(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderHL._top = dBorder;
  this._itemBorderHL._right = dBorder;
  this._itemBorderHL._bottom = dBorder;
  this._itemBorderHL._left = dBorder;
}
function SetItemBorderClick(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderClick._top = dBorder;
  this._itemBorderClick._right = dBorder;
  this._itemBorderClick._bottom = dBorder;
  this._itemBorderClick._left = dBorder;
}
function SetBorderTop(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._border._top = dBorder;
}
function SetItemBorderTop(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorder._top = dBorder;
}
function SetItemBorderTopHL(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderHL._top = dBorder;
}
function SetItemBorderTopClick(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderClick._top = dBorder;
}
function SetBorderRight(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._border._right = dBorder;
}
function SetItemBorderRight(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorder._right = dBorder;
}
function SetItemBorderRightHL(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderHL._right = dBorder;
}
function SetItemBorderRightClick(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderClick._right = dBorder;
}
function SetBorderBottom(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._border._bottom = dBorder;
}
function SetItemBorderBottom(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorder._bottom = dBorder;
}
function SetItemBorderBottomHL(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderHL._bottom = dBorder;
}
function SetItemBorderBottomClick(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderClick._bottom = dBorder;
}
function SetBorderLeft(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._border._left = dBorder;
}
function SetItemBorderLeft(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorder._left = dBorder;
}
function SetItemBorderLeftHL(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderHL._left = dBorder;
}
function SetItemBorderLeftClick(dSize,dColor,dType)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dBorder = dSize + 'px '+ dColor+' '+dType;
  this._itemBorderClick._left = dBorder;
}
function SetItemText(dColor,dAlign,dWeight,dDecoration,dWSpace)
{
  this._itemText._color = dColor;
  (dAlign == '')?this._itemText._align='left':this._itemText._align=dAlign;
  (dWeight == '')?this._itemText._weight='normal':this._itemText._weight=dWeight;
  (dDecoration =='')?this._itemText._decoration='none':this._itemText._decoration=dDecoration;
  (dWSpace == '')?this._itemText._whiteSpace='normal':this._itemText._whiteSpace=dWSpace;
}
function SetItemTextHL(dColor,dAlign,dWeight,dDecoration,dWSpace)
{
  this._itemTextHL._color = dColor;
  (dAlign == '')?this._itemTextHL._align='left':this._itemTextHL._align=dAlign;
  (dWeight == '')?this._itemTextHL._weight='normal':this._itemTextHL._weight=dWeight;
  (dDecoration =='')?this._itemTextHL._decoration='none':this._itemTextHL._decoration=dDecoration;
  (dWSpace == '')?this._itemTextHL._whiteSpace='normal':this._itemTextHL._whiteSpace=dWSpace;
}
function SetItemTextClick(dColor,dAlign,dWeight,dDecoration,dWSpace)
{
  this._itemTextClick._color = dColor;
  (dAlign == '')?this._itemTextClick._align='left':this._itemTextClick._align=dAlign;
  (dWeight == '')?this._itemTextClick._weight='normal':this._itemTextClick._weight=dWeight;
  (dDecoration =='')?this._itemTextClick._decoration='none':this._itemTextClick._decoration=dDecoration;
  (dWSpace == '')?this._itemTextClick._whiteSpace='normal':this._itemTextClick._whiteSpace=dWSpace;
}
function SetPaddings(dSize)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dPad= dSize+'px '+dSize+'px '+dSize+'px '+dSize+'px';
  this._padding =dPad;
}
function SetItemPaddingsHL(dSize)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dPad= dSize+'px '+dSize+'px '+dSize+'px '+dSize+'px';
  this._itemPaddingHL =dPad;
}
function SetItemPaddings(dSize)
{
  dSize = parseInt(dSize);
  if(!dSize)
    dSize = 0;
  var dPad= dSize+'px '+dSize+'px '+dSize+'px '+dSize+'px';
  this._itemPadding =dPad;
}
function SetPadding(dTop,dRight,dBottom,dLeft)
{
  dTop = parseInt(dTop);
  dRight = parseInt(dRight);
  dBottom = parseInt(dBottom);
  dLeft = parseInt(dLeft);
  if(!dTop) dTop = 0;
  if(!dRight) dRight = 0;
  if(!dBottom) dBottom = 0;
  if(!dLeft) dLeft = 0;
  var dPad= dTop+'px '+dRight+'px '+dBottom+'px '+dLeft+'px';
  this._padding =dPad;
}
function SetItemPaddingHL(dTop,dRight,dBottom,dLeft)
{
  dTop = parseInt(dTop);
  dRight = parseInt(dRight);
  dBottom = parseInt(dBottom);
  dLeft = parseInt(dLeft);
  if(!dTop) dTop = 0;
  if(!dRight) dRight = 0;
  if(!dBottom) dBottom = 0;
  if(!dLeft) dLeft = 0;
  var dPad= dTop+'px '+dRight+'px '+dBottom+'px '+dLeft+'px';
  this._itemPaddingHL =dPad;
}
function SetItemPadding(dTop,dRight,dBottom,dLeft)
{
  dTop = parseInt(dTop);
  dRight = parseInt(dRight);
  dBottom = parseInt(dBottom);
  dLeft = parseInt(dLeft);
  if(!dTop) dTop = 0;
  if(!dRight) dRight = 0;
  if(!dBottom) dBottom = 0;
  if(!dLeft) dLeft = 0;
  var dPad= dTop+'px '+dRight+'px '+dBottom+'px '+dLeft+'px';
  this._itemPadding =dPad;
}
function SetSeparator(dWidth,dAlign,dColor1,dColor2)
{
  dWidth = parseInt(dWidth);
  if(!dWidth)
    dWidth = 'auto';
  this._separator._width = dWidth;
  this._separator._align=dAlign;
  this._separator._border._top = '1px '+dColor1+' solid';
  (dColor2 =='')?this._separator._border._bottom='0px none solid':this._separator._border._bottom = '1px '+dColor2+' solid';
}
function SetExpandIcon(dCreate,dSymbol,dSize)
{
  if(dCreate)
  {
    this._expandIcon._create = true;
    switch(dSymbol)
    {
      case '' :
        break;
      default:
        this._expandIcon._symbol = dSymbol;
    }
    dSize = parseInt(dSize);
    if(!dSize)
      dSize = 0;
    this._expandIcon._font._size = dSize +'pt';
  }
  else
    this._expandIcon._create = false;
}
function SetDelay(dTimeOut)
{
  dTimeOut = parseInt(dTimeOut);
  if(!dTimeOut)
    dTimeOut = 0;
  this._timeOut = dTimeOut;
}
function findPosX(obj)
{
  if(_browser._name == "Konqueror")
	  var curleft = 10;
	else
	  var curleft = 0;
	  
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curleft += obj.offsetLeft;
			obj = obj.offsetParent;
		}
	}
	else if (obj.x)
		curleft += obj.x;
	return curleft;
}

function findPosY(obj)
{
  if(_browser._name == "Konqueror")
	  var curtop = 10;
	else
	  var curtop = 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curtop += obj.offsetTop;
			obj = obj.offsetParent;
		}
	}
	else if (obj.y)
		curtop += obj.y;
	return curtop;
}

/*
 	fungsi2 di bawah merupakan fungsi untuk memanipulasi menu
 	mulai dari mouse over,mouse out,mouse click
*/

//start: basic functions
function findTriggerPopID(elmID,mmObj)
{
	var result = -1;
	for(var i=0;i<= _arRegisterTriggerPopIndex[mmObj._index];i++)
	{
		if(_arRegisterTriggerPopID[mmObj._index][i] == elmID)
		{
			result = i;
			break;
		}
	}
	return result;
}
/*
	mengubah style dari anchor
	pada saat anchor mouse over memunculkan pop up menu
	syle diganti agar tetap seperti kondisi hover
*/
function saveTriggerPopID(elmID,mmObj)
{
	_arRegisterTriggerPopIndex[mmObj._index]++;
	var j = _arRegisterTriggerPopIndex[mmObj._index];	
	_arRegisterTriggerPopID[mmObj._index][j] = elmID;
	
	if(document.all)
	  var el = document.all(elmID);
	else if(document.getElementById)
	  var el = document.getElementById(elmID);
	  
	if(el.className.indexOf("TMenuItem") != -1)
	{
		//el.style.backgroundColor = mmObj._itemBackColorClick;
		el.style.backgroundColor    =        mmObj._itemBackClick._color;
		el.style.backgroundImage    = mmObj._itemBackClick._image;
		el.style.backgroundRepeat   =        mmObj._itemBackClick._repeat;
		el.style.backgroundPosition =        mmObj._itemBackClick._position;
		
		//el.style.color           = mmObj._itemTextColorClick;
		el.style.color           = mmObj._itemTextClick._color;
		el.style.fontWeight      = mmObj._itemTextClick._weight;
		el.style.textAlign       = mmObj._itemTextClick._align;
		el.style.textDecoration  = mmObj._itemTextClick._decoration;
		el.style.whiteSpace      = mmObj._itemTextClick._whiteSpace;
		el.style.borderTop       = mmObj._itemBorderClick._top;
		el.style.borderRight     = mmObj._itemBorderClick._right;
		el.style.borderBottom    = mmObj._itemBorderClick._bottom;
		el.style.borderLeft      = mmObj._itemBorderClick._left;	 
	}
	else
	{
		el.className ="TPopUpItem"+mmObj._index+"_1";
	}
}
/*
	mengembalikan style ke style awal
*/
function removeTriggerPopID(elmID,mmObj)
{
	var index = findTriggerPopID(elmID,mmObj)
	if(index > -1)
	{
		for(var i=_arRegisterTriggerPopIndex[mmObj._index];i>=index;i--)
		{
			var ID = _arRegisterTriggerPopID[mmObj._index][i];
			if(document.all)
			  var el = document.all(ID);
			else if(document.getElementById)
			  var el = document.getElementById(ID);
			  
			if(el.className.indexOf("TMenuItem") != -1)
			{
				//el.style.backgroundColor = mmObj._itemBackColor;
		    el.style.backgroundColor    =        mmObj._itemBack._color;
		    el.style.backgroundImage    = mmObj._itemBack._image;
		    el.style.backgroundRepeat   =        mmObj._itemBack._repeat;
		    el.style.backgroundPosition =        mmObj._itemBack._position;				
		    
				//el.style.color           = mmObj._itemTextColor;
				el.style.color           = mmObj._itemText._color;
				el.style.fontWeight      = mmObj._itemText._weight;
		    el.style.textAlign       = mmObj._itemText._align;
		    el.style.textDecoration  = mmObj._itemText._decoration;
		    el.style.whiteSpace      = mmObj._itemText._whiteSpace;
				el.style.borderTop       = mmObj._itemBorder._top;
				el.style.borderRight     = mmObj._itemBorder._right;
				el.style.borderBottom    = mmObj._itemBorder._bottom;
				el.style.borderLeft      = mmObj._itemBorder._left;
			}
			else
			{
			  var IDLen = ID.length;
			  var tableID = 'pr_'+ ID.substr(3,IDLen);
			  if(document.all)
			    var elTable = document.all(tableID);
			  else if(document.getElementById)
			    var elTable = document.getElementById(tableID);
			    
				el.className = "TPopUpItem"+mmObj._index;
				//elTable.style.color =mmObj._pop._itemTextColor;
				elTable.style.color =mmObj._pop._itemText._color;
				elTable.style.textAlign=mmObj._pop._itemText._align;
				elTable.style.textDecoration=mmObj._pop._itemText._decoration;
				elTable.style.whiteSpace=mmObj._pop._itemText._whiteSpace;
				elTable.style.fontWeight=mmObj._pop._itemText._weight;
			}
			_arRegisterTriggerPopID[mmObj._index][i] = null;
		}
		_arRegisterTriggerPopIndex[mmObj._index] = index - 1;
	}
}
function removeTriggerPopIDByIndex(index,mmObj)
{
	if(_arRegisterTriggerPopIndex[mmObj._index] > -1 && index > -1)
	{
		for(var i=_arRegisterTriggerPopIndex[mmObj._index];i>=index;i--)
		{
			var ID = _arRegisterTriggerPopID[mmObj._index][i];
			if(document.all)
			  var el = document.all(ID);
			else if(document.getElementById)
			  var el = document.getElementById(ID);
			
			if(el.className.indexOf("TMenuItem") != -1)
			{
				//el.style.backgroundColor = mmObj._itemBackColor;
		    el.style.backgroundColor    =        mmObj._itemBack._color;
		    el.style.backgroundImage    = mmObj._itemBack._image;
		    el.style.backgroundRepeat   =        mmObj._itemBack._repeat;
		    el.style.backgroundPosition =        mmObj._itemBack._position;				
				
				//el.style.color           = mmObj._itemTextColor;
				el.style.color           = mmObj._itemText._color;
				el.style.fontWeight      = mmObj._itemText._weight;
		    el.style.textAlign       = mmObj._itemText._align;
		    el.style.textDecoration  = mmObj._itemText._decoration;
		    el.style.whiteSpace      = mmObj._itemText._whiteSpace;
				el.style.borderTop       = mmObj._itemBorder._top;
				el.style.borderRight     = mmObj._itemBorder._right;
				el.style.borderBottom    = mmObj._itemBorder._bottom;
				el.style.borderLeft      = mmObj._itemBorder._left;
			}
			else
			{
			  var IDLen = ID.length;
			  var tableID = 'pr_'+ ID.substr(3,IDLen);
			  if(document.all)
			    var elTable = document.all(tableID); 
			  else if(document.getElementById)
			    var elTable = document.getElementById(tableID);				  
			    
				el.className = "TPopUpItem"+mmObj._index;
				//elTable.style.color =mmObj._pop._itemTextColor;
				elTable.style.color =mmObj._pop._itemText._color;
				elTable.style.textAlign=mmObj._pop._itemText._align;
				elTable.style.textDecoration=mmObj._pop._itemText._decoration;
				elTable.style.whiteSpace=mmObj._pop._itemText._whiteSpace;
				elTable.style.fontWeight=mmObj._pop._itemText._weight;				
			}
			_arRegisterTriggerPopID[mmObj._index][i] = null;
		}
		_arRegisterTriggerPopIndex[mmObj._index] = index - 1;
	}
}
function removeAllTriggerPopID(mmObj)
{
	if(_arRegisterTriggerPopIndex[mmObj._index] > -1)
	{
		for(var i=_arRegisterTriggerPopIndex[mmObj._index];i>=0;i--)
		{
			var ID = _arRegisterTriggerPopID[mmObj._index][i];
			if(document.all)
			  var el = document.getElementById(ID);
			else if(document.getElementById)
			  var el = document.getElementById(ID);
			
			if(el.className.indexOf("TMenuItem") != -1)
			{
			  if(_arMMClick[mmObj._index])
			  {
				  //el.style.backgroundColor = mmObj._itemBackColor;
		      el.style.backgroundColor    =        mmObj._itemBack._color;
		      el.style.backgroundImage    = mmObj._itemBack._image;
		      el.style.backgroundRepeat   =        mmObj._itemBack._repeat;
		      el.style.backgroundPosition =        mmObj._itemBack._position;				  
				  
				  //el.style.color           = mmObj._itemTextColor;
				  el.style.color           = mmObj._itemText._color;
				  el.style.fontWeight      = mmObj._itemText._weight;
		      el.style.textAlign       = mmObj._itemText._align;
		      el.style.textDecoration  = mmObj._itemText._decoration;
		      el.style.whiteSpace      = mmObj._itemText._whiteSpace;
				  el.style.borderTop       = mmObj._itemBorder._top;
				  el.style.borderRight     = mmObj._itemBorder._right;
				  el.style.borderBottom    = mmObj._itemBorder._bottom;
				  el.style.borderLeft      = mmObj._itemBorder._left;
				}
			}
			else
			{
  			var IDLen = ID.length;
	  		var tableID = 'pr_'+ ID.substr(3,IDLen);
	  		if(document.all)
		  	  var elTable = document.all(tableID);
		  	else if(document.getElementById)
		  	  var elTable = document.getElementById(tableID);

				el.className = "TPopUpItem"+mmObj._index;
				//elTable.style.color = mmObj._pop._itemTextColor;				
				elTable.style.color =mmObj._pop._itemText._color;
				elTable.style.textAlign=mmObj._pop._itemText._align;
				elTable.style.textDecoration=mmObj._pop._itemText._decoration;
				elTable.style.whiteSpace=mmObj._pop._itemText._whiteSpace;
				elTable.style.fontWeight=mmObj._pop._itemText._weight;
			}			
			_arRegisterTriggerPopID[mmObj._index][i] = null;
		}
		_arRegisterTriggerPopIndex[mmObj._index] = - 1;
	}	
}
function findRegisteredPopUpMenuID(elmID,mmObj)
{
	var result=-1;
	for(var i=0;i<=_arRegisterPopIndex[mmObj._index];i++)
	{
		if(_arRegisterPopID[mmObj._index][i] == elmID)
		{
			result = i;
			break;
		}
	}
	return result;
}
function showPopUpMenu(elmID,mmObj)
{
	//Register PopUpMenu
	_arRegisterPopIndex[mmObj._index]++;
	var j = _arRegisterPopIndex[mmObj._index];
	_arRegisterPopID[mmObj._index][j] = elmID;
	
	if(document.all)
	  document.all(elmID).style.visibility="visible";
	else if(document.getElementById)
	  document.getElementById(elmID).style.visibility="visible";
	if(mmObj._pop._shadow._create)
  {
    if(document.all)
		  document.all('sh_'+elmID).style.visibility="visible";
		else if(document.getElementById)
		  document.getElementById('sh_'+elmID).style.visibility="visible";
	}
}
function hidePopUpMenu(elmID,mmObj)
{
	var index = findRegisteredPopUpMenuID(elmID,mmObj);
	if(index >-1)
	{
		for(var i=_arRegisterPopIndex[mmObj._index];i>=index;i--)
		{
			var ID = _arRegisterPopID[mmObj._index][i];
			
			if(document.all)
			  document.all(ID).style.visibility="hidden";
			else if(document.getElementById)
			  document.getElementById(ID).style.visibility="hidden";
			  
			if(mmObj._pop._shadow._create)
		  {
		    if(document.all)
				  document.all('sh_'+ID).style.visibility="hidden";
				else if(document.getElementById)
				  document.getElementById('sh_'+ID).style.visibility="hidden";
			}
		}
		_arRegisterPopIndex[mmObj._index] = index - 1
	}
}
function hidePopUpMenuByIndex(index,mmObj)
{
	if(_arRegisterPopIndex[mmObj._index] >= index)
	{
		for(var i=_arRegisterPopIndex[mmObj._index];i>=index;i--)
		{
			var ID = _arRegisterPopID[mmObj._index][i];
			if(document.all)
			  document.all(ID).style.visibility="hidden";
			else if(document.getElementById)
			  document.getElementById(ID).style.visibility="hidden";
			if(mmObj._pop._shadow._create)
			{
			  if(document.all)
				  document.all('sh_'+ID).style.visibility="hidden";
				else if(document.getElementById)
				  document.getElementById('sh_'+ID).style.visibility="hidden";
			}
		}
		_arRegisterPopIndex[mmObj._index] = index-1;
	}
}
function hideAllPopUpMenu(mmObj)
{
	var index = _arRegisterPopIndex[mmObj._index];
	if(index > -1)
	{
		for(i=index;i>=0;i--)
		{
			var ID = _arRegisterPopID[mmObj._index][i];
			if(document.all)
			  document.all(ID).style.visibility="hidden";
			else if(document.getElementById)
			  document.getElementById(ID).style.visibility="hidden";
			if(mmObj._pop._shadow._create)
				document.getElementById('sh_'+ID).style.visibility="hidden";
		}
	}
	_arRegisterPopIndex[mmObj._index] = -1;
}
//end: basic functions

//start: fungsi manipulasi main menu
function triggerHideAll(mmObj)
{
  _arTriggerMenu[mmObj._index] = window.setTimeout('hideAll('+mmObj._name+')',mmObj._pop._timeOut);
}
function clearTriggerHideAll(mmObj)
{
  //window.clearTimeout(mmObj._lastPop);
  window.clearTimeout(_arTriggerMenu[mmObj._index]);
}
function hideAll(mmObj)
{
	hideAllPopUpMenu(mmObj);
	removeAllTriggerPopID(mmObj);
	_arMMClick[mmObj._index] = false;
	window.status = '';
}


function onMainClick(event,elm,popID,mmObj)
{
	if(!_arMMClick[mmObj._index])
	{	
		_arMMClick[mmObj._index] = true;
		if(popID !='')
		{
				showPopUpMenu(popID,mmObj);
				saveTriggerPopID(elm.id,mmObj);
		}
	}	
	else
	{
	  _arMMClick[mmObj._index] = false; //di false in sebelum removeAllTriggerPopID()
		hideAllPopUpMenu(mmObj);
		removeAllTriggerPopID(mmObj);
		
		//elm.style.backgroundColor = mmObj._itemBackColorHL;
		elm.style.backgroundColor    =        mmObj._itemBackHL._color;
		elm.style.backgroundImage    = mmObj._itemBackHL._image;
		elm.style.backgroundRepeat   =        mmObj._itemBackHL._repeat;
		elm.style.backgroundPosition =        mmObj._itemBackHL._position;		
		//elm.style.color           = mmObj._itemTextColorHL;	
		elm.style.color           = mmObj._itemTextHL._color;
		elm.style.fontWeight      = mmObj._itemTextHL._weight;
		elm.style.textAlign       = mmObj._itemTextHL._align;
		elm.style.textDecoration  = mmObj._itemTextHL._decoration;
		elm.style.whiteSpace      = mmObj._itemTextHL._whiteSpace;	
		elm.style.borderTop       = mmObj._itemBorderHL._top;
		elm.style.borderRight     = mmObj._itemBorderHL._right;
		elm.style.borderBottom    = mmObj._itemBorderHL._bottom;
		elm.style.borderLeft      = mmObj._itemBorderHL._left;
	}
  onBubble(event);
}
function onMainMOver(event,elm,popID,level,mmObj,status)
{
  window.status = status;
  clearTriggerHideAll(mmObj);
	if(_arRegisterTriggerPopID[mmObj._index][0] != elm.id)
	{
		if(_arRegisterTriggerPopID[mmObj._index][0] !=null)
		{
			removeAllTriggerPopID(mmObj);
		}
		if(_arMMClick[mmObj._index])
		{
			hideAllPopUpMenu(mmObj);
			removeAllTriggerPopID(mmObj);
			if(popID !='')
			{
				showPopUpMenu(popID,mmObj);
				saveTriggerPopID(elm.id,mmObj);
			}
		  //elm.style.backgroundColor = mmObj._itemBackColorClick;
		  elm.style.backgroundColor    =        mmObj._itemBackClick._color;
		  elm.style.backgroundImage    = mmObj._itemBackClick._image;
		  elm.style.backgroundRepeat   =        mmObj._itemBackClick._repeat;
		  elm.style.backgroundPosition =        mmObj._itemBackClick._position;		  

		  //elm.style.color           = mmObj._itemTextColorClick;
		  elm.style.color           = mmObj._itemTextClick._color;
		  elm.style.fontWeight      = mmObj._itemTextClick._weight;
		  elm.style.textAlign       = mmObj._itemTextClick._align;
		  elm.style.textDecoration  = mmObj._itemTextClick._decoration;
		  elm.style.whiteSpace      = mmObj._itemTextClick._whiteSpace;
		  elm.style.borderTop       = mmObj._itemBorderClick._top;
		  elm.style.borderRight     = mmObj._itemBorderClick._right;
		  elm.style.borderBottom    = mmObj._itemBorderClick._bottom;
		  elm.style.borderLeft      = mmObj._itemBorderClick._left;		
		}
	  else
	  {
		  //elm.style.backgroundColor = mmObj._itemBackColorHL;
		  elm.style.backgroundColor    =        mmObj._itemBackHL._color;
		  elm.style.backgroundImage    = mmObj._itemBackHL._image;
		  elm.style.backgroundRepeat   =        mmObj._itemBackHL._repeat;
		  elm.style.backgroundPosition =        mmObj._itemBackHL._position;		  
		  
		  //elm.style.color           = mmObj._itemTextColorHL;
		  elm.style.color           = mmObj._itemTextHL._color;
		  elm.style.fontWeight      = mmObj._itemTextHL._weight;
		  elm.style.textAlign       = mmObj._itemTextHL._align;
		  elm.style.textDecoration  = mmObj._itemTextHL._decoration;
		  elm.style.whiteSpace      = mmObj._itemTextHL._whiteSpace; 
		  elm.style.borderTop       = mmObj._itemBorderHL._top;
		  elm.style.borderRight     = mmObj._itemBorderHL._right;
		  elm.style.borderBottom    = mmObj._itemBorderHL._bottom;
		  elm.style.borderLeft      = mmObj._itemBorderHL._left;		
		}
	}
  onBubble(event);
}
function onBubble(event)
{
	if(!event)
	  var event = window.event;
	event.cancelBubble = true;
  if (event.stopPropagation)
    event.stopPropagation();	
  
}
function onMainMOut(event,elm,popID,mmObj)
{
	if(!_arMMClick[mmObj._index] || popID == '')
	{
		//elm.style.backgroundColor = mmObj._itemBackColor;
		elm.style.backgroundColor    =        mmObj._itemBack._color;
		elm.style.backgroundImage    = mmObj._itemBack._image;
		elm.style.backgroundRepeat   =        mmObj._itemBack._repeat;
		elm.style.backgroundPosition =        mmObj._itemBack._position;		
		
		//elm.style.color           = mmObj._itemTextColor;	
		elm.style.color           = mmObj._itemText._color;
		elm.style.fontWeight      = mmObj._itemText._weight;
		elm.style.textAlign       = mmObj._itemText._align;
		elm.style.textDecoration  = mmObj._itemText._decoration;
		elm.style.whiteSpace      = mmObj._itemText._whiteSpace;
		elm.style.borderTop       = mmObj._itemBorder._top;
		elm.style.borderRight     = mmObj._itemBorder._right;
		elm.style.borderBottom    = mmObj._itemBorder._bottom;
		elm.style.borderLeft      = mmObj._itemBorder._left;
	}
	triggerHideAll(mmObj)
	onBubble(event);		
}
//end: fungsi manipulasi main menu

//start: fungsi manipulasi popup menu
function onPopItemMOver(event,elm,popID,level,mmObj,status)
{
  var index = -1;
  window.status = status;
  clearTriggerHideAll(mmObj);	
  elm.parentNode.className = 'TPopUpItem'+ mmObj._index +'_1';
  elm.style.color =mmObj._pop._itemTextHL._color;
	elm.style.textAlign=mmObj._pop._itemTextHL._align;
	elm.style.textDecoration=mmObj._pop._itemTextHL._decoration;
	elm.style.whiteSpace=mmObj._pop._itemTextHL._whiteSpace;
	elm.style.fontWeight=mmObj._pop._itemTextHL._weight;  
/*
	if(index == -1)
	{
	  //hidePopUpMenuByIndex(level,mmObj);
		//removeTriggerPopIDByIndex(level,mmObj);
		mmObj._lastPop = window.setTimeout(function(){hidePopUpMenuByIndex(level,mmObj);removeTriggerPopIDByIndex(level,mmObj);},500);
	}
*/	

  if(popID !='')
  {
    index = findRegisteredPopUpMenuID(popID,mmObj);
  	if(index == -1)
  	{

		  hidePopUpMenuByIndex(level,mmObj);
		  removeTriggerPopIDByIndex(level,mmObj);	  
  		showPopUpMenu(popID,mmObj);
  		saveTriggerPopID('di_'+popID,mmObj);
  	}
  }	
  else
  {
    //mmObj._lastPop = window.setTimeout(function(){hidePopUpMenuByIndex(level,mmObj);removeTriggerPopIDByIndex(level,mmObj);},500);
    hidePopUpMenuByIndex(level,mmObj);removeTriggerPopIDByIndex(level,mmObj);
	}
	onBubble(event);	
}
function onPopItemMOut(event,elm,popID,mmObj)
{  
	if(popID == '')
	{
	  elm.parentNode.className ='TPopUpItem'+mmObj._index;
	  //elm.style.color= mmObj._pop._itemTextColor;
    elm.style.color =mmObj._pop._itemText._color;
		elm.style.textAlign=mmObj._pop._itemText._align;
		elm.style.textDecoration=mmObj._pop._itemText._decoration;
		elm.style.whiteSpace=mmObj._pop._itemText._whiteSpace;
		elm.style.fontWeight=mmObj._pop._itemText._weight;	  
	}
	
	triggerHideAll(mmObj);
	onBubble(event);
}
//end: fungsi manipulasi popup menu 

//window.onload= function(){Initialize();}
window.onload= Initialize;
//window.onresize = function(){Initialize();}
window.onresize=Initialize;
if(_browser._name == 'Netscape' && _browser._version == 4)
  window.captureEvents(event.MOUSEMOVE || event.RESIZE);

function _openURL(address)
{
  self.location = address;
}  