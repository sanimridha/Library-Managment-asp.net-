using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
                FillAuthorPublisherValues();
            }
                
            
        }

        protected void LinkButton4_Click(object sender, EventArgs e)  //go button
        {
            getBookByID();
        }

        protected void Button1_Click(object sender, EventArgs e)  //add button
        {
            if (txtBookId.Text.Equals(""))
            {
                Response.Write("<script>alert('Enter Book Id First.')</script>");
            }
            else
            {
                if (checkIfBookExists())
                {
                    Response.Write("<script>alert('Book Already Exists, try some other Book ID');</script>");
                }
                else
                {
                    AddNewBook();
                }
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)  //update button
        {
            updateBookByID();
        }

        protected void Button3_Click(object sender, EventArgs e)  //delete button
        {
            deleteBookByID();
        }
        void FillAuthorPublisherValues()
        {
            
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT author_name from author_master_tbl", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DropDownList3.DataSource = dt;
                    DropDownList3.DataValueField = "author_name";
                    DropDownList3.DataBind();

                    cmd = new SqlCommand("SELECT publisher_name from publisher_master_tbl", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    DropDownList2.DataSource = dt;
                    DropDownList2.DataValueField = "publisher_name";
                    DropDownList2.DataBind();

                }
                catch (Exception ex)
                {

                }

            //    string constr = ConfigurationManager.ConnectionStrings["con"].ToString(); // connection string
            //    SqlConnection con = new SqlConnection(strcon);
            //    con.Open();

            //    SqlCommand com = new SqlCommand("select *from author_master_tbl", con); // table name 
            //    SqlDataAdapter da = new SqlDataAdapter(com);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);  // fill dataset
            //    DropDownList3.DataTextField = ds.Tables[0].Columns["author_name"].ToString(); // text field name of table dispalyed in dropdown
            //    DropDownList3.DataValueField = ds.Tables[0].Columns["author_id"].ToString();             // to retrive specific  textfield name 
            //    DropDownList3.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //    DropDownList3.DataBind();  //binding dropdownlist

            //    com = new SqlCommand("select *from publisher_master_tbl", con); // table name 
            //    da = new SqlDataAdapter(com);
            //    ds = new DataSet();
            //    da.Fill(ds);  // fill dataset
            //    DropDownList2.DataTextField = ds.Tables[0].Columns["publisher_name"].ToString(); // text field name of table dispalyed in dropdown
            //    DropDownList2.DataValueField = ds.Tables[0].Columns["publisher_id"].ToString();             // to retrive specific  textfield name 
            //    DropDownList2.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //    DropDownList2.DataBind();  //binding dropdownlist

            //}
            //catch (Exception ex)
            //{

                
            //}
        }
        
        void AddNewBook()
        {
            try
            {
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                // genres = Adventure,Self Help,
                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl(book_id,book_name,genre,author_name,publisher_name,publisher_date,language,edition,book_cost,no_of_pages,Book_description,actual_stock,current_stock,book_img_link) " +
                    "values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)", con);
                cmd.Parameters.AddWithValue("@0", txtBookId.Text.Trim());
                cmd.Parameters.AddWithValue("@1", txtBookName.Text.Trim());
                cmd.Parameters.AddWithValue("@2", genres);
                cmd.Parameters.AddWithValue("@3", DropDownList3.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@4", DropDownList2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@5", txtPublishDate.Text.Trim());
                cmd.Parameters.AddWithValue("@6", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@7", txtEdition.Text.Trim());
                cmd.Parameters.AddWithValue("@8", txtBookCost.Text.Trim());
                cmd.Parameters.AddWithValue("@9", txtPages.Text.Trim());
                cmd.Parameters.AddWithValue("@10", txtBookDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@11", txtActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@12", txtActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@13", filepath);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Book Added Successfully.')</script>");
                GridView1.DataBind();
                clearform();

            }
            catch (Exception ex)
            {


            }

        }
        void getBookByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from book_master_tbl WHERE book_id='" + txtBookId.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    txtBookName.Text = dt.Rows[0]["book_name"].ToString();
                    txtPublishDate.Text = dt.Rows[0]["publisher_date"].ToString();
                    txtEdition.Text = dt.Rows[0]["edition"].ToString();
                    txtBookCost.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    txtPages.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    txtActualStock.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    txtCurrentStock.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    txtBookDescription.Text = dt.Rows[0]["book_description"].ToString();
                    txtIssuedBook.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));

                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();

                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;

                            }
                        }
                    }

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();

                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID');</script>");
                }

            }
            catch (Exception ex)
            {

            }
        }
        bool checkIfBookExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from book_master_tbl where book_id='" + txtBookId.Text.Trim() + "' OR book_name='" + txtBookName.Text.Trim() + "';", con);
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
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void updateBookByID()
        {

            if (checkIfBookExists())
            {
                try
                {

                    int actual_stock = Convert.ToInt32(txtActualStock.Text.Trim());
                    int current_stock = Convert.ToInt32(txtCurrentStock.Text.Trim());

                    if (global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if (actual_stock < global_issued_books)
                        {
                            Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            return;


                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            txtCurrentStock.Text = "" + current_stock;
                        }
                    }

                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);

                    string filepath = "~/book_inventory/books1";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = global_filepath;

                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                        filepath = "~/book_inventory/" + filename;
                    }

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publisher_date=@publisher_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id='" + txtBookId.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@book_name", txtBookName.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_date", txtPublishDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", txtEdition.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_cost", txtBookCost.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", txtPages.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", txtBookDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                    cmd.Parameters.AddWithValue("@book_img_link", filepath);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Updated Successfully');</script>");
                    clearform();


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID');</script>");
            }
        }
        void deleteBookByID()
        {
            if (checkIfBookExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from book_master_tbl WHERE book_id='" + txtBookId.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Book Deleted Successfully');</script>");

                    GridView1.DataBind();
                    clearform();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }
        void clearform()
        {
            txtBookId.Text = "";
            txtBookName.Text = "";
            ListBox1.SelectedValue = null;
            txtPublishDate.Text = "";
            txtPages.Text = "";
            txtIssuedBook.Text = "";
            txtEdition.Text = "";
            txtCurrentStock.Text = "";
            txtBookDescription.Text = "";
            txtActualStock.Text = "";
            txtBookCost.Text = "";
            

        }

    }
}