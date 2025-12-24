import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import type { TotaisPessoa } from '../../types';

export function TotaisPessoaRelatorio() {
  const [dados, setDados] = useState<TotaisPessoa | null>(null);

  useEffect(() => {
    api.get('/Transacao/totais-por-pessoa').then(res => setDados(res.data));
  }, []);

  if (!dados) return <div>Carregando...</div>;

  return (
    <div className="space-y-6">
      <div className="bg-white p-6 rounded-lg shadow">
        <h2 className="text-2xl font-bold mb-4 text-gray-800">Totais por Pessoa</h2>
        <table className="w-full text-left">
          <thead>
            <tr className="bg-gray-100 uppercase text-xs font-bold">
              <th className="p-3">Pessoa</th>
              <th className="p-3 text-green-600">Receitas</th>
              <th className="p-3 text-red-600">Despesas</th>
              <th className="p-3">Saldo</th>
            </tr>
          </thead>
          <tbody>
            {dados.itens.map(item => (
              <tr key={item.pessoaId} className="border-b">
                <td className="p-3 font-medium">{item.nomePessoa}</td>
                <td className="p-3 text-green-600">R$ {item.totalReceitas.toFixed(2)}</td>
                <td className="p-3 text-red-600">R$ {item.totalDespesas.toFixed(2)}</td>
                <td className={`p-3 font-bold ${item.saldo >= 0 ? 'text-blue-600' : 'text-orange-600'}`}>
                  R$ {item.saldo.toFixed(2)}
                </td>
              </tr>
            ))}
          </tbody>
          <tfoot className="bg-blue-50 font-bold">
            <tr>
              <td className="p-3">TOTAL GERAL</td>
              <td className="p-3 text-green-700">R$ {dados.totalGeralReceitas.toFixed(2)}</td>
              <td className="p-3 text-red-700">R$ {dados.totalGeralDespesas.toFixed(2)}</td>
              <td className="p-3 text-blue-800 underline">R$ {dados.saldoGeralLiquido.toFixed(2)}</td>
            </tr>
          </tfoot>
        </table>
      </div>
    </div>
  );
}