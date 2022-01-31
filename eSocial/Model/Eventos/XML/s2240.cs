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
   public class s2240 : bEvento_XML
   {

      public s2240(string sID) : base("evtExpRisco","", "v_S_01_00_00")
      {
         id = sID;

         ideEvento = new sIdeEvento();
         ideVinculo = new sIdeVinculo();

         infoExpRisco = new sInfoExpRisco();
         infoExpRisco.infoAmb = new sInfoExpRisco.sInfoAmb();
         infoExpRisco.infoAtiv = new sInfoExpRisco.sInfoAtiv();        

         infoExpRisco.agNoc = new sInfoExpRisco.sAgNoc();
         infoExpRisco.agNoc.epcEpi = new sInfoExpRisco.sAgNoc.sEpcEpi();
         infoExpRisco.agNoc.epcEpi.epi = new sInfoExpRisco.sAgNoc.sEpcEpi.sEpi();
         infoExpRisco.agNoc.epcEpi.epiCompl = new sInfoExpRisco.sAgNoc.sEpcEpi.sEpiCompl();

         infoExpRisco.respReg = new sInfoExpRisco.sRespReg();
         infoExpRisco.obs = new sInfoExpRisco.sObs();
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

         // infoExpRisco
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "infoExpRisco",
         new XElement(ns + "dtIniCondicao", infoExpRisco.dtIniCondicao),

         // infoAmb   
         new XElement(ns + "infoAmb",
         new XElement(ns + "localAmb", infoExpRisco.infoAmb.localAmb),
         new XElement(ns + "dscSetor", infoExpRisco.infoAmb.dscSetor),
         new XElement(ns + "tpInsc", infoExpRisco.infoAmb.tpInsc),
         new XElement(ns + "nrInsc", infoExpRisco.infoAmb.nrInsc)),

         // infoAtiv
         new XElement(ns + "infoAtiv",
         new XElement(ns + "dscAtivDes", infoExpRisco.infoAtiv.dscAtivDes)),

         // agNoc 1.9999
         from e in lAgNoc
         select e,

         // respReg 1.99
         from e in lRespReg
         select e,

         // obs 0.1
         opElement("obs", infoExpRisco.obs.obsCompl,
         new XElement(ns + "obsCompl", infoExpRisco.obs.obsCompl))));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência


      #region agNoc   

      List<XElement> lAgNoc = new List<XElement>();
      public void add_agNoc()
      {
         lAgNoc.Add(
         new XElement(ns + "agNoc",
         new XElement(ns + "codAgNoc", infoExpRisco.agNoc.codAgNoc),
         opTag("dscAgNoc", infoExpRisco.agNoc.dscAgNoc),
         opTag("tpAval", infoExpRisco.agNoc.tpAval),
         opTag("intConc", infoExpRisco.agNoc.intConc),
         opTag("limTol", infoExpRisco.agNoc.limTol),
         opTag("unMed", infoExpRisco.agNoc.unMed),
         opTag("tecMedicao", infoExpRisco.agNoc.tecMedicao),

         // epcEpi   
         new XElement(ns + "epcEpi",
         new XElement(ns + "utilizEPC", infoExpRisco.agNoc.epcEpi.utilizEpc),
         opTag("eficEpc", infoExpRisco.agNoc.epcEpi.eficEpc),
         new XElement(ns + "utilizEPI", infoExpRisco.agNoc.epcEpi.utilizEPI),
         opTag("eficEpi", infoExpRisco.agNoc.epcEpi.eficEpi),

         // epi 0.50
         from e in lEpi
         select e,

         // epiCompl 0.1
         opElement("epiCompl", infoExpRisco.agNoc.epcEpi.epiCompl.medProtecao,
         new XElement(ns + "medProtecao", infoExpRisco.agNoc.epcEpi.epiCompl.medProtecao),
         new XElement(ns + "condFuncto", infoExpRisco.agNoc.epcEpi.epiCompl.condFuncto),
         new XElement(ns + "usoInint", infoExpRisco.agNoc.epcEpi.epiCompl.usoInint),
         new XElement(ns + "przValid", infoExpRisco.agNoc.epcEpi.epiCompl.przValid),
         new XElement(ns + "periodicTroca", infoExpRisco.agNoc.epcEpi.epiCompl.periodicTroca),
         new XElement(ns + "higienizacao", infoExpRisco.agNoc.epcEpi.epiCompl.higienizacao)))));

         infoExpRisco.agNoc = new sInfoExpRisco.sAgNoc();
      }
      #endregion

      #region epi   

      List<XElement> lEpi = new List<XElement>();
      public void add_epi()
      {
         lEpi.Add(
         opElement("epi", infoExpRisco.agNoc.epcEpi.epi.docAval,
         opTag("docAval", infoExpRisco.agNoc.epcEpi.epi.docAval),
         opTag("dscEPI", infoExpRisco.agNoc.epcEpi.epi.dscEPI)));

         infoExpRisco.agNoc.epcEpi.epi = new sInfoExpRisco.sAgNoc.sEpcEpi.sEpi();
      }
      #endregion

      #region respReg   

      List<XElement> lRespReg = new List<XElement>();
      public void add_respReg()
      {
         lRespReg.Add(
         new XElement(ns + "respReg",
         new XElement(ns + "cpfResp", infoExpRisco.respReg.cpfResp),
         new XElement(ns + "ideOC", infoExpRisco.respReg.ideOC),
         opTag("dscOC", infoExpRisco.respReg.dscOC),
         new XElement(ns + "nrOC", infoExpRisco.respReg.nrOC),
         new XElement(ns + "ufOC", infoExpRisco.respReg.ufOC)));

         infoExpRisco.respReg = new sInfoExpRisco.sRespReg();
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

      public sInfoExpRisco infoExpRisco;
      public struct sInfoExpRisco
      {
         public string dtIniCondicao;

         public sInfoAmb infoAmb;
         public struct sInfoAmb
         {
            public string localAmb;
            public string dscSetor;
            public string tpInsc;
            public string nrInsc;
         }
         public sInfoAtiv infoAtiv;
         public struct sInfoAtiv
         {
            public string dscAtivDes;
         }
         public sAgNoc agNoc;
         public struct sAgNoc
         {
            public string codAgNoc;
            public string dscAgNoc;
            public string tpAval;
            public string intConc;
            public string limTol;
            public string unMed;
            public string tecMedicao;

            public sEpcEpi epcEpi;
            public struct sEpcEpi
            {
               public string utilizEpc;
               public string eficEpc;
               public string utilizEPI;
               public string eficEpi;

               public sEpi epi;
               public struct sEpi
               {
                  public string docAval;
                  public string dscEPI;
               }

               public sEpiCompl epiCompl;
               public struct sEpiCompl
               {
                  public string medProtecao;
                  public string condFuncto;
                  public string usoInint;
                  public string przValid;
                  public string periodicTroca;
                  public string higienizacao;
               }
            }
         }
         public sRespReg respReg;
         public struct sRespReg
         {
            public string cpfResp;
            public string ideOC;
            public string dscOC;
            public string nrOC;
            public string ufOC;
         }
         public sObs obs;
         public struct sObs
         {
            public string obsCompl;
         }
      }
      #endregion
   }
}
