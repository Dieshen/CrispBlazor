namespace CrispBlazor.Shared
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException() : this("Not found")
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }
        public NotFoundException(object key, string objectName)
        : base($"Queried object {objectName} was not found, Key: {key}")
        {
        }

        public NotFoundException(object key, string objectName, Exception innerException)
        : base($"Queried object {objectName} was not found, Key: {key}", innerException)
        {
        }
    }
}
