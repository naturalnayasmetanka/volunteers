using Volunteers.Application.DTO;

namespace Volunteers.API.Processors
{
    public class FormFileProcessor
    {
        private readonly List<FileDTO> _listFileDto = [];
        private const string BUCKET_NAME = "photos";
        public List<FileDTO> Process(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();

                var fileDto = new FileDTO(
                    Stream: stream,
                    BucketName: BUCKET_NAME,
                    FileName: file.FileName,
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
