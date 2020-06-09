using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HF.Run.Services;
using System;
using System.Threading.Tasks;

namespace HF.Run.Jobs
{
    public class StoreScoreJob
    {
        public const string JobId = "StoreScoreJob";

        public StoreScoreJob()
        {
        }

        public async Task Run(PerformContext context, IJobCancellationToken cancellationToken)
        {
            context.WriteLine("Job Started" + DateTime.UtcNow);

            var storageService = new ScoreboardService();
            var status = storageService.StoreVotes(JobId);
            if (status)
            {
                context.WriteLine("Daily votes are stored and scoreboar is clear!");
            }
            else
            {
                context.WriteLine("Storing FAILD BE READY");
            }

            context.WriteLine("Job Ended" + DateTime.UtcNow);
        }
    }
}