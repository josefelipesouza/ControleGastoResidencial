import { Link, Outlet } from 'react-router-dom';
import { Users, Tag, ArrowRightLeft, Home } from 'lucide-react';

export function Layout() {
  return (
    <div className="min-h-screen flex flex-col">
      <header className="bg-white border-b shadow-sm">
        <div className="max-w-7xl mx-auto px-4 h-16 flex items-center space-x-8">
          <Link to="/" className="font-bold text-xl text-blue-600 flex items-center gap-2">
            <Home size={20} /> GastoResidencial
          </Link>
          
          <nav className="flex space-x-6">
            {/* Menu Pessoa */}
            <div className="group relative py-4">
              <button className="flex items-center gap-1 font-medium hover:text-blue-600">
                <Users size={18} /> Pessoa
              </button>
              <div className="hidden group-hover:block absolute top-full left-0 bg-white border shadow-lg rounded-md w-40 z-50">
                <Link to="/pessoas/cadastrar" className="block px-4 py-2 hover:bg-blue-50">Cadastrar</Link>
                <Link to="/pessoas/listar" className="block px-4 py-2 hover:bg-blue-50">Listar</Link>
              </div>
            </div>

            {/* Menu Categoria */}
            <div className="group relative py-4">
              <button className="flex items-center gap-1 font-medium hover:text-blue-600">
                <Tag size={18} /> Categoria
              </button>
              <div className="hidden group-hover:block absolute top-full left-0 bg-white border shadow-lg rounded-md w-40 z-50">
                <Link to="/categorias/cadastrar" className="block px-4 py-2 hover:bg-blue-50">Cadastrar</Link>
                <Link to="/categorias/listar" className="block px-4 py-2 hover:bg-blue-50">Listar</Link>
              </div>
            </div>

            {/* Menu Transações */}
            <div className="group relative py-4">
              <button className="flex items-center gap-1 font-medium hover:text-blue-600">
                <ArrowRightLeft size={18} /> Transações
              </button>
              <div className="hidden group-hover:block absolute top-full left-0 bg-white border shadow-lg rounded-md w-64 z-50">
                <Link to="/transacoes/cadastrar" className="block px-4 py-2 hover:bg-blue-50">Cadastrar</Link>
                <Link to="/transacoes/listar" className="block px-4 py-2 hover:bg-blue-50">Listar</Link>
                <Link to="/transacoes/totais-pessoa" className="block px-4 py-2 hover:bg-blue-50">Totais por Pessoa</Link>
                <Link to="/transacoes/totais-categoria" className="block px-4 py-2 hover:bg-blue-50">Totais por Categoria</Link>
              </div>
            </div>
          </nav>
        </div>
      </header>

      <main className="flex-1 max-w-7xl mx-auto w-full p-6">
        <Outlet />
      </main>
    </div>
  );
}