using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.Stack
{
    public class FileRepository : MongoCRUD
    { 
        private IGridFSBucket gridFsBucket { get; } 

        public FileRepository(IOptions<FilesMongoDbSettings> settings)
            : base(settings.Value.Connection,
                  settings.Value.Database,
                  settings.Value.TableNameFiles)
        { 
            gridFsBucket = new GridFSBucket(_contex);
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", file.ContentType)
            };
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var stream = reader.BaseStream;
                var fileId = await gridFsBucket.UploadFromStreamAsync(file.FileName, stream, options);
                return fileId.ToString();
            }
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", id);
            return await gridFsBucket.Find(filter).AnyAsync();
        }

        public Task<bool> AnyAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Where(x => x.Filename == fileName);
            return gridFsBucket.Find(filter).AnyAsync();
        }

        public async Task DeleteAsync(string fileName)
        {
            var fileInfo = await GetFileInfoAsync(fileName);
            if (fileInfo != null) { await DeleteAsync(fileInfo.Id); }
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await gridFsBucket.DeleteAsync(id);
        }

        private async Task<GridFSFileInfo> GetFileInfoAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName);
            return await gridFsBucket.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<GridFSDownloadStream<ObjectId>> DownloadAsync(ObjectId id)
        {
            return await gridFsBucket.OpenDownloadStreamAsync(id);
        }

        public async Task<GridFSDownloadStream<ObjectId>> DownloadAsync(string fileName)
        {
            return await gridFsBucket.OpenDownloadStreamByNameAsync(fileName);
        }

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id)
        {
            return await gridFsBucket.DownloadAsBytesAsync(id);
        }

        public IEnumerable<FileInfoDto> GetAllFilesByContentType(string contentType, int skip, int take)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Metadata, new BsonDocument(new BsonElement("contentType", contentType)));
            return gridFsBucket.Find(filter, GridFSFindOptions(skip, take)).ToList().Select(s => new FileInfoDto
            {
                Id = s.Id,
                Filename = s.Filename,
                MetaData = s.Metadata,
                Length = s.Length.ToString(),
                UploadDateTime = s.UploadDateTime,
            }).ToList();
        }

        public IEnumerable<FileInfoDto> GetAllFiles(int skip, int take)
        {
            return gridFsBucket.Find(new BsonDocumentFilterDefinition<GridFSFileInfo<ObjectId>>(new BsonDocument()), GridFSFindOptions(skip, take))
                                     .ToList()
                                     .Select(s => new FileInfoDto
                                     {
                                         Id = s.Id,
                                         Filename = s.Filename,
                                         MetaData = s.Metadata,
                                         Length = s.Length.ToString(),
                                         UploadDateTime = s.UploadDateTime,
                                     }).ToList();
        }

        private static GridFSFindOptions GridFSFindOptions(int skip, int take)
            => new GridFSFindOptions
            {
                Limit = take,
                Skip = skip,
            };
    }
}