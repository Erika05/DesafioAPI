using RestSharp;
using DesafioAPI.Helpers;
using System.Collections.Generic;

namespace DesafioAPI.Bases
{
    public class RequestBase
    {
        #region Parameters
        protected string jsonBody = null;

        protected string urlMantis = Properties.Settings.Default.URLMANTIS;

        protected string urlSpotify = Properties.Settings.Default.URLSPOTIFY;

        protected string urlGerarTokenSpotify = Properties.Settings.Default.URLGERARTOKENSPOTIFY;

        protected string requestService = null;

        protected Method method;

        protected bool httpBasicAuthenticator = false;

        protected bool ntlmAuthenticator = false;

        protected bool apiSpotfy = false;

        protected bool apiGerarTokenSpotfy = false;

        //protected string accessToken;

        protected IDictionary<string, string> headers = new Dictionary<string, string>()
        {
            //Dicionário de headeres deve ser iniciado com os headers comuns a todos os métodos da API
            
            { "Content-Type", "application/json"},     
        };

        protected IDictionary<string, string> parametrosBody = new Dictionary<string, string>()
        {
            
        };

        protected IDictionary<string, string> cookies = new Dictionary<string, string>()
        {
            //Dicionário de cookies deve ser iniciado com os headers comuns à todas os métodos da API
        };

        protected IDictionary<string, string> parameters = new Dictionary<string, string>();

        protected bool parameterTypeIsUrlSegment = true;
        #endregion

        #region Actions
        public IRestResponse<dynamic> ExecuteRequest()
        {
            IRestResponse<dynamic> response;

            if (apiSpotfy) {
                response = RestSharpHelpers.ExecuteRequest(urlSpotify, requestService, method, headers, parametrosBody, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);

                ExtentReportHelpers.AddTestInfo(urlSpotify, requestService, method.ToString(), headers, parametrosBody, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response);

            }
            else if(apiGerarTokenSpotfy)
            {
                response = RestSharpHelpers.ExecuteRequest(urlGerarTokenSpotify, requestService, method, headers, parametrosBody, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);

               // ExtentReportHelpers.AddTestInfo(urlGerarTokenSpotify, requestService, method.ToString(), headers, parametrosBody, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response);
            }
            else
            {                
                response = RestSharpHelpers.ExecuteRequest(urlMantis, requestService, method, headers, parametrosBody, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);

                ExtentReportHelpers.AddTestInfo(urlMantis, requestService, method.ToString(), headers, parametrosBody, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response);
            }

            return response;
        }

        public void RemoveHeader(string header)
        {
            headers.Remove(header);           
        }

        public void RemoveCookie(string cookie)
        {
            cookies.Remove(cookie);
        }

        public void RemoveParameter(string parameter)
        {
            parameters.Remove(parameter);
        }

        public void SetMethod(Method method)
        {
            this.method = method;
        }
        #endregion
    }
}
