Dim oFS    Dim oFSFolder    Dim sPath, sFlagPath, nExpireDays   
Dim dFlagDate 
' �ڥؿ� 
sPath = "D:\CIM-SE-RPT-WEB\E-FAB_dotnet\LOG\"
' �ΨӤ���ƬO�_�L���� flag �ɮ� 

' �w�]3�ѫe����Ƨ���O�L������Ƨ� 
nExpireDays = -3   
Set oFS = CreateObject("Scripting.FileSystemObject")    
' �P�_�ڥؿ��O�_�s�b 
If Not oFS.FolderExists(sPath) Then   Set oFS = Nothing   
WScript.Quit 0 End If   ' Ū������n������P�_���]�w�� 
 
Set oFSFolder = oFS.GetFolder(sPath)  
' ���o�ڥؿ��U�Ҧ����l�ؿ� 
Set fDates = oFSFolder.SubFolders 
For Each fDateItem in fDates   
If fDateItem.DateLastModified <= dFlagDate Then    
' �Q�λ��j���覡�R�����n(��)����Ƨ�     
DeleteEmptyFolder(fDateItem)   
End If
Next
Set oFSFolder = Nothing 
Set oFS = Nothing   
Sub DeleteEmptyFolder(oFolder)    
Set oFSFolder = oFS.GetFolder(oFolder.Path)   
Set fItems = oFSFolder.SubFolders   
' �o��O�P�_�ɮשM��Ƨ������Ū����p, �p�G�O�n�R�L�������, �i�A�t�~�P�_   
If (oFSFolder.SubFolders.Count + oFSFolder.Files.Count)=0 Then    
' �p�G����Ƨ����U�L���, �R������Ƨ�     
oFS.DeleteFolder oFSFolder.Path   
Else     
' �p�G���ɮשάO��Ƨ�, �~�򩹤U�i��R�����ʧ@     
For Each fItem in fItems       
DeleteEmptyFolder(fItem)     
Next  End If
End Sub 