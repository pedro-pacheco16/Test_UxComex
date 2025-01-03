using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Runtime.Intrinsics.X86;
using Teste_UxComex.Models;
using Teste_UxComex.Repositories.Interfaces;
using Teste_UxComex.ViewModels;

namespace Teste_UxComex.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PessoaController(IPessoaRepository pessoaRepository, IEnderecoRepository enderecoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _enderecoRepository = enderecoRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            return View(pessoas); 
        }

        
        public IActionResult Create()
        {
            return View(); 
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                var result = await _pessoaRepository.AddAsync(pessoa);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", "Erro ao adicionar pessoa.");
            }
            return View(pessoa); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _pessoaRepository.GetAsync(id);
            if (pessoa == null)
                return NotFound();


            var enderecos = await _enderecoRepository.GetByPessoaIdAsync(id);

            var viewModel = new PessoaViewModel
            {
                Pessoa = pessoa,
                Enderecos = enderecos.ToList() 
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, PessoaViewModel viewModel)
        {
            if (id != viewModel.Pessoa.PessoaId)
            {
                return BadRequest("ID do corpo da requisição não coincide com o parâmetro da URL.");
            }

            viewModel.Enderecos = new();
            if (ModelState.IsValid)
            {
                
                var pessoaResult = await _pessoaRepository.UpdateAsync(viewModel.Pessoa);
                if (!pessoaResult)
                {
                    ModelState.AddModelError("", "Erro ao atualizar pessoa.");
                    return View(viewModel);
                }

                
                var enderecosAtuais = await _enderecoRepository.GetByPessoaIdAsync(id);

                foreach (var endereco in viewModel.Enderecos)
                {
                    if (endereco.EnderecoId == 0) 
                    {
                        endereco.PessoaId = viewModel.Pessoa.PessoaId; 
                        await _enderecoRepository.AddAsync(endereco);
                    }
                    else 
                    {
                        var enderecoExistente = enderecosAtuais.FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);
                        if (enderecoExistente != null)
                        {
                            enderecoExistente.Cidade = endereco.Cidade;
                            enderecoExistente.Estado = endereco.Estado;
                            enderecoExistente.EnderecoCompleto = endereco.EnderecoCompleto;
                            await _enderecoRepository.UpdateAsync(enderecoExistente);
                        }
                    }
                }

                return RedirectToAction(nameof(Index)); 
            }

            
            viewModel.Enderecos = (await _enderecoRepository.GetByPessoaIdAsync(id)).ToList();
            return View(viewModel); 
        }


        public async Task<IActionResult> Delete(int id)
        {
            
            await _pessoaRepository.Delete(id);

         
            return RedirectToAction(nameof(Index));
        }

    }
}
