using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class signUpForm : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    String constr;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
       // con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\admin\Documents\db_Sem6_Project.mdf;Integrated Security=True;Connect Timeout=30";
        con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Admin\Documents\tbl_loginDetails.mdf;Integrated Security=True;Connect Timeout=30";
        con.Open();
    }
    protected void btnsignup_Click(object sender, EventArgs e)
    {
        String str = txtuname.Text + ".jpg";
            FileUpload1.SaveAs(Server.MapPath("~") + "/ProfilePic/" + str);
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "Insert into tbl_loginDetails values('" + txtuname.Text + "','" + txtpswd.Text + "','" + txtfname.Text + "','" + txtlname.Text + "'," + txtphone.Text + ",'" + txtemail.Text + "');";

        // con.Open();
        cmd.CommandType = CommandType.Text;
        int status = cmd.ExecuteNonQuery();
        if (status > 0)
        {
            //  ClientScript.RegisterStartupScript(this.GetType(), "key1", "alert('Login Successful')",true);
            Response.Write("<script>alert('SignIn Successful');</script>");
            string folderPath = Server.MapPath("~/DashBoard/" + txtuname.Text);

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath + "/"+txtuname.Text);
                Directory.CreateDirectory(folderPath + "/" + txtuname.Text+"/SharedFolder");
                Directory.CreateDirectory(folderPath + "/" + txtuname.Text + "/SecureItems");
            }
            Response.Redirect("~/loginForm.aspx");

        }
        else
        {
            Response.Write("<script>alert('SignIn UnSuccessful');</script>");
        }
        con.Close();
    }
}