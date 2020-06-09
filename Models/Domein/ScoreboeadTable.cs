using Models.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.Domein
{
    public class ScoreboeadTable
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public ScoreboeadType Type { get; set; }
        public DateTime StoredDate { get; set; }
        public string StoredBy { get; set; }
        public int VotesCount { get; set; }
        public List<RecipeVote> Votes { get; set; }
    }
}