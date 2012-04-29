<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>

<div class="post xfolkentry" id="post<%=Index %>">

    <h1><a href="<%=Post.RelativeLink %>" class="taggedlink"><%=Server.HtmlEncode(Post.Title) %></a></h1>
    <div class="title">
        <span class="textleft">
            By <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Server.UrlEncode(Post.Author) %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author %></a> at <%=Post.DateCreated.ToString("MMMM dd, yyyy HH:mm") %>
            <br />
            Filed Under: <%=CategoryLinks(", ") %>
        </span>
        <span class="textright"> <%=AdminLinks %> </span>
    </div>

    <div class="text">
        <asp:PlaceHolder ID="BodyContent" runat="server" />
    </div>
    <div>
        <%=Rating %>
    </div>

    <div class="postinfo">
        <span class="tagged">Tags: <%=TagLinks(", ") %></span><br />
    </div>

    <div class="bookmarks">
        <a class="commentslink" rel="nofollow" href="<%=Post.RelativeLink %>#comment"><%=Resources.labels.comments %> (<%=Post.ApprovedComments.Count %>)</a>
        <a class="EmailIt" rel="nofollow" href="mailto:?subject=<%=Post.Title %>&amp;body=Thought you might like this: <%=Post.AbsoluteLink.ToString() %>">E-mail</a>
        <a class="dotnetkicks" rel="nofollow" href="http://www.dotnetkicks.com/submit?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>">Kick it!</a>
        <a class="dzone" rel="nofollow" href="http://www.dzone.com/links/add.html?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>">DZone it!</a>
        <a class="delicious" rel="nofollow" href="http://del.icio.us/post?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>">del.icio.us</a>
        <a class="plink" rel="bookmark" href="<%=Post.PermaLink %>">Permalink</a>
        <a class="rssLink" rel="nofollow" href="<%=CommentFeed %>">Post RSS</a>
    </div>
</div>