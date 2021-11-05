import React from 'react';
import ReactDom from 'react-dom';
import axios from 'axios';
import {toast, ToastContainer} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

class CadastroProduto extends React.Component {
    constructor(props) {
        super(props);
        this.state = { 
            nome: '',
            preco: '',
            estoque: '',
            categoria: '',
            categorias: [
                {id: 1, nome: 'Hardware'},
                {id: 2, nome: 'Periféricos'},
                {id: 3, nome: 'Notebooks'},
                {id: 4, nome: 'Cadeiras/Mesas'},
                {id: 5, nome: 'Monitores'}
            ]
         };
    }

    handleChange = (event) => {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        this.setState({
            [name]: value
        });
    }

    cadastrarProduto = () => {
        const produto = 
        {
            nome: this.state.nome,
            preco: this.state.preco,
            estoque: this.state.estoque,
            categoria: this.state.categoria
        }
        axios.post('Produto/Cadastrar', produto)
        .then(res => {
            toast.success('Produto cadastrado com sucesso!');
            if (res.data.ok) 
            {
                toast.success(res.data.mensagem, {
                    position: "bottom-right",
                    theme: "colored"
                });
            }
            else 
            {
                toast.error(res.data.mensagem, {
                    position: "bottom-right",
                    theme: "colored"
                });
            }
            this.setState({
                nome: '',
                preco: '',
                estoqe: '',
                categoria: ''
            });
        })
    }

    render() { 
        const saida = 
        <>
            <div className="card o-hidden border-0 shadow-lg">
                <div className="card-body p-0">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="p-5">
                                <div className="text-left">
                                    <h1 className="h4 text-gray-900 mb-4">Cadastro de Produto</h1>
                                </div>
                                <form className="user" id="form-produto">
                                    <div className="form-group">
                                        <label>Nome do produto</label>
                                        <input 
                                            type="text" 
                                            name="nome" 
                                            className="form-control" 
                                            placeholder="Nome do produto" 
                                            value={this.state.nome} 
                                            onChange={this.handleChange}
                                            required/>
                                    </div>
                                    <div className="form-group">
                                        <label>Preço do produto</label>
                                        <input 
                                            type="number" 
                                            name="preco" 
                                            className="form-control" 
                                            placeholder="Preço" 
                                            value={this.state.preco} 
                                            onChange={this.handleChange}
                                            required
                                            />
                                    </div>
                                    <div className="form-group">
                                        <label>Estoque do produto</label>
                                        <input 
                                            type="number" 
                                            name="estoque" 
                                            className="form-control" 
                                            placeholder="Estoque" 
                                            value={this.state.estoqe} 
                                            onChange={this.handleChange}
                                            required/>
                                    </div>
                                    <div>
                                        <label>Categoria do produto</label>
                                        <select 
                                            className="form-select" 
                                            name="categoria"
                                            onChange={this.handleChange}>
                                            {
                                                this.state.categorias.map(categoria => {
                                                    return <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                                })
                                            }
                                        </select>
                                    </div>
            
                                    <button 
                                        type="button" 
                                        onClick={this.cadastrarProduto}
                                        className="btn btn-primary w-100 mt-5"
                                        >Cadastrar Produto</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <ToastContainer/>
        </>
        return ( saida );
    }
}
 
export default CadastroProduto;
ReactDom.render(<CadastroProduto/>, document.getElementById("root"));