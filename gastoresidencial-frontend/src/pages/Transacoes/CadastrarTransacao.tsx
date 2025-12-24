import { useState, useEffect } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';

export function CadastrarTransacao() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    descricao: '', valor: 0, tipo: 1, idCategoria: 0, idPessoa: 0
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await api.post('/Transacao', formData);
    navigate('/transacoes/listar');
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md bg-white p-6 rounded shadow space-y-4">
      <h2 className="text-xl font-bold">Nova Transação</h2>
      <input type="text" placeholder="Descrição" className="w-full border p-2" 
             onChange={e => setFormData({...formData, descricao: e.target.value})} />
      <input type="number" placeholder="Valor" className="w-full border p-2" 
             onChange={e => setFormData({...formData, valor: Number(e.target.value)})} />
      <select className="w-full border p-2" onChange={e => setFormData({...formData, tipo: Number(e.target.value)})}>
        <option value={1}>Despesa</option>
        <option value={2}>Receita</option>
      </select>
      {/* Aqui você faria um map de categorias e pessoas reais vindo da API */}
      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">Salvar</button>
    </form>
  );
}