namespace NETCoreJsonMapper.Interfaces.Mappings
{
    public interface IJsonDataSource<TJsonTarget>
        where TJsonTarget : IJsonDataTarget, new()
    {
    }
}