using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using System.Collections;
using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using System.Security.Cryptography;
using System.Text;

public partial class gvDashBoard : System.Web.UI.Page
{


    Hashtable ht;
    String url1 = "";
    private ArrayList extension;
   // List<ListItem> filesExt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["themeStatus"].Equals("1"))
        {
            String i = Session["themeUrl"].ToString();
            bg3.Style["background-image"] = "url('Images/" + i + "')";
        }
        else
        {
            String i = Application["theme"].ToString();
            bg3.Style["background-image"] = "url('Images/" + i + "')";
        }
        String pic = Session["uname"].ToString()+".jpg";
        ht = new Hashtable();
        Image1.ImageUrl = @"~\ProfilePic\" + pic;
        Image1.Visible = true;
        if (!Page.IsPostBack)
        {
            Image1.ImageUrl = @"~\ProfilePic\" + pic;
            Image1.Visible = true;
            
           
            if (Session["count"].ToString() == "1")
            {
                btnMoveHere.Enabled = true;
            }
            else
            {
                btnMoveHere.Enabled = false;
            }

            if (Session["cpaste"].ToString() == "1")
            {
                btnMoveHere.Enabled = true;
            }
            else
            {
                btnMoveHere.Enabled = false;
            }
            TreeView1.Attributes.Add("onclick", "return OnTreeClick(event)");
            DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/Dashboard/" + Session["uname"].ToString()));
            this.PopulateTreeView(rootInfo, null);
        }
    }

    public void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode)
    {
        foreach (DirectoryInfo directory in dirInfo.GetDirectories())
        {
            TreeNode directoryNode = new TreeNode
            {
                Text = directory.Name,
                Value = directory.FullName
            };

            if (treeNode == null)
            {
                //If Root Node, add to TreeView.
                TreeView1.Nodes.Add(directoryNode);
            }
            else
            {
                //If Child Node, add to Parent Node.
                treeNode.ChildNodes.Add(directoryNode);
            }


            PopulateTreeView(directory, directoryNode);
        }

    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        TextBox lbl = (TextBox)Master.FindControl("TextBox1");
        String path = lbl.Text;

        String[] arr;
        Char[] a = { '*', '|', '*' };
        arr = path.Split(a, System.StringSplitOptions.RemoveEmptyEntries);
        string p = string.Join("/", arr);
        Session["curPath"] = p;
        Session["newFolPath"] = p;
        Session["MoveHere"] = p;
        Session["uploadpath"] = p;
        Session["sortPath"] = p;
        //backButton
        if (Session["arrayList"].Equals(""))
        {
            ArrayList al = new ArrayList();
            al.Add(p);
            Session["arrayList"] = al;
        }
        else
        {
            ArrayList al = (ArrayList)Session["arrayList"];
            al.Add(p);
            Session["arrayList"] = al;
        }

       
        String url = p;
        Session["url"] = p;
       
        string[] filePaths = Directory.GetDirectories(url);
        List<Class1> files = new List<Class1>();
        foreach (string filePath in filePaths)
        {

            string result = System.IO.Path.GetFileName(filePath);
            String imageUrl = "~/Images/iconfolder.png";
            float ksize = calculateFolderSize(filePath);
            float fsize = ksize / 1024;
            files.Add(new Class1() { imgUrl = imageUrl, fileName = result, fileLastModified = Directory.GetLastAccessTime(filePath).ToString(), fileType = "", fileSize = fsize });
            ht.Add(Path.GetFileName(filePath), filePath);
        }

        string[] content = Directory.GetFiles(url + "/");
        // List<ListItem> con = new List<ListItem>();
        extension = new ArrayList();
        // List<Class1> files1=new List<Class1>();
        foreach (string filePath in content)
        {
            string fileName = Path.GetFileName(filePath);
            String u;
            String ext = Path.GetExtension(filePath);
            String[] a1 = ext.Split('.');
            if (a1[1].Equals("txt"))
            {
                u = @"~\Images\txt.png";
            }
            else if ((a1[1].Equals("jpg")) || (a1[1].Equals("png")))
            {
                u = @"~\Images\img.png";
            }
            else if (a1[1].Equals("pdf"))
            {
                u = @"~\Images\pdficon.jpg";
            }
            else if (a1[1].Equals("pptx"))
            {
                u = @"~\Images\ppticon.png";
            }
            else if (a1[1].Equals("docx"))
            {
                u = @"~\Images\docxicon.png";
            }
            else if (a1[1].Equals("bmp"))
            {
                u = @"~\Images\bmpicon.png";
            }
            else if (a1[1].Equals("zip"))
            {
                u = @"~\Images\raricon.png";
            }
            else
            {
                u = @"~\Images\iconfolder.png";
            }
            FileInfo f = new FileInfo(filePath);
            float len1 = f.Length;
            float len = len1 / 1024;
            files.Add(new Class1() { imgUrl = u, fileName = fileName, fileLastModified = File.GetLastAccessTime(filePath).ToString(), fileType = a1[1], fileSize = len });
            ht.Add(Path.GetFileName(filePath), filePath);
        }
        Session["hashtable"] = ht;
        GridView1.DataSource = files;
        //getImage();
        GridView1.DataBind();



    }

    protected static float calculateFolderSize(string folder)
    {
        float folSize = 0.0f;
        try
        {
            if (!Directory.Exists(folder))
            {
                return folSize;
            }
            else
            {
                try
                {
                    foreach (string file in Directory.GetFiles(folder))
                    {
                        if (File.Exists(file))
                        {
                            FileInfo f = new FileInfo(file);
                            folSize += f.Length;
                        }
                    }
                    foreach (string dir in Directory.GetDirectories(folder))
                    {
                        folSize += calculateFolderSize(dir);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("Unable to calculate folder size:{0}", e.Message);
                }

            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine("Unable to calculate folder size:{0}", e.Message);
        }
        return folSize;
    }

    protected String getUrl(String folder)
    {
        String u = "";
        Hashtable ht1 = new Hashtable();
        ht1 = Session["hashtable"] as Hashtable;
        IDictionaryEnumerator e1 = ht1.GetEnumerator();
        while (e1.MoveNext())
        {
            if (e1.Key.ToString().Equals(folder))
            {
                u = e1.Value.ToString();
                Session["searchStatus"] = 0;
            }

        }
        return u;
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Folder"))
        {

            String folder = e.CommandArgument.ToString();
            String searchUrl;
            int status = int.Parse(Session["searchStatus"].ToString());
            if (status == 1)
            {
                searchUrl = getUrl(folder);
                Session["url"] = searchUrl;
                Session["url"] += "\\";
                GridView1.AllowPaging = true;
            }
            else
            {
                Session["url"] += "/" + folder;
            }



            String url = Session["url"].ToString();
            Session["newFolPath"] = url;
            Session["uploadpath"] = url;
            if (Session["arrayList"].Equals(""))
            {
                ArrayList al = new ArrayList();
                al.Add(url);
                Session["arrayList"] = al;
            }
            else
            {
                ArrayList al = (ArrayList)Session["arrayList"];
                al.Add(url);
                Session["arrayList"] = al;
            }



            //Response.Write("<script>alert('" + folder + "')</script>");
            string[] filePaths = Directory.GetDirectories(url);
            List<Class1> files = new List<Class1>();

            foreach (string filePath in filePaths)
            {
                string result = System.IO.Path.GetFileName(filePath);
                String imageUrl = "~/Images/iconfolder.png";
                float ksize = calculateFolderSize(filePath);
                float fsize = ksize / 1024;
                files.Add(new Class1() { imgUrl = imageUrl, fileName = result, fileLastModified = Directory.GetLastAccessTime(filePath).ToString(), fileType = "", fileSize = fsize });

                ht.Add(Path.GetFileName(filePath), filePath);

            }

            string[] content = Directory.GetFiles(url + "/");
            // List<ListItem> con = new List<ListItem>();
            foreach (string filePath in content)
            {
                Path.GetExtension(filePath);
                // string result = System.IO.Path.GetFileName(filePath);
                string fileName = Path.GetFileName(filePath);
                String u;
                String ext = Path.GetExtension(filePath);
                String[] a1 = ext.Split('.');
                if (a1[1].Equals("txt"))
                {
                    u = @"~\Images\txt.png";
                }
                else if ((a1[1].Equals("jpg")) || (a1[1].Equals("png")))
                {
                    u = @"~\Images\img.png";
                }
                else if (a1[1].Equals("pdf"))
                {
                    u = @"~\Images\pdficon.jpg";
                }
                else if (a1[1].Equals("pptx"))
                {
                    u = @"~\Images\ppticon.png";
                }
                else if (a1[1].Equals("docx"))
                {
                    u = @"~\Images\docxicon.png";
                }
                else if (a1[1].Equals("bmp"))
                {
                    u = @"~\Images\bmpicon.png";
                }
                else if (a1[1].Equals("zip"))
                {
                    u = @"~\Images\raricon.png";
                }
                else
                {
                    u = @"~\Images\iconfolder.png";
                }
                FileInfo f = new FileInfo(filePath);
                float len1 = f.Length;
                float len = len1 / 1024;
                files.Add(new Class1() { imgUrl = u, fileName = fileName, fileLastModified = File.GetLastAccessTime(filePath).ToString(), fileType = a1[1], fileSize = len });

                ht.Add(Path.GetFileName(filePath), filePath);

            }
            // sw.Flush();
            // fs.Close();

            GridView1.DataSource = files;
            GridView1.DataBind();

            Session["hashtable"] = ht;
        }
        else if (e.CommandName.Equals("xDownload"))
        {

            String fileName1 = "";
            String fileName2 = "";
            fileName1 = e.CommandArgument.ToString();
            if (fileName1.Contains("_Encrypt"))
            {
                DecryptFile(fileName1);
            }
            else
            {
                    String searchUrl;
                    int status = int.Parse(Session["searchStatus"].ToString());
                    if (status == 1)
                    {
                        searchUrl = getUrl(fileName1);
                        Session["url"] = searchUrl;
                        fileName2 = Session["url"].ToString();
                        GridView1.AllowPaging = true;
                    }
                    else
                    {
                        fileName2 = Session["url"].ToString() + "/" + fileName1;
                    }

                    String ex = Path.GetExtension(fileName2);
                    if (ex.Equals(""))
                    {
                        String newZip = fileName1 + "Tmp.zip";
                        ZipFile.CreateFromDirectory(fileName2, "C:/SEM6/Projects/Ayushi_6_4/SDMS/temp/" + newZip);
                        url1 = newZip;
                        String newUrl = "C:/SEM6/Projects/Ayushi_6_4/SDMS/temp/" + newZip;
                        DownloadFromServer(newUrl);
                    }
                    else
                    {
                        //fileDownload(fileName1);
                        // Session["url"] += "/" + fileName1;


                        String[] arr;
                        //  Char[] a = { '*', '|', '*' };
                        arr = fileName2.Split('/');
                        //string p = string.Join("/", arr);

                        for (int i = arr.Length - 1; i > 0; i--)
                        {
                            url1 = arr[i];
                            break;
                        }
                        // Response.Write("<script>alert('" + url1 + "')</script>");
                        //Class1.DownloadFromServer(fileName2,url1);
                        DownloadFromServer(fileName2);
                    }


            }
            
        }
        else if (e.CommandName.Equals("xDelete"))
        {
            String urlBind = "";
            String path = e.CommandArgument.ToString();
            String searchUrl = "";
            int status = int.Parse(Session["searchStatus"].ToString());
            if (status == 1)
            {
                //  searchUrl = getUrl(path);
                //  Session["url"] = searchUrl;
                ////  String u = "";
                //  String[] str = searchUrl.Split('\\');
                //  for (int i = 0;i<str.Length-2;i++)
                //  {
                //      urlBind += str[i] + "/";

                //  }
                // Session["url"] += "/";
                //GridView1.AllowPaging = true;
                String u = "";
                Hashtable ht1 = new Hashtable();
                ht1 = Session["hashtable"] as Hashtable;
                IDictionaryEnumerator e1 = ht1.GetEnumerator();
                while (e1.MoveNext())
                {
                    if (e1.Key.ToString().Equals(path))
                    {
                        searchUrl = e1.Value.ToString();
                        Session["searchStatus"] = 0;
                        ht1.Remove(e1.Key.ToString());
                        break;
                    }

                }
                Session["hashtable"] = ht1;
                Session["url"] = searchUrl;
                String folpath = Session["url"].ToString();
                if (Directory.Exists(folpath))
                {

                    DeleteDirectory(folpath);
                }
                else if (File.Exists(folpath))
                {
                    File.Delete(folpath);
                }
                else
                {
                    Response.Write("<script>alert('No such file or directory')</script>");
                }
                List<Class1> files = new List<Class1>();
                Hashtable ht = new Hashtable();
                ht = Session["hashtable"] as Hashtable;
                IDictionaryEnumerator e2 = ht.GetEnumerator();
                while (e2.MoveNext())
                {
                    String check = Path.GetExtension(e2.Value.ToString());
                    if (check.Equals(""))
                    {
                        string result = System.IO.Path.GetFileName(e2.Value.ToString());
                        String imageUrl = "~/Images/iconfolder.png";
                        float ksize = calculateFolderSize(e2.Value.ToString());
                        float fsize = ksize / 1024;
                        files.Add(new Class1() { imgUrl = imageUrl, fileName = result, fileLastModified = Directory.GetLastAccessTime(e2.Value.ToString()).ToString(), fileType = "", fileSize = fsize });
                    }
                    else
                    {

                        // string result = System.IO.Path.GetFileName(filePath);
                        string fileName = Path.GetFileName(e2.Value.ToString());
                        String u1 = "";
                        String ext = Path.GetExtension(e2.Value.ToString());
                        String[] a1 = ext.Split('.');
                        if (a1[1].Equals("txt"))
                        {
                            u = @"~\Images\txt.png";
                        }
                        else if ((a1[1].Equals("jpg")) || (a1[1].Equals("png")))
                        {
                            u = @"~\Images\img.png";
                        }
                        else if (a1[1].Equals("pdf"))
                        {
                            u = @"~\Images\pdficon.jpg";
                        }
                        else if (a1[1].Equals("pptx"))
                        {
                            u = @"~\Images\ppticon.png";
                        }
                        else if (a1[1].Equals("docx"))
                        {
                            u = @"~\Images\docxicon.png";
                        }
                        else if (a1[1].Equals("bmp"))
                        {
                            u = @"~\Images\bmpicon.png";
                        }
                        else if (a1[1].Equals("rar"))
                        {
                            u = @"~\Images\raricon.png";
                        }
                        else
                        {
                            u = @"~\Images\iconfolder.png";
                        }
                        FileInfo f = new FileInfo(e2.Value.ToString());
                        float len1 = f.Length;
                        float len = len1 / 1024;
                        files.Add(new Class1() { imgUrl = u1, fileName = fileName, fileLastModified = File.GetLastAccessTime(e2.Value.ToString()).ToString(), fileType = a1[1], fileSize = len });

                    }
                }
                GridView1.DataSource = files;
                GridView1.DataBind();

                Session["hashtable"] = ht;

            }
            else
            {
                urlBind = Session["url"].ToString();
             //   Session["url"] += "/" 
                String folpath = Session["url"].ToString() + "/" + path;
                if (Directory.Exists(folpath))
                {

                    DeleteDirectory(folpath);
                }
                else if (File.Exists(folpath))
                {
                    File.Delete(folpath);
                }
                else
                {
                    Response.Write("<script>alert('No such file or directory')</script>");
                }
                bindDataList(urlBind);
            }


        }
        else if (e.CommandName.Equals("xView"))
        {
            String fileName1 = "";
            fileName1 = e.CommandArgument.ToString();
            String viewUrl = Session["url"].ToString();
            Session["viewPath"] = viewUrl + "/" + fileName1;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ViewPage.aspx','_newtab');", true);
        }
        else if (e.CommandName.Equals("xShare"))
        {
            Session["shareUrl"] = Session["url"].ToString();
            Session["shareFname"] = e.CommandArgument.ToString();
            Response.Redirect("~/SharePage.aspx");
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('SharePage.aspx','_newtab');", true);
        }
    }

    
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    public void DownloadFromServer(String fileName)
    {
        String zipfile = fileName;
        string filePath = fileName;
        if (!File.Exists(fileName))
        {
            Response.Write("<script>alert('Please Zip Folder and then Download Zip Folder..!!')</script>");
        }
        else
        {
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment;filename=" + url1);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        
    }
    public void DecryptFile(String filename1)
    {
        string fileEncrypted = @"G:\Ayushi_6_4\SDMS\DashBoard\" + Session["uname"].ToString() + "\\" + Session["uname"].ToString() + "\\SecureItems\\" + filename1;
        string password = "abcd1234";
        url1 = filename1;
        byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

        string file = @"G:\Ayushi_6_4\SDMS\temp\" + filename1;
        File.WriteAllBytes(file, bytesDecrypted);
        DownloadFromServer(file);
    }
    public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
    public static HttpResponse GetHttpResponse()
    {
        return HttpContext.Current.Response;
    }
    public void bindDataList(String url)
    {
        //Display Folder
        string[] filePaths = Directory.GetDirectories(url);
        List<Class1> files = new List<Class1>();

        foreach (string filePath in filePaths)
        {
            string result = System.IO.Path.GetFileName(filePath);
            String imageUrl = "~/Images/iconfolder.png";
            float ksize = calculateFolderSize(filePath);
            float fsize = ksize / 1024;
            files.Add(new Class1() { imgUrl = imageUrl, fileName = result, fileLastModified = Directory.GetLastAccessTime(filePath).ToString(), fileType = "", fileSize = fsize });

            //  ht.Add(Path.GetFileName(filePath), filePath);

        }

        string[] content = Directory.GetFiles(url + "/");
        // List<ListItem> con = new List<ListItem>();
        foreach (string filePath in content)
        {
            Path.GetExtension(filePath);
            // string result = System.IO.Path.GetFileName(filePath);
            string fileName = Path.GetFileName(filePath);
            String u;
            String ext = Path.GetExtension(filePath);
            String[] a1 = ext.Split('.');
            if (a1[1].Equals("txt"))
            {
                u = @"~\Images\txt.png";
            }
            else if ((a1[1].Equals("jpg")) || (a1[1].Equals("png")))
            {
                u = @"~\Images\img.png";
            }
            else if (a1[1].Equals("pdf"))
            {
                u = @"~\Images\pdficon.jpg";
            }
            else if (a1[1].Equals("pptx"))
            {
                u = @"~\Images\ppticon.png";
            }
            else if (a1[1].Equals("docx"))
            {
                u = @"~\Images\docxicon.png";
            }
            else if (a1[1].Equals("bmp"))
            {
                u = @"~\Images\bmpicon.png";
            }
            else if (a1[1].Equals("zip"))
            {
                u = @"~\Images\raricon.png";
            }
            else
            {
                u = @"~\Images\iconfolder.png";
            }
            FileInfo f = new FileInfo(filePath);
            float len1 = f.Length;
            float len = len1 / 1024;
            files.Add(new Class1() { imgUrl = u, fileName = fileName, fileLastModified = File.GetLastAccessTime(filePath).ToString(), fileType = a1[1], fileSize = len });

            //  ht.Add(Path.GetFileName(filePath), filePath);

        }
        GridView1.DataSource = files;
        GridView1.DataBind();

        //treeview update
        TreeView1.Nodes.Clear();
        TreeView1.Attributes.Add("onclick", "return OnTreeClick(event)");
        DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/Dashboard/" + Session["uname"].ToString() + "/"));
        this.PopulateTreeView(rootInfo, null);
        TreeView1.ExpandAll();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        String bindUrl = "";
        Session["cpaste"] = "1";
        String cbpath = "";
        Session["id"] = "";
        Session["checkboxPath"] = "";
        Session["source"] = "";
        Session["dest"] = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            Hashtable ht1 = new Hashtable();
            ht1 = Session["hashtable"] as Hashtable;
            if (((System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBox1")).Checked == true)
            {
                Session["id"] = ((Label)row.FindControl("Label1")).Text;
              //  Response.Write("<script>alert(Id : '" + Session["id"].ToString() + "')</script>");
                IDictionaryEnumerator e1 = ht1.GetEnumerator();
                while (e1.MoveNext())
                {
                    if (Session["id"].ToString() == e1.Key.ToString())
                    {
                        cbpath = e1.Value.ToString();
                        Session["PasteFileName"] = e1.Key.ToString();
                        Session["checkboxPath"] = cbpath;
                        String u = "";
                        if (File.Exists(cbpath))
                        {
                            String[] str = cbpath.Split('/');
                            for (int i = 0; i < str.Length - 1; i++)
                            {
                                u += str[i] + "/";

                            }
                            Session["source"] += cbpath + ",";
                            btnPaste.Enabled = true;
                            //  bindDataList(cbpath);
                        }
                        else
                        {
                            btnPaste.Enabled = true;
                            String[] arr = cbpath.Split(new[] { "\\" }, StringSplitOptions.None);
                            bindUrl = arr[0];
                            String path = arr[0] + "/" + arr[1];
                            Session["source"] = path + ",";
                            Session["folderName"] = arr[1];
                        }
                        Session["dest"] += Session["checkboxPath"].ToString() + ",";
                        // bindDataList(bindUrl);
                    }
                }
            }
        }

    }
    protected void btnPaste_Click(object sender, EventArgs e)
    {
        String src = Session["source"].ToString();
        String[] arr = src.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (String s in arr)
        {
            if (File.Exists(s))
            {
                File.Copy(s, Session["MoveHere"].ToString() + "/" + Session["PasteFileName"].ToString());
                //File.Copy(
            }
            else
            {

                String path1 = Session["MoveHere"].ToString() + "/" + Session["folderName"].ToString();
                DirectoryCopy(s, path1, true);
            }


        }

        Session["cpaste"] = "0";
        bindDataList(Session["MoveHere"].ToString());
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

    protected void btnZip_Click(object sender, EventArgs e)
    {
        String bindurl = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            
            Session["id"] = ((Label)row.FindControl("Label1")).Text;
            String cbpath = "";
            Hashtable ht1 = new Hashtable();
            ht1 = Session["hashtable"] as Hashtable;
            if (((System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBox1")).Checked == true)
            {
                IDictionaryEnumerator e1 = ht1.GetEnumerator();
                while (e1.MoveNext())
                {
                    if (Session["id"].ToString() == e1.Key.ToString())
                    {
                        cbpath = e1.Value.ToString();
                        Session["checkboxPath"] = cbpath;
                        String u = "";
                        if (File.Exists(cbpath))
                        {
                            Response.Write("<script>alert('File Cannot be Zip')</script>");
                        }
                        else
                        {
                            String[] arr = cbpath.Split(new[] { "\\" }, StringSplitOptions.None);
                            String path = arr[0] + "/" + arr[1];
                            Session["source"] = path;
                            bindurl = arr[0];
                            
                            ZipFile.CreateFromDirectory(Session["source"].ToString(), Session["source"].ToString() + "Tmp.zip");
                            break;
                        }
                    }
                }
            }



            //            ZipFile.CreateFromDirectory("C:\SEM6\Projects\Ayushi_27_3\SDMS\DashBoard\Bhakti\Bhakti\Flash\BarryAllen", "C:\SEM6\Projects\Ayushi_27_3\SDMS\DashBoard\Bhakti\Bhakti\Flash\BarryAllenTemp" + ".zip");
        }

        bindDataList(bindurl);
    }
    protected void btnMoveTo_Click(object sender, EventArgs e)
    {
        String bindUrl = "";
        Session["count"] = "1";
        String cbpath = "";
        Session["id"] = "";
        Session["checkboxPath"] = "";
        Session["source"] = "";
        Session["dest"] = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            Hashtable ht1 = new Hashtable();
            ht1 = Session["hashtable"] as Hashtable;
            if (((System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBox1")).Checked == true)
            {
                Session["id"] = ((Label)row.FindControl("Label1")).Text;
               // Response.Write("<script>alert(Id : '" + Session["id"].ToString() + "')</script>");
                IDictionaryEnumerator e1 = ht1.GetEnumerator();
                while (e1.MoveNext())
                {
                    if (Session["id"].ToString() == e1.Key.ToString())
                    {
                        cbpath = e1.Value.ToString();
                        Session["MoveFileName"] = e1.Key.ToString();
                        Session["checkboxPath"] = cbpath;
                        String u = "";
                        if (File.Exists(cbpath))
                        {
                            String[] str = cbpath.Split('/');
                            for (int i = 0; i < str.Length - 1; i++)
                            {
                                u += str[i] + "/";

                            }
                            Session["source"] += cbpath + ",";
                            btnMoveHere.Enabled = true;

                        }
                        else
                        {
                            btnMoveHere.Enabled = true;
                            String[] arr = cbpath.Split(new[] { "\\" }, StringSplitOptions.None);
                            bindUrl = arr[0];
                            String path = arr[0] + "/" + arr[1];
                            Session["source"] = path + ",";
                            Session["folderName"] = arr[1];
                        }
                        Session["dest"] += Session["checkboxPath"].ToString() + ",";
                    }
                }
            }
        }
        //bindDataList(bindUrl);
    }
    protected void btnMoveHere_Click(object sender, EventArgs e)
    {
        String src = Session["source"].ToString();
        String[] arr = src.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (String s in arr)
        {
            if (File.Exists(s))
            {
                File.Copy(s, Session["MoveHere"].ToString() + "/" + Session["MoveFileName"].ToString());
            }
            else
            {

                String path1 = Session["MoveHere"].ToString() + "/" + Session["folderName"];
                DirectoryCopy(s, path1, true);
            }

            if (Directory.Exists(s))
            {
                Directory.Delete(s, true);
            }
            else if (File.Exists(s))
            {
                File.Delete(s);
            }
            else
            {
                Response.Write("<script>alert('No such file or directory')</script>");
            }
        }

        Session["count"] = "0";
        bindDataList(Session["MoveHere"].ToString());
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        bindDataList(Session["url"].ToString());
    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;
         int temp = 0;
         String search = Request.Form["txtSearch"];
       //  String dirUrl = Session["url"].ToString();
         string[] filePaths = Directory.GetDirectories(Server.MapPath("~/Dashboard/" + Session["uname"].ToString()),search+"*",SearchOption.AllDirectories);
         List<Class1> files = new List<Class1>();

         foreach (string filePath in filePaths)
         {
            
             string result = System.IO.Path.GetFileName(filePath);
           
                    String imageUrl = "~/Images/iconfolder.png";
                    float ksize = calculateFolderSize(filePath);
                    float fsize = ksize / 1024;
                     files.Add(new Class1() { imgUrl = imageUrl, fileName = result, fileLastModified = Directory.GetLastAccessTime(filePath).ToString(), fileType = "", fileSize = fsize });

                     ht.Add(Path.GetFileName(filePath), filePath);
                 temp=1;
           
         }

         string[] content = Directory.GetFiles(Server.MapPath("~/Dashboard/Bhakti"),search+"*",SearchOption.AllDirectories);
         // List<ListItem> con = new List<ListItem>();
         foreach (string filePath in content)
         {
             Path.GetExtension(filePath);
             // string result = System.IO.Path.GetFileName(filePath);
             string fileName = Path.GetFileName(filePath);
             String u;
             String ext = Path.GetExtension(filePath);
             String[] a1 = ext.Split('.');
             if (a1[1].Equals("txt"))
             {
                 u = @"~\Images\txt.png";
             }
             else if ((a1[1].Equals("jpg")) || (a1[1].Equals("png")))
             {
                 u = @"~\Images\img.png";
             }
             else if(a1[1].Equals("pdf"))
             {
                 u = @"~\Images\pdficon.jpg";
             }
             else if (a1[1].Equals("pptx"))
             {
                 u = @"~\Images\ppticon.png";
             }
             else if (a1[1].Equals("docx"))
             {
                 u = @"~\Images\docxicon.png";
             }
             else if (a1[1].Equals("bmp"))
             {
                 u = @"~\Images\bmpicon.png";
             }
             else if (a1[1].Equals("rar"))
             {
                 u = @"~\Images\raricon.png";
             }
             else
             {
                 u = @"~\Images\iconfolder.png";
             }
             FileInfo f = new FileInfo(filePath);
             float len1 = f.Length;
             float len = len1/1024;
             files.Add(new Class1() { imgUrl = u, fileName = fileName, fileLastModified = File.GetLastAccessTime(filePath).ToString(), fileType = a1[1], fileSize = len });

             ht.Add(Path.GetFileName(filePath), filePath);
             temp = 1;
         }
         // sw.Flush();
         // fs.Close();

        
         if (temp == 1)
         {
             GridView1.AllowPaging = true;
             GridView1.DataSource = files;
             GridView1.DataBind();
           //  GridView1.AllowPaging = true;
             Session["hashtable"] = ht;
             Session["searchStatus"] = 1;
         }

         if (temp == 0)
         {
             Response.Write("<script>alert('No such File or Directory Found..!!')</script>");
             GridView1.AllowPaging = true;
         }
       }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        string[] content = Directory.GetFiles("G:/Ayushi_6_4/SDMS/temp" + "/");
        // List<ListItem> con = new List<ListItem>();
        foreach (string filePath in content)
        {
            File.Delete(filePath);
        }
        Session["uname"] = "";
        Response.Redirect("~/loginForm.aspx");
    }
    protected void btnViewProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ViewProfile.aspx");
    }
    //User Defined Function
    protected void btnUpload_Click(object sender, EventArgs e)
    {
       // Response.Write("<script>alert(Too Large..!!)</script>");
        String fname = FileUpload1.FileName;
        bool chkfile = FileUpload1.HasFile;
        if (chkfile == true)
        {
            int filesize = FileUpload1.PostedFile.ContentLength;
            if (filesize > (1024 * 1024 * 1024))
            {
                Response.Write("<script>alert(Too Large..!!)</script>");
            }
            FileUpload1.SaveAs(Session["uploadpath"].ToString() + "/" + FileUpload1.FileName);
            bindDataList(Session["uploadpath"].ToString());
            
            /*
             * if (FileUpload1.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.FileName);

                FileUpload1.SaveAs(Session["newFolPath"].ToString() + "/" + filename);
             //   lbluploadStatus.Text = "Upload status: File uploaded!";
                bindDataList(Session["newFolPath"].ToString());
                TreeView1.Nodes.Clear();
                TreeView1.Attributes.Add("onclick", "return OnTreeClick(event)");
                DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/Dashboard/" + Session["uname"].ToString() + "/"));
                this.PopulateTreeView(rootInfo, null);
                TreeView1.ExpandAll();
            }
            catch (Exception ex)
            {
              //  lbluploadStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
             * */
        }
        else
        {
            Response.Write("<script>alert('No File Selected')</script>");
        }


    }
    protected void btnNewFolder_Click(object sender, EventArgs e)
    {
        String uname = Session["uname"].ToString();
        string folderPath = Session["newFolPath"].ToString() + "/" + Request.Form["txtSearch"];

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
            FileStream fs = new FileStream(@"G:\Ayushi_6_4\SDMS\xy.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            DirectoryInfo dinfo = new DirectoryInfo(folderPath); // Populates field with all Sub Folders
            FileInfo[] Files = dinfo.GetFiles("*.sto");
            foreach (FileInfo file in Files)
            {
                sw.WriteLine(file.Name);
             }



            sw.Flush();
            fs.Close();
            bindDataList(Session["newFolPath"].ToString());

        }
        else
        {
            Response.Write("<script>alert('File with this name already exists')</script>");
        }
    }
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        EncryptFile();
    }
    
    public void EncryptFile()
    {
        string cbpath="";
        String[] arr = {""};
        String u = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            Hashtable ht1 = new Hashtable();
            ht1 = Session["hashtable"] as Hashtable;
            if (((System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBox1")).Checked == true)
            {
                Session["id"] = ((Label)row.FindControl("Label1")).Text;
               // Response.Write("<script>alert(Id : '" + Session["id"].ToString() + "')</script>");
                IDictionaryEnumerator e1 = ht1.GetEnumerator();
                while (e1.MoveNext())
                {
                    if (Session["id"].ToString() == e1.Key.ToString())
                    {
                        cbpath = e1.Value.ToString();
                        //arr = cbpath.Split(new[] { "\\" }, StringSplitOptions.None);
                        if (File.Exists(cbpath))
                        {
                            String[] str = cbpath.Split('/');
                            for (int i = str.Length - 1; i > 0; i--)
                            {
                                u = str[i];
                                break;
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('FOlder cannot be decrypted')</script>");
                        }
                        
                    }
                }
            }
        }


        string file = cbpath;
        string password = "abcd1234";

        byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
        String filen = "";
        string fileEncrypted = "";
        String n = Path.GetFileName(cbpath);
            String[] name = n.Split('.');
            name[0] += "_Encrypt";
            filen = name[0] + "." + name[1];
            String path1 = @"G:\Ayushi_6_4\SDMS\DashBoard\" + Session["uname"].ToString() + "\\" + Session["uname"].ToString() + "\\SecureItems\\";
            fileEncrypted = path1 + filen;
        

        File.WriteAllBytes(fileEncrypted, bytesEncrypted);
    }
    public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

    protected void btnSort_Click(object sender, ImageClickEventArgs e)
    {
        String refreshPath = Session["sortPath"].ToString();
        GridView1.AllowSorting = false;
        bindDataList(refreshPath);
    }
    protected void btnTheme_Click(object sender, ImageClickEventArgs e)
    {
        Session["themeStatus"] = "1";
        Response.Redirect("~/ThemeChange.aspx");
    }
}