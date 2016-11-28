<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="InvestmentAdviser.Quiz" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Quiz</h2>
    <div style="text-align: center; width: 700px; margin: 0 auto;">
        <h3>Think carefully before you answer, you have only 3 chances to pass the quiz.</h3>
        <br />
        <table style="text-align: left; width: 700px;" border="1">
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>Choose the <font color="red"><b>Incorrect</b></font> sentence:
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Let's suppose you hired a worker with the rank #2.
                            <br />
                    How many prize points you will receive?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>90</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Assuming you have managed to accumulate 1000 prize points. What will be your bonus?
                        <asp:RadioButtonList ID="rbl3" runat="server">
                            <asp:ListItem>10 cents</asp:ListItem>
                            <asp:ListItem>40 cents</asp:ListItem>
                            <asp:ListItem>25 cents</asp:ListItem>
                            <asp:ListItem>20 cents</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnPrevToInstructions" runat="server" Text="Back To Instructions" OnClick="btnPrevToInstructions_Click" />
    <asp:Button ID="btnNextToProceedToGame" runat="server" Text="Next" OnClick="btnNextToProceedToGame_Click" />

</asp:Content>
