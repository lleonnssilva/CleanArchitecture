namespace CleanArchitecture.Application.Interfaces.Persistence
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
