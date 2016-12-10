<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProceedToGame.aspx.cs" Inherits="InvestmentAdviser.ProceedToGame" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Game Instructions</h2>
    <div style="text-align: center; width: 750px; margin: 0 auto;">
        <table style="text-align: left; width: 750px;" border="1">
            <tr>
                <td>
                    <br />
                    &nbsp;The following graph presents the historical earning distribution of investment agents in prior years, just to give you a general sense of what the markets is like.
                    <br />
                    <br />
                    &nbsp;The x-axis represent the change of the fund over a year (in percentage).
                    <br />
                    <br />
                    &nbsp;The y-axis represent the probability for that change.
                    <br />
                    <br />
                    &nbsp;As you can see, all profits are non-negative.
                    <br />
                    <br />
                    <center>
                        <div id="chart_container" style="height: 350px; width: 90%;"></div>
                    </center>
                    <br />
                    <br />

                    &nbsp;Press 'Next' to continue.
                            <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    
    <br />
    <asp:Button ID="btnNextToGame" runat="server" Text="Next" OnClick="btnNextToGame_Click" />

</asp:Content>
