using DesafioAPI.Bases;
using DesafioAPI.Requests.Spotify.PlayList;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections;
using System.Collections.Generic;

namespace DesafioAPI.Tests.Spotify.PlayList
{
    [TestFixture]
    public class ConsultaPlayListTests : TestBase
    { 
        [Test]
        public void ConsultarTarefa()
        {
            var First = "{'events': [{'name': 'SL Benfica - Porto','homeName': 'SL Benfica', 'awayName': 'Porto','start': '2017-06-09T18:00Z','group': 'LPB','type': 'ET_MATCH','sport': 'BASKETBALL', 'state': 'NOT_STARTED'}] }";

            JObject ja = JObject.Parse(First);

            ConsultaPlayLisRequest consultaPlayListRequests = new ConsultaPlayLisRequest();
            #region Parameters           
            string nomePlayList = "Teste Postman";
            string statusCodeEsperado = "OK";
            #endregion
            IRestResponse<dynamic> response = consultaPlayListRequests.ExecuteRequest();
            string dadosProjeto = response.Data["items"][1]["name"];
            ///  List<string> list = response.Data["items"]["name"];
            //  ja = JObject.Parse(response.Data["items"]["name"]);
            //JArray array = response.Content.re;
            //var e = response.Data["items"];
            //JArray results = JArray.Parse(response.Data["items"][0]["name"]);


            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                   // Assert.IsTrue(verificaPlayList(list, nomePlayList));
                });
        }

        public bool verificaPlayList(List<string> list, string nomePlayList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].Equals(nomePlayList))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
