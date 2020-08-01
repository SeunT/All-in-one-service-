using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.IO;

namespace combined_webform
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["cse445"];
            if (myCookies != null)
            {
               
                Label1.Text = "Welcome back " + myCookies["firstName"];
            }

        }

     

        protected void Button1_Click(object sender, EventArgs e)
        {

            HttpCookie myCookies = Request.Cookies["cse445"];
            if (myCookies == null)
            {
                myCookies = new HttpCookie("cse445");
                myCookies.Expires = DateTime.Now.AddSeconds(60);

                myCookies["firstName"] = TextBox1.Text;
                myCookies["lastName"] = TextBox2.Text;

                Response.Cookies.Add(myCookies);
                Response.Redirect("WebForm1.aspx");
            }
            else
            {
                myCookies["firstName"] = TextBox1.Text;
                myCookies["lastName"] = TextBox2.Text;
                myCookies.Expires = DateTime.Now.AddSeconds(60);
                Response.Cookies.Add(myCookies);
                Response.Redirect("WebForm1.aspx");
            }
        }
        protected void Remove_Click(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["cse445"];
            if (myCookies != null)
            {
                myCookies["firstName"] = "";
                myCookies["lastName"] = "";
                Response.Cookies.Remove("cse445");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Cache["cachedData"] == null)
            {
                string d = File.ReadAllText(Server.MapPath("cacheData.txt"));
                Label3.Text = d;
              

                Cache.Insert("cachedData", d, null, DateTime.Now.AddSeconds(60), Cache.NoSlidingExpiration);

            }
            else
            {
                Label3.Text = Cache["cachedData"].ToString();

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Cache["cachedDataDepend"] == null)
            {
                string d = File.ReadAllText(Server.MapPath("cacheData.txt"));
                Label2.Text = d;
                Cache.Insert("cachedDataDepend", d, new CacheDependency(Server.MapPath("cacheData.txt")),
                DateTime.Now.AddSeconds(200), Cache.NoSlidingExpiration);



            }
            else
            {
                Label2.Text = Cache["cachedDataDepend"].ToString();
            }
        }
    }
}