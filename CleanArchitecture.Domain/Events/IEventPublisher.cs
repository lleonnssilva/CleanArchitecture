namespace CleanArchitecture.Domain.Events
{
   public interface IEventPublisher
    {
    void Publicar<T>(T evento);
}
}
