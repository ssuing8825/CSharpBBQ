<%@ Control Language="C#" AutoEventWireup="true" Inherits="Admin.ExtensionManager.UserControlsXmanagerSourceEditor" Codebehind="~/admin/Extension Manager/Editor.ascx.cs" %>
<h1><%=Resources.labels.sourceViewer %>: <%=ExtensionName%></h1>
<div>
    <asp:TextBox ID="txtEditor" runat="server" TextMode="multiLine" Width="100%" Height="320"></asp:TextBox>
</div>
<div style="padding:5px 0 0 0">
    <asp:Button ID="btnSave" CssClass="btn primary" Visible="true" runat="server" Text="<%$ Resources:labels, save %>" OnClick="BtnSaveClick"  />
    <%=Resources.labels.or %> <a href="default.aspx"><%=Resources.labels.cancel %></a>
    <span style="padding-left:10px">[<%=GetExtFileName() %>]</span>
</div>