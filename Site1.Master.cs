using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session["role"].Equals(""))
                {
                    LinkButton7.Visible = false;  //author management button
                    LinkButton8.Visible = false;  //publisher management button
                    LinkButton9.Visible = false;  //book inventory button
                    LinkButton10.Visible = false;  //book issueing
                    LinkButton11.Visible = false;  //member management
                    LinkButton4.Visible = false;  //logout button
                    LinkButton5.Visible = false;  //Hello User

                    LinkButton2.Visible = true;  //user login
                    LinkButton3.Visible = true;  //user signup
                    LinkButton6.Visible = true;   //admin login button
                }
                else if(Session["role"].Equals("user"))
                {
                    LinkButton7.Visible = false;  //author management button
                    LinkButton8.Visible = false;  //publisher management button
                    LinkButton9.Visible = false;  //book inventory button
                    LinkButton10.Visible = false;  //book issueing
                    LinkButton11.Visible = false;  //member management
                    LinkButton4.Visible = true;  //logout button
                    LinkButton5.Visible = true;  //hello user
                    LinkButton5.Text = "Hello "+Session["username"].ToString();  //Hello User

                    LinkButton2.Visible = false;  //userlogin
                    LinkButton3.Visible = false;  //usersignup
                    LinkButton6.Visible = true;  //adminlogin
                }
                else if (Session["role"].Equals("admin"))
                {
                    LinkButton7.Visible = true;  //author management button
                    LinkButton8.Visible = true;  //publisher management button
                    LinkButton9.Visible = true;  //book inventory button
                    LinkButton10.Visible = true;  //book issueing
                    LinkButton11.Visible = true;  //member management
                    LinkButton4.Visible = true;  //logout button
                    LinkButton5.Visible = true;  //hello user
                    LinkButton5.Text = "Hello Admin: " + Session["username"].ToString();  //Hello User

                    LinkButton2.Visible = false;  //userlogin
                    LinkButton3.Visible = false;  //usersignup
                    LinkButton6.Visible = false;  //adminlogin
                }
            
            }
            catch (Exception ex)
            {

                
            }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminauthormanagement.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminpublishermanagement.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookinventory.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookissuing.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminmembermanagement.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("usersignup.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewbooks.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)            //logout button
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            LinkButton7.Visible = false;  //author management button
            LinkButton8.Visible = false;  //publisher management button
            LinkButton9.Visible = false;  //book inventory button
            LinkButton10.Visible = false;  //book issueing
            LinkButton11.Visible = false;  //member management
            LinkButton4.Visible = false;  //logout button
            LinkButton5.Visible = false;  //Hello User

            LinkButton2.Visible = true;  //user login
            LinkButton3.Visible = true;  //user signup
            LinkButton6.Visible = true;   //admin login button
        }
    }
}