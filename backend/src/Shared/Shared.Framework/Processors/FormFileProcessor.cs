using Microsoft.AspNetCore.Http;
using Shared.Core.DTO;

namespace Shared.Framework.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileDTO> _listFileDto = [];

        public List<FileDTO> Process(string BUCKET_NAME, IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();

                var fileDto = new FileDTO(
                    Stream: stream,
                    BucketName: BUCKET_NAME,
                    FileName: Guid.NewGuid() + Path.GetExtension(file.FileName),
                    ContentType: null);

                _listFileDto.Add(fileDto);
            }

            return _listFileDto;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var file in _listFileDto)
            {
                if (file.Stream is not null)
                    await file.Stream.DisposeAsync();
            }
        }
    }
}
