using Microsoft.Extensions.Options;
using Settings;
using System;

namespace MongoDB.Stack
{
    public class FeedbackRepository : MongoCRUD
    { 
        public FeedbackRepository(IOptions<FeedbackMongoDbSettings> settings)
            : base(settings.Value.Connection,
                  settings.Value.Database,
                  settings.Value.TableNameFeedback)
        {
 
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }
    }
}