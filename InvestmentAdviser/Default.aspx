<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InvestmentAdviser.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2>You will recieve 20 cents for this HIT and it will take about 10 minutes of your time. </h2>
    <h2>Game Background</h2>

    <div style="text-align: center; width: 800px; margin: 0 auto;">
        <table style="text-align: center; max-width: 800px; font-size: large">
            <tr>
                <td>You have decided to invest in the stock market.</td>
            </tr>
            <tr>
                <td>In this game you an Investment Adviser wish to gain as much money as possible from these investments.</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>You have 20 turns, in each turn the adviser invests 100$ on stocks.
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <b><span style="font-size: 20px;">You will receive an additional bonus after the game. </span></b>
                    <br />
                    <asp:Label ID="backgroundText3" runat="server" Text="Each dollar will win you one prize point. At the end, you will get a cent for each 25 prize points."></asp:Label>
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