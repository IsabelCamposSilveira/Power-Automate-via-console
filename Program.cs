using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;
using System.Configuration;
using Microsoft.Xrm.Sdk.Query;
using System.Windows.Controls;
using Microsoft.Crm.Sdk.Messages;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Metadata;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleAppDynamics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criando a conexão
            Console.WriteLine("Conectando ao CRM. Aguarde um instante...\n");
            CRM_Connection connection = new CRM_Connection();
            // CrmServiceClient connectionDev = connection.ConectarCRM_DEV();
            CrmServiceClient connectionProd = connection.ConectarCRM_PROD();

            // Chamar as funções
            ChamarFluxoAutomatizadoHTTP(connectionProd).GetAwaiter().GetResult();
        }

        public static async Task ChamarFluxoAutomatizadoHTTP(CrmServiceClient service)
        {
            // Criar o HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Insira a URL DE HTTP POST fornecida ao salvar seu fluxo
                    string urlDoFluxo = "URL AQUI";

                    // Criar objeto com os dados a serem enviados
                    var dados = new
                    {
                        // No exemplo fornecido seria senha e id, porém irá alterar conforme os dados presentes no corpo do seu fluxo
                        senha = "sua_senha",
                        id = "seu_id"
                    };

                    // Serializar o objeto para JSON
                    string jsonDados = JsonConvert.SerializeObject(dados);

                    // Criar o conteúdo da solicitação com o JSON
                    HttpContent content = new StringContent(jsonDados, Encoding.UTF8, "application/json");


                    // Enviar a solicitação HTTP POST para o endpoint do fluxo
                    HttpResponseMessage response = await httpClient.PostAsync(urlDoFluxo, content);

                    // Verificar se a solicitação foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Fluxo chamado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao chamar o fluxo. Código de status: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao chamar o fluxo: " + ex.Message);
                }
            }
        }
    }
}
