using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ThemeChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t1.jpg";
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t2.jpg";
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t3.jpg";
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t4.jpg";
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t5.jpg";
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t6.jpg";
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t7.jpg";
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t8.jpg";
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t9.jpg";
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeUrl"] = "t10.jpg";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Application["theme"] = Session["themeUrl"].ToString();
        Response.Redirect("gvDashBoard.aspx");
    }
}