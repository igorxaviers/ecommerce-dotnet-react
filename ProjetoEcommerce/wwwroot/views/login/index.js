var indexLogin = {
    login: function () {
        let forms = document.getElementById("form-login").elements;
        
        let dados = {
            login: forms['login'].value,
            senha: forms['senha'].value,
        }

        HTTPClient.post("Admin/Login/Autenticar", dados)
        .then(resp => {
            return resp.json();
        })
        .then(resp => {
            document.getElementById('msg').innerHTML = resp.msg;
        })
        .catch((e) => {
            console.log("Deu erro.", e);
            document.getElementById('msg').innerHTML = "Erro";
        });
    },
    cadastro:{}
}