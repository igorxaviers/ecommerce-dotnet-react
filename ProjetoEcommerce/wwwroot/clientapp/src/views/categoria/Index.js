import React from 'react';
import ReactDom from 'react-dom';
import axios from 'axios';
import {toast, ToastContainer} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
class CadastroCategoria extends React.Component {
    constructor(props) {
        super(props);
        this.state = { 
            nome: '',
        }
    }

    // isValid = () => {
    //     for (let key in this.state) {
    //         if (this.state[key] === '') {
    //             return false;
    //         }
    //     }
    // }

    cadastrar = () => {
        // if(this.isValid()) {

        // }
        const categoria = {
            nome: this.state.nome
        }
        axios.post('Categoria/Cadastrar', categoria)
        .then(res => {
            console.log(res.data);
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
        })
        .catch(error => {
            this.setState({
                erro: error.data.mensagem
            });
        });
    }

    render = () => { 
        let saida =
        <>
            <div className="card o-hidden border-0 shadow-lg">
                <div className="card-body p-0">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="p-5">
                                <div className="text-left">
                                    <h1 className="h4 text-gray-900 mb-4">Cadastro de Categoria</h1>
                                </div>
                                <form className="user" id="form-categoria">
                                    <div className="form-group">
                                        <label>Nome da categoria</label>
                                        <input 
                                            type="text" 
                                            className="form-control" 
                                            placeholder="Nome"
                                            value={this.state.categoria.nome}
                                            onChange={(e)=>this.setState({nome: e.target.value})}/>
                                    </div>
                                    <button 
                                        type="button" 
                                        onclick="indexCat.cadastrar()" 
                                        className="btn btn-primary w-100 mt-5"
                                        onClick={this.cadastrar}
                                        >Cadastrar categoria</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <ToastContainer/>
        </>;
        return (saida);
    }
}
 
export default CadastroCategoria;

ReactDom.render(<CadastroCategoria/>, document.getElementById("root"));