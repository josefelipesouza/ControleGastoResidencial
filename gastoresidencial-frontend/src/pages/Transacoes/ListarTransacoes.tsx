import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import type { Transacao } from '../../types';
import { Receipt, ArrowUpCircle, ArrowDownCircle, Search } from 'lucide-react';

export function ListarTransacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [filtro, setFiltro] = useState('');
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    api.get<Transacao[]>('/Transacao')
      .then(res => {
        setTransacoes(res.data);
      })
      .catch(err => console.error("Erro ao carregar transações:", err))
      .finally(() => setCarregando(false));
  }, []);

  // Filtro simples por descrição ou nome da pessoa
  const transacoesFiltradas = transacoes.filter(t => 
    t.descricao.toLowerCase().includes(filtro.toLowerCase()) ||
    t.nomePessoa.toLowerCase().includes(filtro.toLowerCase())
  );

  const formatarMoeda = (valor: number) => {
    return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  if (carregando) return <div className="p-6 text-center">Carregando lançamentos...</div>;

  return (
    <div className="bg-white rounded-xl shadow-md border border-gray-100 overflow-hidden">
      {/* Cabeçalho e Busca */}
      <div className="p-6 border-b border-gray-100 flex flex-col md:flex-row md:items-center justify-between gap-4">
        <div className="flex items-center gap-3 text-blue-600">
          <Receipt size={28} />
          <h2 className="text-2xl font-bold text-gray-800">Extrato de Transações</h2>
        </div>
        
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" size={18} />
          <input 
            type="text"
            placeholder="Buscar por descrição ou pessoa..."
            className="pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none w-full md:w-80"
            value={filtro}
            onChange={(e) => setFiltro(e.target.value)}
          />
        </div>
      </div>

      {/* Tabela */}
      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-gray-50 text-gray-600 uppercase text-xs font-bold">
              <th className="p-4">Tipo</th>
              <th className="p-4">Descrição</th>
              <th className="p-4">Pessoa</th>
              <th className="p-4">Categoria</th>
              <th className="p-4 text-right">Valor</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-100">
            {transacoesFiltradas.length > 0 ? (
              transacoesFiltradas.map((t) => (
                <tr key={t.id} className="hover:bg-blue-50/50 transition-colors">
                  <td className="p-4 text-center w-16">
                    {/* Valida se o tipo é "Receita" ou "2" conforme seu Handler */}
                    {t.tipo === "Receita" || t.tipo === "2" ? (
                      <ArrowUpCircle className="text-green-500" size={24} aria-label="Receita" />
                    ) : (
                      <ArrowDownCircle className="text-red-500" size={24} aria-label="Despesa" />
                    )}
                  </td>
                  <td className="p-4 font-medium text-gray-800">{t.descricao}</td>
                  <td className="p-4 text-gray-600">{t.nomePessoa}</td>
                  <td className="p-4">
                    <span className="px-2 py-1 bg-gray-100 text-gray-600 rounded-md text-sm">
                      {t.nomeCategoria}
                    </span>
                  </td>
                  <td className={`p-4 text-right font-bold ${t.tipo === "Receita" || t.tipo === "2" ? 'text-green-600' : 'text-red-600'}`}>
                    {formatarMoeda(t.valor)}
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={5} className="p-10 text-center text-gray-400">
                  Nenhuma transação encontrada.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}