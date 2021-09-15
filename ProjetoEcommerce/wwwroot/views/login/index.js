var indexLogin = {
    login: function () {
        let forms = document.getElementById("form-login").elements;
        
        let dados = {
            login: forms['login'].value,
            senha: forms['senha'].value,
        }

        console.log(dados);

        HTTPClient.post("Admin/Login/Autenticar", dados)
        .then(resp => {
            console.log(resp.json());
            return resp.json();
        })
        .then(resp => {
            alert(resp.msg);
        })
        .catch((e) => {
            console.log("Deu erro.", e);
        });
    },
    cadastro:{}
}