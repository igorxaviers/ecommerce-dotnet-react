import React from 'react';
class CadastroCategoria extends Component {
    constructor(props) {
        super(props);
        this.state = { 
            id: null,
            nome: ''
         }
    }

    cadastrar = () => {
        const dados = {
            nome: this.state.nome
        }
        axios.post('/admin/categoria/cadastrar', dados)
        .then(response => {
            console.log(response.data);
            if (response.data.ok) 
                window.location.href = response.data.url;
            else 
            {
                this.setState({
                    erro: response.data.mensagem
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
                                            value={this.state.nome}
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
        </>;
        return (saida);
    }
}
 
export default CadastroCategoria;