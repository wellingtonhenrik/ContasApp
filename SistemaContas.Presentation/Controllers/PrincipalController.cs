using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        private readonly ContaRepository _contaRepository;
        public PrincipalController()
        {
            _contaRepository = new ContaRepository();
        }
        //GET: Principal/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
        public JsonResult ConsutarContas(DateTime? dtInicio, DateTime? dtFim)
        {
            try
            {
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);
                DateTime data = DateTime.Today;

                //caso não passe filtro pega do dia primero ao ultimo dia do mes
                dtInicio = dtInicio.HasValue ? dtInicio: new DateTime(data.Year, data.Month, 1);
                dtFim = dtFim.HasValue ? dtFim : new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
               
                var contas = _contaRepository.GetByUserIdAndDatas(auth.Id, dtInicio, dtFim);

                var totalTipos = contas.GroupBy(c => c.Categoria?.Tipo)
                    .Select(c => new
                    {
                        Tipo = c.Key.ToString(), //nome do tipo (Receitas ou Despesas)
                        Total = c.Sum(c=>c.Valor) //somatório do valor de cada conta
                    }).ToList();

                var totalDespesas = contas.Where(a => a.Categoria.Tipo == Data.Enums.TipoCategoria.Despesas)
                    .GroupBy(a => a.Categoria?.Nome)
                    .Select(a => new
                    {
                       Nome = a.Key.ToString(), //nome da categoria
                       Total = a.Sum(a=>a.Valor) //Somatório do valor de cada conta
                    }).ToList();

                return Json(new {totalTipos, totalDespesas});
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
