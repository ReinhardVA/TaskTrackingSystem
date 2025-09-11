namespace TaskTrackingSystem.Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public string Name { get; }
        public object Key { get; }

        public NotFoundException(string name, object key)
            :base($"{name} ({key}) bulunamadı.")
        {
            Name = name;
            Key = key;
        }
    }
}
