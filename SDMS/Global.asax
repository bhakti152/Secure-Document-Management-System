<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Application["theme"] = "";
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Session["uname"] = "";
        Session["path"] = "0";
        Session["count"] = "0";
        Session["source"] = "";
        Session["arrayList"] = "";
        Session["backCount"] = "0";
        Session["newFolPath"] = "";
        Session["MoveHere"] = "";
        Session["hashtable"] = "";
        Session["folderName"] = "";
        Session["button"] = "";
        Session["searchStatus"] = 0;
        Session["uploadpath"] = "";
        Session["cpaste"] = "0";
        Session["MoveFileName"] = "";
        Session["viewPath"] = "";
        Session["shareUrl"] = "";
        Session["shareFname"] = "";
        Session["sortPath"] = "";
        Session["themeStatus"] = "0";
        Session["themeUrl"] = "";
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session["uname"] = "";
        Session["path"] = "0";
        Session["count"] = "0";
        Session["source"] = "";
        Session["arrayList"] = "";
        Session["backCount"] = "0";
        Session["newFolPath"] = "";
        Session["MoveHere"] = "";
        Session["hashtable"] = "";
        Session["folderName"] = "";
        Session["button"] = "";
        Session["cpaste"] = "0";
        Session["MoveFileName"] = "";
        Session["viewPath"] = "";
        Session["shareUrl"] = "";
        Session["shareFname"] = "";
        Session["sortPath"] = "";
              Session["themeStatus"] = "0";
        Session["themeUrl"] = "";
    }
       
</script>
