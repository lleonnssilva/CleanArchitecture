namespace CleanArchitecture.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset DataCriacao { get;protected set; }
        public DateTimeOffset? DataAtualizacao { get; protected set; }
        public DateTimeOffset? DataExclusao { get; protected set; }
    }
}
