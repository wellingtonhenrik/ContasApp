using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private readonly ContaRepository _contaRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public ContaController()
        {
            _contaRepository = new ContaRepository();
            _categoriaRepository = new CategoriaRepository();
        }

        //GET: Conta/Cadastro
        public IActionResult Cadastro()
        {
            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContaCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);
                    var contaId = Guid.NewGuid();

                    if (_contaRepository.GetByNomeIdCategoria(model.Nome, contaId, model.CategoriaId, auth?.Id) is Conta)
                        throw new Exception("Já existe uma conta com este tipo");

                    var conta = new Conta()
                    {
                        ContaId = contaId,
                        CategoriaId = model.CategoriaId,
                        Data = model.Data,
                        Nome = model.Nome,
                        Observacao = model.Observacao,
                        Valor = model.Valor,
                        UsuarioId = auth?.Id,

                    };

                    _contaRepository.Add(conta);

                    ModelState.Clear();

                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', cadastrada com sucesso";
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        //GET: Conta/Exclusao
        public IActionResult Exclusao(Guid? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = _contaRepository.GetById(id);
                    if (conta is null)
                        throw new Exception("Conta não encontrada");
                    _contaRepository.Delete(conta);

                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', excluida com sucesso";
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public IActionResult Consulta()
        {
            var model = new ContaConsultaViewModel();

            //capturando o primeiro e ultimo dia do mês atual
            var dataAtual = DateTime.Now;
            model.DataInicio = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            model.DataFim = model.DataInicio?.AddMonths(1).AddDays(-1);

            model = ConsultaContasPeridoData(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Consulta(ContaConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = ConsultaContasPeridoData(model);
            }

            return View(model);
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new ContaEdicaoViewModel();

            try
            {
                var conta = _contaRepository.GetById(id);
                if (conta is null)
                    throw new Exception("Conta não encontrada");

                model.Data = conta.Data;
                model.Observacao = conta.Observacao;
                model.Valor = conta.Valor;
                model.ContaId = conta.ContaId;
                model.Nome = conta.Nome;
                model.CategoriaId = conta.CategoriaId;

                ViewBag.Categorias = ObterCategorias();
                return View(model);

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }


            return View();
        }

        [HttpPost]
        public IActionResult Edicao(ContaEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = _contaRepository.GetById(model.ContaId);
                    if (conta is null)
                        throw new Exception("Conta não encontrada");

                    conta.Data = model.Data;
                    conta.Observacao = model.Observacao;
                    conta.Valor = model.Valor;
                    conta.CategoriaId = model.CategoriaId;
                    conta.Nome = model.Nome;
                    model.ContaId = conta.ContaId;

                    _contaRepository.Update(conta);

                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', editada com sucesso";

                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }
            return RedirectToAction("Consulta");
        }

        /// <summary>
        /// Método para popular o campo DropDownLIst de seleção de categorias
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> ObterCategorias()
        {
            var list = new List<SelectListItem>();

            try
            {
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                foreach (var item in _categoriaRepository.GetByUser(auth.Id))
                {
                    var dropDown = new SelectListItem()
                    {
                        Text = $"{item.Nome} ({item.Tipo.Value})",
                        Value = item.CategoriaId.ToString(),
                    };

                    list.Add(dropDown);

                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return list;
        }
        private ContaConsultaViewModel ConsultaContasPeridoData(ContaConsultaViewModel model)
        {
            try
            {
                ViewBag.Categorias = ObterCategorias();
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);
                var contas = new List<Conta>();
                if (model.DataInicio.HasValue && model.DataFim.HasValue)
                {
                    contas = _contaRepository.GetByUserIdAndDatas(auth.Id, model.DataInicio, model.DataFim);
                }
                else
                {
                    contas = _contaRepository.GetByUserId(auth?.Id);
                }
                foreach (var item in contas)
                {
                    var conta = new ContasConsultaResultadoViewModel()
                    {
                        Tipo = item.Categoria?.Tipo.ToString(),
                        Nome = item.Nome,
                        Observacao = item.Observacao,
                        Valor = item.Valor,
                        Data = item.Data,
                        ContaId = item.ContaId,
                        Categoria = item.Categoria?.Nome,
                        StatusConta = item.StatusConta.ToString(),
                    };
                    model?.Resultado?.Add(conta);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            ViewBag.StatusConta = new SelectList(Enum.GetValues(typeof(StatusConta)));
            return model;
        }


        public JsonResult EfetivarConta(Guid id)
        {
            try
            {
                var conta = _contaRepository.GetById(id);
                if (conta is null)
                    return Json("Conta não encontrada");

                if (conta.StatusConta == StatusConta.Paga)
                    return Json("Conta já está paga");

                conta.StatusConta = StatusConta.Paga;

                _contaRepository.Update(conta);
                ViewBag.Categorias = ObterCategorias();

                return Json("Conta efetivada com sucesso");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}
