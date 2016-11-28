<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionsPage.aspx.cs" Inherits="InvestmentAdviser.InstructionsPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img id="InstructionImage" src="Images/Instructions0.png" width="750" height="450" />

    <br />
    <br />
    <div id="myProgress">
        <div id="myBar">
            <div id="label"></div>
        </div>
    </div>

    <br />

    <button id="prevBtn" onclick="prev()" type='button' disabled="disabled">Prev</button>
    <button id="nextBtn" onclick="next()" type='button'>Next</button>
    <asp:Button ID="continueToQuiz" Text="Continue to Quiz" style="display:none" runat="server" OnClick="btnNextInstruction_Click"/>
    <script type="text/javascript">
        var currentInstruction = 0;
        var lastInstruction = 1;

        function updateInstruction() {
            document.getElementById("InstructionImage").src = "Images/Instructions" + currentInstruction.toString() + ".png";
        }

        function getCurrentProgress() {
            return Math.round(currentInstruction * 100 / lastInstruction);
        }

        function updateProgressBar() {
            var progress = getCurrentProgress();
            var elem = document.getElementById("myBar");
            elem.style.width = progress + '%';
            document.getElementById("label").innerHTML = progress * 1 + '%';
        }

        function next() {
            if (currentInstruction <= lastInstruction) {
                currentInstruction++;

                updateInstruction();

                var prevBtn = document.getElementById("prevBtn");
                prevBtn.disabled = false;
                
                if (currentInstruction == lastInstruction) {
                    var nextBtn = document.getElementById("nextBtn");
                    nextBtn.disabled = true;
                    nextBtn.hidden = true;

                    var quizBtn = document.getElementById('<%= continueToQuiz.ClientID %>');
                    if (quizBtn) {
                        quizBtn.style.display = 'inherit';
                    }
                }

                updateProgressBar();
                return false;
            }
        }

        function prev() {
            if (currentInstruction > 0) {
                currentInstruction--;

                updateInstruction();

                var nextBtn = document.getElementById("nextBtn");
                nextBtn.disabled = false;
                nextBtn.hidden = false;

                var quizBtn = document.getElementById('<%= continueToQuiz.ClientID %>');
                if (quizBtn) {
                    quizBtn.style.display = 'none';
                }

                if (currentInstruction == 0) {
                    var prevBtn = document.getElementById("prevBtn");
                    prevBtn.disabled = true;
                }

                updateProgressBar();
                return false;
            }
        }
    </script>
</asp:Content>
