using MongoDB.Bson;
using System;

namespace Models.DTO
{
    public class FileInfoDto : IBindingModel
    {
        public ObjectId Id { get; set; }
        public string Filename { get; set; }
        public BsonDocument MetaData { get; set; }
        public string Length { get; set; }
        public DateTime UploadDateTime { get; set; }
    }
}