using DesafioAPI.Bases;
using DesafioAPI.Tests.Spotify;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Spotify.PlayList
{
    public class ConsultaArtistaResquests : RequestBase
    {
        HelpersSpotify helpersSpotify = new HelpersSpotify();

        public ConsultaArtistaResquests(string idMusic)
        {
            accessToken = helpersSpotify.AutenticacaoSpotify();
            requestService = "/tracks/" + idMusic;
            method = Method.GET;
            headers.Add("Authorization", "Bearer " + accessToken);
            apiSpotfy = true;
        }
    }
}
