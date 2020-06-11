using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Tarefas;
using System.Collections.Generic;
using DesafioAPI.Requests.Usuario;
using DesafioAPI.Tests.Mantis.Projeto;
using DesafioAPI.TestsMantis.Mantis.Usuario;

namespace DesafioAPI.Tests.Mantis.Usuario
{
    [TestFixture]
    public class DeletaUsuariosTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        HelpersUsuarios helpersUsuarios = new HelpersUsuarios();

        [Test]
        public void DeletarUsuario()
        {
            #region Parameters Cadastro Tarefa
            string nomeUsuario = "nome user";
            string nomeReal = "nome real user";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "NoContent";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string idUsuario =  helpersUsuarios.PreparaBaseCadastradoUsuario(nomeUsuario, senha, nomeReal, email);
            DeletaUsuarioRequest deletaUsuarioRequest = new DeletaUsuarioRequest(idUsuario);
            IRestResponse<dynamic> response = deletaUsuarioRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(string.Empty, UsuarioDBSteps.VerificaUsuarioExiste(nomeUsuario));
            });
        }
    }
}