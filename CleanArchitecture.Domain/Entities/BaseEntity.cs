namespace CleanArchitecture.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataAtualizacao { get; set; }
        public DateTimeOffset? DataExclusao { get; set; }
    }
}
