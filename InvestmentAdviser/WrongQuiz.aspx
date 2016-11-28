<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WrongQuiz.aspx.cs" Inherits="InvestmentAdviser.WrongQuiz" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Quiz</h2>
    <div style="text-align: center; width: 640px; margin: 0 auto;">
        <table style="text-align: left; width: 640px;" border="1">
            <tr>
                <td>
                    <br />
                    <asp:Label ID="QuizWrongLbl" runat="server"></asp:Label>
                    <br />
                    <br />
                    &nbsp;The game is over, thank you for your time.
                            <br />
                    <br />
                    &nbsp;Please return the HIT.
                            <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
