using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Teste_UxComex.Models;
using Teste_UxComex.Repositories;
using Teste_UxComex.Repositories.Interfaces;

namespace Teste_UxComex.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public EnderecoController(IPessoaRepository pessoaRepository, IEnderecoRepository enderecoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _enderecoRepository = enderecoRepository;
        }



        public async Task<IActionResult> Index(int pessoaId)
        {
            var enderecos = await _enderecoRepository.GetByPessoaIdAsync(pessoaId);
            return View(enderecos);  
        }

        
        public IActionResult CreateEndereco(int pessoaId)
        {
            var endereco = new Endereco { PessoaId = pessoaId };
            return View(endereco);  
        }
        [HttpPost]
        public async Task<IActionResult> CreateEndereco([FromBody] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Dados do endereço: " + JsonConvert.SerializeObject(endereco)); 
                var success = await _enderecoRepository.AddAsync(endereco);
                if (success)
                {
                    return Json(new
                    {
                        success = true,
                        endereco = new
                        {
                            enderecoId = endereco.EnderecoId,
                            enderecoCompleto = endereco.EnderecoCompleto,
                            cep = endereco.CEP,
                            cidade = endereco.Cidade,
                            estado = endereco.Estado
                        }
                    });
                }
            }


            return Json(new
            {
                success = false,
                message = "Não foi possível salvar o endereço. Verifique os dados informados."
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
        {
            if (id != endereco.EnderecoId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var success = await _enderecoRepository.UpdateAsync(endereco);
                if (success)
                    return RedirectToAction("Edit", "Pessoa", new { id = endereco.PessoaId });
            }

            return View(endereco);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);

            if (endereco == null)
                return NotFound();

            var sucesso = await _enderecoRepository.DeleteAsync(id);

            if (!sucesso)
                return BadRequest("Não foi possível excluir o endereço.");

            var pessoa = await _pessoaRepository.GetAsync(endereco.PessoaId);
            return RedirectToAction("Edit", "Pessoa", new { id = pessoa.PessoaId });
        }


    }
}
