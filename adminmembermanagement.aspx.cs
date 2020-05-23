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
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void LinkButton4_Click(object sender, EventArgs e) //go button
        {
            if (txtMemberId.Text.Trim().Equals(""))
            {
                Response.Write("<script>alert('Please Enter Member ID first')</script>");
            }
            else
            {
                GetMemberById();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)  //active button
        {
            ChangeAccountStatusById("active");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)  //pending button
        {
            ChangeAccountStatusById("pending");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)  //disable button
        {
            ChangeAccountStatusById("disable");
        }

        protected void Button1_Click(object sender, EventArgs e)  //delete user permanently
        {
            DeleteAuthorById();
        }
        void GetMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id = '" + txtMemberId.Text.Trim() + "'; ", con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtFullName.Text = dr.GetValue(0).ToString();
                        txtStatus.Text = dr.GetValue(10).ToString();
                        txtDOB.Text = dr.GetValue(1).ToString();
                        txtContactNo.Text = dr.GetValue(2).ToString();
                        txtEmailId.Text = dr.GetValue(3).ToString();
                        txtState.Text = dr.GetValue(4).ToString();
                        txtCity.Text = dr.GetValue(5).ToString();
                        txtPinCode.Text = dr.GetValue(6).ToString();
                        txtFullAddress.Text = dr.GetValue(7).ToString();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Incorrect Member ID!')</script>");
                    
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");

            }
        }
        void ChangeAccountStatusById(string status)
        {
            if (CheckIsMemberExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status='" + status + "' where member_id='" + txtMemberId.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    txtStatus.Text = status;
                    con.Close();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('Please Enter valid Member ID first')</script>");
            }
            
        }
        void DeleteAuthorById()
        {
            if (txtMemberId.Text.Trim().Equals(""))
            {
                Response.Write("<script>alert('Member ID Can not be Blank.')</script>");
            }
            else
            {
                if (CheckIsMemberExist())
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(strcon);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("DELETE from member_master_tbl WHERE member_id='" + txtMemberId.Text.Trim() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        GridView1.DataBind();
                        clearTextBox();
                        Response.Write("<script>alert('Member Deleted Succesfully.')</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please Enter valid Member ID first')</script>");
                }
                
            }
            

        }
        bool CheckIsMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + txtMemberId.Text.Trim() + "'", con);
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
        void clearTextBox()
        {
            txtMemberId.Text = "";
            txtFullName.Text = "";
            txtStatus.Text = "";
            txtDOB.Text = "";
            txtContactNo.Text = "";
            txtEmailId.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtPinCode.Text = "";
            txtFullAddress.Text = "";
        }
    }
}