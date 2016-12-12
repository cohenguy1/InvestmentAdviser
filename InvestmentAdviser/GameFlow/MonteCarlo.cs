using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentAdviser
{
    public static class MonteCarlo
    {
        public static int[] ChangesArray = new int[Common.NumOfChanges];

        public const int NumOfVectors = 1000000;

        public const double alpha = 0.45;

        public static void InitializeChangeProbabilities()
        {
            DbHandler dbHandler = new DbHandler();
            var changes = dbHandler.GetChanges();
            
            for (int i = 0; i < Common.NumOfChanges; i++)
            {
                ChangesArray[i] = changes[i];
            }
        }

        public static bool ShouldAsk(int[] changes, int stoppingDecision, Random random)
        {
            double[] exponentialSmoothing = new double[Common.TotalInvestmentsTurns];
            double[] exponentialSmoothingAccumulated = new double[Common.TotalInvestmentsTurns];

            for (var turnsIndex = 0; turnsIndex <= stoppingDecision; turnsIndex++)
            {
                if (turnsIndex == 0)
                {
                    exponentialSmoothing[turnsIndex] = changes[0];
                }
                else
                {
                    exponentialSmoothing[turnsIndex] = alpha * changes[turnsIndex] + (1 - alpha) * exponentialSmoothing[turnsIndex - 1];
                }
            }

            for (var i = 0; i < NumOfVectors; i++)
            {
                for (var turnIndex = stoppingDecision + 1; turnIndex < Common.TotalInvestmentsTurns; turnIndex++)
                {
                    var randomChange = GetRandomChange(random);

                    // determine the exponential smoothing according to the new randomized turns
                    exponentialSmoothing[turnIndex] = alpha * randomChange + (1 - alpha) * exponentialSmoothing[turnIndex - 1];
                    exponentialSmoothingAccumulated[turnIndex] += exponentialSmoothing[turnIndex];
                }
            }

            // precalculated smooting (monte carlo doesn't affect this smoothing)
            for (var turnIndex = 0; turnIndex <= stoppingDecision; turnIndex++)
            {
                exponentialSmoothingAccumulated[turnIndex] = exponentialSmoothing[turnIndex];
            }

            for (var turnIndex = stoppingDecision + 1; turnIndex < Common.TotalInvestmentsTurns; turnIndex++)
            {
                exponentialSmoothingAccumulated[turnIndex] /= NumOfVectors;
            }

            bool foundBetter = false;
            var currentES = exponentialSmoothingAccumulated[stoppingDecision];
            for (var turnIndex = stoppingDecision + 1; turnIndex < Common.TotalInvestmentsTurns; turnIndex++)
            {
                if (exponentialSmoothingAccumulated[turnIndex] > currentES)
                {
                    foundBetter = true;
                }
            }

            return !foundBetter;
        }

        private static int GetRandomChange(Random random)
        {
            var changeIndex = random.Next(Common.NumOfChanges);

            return ChangesArray[changeIndex];
        }
    }
}
