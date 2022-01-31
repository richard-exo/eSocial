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
   public class s5011 : bEvento_XML
   {

      public s5011(string sID) : base("evtCS")
      {
         id = sID;

         ideEvento = new sIdeEvento();
         infoCS = new sInfoCS();         
         infoCS.ideEstab = new sInfoCS.sIdeEstab();
         infoCS.ideEstab.basesAquis = new sInfoCS.sIdeEstab.sBasesAquis();
         infoCS.ideEstab.basesComerc = new sInfoCS.sIdeEstab.sBasesComerc();
         infoCS.ideEstab.ideLotacao = new sInfoCS.sIdeEstab.sIdeLotacao();
         infoCS.ideEstab.ideLotacao.infoTercSusp = new sInfoCS.sIdeEstab.sIdeLotacao.sInfoTercSusp();
         infoCS.ideEstab.ideLotacao.infoEmpreParcial = new sInfoCS.sIdeEstab.sIdeLotacao.sInfoEmpreParcial();
         infoCS.ideEstab.ideLotacao.dadosOpPort = new sInfoCS.sIdeEstab.sIdeLotacao.sDadosOpPort();
         infoCS.ideEstab.ideLotacao.basesRemun = new sInfoCS.sIdeEstab.sIdeLotacao.sBasesRemun();
         infoCS.ideEstab.ideLotacao.basesAvNPort = new sInfoCS.sIdeEstab.sIdeLotacao.sBasesAvNPort();
         infoCS.ideEstab.ideLotacao.infoSubstPartrOpPort = new sInfoCS.sIdeEstab.sIdeLotacao.sInfoSubstPartrOpPort();
         infoCS.ideEstab.infoCREstab = new sInfoCS.sIdeEstab.sInfoCREstab();
         infoCS.ideEstab.infoEstab = new sInfoCS.sIdeEstab.sInfoEstab();
         infoCS.infoContrib = new sInfoCS.sInfoContrib();
         infoCS.infoContrib.infoPJ = new sInfoCS.sInfoContrib.sInfoPJ();
         infoCS.infoCPSeg = new sInfoCS.sInfoCPSeg();
         infoCS.infoCRContrib = new sInfoCS.sInfoCRContrib();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {
         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

         new XElement(ns + "indApuracao", ideEvento.indApuracao),
         new XElement(ns + "perApur", ideEvento.perApur));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // infoCS
         xml.Elements().ElementAt(0).Add(
         opTag("nrRecArqBase", ideEvento.nrRecArqBase),
         new XElement(ns + "indExistInfo", ideEvento.indExistInfo));

         // infoCPSeg
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "vrDescCP", infoCS.infoCPSeg.vrDescCP),
         opTag("vrCpSeg", infoCS.infoCPSeg.VRcpSeg));

         // infoContrib
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "classtrib", infoCS.infoContrib.classTrib));

         // infoPJ
         xml.Elements().ElementAt(0).Add(
         opTag("indCoop", infoCS.infoContrib.infoPJ.indCoop),
         new XElement(ns + "indConstr", infoCS.infoContrib.infoPJ.indConstr),
         opTag("indSubsPatr", infoCS.infoContrib.infoPJ.indSubstPatr),
         opTag("percRedContrib", infoCS.infoContrib.infoPJ.percRedContrib));

         // infoAtConc
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "fatorMes", infoCS.infoContrib.infoPJ.infoAtConc.fatorMes),
         new XElement(ns + "fator13", infoCS.infoContrib.infoPJ.infoAtConc.fator13));

         // ideEstab 0.99
         xml.Elements().ElementAt(0).Add(
         from e in lIdeEstab
         select e);

         // infoCREstab 0.99
         xml.Elements().ElementAt(0).Add(
         from e in lInfoCRContrib
         select e);

         return x509.signXMLSHA256(xml, cert);
      }

      #region ideEstab  

      List<XElement> lIdeEstab = new List<XElement>();
      public void add_ideEstab()
      {
         lIdeEstab.Add(

         new XElement(ns + "ideEstab",
         new XElement(ns + "tpInsc", infoCS.ideEstab.tpInsc),
         new XElement(ns + "nrInsc", infoCS.ideEstab.nrInscr),

         new XElement(ns + "infoEstab",
         new XElement(ns + "cnaePrep", infoCS.ideEstab.tpInsc),
         new XElement(ns + "aliqRat", infoCS.ideEstab.tpInsc),
         new XElement(ns + "fap", infoCS.ideEstab.tpInsc),
         new XElement(ns + "aliqRatAjust", infoCS.ideEstab.tpInsc),

         new XElement(ns + "infoComplObra",
         new XElement(ns + "indSubsPatrObra", infoCS.ideEstab.tpInsc),

         // ideLotacao 0.9999
         from e in lIdeLotacao
         select e,

         // basesAquis 0.3
         from e in lBasesAquis
         select e,

         // basesComerc 0.4
         from e in lBasesComerc
         select e,

         // infoCREstab 0.99
         from e in lInfoCREstab
         select e))));

         infoCS.ideEstab = new sInfoCS.sIdeEstab();
      }
      #endregion

      #region ideLotacao

      List<XElement> lIdeLotacao = new List<XElement>();
      public void add_ideLotacao()
      {
         lIdeLotacao.Add(

         new XElement(ns + "ideLotacao",
         new XElement(ns + "codLotacao", infoCS.ideEstab.ideLotacao.codLotacao),
         new XElement(ns + "fpas", infoCS.ideEstab.ideLotacao.fpas),
         new XElement(ns + "codTercs", infoCS.ideEstab.ideLotacao.codTercs),
         opTag("codTercsSusp", infoCS.ideEstab.ideLotacao.codTercsSusp),

         // infoTercSusp 0.15
         from e in lInfoTercSusp
         select e,

         new XElement(ns + "infoEmprParcial",
         new XElement(ns + "tpInscContrat", infoCS.ideEstab.ideLotacao.infoEmpreParcial.tpInscContrat),
         new XElement(ns + "nrInscContrat", infoCS.ideEstab.ideLotacao.infoEmpreParcial.nrInscContrat),
         new XElement(ns + "tpInscProp", infoCS.ideEstab.ideLotacao.infoEmpreParcial.tpInscProp),
         new XElement(ns + "nrInscProp", infoCS.ideEstab.ideLotacao.infoEmpreParcial.nrInscProp),

         new XElement(ns + "dadosOpPort",
         new XElement(ns + "cnpjOpPortuario", infoCS.ideEstab.ideLotacao.dadosOpPort.cnpjOpPortuario),
         new XElement(ns + "aliqRat", infoCS.ideEstab.ideLotacao.dadosOpPort.aliqRat),
         new XElement(ns + "fap", infoCS.ideEstab.ideLotacao.dadosOpPort.fap),
         new XElement(ns + "aliqRatAjust", infoCS.ideEstab.ideLotacao.dadosOpPort.aliqRatAjust),

         // infoTercSusp 0.15
         from e in lBasesRemun
         select e,

         new XElement(ns + "basesAvNPort",
         new XElement(ns + "vrBcCp00", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcCp00),
         new XElement(ns + "vrBcCp15", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcCp15),
         new XElement(ns + "vrBcCp20", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcCp20),
         new XElement(ns + "vrBcCp25", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcCp25),
         new XElement(ns + "vrBcCp13", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcCp13),
         new XElement(ns + "vrBcFgts", infoCS.ideEstab.ideLotacao.basesAvNPort.vrBcFgts),
         new XElement(ns + "vrDescCP", infoCS.ideEstab.ideLotacao.basesAvNPort.vrDescCP),

         // infoTercSusp 0.999
         from e in lInfoSubstPatrOpPort
         select e)))));

         infoCS.ideEstab.ideLotacao = new sInfoCS.sIdeEstab.sIdeLotacao();
      }
      #endregion

      #region infoTercSusp

      List<XElement> lInfoTercSusp = new List<XElement>();
      public void add_infoTercSusp()
      {
         lInfoTercSusp.Add(

         new XElement(ns + "infoTercSusp",
         new XElement(ns + "codTerc", infoCS.ideEstab.ideLotacao.infoTercSusp.codTerc)));

         infoCS.ideEstab.ideLotacao.infoTercSusp = new sInfoCS.sIdeEstab.sIdeLotacao.sInfoTercSusp();
      }
      #endregion

      #region basesRemun

      List<XElement> lBasesRemun = new List<XElement>();
      public void add_basesRemun()
      {
         lBasesRemun.Add(

         new XElement(ns + "basesRemun",
         new XElement(ns + "indIncid", infoCS.ideEstab.ideLotacao.basesRemun.indIncid),
         new XElement(ns + "codCateg", infoCS.ideEstab.ideLotacao.basesRemun.codCateg),

         new XElement(ns + "basesCP",
         new XElement(ns + "vrBcCp00", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrBcCp00),
         new XElement(ns + "vrBcCp15", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrBcCp15),
         new XElement(ns + "vrBcCp20", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrBcCp20),
         new XElement(ns + "vrBcCp25", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrBcCp25),
         new XElement(ns + "vrSuspBcCp00", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSuspBcCp00),
         new XElement(ns + "vrSuspBcCp15", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSuspBcCp15),
         new XElement(ns + "vrSuspBcCp20", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSuspBcCp20),
         new XElement(ns + "vrSuspBcCp25", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSuspBcCp25),
         new XElement(ns + "vrDescSest", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrDescSest),
         new XElement(ns + "vrCalcSest", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrCalcSest),
         new XElement(ns + "vrDescSenat", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrDescSenat),
         new XElement(ns + "vrCalcSenat", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrCalcSenat),
         new XElement(ns + "vrSalFam", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSalFam),
         new XElement(ns + "vrSalMat", infoCS.ideEstab.ideLotacao.basesRemun.basesCP.vrSalMat))));

         infoCS.ideEstab.ideLotacao.basesRemun = new sInfoCS.sIdeEstab.sIdeLotacao.sBasesRemun();
      }
      #endregion

      #region infoSubstPatrOpPort

      List<XElement> lInfoSubstPatrOpPort = new List<XElement>();
      public void add_infoSubstPatrOpPort()
      {
         lInfoSubstPatrOpPort.Add(

         new XElement(ns + "infoSubstPatrOpPort",
         new XElement(ns + "cnpjOpPortuario", infoCS.ideEstab.ideLotacao.infoSubstPartrOpPort.cnpjOpPortuario)));

         infoCS.ideEstab.ideLotacao.infoSubstPartrOpPort = new sInfoCS.sIdeEstab.sIdeLotacao.sInfoSubstPartrOpPort();
      }
      #endregion

      #region basesAquis

      List<XElement> lBasesAquis = new List<XElement>();
      public void add_basesAquis()
      {
         lBasesAquis.Add(

         new XElement(ns + "basesAquis",
         new XElement(ns + "indAquis", infoCS.ideEstab.basesAquis.indaquis),
         new XElement(ns + "vlrAquis", infoCS.ideEstab.basesAquis.vlrAquis),
         new XElement(ns + "vrCPDescPR", infoCS.ideEstab.basesAquis.vrCPDescPR),

         new XElement(ns + "vrCPNRet", infoCS.ideEstab.basesAquis.vrCPNRet),
         new XElement(ns + "vrRatNRet", infoCS.ideEstab.basesAquis.vrRatNRet),
         new XElement(ns + "vrSenarNRet", infoCS.ideEstab.basesAquis.vrSenarNRet),
         new XElement(ns + "vrCPCalcPR", infoCS.ideEstab.basesAquis.vrCPCalcPR),

         new XElement(ns + "vrRatDescPR", infoCS.ideEstab.basesAquis.vrRatDescPR),
         new XElement(ns + "vrRatCalcPR", infoCS.ideEstab.basesAquis.vrRatCalcPR),
         new XElement(ns + "vrSenarDesc", infoCS.ideEstab.basesAquis.vrSenarDesc),
         new XElement(ns + "vrSenarCalc", infoCS.ideEstab.basesAquis.vrSenarCalc)));

         infoCS.ideEstab.basesAquis = new sInfoCS.sIdeEstab.sBasesAquis();
      }
      #endregion

      #region basesComerc

      List<XElement> lBasesComerc = new List<XElement>();
      public void add_basesComerc()
      {
         lBasesComerc.Add(

         new XElement(ns + "basesComerc",
         new XElement(ns + "indComerc", infoCS.ideEstab.basesComerc.indComerc),
         new XElement(ns + "vrBcComPR", infoCS.ideEstab.basesComerc.vrBcComPR),
         new XElement(ns + "vrCPSusp", infoCS.ideEstab.basesComerc.vrCPSusp),
         new XElement(ns + "vrRatSusp", infoCS.ideEstab.basesComerc.vrRatSusp),
         new XElement(ns + "vrSenarSusp", infoCS.ideEstab.basesComerc.vrSenarSusp)));

         infoCS.ideEstab.basesComerc = new sInfoCS.sIdeEstab.sBasesComerc();
      }
      #endregion

      #region infoCREstab
      List<XElement> lInfoCREstab = new List<XElement>();
      public void add_infoCREstab()
      {
         lInfoCREstab.Add(

         new XElement(ns + "infoCREstab",
         new XElement(ns + "tpCR", infoCS.ideEstab.infoCREstab.tpCR),
         new XElement(ns + "vrCR", infoCS.ideEstab.infoCREstab.vrCR),
         new XElement(ns + "vrSuspCR", infoCS.ideEstab.infoCREstab.vrSuspCR)));

         infoCS.ideEstab.infoCREstab = new sInfoCS.sIdeEstab.sInfoCREstab();
      }
      #endregion

      #region infoCRContrib
      List<XElement> lInfoCRContrib = new List<XElement>();
      public void add_infoCRContrib()
      {
         lInfoCRContrib.Add(

         new XElement(ns + "infoCRContrib",
         new XElement(ns + "tpCR", infoCS.infoCRContrib.tpCR),
         new XElement(ns + "vrCR", infoCS.infoCRContrib.vrCR),
         new XElement(ns + "vrCRSusp", infoCS.infoCRContrib.vrCRSusp)));

         infoCS.infoCRContrib = new sInfoCS.sInfoCRContrib();
      }
      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string nrRecArqBase, perApur;
         public string indApuracao, indExistInfo;
      }

      public sInfoCS infoCS;
      public struct sInfoCS
      {
         public sInfoCPSeg infoCPSeg;
         public struct sInfoCPSeg
         {
            public string vrDescCP, VRcpSeg;
         }
         public sInfoContrib infoContrib;
         public struct sInfoContrib
         {
            public string classTrib;

            public sInfoPJ infoPJ;
            public struct sInfoPJ
            {
               public string indCoop, indConstr, indSubstPatr, percRedContrib;

               public sInfoAtConc infoAtConc;
               public struct sInfoAtConc
               {
                  public string fatorMes, fator13;
               }
            }
         }

         public sIdeEstab ideEstab;
         public struct sIdeEstab
         {
            public string tpInsc, nrInscr;

            public sInfoEstab infoEstab;
            public struct sInfoEstab
            {
               public string cnaePrep, aliRat, fap, aliqRatAjust;

               public sInfoComplObra infoComplObra;
               public struct sInfoComplObra
               {
                  public string indSubstPartObra;
               }
            }

            public sIdeLotacao ideLotacao;
            public struct sIdeLotacao
            {
               public string codLotacao, fpas, codTercs, codTercsSusp;

               public sInfoTercSusp infoTercSusp;
               public struct sInfoTercSusp
               {
                  public string codTerc;
               }
               public sInfoEmpreParcial infoEmpreParcial;
               public struct sInfoEmpreParcial
               {
                  public string tpInscContrat, nrInscContrat, tpInscProp, nrInscProp;
               }
               public sDadosOpPort dadosOpPort;
               public struct sDadosOpPort
               {
                  public string cnpjOpPortuario, aliqRat, fap, aliqRatAjust;
               }
               public sBasesRemun basesRemun;
               public struct sBasesRemun
               {
                  public string indIncid, codCateg;

                  public sBasesCP basesCP;
                  public struct sBasesCP
                  {
                     public string vrBcCp00, vrBcCp15, vrBcCp20, vrBcCp25, vrSuspBcCp00, vrSuspBcCp15, vrSuspBcCp20, vrSuspBcCp25;
                     public string vrDescSest, vrCalcSest, vrDescSenat, vrCalcSenat, vrSalFam, vrSalMat;
                  }
               }
               public sBasesAvNPort basesAvNPort;
               public struct sBasesAvNPort
               {
                  public string vrBcCp00, vrBcCp15, vrBcCp20, vrBcCp25, vrBcCp13, vrBcFgts,vrDescCP;
               }
               public sInfoSubstPartrOpPort infoSubstPartrOpPort;
               public struct sInfoSubstPartrOpPort
               {
                  public string cnpjOpPortuario;
               }
            }
            public sBasesAquis basesAquis;
            public struct sBasesAquis
            {
               public string indaquis, vlrAquis, vrCPDescPR, vrCPNRet, vrRatNRet, vrSenarNRet, vrCPCalcPR, vrRatDescPR, vrRatCalcPR, vrSenarDesc, vrSenarCalc;
            }
            public sBasesComerc basesComerc;
            public struct sBasesComerc
            {
               public string indComerc, vrBcComPR, vrCPSusp, vrRatSusp, vrSenarSusp;
            }
            public sInfoCREstab infoCREstab;
            public struct sInfoCREstab
            {
               public string tpCR, vrCR, vrSuspCR;
            }
         }
         public sInfoCRContrib infoCRContrib;
         public struct sInfoCRContrib
         {
            public string tpCR, vrCR, vrCRSusp;
         }
      }
      #endregion
   }
}