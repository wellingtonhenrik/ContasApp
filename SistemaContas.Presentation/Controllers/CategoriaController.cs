using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository _repository;
        public CategoriaController()
        {
            _repository = new CategoriaRepository();
        }
        //GET: Categoria/Cadastro
        public IActionResult Cadastro()
        {
            //carregando uma ViewBag com a opção que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));

            return View();
        }

        //POST: Categorias/Cadastro
        [HttpPost]
        public IActionResult Cadastro(CategoriasCadastroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //deserializar os dados do usuário
                    var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity?.Name);
                    var categoriaId = Guid.NewGuid();
                    if (_repository.GetByNomeUserTipoId(model.Nome, auth.Id, model.Tipo, categoriaId) is Categoria)
                        throw new Exception("Já existe essa categoria para este usuario");

                    var categoria = new Categoria()
                    {
                        CategoriaId = categoriaId,
                        Nome = model.Nome,
                        Tipo = model.Tipo,
                        UsuarioId = auth?.Id
                    };

                    _repository.Add(categoria);

                    ModelState.Clear();

                    TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', cadastrada com sucesso";
                }

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            //carregando uma ViewBag com a opção que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));

            return View(); //voltando para a página de cadastro
        }

        //GET: Categoria/Consulta
        public IActionResult Consulta()
        {
            var model = new List<CategoriaConsultaViewModel>();

            try
            {
                //deserializar os dados do usuário
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity?.Name);

                foreach (var item in _repository.GetByUser(auth.Id))
                {
                    model.Add(new CategoriaConsultaViewModel
                    {
                        Nome = item?.Nome,
                        Tipo = item?.Tipo,
                        CategoriaId = item.CategoriaId,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            //eniando a lista para a página
            return View(model);
        }

        //GET: Categoria/Exclusao
        public IActionResult Exclusao(Guid? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = _repository.GetById(id);

                    if (categoria is null)
                        throw new Exception("Categoria nã encontrada");

                    _repository.Delete(categoria);

                    TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', excluida com sucesso";
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            return RedirectToAction("Consulta");
        }

        //GET Categoria/Editar
        public IActionResult Edicao(Guid? id)
        {
            var model = new CategoriasEdicaoViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = _repository.GetById(id);
                    if (categoria is null)
                        throw new Exception("Categoria não encontrada");
                    model.Nome = categoria.Nome;
                    model.Tipo = categoria.Tipo;
                    model.CategoriasId = categoria.CategoriaId;
                    ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            //carregando uma ViewBag com a opção que serão exibidas no campo DropDownList

            return View(model);
        }

        [HttpPost]
        //POST: Categoria/Edicao
        public IActionResult Edicao(CategoriasEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = _repository.GetById(model.CategoriasId);

                    if (categoria is null)
                        throw new Exception("Categoria não encontrada");

                    categoria.Tipo = model.Tipo;
                    categoria.Nome = model.Nome;

                    _repository.Update(categoria);
                    TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', atualizada com sucesso";
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            return RedirectToAction("Consulta");
        }
    }
}
