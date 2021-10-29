var indexCat = {
    cadastrar: function () {
        let forms = document.getElementById("form-categoria").elements;
        
        let dados = {
            id: 0,
            nome: forms['nome'].value,
        }
        console.log(dados);

        HTTPClient.post("Admin/Categoria/Cadastrar", dados)
        .then(resp => {
            return resp.json();
        })
        .then(resp => {
            console.log(resp);
            alert(resp.mensagem);
        })
        .catch((e) => {
            console.log("Deu erro.", e);
        });
    }
}