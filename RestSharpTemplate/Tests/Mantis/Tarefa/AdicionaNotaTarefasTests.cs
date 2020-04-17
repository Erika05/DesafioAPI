using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class AdicionaNotaTarefasTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AdicionaNotaTarefaRequest adicionaNotaTarefaRequest = new AdicionaNotaTarefaRequest();

        [Test]
        public void AdicionarNotaTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar nota tarefa";
            string descricao = "Descricao tarefa nota";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string nota = "nota teste";
            string statusNota = "private";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody(nota, statusNota);
            IRestResponse<dynamic> response = adicionaNotaTarefaRequest.ExecuteRequest();

            string retornoNota = response.Data["note"]["text"];
            string retornoSatatusNota = response.Data["note"]["view_state"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nota, retornoNota);
                Assert.AreEqual(statusNota, retornoSatatusNota);
            });
        }

        [Test]
        public void NotaTarefaNaoinformada()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar nota tarefa";
            string descricao = "Descricao tarefa nota";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string statusNota = "private";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Issue note not specified.";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody("", statusNota);
            IRestResponse<dynamic> response = adicionaNotaTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }

        [Test]
        public void AdicionarNotaComTempoTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar nota com duracao tarefa";
            string descricao = "Descricao tarefa nota com duracao";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string nota = "nota teste";
            string statusNota = "private";
            string duracao = "00:00:15";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody(nota, statusNota, duracao);
            IRestResponse<dynamic> response = adicionaNotaTarefaRequest.ExecuteRequest();

            string retornoNota = response.Data["note"]["text"];
            string retornoSatatusNota = response.Data["note"]["view_state"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nota, retornoNota);
                Assert.AreEqual(statusNota, retornoSatatusNota);
            });
        }

        [Test]
        public void AdicionarNotaComAnexoTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar nota  com anexo tarefa";
            string descricao = "Descricao tarefa nota com anexo";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string nota = "nota teste";
            string statusNota = "public";
            string duracao = "00:00:15";
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody(nota, statusNota, duracao, nomeAnexo, anexo);
            IRestResponse<dynamic> response = adicionaNotaTarefaRequest.ExecuteRequest();

            string retornoNota = response.Data["note"]["text"];
            string retornoSatatusNota = response.Data["note"]["view_state"]["name"];
            string retornoNomeAnexo = response.Data["issue"]["attachments"][0]["filename"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nota, retornoNota);
                Assert.AreEqual(statusNota, retornoSatatusNota);
                Assert.AreEqual(nomeAnexo, retornoNomeAnexo);
            });
        }
    }
}