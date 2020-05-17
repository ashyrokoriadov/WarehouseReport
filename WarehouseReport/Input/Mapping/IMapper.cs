namespace WarehouseReport.Input.Mapping
{
    public interface IMapper<in TInput, out TResult>
    {
        TResult Map(TInput input);
    }
}
