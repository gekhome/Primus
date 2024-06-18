using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IUploadService
    {
        void Create(UploadsViewModel data, int schoolId);
        void DeleteFile(UploadsFilesViewModel data);
        void Destroy(UploadsViewModel data);
        List<UploadsFilesViewModel> GetFiles(int uploadId);
        List<UploadsViewModel> Read(int schoolId);
        UploadsViewModel Refresh(int entityId);
        void Update(UploadsViewModel data, int schoolId);
    }
}