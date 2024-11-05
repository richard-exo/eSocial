using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML
{
   public class s2555 : bEvento_XML
   {

      public s2555(string sID) : base("evtConsolidContProc", "ideProc", "v_S_01_02_00")
      {

         id = sID;
         ideEvento = new sIdeEvento();
         ideProc = new sIdeProc();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
         //new XElement(ns + "indRetif", ideEvento.indRetif),
         //opTag("nrRecibo", ideEvento.nrRecibo),
         new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
         new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
         new XElement(ns + "verProc", ideEvento.verProc));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // ideVinculo
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").AddAfterSelf(
         new XElement(ns + "ideProc",
         new XElement(ns + "nrProcTrab", ideProc.nrProcTrab),
         new XElement(ns + "perApurPgto", ideProc.perApurPgto)
         ));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      #region ***************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         //public string indRetif, nrRecibo, verProc;
         public string verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeProc ideProc;
      public struct sIdeProc
      {
         public string nrProcTrab, perApurPgto;
      }


      #endregion

      #endregion
   }
}
