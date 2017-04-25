using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace InvestmentAdviser
{
    public partial class DbHandler : System.Web.UI.Page
    {
        public AskPositionHeuristic GetAskPosition(bool isFriend)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            if (isFriend)
            {
                VectorNum = 1;
                return AskPositionHeuristic.MonteCarlo;

                Random ran = new Random();
                int randomAsk = ran.Next(5);

                VectorNum = ran.Next(50) + 1;
                
                switch (randomAsk)
                {
                    case 0:
                        return AskPositionHeuristic.First;
                    case 1:
                        return AskPositionHeuristic.Last;
                    case 2:
                        Random rand = new Random();
                        RandomHuristicAskPosition = rand.Next(Common.TotalInvestmentsTurns) + 1;
                        return AskPositionHeuristic.Random;
                    case 3:
                        return AskPositionHeuristic.Optimal;
                    case 4:
                        // MonteCarlo.InitializeChangeProbabilities();
                        return AskPositionHeuristic.MonteCarlo;
                    default:
                        return AskPositionHeuristic.First;
                }
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.First);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.First;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Optimal);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.Optimal;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.MonteCarlo);
            if (VectorNum != null)
            {
                // MonteCarlo.InitializeChangeProbabilities();
                return AskPositionHeuristic.MonteCarlo;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Last);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.Last;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Random);
            if (VectorNum != null)
            {
                Random ran = new Random();
                RandomHuristicAskPosition = ran.Next(Common.TotalInvestmentsTurns) + 1;
                return AskPositionHeuristic.Random;
            }

            throw new Exception("No Hit Slots available");
        }

        public void SetVectorAssignmentNull()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("update VectorsAssignments set LastStarted = NULL Where VectorNum=" + VectorNum))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int? GetFirstVectorSatisfying(AskPositionHeuristic askPosition)
        {
            var askPositionHeuristics = askPosition.ToString();

            if (!IsAskHeuristicsEnabled(askPositionHeuristics))
            {
                return null;
            }

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Select VectorNum, LastStarted " +
                    "from VectorsAssignments Where NextAskHeuristic='" + askPositionHeuristics + "' order by VectorNum"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    int vectorNum = 0;

                    using (SQLiteDataReader result = (SQLiteDataReader)cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            vectorNum = result.GetInt32(0);

                            DateTime lastStarted;
                            double diffFromNow;

                            if (!result.IsDBNull(1))
                            {
                                lastStarted = DateTime.Parse(result.GetString(1), new CultureInfo("fr-FR", false));
                                diffFromNow = (DateTime.Now - lastStarted).TotalHours;

                                if (diffFromNow < 0.5)
                                {
                                    continue;
                                }
                            }

                            var lastStartedStr = DateTime.Now.ToString(new CultureInfo("fr-FR", false));

                            using (SQLiteCommand cmd2 = new SQLiteCommand("update VectorsAssignments set LastStarted='" + lastStartedStr + "' " +
                                "where VectorNum = " + vectorNum))
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection = sqlConnection1;

                                cmd2.ExecuteNonQuery();
                            }

                            sqlConnection1.Close();
                            return vectorNum;
                        }
                    }
                }

                sqlConnection1.Close();
            }

            return null;
        }

        private static bool IsAskHeuristicsEnabled(string askPositionHeuristics)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            bool enabled = false;
            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    sqlConnection1.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand("Select Enabled from Configuration Where AskHeuristics='" + askPositionHeuristics + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        using (SQLiteDataReader result = (SQLiteDataReader)cmd.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                int value = result.GetInt32(0);
                                enabled = value == 1 ? true : false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                enabled = false;
            }

            return enabled;
        }

        public static int GetIntFromConfig(string key)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            string value;
            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Select Value from Configuration Where Key='" + key + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    value = (string)cmd.ExecuteScalar();
                }
            }

            return GetIntFromString(value);
        }

        private static int GetIntFromString(string value)
        {
            int intValue;

            if ((value == null) || (!int.TryParse(value, out intValue)))
            {
                return 0;
            }

            return intValue;
        }

        public void SetVectorNextAskPosition(string nextAskPosition)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("update VectorsAssignments set NextAskHeuristic ='" + nextAskPosition + "' Where VectorNum=" + VectorNum))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd2 = new SQLiteCommand("update VectorsAssignments set LastStarted = NULL Where VectorNum=" + VectorNum))
                {
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = sqlConnection1;

                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public int[] GetScenariosProfits()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            int[] profits = new int[Common.TotalInvestmentsTurns];

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                var command = new StringBuilder();
                command.Append("Select Turn1");
                for (int i = 2; i <= Common.TotalInvestmentsTurns; i++)
                {
                    command.Append(", Turn" + i);
                }
                command.Append(" from Vectors Where VectorNum=" + VectorNum);

                using (SQLiteCommand cmd = new SQLiteCommand(command.ToString()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            for (int i = 0; i < Common.TotalInvestmentsTurns; i++)
                            {
                                profits[i] = result.GetInt32(i);
                            }
                        };
                    }
                }
            }

            return profits;
        }

        public int[] GetChanges()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            int[] changes = new int[Common.NumOfChanges];

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                var command = "Select Change from Changes";

                using (SQLiteCommand cmd = new SQLiteCommand(command))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        int count = 0;

                        while (result.Read())
                        {
                            var change = result.GetInt32(0);

                            changes[count] = change;

                            count++;
                        };
                    }
                }
            }

            return changes;
        }
    }
}