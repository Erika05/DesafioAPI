﻿using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Projeto;

namespace DesafioAPI.Tests.Usuario
{
    [TestFixture]
    public class CadastraUsuariosTests : TestBase
    {
        CadastraUsuarioRequest cadastraUsuarioRequest = new CadastraUsuarioRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        HelpersUsuarios helpersUsuarios = new HelpersUsuarios();

        [Test]
        public void CadastrarUsuario()
        {
            #region Parameters Cadastro Tarefa
            string nome = "nome user";
            string nomeReal = "nome real";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            helpersUsuarios.PreparaBaseDeletadoUsuario(nome);

            cadastraUsuarioRequest.SetJsonBody(nome, senha, nomeReal, email);
            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();

            string retornoUserName = response.Data["user"]["name"];
            string retornoRealName = response.Data["user"]["real_name"];
            string retornoEmail = response.Data["user"]["email"];
            string retornoProjeto = response.Data["user"]["projects"][0]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nome, retornoUserName);
                Assert.AreEqual(nomeReal, retornoRealName);
                Assert.AreEqual(email, retornoEmail);
                Assert.AreEqual(projeto, retornoProjeto);
            });
        }

        [Test]
        public void NomeUsuarioNaoInformado()
        {
            #region Parameters Cadastro Tarefa
            string nomeReal = "nome real";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Invalid username";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraUsuarioRequest.SetJsonBody("", senha, nomeReal, email);
            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, retornoMensagemErro);
            });
        }

        [Test]
        public void UsuarioJaCadastrado()
        {
            #region Parameters Cadastro Tarefa
            string nome = "nome user";
            string nomeReal = "nome real";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Username '"+ nome + "' already used.";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string idUsuario = helpersUsuarios.PreparaBaseCadastradoUsuario(nome, senha, nomeReal, email);
            cadastraUsuarioRequest.SetJsonBody(nome, senha, nomeReal, email);
            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }

        [Test]
        public void EmailJaCadastrado()
        {
            #region Parameters Cadastro Tarefa
            string nome = "nome user";
            string nomeReal = "nome real";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Email '" + email + "' already used.";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string idUsuario = helpersUsuarios.PreparaBaseCadastradoUsuario(nome, senha, nomeReal, email);
            cadastraUsuarioRequest.SetJsonBody("erika", senha, nomeReal, email);
            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }
    }
}