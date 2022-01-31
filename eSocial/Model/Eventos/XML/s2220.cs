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
   public class s2220 : bEvento_XML
   {

      public s2220(string sID) : base("evtMonit", "", "v_S_01_00_00")
      {
         id = sID;

         ideEvento = new sIdeEvento();
         ideVinculo = new sIdeVinculo();

         exMedOcup = new sExMedOcup();
         exMedOcup.aso = new sExMedOcup.sAso();
         exMedOcup.aso.exame = new sExMedOcup.sAso.sExame();
         exMedOcup.respMonit = new sExMedOcup.sRespMonit();
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
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideVinculo",
         new XElement(ns + "cpfTrab", ideVinculo.cpfTrab),
         opTag("matricula", ideVinculo.matricula),
         opTag("codCateg", ideVinculo.codCateg)));

         // exMedOcup
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "exMedOcup",
         new XElement(ns + "tpExameOcup", exMedOcup.tpExameOcup),

         // aso   
         new XElement(ns + "aso",
         new XElement(ns + "dtAso", exMedOcup.aso.dtAso),
         new XElement(ns + "resAso", exMedOcup.aso.resAso),

         // agNoc 1.9999
         from e in lExame
         select e,

         // medico   
         new XElement(ns + "medico",
         new XElement(ns + "nmMed", exMedOcup.aso.medico.nmMed),
         new XElement(ns + "nrCRM", exMedOcup.aso.medico.nrCRM),
         new XElement(ns + "ufCRM", exMedOcup.aso.medico.ufCRM))),

         // obs 0.1
         opElement("respMonit", exMedOcup.respMonit.nmResp,
         opTag("cpfResp", exMedOcup.respMonit.cpfResp),
         new XElement(ns + "nmResp", exMedOcup.respMonit.nmResp),
         new XElement(ns + "nrCRM", exMedOcup.respMonit.nrCRM),
         new XElement(ns + "ufCRM", exMedOcup.respMonit.ufCRM))));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência


      #region agNoc   

      List<XElement> lExame = new List<XElement>();
      public void add_exame()
      {
         lExame.Add(
         new XElement(ns + "exame",
         new XElement(ns + "dtExm", exMedOcup.aso.exame.dtExm),
         new XElement(ns + "procRealizado", exMedOcup.aso.exame.procRealizado),
         opTag("obsProc", exMedOcup.aso.exame.obsProc),
         opTag("ordExame", exMedOcup.aso.exame.ordExame),
         opTag("indResult", exMedOcup.aso.exame.indResult)));

         exMedOcup.aso.exame = new sExMedOcup.sAso.sExame();
      }
      #endregion 

      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, nrRecibo, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeVinculo ideVinculo;
      public struct sIdeVinculo
      {
         public string cpfTrab, matricula;
         public string codCateg;
      }

      public sExMedOcup exMedOcup;
      public struct sExMedOcup
      {
         public string tpExameOcup;

         public sAso aso;
         public struct sAso
         {
            public string dtAso;
            public string resAso;

            public sExame exame;
            public struct sExame
            {
               public string dtExm;
               public string procRealizado;
               public string obsProc;
               public string ordExame;
               public string indResult;
            }

            public sMedico medico;
            public struct sMedico
            {
               public string nmMed;
               public string nrCRM;
               public string ufCRM;
            }
         }
         public sRespMonit respMonit;
         public struct sRespMonit
         {
            public string cpfResp;
            public string nmResp;
            public string nrCRM;
            public string ufCRM;
         }
      }
      #endregion
   }
}
