import React from 'react';
import ReactDom from 'react-dom';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

class CadastroProduto extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: '0',
            nome: '',
            preco: '',
            estoque: '',
            categoria_id: '1',
            categorias: [
                { id: 1, nome: 'Hardware' },
                { id: 2, nome: 'Periféricos' },
                { id: 3, nome: 'Cadeiras/Mesas' },
                { id: 4, nome: 'Monitores' },
                { id: 5, nome: 'Notebooks' },
            ],
            loading: false
        };
    }

    handleChange = e => {
        const target = e.target;
        const value = target.value;
        const name = target.name;
        this.setState({
            [name]: value
        });
    }

    handleSelect = e => {
        const categoria_id = parseInt(e.target.value);
        this.setState({ categoria_id });
     }

    salvarProduto = () => {
        this.setState({ loading: true });
        const produto =
        {
            id: this.state.id,
            nome: this.state.nome,
            preco: this.state.preco,
            estoque: this.state.estoque,
            categoria_id: this.state.categoria_id
        }
        axios.post('Salvar', produto)
        .then(response => {
            const {data} = response;
            if (data.sucesso) {
                toast.success(data.mensagem, {
                    position: "bottom-right",
                    theme: "colored"
                });
            }
            else {
                toast.error(data.mensagem, {
                    position: "bottom-right",
                    theme: "colored"
                });
            }
        })
        .catch(error => {
            toast.error('Erro ao cadastrar produto!', {
                position: "bottom-right",
                theme: "colored"
            });
        })
        .finally(() => {
            this.setState({ loading: false });
        });
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
                                                required />
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
                                                required />
                                        </div>
                                        <div>
                                            <label>Categoria do produto</label>
                                            <select
                                                className="form-select"
                                                name="categoria"
                                                onChange={this.handleSelect}>
                                                {
                                                    this.state.categorias.map(categoria => {
                                                        return <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                                    })
                                                }
                                            </select>
                                        </div>

                                        <button
                                            type="button"
                                            className={"btn btn-primary " + (this.state.loading ? "disable" : "")}
                                            onClick={this.salvarProduto}>
                                            {this.state.loading ? <i className="fas fa-circle-notch fa-spin"></i> : ""}
                                            Cadastrar Produto
                                        </button>

                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <ToastContainer />
            </>
        return (saida);
    }
}

export default CadastroProduto;
ReactDom.render(<CadastroProduto />, document.getElementById("root"));