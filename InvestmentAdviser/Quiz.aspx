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
                            <asp:ListItem>Each turn, the agent invests 100$ in several stocks.</asp:ListItem>
                            <asp:ListItem>There are total of 10 investments.</asp:ListItem>
                            <asp:ListItem>The agent makes investments for one year.</asp:ListItem>
                            <asp:ListItem>The earnings from each investment affect affect the bonus you get.</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>What is the exact range of the possible profits for each investment?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>-10 - 10</asp:ListItem>
                            <asp:ListItem>-50 - 50</asp:ListItem>
                            <asp:ListItem>0 - 30</asp:ListItem>
                            <asp:ListItem>0 - 100</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Assuming you have managed to accumulate 400 virtual dollars. What will be your bonus?
                        <asp:RadioButtonList ID="rbl3" runat="server">
                            <asp:ListItem>10 cents</asp:ListItem>
                            <asp:ListItem>20 cents</asp:ListItem>
                            <asp:ListItem>25 cents</asp:ListItem>
                            <asp:ListItem>40 cents</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnPrevToInstructions" runat="server" Text="Back To Instructions" OnClick="btnPrevToInstructions_Click" />
    <asp:Button ID="btnNextToProceedToGame" runat="server" Text="Next" OnClick="btnNextToProceedToGame_Click" />

</asp:Content>
