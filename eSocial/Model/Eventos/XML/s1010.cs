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
    public class s1010 : bEvento_XML {

        public s1010(string sID) : base("evtTabRubrica", "infoRubrica", "v_S_01_02_00") {

            id = sID;

            infoRubrica = new sInfoRubrica();

            infoRubrica.inclusao = new sInfoRubrica.sIncAlt();
            infoRubrica.inclusao.ideRubrica = new sInfoRubrica.sIncAlt.sIdeRubrica();

            infoRubrica.inclusao.dadosRubrica = new sInfoRubrica.sIncAlt.sDadosRubrica();
            infoRubrica.inclusao.dadosRubrica.ideProcessoCP = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcessoCP();

            infoRubrica.inclusao.dadosRubrica.ideProcessoIRRF = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
            infoRubrica.inclusao.dadosRubrica.ideProcessoFGTS = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
            infoRubrica.inclusao.dadosRubrica.ideProcessoSIND = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();

            infoRubrica.alteracao = new sInfoRubrica.sIncAlt();
            infoRubrica.alteracao.ideRubrica = new sInfoRubrica.sIncAlt.sIdeRubrica();

            infoRubrica.alteracao.dadosRubrica = new sInfoRubrica.sIncAlt.sDadosRubrica();
            infoRubrica.alteracao.dadosRubrica.ideProcessoCP = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcessoCP();

            infoRubrica.alteracao.dadosRubrica.ideProcessoIRRF = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
            infoRubrica.alteracao.dadosRubrica.ideProcessoFGTS = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
            infoRubrica.alteracao.dadosRubrica.ideProcessoSIND = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();

            infoRubrica.alteracao.novaValidade = new sIdePeriodo();

            infoRubrica.exclusao = new sInfoRubrica.sExclusao();
            infoRubrica.exclusao.ideRubrica = new sInfoRubrica.sIncAlt.sIdeRubrica();

        }
        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // infoRubrica
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

               // inclusao 0.1                  
               opElement("inclusao", infoRubrica.inclusao.ideRubrica.codRubr,

                  // ideRubrica
                  new XElement(ns + "ideRubrica",
                  new XElement(ns + "codRubr", infoRubrica.inclusao.ideRubrica.codRubr),
                  new XElement(ns + "ideTabRubr", infoRubrica.inclusao.ideRubrica.ideTabRubr),
                  new XElement(ns + "iniValid", infoRubrica.inclusao.ideRubrica.iniValid),
                  opTag("fimValid", infoRubrica.inclusao.ideRubrica.fimValid)),

                  // dadosRubrica
                  new XElement(ns + "dadosRubrica",
                  new XElement(ns + "dscRubr", infoRubrica.inclusao.dadosRubrica.dscRubr),
                  new XElement(ns + "natRubr", infoRubrica.inclusao.dadosRubrica.natRubr),
                  new XElement(ns + "tpRubr", infoRubrica.inclusao.dadosRubrica.tpRubr),
                  new XElement(ns + "codIncCP", infoRubrica.inclusao.dadosRubrica.codIncCP),
                  new XElement(ns + "codIncIRRF", infoRubrica.inclusao.dadosRubrica.codIncIRRF),
                  new XElement(ns + "codIncFGTS", infoRubrica.inclusao.dadosRubrica.codIncFGTS),
                  opTag("codIncCPRP", infoRubrica.inclusao.dadosRubrica.codIncCPRP),
                  opTag("tetoRemun", infoRubrica.inclusao.dadosRubrica.tetoRemun),
                  opTag("observacao", infoRubrica.inclusao.dadosRubrica.observacao),

                  // ideProcessoCP 0.99
                  from e in lIdeProcessoCP_inclusao
                  select e
                  ),

                  // ideProcessoIRRF 0.99
                  from e in lIdeProcessoIRRF_inclusao
                  select e,

                  // ideProcessoFGTS 0.99
                  from e in lIdeProcessoFGTS_inclusao
                  select e

                  ), // inclusão

               // alteracao 0.1                  
               opElement("alteracao", infoRubrica.alteracao.ideRubrica.codRubr,

                  // ideRubrica
                  new XElement(ns + "ideRubrica",
                  new XElement(ns + "codRubr", infoRubrica.alteracao.ideRubrica.codRubr),
                  new XElement(ns + "ideTabRubr", infoRubrica.alteracao.ideRubrica.ideTabRubr),
                  new XElement(ns + "iniValid", infoRubrica.alteracao.ideRubrica.iniValid),
                  opTag("fimValid", infoRubrica.alteracao.ideRubrica.fimValid)),

                  // dadosRubrica
                  new XElement(ns + "dadosRubrica",
                  new XElement(ns + "dscRubr", infoRubrica.alteracao.dadosRubrica.dscRubr),
                  new XElement(ns + "natRubr", infoRubrica.alteracao.dadosRubrica.natRubr),
                  new XElement(ns + "tpRubr", infoRubrica.alteracao.dadosRubrica.tpRubr),
                  new XElement(ns + "codIncCP", infoRubrica.alteracao.dadosRubrica.codIncCP),
                  new XElement(ns + "codIncIRRF", infoRubrica.alteracao.dadosRubrica.codIncIRRF),
                  new XElement(ns + "codIncFGTS", infoRubrica.alteracao.dadosRubrica.codIncFGTS),
                  opTag("codIncCPRP", infoRubrica.alteracao.dadosRubrica.codIncCPRP),
                  opTag("tetoRemun", infoRubrica.alteracao.dadosRubrica.tetoRemun),
                  opTag("observacao", infoRubrica.alteracao.dadosRubrica.observacao)),

                  // novaValidade 0.1
                  opElement("novaValidade", infoRubrica.alteracao.novaValidade.iniValid,
                  new XElement(ns + "iniValid", infoRubrica.alteracao.novaValidade.iniValid),
                  opTag("fimValid", infoRubrica.alteracao.novaValidade.fimValid))

                  ), // alteracao

               // exclusao 0.1
               opElement("exclusao", infoRubrica.exclusao.ideRubrica.codRubr,

               new XElement(ns + "ideRubrica",

               new XElement(ns + "codRubr", infoRubrica.exclusao.ideRubrica.codRubr),
               new XElement(ns + "ideTabRubr", infoRubrica.exclusao.ideRubrica.ideTabRubr),
               new XElement(ns + "iniValid", infoRubrica.exclusao.ideRubrica.iniValid),
               opTag("fimValid", infoRubrica.exclusao.ideRubrica.fimValid)))

               ); // exclusao

            return x509.signXMLSHA256(xml, cert);
        }

        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

        #region ideProcessoCP_inclusao   

        List<XElement> lIdeProcessoCP_inclusao = new List<XElement>();
        public void add_ideProcessoCP_inclusao() {

            lIdeProcessoCP_inclusao.Add(
            new XElement(ns + "ideProcessoCP",
            new XElement(ns + "tpProc", infoRubrica.inclusao.dadosRubrica.ideProcessoCP.tpProc),
            new XElement(ns + "nrProc", infoRubrica.inclusao.dadosRubrica.ideProcessoCP.nrProc),
            new XElement(ns + "extDecisao", infoRubrica.inclusao.dadosRubrica.ideProcessoCP.extDecisao),
            new XElement(ns + "codSusp", infoRubrica.inclusao.dadosRubrica.ideProcessoCP.codSusp)));

            infoRubrica.inclusao.dadosRubrica.ideProcessoCP = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcessoCP();
        }
        #endregion

        #region ideProcessoIRRF_inclusao   

        List<XElement> lIdeProcessoIRRF_inclusao = new List<XElement>();
        public void add_ideProcessoIRRF_inclusao() {

            lIdeProcessoIRRF_inclusao.Add(
            new XElement(ns + "ideProcessoIRRF",
            new XElement(ns + "nrProc", infoRubrica.inclusao.dadosRubrica.ideProcessoIRRF.nrProc),
            new XElement(ns + "codSusp", infoRubrica.inclusao.dadosRubrica.ideProcessoIRRF.codSusp)));

            infoRubrica.inclusao.dadosRubrica.ideProcessoIRRF = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #region ideProcessoFGTS_inclusao   

        List<XElement> lIdeProcessoFGTS_inclusao = new List<XElement>();
        public void add_ideProcessoFGTS_inclusao() {

            lIdeProcessoFGTS_inclusao.Add(
            new XElement(ns + "ideProcessoFGTS",
            new XElement(ns + "nrProc", infoRubrica.inclusao.dadosRubrica.ideProcessoFGTS.nrProc)));

            infoRubrica.inclusao.dadosRubrica.ideProcessoFGTS = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #region ideProcessoSIND_inclusao   

        List<XElement> lIdeProcessoSIND_inclusao = new List<XElement>();
        public void add_ideProcessoSIND_inclusao() {

            lIdeProcessoSIND_inclusao.Add(
            new XElement(ns + "ideProcessoSIND",
            new XElement(ns + "nrProc", infoRubrica.inclusao.dadosRubrica.ideProcessoSIND.nrProc)));

            infoRubrica.inclusao.dadosRubrica.ideProcessoSIND = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #region ideProcessoCP_alteracao 

        List<XElement> lIdeProcessoCP_alteracao = new List<XElement>();
        public void add_ideProcessoCP_alteracao() {

            lIdeProcessoCP_alteracao.Add(
            new XElement(ns + "ideProcessoCP",
            new XElement(ns + "tpProc", infoRubrica.alteracao.dadosRubrica.ideProcessoCP.tpProc),
            new XElement(ns + "nrProc", infoRubrica.alteracao.dadosRubrica.ideProcessoCP.nrProc),
            new XElement(ns + "extDecisao", infoRubrica.alteracao.dadosRubrica.ideProcessoCP.extDecisao),
            new XElement(ns + "codSusp", infoRubrica.alteracao.dadosRubrica.ideProcessoCP.codSusp)));

            infoRubrica.alteracao.dadosRubrica.ideProcessoCP = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcessoCP();
        }
        #endregion

        #region ideProcessoIRRF_alteracao 

        List<XElement> lIdeProcessoIRRF_alteracao = new List<XElement>();
        public void add_ideProcessoIRRF_alteracao() {

            lIdeProcessoIRRF_alteracao.Add(
            new XElement(ns + "ideProcessoIRRF",
            new XElement(ns + "nrProc", infoRubrica.alteracao.dadosRubrica.ideProcessoIRRF.nrProc),
            new XElement(ns + "codSusp", infoRubrica.alteracao.dadosRubrica.ideProcessoIRRF.codSusp)));

            infoRubrica.alteracao.dadosRubrica.ideProcessoIRRF = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #region ideProcessoFGTS_alteracao 

        List<XElement> lIdeProcessoFGTS_alteracao = new List<XElement>();
        public void add_ideProcessoFGTS_alteracao() {

            lIdeProcessoFGTS_alteracao.Add(
            new XElement(ns + "ideProcessoFGTS",
            new XElement(ns + "nrProc", infoRubrica.alteracao.dadosRubrica.ideProcessoFGTS.nrProc)));

            infoRubrica.alteracao.dadosRubrica.ideProcessoFGTS = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #region ideProcessoSIND_alteracao 

        List<XElement> lIdeProcessoSIND_alteracao = new List<XElement>();
        public void add_ideProcessoSIND_alteracao() {

            lIdeProcessoSIND_alteracao.Add(
            new XElement(ns + "ideProcessoSIND",
            new XElement(ns + "nrProc", infoRubrica.alteracao.dadosRubrica.ideProcessoSIND.nrProc)));

            infoRubrica.alteracao.dadosRubrica.ideProcessoSIND = new sInfoRubrica.sIncAlt.sDadosRubrica.sIdeProcesso();
        }
        #endregion

        #endregion

        #region ********************************************************************************************************************************************************* Structs

        public sInfoRubrica infoRubrica;
        public struct sInfoRubrica {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;
            public struct sIncAlt {

                public sIdeRubrica ideRubrica;
                public struct sIdeRubrica { public string codRubr, ideTabRubr, iniValid, fimValid; }

                public sDadosRubrica dadosRubrica;
                public struct sDadosRubrica {
                    public string dscRubr, codIncCP, codIncIRRF, codIncFGTS, codIncCPRP, tetoRemun, observacao;
                    public string natRubr, tpRubr;

                    public sIdeProcessoCP ideProcessoCP;
                    public struct sIdeProcessoCP {
                        public string tpProc, extDecisao, codSusp;
                        public string nrProc;
                    }
                    public sIdeProcesso ideProcessoIRRF, ideProcessoFGTS, ideProcessoSIND;
                    public struct sIdeProcesso {
                        public string nrProc;
                        public string codSusp;
                    }
                }
                public sIdePeriodo novaValidade;
            }
            public struct sExclusao { public sIncAlt.sIdeRubrica ideRubrica; }
        }
        #endregion
    }
}