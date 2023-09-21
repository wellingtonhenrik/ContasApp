using Bogus;
using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Messages.Models;
using ContasApp.Messages.Services;
using ContasApp.Presentation.Helpers;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using SistemaContas.Presentation.Models;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace SistemaContas.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public AccountController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        //GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: /Account/Login
        [HttpPost] //Método para receber o SUBMIT POST do formulário
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuário no banco de dados através do email e da senha
                    var usuario = _usuarioRepository.GetByEmailAndSenha(model.Email, MD5Helper.Encrypt(model.Senha));

                    //verifica se o usuário foi encontrado
                    if (usuario is Usuario)
                    {
                        //criando um objeto de dados que será gravado no cookie e  serializando 
                        //para JSON
                        var authJson = JsonConvert.SerializeObject(new AuthViewModel()
                        {
                            Id = usuario.UsuarioId,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now,
                        });

                        //criar o conteúdo do cookie de autenticação (identificação)
                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, authJson)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravando o cookie de autenticação
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Dashboard", "Principal");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Acesso negado.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            return View();
        }

        //GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            try
            {
                //Verificando se já existe um usuario com o email passado
                //
                if (_usuarioRepository.GetByEmail(model.Email) is Usuario)
                {
                    ModelState.AddModelError("Email", "O email informado já está cadastrado para outro usuário");
                }
                else
                {
                    //verificando se todos os campos enviados pelo model
                    //passaram nas regras de validação
                    if (ModelState.IsValid)
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = MD5Helper.Encrypt(model.Senha),
                        };

                        _usuarioRepository.Add(usuario);

                        //gerando uma mensagem
                        TempData["MensagemSucesso"] = "Parabéns, seu conta de usuário foi criada com sucesso.";

                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MessagemErro"] = ex.Message;
            }

            return View();
        }

        //GET: /Account/PasswordRecorver
        public IActionResult PasswordRecorver()
        {
            return View();
        }

        //POST: /Account/PasswordRecover
        [HttpPost]
        public IActionResult PasswordRecorver(AccountPasswordRecoverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _usuarioRepository.GetByEmail(model.Email);

                    if (usuario == null)
                    {
                        TempData["MensagemErro"] = "Usuário não encontrado. Verifique o email informado";
                    }

                    Faker faker = new Faker();

                    usuario.Senha = $"@{faker.Internet.Password(8)}{new Random(999).Next()}";

                    var emailMessageModel = new EmailMessagemModel()
                    {
                        Corpo = $"Prezado, {usuario.Nome}, \nSua nova senha de acesso é: {usuario.Senha}\nAtt\nEquipe Contas App",
                        Assunto = "Recuperação de senha de usuário - Contas App",
                        EmailDestinatario = usuario.Email,
                    };

                    EmailMessageService.Send(emailMessageModel);

                    usuario.Senha = MD5Helper.Encrypt(usuario.Senha);

                    _usuarioRepository.Update(usuario);

                    ModelState.Clear();
                    TempData["MensagemSucesso"] = "Recuperação de senha realizada com sucesso. Verifique sua caixa de email";

                    return View();

                }catch(Exception ex)
                {
                    TempData["MensagemErro"] = ex.Message;
                }
            }

            return View();
        }

        //GET: Accont/Logout
        public IActionResult Logout()
        {
            //apagar o cookie de autenticação do AspNet
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar para a página /Account/Login
            return RedirectToAction("Login", "Account");
        }
    }
}
