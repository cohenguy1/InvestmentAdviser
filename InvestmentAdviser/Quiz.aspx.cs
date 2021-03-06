﻿using System;
using System.Data;
using System.Data.SQLite;

namespace InvestmentAdviser
{
    public partial class Quiz : System.Web.UI.Page
    {
        public const int MaxNumOfWrongQuizAnswers = 3;

        protected void btnNextToProceedToGame_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            var tryNumber = NumOfWrongQuizAnswers + 1;
            var answer1 = rbl1.SelectedIndex;
            var answer2 = rbl2.SelectedIndex;
            var answer3 = rbl3.SelectedIndex;

            if (rbl1.SelectedItem == null || rbl2.SelectedItem == null || rbl3.SelectedItem == null)
            {
                Alert.Show("You have to answer all the questions!");
                return;
            }

            var correct = (rbl1.SelectedIndex == 1) &&
                          (rbl2.SelectedIndex == 2) &&
                          (rbl3.SelectedIndex == 3);

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand
                        ("INSERT INTO Quiz (UserId, TryNumber, Answer1, Answer2, Answer3, Explanation, Correct) " +
                         "VALUES (@UserId, @TryNumber, @Answer1, @Answer2, @Answer3, @Explanation, @Correct)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@TryNumber", tryNumber);
                        cmd.Parameters.AddWithValue("@Answer1", answer1);
                        cmd.Parameters.AddWithValue("@Answer2", answer2);
                        cmd.Parameters.AddWithValue("@Answer3", answer3);
                        cmd.Parameters.AddWithValue("@Explanation", explanationTxtBox.Text);
                        cmd.Parameters.AddWithValue("@Correct", correct.ToString());
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }


            if (correct)
            {
                Response.Redirect("Game.aspx");
            }
            else
            {
                NumOfWrongQuizAnswers++;

                if (NumOfWrongQuizAnswers >= MaxNumOfWrongQuizAnswers)
                {
                    RevokeUser();

                    Response.Redirect("WrongQuiz.aspx");
                }
                else
                {
                    var triesRemaining = MaxNumOfWrongQuizAnswers - NumOfWrongQuizAnswers;
                    Alert.Show("Wrong answer, please try again. You have " + triesRemaining + " tries remaining.");
                }
            }
        }

        public void RevokeUser()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO WrongQuizUsers (UserId) VALUES (@UserId)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnPrevToInstructions_Click(object sender, EventArgs e)
        {
            Response.Redirect("InstructionsPage.aspx");
        }
    }
}