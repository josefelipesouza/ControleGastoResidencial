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

export interface ItemTotalPessoa {
    pessoaId: number;
    nomePessoa: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

// Usando 'as const' para que o TS entenda como valores fixos (Tipo Eraseble)
export const Finalidade = {
    Despesa: 1,
    Receita: 2,
    Ambas: 3
} as const;

// Isso cria um tipo que aceita apenas 1, 2 ou 3
export type FinalidadeType = typeof Finalidade[keyof typeof Finalidade];

export interface Categoria {
    id: number;
    nome: string;
    descricao: string;
    // Alterado para number para bater com a expectativa do compilador
    finalidade: number; 
}

export interface ItemTotalCategoria {
    categoriaId: number;
    nomeCategoria: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface TotaisCategoriaResponse {
    itens: ItemTotalCategoria[];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoGeralLiquido: number;
}