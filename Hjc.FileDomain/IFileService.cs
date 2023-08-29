namespace Hjc.FileDomain
{
    public interface IFileService
    {
        public Task<string> GetByFileStringAsync(Stream fileStream);
    }
}