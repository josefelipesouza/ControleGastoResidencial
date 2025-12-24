import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import type { Categoria } from '../../types';

export function ListarCategorias() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  useEffect(() => {
    api.get('/Categoria').then(res => setCategorias(res.data));
  }, []);

  return (
    <div className="bg-white p-6 rounded shadow">
      <h2 className="text-xl font-bold mb-4">Categorias</h2>
      <table className="w-full text-left border-collapse">
        <thead>
          <tr className="bg-gray-100">
            <th className="p-2">Nome</th>
            <th className="p-2">Descrição</th>
            <th className="p-2">Finalidade</th>
          </tr>
        </thead>
        <tbody>
          {categorias.map(c => (
            <tr key={c.id} className="border-b">
              <td className="p-2">{c.nome}</td>
              <td className="p-2">{c.descricao}</td>
              <td className="p-2">{c.finalidade === 1 ? 'Despesa' : c.finalidade === 2 ? 'Receita' : 'Ambas'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}