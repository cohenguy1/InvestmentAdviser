<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InvestmentAdviser.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2>You will receive 20 cents for this HIT and it will take about 10 minutes of your time. </h2>
    <h2>Game Background</h2>

    <div style="text-align: center; width: 800px; margin: 0 auto;">
        <table style="text-align: center; max-width: 800px; font-size: large">
            <tr>
                <td>In this game our Investment Agent will invest some virtual money on your behalf.</td>
            </tr>
            <tr>
                <td>The agent will make 20 investments, each time investing 100$ on several stocks for one year.
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>After each investment, you will see the earnings for that investment.
                    <br />
                    Your earnings will influence your bonus in this game - for each 25 virtual dollars earned, you will get a real cent as a bonus.
                </td>

            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Press 'Next' to continue. </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNextToInfo" runat="server" Text="Next" OnClick="btnNextToInfo_Click" Enabled="true" /></td>
            </tr>
        </table>
    </div>
</asp:Content>