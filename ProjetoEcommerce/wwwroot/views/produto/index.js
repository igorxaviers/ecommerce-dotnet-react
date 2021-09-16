var indexProd = {
    cadastrar: function () {
        let forms = document.getElementById("form-produto").elements;
        
        let dados = {
            id: 0,
            nome: forms['nome'].value,
            nome: forms['preco'].value,
            nome: forms['estoque'].value,
            nome: forms['categoria'].value
        }

        HTTPClient.post("Admin/Produto/Cadastrar", dados)
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