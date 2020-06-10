using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Stack;
using Settings;
using System;
using System.Threading.Tasks;

namespace EatCode.Api.Services
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file);

        Task<byte[]> DownloadAsyncAsByte(string id);

        Task<GridFSDownloadStream<ObjectId>> DownloadAsyncAsStreamById(string id);

        Task<GridFSDownloadStream<ObjectId>> DownloadAsyncAsStreamByName(string name);

        Task<bool> DeleteFile(string id);
    }

    public class FileService : IFileService
    {
        private readonly IMapper mapper;
        private readonly FileRepository repository;

        public FileService(IMapper mapper, IOptions<FilesMongoDbSettings> settings)
        { 
            repository = new FileRepository(settings);
            this.mapper = mapper;
        }

        public async Task<bool> DeleteFile(string id)
        {
            try
            {
                ObjectId _id = ObjectId.Parse(id);
                await repository.DeleteAsync(_id);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return false;
            }
        }

        public async Task<byte[]> DownloadAsyncAsByte(string id)
        {
            try
            {
                ObjectId _id = ObjectId.Parse(id);
                return await repository.DownloadAsBytesAsync(_id);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }

        public async Task<GridFSDownloadStream<ObjectId>> DownloadAsyncAsStreamById(string id)
        {
            try
            {
                ObjectId _id = ObjectId.Parse(id);
                return await repository.DownloadAsync(_id);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }

        public async Task<GridFSDownloadStream<ObjectId>> DownloadAsyncAsStreamByName(string name)
        {
            try
            {
                return await repository.DownloadAsync(name);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }

        public async Task<string> Upload(IFormFile file)
        {
            try
            {
                if (file != null) { return await repository.UploadAsync(file); }
                return null;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }
    }
}