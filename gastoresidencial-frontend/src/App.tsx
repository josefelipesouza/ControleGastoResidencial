import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { ListarPessoas } from './pages/Pessoas/ListarPessoas';
import { TotaisPessoaRelatorio } from './pages/Transacoes/TotaisPessoa';
// Importe as outras páginas que você criar seguindo o mesmo padrão...

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<div className="text-3xl font-bold">Bem vindo ao Sistema de Gastos!</div>} />
          <Route path="pessoas/listar" element={<ListarPessoas />} />
          <Route path="transacoes/totais-pessoa" element={<TotaisPessoaRelatorio />} />
          {/* Adicionar as rotas de cadastros e categorias aqui */}
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;