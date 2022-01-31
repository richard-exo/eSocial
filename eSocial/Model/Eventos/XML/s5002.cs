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
   public class s5002 : bEvento_XML {

      public s5002(string sID) : base("evtIrrfBenef") {

         id = sID;

         ideEvento = new sIdeEvento();
         ideTrabalhador = new sIdeTrabalhador();
         infoDep = new sInfoDep();

         infoIrrf = new sInfoIrrf();

         infoIrrf.basesIrrf = new sInfoIrrf.sBasesIrrf();
         infoIrrf.irrf = new sInfoIrrf.sIrrf();

         infoIrrf.idePgtoExt = new sInfoIrrf.sIdePgtoExt();
         infoIrrf.idePgtoExt.idePais = new sInfoIrrf.sIdePgtoExt.sIidePais();
         infoIrrf.idePgtoExt.endExt = new sInfoIrrf.sIdePgtoExt.sEndExt();
      }

      public override XElement genSignedXML(X509Certificate2 cert) {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

         opTag("nrRecArqBase", ideEvento.nrRecArqBase),
         new XElement(ns + "perApur", ideEvento.perApur));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // ideTrabalhador
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideTrabalhador",
         new XElement(ns + "cpfTrab", ideTrabalhador.cpfTrab),

         // infoDep 0.1
         opElement("infoDep", infoDep.vrDedDep,
         new XElement(ns + "vrDedDep", infoDep.vrDedDep)),

         // infoIrrf 1.9
         from e in lInfoIrrf
         select e));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      #region infoIrrf

      List<XElement> lInfoIrrf = new List<XElement>();
      public void add_infoIrrf() {

         lInfoIrrf.Add(

         new XElement(ns + "infoIrrf",
         opTag("codCateg", infoIrrf.codCateg),
         new XElement(ns + "indResBr", infoIrrf.indResBr),

         // basesIrrf 1.99
         from e in lBasesIrrf
         select e,

         // irrf 0.20
         from e in lIrrf
         select e));

         infoIrrf = new sInfoIrrf();
      }
      #endregion

      #region basesIrrf

      List<XElement> lBasesIrrf = new List<XElement>();
      public void add_basesIrrf() {

         lBasesIrrf.Add(

         new XElement(ns + "basesIrrf",
         new XElement(ns + "tpValor", infoIrrf.basesIrrf.tpValor),
         new XElement(ns + "valor", infoIrrf.basesIrrf.valor)));

         infoIrrf.basesIrrf = new sInfoIrrf.sBasesIrrf();
      }
      #endregion

      #region irrf

      List<XElement> lIrrf = new List<XElement>();
      public void add_irrf() {

         lIrrf.Add(

         new XElement(ns + "irrf",
         new XElement(ns + "tpCR", infoIrrf.irrf.tpCR),
         new XElement(ns + "vrIrrfDesc", infoIrrf.irrf.vrIrrfDesc),

         // idePgtoExt 0.1
         opElement("idePgtoExt", infoIrrf.idePgtoExt.idePais.codPais,

         // idePais
         new XElement(ns + "idePais",
         new XElement(ns + "codPais", infoIrrf.idePgtoExt.idePais.codPais),
         new XElement(ns + "indNIF", infoIrrf.idePgtoExt.idePais.indNIF),
         opTag("nifBenef", infoIrrf.idePgtoExt.idePais.nifBenef)),

         // endExt
         new XElement(ns + "endExt",
         new XElement(ns + "dscLograd", infoIrrf.idePgtoExt.endExt.dscLograd),
         opTag("nrLograd", infoIrrf.idePgtoExt.endExt.nrLograd),
         opTag("complem", infoIrrf.idePgtoExt.endExt.complem),
         opTag("bairro", infoIrrf.idePgtoExt.endExt.bairro),
         new XElement(ns + "nmCid", infoIrrf.idePgtoExt.endExt.nmCid),
         opTag("codPostal", infoIrrf.idePgtoExt.endExt.codPostal)))));

         infoIrrf.irrf = new sInfoIrrf.sIrrf();
         infoIrrf.idePgtoExt = new sInfoIrrf.sIdePgtoExt();
         infoIrrf.idePgtoExt.idePais = new sInfoIrrf.sIdePgtoExt.sIidePais();
         infoIrrf.idePgtoExt.endExt = new sInfoIrrf.sIdePgtoExt.sEndExt();
      }

      #endregion

      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento { public string nrRecArqBase, perApur; }

      public sIdeTrabalhador ideTrabalhador;
      public struct sIdeTrabalhador { public string cpfTrab; }

      public sInfoDep infoDep;
      public struct sInfoDep { public string vrDedDep; }

      public sInfoIrrf infoIrrf;
      public struct sInfoIrrf {
         public string codCateg;
         public string indResBr;

         public sBasesIrrf basesIrrf;
         public struct sBasesIrrf {
            public string tpValor;
            public string valor;
         }

         public sIrrf irrf;
         public struct sIrrf {
            public string tpCR;
            public string vrIrrfDesc;
         }

         public sIdePgtoExt idePgtoExt;
         public struct sIdePgtoExt {
            public string tpCR;
            public string vrIrrfDesc;

            public sIidePais idePais;
            public struct sIidePais {
               public string indNIF;
               public string codPais, nifBenef;
            }

            public sEndExt endExt;
            public struct sEndExt { public string dscLograd, nrLograd, complem, bairro, nmCid, codPostal; }

         }
      }
      #endregion
   }
}