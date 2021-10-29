var indexProd = {
    cadastrar: function () {
        let forms = document.getElementById("form-produto").elements;
        
        let dados = {
            id: 0,
            nome: forms['nome'].value,
            preco: forms['preco'].value,
            estoque: forms['estoque'].value,
            categoria: forms['categoria'].value
        }

        console.log(dados);

        HTTPClient.post("Admin/Produto/Cadastrar", dados)
        .then(resp => {
            return resp.json();
        })
        .then(resp => {
            console.log(resp);
            if(resp.url)
                window.location.href = resp.url;
            alert(resp.mensagem, resp);
        })
        .catch((e) => {
            console.log("Deu erro.", e);
        });
    },
    cadastro:{}
}