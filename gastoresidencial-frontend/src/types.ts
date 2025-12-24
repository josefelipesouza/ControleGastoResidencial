export interface Pessoa { id: number; nome: string; idade: number; }
export interface Categoria { id: number; nome: string; descricao: string; finalidade: number; }
export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: string;
  nomeCategoria: string;
  nomePessoa: string;
}
export interface TotaisPessoa {
  itens: { pessoaId: number; nomePessoa: string; totalReceitas: number; totalDespesas: number; saldo: number }[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeralLiquido: number;
}