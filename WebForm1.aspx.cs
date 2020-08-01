using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*Oluwaseun Talabi
 * 4/11/2020
 * asurite:ogtalabi
 * Visual studio version 2019
 */
namespace combined_webform
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

                HttpCookie myCookies = Request.Cookies["cse445"];
                if ((myCookies == null) || (myCookies["firstName"] == ""))
                {
                    Label13.Text = "Welcome, new user";


                }
                else
                {
                Label14.Text = "Welcome, " + myCookies["firstName"];
                 Label15.Text = " " + myCookies["lastName"];


                }
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {//news focus api
            //store user input for rest call
            string topic = this.TextBox1.Text;
            //combine user input with url
            string url = "http://webstrar12.fulton.asu.edu/page2/Service1.svc/search/" + topic;
            //declare a string list to store the output into later
            List<String> result = new List<string>();
           

            //make a web request to get a response
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            JsonTextReader reader = new JsonTextReader(responseReader);
            //itterate through the response
            while (reader.Read())
            {
                //store the value in k to test
                string k = (string)reader.Value;
                //make sure it doesnt return a link to an image and its not null
                if (reader.Value != null && !k.Contains("png"))
                {//store the value into an array
                    result.Add((string)reader.Value);
                }
            }

            //result = client.getNews(topic);
            try
            {
                this.Label1.Text = result[0];
                this.Label2.Text = result[1];
                this.Label3.Text = result[2];
                this.Label4.Text = result[3];
            }
            catch (Exception)
            {//if no results were found give the user a message
                this.Label1.Text = "There were no search results for " + topic;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {//store user input for api call
            string zipcode = this.TextBox2.Text;
            //add user input into url
            string url = "http://webstrar12.fulton.asu.edu/page1/Service1.svc/Forecast/" + zipcode;
          
            //make a web request to get a response
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //input will be combined with $ so we need to split it so we get all the individual outputs
            String[] items = reader.ReadToEnd().Split('$');
            //get the date and time for today
            DateTime thisDay = DateTime.Today;
            //display the output
            Label5.Text = thisDay.AddDays(0).ToString("d") + " " + items[0] + " °F";
            Label6.Text = thisDay.AddDays(1).ToString("d") + " " + items[1] + " °F";
            Label7.Text = thisDay.AddDays(2).ToString("d") + " " + items[2] + " °F";
            Label8.Text = thisDay.AddDays(3).ToString("d") + " " + items[3] + " °F";
            Label9.Text = thisDay.AddDays(4).ToString("d") + " " + items[4] + " °F";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //store user input
            string zip = this.TextBox3.Text;
            //add user input into url
            string url = "http://webstrar12.fulton.asu.edu/page3/Service1.svc/solarcalc/" + zip;

            //make a web request to get output
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            //store the response into a string
            string decoded = reader.ReadToEnd();

            int i = 0;
            //results will be combined with $ symbol so we have to split them up to get the individulas
            String[] result = decoded.Split('$');
    
            //removed unnecesary symbols from the output to get the raw data and display
            this.Label10.Text = result[0].Trim(new Char[] { '"', '[', '\\' });
            this.Label11.Text = result[1].Trim(new Char[] { '"', '[', '\\' });
            this.Label12.Text = result[2].Trim(new Char[] { '"', '[', '\\' });
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }
    }
}