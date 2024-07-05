using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
   public class s3000 : bEvento_XML {

      public s3000(string sID) : base("evtExclusao", "infoExclusao", "v_S_01_02_00") {

         id = sID;

         infoExclusao = new sInfoExclusao();
         infoExclusao.ideTrabalhador = new sInfoExclusao.sIdeTrabalhador();
         infoExclusao.ideFolhaPagto = new sInfoExclusao.sIdeFolhaPagto();

      }

      public override XElement genSignedXML(X509Certificate2 cert) {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
         new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
         new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
         new XElement(ns + "verProc", ideEvento.verProc));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // infoExclusao
         xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

         new XElement(ns + "tpEvento", infoExclusao.tpEvento),
         new XElement(ns + "nrRecEvt", infoExclusao.nrRecEvt),

         // ideTrabalhador 0.1
         opElement("ideTrabalhador", infoExclusao.ideTrabalhador.cpfTrab,
         new XElement(ns + "cpfTrab", infoExclusao.ideTrabalhador.cpfTrab)),

         // ideFolhaPagto 0.1
         opElement("ideFolhaPagto", infoExclusao.ideFolhaPagto.perApur,
         //new XElement(ns + "indApuracao", infoExclusao.ideFolhaPagto.indApuracao),
         opTag("indApuracao", infoExclusao.ideFolhaPagto.indApuracao),
         //new XElement(ns + "perApur", infoExclusao.ideFolhaPagto.perApur)));
         opTag("perApur", infoExclusao.ideFolhaPagto.perApur)));

         return x509.signXMLSHA256(xml, cert);

      }

      #region ********************************************************************************************************************************************************* Structs

      public sInfoExclusao infoExclusao;
      public struct sInfoExclusao {
         public string tpEvento, nrRecEvt;

         public sIdeTrabalhador ideTrabalhador;
         public struct sIdeTrabalhador { public string cpfTrab, nisTrab; }

         public sIdeFolhaPagto ideFolhaPagto;
         public struct sIdeFolhaPagto { public string indApuracao, perApur; }
      }

      #endregion
   }
}