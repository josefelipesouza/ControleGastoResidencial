import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import type { Pessoa } from '../../types';
import { Trash2 } from 'lucide-react';

export function ListarPessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const carregar = () => api.get('/Pessoa').then(res => setPessoas(res.data));

  useEffect(() => { carregar(); }, []);

  const excluir = async (id: number) => {
    if (confirm('Deseja excluir?')) {
      await api.delete(`/Pessoa/${id}`);
      carregar();
    }
  };

  return (
    <div className="bg-white p-6 rounded-lg shadow">
      <h2 className="text-2xl font-bold mb-4">Lista de Pessoas</h2>
      <table className="w-full text-left border-collapse">
        <thead>
          <tr className="border-b bg-gray-50">
            <th className="p-3">ID</th>
            <th className="p-3">Nome</th>
            <th className="p-3">Idade</th>
            <th className="p-3">Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map(p => (
            <tr key={p.id} className="border-b hover:bg-gray-50">
              <td className="p-3">{p.id}</td>
              <td className="p-3">{p.nome}</td>
              <td className="p-3">{p.idade}</td>
              <td className="p-3">
                <button onClick={() => excluir(p.id)} className="text-red-500 hover:text-red-700">
                  <Trash2 size={18} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}