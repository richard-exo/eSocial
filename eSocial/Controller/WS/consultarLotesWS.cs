using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using eSocial.Model.Eventos.Retorno;

namespace eSocial.Controller.WS {
   public sealed partial class consultarLotesEventosWS : cBase {

      wsConsultarLoteEventos.ServicoConsultarLoteEventosClient oWs;

      public consultarLotesEventosWS(enTpAmb tpAmb, X509Certificate2 certificado) {

         string sEndPointAdd = ConfigurationManager.AppSettings[(tpAmb.Equals(enTpAmb.producaoRestrita_2) ? "addrWsConsultarLoteTeste" : "addrWsConsultarLote")];

         oWs = new wsConsultarLoteEventos.ServicoConsultarLoteEventosClient("Servicos.Empregador_ServicoConsultarLoteEventos", new EndpointAddress(sEndPointAdd));
         oWs.ClientCredentials.ClientCertificate.Certificate = certificado;
      }

      public retProcessamentoLote consultar(string protocoloEnvio) {

         XNamespace ns = "http://www.esocial.gov.br/schema/lote/eventos/envio/consulta/retornoProcessamento/" + ConfigurationManager.AppSettings["vLayoutConsultaWS"];

         xml =
               new XElement(ns + "eSocial",
               new XElement(ns + "consultaLoteEventos",
               new XElement(ns + "protocoloEnvio", protocoloEnvio)));

         try {
            return new retProcessamentoLote(oWs.ConsultarLoteEventos(xml), protocoloEnvio);
         }
         catch (Exception e) { addError("controller.WS.consultarLotesEventosWS", e.Message); return null; }
      }
   }
}
