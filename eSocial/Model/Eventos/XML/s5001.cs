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
   public class s5001 : bEvento_XML {

      public s5001(string sID) : base("evtBasesTrab", "infoCp") {

         id = sID;

         ideEvento = new sIdeEvento();
         ideTrabalhador = new sIdeTrabalhador();
         ideTrabalhador.procJudTrab = new sIdeTrabalhador.sProcJudTrab();

         infoCpCalc = new sInfoCpCalc();
         infoCp = new sInfoCp();
         infoCp.ideEstabLot = new sInfoCp.sIdeEstabLot();
         infoCp.ideEstabLot.infoCategIncid = new sInfoCp.sIdeEstabLot.sInfoCategIncid();
         infoCp.ideEstabLot.infoCategIncid.infoBaseCS = new sInfoCp.sIdeEstabLot.sInfoCategIncid.sInfoBaseCS();
         infoCp.ideEstabLot.calcTerc = new sInfoCp.sIdeEstabLot.sCalcTerc();
      }

      public override XElement genSignedXML(X509Certificate2 cert) {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

         opTag("nrRecArqBase", ideEvento.nrRecArqBase),
         new XElement(ns + "indApuracao", ideEvento.indApuracao),
         new XElement(ns + "perApur", ideEvento.perApur));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // ideTrabalhador
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideTrabalhador",
         new XElement(ns + "cpfTrab", ideTrabalhador.cpfTrab),

         // procJudTrab 0.99
         from e in lProcJudTrab
         select e));

         // infoCpCalc 0.9
         xml.Elements().ElementAt(0).Add(
         from e in lInfoCpCalc
         select e);

         // infoCp
         //xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(
         xml.Elements().ElementAt(0).Add(
         from e in lIdeEstabLot
         select e);

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      #region procJudTrab  

      List<XElement> lProcJudTrab = new List<XElement>();
      public void add_ideEstab() {

         lProcJudTrab.Add(

         new XElement(ns + "procJudTrab",
         new XElement(ns + "nrProcJud", ideTrabalhador.procJudTrab.nrProcJud),
         new XElement(ns + "codSusp", ideTrabalhador.procJudTrab.codSusp)));

         ideTrabalhador.procJudTrab = new sIdeTrabalhador.sProcJudTrab();
      }
      #endregion

      #region infoCpCalc

      List<XElement> lInfoCpCalc = new List<XElement>();
      public void addCalc() {

         lInfoCpCalc.Add(

         new XElement(ns + "infoCpCalc",
         new XElement(ns + "tpCR", infoCpCalc.tpCR),
         new XElement(ns + "vrCpSeg", infoCpCalc.vrCpSeg),
         new XElement(ns + "vrDescSeg", infoCpCalc.vrDescSeg)));

         infoCpCalc = new sInfoCpCalc();
      }
      #endregion

      #region ideEstabLot

      List<XElement> lIdeEstabLot = new List<XElement>();
      public void add_ideEstabLot() {

         lIdeEstabLot.Add(

         new XElement(ns + "infoCp",
         new XElement(ns + "ideEstabLot",
         new XElement(ns + "tpInsc", infoCp.ideEstabLot.tpInsc),
         new XElement(ns + "nrInsc", infoCp.ideEstabLot.nrInsc),
         new XElement(ns + "codLotacao", infoCp.ideEstabLot.codLotacao),

         // infoCategIncid 1.10
         from e in lInfoCategIncid
         select e,

         // calcTerc 0.2
         from e in lCalcTerc
         select e)));

         infoCp.ideEstabLot = new sInfoCp.sIdeEstabLot();
      }
      #endregion

      #region infoCategIncid

      List<XElement> lInfoCategIncid = new List<XElement>();
      public void add_infoCategIncid() {

         lInfoCategIncid.Add(

         new XElement(ns + "infoCategIncid",
         opTag("matricula", infoCp.ideEstabLot.infoCategIncid.matricula),
         new XElement(ns + "codCateg", infoCp.ideEstabLot.infoCategIncid.codCateg),
         opTag("indSimples", infoCp.ideEstabLot.infoCategIncid.indSimples),

         // infoBaseCS 0.99
         from e in lInfoBaseCS
         select e,

         // calcTerc 0.2
         from e in lCalcTerc
         select e));

         infoCp.ideEstabLot.infoCategIncid = new sInfoCp.sIdeEstabLot.sInfoCategIncid();
      }
      #endregion

      #region infoBaseCS

      List<XElement> lInfoBaseCS = new List<XElement>();
      public void add_infoBaseCS() {

         lInfoBaseCS.Add(

         new XElement(ns + "infoBaseCS",
         new XElement(ns + "ind13", infoCp.ideEstabLot.infoCategIncid.infoBaseCS.ind13),
         new XElement(ns + "tpValor", infoCp.ideEstabLot.infoCategIncid.infoBaseCS.tpValor),
         new XElement(ns + "valor", infoCp.ideEstabLot.infoCategIncid.infoBaseCS.valor)));

         infoCp.ideEstabLot.infoCategIncid.infoBaseCS = new sInfoCp.sIdeEstabLot.sInfoCategIncid.sInfoBaseCS();
      }
      #endregion

      #region calcTerc

      List<XElement> lCalcTerc = new List<XElement>();
      public void add_calcTerc() {

         lCalcTerc.Add(

         new XElement(ns + "infoCategIncid",
         new XElement(ns + "tpCR", infoCp.ideEstabLot.calcTerc.tpCR),
         new XElement(ns + "vrCsSegTerc", infoCp.ideEstabLot.calcTerc.vrCsSegTerc),
         new XElement(ns + "vrDescTerc", infoCp.ideEstabLot.calcTerc.vrDescTerc)));

         infoCp.ideEstabLot.calcTerc = new sInfoCp.sIdeEstabLot.sCalcTerc();
      }
      #endregion

      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento {
         public string nrRecArqBase, perApur;
         public string indApuracao;
      }

      public sIdeTrabalhador ideTrabalhador;
      public struct sIdeTrabalhador {
         public string cpfTrab;

         public sProcJudTrab procJudTrab;
         public struct sProcJudTrab {
            public string nrProcJud;
            public string codSusp;
         }
      }

      public sInfoCpCalc infoCpCalc;
      public struct sInfoCpCalc {
         public string tpCR;
         public string vrCpSeg, vrDescSeg;
      }

      public sInfoCp infoCp;
      public struct sInfoCp {

         public sIdeEstabLot ideEstabLot;
         public struct sIdeEstabLot {
            public string tpInsc;
            public string nrInsc, codLotacao;

            public sInfoCategIncid infoCategIncid;
            public struct sInfoCategIncid {
               public string codCateg, indSimples;
               public string matricula;

               public sInfoBaseCS infoBaseCS;
               public struct sInfoBaseCS {
                  public string ind13, valor;
                  public string tpValor;
               }
            }
            public sCalcTerc calcTerc;
            public struct sCalcTerc {
               public string tpCR;
               public string vrCsSegTerc, vrDescTerc;
            }
         }
      }

      #endregion
   }
}