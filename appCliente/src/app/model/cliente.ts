export interface Cliente {
  id: string;                      // Identificador único do cliente
  nome: string;                    // Nome do cliente
  telefone: Telefone;              // Informações do telefone
  email: Email;                    // Informações do e-mail
  endereco: Endereco;              // Endereço completo
}

export interface Telefone {
  ddd: string;                     // Código de área do telefone
  numeroTelefone: string;          // Número de telefone do cliente
}

export interface Email {
  enderecoEmail: string;           // Endereço de e-mail do cliente
}

export interface Endereco {
  rua: string;                     // Nome da rua
  numero: string;                  // Número do imóvel
  bairro: string;                  // Bairro do cliente
  cidade: string;                  // Cidade onde o cliente reside
  estado: string;                  // Estado
  cep: string;                     // CEP
}