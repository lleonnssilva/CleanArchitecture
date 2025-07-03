namespace CleanArchitecture.Domain.Events
{
   public interface IEventPublisher
    {
    void Publish<T>(T evento);
}
}
