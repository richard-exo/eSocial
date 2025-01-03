using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
    class s1210 : bEvento_XML {

        public s1210(string sID) : base("evtPgtos", "", "v_S_01_03_00") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideBenef = new sIdeBenef();
            ideBenef.deps = new sIdeBenef.sDeps();
            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();
            ideBenef.infoPgto.infoPgtoExt = new sIdeBenef.sInfoPgto.sInfoPgtoExt();
            ideBenef.infoPgto.infoPgtoExt.endExt = new sIdeBenef.sInfoPgto.sInfoPgtoExt.sEndExt();

            ideBenef.infoIRComplem = new sIdeBenef.sInfoIRComple();
            ideBenef.infoIRComplem.infoDep = new sIdeBenef.sInfoIRComple.sInfoDep();
            ideBenef.infoIRComplem.infoIRCR = new sIdeBenef.sInfoIRComple.sInfoIRCR();
            ideBenef.infoIRComplem.infoIRCR.dedDepen = new sIdeBenef.sInfoIRComple.sInfoIRCR.sDedDepen();
            ideBenef.infoIRComplem.infoIRCR.penAlim = new sIdeBenef.sInfoIRComple.sInfoIRCR.sPenAlim();
            ideBenef.infoIRComplem.infoIRCR.previdCompl = new sIdeBenef.sInfoIRComple.sInfoIRCR.sPrevidCompl();
            ideBenef.infoIRComplem.infoIRCR.infoProcRet = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet();
            ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet.sInfoValores();
            ideBenef.infoIRComplem.planSaude = new sIdeBenef.sInfoIRComple.sPlanSaude();
            ideBenef.infoIRComplem.planSaude.infoDepSau = new sIdeBenef.sInfoIRComple.sPlanSaude.sInfoDepSau();
            ideBenef.infoIRComplem.infoReembMed = new sIdeBenef.sInfoIRComple.sInfoReembMed();

        }

        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

            new XElement(ns + "indRetif", ideEvento.indRetif),
            opTag("nrRecibo", ideEvento.nrRecibo),
            new XElement(ns + "perApur", ideEvento.perApur),
            opTag("indGuia", ideEvento.indGuia),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // ideBenef
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "ideBenef",
            new XElement(ns + "cpfBenef", ideBenef.cpfBenef),

            // infoPgto 1.60
            from e in lInfoPgto
            select e,

            //infoIRComplem 0.1 (ativar em fevereiro/24)
            from e in lIfoIRComplem
            select e


         ));

         lInfoPgto = new List<XElement>();
         lInfoPgtoExt = new List<XElement>();
         lEndExt = new List<XElement>();
         lIfoIRComplem = new List<XElement>();
         lInfoDep = new List<XElement>();
         lInfoIRCR = new List<XElement>();
         //lDedPen = new List<XElement>();
         //lPenAlim = new List<XElement>();
         //lPrevidCompl = new List<XElement>();
         //lInfoProcRet = new List<XElement>();
         //lInfoValores = new List<XElement>();
         //lDedSusp = new List<XElement>();
         //lBenefPen = new List<XElement>();
         //lPlanSaude = new List<XElement>();
         //lInfoDepSau = new List<XElement>();
         //lInfoReembMed = new List<XElement>();

         return x509.signXMLSHA256(xml, cert);
      }

      #region infoPgto

        List<XElement> lInfoPgto = new List<XElement>();
        public void add_infoPgto() {

            lInfoPgto.Add(

            new XElement(ns + "infoPgto",
            new XElement(ns + "dtPgto", ideBenef.infoPgto.dtPgto),
            new XElement(ns + "tpPgto", ideBenef.infoPgto.tpPgto),
            new XElement(ns + "perRef", ideBenef.infoPgto.perRef),
            new XElement(ns + "ideDmDev", ideBenef.infoPgto.ideDmDev),
            new XElement(ns + "vrLiq", ideBenef.infoPgto.vrLiq),
            opTag("paisResidExt", ideBenef.infoPgto.paisResidExt),


            // infoPgtoExt 0.1
            from e in lInfoPgtoExt
            select e
            
            ));

            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();
        }


         List<XElement> lInfoPgtoExt = new List<XElement>();
         public void add_infoPgtoExt()
         {

            lInfoPgtoExt.Add(

            opElement("infoPgtoExt", ideBenef.infoPgto.infoPgtoExt.indNIF,
            new XElement(ns + "indNIF", ideBenef.infoPgto.infoPgtoExt.indNIF),
            new XElement(ns + "nifBenef", ideBenef.infoPgto.infoPgtoExt.nifBenef),
            new XElement(ns + "frmTribut", ideBenef.infoPgto.infoPgtoExt.frmTribut),

            // infoPgto 1.60
            from e in lEndExt
            select e

            ));

            ideBenef.infoPgto.infoPgtoExt = new sIdeBenef.sInfoPgto.sInfoPgtoExt();
         }

         List<XElement> lEndExt = new List<XElement>();
         public void add_endExt()
         {

            lEndExt.Add(

            opElement("endExt", ideBenef.infoPgto.infoPgtoExt.endExt.endDscLograd,
            new XElement(ns + "endDscLograd", ideBenef.infoPgto.infoPgtoExt.endExt.endDscLograd),
            new XElement(ns + "endNrLograd", ideBenef.infoPgto.infoPgtoExt.endExt.endNrLograd),
            new XElement(ns + "endComplem", ideBenef.infoPgto.infoPgtoExt.endExt.endComplem),
            new XElement(ns + "endBairro", ideBenef.infoPgto.infoPgtoExt.endExt.endBairro),
            new XElement(ns + "endCidade", ideBenef.infoPgto.infoPgtoExt.endExt.endCidade),
            new XElement(ns + "endEstado", ideBenef.infoPgto.infoPgtoExt.endExt.endEstado),
            new XElement(ns + "endCodPostal", ideBenef.infoPgto.infoPgtoExt.endExt.endCodPostal),
            new XElement(ns + "telef", ideBenef.infoPgto.infoPgtoExt.endExt.telef)));
                  
            ideBenef.infoPgto.infoPgtoExt.endExt = new sIdeBenef.sInfoPgto.sInfoPgtoExt.sEndExt();
         }
      #endregion


      #region infoIRComplem

      List<XElement> lIfoIRComplem = new List<XElement>();
      public void add_infoIRComplem()
      {

         lIfoIRComplem.Add(

         opElement("infoIRComplem", ideBenef.infoIRComplem.temInfoIRComplem,
         opElement("dtLaudo", ideBenef.infoIRComplem.dtLaudo),

         // infoDep 0.999
         from e in lInfoDep
         select e,

         // infoIRCR 0.99
         from e in lInfoIRCR
         select e,

         // planSaude 0.99
         from e in lPlanSaude
         select e,

         // infoReembMed 0.99
         from e in lInfoReembMed
         select e

         ));

         ideBenef.infoIRComplem = new sIdeBenef.sInfoIRComple();
         lInfoDep = new List<XElement>();
         lPlanSaude = new List<XElement>();
         lInfoDepSau = new List<XElement>();
         lInfoReembMed = new List<XElement>();
      }


      List<XElement> lInfoDep = new List<XElement>();
      public void add_infoDep()
      {

         lInfoDep.Add(

         opElement("infoDep", ideBenef.infoIRComplem.infoDep.cpfDep,
         new XElement(ns + "cpfDep", ideBenef.infoIRComplem.infoDep.cpfDep),
         new XElement(ns + "dtNascto", ideBenef.infoIRComplem.infoDep.dtNascto),
         new XElement(ns + "nome", ideBenef.infoIRComplem.infoDep.nome),
         new XElement(ns + "depIRRF", ideBenef.infoIRComplem.infoDep.depIRRF),
         new XElement(ns + "tpDep", ideBenef.infoIRComplem.infoDep.tpDep),
         new XElement(ns + "descrDep", ideBenef.infoIRComplem.infoDep.descrDep)

         ));

         ideBenef.infoIRComplem.infoDep = new sIdeBenef.sInfoIRComple.sInfoDep();
      }

      List<XElement> lInfoIRCR = new List<XElement>();
      public void add_infoIRCR()
      {

         lInfoIRCR.Add(

         opElement("infoIRCR", ideBenef.infoIRComplem.infoIRCR.tpCR,
         new XElement(ns + "tpCR", ideBenef.infoIRComplem.infoIRCR.tpCR),

         // dedPen 0.999
         from e in lDedPen
         select e,

         // penAlim 0.999
         from e in lPenAlim
         select e,

         // previdCompl 0.999
         from e in lPrevidCompl
         select e,

         // infoProcRet 0.50
         from e in lInfoProcRet
         select e

         ));

         ideBenef.infoIRComplem.infoIRCR = new sIdeBenef.sInfoIRComple.sInfoIRCR();
         lDedPen = new List<XElement>();
         lPenAlim = new List<XElement>();
         lPrevidCompl = new List<XElement>();
         lInfoProcRet = new List<XElement>();
         lInfoValores = new List<XElement>();
      }

      List<XElement> lDedPen = new List<XElement>();
      public void add_dedPen()
      {

         lDedPen.Add(

         opElement("dedDepen", ideBenef.infoIRComplem.infoIRCR.dedDepen.tpRend,
         new XElement(ns + "tpRend", ideBenef.infoIRComplem.infoIRCR.dedDepen.tpRend),
         new XElement(ns + "cpfDep", ideBenef.infoIRComplem.infoIRCR.dedDepen.cpfDep),
         new XElement(ns + "vlrDedDep", ideBenef.infoIRComplem.infoIRCR.dedDepen.vlrDedDep)

         ));

         ideBenef.infoIRComplem.infoIRCR.dedDepen = new sIdeBenef.sInfoIRComple.sInfoIRCR.sDedDepen();
      }

      List<XElement> lPenAlim = new List<XElement>();
      public void add_penAlim()
      {

         lPenAlim.Add(

         opElement("penAlim", ideBenef.infoIRComplem.infoIRCR.penAlim.tpRend,
         new XElement(ns + "tpRend", ideBenef.infoIRComplem.infoIRCR.penAlim.tpRend),
         new XElement(ns + "cpfDep", ideBenef.infoIRComplem.infoIRCR.penAlim.cpfDep),
         new XElement(ns + "vlrDedPenAlim", ideBenef.infoIRComplem.infoIRCR.penAlim.vlrDedPenAlim)

         ));

         ideBenef.infoIRComplem.infoIRCR.penAlim = new sIdeBenef.sInfoIRComple.sInfoIRCR.sPenAlim();
      }

      List<XElement> lPrevidCompl = new List<XElement>();
      public void add_previdCompl()
      {

         lPrevidCompl.Add(

         opElement("previdCompl", ideBenef.infoIRComplem.infoIRCR.previdCompl.tpPrev,
         new XElement(ns + "tpPrev", ideBenef.infoIRComplem.infoIRCR.previdCompl.tpPrev),
         new XElement(ns + "cnpjEntidPC", ideBenef.infoIRComplem.infoIRCR.previdCompl.cnpjEntidPC),
         new XElement(ns + "vlrDedPC", ideBenef.infoIRComplem.infoIRCR.previdCompl.vlrDedPC),
         new XElement(ns + "vlrPatrocFunp", ideBenef.infoIRComplem.infoIRCR.previdCompl.vlrPatrocFunp)

         ));

         ideBenef.infoIRComplem.infoIRCR.previdCompl = new sIdeBenef.sInfoIRComple.sInfoIRCR.sPrevidCompl();
      }


      List<XElement> lInfoProcRet = new List<XElement>();
      public void add_infoProcRet()
      {

         lInfoProcRet.Add(

         opElement("infoProcRet", ideBenef.infoIRComplem.infoIRCR.infoProcRet.tpProcRet,
         new XElement(ns + "tpProcRet", ideBenef.infoIRComplem.infoIRCR.infoProcRet.tpProcRet),
         new XElement(ns + "nrProcRet", ideBenef.infoIRComplem.infoIRCR.infoProcRet.nrProcRet),
         new XElement(ns + "codSusp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.codSusp),

         // infoProcRet 0.2
         from e in lInfoValores
         select e

         ));

         ideBenef.infoIRComplem.infoIRCR.infoProcRet = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet();
      }

      List<XElement> lInfoValores = new List<XElement>();
      public void add_infoValores()
      {

         lInfoValores.Add(

         opElement("infoValores", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.indApuracao,
         new XElement(ns + "indApuracao", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.indApuracao),
         new XElement(ns + "vlrNRetido", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrNRetido),
         new XElement(ns + "vlrDepJud", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrDepJud),
         new XElement(ns + "vlrCmpAnoCal", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrDepJud),
         new XElement(ns + "vlrCmpAnoAnt", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrCmpAnoAnt),
         new XElement(ns + "vlrRendSusp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrRendSusp),

         // dedSusp 0.25
         from e in lDedSusp
         select e

         ));

         ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet.sInfoValores();
      }

      List<XElement> lDedSusp = new List<XElement>();
      public void add_dedSusp()
      {

         lDedSusp.Add(

         opElement("dedSusp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.indTpDeducao,
         new XElement(ns + "indTpDeducao", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.indTpDeducao),
         new XElement(ns + "vlrDedSusp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.vlrDedSusp),
         new XElement(ns + "cnpjEntidPC", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.cnpjEntidPC),
         new XElement(ns + "vlrPatrocFunp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.vlrPatrocFunp),

         // benefPen 0.99
         from e in lBenefPen
         select e

         ));

         ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet.sInfoValores.sDedSusp();
      }

      List<XElement> lBenefPen = new List<XElement>();
      public void add_benefPen()
      {

         lBenefPen.Add(

         opElement("benefPen", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.benefPen.cpfDep,
         new XElement(ns + "cpfDep", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.benefPen.cpfDep),
         new XElement(ns + "vlrDepenSusp", ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.benefPen.vlrDepenSusp)

         ));

         ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.dedSusp.benefPen = new sIdeBenef.sInfoIRComple.sInfoIRCR.sInfoProcRet.sInfoValores.sDedSusp.sBenefPen();
      }

      List<XElement> lPlanSaude = new List<XElement>();
      public void add_planSaude()
      {

         lPlanSaude.Add(

         opElement("planSaude", ideBenef.infoIRComplem.planSaude.cnpjOper,
         new XElement(ns + "cnpjOper", ideBenef.infoIRComplem.planSaude.cnpjOper),
         new XElement(ns + "regANS", ideBenef.infoIRComplem.planSaude.regANS),
         new XElement(ns + "vlrSaudeTit", ideBenef.infoIRComplem.planSaude.vlrSaudeTit),

         // infoDepSau 0.99
         from e in lInfoDepSau
         select e

         ));

         ideBenef.infoIRComplem.planSaude = new  sIdeBenef.sInfoIRComple.sPlanSaude();
      }

      List<XElement> lInfoDepSau = new List<XElement>();
      public void add_infoDepSau()
      {

         lInfoDepSau.Add(

         opElement("infoDepSau", ideBenef.infoIRComplem.planSaude.infoDepSau.cpfDep,
         new XElement(ns + "cpfDep", ideBenef.infoIRComplem.planSaude.infoDepSau.cpfDep),
         new XElement(ns + "vlrSaudeDep", ideBenef.infoIRComplem.planSaude.infoDepSau.vlrSaudeDep)

         ));

         ideBenef.infoIRComplem.planSaude.infoDepSau = new sIdeBenef.sInfoIRComple.sPlanSaude.sInfoDepSau();
      }

      List<XElement> lInfoReembMed = new List<XElement>();
      public void add_infoReembMed()
      {

         lInfoReembMed.Add(

         opElement("infoReembMed", ideBenef.infoIRComplem.infoReembMed.indOrgReemb,
         new XElement(ns + "indOrgReemb", ideBenef.infoIRComplem.infoReembMed.indOrgReemb),
         new XElement(ns + "cnpjOper", ideBenef.infoIRComplem.infoReembMed.cnpjOper)

         ));

         ideBenef.infoIRComplem.infoReembMed = new sIdeBenef.sInfoIRComple.sInfoReembMed();
      }
      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao, perApur, indGuia, verProc, nrRecibo;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sIdeBenef ideBenef;
        public struct sIdeBenef {
            public string cpfBenef;

            public sDeps deps;
            public struct sDeps { public string vrDedDep; }

            public sInfoPgto infoPgto;
            public struct sInfoPgto {
                public string dtPgto;
                public string tpPgto;
                public string perRef;
                public string ideDmDev;
                public string vrLiq;
                public string paisResidExt;


               public sInfoPgtoExt infoPgtoExt;
               public struct sInfoPgtoExt
               {
                  public string indNIF;
                  public string nifBenef;
                  public string frmTribut;

                  public sEndExt endExt;
                  public struct sEndExt
                  {
                     public string endDscLograd;
                     public string endNrLograd;
                     public string endComplem;
                     public string endBairro;
                     public string endCidade;
                     public string endEstado;
                     public string endCodPostal;
                     public string telef;
                  }
               }
            }

            public sInfoIRComple infoIRComplem;
            public struct sInfoIRComple
            {
               public string dtLaudo, temInfoIRComplem;

               public sInfoDep infoDep;
               public struct sInfoDep
               {
                  public string cpfDep;
                  public string dtNascto;
                  public string nome;
                  public string depIRRF;
                  public string tpDep;
                  public string descrDep;
               }

               public sInfoIRCR infoIRCR;
               public struct sInfoIRCR
               {
                  public string tpCR;

                  public sDedDepen dedDepen;
                  public struct sDedDepen
                  {
                     public string tpRend;
                     public string cpfDep;
                     public string vlrDedDep;
                  }

                  public sPenAlim penAlim;
                  public struct sPenAlim
                  {
                     public string tpRend;
                     public string cpfDep;
                     public string vlrDedPenAlim;
                  }

                  public sPrevidCompl previdCompl;
                  public struct sPrevidCompl
                  {
                     public string tpPrev;
                     public string cnpjEntidPC;
                     public string vlrDedPC;
                     public string vlrPatrocFunp;
                  }

                  public sInfoProcRet infoProcRet;
                  public struct sInfoProcRet
                  {
                     public string tpProcRet;
                     public string nrProcRet;
                     public string codSusp;

                     public sInfoValores infoValores;
                     public struct sInfoValores
                     {
                        public string indApuracao;
                        public string vlrNRetido;
                        public string vlrDepJud;
                        public string vlrCmpAnoCal;
                        public string vlrCmpAnoAnt;
                        public string vlrRendSusp;

                        public sDedSusp dedSusp;
                        public struct sDedSusp
                        {
                           public string indTpDeducao;
                           public string vlrDedSusp;
                           public string cnpjEntidPC;
                           public string vlrPatrocFunp;

                           public sBenefPen benefPen;
                           public struct sBenefPen
                           {
                              public string cpfDep;
                              public string vlrDepenSusp;
                           }
                        }
                     }
                  }

               }
               public sPlanSaude planSaude;
               public struct sPlanSaude
               {
                  public string cnpjOper;
                  public string regANS;
                  public string vlrSaudeTit;

                  public sInfoDepSau infoDepSau;
                  public struct sInfoDepSau
                  {
                     public string cpfDep;
                     public string vlrSaudeDep;
                  }
               }

               public sInfoReembMed infoReembMed;
               public struct sInfoReembMed
               {
                  public string indOrgReemb;
                  public string cnpjOper;
               }
            }
         }
         #endregion
      }
}
