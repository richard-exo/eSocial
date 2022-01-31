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
    public class s2299 : bEvento_XML {

        public s2299(string sID) : base("evtDeslig", "infoDeslig") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideVinculo = new sIdeVinculo();

            infoDeslig = new sInfoDeslig();
            infoDeslig.observacoes = new sInfoDeslig.sObservacoes();
            infoDeslig.sucessaoVinc = new sInfoDeslig.sSucessaoVinc();
            infoDeslig.transfTit = new sInfoDeslig.sTransfTit();
            infoDeslig.mudancaCPF = new sInfoDeslig.sMudancaCPF();

            infoDeslig.verbasResc = new sInfoDeslig.sVerbasResc();
            infoDeslig.verbasResc.dmDev = new sInfoDeslig.sVerbasResc.sDmDev();
            infoDeslig.verbasResc.dmDev.infoPerApur = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur();

            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sDetVerbas();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet.sDetOper();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet.sDetOper.sDetPlano();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoAgNocivo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoAgNocivo();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSimples = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSimples();

            infoDeslig.verbasResc.dmDev.infoPerAnt = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoAgNocivo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot.sInfoAgNocivo();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoSimples = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot.sInfoSimples();

            infoDeslig.verbasResc.dmDev.infoTrabInterm = new sInfoDeslig.sVerbasResc.sDmDev.sInfoTrabInterm();

            infoDeslig.verbasResc.procJudTrab = new sInfoDeslig.sVerbasResc.sProcJudTrab();
            infoDeslig.verbasResc.infoMV = new sInfoDeslig.sVerbasResc.sInfoMV();
            infoDeslig.verbasResc.infoMV.remunOutrEmpr = new sInfoDeslig.sVerbasResc.sInfoMV.sRemunOutrEmpr();

            infoDeslig.verbasResc.procCS = new sInfoDeslig.sVerbasResc.sProcCS();

            infoDeslig.quarentena = new sInfoDeslig.sQuarentena();
            infoDeslig.consigFGTS = new sInfoDeslig.sConsigFGTS();

        }

        public override XElement genSignedXML(X509Certificate2 cert) {

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
            new XElement(ns + "ideVinculo",
            new XElement(ns + "cpfTrab", ideVinculo.cpfTrab),
            new XElement(ns + "nisTrab", ideVinculo.nisTrab),
            new XElement(ns + "matricula", ideVinculo.matricula)));

            // infoDeslig
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            new XElement(ns + "mtvDeslig", infoDeslig.mtvDeslig),
            new XElement(ns + "dtDeslig", infoDeslig.dtDeslig),
            new XElement(ns + "indPagtoAPI", infoDeslig.indPagtoAPI),
            opTag("dtProjFimAPI", infoDeslig.dtProjFimAPI),
            new XElement(ns + "pensAlim", infoDeslig.pensAlim),
            opTag("percAliment", infoDeslig.percAliment),
            opTag("vrAlim", infoDeslig.vrAlim),
            opTag("nrCertObito", infoDeslig.nrCertObito),
            opTag("nrProcTrab", infoDeslig.nrProcTrab),
            new XElement(ns + "indCumprParc", infoDeslig.indCumprParc),
            opTag("qtdDiasInterm", infoDeslig.qtdDiasInterm),

            // observacoes 0.99
            from e in lObservacoes
            select e,

            // sucessaoVinc 0.1
            opElement("sucessaoVinc", infoDeslig.sucessaoVinc.cnpjSucessora,
            new XElement(ns + "tpInscSuc", infoDeslig.sucessaoVinc.tpInscSuc),
            new XElement(ns + "cnpjSucessora", infoDeslig.sucessaoVinc.cnpjSucessora)),

            // transTit 0.1
            opElement("transTit", infoDeslig.transfTit.cpfSubstituto,
            new XElement(ns + "cpfSubstituto", infoDeslig.transfTit.cpfSubstituto),
            new XElement(ns + "dtNascto", infoDeslig.transfTit.dtNascto)),

            // mudancaCPF 0.1
            opElement("mudancaCPF", infoDeslig.mudancaCPF.novoCPF,
            new XElement(ns + "novoCPF", infoDeslig.mudancaCPF.novoCPF)),

            // verbasResc 0.1
            opElement("verbasResc", lDmDev, 

            // dmDev 1.50
            from e in lDmDev
            select e,

            // procJudTrab 0.99
            from e in lProcJudTrab
            select e,

            // infoMV 0.1
            opElement("infoMV", infoDeslig.verbasResc.infoMV.indMV,
            new XElement(ns + "indMV", infoDeslig.verbasResc.infoMV.indMV),

            // remunOutrEmpr 1.10
            from e in lRemunOutrEmpr
            select e),

            // procCS 0.1
            opElement("procCS", infoDeslig.verbasResc.procCS.nrProcJud,
            new XElement(ns + "nrProcJud", infoDeslig.verbasResc.procCS.nrProcJud))

            ), // verbasResc

            // quarentena 0.1
            opElement("quarentena", infoDeslig.quarentena.dtFimQuar,
            new XElement(ns + "dtFimQuar", infoDeslig.quarentena.dtFimQuar)),

            // consigFGTS 0.9
            from e in lConsigFGTS
            select e

            ); // infoDeslig

            return x509.signXMLSHA256(xml, cert);
        }

        #region *************************************************************************************************************** Tags com +1 ocorrência

        #region observacoes

        List<XElement> lObservacoes = new List<XElement>();
        public void add_observacoes() {

            lObservacoes.Add(
            new XElement(ns + "observacoes",
            new XElement(ns + "observacao", infoDeslig.observacoes.observacao)));

            infoDeslig.observacoes = new sInfoDeslig.sObservacoes();
        }

        #endregion

        #region dmDev  

        List<XElement> lDmDev = new List<XElement>();
        public void add_dmDev() {

            lDmDev.Add(

            new XElement(ns + "dmDev",
            new XElement(ns + "ideDmDev", infoDeslig.verbasResc.dmDev.ideDmDev),

            // infoPerApur 0.1
            opElement("infoPerApur", lIdeEstabLot_infoPerApur,

            // ideEstabLot 1.24
            from e in lIdeEstabLot_infoPerApur
            select e),

            // infoPerAnt 0.1
            opElement("infoPerAnt", lIdeADC,

            // ideADC 1.8
            from e in lIdeADC
            select e),

            // infoTrabInterm 0.99
            from e in lInfoTrabInterm
            select e

            ));

            infoDeslig.verbasResc.dmDev = new sInfoDeslig.sVerbasResc.sDmDev();
            infoDeslig.verbasResc.dmDev.infoPerApur = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur();
            infoDeslig.verbasResc.dmDev.infoPerAnt = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt();

            lIdeEstabLot_infoPerApur = new List<XElement>();
            lIdeADC = new List<XElement>();
            lInfoTrabInterm = new List<XElement>();
      }

      #endregion

      #region ideEstabLot

      List<XElement> lIdeEstabLot_infoPerApur = new List<XElement>();
        public void add_ideEstabLot_infoPerApur() {

            lIdeEstabLot_infoPerApur.Add(

            new XElement(ns + "ideEstabLot",
            new XElement(ns + "tpInsc", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.tpInsc),
            new XElement(ns + "nrInsc", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.nrInsc),
            new XElement(ns + "codLotacao", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.codLotacao),

            // detVerbas 1.200
            from e in lDetVerbas_infoPerApur
            select e,

            // infoSaudeColet 0.1
            opElement("infoSaudeColet", lDetOper,

            // detOper 1.99
            from e in lDetOper
            select e),

            // infoAgNocivo 0.1
            opElement("infoAgNocivo", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoAgNocivo.grauExp,
            new XElement(ns + "grauExp", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoAgNocivo.grauExp)),

            // infoSimples 0.1
            opElement("infoSimples", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSimples.indSimples,
            new XElement(ns + "indSimples", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSimples.indSimples))));

            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoAgNocivo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoAgNocivo();
            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSimples = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSimples();

            lDetVerbas_infoPerApur = new List<XElement>();
            lDetOper = new List<XElement>();
         }
        #endregion

        #region detVerbas

        List<XElement> lDetVerbas_infoPerApur = new List<XElement>();
        public void add_detVerbas_infoPerApur() {

            lDetVerbas_infoPerApur.Add(

            new XElement(ns + "detVerbas",
            new XElement(ns + "codRubr", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.codRubr),
            new XElement(ns + "ideTabRubr", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.ideTabRubr),
            opTag("qtdRubr", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.qtdRubr),
            opTag("fatorRubr", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.fatorRubr),
            opTag("vrUnit", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.vrUnit),
            new XElement(ns + "vrRubr", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.vrRubr)));

            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sDetVerbas();
        }
        #endregion

        #region detOper

        List<XElement> lDetOper = new List<XElement>();
        public void add_detOper() {

            lDetOper.Add(

            new XElement(ns + "detOper",
            new XElement(ns + "cnpjOper", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.cnpjOper),
            new XElement(ns + "regANS", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.regANS),
            new XElement(ns + "vrPgTit", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.vrPgTit),

            // detPlano 0.99
            from e in lDetPlano
            select e));

            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet.sDetOper();
        }
        #endregion

        #region detPlano

        List<XElement> lDetPlano = new List<XElement>();
        public void add_detPlano() {

            lDetPlano.Add(

            new XElement(ns + "detPlano",
            new XElement(ns + "tpDep", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.tpDep),
            opTag("cpfDep", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.cpfDep),
            new XElement(ns + "nmDep", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.nmDep),
            new XElement(ns + "dtNascto", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.dtNascto),
            new XElement(ns + "vlrPgDep", infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.vlrPgDep)));

            infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerApur.sIdeEstabLot.sInfoSaudeColet.sDetOper.sDetPlano();
        }
        #endregion

        #region ideADC

        List<XElement> lIdeADC = new List<XElement>();
        public void add_ideADC() {

            lIdeADC.Add(

            new XElement(ns + "ideADC",
            new XElement(ns + "dtAcConv", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dtAcConv),
            new XElement(ns + "tpAcConv", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.tpAcConv),
            opTag("compAcConv", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.compAcConv),
            new XElement(ns + "dtEfAcConv", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dtEfAcConv),
            new XElement(ns + "dsc", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dsc),

            // idePeriodo 1.180
            from e in lIdePeriodo
            select e));

            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC();

        }
        #endregion

        #region idePeriodo

        List<XElement> lIdePeriodo = new List<XElement>();
        public void add_idePeriodo() {

            lIdePeriodo.Add(

            new XElement(ns + "idePeriodo",
            new XElement(ns + "perRef", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.perRef),

            // ideEstabLot 1.24
            from e in lIdeEstabLot_infoPerAnt
            select e));

            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo();
        }
        #endregion

        #region ideEstabLot_ideADC

        List<XElement> lIdeEstabLot_infoPerAnt = new List<XElement>();
        public void add_ideEstabLot_infoPerAnt() {

            lIdeEstabLot_infoPerAnt.Add(

            new XElement(ns + "ideEstabLot",
            new XElement(ns + "tpInsc", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.tpInsc),
            new XElement(ns + "nrInsc", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.nrInsc),
            new XElement(ns + "codLotacao", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.codLotacao),

            // detVerbas 1.200
            from e in lDetVerbas_infoPerAnt
            select e,

            // infoAgNocivo 0.1
            opElement("infoAgNocivo", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoAgNocivo.grauExp,
            new XElement(ns + "grauExp", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoAgNocivo.grauExp)),

            // infoSimples 0.1
            opElement("infoSimples", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoSimples.indSimples,
            new XElement(ns + "indSimples", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoSimples.indSimples))));

            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoAgNocivo = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot.sInfoAgNocivo();
            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoSimples = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot.sInfoSimples();
        }
        #endregion

        #region detVerbas_ideADC

        List<XElement> lDetVerbas_infoPerAnt = new List<XElement>();
        public void add_detVerbas_infoPerAnt() {

            lDetVerbas_infoPerAnt.Add(

            new XElement(ns + "detVerbas",
            new XElement(ns + "codRubr", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.codRubr),
            new XElement(ns + "ideTabRubr", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.ideTabRubr),
            opTag("qtdRubr", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.qtdRubr),
            opTag("fatorRubr", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.fatorRubr),
            opTag("vrUnit", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.vrUnit),
            new XElement(ns + "vrRubr", infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.vrRubr)));

            infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas = new sInfoDeslig.sVerbasResc.sDmDev.sInfoPerAnt.sIdeADC.sIdePeriodo.sIdeEstabLot.sDetVerbas();
        }
        #endregion

        #region infoTrabInterm

        List<XElement> lInfoTrabInterm = new List<XElement>();
        public void add_infoTrabInterm() {

            lInfoTrabInterm.Add(

            new XElement(ns + "infoTrabInterm",
            new XElement(ns + "codConv", infoDeslig.verbasResc.dmDev.infoTrabInterm.codConv)));

            infoDeslig.verbasResc.dmDev.infoTrabInterm = new sInfoDeslig.sVerbasResc.sDmDev.sInfoTrabInterm();
        }
        #endregion

        #region procJudTrab

        List<XElement> lProcJudTrab = new List<XElement>();
        public void add_procJudTrab() {

            lProcJudTrab.Add(

            new XElement(ns + "procJudTrab",
            new XElement(ns + "tpTrib", infoDeslig.verbasResc.procJudTrab.tpTrib),
            new XElement(ns + "nrProcJud", infoDeslig.verbasResc.procJudTrab.nrProcJud),
            opTag("codSusp", infoDeslig.verbasResc.procJudTrab.codSusp)));

            infoDeslig.verbasResc.procJudTrab = new sInfoDeslig.sVerbasResc.sProcJudTrab();
        }
        #endregion

        #region remunOutrEmpr

        List<XElement> lRemunOutrEmpr = new List<XElement>();
        public void add_remunOutrEmpr() {

            lRemunOutrEmpr.Add(

            new XElement(ns + "remunOutrEmpr",
            new XElement(ns + "tpInsc", infoDeslig.verbasResc.infoMV.remunOutrEmpr.tpInsc),
            new XElement(ns + "nrInsc", infoDeslig.verbasResc.infoMV.remunOutrEmpr.nrInsc),
            new XElement(ns + "codCateg", infoDeslig.verbasResc.infoMV.remunOutrEmpr.codCateg),
            new XElement(ns + "vlrRemunOE", infoDeslig.verbasResc.infoMV.remunOutrEmpr.vlrRemunOE)));

            infoDeslig.verbasResc.infoMV.remunOutrEmpr = new sInfoDeslig.sVerbasResc.sInfoMV.sRemunOutrEmpr();
        }
        #endregion

        #region consigFGTS

        List<XElement> lConsigFGTS = new List<XElement>();
        public void add_consigFGTS() {

            lConsigFGTS.Add(

            new XElement(ns + "consigFGTS",
            new XElement(ns + "insConsig", infoDeslig.consigFGTS.insConsig),
            new XElement(ns + "nrContr", infoDeslig.consigFGTS.nrContr)));

            infoDeslig.consigFGTS = new sInfoDeslig.sConsigFGTS();
        }
      #endregion

      #endregion

      #region ***************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, indApuracao, nrRecibo, perApur, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeVinculo ideVinculo;
        public struct sIdeVinculo { public string cpfTrab, nisTrab, matricula; }

        public sInfoDeslig infoDeslig;
        public struct sInfoDeslig {
            public string mtvDeslig, indPagtoAPI, nrCertObito, nrProcTrab;
            public string dtDeslig, dtProjFimAPI;
            public string pensAlim, indCumprParc, qtdDiasInterm;
            public string percAliment, vrAlim;

            public sObservacoes observacoes;
            public struct sObservacoes { public string observacao; }

            public sSucessaoVinc sucessaoVinc;
            public struct sSucessaoVinc {
                public string tpInscSuc;
                public string cnpjSucessora;
            }

            public sTransfTit transfTit;
            public struct sTransfTit {
                public string cpfSubstituto;
                public string dtNascto;
            }

            public sMudancaCPF mudancaCPF;
            public struct sMudancaCPF
            {
               public string novoCPF;
            }

            public sVerbasResc verbasResc;
            public struct sVerbasResc {
                public sDmDev dmDev;
                public struct sDmDev {
                    public string ideDmDev;

                    public sInfoPerApur infoPerApur;
                    public struct sInfoPerApur {
                        public sIdeEstabLot ideEstabLot;
                        public struct sIdeEstabLot {
                            public string tpInsc;
                            public string nrInsc, codLotacao;

                            public sDetVerbas detVerbas;
                            public struct sDetVerbas {
                                public string qtdRubr, fatorRubr, vrUnit, vrRubr;
                                public string codRubr, ideTabRubr;
                            }
                            public sInfoSaudeColet infoSaudeColet;
                            public struct sInfoSaudeColet {
                                public sDetOper detOper;
                                public struct sDetOper {
                                    public string cnpjOper, regANS;
                                    public string vrPgTit;

                                    public sDetPlano detPlano;
                                    public struct sDetPlano {
                                        public string tpDep, cpfDep, nmDep;
                                        public string dtNascto;
                                        public string vlrPgDep;
                                    }
                                }
                            }
                            public sInfoAgNocivo infoAgNocivo;
                            public struct sInfoAgNocivo { public string grauExp; }

                            public sInfoSimples infoSimples;
                            public struct sInfoSimples { public string indSimples; }
                        }
                    }
                    public sInfoPerAnt infoPerAnt;
                    public struct sInfoPerAnt {
                        public sIdeADC ideADC;
                        public struct sIdeADC {
                            public string dtAcConv, dtEfAcConv;
                            public string tpAcConv, compAcConv, dsc;

                            public sIdePeriodo idePeriodo;
                            public struct sIdePeriodo {
                                public string perRef;

                                public sIdeEstabLot ideEstabLot;
                                public struct sIdeEstabLot {
                                    public string tpInsc;
                                    public string nrInsc, codLotacao;

                                    public sDetVerbas detVerbas;
                                    public struct sDetVerbas {
                                        public string codRubr, ideTabRubr;
                                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;
                                    }
                                    public sInfoAgNocivo infoAgNocivo;
                                    public struct sInfoAgNocivo { public string grauExp; }

                                    public sInfoSimples infoSimples;
                                    public struct sInfoSimples { public string indSimples; }
                                }
                            }
                        }
                    }
                    public sInfoTrabInterm infoTrabInterm;
                    public struct sInfoTrabInterm { public string codConv; }
                }
                public sProcJudTrab procJudTrab;
                public struct sProcJudTrab {
                    public string tpTrib, codSusp;
                    public string nrProcJud;
                }
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
                public sProcCS procCS;
                public struct sProcCS { public string nrProcJud; }
            }
            public sQuarentena quarentena;
            public struct sQuarentena { public string dtFimQuar; }

            public sConsigFGTS consigFGTS;
            public struct sConsigFGTS { public string insConsig, nrContr; }

        }
        #endregion
    }
}