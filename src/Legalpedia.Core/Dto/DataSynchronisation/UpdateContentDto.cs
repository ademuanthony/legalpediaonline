namespace Legalpedia.Dto.DataSynchronisation
{
    public class UpdateContentDto<T>
    {
        public bool IsNewRecord { get; set; }
        public T Content { get; set; }
    }
}
