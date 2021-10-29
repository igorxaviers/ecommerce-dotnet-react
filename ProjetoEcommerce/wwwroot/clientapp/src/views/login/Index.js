import React from 'react';
import ReactDom from 'react-dom';
import axios from 'axios';

class Login extends React.Component {

    constructor() {
        super();
        this.state = {
            login: '',
            senha: '',
            erro: null
        };
    }

    sendLogin = () => {
        const dados = {
            login: this.state.login,
            senha: this.state.senha
        }
        axios.post('/admin/login/autenticar', dados)
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
            <div className="container py-5">
                <div className="row justify-content-center">
                    <div className="col-xl-7 col-lg-12 col-md-7">
                        <div className="card o-hidden border-0 shadow-lg my-5">
                            <div className="card-body p-0">
                                <div className="row mt-5    ">
                                    <div className="col">
                                        <div className="p-5">
                                            <div className="text-center">
                                                <h1 className="h4 text-gray-900 mb-4">Bem-vindo de volta!</h1>
                                            </div>
                                            <form className="user" id="form-login">
                                                <div className="form-group">
                                                    <label>Login</label>
                                                    <input 
                                                        type="text" 
                                                        className="form-control" 
                                                        placeholder="Insira o login..."
                                                        value={this.state.login}
                                                        onChange={(e)=>this.setState({login: e.target.value})}/>
                                                </div>
                                                <div className="form-group">
                                                    <label>Senha</label>
                                                    <input 
                                                        type="password" 
                                                        className="form-control" 
                                                        placeholder="**********"
                                                        value={this.state.senha}
                                                        onChange={(e)=>this.setState({senha: e.target.value})}/>
                                                </div>
                                                
                                                <p>{this.state.erro}</p>

                                                <button 
                                                    type="button" 
                                                    className="btn btn-primary w-100"
                                                    onClick={this.sendLogin}>ENTRAR</button>
                                            </form>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
        return (saida);
    }
}
 
export default Login;
ReactDom.render(<Login/>, document.getElementById("root"));
