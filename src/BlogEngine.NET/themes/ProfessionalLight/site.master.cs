using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class ProfessionalLight : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
		if (Page.User.Identity.IsAuthenticated)
		{
			aLogin.InnerText = Resources.labels.logoff;
			aLogin.HRef = BlogEngine.Core.Utils.RelativeWebRoot + @"account/login.aspx?logoff";
		}
		else
		{
			aLogin.HRef = BlogEngine.Core.Utils.RelativeWebRoot + @"account/login.aspx";
			aLogin.InnerText = Resources.labels.login;
		}
  }

}
