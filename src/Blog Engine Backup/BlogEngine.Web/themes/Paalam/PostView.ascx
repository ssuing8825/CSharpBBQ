<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>
<div class="post xfolkentry">
    <p class="date">
        <span class="month"><%=Post.DateCreated.ToString("MMM")%></span><span class="day"><%=Post.DateCreated.ToString("dd")%></span><span class="year"><%=Post.DateCreated.ToString("yyyy")%></span>
    </p>
    <h2 class="title">
        <a class="postheader taggedlink" href="<%=Post.RelativeLink %>"><%=Post.Title %></a>
    </h2>
    <div class="meta">
        <p>
            Published by <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Post.Author %>.aspx"><%=Post.Author %></a> at <%=Post.DateCreated.ToString("t")%> under <%=CategoryLinks(" | ") %>
        </p>
    </div>
    <div class="entry">        
        <asp:PlaceHolder ID="BodyContent" runat="server" />
        <br />
        <br />
        <span class="links" >
            <a target="_blank" title="Kick it!" href="http://www.dotnetkicks.com/submit?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[KickIt]" src="http://www.dotnetkicks.com/favicon.ico" width="16" /></a>
            <a target="_blank" title="DZone" href="http://www.dzone.com/links/add.html?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[Dzone]" src="http://www.dzone.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Digg This Story" href="http://digg.com/submit?phase=2&amp;url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[Digg]" src="http://www.digg.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Reddit" href="http://reddit.com/submit?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[Reddit]" src="http://www.reddit.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Save to del.icio.us" href="http://del.icio.us/post?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[del.icio.us]" src="http://www.delicious.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Share on Facebook" href="http://www.facebook.com/share.php?u=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>"><img height="18" alt="[Facebook]" src="http://www.facebook.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Add to my Technorati Favorites" href="http://technorati.com/faves?add=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>"><img height="18" alt="[Technorati]" src="http://www.technorati.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Save to Google Bookmarks" href="http://www.google.com/bookmarks/mark?op=edit&output=popup&bkmk=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><img height="18" alt="[Google]" src="http://www.google.com/favicon.ico" width="16" /></a> 
            <a target="_blank" title="Stumble it!" href="http://www.stumbleupon.com/submit?url=<%=Server.UrlEncode(Post.AbsoluteLink.ToString()) %>&amp;title=<%=Server.UrlEncode(Post.Title) %>"><IMG height="18" alt="[StumbleUpon]" src="http://www.stumbleupon.com/favicon.ico" width="16" /></a>
        </span>
        <p class="tags">
            Tags: <%=TagLinks(", ") %>
        </p>
    </div>
    <p class="comments" style="clear:both">
        <span style="float: right">
            <%=AdminLinks %>
            <a href="mailto:?subject=<%=Post.Title %>&amp;body=Thought you might like this: <%=Post.AbsoluteLink.ToString() %>">E-mail</a> | 
            <a href="<%=Post.PermaLink %>" rel="bookmark">Permalink</a> |
            <a href="<%=Post.TrackbackLink %>">Trackback</a> | 
            <a href="<%=CommentFeed %>">Post RSS<img src="<%=VirtualPathUtility.ToAbsolute("~/pics/")%>rssButton.gif" alt="RSS comment feed" style="margin-left:3px" /></a>
        </span>
        <a href="<%=Post.RelativeLink %>#comment"><%=Post.ApprovedComments.Count %> Responses</a>
    </p>
</div>