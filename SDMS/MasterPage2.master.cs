using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["themeStatus"].Equals("1"))
        {
            String i = Session["themeUrl"].ToString();
            bg1.Style["background-image"] = "url('Images/" + i + "')";
            bg2.Style["background-image"] = "url('Images/" + i + "')";
        }
        else
        {
            String i = Application["theme"].ToString();
            bg1.Style["background-image"] = "url('Images/" + i + "')";
            bg2.Style["background-image"] = "url('Images/" + i + "')";
        }
    }
}
