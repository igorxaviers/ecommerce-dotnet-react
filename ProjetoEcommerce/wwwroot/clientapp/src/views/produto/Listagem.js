import React from 'react';
import ReactDOM from 'react-dom';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Listagem.css';

class Listagem extends React.Component {

    constructor(props) {
        super(props);
        this.state = {  
            produtos: [],
            loadging: false
        }
    }

    componentDidMount = () => {
        this.setState({ loading: true });
        axios.get('Listar')
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
        axios.delete('Excluir/' + id)
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
                console.log('uai');
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

    showLoading = () => {
        //return 4 times the same div

    }

    render() { 
        let saida = 
            <>
                <h1>Listagem de Produtos</h1>
                <div className="p-5">
                    <div className="col-12">
                        <div className="row border-0 bg-light rounded my-5 ">
                            {
                                this.state.loading 
                                ? 
                                Array.from(Array(4).keys()).map(i =>
                                    <div className="produto loading col-8 p-3 mb-3 bg-white shadow-sm row flex-wrap align-items-center">
                                    <div className="col-auto placeholder-glow">
                                        <div className="placeholder " style={{height: 220, width: 220, borderRadius: 10}}> </div>
                                    </div>
                                    <div className="col row ps-5 placeholder-glow">
                                        <h2 className="placeholder mb-4 rounded-3"></h2>
                                        <div className="p-0">
                                            <span className="placeholder mb-3 rounded-3" style={{height: 35, width: 100}}></span>
                                        </div>
                                        <div className="p-0">
                                            <span className="placeholder mb-3 rounded-3" style={{height: 35, width: 200}}></span>
                                        </div>
                                        <div className="p-0 mb-4">
                                            <span className="placeholder mb-3 rounded-3" style={{height: 35, width: 240}}></span>
                                        </div>
                                        <div className="p-0 row justify-content-end">
                                            <span className="placeholder w-10 mb-2 me-3 rounded-3" style={{width: 80, height: 35}}></span>
                                            <span className="placeholder w-10 mb-2 me-3 rounded-3" style={{width: 80, height: 35}}></span>
                                        </div>
                                    </div>
                                </div>
                                )
                                : this.state.produtos.map(produto => {
                                    return (
                                        <div className="produto col-8 p-3 mb-3 bg-white shadow-sm row flex-wrap align-items-center" key={produto.id}>
                                            <div className="col-auto">
                                                <img 
                                                    src="https://via.placeholder.com/200"
                                                    className="col-auto"
                                                    style={{height: 220, width: 220, borderRadius: 10}}
                                                />
                                            </div>
                                            <div className="col row ps-3">
                                                <h2>{produto.nome}</h2>
                                                <p className="preco">
                                                    <i class="fas fa-dollar-sign"></i>
                                                    R$ {produto.preco}
                                                </p>
                                                <div className="p-0">
                                                    <p className={`badge-estoque ${this.getColor(produto.estoque)}`}>
                                                        <i class="fas fa-boxes"></i>
                                                        Qtd. Estoque <span className="estoque">{produto.estoque}</span>
                                                    </p>
                                                </div>
                                                <p>
                                                    <i class="fas fa-list"></i> 
                                                    Categoria: <strong>{produto.categoria.nome}</strong>
                                                </p>
                                                <div className="row justify-content-end">
                                                    <button 
                                                        className="btn btn-primary col-auto mr-3"
                                                        onClick={() =>console.log('editas') }>
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