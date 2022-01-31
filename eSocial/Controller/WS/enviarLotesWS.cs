using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using eSocial.Model.Eventos.Retorno;
using eSocial.Model.Eventos.XML;
using eSocial.Model;

namespace eSocial.Controller.WS {

   public sealed partial class enviarLotesEventosWS : cBase {

      XElement xmlEnv;
      sLote _lote;
      enTipoEvento _tipo;

      wsEnviarLoteEventos.ServicoEnviarLoteEventosClient oWs;

      public enviarLotesEventosWS(sLote lote, enTipoEvento tipoEvento) {

         _lote = lote;
         _tipo = tipoEvento;
         enTpAmb tpAmb = lote.eventos.ToList().First().tpAmb;

         string sEndPointAdd = ConfigurationManager.AppSettings[(tpAmb.Equals(enTpAmb.producaoRestrita_2) ? "addrWsEnviarLoteTeste" : "addrWsEnviarLote")];

         oWs = new wsEnviarLoteEventos.ServicoEnviarLoteEventosClient("WsEnviarLoteEventos", new EndpointAddress(sEndPointAdd));
         oWs.ClientCredentials.ClientCertificate.Certificate = lote.certificado;

      }

      public retEnvioLoteEventos enviar() {

         XNamespace ns = "http://www.esocial.gov.br/schema/lote/eventos/envio/" + ConfigurationManager.AppSettings["vLayoutEnvioWS"];

         xmlEnv =
         new XElement(ns + "eSocial",

         new XElement(ns + "envioLoteEventos",

         new XAttribute("grupo", _tipo.GetHashCode()),

         new XElement(ns + "ideEmpregador",
         new XElement(ns + "tpInsc", _lote.ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", _lote.ideEmpregador.nrInsc)),

         new XElement(ns + "ideTransmissor",
         new XElement(ns + "tpInsc", _lote.ideTransmissor.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", _lote.ideTransmissor.nrInsc)),

         new XElement(ns + "eventos",
         from e in _lote.eventos
         select new XElement(ns + "evento", new XAttribute("Id", e.id), e.eventoAssinadoXML))
         ));

         try { return new retEnvioLoteEventos(xmlEnv, oWs.EnviarLoteEventos(xmlEnv), _lote); }
         catch (Exception e) { addError("controller.WS.enviarLotesEventosWS", e.Message); return null; }
      }
      public sLote empresa { get { return _lote; } }
   }
}