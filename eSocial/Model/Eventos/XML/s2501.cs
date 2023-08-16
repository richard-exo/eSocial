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
   public class s2501 : bEvento_XML
   {

      public s2501(string sID) : base("evtContProc", "ideTrab", "v_S_01_01_00")
      {

         id = sID;

         ideEvento = new sIdeEvento();
       
         ideTrab = new sIdeTrab();
         ideTrab.calcTrib = new sIdeTrab.sCalcTrib();
         ideTrab.calcTrib.infoCRContrib = new sIdeTrab.sCalcTrib.sInfoCRContrib();
         ideTrab.infoCRIRRF = new sIdeTrab.sInfoCRIRRF();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
         new XElement(ns + "indRetif", ideEvento.indRetif),
         opTag("nrRecibo", ideEvento.nrRecibo),
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
         new XElement(ns + "perApurPgto", ideProc.perApurPgto),
         opTag("obs", ideProc.obs)
         ));

         // ideTrab 
         xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(
         new XAttribute("cpfTrab", ideTrab.cpfTrab),

         // calcTrib 1 - 360
         from e in lCalcTrib
         select e,

         // infoCRIRRF 0 - 99
         from e in lInfoCRIRRF
         select e

         );

         lCalcTrib = new List<XElement>();
         lInfoCRIRRF = new List<XElement>();

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      #region lCalcTrib

      List<XElement> lCalcTrib = new List<XElement>();
      public void add_calcTrib()
      {
         lCalcTrib.Add(
         new XElement(ns + "calcTrib",
         new XAttribute("perRef", ideTrab.calcTrib.perRef),
         new XAttribute("vrBcCpMensal", ideTrab.calcTrib.vrBcCpMensal),
         new XAttribute("vrBcCp13", ideTrab.calcTrib.vrBcCp13),
         new XAttribute("vrRendIRRF", ideTrab.calcTrib.vrRendIRRF),
         new XAttribute("vrRendIRRF13", ideTrab.calcTrib.vrRendIRRF13)

         //// infoCRContrib 0.99
         //opElement("infoCRContrib", ideTrab.calcTrib.infoCRContrib.tpCR,
         //new XAttribute("tpCR", ideTrab.calcTrib.infoCRContrib.tpCR),
         //new XAttribute("vrCR", ideTrab.calcTrib.infoCRContrib.vrCR)

         //)));

         ));

         ideTrab.calcTrib = new sIdeTrab.sCalcTrib();
      }

      #endregion

      #region lInfoCRIRRF

      List<XElement> lInfoCRIRRF = new List<XElement>();
      public void add_infoCRIRRF()
      {

         lInfoCRIRRF.Add(
         opElement("infoCRIRRF", ideTrab.infoCRIRRF.tpCR,
         new XElement(ns + "tpCR", ideTrab.infoCRIRRF.tpCR),
         new XElement(ns + "vrCR", ideTrab.infoCRIRRF.vrCR)

         ));

         ideTrab.infoCRIRRF = new sIdeTrab.sInfoCRIRRF();
      }

      #endregion

      #region ***************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, nrRecibo, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeProc ideProc;
      public struct sIdeProc
      {
         public string nrProcTrab, perApurPgto, obs;
      }

      public sIdeTrab ideTrab;
      public struct sIdeTrab
      {
         public string cpfTrab;

         public sCalcTrib calcTrib;
         public struct sCalcTrib
         {
            public string perRef, vrBcCpMensal, vrBcCp13, vrRendIRRF, vrRendIRRF13;

            public sInfoCRContrib infoCRContrib;
            public struct sInfoCRContrib
            {
               public string tpCR, vrCR;
            }
         }

         public sInfoCRIRRF infoCRIRRF;
         public struct sInfoCRIRRF
         {
            public string tpCR, vrCR;
         }
      }


      #endregion

      #endregion
   }
}
