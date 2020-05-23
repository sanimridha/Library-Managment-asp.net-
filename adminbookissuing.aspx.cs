using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class adminbookissuing : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)  //go button
        {
            GetNames();
        }

        protected void Button1_Click(object sender, EventArgs e)  //issue button
        {
            if(CheckIfMemberExist() && CheckIfBookExist())
            {
                if (txtBookName.Text.Equals("") && txtMemberName.Text.Equals(""))
                {
                    Response.Write("<script>alert('Please Select User By Pressing Go Button.')</script>");

                }
                else
                {
                    if (CheckIfIssueBookExist())
                    {
                        Response.Write("<script>alert('This Book Already taken By this User!!')</script>");
                    }
                    else
                    {
                        issueBook();
                    }
                }

            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or Member ID')</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)  //return book
        {
            if (CheckIfMemberExist() && CheckIfBookExist())
            {
                if (txtBookName.Text.Equals("") && txtMemberName.Text.Equals(""))
                {
                    Response.Write("<script>alert('Please Select User By Pressing Go Button.')</script>");

                }
                else
                {
                    if (CheckIfIssueBookExist())
                    {
                        returnBook();
                    }
                    else
                    {
                        Response.Write("<script>alert('this Entry does not Exist!!')</script>");
                    }
                }

            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or Member ID')</script>");
            }
        }
        void issueBook()
        {
            try
            {
                
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO book_issue_tbl (member_id,member_name,book_id,book_name,issue_date,due_date) " +
                    "VALUES (@member_id,@member_name,@book_id,@book_name,@issue_date,@due_date)", con);
                cmd.Parameters.AddWithValue("@member_id", txtMemberId.Text.Trim());
                cmd.Parameters.AddWithValue("@member_name", txtMemberName.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", txtBookId.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", txtBookName.Text.Trim());
                cmd.Parameters.AddWithValue("@issue_date", txtStartDate.Text.Trim());
                cmd.Parameters.AddWithValue("@due_date", txtEndDate.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE book_master_tbl SET current_stock = current_stock - 1 WHERE book_id = '"+txtBookId.Text.Trim()+"'",con);
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                Response.Write("<script>alert('Book Issued Succesfully.')</script>");
                ClearTextBoxes();

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
        }
        void returnBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM book_issue_tbl WHERE book_id='" + txtBookId.Text.Trim() + "' AND member_id='" + txtMemberId.Text.Trim() + "'", con);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    cmd = new SqlCommand("UPDATE book_master_tbl SET current_stock =current_stock+1 WHERE book_id='" + txtBookId.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Book Returned Succesfully.')</script>");
                    con.Close();
                    ClearTextBoxes();
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error- Invalid Details.. .')</script>");

            }
        }
        void GetNames()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //searching book by id
                SqlCommand cmd = new SqlCommand("SELECT book_name FROM book_master_tbl WHERE book_id='" + txtBookId.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1)
                {
                    txtBookName.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Incorrect Book ID!')</script>");
                }
                //searching member by id
                 cmd = new SqlCommand("SELECT full_name FROM member_master_tbl WHERE member_id='" + txtMemberId.Text.Trim() + "'", con);
                 da = new SqlDataAdapter(cmd);
                 dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    txtMemberName.Text = dt.Rows[0]["full_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Incorrect Member ID!')</script>");
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
        }
        bool CheckIfBookExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //searching book by id
                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id='" + txtBookId.Text.Trim() + "'AND current_stock >0", con);
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
        bool CheckIfMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //searching book by id
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + txtMemberId.Text.Trim() + "'", con);
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
        bool CheckIfIssueBookExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //searching book by id
                SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE member_id='" + txtMemberId.Text.Trim() + "' AND book_id='"+txtBookId.Text.Trim()+"'", con);
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
        void ClearTextBoxes()
        {
            txtBookId.Text = "";
            txtMemberId.Text = "";
            txtBookName.Text = "";
            txtMemberName.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    //check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if(today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.OrangeRed;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                
            }
        }
    }
}