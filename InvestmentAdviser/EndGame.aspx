<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EndGame.aspx.cs" Inherits="InvestmentAdviser.EndGame" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Thank you for playing!</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            &nbsp;Thanks for playing, You did great!
                            <br />
                            <br />
                            &nbsp;Please provide feedback about the game (how hard it was, was it fun, etc.):
                            <br />
                            <br />
                            <center>
                            <asp:TextBox ID="feedbackTxtBox" onkeypress="return this.value.length<=300" runat="server" Rows="4" Columns="40" TextMode="multiline" Style="margin-left: 5px"></asp:TextBox>
                            </center>
                            <br />
                            <br />
                            &nbsp;Total Prize Points won:
                            <br />
                            <br />
                            <center>
                            <asp:Label ID="TotalPrizePointsLbl" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Larger"></asp:Label>
                            </center>
                            <br />
                            <br />
                            &nbsp;Your bonus is:
                            <br />
                            <br />
                            <center>
                            <asp:Label ID="BonusLbl" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="Larger"></asp:Label>
                            </center>
                            <br />
                            <br />
                            &nbsp;Please click on 'Collect your reward' to submit the HIT and send your feedback.
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <asp:Button ID="rewardBtn" runat="server" OnClick="rewardBtn_Click" Text="Collect your reward" />
</asp:Content>
