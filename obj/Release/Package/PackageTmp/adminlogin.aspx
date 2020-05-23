<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="WebApplication1.adminlogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
        <div class="container"><br>
        <div class="row">
            <div class="col-md-6 mx-auto">
               
                <div class="card card text-white bg-danger mb-3"style="max-width: 30rem;">
                    <div class="card-body">
                        <div class="row">
                             
                            <div class="col">
                                <center>
                                    <img width="150px" src="img/adminuser.png" />
                                </center>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Admin Login</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <hr/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Admin ID"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>
                            <div class="form-group">
                                <asp:Button Class="btn btn-success btn-block btn-lg" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                </div>

                            </div>
                        </div>
                                
                         </div>
                        


                    </div>
                <a href="homepage.aspx"><< Back To Home</a><br><br>
                </div>

            </div>


        </div>
        
       
</asp:Content>
