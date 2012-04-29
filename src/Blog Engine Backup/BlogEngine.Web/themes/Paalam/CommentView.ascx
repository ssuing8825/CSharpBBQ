<%@ Control Language="C#" AutoEventWireup="true" Inherits="themes_Paalam_CommentView" Codebehind="CommentView.ascx.cs" %>
<br />
<div id="id_<%=Comment.Id %>" class="underline comment<%= Post.Author.Equals(Comment.Author, StringComparison.OrdinalIgnoreCase) ? " self" : "" %>">
  <span id="gravatar"><%= Gravatar(80)%></span>
  <p><cite>Response by <%= Comment.Website != null ? "<a href=\"" + Comment.Website + "\">" + Comment.Author + "</a>" : Comment.Author %></cite> <%= Flag %> <em>on <%= Comment.DateCreated %></em></p>
  <p class="cmtinfo"><%= Text %></p>
  <br />
  <%= AdminLinks %>
  <br />
</div>
<div style=" clear: both"></div>