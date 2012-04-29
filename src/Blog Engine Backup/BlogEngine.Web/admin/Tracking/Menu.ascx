<%@ Control Language="C#" AutoEventWireup="true" Inherits="Admin.Comments.Menu" Codebehind="Menu.ascx.cs" %>
<%@ Import Namespace="BlogEngine.Core" %>
<ul>
    <li <%=Current("Pingbacks.aspx")%>><a href="Pingbacks.aspx"><%=Resources.labels.pingbacksAndTrackbacks %></a></li>
    <li <%=Current("referrers.aspx")%>><a href="referrers.aspx"><%=Resources.labels.referrers %></a></li>
</ul>