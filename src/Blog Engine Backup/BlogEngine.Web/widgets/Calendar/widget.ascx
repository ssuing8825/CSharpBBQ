﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Widgets.Calendar.Widget" Codebehind="widget.ascx.cs" %>
<%@ Import Namespace="BlogEngine.Core" %>
<div style="text-align: center">
    <blog:PostCalendar ID="PostCalendar1" runat="Server" NextMonthText=">>" DayNameFormat="FirstTwoLetters"
        FirstDayOfWeek="monday" PrevMonthText="<<" CssClass="calendar" BorderWidth="0"
        WeekendDayStyle-CssClass="weekend" OtherMonthDayStyle-CssClass="other" UseAccessibleHeader="true"
        EnableViewState="false" />
    <br />
    <a href="<%=Utils.AbsoluteWebRoot %>calendar/default.aspx">View posts in large calendar</a>
</div>
