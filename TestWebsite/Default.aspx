<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="TestWebsite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Throttled fileserving</title>
</head>
<body>
	<form id="form1" runat="server" defaultbutton="cmdDownload">
		Maximum kbps to serve:&nbsp;&nbsp;
		<asp:CustomValidator ID="cvMaximumKbps" runat="server" ControlToValidate="txtMaximumKbps"
			Display="Dynamic" OnServerValidate="cvMaximumKbps_ServerValidate" /><br />
		<asp:TextBox ID="txtMaximumKbps" runat="server" />
		<asp:Button ID="cmdDownload" runat="server" Text="Download" OnClick="cmdDownload_Click" />
	</form>
</body>
</html>