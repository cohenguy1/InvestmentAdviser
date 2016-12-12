<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="InvestmentAdviser.Quiz" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Quiz</h2>
    <div style="text-align: center; width: 800px; margin: 0 auto;">
        <h3>Think carefully before you answer, you have only 5 chances to pass the quiz.</h3>
        <br />
        <table style="text-align: left; width: 800px;" border="1">
            <tr>
                <td colspan="2">&nbsp;How much does the agent invest each time?
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem>$ 10</asp:ListItem>
                            <asp:ListItem>$ 100</asp:ListItem>
                            <asp:ListItem>$ 20</asp:ListItem>
                            <asp:ListItem>$ 40</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="border-right:none; width:56%; text-align:left;vertical-align:top;">
                    <br />
                    &nbsp;What is the exact range of the possible profits for each investment?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>-10 - 10</asp:ListItem>
                            <asp:ListItem>-50 - 50</asp:ListItem>
                            <asp:ListItem>0 - 30</asp:ListItem>
                            <asp:ListItem>0 - 100</asp:ListItem>
                        </asp:RadioButtonList>
                </td>
                <td style="border-left:none">
                    <div id="chart_container" style="height: 200px; width:100%"></div>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;Assuming you have managed to accumulate 400 virtual dollars (remember that each 20 dollars will get you a bonus of a cent). 
                    What will be your bonus?
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
    <asp:Button ID="btnNextToProceedToGame" runat="server" Text="Next" OnClick="btnNextToProceedToGame_Click" />

</asp:Content>
