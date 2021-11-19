import React from 'react';
import ReactDOM from 'react-dom';
import axios from 'axios';
import CadastroEdit from './CadastroEdit';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Listagem.css';

class Listagem extends React.Component {

    constructor(props) {
        super(props);
        this.state = {  
            id: '0',
            nome: '',
            preco: '',
            estoque: '',
            categoria_id: '1',
            categoria_nome: '',
            produtos: [],
            loadging: false,
            show: false
        }
    }

    componentDidMount = () => {
        this.setState({ loading: true });
        axios.get('Produto/Listar')
        .then(response => {
            console.log(response.data);
            this.setState({ produtos: response.data });
        })
        .catch(error => {
            console.log(error);
        })
        .finally(() => {
            this.setState({ loading: false });
            console.log('Finalizado');
        });
    }

    excluirProduto = id => {
        axios.delete('Produto/Excluir/' + id)
        .then(response => {
            const {data} = response;
            if(data.sucesso) {
                let produtos = this.state.produtos.filter(produto => produto.id !== id);
                console.log(produtos);
                this.setState({produtos});
                console.log(this.state.produtos);
                toast.success(data.mensagem);
            }
            else{
                toast.error(data.mensagem);
            }
            console.log(response.data);
        })
        .catch(error => {
            console.log(error);
        })
        .finally(() => {
            console.log('Finalizado');
        });
    }

    salvarProduto = () => {
        this.setState({ loading: true });
        const produto =
        {
            id: Number(this.state.id),
            nome: this.state.nome,
            preco:  Number(this.state.preco),
            estoque: Number(this.state.estoque),
            categoria_id: Number(this.state.categoria_id)
        }
        console.log(produto);
        axios.post('Produto/Salvar', produto)
        .then(response => {
            const {data} = response;
            if (data.sucesso) {
                console.log(data);
                toast.success(data.mensagem, {
                    position: "bottom-right",
                    theme: "colored"
                });
                // this.state.produtos.push(data.produto);
                this.fecharModal();
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


    abrirModal = produto => {
        this.setState({ show: true });
        this.setState({
            id: produto.id,
            nome: produto.nome,
            preco: produto.preco,
            estoque: produto.estoque,
            categoria_id: produto.categoria.id
        });
    }



    getColor = estoque => {
        if(estoque > 20) {
            return 'border-success';
        }
        else if(estoque > 10) {
            return 'border-warning';
        }
        else {
            return 'border-danger';
        }
    }

    render() { 
        let saida = 
            <>
                <CadastroEdit pai={this}/>
                <h1>Listagem de Produtos</h1>
                <div className="p-5">
                    <button 
                        className="btn btn-success col-auto"
                        onClick={() => this.setState({show: true})}>
                            <i className="fas fa-plus me-3" aria-hidden="true"></i>
                        Novo produto
                    </button>
                    <div className="col-12">
                        <div className="row border-0 bg-light rounded my-5 ">
                            {
                                this.state.loading 
                                ? 
                                Array.from(Array(6).keys()).map(i =>
                                <div key={i} className="col-6 mb-3">
                                    <div className="produto loading m-0 py-2 bg-white shadow-sm row flex-wrap align-items-center"> 
                                        <div className="col-auto placeholder-glow">
                                            <div className="placeholder " style={{height: 160, width: 160, borderRadius: 10}}> </div>
                                        </div>
                                        <div className="col row ps-5 pe-3 placeholder-glow">
                                            <h2 className="placeholder my-3 rounded-3"></h2>
                                            <div className="p-0">
                                                <span className="placeholder mb-2 rounded-3" style={{height: 25, width: 100}}></span>
                                            </div>
                                            <div className="p-0">
                                                <span className="placeholder mb-2 rounded-3" style={{height: 25, width: 200}}></span>
                                            </div>
                                            <div className="p-0 mb-4">
                                                <span className="placeholder mb-2 rounded-3" style={{height: 25, width: 240}}></span>
                                            </div>
                                            <div className="p-0 row justify-content-end">
                                                <span className="placeholder w-10 mb-2 me-3 rounded-3" style={{width: 80, height: 35}}></span>
                                                <span className="placeholder w-10 mb-2 me-3 rounded-3" style={{width: 80, height: 35}}></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                )
                                : this.state.produtos.map(produto => {
                                    return (
                                        <div className="col-6 mb-3" key={produto.id}>
                                            <div className="produto m-0 py-3 bg-white shadow-sm row flex-wrap align-items-center">
                                                <div className="col-auto">
                                                    <img 
                                                        src="https://via.placeholder.com/200"
                                                        className="d-block mx-auto"
                                                        style={{height: 160, width: 160, borderRadius: 10}}
                                                    />
                                                </div>
                                                <div className="col row px-3">
                                                    <h2>{produto.nome}</h2>
                                                    <p className="preco">
                                                        <i className="fas fa-dollar-sign"></i>
                                                        R$ {produto.preco}
                                                    </p>
                                                    <div className="p-0">
                                                        <p className={`badge-estoque ${this.getColor(produto.estoque)}`}>
                                                            <i className="fas fa-boxes"></i>
                                                            Qtd. Estoque <span className="estoque">{produto.estoque}</span>
                                                        </p>
                                                    </div>
                                                    <p>
                                                        <i className="fas fa-list"></i> 
                                                        Categoria: <strong>{produto.categoria.nome}</strong>
                                                    </p>
                                                    <div className="row justify-content-end">
                                                        <button 
                                                            className="btn btn-primary col-auto mr-3"
                                                            onClick={() => this.abrirModal(produto)}>
                                                            Editar
                                                        </button>
                                                        <button 
                                                            className="btn btn-danger col-auto"
                                                            onClick={() => this.excluirProduto(produto.id)}>
                                                            Exlcuir
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    )
                                })
                            }
                        </div>
                    </div>
                </div>
                <ToastContainer 
                    position="bottom-right"
                    theme="colored"
                />
            </>
        return saida;
    }
}
 
export default Listagem;

ReactDOM.render(<Listagem />, document.getElementById('root'));