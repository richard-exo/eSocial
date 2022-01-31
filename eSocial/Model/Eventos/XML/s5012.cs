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

namespace eSocial.Model.Eventos.XML
{
   public class s5012 : bEvento_XML
   {
      public s5012(string sID) : base("evtIrrf")
      {
         id = sID;

         ideEvento = new sIdeEvento();
         ideEmpregador = new sIdeEmpregador();
         infoIRRF = new sInfoIRRF();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {
         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
         new XElement(ns + "perApur", ideEvento.perApur));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // infoIRRF
         xml.Elements().ElementAt(0).Add(
         opTag("nrRecArqBase", ideEvento.nrRecArqBase),
         new XElement(ns + "indExistInfo", ideEvento.indExistInfo));

         // infoCRContrib 0.9
         xml.Elements().ElementAt(0).Add(
         from e in lInfoCRContrib
         select e);         

         return x509.signXMLSHA256(xml, cert);
      }

      #region infoCRContrib  

      List<XElement> lInfoCRContrib = new List<XElement>();
      public void add_infoCRContrib()
      {
         lInfoCRContrib.Add(

         new XElement(ns + "lInfoCRContrib", 
         new XElement(ns + "tpCR", infoIRRF.infoCRContrib.tpCR),
         new XElement(ns + "vrCR", infoIRRF.infoCRContrib.vrCR)));

         infoIRRF.infoCRContrib = new sInfoIRRF.sInfoCRContrib();  
      }
      #endregion
 
      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string nrRecArqBase, perApur;
         public string indExistInfo;
      }

      public new sIdeEmpregador ideEmpregador;
      public struct sIdeEmpregador
      {
         public string tpInsc, nrInsc;
      }

      public sInfoIRRF infoIRRF;
      public struct sInfoIRRF
      {
         public sInfoCRContrib infoCRContrib;
         public struct sInfoCRContrib
         {
            public string tpCR, vrCR;
         }
      }
      #endregion
   }
}