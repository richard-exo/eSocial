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
    public class s1200 : bEvento_XML {

        public s1200(string sID) : base("evtRemun", "", "v_S_01_03_00") {

            id = sID;

            ideEvento = new sIdeEvento();

            ideTrabalhador = new sIdeTrabalhador();
            ideTrabalhador.infoMV = new sIdeTrabalhador.sInfoMV();
            ideTrabalhador.infoMV.remunOutrEmpr = new sIdeTrabalhador.sInfoMV.sRemunOutrEmpr();

            ideTrabalhador.infoComplem = new sIdeTrabalhador.sInfoComplem();
            ideTrabalhador.infoComplem.sucessaoVinc = new sIdeTrabalhador.sInfoComplem.sSucessaoVinc();

            ideTrabalhador.infoInterm = new sIdeTrabalhador.sInfoInterm();
            ideTrabalhador.procJudTrab = new sIdeTrabalhador.sProcJudTrab();

            dmDev = new sDmDev();
            dmDev.infoComplCont = new sDmDev.sInfoComplCont();

            dmDev.infoPerApur = new sDmDev.sInfoPerApur();
            dmDev.infoPerApur.ideEstabLot = new sIdeEstabLot();
            dmDev.infoPerApur.ideEstabLot.remunPerApur = new sIdeEstabLot.sRemunPer();
            //dmDev.infoPerApur.ideEstabLot.remunPerApur.descFolha = new sIdeEstabLot.sRemunPer.sDescFolha();
            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet = new sIdeEstabLot.sRemunPer.sInfoSaudeColet();
            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper = new sIdeEstabLot.sRemunPer.sInfoSaudeColet.sDetOper();
            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano = new sIdeEstabLot.sRemunPer.sInfoSaudeColet.sDetOper.sDetPlano();
            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoAgNocivo = new sIdeEstabLot.sRemunPer.sInfoAgNocivo();

            dmDev.infoPerAnt = new sDmDev.sInfoPerAnt();
            dmDev.infoPerAnt.ideADC = new sDmDev.sInfoPerAnt.sIdeADC();
            dmDev.infoPerAnt.ideADC.idePeriodo = new sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo();
            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot = new sIdeEstabLot();
            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerApur = new sIdeEstabLot.sRemunPer();
            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerApur.infoTrabInterm = new sIdeEstabLot.sRemunPer.sInfoTrabInterm();
        }

        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

            new XElement(ns + "indRetif", ideEvento.indRetif),
            opTag("nrRecibo", ideEvento.nrRecibo),
            new XElement(ns + "indApuracao", ideEvento.indApuracao),
            new XElement(ns + "perApur", ideEvento.perApur),
            opTag("indGuia", ideEvento.indGuia),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode().ToString()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode().ToString()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // ideTrabalhador
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "ideTrabalhador",
            new XElement(ns + "cpfTrab", ideTrabalhador.cpfTrab),

            // infoMV 0.1
            opElement("infoMV", ideTrabalhador.infoMV.indMV,
            new XElement(ns + "indMV", ideTrabalhador.infoMV.indMV),

            // remunOutrEmpr 1.10
            from e in lRemunOutrEmpr
            select e),

            // infoComplem 0.1
            opElement("infoComplem", ideTrabalhador.infoComplem.nmTrab,
            new XElement(ns + "nmTrab", ideTrabalhador.infoComplem.nmTrab),
            new XElement(ns + "dtNascto", ideTrabalhador.infoComplem.dtNascto),

            // sucessaoVinc 0.1
            opElement("sucessaoVinc", ideTrabalhador.infoComplem.sucessaoVinc.nrInsc,
            new XElement(ns + "tpInsc", ideTrabalhador.infoComplem.sucessaoVinc.tpInsc),
            new XElement(ns + "nrInsc", ideTrabalhador.infoComplem.sucessaoVinc.nrInsc),
            opTag("matricAnt", ideTrabalhador.infoComplem.sucessaoVinc.matricAnt),
            new XElement(ns + "dtAdm", ideTrabalhador.infoComplem.sucessaoVinc.dtAdm),
            opTag("observacao", ideTrabalhador.infoComplem.sucessaoVinc.observacao))

            ),

            // procJudTrab 0.99
            from e in lProcJudTrab
            select e,

            // infoInterm 0.31
            //opElement("infoInterm", lInfoInterm,
            //from e in lInfoInterm
            //select e)            
            from e in lInfoInterm
            select e

            )); // ideTrabalhador

            // dmDev 1.99
            xml.Elements().ElementAt(0).Add(
            from e in lDmDev
            select e);

            return x509.signXMLSHA256(xml, cert);
        }

        #region *************************************************************************************************************** Tags com +1 ocorrência

        #region remunOutrEmpr   

        List<XElement> lRemunOutrEmpr = new List<XElement>();
        public void add_remunOutrEmpr() {

            lRemunOutrEmpr.Add(
            new XElement(ns + "remunOutrEmpr",
            new XElement(ns + "tpInsc", ideTrabalhador.infoMV.remunOutrEmpr.tpInsc),
            new XElement(ns + "nrInsc", ideTrabalhador.infoMV.remunOutrEmpr.nrInsc),
            new XElement(ns + "codCateg", ideTrabalhador.infoMV.remunOutrEmpr.codCateg),
            new XElement(ns + "vlrRemunOE", ideTrabalhador.infoMV.remunOutrEmpr.vlrRemunOE)));

            ideTrabalhador.infoMV.remunOutrEmpr = new sIdeTrabalhador.sInfoMV.sRemunOutrEmpr();

        }
        #endregion

        #region procJudTrab

        List<XElement> lProcJudTrab = new List<XElement>();
        public void add_procJudTrab() {

            lProcJudTrab.Add(
            new XElement(ns + "procJudTrab",
            new XElement(ns + "tpTrib", ideTrabalhador.procJudTrab.tpTrib),
            new XElement(ns + "nrProcJud", ideTrabalhador.procJudTrab.nrProcJud),
            new XElement(ns + "codSusp", ideTrabalhador.procJudTrab.codSusp)));

            ideTrabalhador.procJudTrab = new sIdeTrabalhador.sProcJudTrab();
        }
      #endregion

      #region infoInterm
      List<XElement> lInfoInterm = new List<XElement>();
      public void add_infoInterm()
      {

         lInfoInterm.Add(
         new XElement(ns + "infoInterm",
         new XElement(ns + "dia", ideTrabalhador.infoInterm.dia)));

         ideTrabalhador.infoInterm = new sIdeTrabalhador.sInfoInterm();
      }
      #endregion

      #region dmDev

      List<XElement> lDmDev = new List<XElement>();
        public void add_dmDev() {

            lDmDev.Add(

            new XElement(ns + "dmDev",
            new XElement(ns + "ideDmDev", dmDev.ideDmDev),
            new XElement(ns + "codCateg", dmDev.codCateg),

            // infoPerApur 0.1
            opElement("infoPerApur", lIdeEstabLot_infoPerApur,

            // ideEstabLot 1.500
            from e in lIdeEstabLot_infoPerApur
            select e),

            // infoPerAnt 0.1
            opElement("infoPerAnt", lIdeAdc,

            // ideADC 1.8
            from e in lIdeAdc
            select e),

            // infoComplCont 0.1
            opElement("infoComplCont", dmDev.infoComplCont.codCBO,
            new XElement(ns + "codCBO", dmDev.infoComplCont.codCBO),
            opTag("natAtividade", dmDev.infoComplCont.natAtividade),
            opTag("qtdDiasTrab", dmDev.infoComplCont.qtdDiasTrab))

            ));

            dmDev = new sDmDev();
            dmDev.infoPerApur = new sDmDev.sInfoPerApur();
            dmDev.infoPerAnt = new sDmDev.sInfoPerAnt();
            dmDev.infoComplCont = new sDmDev.sInfoComplCont();

            lIdeEstabLot_infoPerApur = new List<XElement>();

        }
        #endregion

        #region ideEstabLot_infoPerApur

        List<XElement> lIdeEstabLot_infoPerApur = new List<XElement>();
        public void add_ideEstabLot_infoPerApur() {

            lIdeEstabLot_infoPerApur.Add(
            new XElement(ns + "ideEstabLot",
            new XElement(ns + "tpInsc", dmDev.infoPerApur.ideEstabLot.tpInsc),
            new XElement(ns + "nrInsc", dmDev.infoPerApur.ideEstabLot.nrInsc),
            new XElement(ns + "codLotacao", dmDev.infoPerApur.ideEstabLot.codLotacao),
            opTag("qtDiasAv", dmDev.infoPerApur.ideEstabLot.qtdDiasAv),

            // remunPerApur 1.8
            from e in lRemunPerApur
            select e));

            dmDev.infoPerApur.ideEstabLot = new sIdeEstabLot();

            lItensRemun_infoPerApur = new List<XElement>();
            lRemunPerApur = new List<XElement>();
        }
        #endregion

        #region remunPerApur

        List<XElement> lRemunPerApur = new List<XElement>();
        public void add_remunPerApur() {

            lRemunPerApur.Add(
            new XElement(ns + "remunPerApur",
            opTag("matricula", dmDev.infoPerApur.ideEstabLot.remunPerApur.matricula),
            opTag("indSimples", dmDev.infoPerApur.ideEstabLot.remunPerApur.indSimples),

            // itensRemun 1.200
            from e in lItensRemun_infoPerApur
            select e,

           //// infoSaudeColet 0.1
           //opElement("infoSaudeColet", lDetOper_infoPerApur,

           //// detOper 1.99
           //from e in lDetOper_infoPerApur
           //select e),

           // infoAgNocivo 0.1
           opElement("infoAgNocivo", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoAgNocivo.grauExp,
           new XElement(ns + "grauExp", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoAgNocivo.grauExp))));

           //// infoTrabInterm 0.99
           //from e in lInfoTrabInterm_infoPerApur
           //select e));

            dmDev.infoPerApur.ideEstabLot.remunPerApur = new sIdeEstabLot.sRemunPer();
            //dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet = new sIdeEstabLot.sRemunPer.sInfoSaudeColet();
            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoAgNocivo = new sIdeEstabLot.sRemunPer.sInfoAgNocivo();
        }
        #endregion

        #region itensRemun_infoPerApur

        List<XElement> lItensRemun_infoPerApur = new List<XElement>();
        public void add_itensRemun_infoPerApur() {

            lItensRemun_infoPerApur.Add(

            new XElement(ns + "itensRemun",
            new XElement(ns + "codRubr", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.codRubr),
            new XElement(ns + "ideTabRubr", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.ideTabRubr),
            opTag("qtdRubr", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.qtdRubr),
            opTag("fatorRubr", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.fatorRubr),
            new XElement(ns + "vrRubr", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.vrRubr),
            opTag("indApurIR", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.indApurIR),

            // descFolha 0.1
            opElement("descFolha", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.descFolha.tpDesc,
            new XElement(ns + "tpDesc", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.descFolha.tpDesc),
            new XElement(ns + "instFinanc", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.descFolha.instFinanc),
            new XElement(ns + "nrDoc", dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.descFolha.nrContrato))

            ));

            dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun = new sIdeEstabLot.sRemunPer.sItensRemun();
      }
      #endregion

      #region detOper_infoPerApur

      List<XElement> lDetOper_infoPerApur = new List<XElement>();
        public void add_detOper_infoPerApur() {

            lDetOper_infoPerApur.Add(
            new XElement(ns + "detOper",
            new XElement(ns + "cnpjOper", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.cnpjOper),
            new XElement(ns + "regANS", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.regANS),
            new XElement(ns + "vrPgTit", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.vrPgTit),

            // detPlano 0.99
            from e in lDetPlano_infoPerApur
            select e));

            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper = new sIdeEstabLot.sRemunPer.sInfoSaudeColet.sDetOper();
        }
        #endregion

        #region detPlano_infoPerApur

        List<XElement> lDetPlano_infoPerApur = new List<XElement>();
        public void add_detPlano_infoPerApur() {

            lDetPlano_infoPerApur.Add(
            new XElement(ns + "detPlano",
            new XElement(ns + "tpDep", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano.tpDep),
            opTag("cpfDep", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano.cpfDep),
            new XElement(ns + "nmDep", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano.nmDep),
            new XElement(ns + "dtNascto", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano.dtNascto),
            new XElement(ns + "vlrPgDep", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano.vlrPgDep)));

            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.detPlano = new sIdeEstabLot.sRemunPer.sInfoSaudeColet.sDetOper.sDetPlano();
        }
        #endregion

        #region infoTrabInterm_infoPerApur

        List<XElement> lInfoTrabInterm_infoPerApur = new List<XElement>();
        public void add_infoTrabInterm_infoPerApur() {

            lInfoTrabInterm_infoPerApur.Add(
            new XElement(ns + "infoTrabInterm",
            new XElement(ns + "codConv", dmDev.infoPerApur.ideEstabLot.remunPerApur.infoTrabInterm.codConv)));

            dmDev.infoPerApur.ideEstabLot.remunPerApur.infoTrabInterm = new sIdeEstabLot.sRemunPer.sInfoTrabInterm();
        }
        #endregion

        #region ideAdc

        List<XElement> lIdeAdc = new List<XElement>();
        public void add_ideAdc() {

            lIdeAdc.Add(
            new XElement(ns + "ideADC",
            opTag("dtAcConv", dmDev.infoPerAnt.ideADC.dtAcConv),
            new XElement(ns + "tpAcConv", dmDev.infoPerAnt.ideADC.tpAcConv),
            new XElement(ns + "dsc", dmDev.infoPerAnt.ideADC.dsc),
            new XElement(ns + "remunSuc", dmDev.infoPerAnt.ideADC.remunSuc),

             // idePeriodo 1.180
             from e in lIdePeriodo
             select e));

            dmDev.infoPerAnt.ideADC = new sDmDev.sInfoPerAnt.sIdeADC();

            lIdePeriodo = new List<XElement>();
            lIdeEstabLot_infoPerAnt = new List<XElement>();
            lRemunPerAnt_infoPerAnt = new List<XElement>();
            lItensRemun_infoPerAnt = new List<XElement>();
        }
        #endregion

        #region idePeriodo
        List<XElement> lIdePeriodo = new List<XElement>();
        public void add_idePeriodo() {

            lIdePeriodo.Add(
            new XElement(ns + "idePeriodo",
            new XElement(ns + "perRef", dmDev.infoPerAnt.ideADC.idePeriodo.perRef),

            // ideEstabLot 1.500
            from e in lIdeEstabLot_infoPerAnt
            select e));

            dmDev.infoPerAnt.ideADC.idePeriodo = new sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo();
        }

        #endregion

        #region ideEstabLot_infoPerAnt
        List<XElement> lIdeEstabLot_infoPerAnt = new List<XElement>();
        public void add_ideEstabLot_infoPerAnt() {

            lIdeEstabLot_infoPerAnt.Add(
            new XElement(ns + "ideEstabLot",
            new XElement(ns + "tpInsc", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.tpInsc),
            new XElement(ns + "nrInsc", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.nrInsc),
            new XElement(ns + "codLotacao", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.codLotacao),

            // remunPerApur 1.8
            from e in lRemunPerAnt_infoPerAnt
            select e));

            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot = new sIdeEstabLot();
        }

        #endregion

        #region remunPerAnt

        List<XElement> lRemunPerAnt_infoPerAnt = new List<XElement>();
        public void add_remunPerAnt_infoPerAnt() {

            lRemunPerAnt_infoPerAnt.Add(
            new XElement(ns + "remunPerAnt",
            opTag("matricula", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.matricula),
            opTag("indSimples", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.indSimples),

            // itensRemun 1.200
            from e in lItensRemun_infoPerAnt
            select e,

            // infoAgNocivo 0.1
            opElement("infoAgNocivo", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.infoAgNocivo.grauExp,
            new XElement(ns + "grauExp", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.infoAgNocivo.grauExp))));

            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt = new sIdeEstabLot.sRemunPer();
            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.infoAgNocivo = new sIdeEstabLot.sRemunPer.sInfoAgNocivo();
        }

        #endregion

        #region itensRemun_infoPerAnt
        List<XElement> lItensRemun_infoPerAnt = new List<XElement>();
        public void add_itensRemun_infoPerAnt() {

            lItensRemun_infoPerAnt.Add(
            new XElement(ns + "itensRemun",
            new XElement(ns + "codRubr", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.codRubr),
            new XElement(ns + "ideTabRubr", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.ideTabRubr),
            opTag("qtdRubr", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.qtdRubr),
            opTag("fatorRubr", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.fatorRubr),
            new XElement(ns + "vrRubr", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.vrRubr),
            opTag("indApurIR", dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun.indApurIR)));

            dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.itensRemun = new sIdeEstabLot.sRemunPer.sItensRemun();
        }
        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao;
            public string perApur, indGuia, verProc, nrRecibo;
            public enProcEmi procEmi;
            public enTpAmb tpAmb;
        }

        public sIdeTrabalhador ideTrabalhador;
        public struct sIdeTrabalhador {

            public string cpfTrab;
            public string nisTrab;

            public sInfoMV infoMV;
            public struct sInfoMV {

                public string indMV;

                public sRemunOutrEmpr remunOutrEmpr;
                public struct sRemunOutrEmpr {
                    public string tpInsc, codCateg;
                    public string vlrRemunOE;
                    public string nrInsc;
                }
            }

            public sInfoComplem infoComplem;
            public struct sInfoComplem {

                public string nmTrab, dtNascto;

                public sSucessaoVinc sucessaoVinc;
                public struct sSucessaoVinc {
                    public string tpInsc, nrInsc, matricAnt, observacao;
                    public string dtAdm;
                }
            }

            public sInfoInterm infoInterm;
            public struct sInfoInterm { public string dia; }

            public sProcJudTrab procJudTrab;
            public struct sProcJudTrab {
                public string tpTrib, codSusp;
                public string nrProcJud;
            }
        }
        public struct sIdeEstabLot {
            public string tpInsc, qtdDiasAv;
            public string nrInsc, codLotacao;

            public sRemunPer remunPerApur;
            public sRemunPer remunPerAnt;
            public struct sRemunPer {

                public string matricula, indSimples;

                public sItensRemun itensRemun;
                public struct sItensRemun { public string codRubr, ideTabRubr, qtdRubr, fatorRubr, vrUnit, vrRubr, indApurIR;

                  public sDescFolha descFolha;
                  public struct sDescFolha { public string tpDesc, instFinanc, nrContrato; }
                }

                public sInfoSaudeColet infoSaudeColet;
                public struct sInfoSaudeColet {

                    public sDetOper detOper;
                    public struct sDetOper {

                        public string cnpjOper, regANS;
                        public string vrPgTit;

                        public sDetPlano detPlano;
                        public struct sDetPlano {
                            public string nmDep, cpfDep, tpDep;
                            public string vlrPgDep;
                            public string dtNascto;
                        }
                    }
                }
                public sInfoAgNocivo infoAgNocivo;
                public struct sInfoAgNocivo {
                    public string grauExp;
                }

                public sInfoTrabInterm infoTrabInterm;
                public struct sInfoTrabInterm {
                    public string codConv;
                }
            }
        }

        public sDmDev dmDev;
        public struct sDmDev {
            public string codCateg;
            public string ideDmDev;

            public sInfoPerApur infoPerApur;
            public struct sInfoPerApur {
                public sIdeEstabLot ideEstabLot;
            }

            public sInfoPerAnt infoPerAnt;
            public struct sInfoPerAnt {
                public sIdeADC ideADC;
                public struct sIdeADC {

                    public string dtAcConv, dtEfAcConv, remunSuc;

                    public string tpAcConv, dsc, compAcConv;

                    public sIdePeriodo idePeriodo;
                    public struct sIdePeriodo {
                        public string perRef;
                        public sIdeEstabLot ideEstabLot;
                    }
                }
            }

            public sInfoComplCont infoComplCont;
            public struct sInfoComplCont {
                public string codCBO;
                public string natAtividade, qtdDiasTrab;
            }

        }
        #endregion
    }
}
