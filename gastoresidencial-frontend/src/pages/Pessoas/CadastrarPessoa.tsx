import React, { useState } from 'react'; // Importação explícita do React
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';
import { UserPlus } from 'lucide-react';


export function CadastrarPessoa() {
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState('');
  const [erro, setErro] = useState<string | null>(null);
  const navigate = useNavigate();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setErro(null);

    const idadeNum = parseInt(idade);
    if (isNaN(idadeNum)) {
      setErro("Idade inválida");
      return;
    }

    try {
      await api.post('/Pessoa', {
        nome: nome,
        idade: idadeNum
      });

      alert('Cadastrado com sucesso!');
      navigate('/pessoas/listar');
    } catch (err: any) {
      const msg = err.response?.data?.errors?.[0]?.description || "Erro ao salvar";
      setErro(msg);
    }
  }

  return (
    <div className="max-w-md mx-auto bg-white p-8 rounded-lg shadow-md border border-gray-100">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-2xl font-bold flex items-center gap-2 text-gray-800">
          <UserPlus className="text-blue-600" /> Cadastrar Pessoa
        </h2>
      </div>

      {erro && (
        <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-md border border-red-200">
          {erro}
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Nome</label>
          <input
            type="text"
            className="w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 outline-none"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Idade</label>
          <input
            type="number"
            className="w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 outline-none"
            value={idade}
            onChange={(e) => setIdade(e.target.value)}
            required
          />
        </div>
        <div className="flex gap-2 pt-2">
          <button
            type="button"
            onClick={() => navigate('/pessoas/listar')}
            className="flex-1 px-4 py-2 border rounded-md hover:bg-gray-50"
          >
            Cancelar
          </button>
          <button
            type="submit"
            className="flex-1 px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 font-medium"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}