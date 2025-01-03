document.addEventListener("DOMContentLoaded", function () {
    const inputCep = document.getElementById("cep");
    const inputEndereco = document.getElementById("endereco");
    const inputCidade = document.getElementById("cidade");
    const inputEstado = document.getElementById("estado");
    const inputpessoa = document.getElementById("pessoa"); 
    const salvarEnderecoBtn = document.getElementById("salvarEndereco");
    const form = document.getElementById("formEndereco");


    function buscarEnderecoPorCep(cep) {
        if (!cep) {
            alert("Por favor, insira um CEP válido.");
            return;
        }

        const url = `https://viacep.com.br/ws/${cep}/json/`;

        fetch(url)
            .then((response) => {
                if (!response.ok) throw new Error("Erro ao buscar o CEP.");
                return response.json();
            })
            .then((data) => {
                if (data.erro) {
                    alert("CEP não encontrado!");
                } else {
                    inputCep.value = data.cep || "";
                    inputEndereco.value = data.logradouro || "";
                    inputCidade.value = data.localidade || "";
                    inputEstado.value = data.uf || "";
                }
            })
            .catch((error) => {
                console.error(error);
                alert("Ocorreu um erro ao buscar o CEP.");
            });
    }

    // Evento para buscar o CEP ao sair do campo
    inputCep.addEventListener("blur", function () {
        const cep = inputCep.value.trim();
        if (cep) {
            buscarEnderecoPorCep(cep);
        }
    });

    
    salvarEnderecoBtn.addEventListener("click", function () {
        const endereco = {
            pessoaId: inputpessoa.value,
            cep: inputCep.value,
            enderecoCompleto: inputEndereco.value,
            cidade: inputCidade.value,
            estado: inputEstado.value
        };

        fetch("/Endereco/CreateEndereco", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(endereco)
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Erro ao salvar o endereço.");
                }
                return response.json();
            })
            .then((data) => {
                if (data.success) {
                    alert("Endereço salvo com sucesso!");
                    const tabela = document.querySelector("table tbody");
                    const novaLinha = `
                    <tr>
                        <td>${data.endereco.enderecoCompleto}</td>
                        <td>${data.endereco.cep}</td>
                        <td>${data.endereco.cidade}</td>
                        <td>${data.endereco.estado}</td>
                        <td>
                            <a href="/Endereco/Edit/${data.endereco.enderecoId}">Editar</a> |
                            <a href="/Endereco/Delete/${data.endereco.enderecoId}" onclick="return confirm('Tem certeza que deseja excluir?');">X</a>
                        </td>
                    </tr>`;
                    tabela.insertAdjacentHTML("beforeend", novaLinha);
                    const modal = document.querySelector("#EnderecoModal");
                    $(modal).modal("hide");
                    form.reset();
                } else {
                    alert("Erro: " + data.message);
                }
            })
            .catch((error) => {
                console.error(error);
                alert("Erro ao processar o endereço.");
            });
    });

});
