using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)  //sign up button
        {
            //if(TextBox1.Text.Equals("") && TextBox2.Text.Equals("") && TextBox3.Text.Equals("") && TextBox4.Text.Equals("") && TextBox5.Text.Equals("") && TextBox6.Text.Equals("") && TextBox7.Text.Equals("") && TextBox8.Text.Equals("") && TextBox9.Text.Equals(""))
            //{
            //    Response.Write("<script>alert('Please Fill All the Text Boxes.');</script>");

            //}

            if (TextBox9.Text.Equals("")) { 
                Response.Write("<script>alert('Please Fill All the Text Boxes.');</script>");
            }
            else
            {
                if (CheckMemberExist())
                {
                    Response.Write("<script>alert('Member Already Exist, Please try with different Username');</script>");
                }
                else
                {
                    SignUpnewMember();
                }
            }
            
            

        }
        bool CheckMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + TextBox8.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        void SignUpnewMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl (full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) Values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)", con);
                cmd.Parameters.AddWithValue("@0", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@1", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@2", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@3", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@4", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@5", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@6", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@7", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@8", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@9", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@10", "pending");

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successful. Go to User Login To Login');</script>");
                ClearTextBox();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void ClearTextBox()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            DropDownList1.SelectedValue = null;
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox1.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
        }
        
    }
}