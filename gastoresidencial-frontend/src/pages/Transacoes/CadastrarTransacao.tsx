import { useEffect, useState } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';
import { extractErrorMessage } from '../../utils/error';

interface Pessoa { id: number; nome: string; }
interface Categoria { id: number; nome: string; }

export function CadastrarTransacao() {
  const navigate = useNavigate();
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [erro, setErro] = useState<string | null>(null);

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
    setErro(null);

    if (!formData.idCategoria || !formData.idPessoa) {
      setErro('Selecione uma categoria e uma pessoa');
      return;
    }

    try {
      await api.post('/Transacao', formData);
      navigate('/transacoes/listar');
    } catch (err: any) {
      setErro(extractErrorMessage(err));
    }
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto bg-white p-6 rounded shadow space-y-4">
      <h2 className="text-xl font-bold mb-4">Nova Transação</h2>

      {erro && (
        <div className="p-3 bg-red-100 text-red-700 rounded border">
          {erro}
        </div>
      )}

      <input className="w-full border p-2 rounded"
        placeholder="Descrição"
        onChange={e => setFormData({ ...formData, descricao: e.target.value })} />

      <input type="number" className="w-full border p-2 rounded"
        placeholder="Valor"
        onChange={e => setFormData({ ...formData, valor: Number(e.target.value) })} />

      <select className="w-full border p-2 rounded"
        onChange={e => setFormData({ ...formData, tipo: Number(e.target.value) })}>
        <option value={1}>Despesa</option>
        <option value={2}>Receita</option>
      </select>

      <select className="w-full border p-2 rounded"
        onChange={e => setFormData({ ...formData, idCategoria: Number(e.target.value) })}>
        <option value={0}>Selecione uma categoria</option>
        {categorias.map(c => <option key={c.id} value={c.id}>{c.nome}</option>)}
      </select>

      <select className="w-full border p-2 rounded"
        onChange={e => setFormData({ ...formData, idPessoa: Number(e.target.value) })}>
        <option value={0}>Selecione uma pessoa</option>
        {pessoas.map(p => <option key={p.id} value={p.id}>{p.nome}</option>)}
      </select>

      <button className="w-full bg-blue-600 text-white py-2 rounded font-bold">
        Salvar Transação
      </button>
    </form>
  );
}
