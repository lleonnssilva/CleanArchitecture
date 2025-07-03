using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Events
{
    public class ClienteCadastradoEvent
    {
        public Cliente Cliente { get; private set; }

        public ClienteCadastradoEvent(Cliente cliente)
        {
            Cliente = cliente;
        }
    }
}
