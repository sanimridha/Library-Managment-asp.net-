using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class adminlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from admin_login_tbl where username = '" + TextBox1.Text.Trim() + "' AND password = '" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        Response.Write("<script>alert('Hi...." + sqlDataReader.GetValue(2) + "')</script>");
                        Session["username"] = sqlDataReader.GetValue(0);
                        Session["fullname"] = sqlDataReader.GetValue(2);
                        Session["role"] = "admin";
                    }
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Incorrect Admin Username or password')</script>");
                }
            }
            
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                
            }
        }
    }
}