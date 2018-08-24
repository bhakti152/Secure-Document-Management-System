using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class loginForm : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void openConnection()
    {
        con = new SqlConnection();
        //con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\admin\Documents\db_Sem6_Project.mdf;Integrated Security=True;Connect Timeout=30";
        con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Admin\Documents\tbl_loginDetails.mdf;Integrated Security=True;Connect Timeout=30";
        con.Open();
        cmd = new SqlCommand();
        cmd.Connection = con;
        
    }
    
    protected void btnLogin_Click(object sender, EventArgs e)
    {          
        String username = TextBox1.Text;
        openConnection();
        cmd.CommandText = "Select count(*) from tbl_loginDetails where uname='" + TextBox1.Text + "' and pswd='" + TextBox2.Text + "';";
        cmd.CommandType = CommandType.Text;
        int status = (int)cmd.ExecuteScalar();
        if (status == 1)
        {
            // ClientScript.RegisterStartupScript(this.GetType(), "key", "alert('Login Successfully');", true);
            Response.Write("<script>alert('Login Successfully')</script>");

            Session["uname"] = username;
            Response.Redirect("~/gvDashBoard.aspx");
            
        }
        else
        {
            // ClientScript.RegisterStartupScript(this.GetType(), "key", "alert('Login UnSuccessfull');", true);
            Response.Write("<script>alert('Login UnSuccessfully')</script>");
        }
        con.Close();
    }
    
}