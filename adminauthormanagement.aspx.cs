using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)   //go button
        {
            GetAuthorById();
        }

        protected void Button1_Click(object sender, EventArgs e)  //add button
        {
            if(CheckIsauthorExist())
            {
                Response.Write("<script>alert('Already Author Exist Using This ID, Please try with different ID')</script>");
            }
            else
            {
                AddNewAuthor();
            }
            

        }

        protected void Button3_Click(object sender, EventArgs e)  //update button
        {
            if(CheckIsauthorExist())
            {
                UpdateAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author Does not Exist!')</script>");
            }
                    
            
        }

        protected void Button4_Click(object sender, EventArgs e)   //delete button
        {
            if (CheckIsauthorExist())
            {
                DeleteAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author Does not Exist!')</script>");
            }
        }

        bool CheckIsauthorExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox3.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
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
        void AddNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl (author_id,author_name) Values(@0,@1)", con);
                cmd.Parameters.AddWithValue("@0", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@1", TextBox4.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();
                //Response.Write("<script>alert('Author Added Succesfully');</script>");
                ClearTextBox();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void UpdateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name='"+TextBox4.Text.Trim()+"' WHERE author_id='" + TextBox3.Text.Trim()+"'", con);
                //cmd.Parameters.AddWithValue("@1", TextBox4.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Updated Succesfully');</script>");
                ClearTextBox();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void DeleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl WHERE author_id='" + TextBox3.Text.Trim() + "'", con);
                //cmd.Parameters.AddWithValue("@1", TextBox4.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Deleted Succesfully');</script>");
                ClearTextBox();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void GetAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox3.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox4.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid ID!')</script>");
                }
            }
            catch (Exception ex)
            {

                
            }
        }
        void ClearTextBox()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
        }
        
    }
}