Dim oFS    Dim oFSFolder    Dim sPath, sFlagPath, nExpireDays   
Dim dFlagDate 
' 根目錄 
sPath = "D:\CIM-SE-RPT-WEB\E-FAB_dotnet\LOG\"
' 用來比對資料是否過期的 flag 檔案 

' 預設3天前的資料夾算是過期的資料夾 
nExpireDays = -3   
Set oFS = CreateObject("Scripting.FileSystemObject")    
' 判斷根目錄是否存在 
If Not oFS.FolderExists(sPath) Then   Set oFS = Nothing   
WScript.Quit 0 End If   ' 讀取後續要做日期判斷的設定值 
 
Set oFSFolder = oFS.GetFolder(sPath)  
' 取得根目錄下所有的子目錄 
Set fDates = oFSFolder.SubFolders 
For Each fDateItem in fDates   
If fDateItem.DateLastModified <= dFlagDate Then    
' 利用遞迴的方式刪除不要(空)的資料夾     
DeleteEmptyFolder(fDateItem)   
End If
Next
Set oFSFolder = Nothing 
Set oFS = Nothing   
Sub DeleteEmptyFolder(oFolder)    
Set oFSFolder = oFS.GetFolder(oFolder.Path)   
Set fItems = oFSFolder.SubFolders   
' 這邊是判斷檔案和資料夾都為空的情況, 如果是要刪過期的資料, 可再另外判斷   
If (oFSFolder.SubFolders.Count + oFSFolder.Files.Count)=0 Then    
' 如果此資料夾底下無資料, 刪除此資料夾     
oFS.DeleteFolder oFSFolder.Path   
Else     
' 如果有檔案或是資料夾, 繼續往下進行刪除的動作     
For Each fItem in fItems       
DeleteEmptyFolder(fItem)     
Next  End If
End Sub 