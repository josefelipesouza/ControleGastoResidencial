import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Layout } from './components/Layout';

// Importações das páginas
import { ListarPessoas } from './pages/Pessoas/ListarPessoas';
import { CadastrarPessoa } from './pages/Pessoas/CadastrarPessoa';
import { ListarCategorias } from './pages/Categoria/ListarCategorias';
import { CadastrarCategoria } from './pages/Categoria/CadastrarCategoria'; 
import { ListarTransacoes } from './pages/Transacoes/ListarTransacoes'; 
import { CadastrarTransacao } from './pages/Transacoes/CadastrarTransacao';
import { TotaisPessoaRelatorio } from './pages/Transacoes/TotaisPessoa'; 
import { TotaisCategoria } from './pages/Categoria/TotaisCategoria';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          
          {/* Tela inicial padrão */}
          <Route index element={
            <div className="flex flex-col items-center justify-center h-64 bg-white rounded-lg shadow border border-dashed border-gray-300">
              <h1 className="text-3xl font-bold text-blue-600">GastoResidencial</h1>
              <p className="text-gray-500 mt-2">Sistema carregado. Use o menu acima.</p>
            </div>
          } />

          {/* Grupo Pessoas */}
          <Route path="pessoas">
            <Route path="listar" element={<ListarPessoas />} />
            <Route path="cadastrar" element={<CadastrarPessoa />} />
          </Route>

          {/* Grupo Categorias */}
          <Route path="categorias">
            <Route path="listar" element={<ListarCategorias />} />
            {/* CORREÇÃO: Substituído o 'div' pelo componente importado */}
            <Route path="cadastrar" element={<CadastrarCategoria />} />
          </Route>

          {/* Grupo Transações */}
          <Route path="transacoes">
            {/* CORREÇÃO: Substituído o 'div' pelo componente importado */}
            <Route path="listar" element={<ListarTransacoes />} />
            <Route path="cadastrar" element={<CadastrarTransacao />} />
            <Route path="totais-pessoa" element={<TotaisPessoaRelatorio />} />
            <Route path="totais-categoria" element={<TotaisCategoria />} />
          </Route>

          {/* Redirecionamento para rotas inexistentes */}
          <Route path="*" element={<Navigate to="/" />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;