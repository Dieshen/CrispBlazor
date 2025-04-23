using CrispBlazor.Shared.Requests;
using CrispBlazor.Shared.Responses;

namespace CrispBlazor.Shared.Interfaces
{
    public interface IFileService
    {
        ValueTask<FileResponse> Download(FileRequest request);
        ValueTask<FileResponse> Upload(FileUploadRequest request);
        ValueTask<bool> Move(FileUpdateRequest request);
        ValueTask<bool> Delete(FileRequest request);
        ValueTask<bool> Exists(FileRequest request);
        ValueTask<bool> Copy(FileUpdateRequest request);
    }
}
