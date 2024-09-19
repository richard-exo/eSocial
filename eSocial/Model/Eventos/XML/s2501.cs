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

      public s2501(string sID) : base("evtContProc", "ideTrab", "v_S_01_02_00")
      {

         id = sID;

         ideEvento = new sIdeEvento();
       
         ideTrab = new sIdeTrab();
         ideTrab.calcTrib = new sIdeTrab.sCalcTrib();
         ideTrab.calcTrib.infoCRContrib = new sIdeTrab.sCalcTrib.sInfoCRContrib();
         ideTrab.infoCRIRRF = new sIdeTrab.sInfoCRIRRF();
         ideTrab.infoCRIRRF.infoIR = new sIdeTrab.sInfoCRIRRF.sInfoIR();
         ideTrab.infoCRIRRF.infoRRA = new sIdeTrab.sInfoCRIRRF.sInfoRRA();
         ideTrab.infoCRIRRF.infoRRA.despProcJud = new sIdeTrab.sInfoCRIRRF.sInfoRRA.sDespProcJud();
         ideTrab.infoCRIRRF.infoRRA.ideAdv = new sIdeTrab.sInfoCRIRRF.sInfoRRA.sIdeAdv();
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
         //lCalcTribInfo = new List<XElement>();
         lInfoCRIRRF = new List<XElement>();
         lInfoAdv = new List<XElement>();

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

         // infoCRContrib 0.99
         from e in lCalcTribInfo
         select e

         ));

         ideTrab.calcTrib = new sIdeTrab.sCalcTrib();
         lCalcTribInfo = new List<XElement>();
      }

      List<XElement> lCalcTribInfo = new List<XElement>();
      public void add_calcTribInfo()
      {
         lCalcTribInfo.Add(
         opElement("infoCRContrib", ideTrab.calcTrib.infoCRContrib.tpCR,
         new XAttribute("tpCR", ideTrab.calcTrib.infoCRContrib.tpCR),
         new XAttribute("vrCR", ideTrab.calcTrib.infoCRContrib.vrCR)));

         ideTrab.calcTrib.infoCRContrib = new sIdeTrab.sCalcTrib.sInfoCRContrib();
      }

      #endregion

      #region lInfoCRIRRF

      List<XElement> lInfoCRIRRF = new List<XElement>();
      public void add_infoCRIRRF()
      {
         lInfoCRIRRF.Add(
         opElement("infoCRIRRF", ideTrab.infoCRIRRF.tpCR,
         new XAttribute("tpCR", ideTrab.infoCRIRRF.tpCR),
         new XAttribute("vrCR", ideTrab.infoCRIRRF.vrCR),

         // infoCRContrib 0.99
         opElement("infoIR", ideTrab.infoCRIRRF.infoIR.vrRendTrib,
         new XAttribute("vrRendTrib", ideTrab.infoCRIRRF.infoIR.vrRendTrib),
         new XAttribute("vrRendTrib13", ideTrab.infoCRIRRF.infoIR.vrRendTrib13),
         new XAttribute("vrRendMoleGrave", ideTrab.infoCRIRRF.infoIR.vrRendMoleGrave),
         new XAttribute("vrRendIsen65", ideTrab.infoCRIRRF.infoIR.vrRendIsen65),
         new XAttribute("vrJurosMora", ideTrab.infoCRIRRF.infoIR.vrJurosMora),
         new XAttribute("vrRendIsenNTrib", ideTrab.infoCRIRRF.infoIR.vrRendIsenNTrib),
         new XAttribute("descIsenNTrib", ideTrab.infoCRIRRF.infoIR.descIsenNTrib),
         new XAttribute("vrPrevOficial", ideTrab.infoCRIRRF.infoIR.vrPrevOficial)),

         opElement("infoRRA", ideTrab.infoCRIRRF.infoRRA.descRRA,
         new XAttribute("descRRA", ideTrab.infoCRIRRF.infoRRA.descRRA),
         new XAttribute("qtdMesesRRA", ideTrab.infoCRIRRF.infoRRA.qtdMesesRRA),

         opElement("despProcJud", ideTrab.infoCRIRRF.infoRRA.despProcJud.vlrDespCustas,
         new XAttribute("vlrDespCustas", ideTrab.infoCRIRRF.infoRRA.despProcJud.vlrDespCustas),
         new XAttribute("vlrDespAdvogados", ideTrab.infoCRIRRF.infoRRA.despProcJud.vlrDespAdvogados)),

         // ideAdv - 99
         from e in lInfoAdv
         select e

         )));

         ideTrab.infoCRIRRF = new sIdeTrab.sInfoCRIRRF();
      }

      List<XElement> lInfoAdv = new List<XElement>();
      public void add_infoAdv()
      {
         lInfoAdv.Add(
         opElement("ideAdv", ideTrab.infoCRIRRF.infoRRA.ideAdv.tpInsc,
         new XAttribute("tpInsc", ideTrab.infoCRIRRF.infoRRA.ideAdv.tpInsc),
         new XAttribute("nrInsc", ideTrab.infoCRIRRF.infoRRA.ideAdv.nrInsc),
         new XAttribute("vlrAdv", ideTrab.infoCRIRRF.infoRRA.ideAdv.vlrAdv)));

         ideTrab.infoCRIRRF.infoRRA.ideAdv = new sIdeTrab.sInfoCRIRRF.sInfoRRA.sIdeAdv();
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
            public string perRef, vrBcCpMensal, vrBcCp13;

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

            public sInfoIR infoIR;
            public struct sInfoIR
            {
               public string vrRendTrib, vrRendTrib13, vrRendMoleGrave, vrRendIsen65, vrJurosMora, vrRendIsenNTrib, descIsenNTrib, vrPrevOficial;
            }

            public sInfoRRA infoRRA;
            public struct sInfoRRA
            {
               public string descRRA, qtdMesesRRA;

               public sDespProcJud despProcJud;
               public struct sDespProcJud
               {
                  public string vlrDespCustas, vlrDespAdvogados;
               }

               public sIdeAdv ideAdv;
               public struct sIdeAdv
               {
                  public string tpInsc, nrInsc, vlrAdv;
               }
            }
         }
      }

      #endregion

      #endregion
   }
}
