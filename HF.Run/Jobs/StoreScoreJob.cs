using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HF.Run.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HF.Run.Jobs
{
    public class StoreScoreJob
    {
        public const string JobId = "StoreScoreJob";
        private readonly RedisDb client;
        public StoreScoreJob()
        {
            client = new RedisDb();
        }

        public async Task Run(PerformContext context, IJobCancellationToken cancellationToken)
        {
            context.WriteLine("Job Started");
            //client.Test();
            // Redis stuff here :) 
            context.WriteLine("Job Ended");
        }

    }
}
