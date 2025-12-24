import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';

interface Pessoa {
  id: number;
  nome: string;
}

interface Categoria {
  id: number;
  nome: string;
}

export function CadastrarTransacao() {
  const navigate = useNavigate();

  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  const [formData, setFormData] = useState({
    descricao: '',
    valor: 0,
    tipo: 1,
    idCategoria: 0,
    idPessoa: 0
  });

  useEffect(() => {
    api.get('/Pessoa').then(res => setPessoas(res.data));
    api.get('/Categoria').then(res => setCategorias(res.data));
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!formData.idCategoria || !formData.idPessoa) {
      alert('Selecione uma categoria e uma pessoa');
      return;
    }

    await api.post('/Transacao', formData);
    navigate('/transacoes/listar');
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md bg-white p-6 rounded shadow space-y-4">
      <h2 className="text-xl font-bold">Nova Transação</h2>

      <input
        type="text"
        placeholder="Descrição"
        className="w-full border p-2"
        required
        onChange={e => setFormData({ ...formData, descricao: e.target.value })}
      />

      <input
        type="number"
        placeholder="Valor"
        className="w-full border p-2"
        required
        onChange={e => setFormData({ ...formData, valor: Number(e.target.value) })}
      />

      <select
        className="w-full border p-2"
        required
        onChange={e => setFormData({ ...formData, tipo: Number(e.target.value) })}
      >
        <option value={1}>Despesa</option>
        <option value={2}>Receita</option>
      </select>

      <select
        className="w-full border p-2"
        required
        onChange={e => setFormData({ ...formData, idCategoria: Number(e.target.value) })}
      >
        <option value={0}>Selecione uma categoria</option>
        {categorias.map(c => (
          <option key={c.id} value={c.id}>{c.nome}</option>
        ))}
      </select>

      <select
        className="w-full border p-2"
        required
        onChange={e => setFormData({ ...formData, idPessoa: Number(e.target.value) })}
      >
        <option value={0}>Selecione uma pessoa</option>
        {pessoas.map(p => (
          <option key={p.id} value={p.id}>{p.nome}</option>
        ))}
      </select>

      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
        Salvar
      </button>
    </form>
  );
}
