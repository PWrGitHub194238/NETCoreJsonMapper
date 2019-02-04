namespace NETCoreJsonMapper.Interface.Mappings
{
    public interface IJsonDataSource<TJsonTarget>
        where TJsonTarget : IJsonDataTarget, new()
    {
    }
}