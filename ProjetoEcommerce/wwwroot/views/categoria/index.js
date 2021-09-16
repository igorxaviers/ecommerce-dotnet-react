var indexCat = {
    cadastrar: function () {
        let forms = document.getElementById("form-categoria").elements;
        
        let dados = {
            id: 0,
            nome: forms['nome'].value,
        }

        HTTPClient.post("Admin/Categoria/Cadastrar", dados)
        .then(resp => {
            return resp.json();
        })
        .then(resp => {
            window.location.href = resp.url;
            alert(resp.msg);
        })
        .catch((e) => {
            console.log("Deu erro.", e);
        });
    },
    cadastro:{}
}