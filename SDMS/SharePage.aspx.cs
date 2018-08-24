using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class SharePage : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adp;
    String user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            openConnection();
            cmd.CommandType = CommandType.Text;
            adp = new SqlDataAdapter("Select * from tbl_loginDetails", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "uname";
            DropDownList1.DataValueField = "uname";
            DropDownList1.DataBind();
          
        }
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        String s = Session["shareUrl"].ToString() + "/" + Session["shareFname"].ToString();
        if (File.Exists(s))
        {
            File.Copy(s, "G:/Ayushi_6_4/SDMS/DashBoard/" + user + "/" + user + "/SharedFolder/" + Session["shareFname"].ToString());
            //File.Copy(
        }
        else
        {
            DirectoryCopy(s, "G:/Ayushi_6_4/SDMS/DashBoard/" + user + "/" + user + "/SharedFolder/" + Session["shareFname"].ToString(), true);
        }
        Response.Redirect("~/gvDashBoard.aspx");
    }

    public static void DirectoryCopy(
       string sourceDirName, string destDirName, bool copySubDirs)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();


        // If the source directory does not exist, throw an exception.
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory does not exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }


        // Get the file contents of the directory to copy.
        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            // Create the path to the new copy of the file.
            string temppath = Path.Combine(destDirName, file.Name);

            // Copy the file.
            file.CopyTo(temppath, false);
        }

        // If copySubDirs is true, copy the subdirectories.
        if (copySubDirs)
        {

            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                string temppath = Path.Combine(destDirName, subdir.Name);

                // Copy the subdirectories.
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        user = DropDownList1.SelectedItem.ToString();
    }
}