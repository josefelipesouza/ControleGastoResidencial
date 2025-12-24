import { useState } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';
import { UserPlus } from 'lucide-react';

export function CadastrarPessoa() {
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState<number | string>('');
  const [erro, setErro] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErro(null);

    try {
      // O objeto enviado segue exatamente o seu Record do backend
      await api.post('/Pessoa', {
        nome: nome,
        idade: Number(idade)
      });

      alert('Pessoa cadastrada com sucesso!');
      navigate('/pessoas/listar'); // Redireciona para a lista após o sucesso
    } catch (err: any) {
      // Captura o erro retornado pelo ErrorOr/FluentValidation
      const mensagemErro = err.response?.data?.errors?.[0]?.description 
                        || err.response?.data?.detail 
                        || 'Erro ao cadastrar pessoa.';
      setErro(mensagemErro);
    }
  };

  return (
    <div className="max-w-md mx-auto bg-white p-8 rounded-lg shadow-md">
      <div className="flex items-center gap-2 mb-6 text-blue-600">
        <UserPlus size={24} />
        <h2 className="text-2xl font-bold text-gray-800">Cadastrar Pessoa</h2>
      </div>

      {erro && (
        <div className="mb-4 p-3 bg-red-100 border border-red-400 text-red-700 rounded">
          {erro}
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700">Nome Completo</label>
          <input
            type="text"
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            placeholder="Ex: José Carlos"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Idade</label>
          <input
            type="number"
            required
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
            value={idade}
            onChange={(e) => setIdade(e.target.value)}
            placeholder="Ex: 25"
          />
        </div>

        <button
          type="submit"
          className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
        >
          Salvar Pessoa
        </button>
      </form>
    </div>
  );
}