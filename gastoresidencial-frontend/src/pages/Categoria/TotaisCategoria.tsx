import { useEffect, useState } from 'react';
import { api } from '../../services/api';
// Usamos 'import type' para satisfazer a regra erasableSyntaxOnly
import type { TotaisCategoriaResponse } from '../../types';

export function TotaisCategoria() {
  const [dados, setDados] = useState<TotaisCategoriaResponse | null>(null);
  const [erro, setErro] = useState<string | null>(null);

  useEffect(() => {
    api.get<TotaisCategoriaResponse>('/Transacao/totais-por-categoria')
      .then(res => {
        setDados(res.data);
      })
      .catch(err => {
        console.error(err);
        setErro("Não foi possível carregar os dados. Verifique se o back-end está rodando.");
      });
  }, []);

  // Formatação de moeda brasileira
  const formatarMoeda = (valor: number) => {
    return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  if (erro) return <div className="text-red-500 p-6">{erro}</div>;
  if (!dados) return <div className="p-6">Carregando relatório...</div>;

  return (
    <div className="bg-white p-6 rounded-lg shadow-md border border-gray-200">
      <div className="mb-6">
        <h2 className="text-2xl font-bold text-gray-800">Relatório: Totais por Categoria</h2>
        <p className="text-gray-500 text-sm">Visão consolidada de receitas e despesas.</p>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="p-4 font-semibold text-gray-700">Categoria</th>
              <th className="p-4 font-semibold text-green-700 text-right">Receitas</th>
              <th className="p-4 font-semibold text-red-700 text-right">Despesas</th>
              <th className="p-4 font-semibold text-gray-700 text-right">Saldo</th>
            </tr>
          </thead>
          <tbody>
            {dados.itens.map((item) => (
              <tr key={item.categoriaId} className="border-b border-gray-100 hover:bg-gray-50 transition-colors">
                <td className="p-4 text-gray-800 font-medium">{item.nomeCategoria}</td>
                <td className="p-4 text-green-600 text-right">{formatarMoeda(item.totalReceitas)}</td>
                <td className="p-4 text-red-600 text-right">{formatarMoeda(item.totalDespesas)}</td>
                <td className={`p-4 text-right font-bold ${item.saldo >= 0 ? 'text-blue-600' : 'text-red-600'}`}>
                  {formatarMoeda(item.saldo)}
                </td>
              </tr>
            ))}
          </tbody>
          <tfoot className="bg-gray-800 text-white">
            <tr>
              <td className="p-4 font-bold rounded-bl-lg">TOTAL GERAL</td>
              <td className="p-4 font-bold text-right text-green-400">{formatarMoeda(dados.totalGeralReceitas)}</td>
              <td className="p-4 font-bold text-right text-red-400">{formatarMoeda(dados.totalGeralDespesas)}</td>
              <td className="p-4 font-bold text-right text-white underline rounded-br-lg">
                {formatarMoeda(dados.saldoGeralLiquido)}
              </td>
            </tr>
          </tfoot>
        </table>
      </div>
    </div>
  );
}