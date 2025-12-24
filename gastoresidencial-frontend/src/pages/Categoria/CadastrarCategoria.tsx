import { useState } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';
import { Tag, Save, ArrowLeft } from 'lucide-react';
import { Finalidade } from '../../types';

export function CadastrarCategoria() {
  const navigate = useNavigate();
  
  const [nome, setNome] = useState('');
  const [descricao, setDescricao] = useState('');
  const [finalidade, setFinalidade] = useState<number>(Finalidade.Despesa);
  const [erro, setErro] = useState<string | null>(null);
  const [carregando, setCarregando] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErro(null);
    setCarregando(true);

    try {
      // O objeto segue o record CadastrarCategoriaRequest do seu backend
      await api.post('/Categoria', {
        nome,
        descricao,
        finalidade: Number(finalidade)
      });

      alert('Categoria criada com sucesso!');
      navigate('/categorias/listar');
    } catch (err: any) {
      // Captura erros de validação do FluentValidation vindo do backend
      const mensagem = err.response?.data?.errors?.[0]?.description 
                    || err.response?.data?.detail 
                    || 'Erro ao conectar com o servidor.';
      setErro(mensagem);
    } finally {
      setCarregando(false);
    }
  };

  return (
    <div className="max-w-2xl mx-auto bg-white p-8 rounded-xl shadow-md border border-gray-100">
      <div className="flex items-center justify-between mb-8">
        <div className="flex items-center gap-3 text-blue-600">
          <Tag size={28} />
          <h2 className="text-2xl font-bold text-gray-800">Nova Categoria</h2>
        </div>
        <button 
          onClick={() => navigate(-1)}
          className="flex items-center gap-1 text-gray-500 hover:text-gray-700 transition-colors"
        >
          <ArrowLeft size={18} />
          Voltar
        </button>
      </div>

      {erro && (
        <div className="mb-6 p-4 bg-red-50 border-l-4 border-red-500 text-red-700">
          <p className="font-bold">Atenção</p>
          <p>{erro}</p>
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-6">
        <div>
          <label className="block text-sm font-semibold text-gray-700 mb-1">
            Nome da Categoria
          </label>
          <input
            type="text"
            required
            maxLength={100}
            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            placeholder="Ex: Alimentação, Aluguel, Salário..."
          />
        </div>

        <div>
          <label className="block text-sm font-semibold text-gray-700 mb-1">
            Descrição (Opcional)
          </label>
          <textarea
            rows={3}
            maxLength={250}
            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            placeholder="Breve detalhamento sobre o que compõe esta categoria"
          />
        </div>

        <div>
          <label className="block text-sm font-semibold text-gray-700 mb-1">
            Finalidade
          </label>
          <select
            className="w-full px-4 py-2 border border-gray-300 rounded-lg bg-white focus:ring-2 focus:ring-blue-500 outline-none transition-all"
            value={finalidade}
            onChange={(e) => setFinalidade(Number(e.target.value))}
          >
            <option value={Finalidade.Despesa}>Despesa (Dinheiro Saindo)</option>
            <option value={Finalidade.Receita}>Receita (Dinheiro Entrando)</option>
            <option value={Finalidade.Ambas}>Ambas (Permitir Entrada e Saída)</option>
          </select>
          <p className="mt-2 text-xs text-gray-500">
            Define se esta categoria aparecerá em cadastros de receitas ou despesas.
          </p>
        </div>

        <button
          type="submit"
          disabled={carregando}
          className={`w-full flex items-center justify-center gap-2 py-3 px-4 rounded-lg font-bold text-white transition-all 
            ${carregando ? 'bg-gray-400 cursor-not-allowed' : 'bg-blue-600 hover:bg-blue-700 shadow-lg active:transform active:scale-95'}`}
        >
          {carregando ? 'Salvando...' : (
            <>
              <Save size={20} />
              Cadastrar Categoria
            </>
          )}
        </button>
      </form>
    </div>
  );
}