import React from 'react';
import ReactDOM from 'react-dom';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Listagem.css';

class CadatroEdit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
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

    componentDidMount() {
        if(this.props.pai.state.produto != null) {
            this.setState({
                id: this.props.pai.state.id,
                nome: this.props.pai.state.nome,
                preco: this.props.pai.state.preco,
                estoque: this.props.pai.state.estoque,
                categoria_id: this.props.pai.state.categoria_id
            });
        }
    }   

    handleChange = e => {
        const target = e.target;
        const value = target.value;
        const name = target.name;
        this.props.pai.setState({
            [name]: value
        });
    }

    handleSelect = e => {
        const categoria_id = parseInt(e.target.value);
        this.props.pai.setState({ categoria_id });
    }
    
    
    fecharModal = () => {
        this.props.pai.setState({ show: false });
        this.props.pai.setState({ produto: null });
    }

    render() {
        const saida =
            <>
                <div className={'col-lg-4 col-md-6 col-12 modal-produto '+ (this.props.pai.state.show ? 'show' : '')}>
                    <div className="modal-header">
                        <h5 className="modal-title">Cadastro de Produto</h5>
                        <button type="button" className="close">
                            <i 
                                className="fas fa-times"
                                onClick={this.fecharModal}>
                            </i>
                        </button>
                    </div>
                    <div className="modal-body">
                        <form className="user" id="form-produto">
                            <div className="form-group">
                                <label>Nome do produto</label>
                                <input
                                    type="text"
                                    name="nome"
                                    className="form-control"
                                    placeholder="Nome do produto"
                                    value={this.props.pai.state.nome}
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
                                    value={this.props.pai.state.preco}
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
                                    value={this.props.pai.state.estoque}
                                    onChange={this.handleChange}
                                    required />
                            </div>
                            <div>
                                <label>Categoria do produto</label>
                                <select
                                    className="form-select"
                                    name="categoria"
                                    value={this.props.pai.state.categoria_id}
                                    onChange={this.handleSelect}>
                                    {
                                        this.state.categorias.map(categoria => {
                                            return <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                        })
                                    }
                                </select>
                            </div>

                            <div className="modal-footer border-top mt-4 mx-0">
                                <button
                                    type="button"
                                    className={"btn bg-light col-auto" + (this.state.loading ? "disable" : "")}
                                    onClick={this.fecharModal}>
                                    Fechar
                                </button>
                                <button
                                    type="button"
                                    className={"btn btn-lg btn-primary col-auto px-5" + (this.state.loading ? "disable" : "")}
                                    onClick={this.props.pai.salvarProduto}>
                                    {this.state.loading ? <i className="fas fa-circle-notch fa-spin"></i> : ""}
                                    Salvar
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
                <ToastContainer />
            </>
        return (saida);
    }
}
 
export default CadatroEdit;