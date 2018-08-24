using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class ViewProfile : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    String constr;
    SqlDataReader dr;
    int flag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String pic = Session["uname"].ToString() + ".jpg";
            Image1.ImageUrl = @"~\ProfilePic\" + pic;
            openConnection();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from tbl_loginDetails where uname='" + Session["uname"].ToString() + "'";
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                dr.Close();
                con.Close();
                Response.Write("<script>alert('NO records found');</script>");
            }
            else
            {
                dr.Read();
                txtuname.Text = dr[0].ToString();
                txtpswd.Text = dr[1].ToString();
                txtfname.Text = dr[2].ToString();
                txtlname.Text = dr[3].ToString();
                txtphone.Text = dr[4].ToString();
                txtemail.Text = dr[5].ToString();
                dr.Close();
                con.Close();
            }
        }
    }

    public void openConnection()
    {
        con = new SqlConnection();
        //con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\admin\Documents\db_Sem6_Project.mdf;Integrated Security=True;Connect Timeout=30";
        con.ConnectionString=@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Admin\Documents\tbl_loginDetails.mdf;Integrated Security=True;Connect Timeout=30";
        con.Open();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            String path = Server.MapPath("~") + "/ProfilePic/" + txtuname.Text + ".jpg";
            File.Delete(path);
            FileUpload1.SaveAs(Server.MapPath("~") + "/ProfilePic/" + txtuname.Text + ".jpg");
        }
        openConnection();
        String uname = Session["uname"].ToString();

        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update tbl_loginDetails set pswd='" + txtpswd.Text + "', fname='" + txtfname.Text + "', lname='" + txtlname.Text + "', telephone=" + txtphone.Text + ", email='" + txtemail.Text + "' where uname='" + Session["uname"].ToString() + "';";
        int status = cmd.ExecuteNonQuery();

        if (status >= 1)
        {
            Response.Write("<script>alert('Success')</script>");
            Response.Redirect("~/gvDashBoard.aspx");
        }
        else
        {
            Response.Write("<script>alert('Failed')</script>");
        }
        con.Close();
    }
}