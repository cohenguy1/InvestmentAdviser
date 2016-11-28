<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="InvestmentAdviser.Game" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="TimerGame" OnTick="TimerGame_Tick" runat="server" Enabled="false"></asp:Timer>


    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewGame" runat="server">
            <asp:Label ID="PositionHeader" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;"></asp:Label>

            <br />
            <asp:Panel ID="PanelInterviewSpeedBasket" runat="server" Width="600px" Style="margin-left: 0px; float: right">
                <asp:Panel ID="PanelInterviewBasket" runat="server" Style="margin-left: 0px">
                    <br />
                    <asp:Panel ID="PanelInterview" runat="server" Width="600px" Style="margin-left: 20px; float: left">
                        <br />
                        <asp:Panel runat="server" Style="margin-left: 0px; text-align: left">
                            <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                        </asp:Panel>
                        <br />
                    </asp:Panel>

                </asp:Panel>

                <br />
                <asp:Panel ID="PanelSpeed" runat="server" Style="margin-left: 0px; margin-top: 60px; align-content: center;" Width="600px">
                    <asp:Label ID="LabelInterviewing" runat="server" Font-Size="Medium" Text="Analyzing stocks and investing money...<br /><br /><br />"></asp:Label>
                    <asp:Image ID="ImageInterview" runat="server" Height="187px" Width="235px" ImageUrl="~/Images/InterviewingCandidates.gif" Visible="False" />
                    <asp:Label ID="MovingToNextPositionLabel" runat="server" Font-Size="Larger" Visible="false"></asp:Label>
                    <asp:Label ID="MovingJobTitleLabel" runat="server" Style="margin-top: 20px;" Font-Bold="true" Font-Size="X-Large" Visible="false" ForeColor="Green"></asp:Label>
                    <br />
                    <asp:Label ID="TurnSummaryLbl1" runat="server" Font-Size="Large" Visible="false" Text="<br />The investment adviser gained for you&nbsp;"></asp:Label>
                    <asp:Label ID="TurnSummaryLbl2" runat="server" Font-Size="X-Large" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                    <asp:Label ID="TurnSummaryLbl3" runat="server" Font-Size="Large" Visible="false" Text="."></asp:Label>
                    <asp:Label ID="PrizePointsLbl1" runat="server" Font-Size="Large" Visible="false" Text="<br /><br />Prize points received:&nbsp;"></asp:Label>
                    <asp:Label ID="PrizePointsLbl2" runat="server" Font-Size="X-Large" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                    <asp:Label ID="PrizePointsLbl3" runat="server" Font-Size="Large" Visible="false" Text="."></asp:Label>
                    <asp:Label ID="SummaryNextLbl" runat="server" Visible="false"></asp:Label>
                    <br />
                    <asp:Button ID="btnNextTurn" runat="server" Visible="false" Text="Next" OnClick="btnNextTurn_Click" />
                </asp:Panel>


            </asp:Panel>
        </asp:View>
        <asp:View ID="ViewRating" runat="server">

            <asp:Label ID="Label2" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;">Adviser Rating</asp:Label>
            <br />
            <asp:Panel ID="Panel3" runat="server" Style="margin-left: 20px; float: right">
                <br />

                <div style="text-align: center; width: 600px; margin: 0 auto;">
                    <table style="text-align: left; width: 600px;" border="1">
                        <tr>
                            <td>&nbsp;Hi!
                                        <br />
                                <br />
                                &nbsp;We stopped for a moment so you can rate the Investment Adviser.
                                        <br />
                                &nbsp;Your rating should be based on how good you think the adviser is.
                                        <br />
                                <br />

                                <asp:Label runat="server" Font-Italic="true" Font-Bold="true">
                                        &nbsp;The rating you give will not affect the game or the bonus you get.
                                </asp:Label>
                                <br />
                                <br />
                                &nbsp;Rate the Investment Adviser from 1 to 10, 10 being the best:
                                <br />
                                <br />

                                <div class="rating-star-block" id='rating-star-block'>
                                    <div class="ratingLabel" id="ratingIndication">Horrible</div>
                                    <div class="star outline" href="#" rating="1" title="rate 1" id="star1">rate 1</div>
                                    <div class="star outline" href="#" rating="2" title="rate 2" id="star2">rate 2</div>
                                    <div class="star outline" href="#" rating="3" title="rate 3" id="star3">rate 3</div>
                                    <div class="star outline" href="#" rating="4" title="rate 4" id="star4">rate 4</div>
                                    <div class="star outline" href="#" rating="5" title="rate 5" id="star5">rate 5</div>
                                    <div class="star outline" href="#" rating="6" title="rate 6" id="star6">rate 6</div>
                                    <div class="star outline" href="#" rating="7" title="rate 7" id="star7">rate 7</div>
                                    <div class="star outline" href="#" rating="8" title="rate 8" id="star8">rate 8</div>
                                    <div class="star outline" href="#" rating="9" title="rate 9" id="star9">rate 9</div>
                                    <div class="star outline" href="#" rating="10" title="rate 10" id="star10">rate 10</div>
                                    <div class="ratingLabel" id="ratingLbl">Great</div>
                                </div>

                                <asp:HiddenField ID="ratingHdnValue" Value="0" runat="server" />

                                <br />
                                &nbsp;Please explain why you rated the adviser as you did:
                            <br />
                                <br />
                                <center>
                            <asp:TextBox ID="reasonTxtBox" onkeypress="return this.value.length<=300" runat="server" Rows="5" Columns="40" TextMode="multiline" Style="margin-left: 5px"></asp:TextBox>
                            </center>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <asp:Button ID="btnRate" runat="server" Text="Rate!" OnClientClick="returnString();" OnClick="btnRate_Click" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="PanelScenarioTurns" runat="server" Style="margin-left: 0px; float: left">
        <asp:Table ID="ScenarioTurnsTable"
            BorderColor="black"
            BorderWidth="1"
            GridLines="Both"
            runat="server">
            <asp:TableRow Height="35px">
                <asp:TableCell ID="cell1" Width="60px" HorizontalAlign="Left" Text="&nbsp;Turns" Style="color: blue;" Font-Bold="true"></asp:TableCell>
                <asp:TableCell ID="cell2" Width="40px" HorizontalAlign="Center" Text="&nbsp;Gain&nbsp;" Style="color: blue;" Font-Bold="true"></asp:TableCell>
                <asp:TableCell ID="cell3" Width="60px" HorizontalAlign="Center" Text="&nbsp;Prize Points" Style="color: blue;" Font-Bold="true"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn1Cell" HorizontalAlign="Left" Text="&nbsp;Turn 1"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn1RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn1PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn2Cell" HorizontalAlign="Left" Text="&nbsp;Turn 2"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn2RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn2PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn3Cell" HorizontalAlign="Left" Text="&nbsp;Turn 3"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn3RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn3PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn4Cell" HorizontalAlign="Left" Text="&nbsp;Turn 4"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn4RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn4PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn5Cell" HorizontalAlign="Left" Text="&nbsp;Turn 5"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn5RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn5PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn6Cell" HorizontalAlign="Left" Text="&nbsp;Turn 6"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn6RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn6PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn7Cell" HorizontalAlign="Left" Text="&nbsp;Turn 7"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn7RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn7PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn8Cell" HorizontalAlign="Left" Text="&nbsp;Turn 8"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn8RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn8PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn9Cell" HorizontalAlign="Left" Text="&nbsp;Turn 9"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn9RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn9PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="ScenarioTurn10Cell" HorizontalAlign="Left" Text="&nbsp;Turn 10"></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn10RankCell" HorizontalAlign="Center" Text=""></asp:TableCell>
                <asp:TableCell ID="ScenarioTurn10PrizeCell" HorizontalAlign="Center" Text=""></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="35px">
                <asp:TableCell ID="TotalPrizePointsCell" ColumnSpan="3" Font-Size="Large" Font-Bold="true" ForeColor="Purple" HorizontalAlign="Left" Text="&nbsp;Total Prize Points:"></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </asp:Panel>


    <script type="text/javascript">
        function returnString() {
            debugger;
            var ratingStarBlock = document.getElementById('rating-star-block');
            var stars = ratingStarBlock.getElementsByTagName('div');

            var savedRank = 0;
            for (var i = 0; i < stars.length; i++) {
                var child = stars[i];
                if (child.id.toString().startsWith("star") && child.classList.contains("selected")) {
                    savedRank++;
                }
            }

            if (savedRank == 0) {
                alert("Please rate the Investment Adviser!");
                return false;
            }

            document.getElementById('<%=ratingHdnValue.ClientID %>').value = savedRank.toString();
        }
    </script>
</asp:Content>
