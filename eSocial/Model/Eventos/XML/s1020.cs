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
    public class s1020 : bEvento_XML {

        public s1020(string sID) : base("evtTabLotacao", "infoLotacao", "v_S_01_03_00") {

            id = sID;

            infoLotacao = new sInfoLotacao();
            infoLotacao.inclusao = new sInfoLotacao.sIncAlt();

            infoLotacao.inclusao.ideLotacao = new sInfoLotacao.sIncAlt.sIdeLotacao();

            infoLotacao.inclusao.dadosLotacao = new sInfoLotacao.sIncAlt.sDadosLotacao();
            infoLotacao.inclusao.dadosLotacao.fpasLotacao = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao();
            infoLotacao.inclusao.dadosLotacao.fpasLotacao.infoProcJudTerceiros = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao.sInfoProcJudTerceiros();


            infoLotacao.alteracao.ideLotacao = new sInfoLotacao.sIncAlt.sIdeLotacao();

            infoLotacao.alteracao.dadosLotacao = new sInfoLotacao.sIncAlt.sDadosLotacao();
            infoLotacao.alteracao.dadosLotacao.fpasLotacao = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao();
            infoLotacao.alteracao.dadosLotacao.fpasLotacao.infoProcJudTerceiros = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao.sInfoProcJudTerceiros();

            infoLotacao.alteracao.novaValidade = new sIdePeriodo();

            infoLotacao.exclusao = new sInfoLotacao.sExclusao();
            infoLotacao.exclusao.ideEstab = new sInfoLotacao.sIncAlt.sIdeLotacao();

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

            // infoLotacao
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1
            opElement("inclusao", infoLotacao.inclusao.ideLotacao.codLotacao,

            // ideLotacao
            new XElement(ns + "ideLotacao",
            new XElement(ns + "codLotacao", infoLotacao.inclusao.ideLotacao.codLotacao),
            new XElement(ns + "iniValid", infoLotacao.inclusao.ideLotacao.iniValid),
            opTag("fimValid", infoLotacao.inclusao.ideLotacao.fimValid)),

            // dadosLotacao
            new XElement(ns + "dadosLotacao",
            new XElement(ns + "tpLotacao", infoLotacao.inclusao.dadosLotacao.tpLotacao),
            opTag("tpInsc", infoLotacao.inclusao.dadosLotacao.tpInsc),
            opTag("nrInsc", infoLotacao.inclusao.dadosLotacao.nrInsc),

            // fpasLotacao
            new XElement(ns + "fpasLotacao",
            new XElement(ns + "fpas", infoLotacao.inclusao.dadosLotacao.fpasLotacao.fPas),
            new XElement(ns + "codTercs", infoLotacao.inclusao.dadosLotacao.fpasLotacao.codTercs),
            opTag("codTercsSusp", infoLotacao.inclusao.dadosLotacao.fpasLotacao.codTercsSusp),

             // procJudTerceiro_inclusao 1.99
             from e in lProcJudTerceiro_inclusao
             select e),

            // infoEmprParcial 0.1
            opElement("infoEmprParcial", infoLotacao.inclusao.dadosLotacao.infoEmprParcial.tpInscContrat,
            new XElement(ns + "tpInscContrat", infoLotacao.inclusao.dadosLotacao.infoEmprParcial.tpInscContrat),
            new XElement(ns + "nrInscContrat", infoLotacao.inclusao.dadosLotacao.infoEmprParcial.nrInscContrat),
            new XElement(ns + "tpInscProp", infoLotacao.inclusao.dadosLotacao.infoEmprParcial.tpInscProp),
            new XElement(ns + "nrInscProp", infoLotacao.inclusao.dadosLotacao.infoEmprParcial.nrInscProp)),

            opElement("dadosOpPort", infoLotacao.inclusao.dadosLotacao.dadosOpPort.aliqRat,
            new XElement(ns + "aliqRat", infoLotacao.inclusao.dadosLotacao.dadosOpPort.aliqRat),
            new XElement(ns + "fap", infoLotacao.inclusao.dadosLotacao.dadosOpPort.fap))

            )), // alteracao

            // alteracao 0.1
            opElement("alteracao", infoLotacao.alteracao.ideLotacao.codLotacao,

            // ideLotacao
            new XElement(ns + "ideLotacao",
            new XElement(ns + "codLotacao", infoLotacao.alteracao.ideLotacao.codLotacao),
            new XElement(ns + "iniValid", infoLotacao.alteracao.ideLotacao.iniValid),
            opTag("fimValid", infoLotacao.alteracao.ideLotacao.fimValid)),

            // dadosLotacao
            new XElement(ns + "dadosLotacao",
            new XElement(ns + "tpLotacao", infoLotacao.alteracao.dadosLotacao.tpLotacao),
            opTag("tpInsc", infoLotacao.alteracao.dadosLotacao.tpInsc),
            opTag("nrInsc", infoLotacao.alteracao.dadosLotacao.nrInsc)),

            // novaValidade 0.1
            opElement("novaValidade", infoLotacao.alteracao.novaValidade.iniValid,
            new XElement(ns + "iniValid", infoLotacao.alteracao.novaValidade.iniValid),
            opTag("fimValid", infoLotacao.alteracao.novaValidade.fimValid))

            ), // alteracao

            // exclusao 0.1
            opElement("exclusao", infoLotacao.exclusao.ideEstab.codLotacao,

            // ideLotacao
            new XElement(ns + "ideLotacao",

            new XElement(ns + "codLotacao", infoLotacao.exclusao.ideEstab.codLotacao),
            new XElement(ns + "iniValid", infoLotacao.exclusao.ideEstab.iniValid),
            opTag("fimValid", infoLotacao.exclusao.ideEstab.fimValid)))

            ); // exclusao

            return x509.signXMLSHA256(xml, cert);
        }

        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

        #region procJudTerceiro_inclusao   

        List<XElement> lProcJudTerceiro_inclusao = new List<XElement>();
        public void add_procJudTerceiro_inclusao() {

            lProcJudTerceiro_inclusao.Add(
            new XElement(ns + "infoProcJudTerceiros",
            new XElement(ns + "procJudTerceiro",
            new XElement(ns + "codTerc", infoLotacao.inclusao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.codTerc),
            new XElement(ns + "nrProcJud", infoLotacao.inclusao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.nrProcJud),
            new XElement(ns + "codSusp", infoLotacao.inclusao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.codSusp))));

            infoLotacao.inclusao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao.sInfoProcJudTerceiros.sProcJudTerceiro();
        }
        #endregion

        #region procJudTerceiro_alteracao

        List<XElement> lProcJudTerceiro_alteracao = new List<XElement>();
        public void add_procJudTerceiro_alteracao() {

            lProcJudTerceiro_alteracao.Add(
            new XElement(ns + "infoProcJudTerceiros",
            new XElement(ns + "procJudTerceiro",
            new XElement(ns + "codTerc", infoLotacao.alteracao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.codTerc),
            new XElement(ns + "nrProcJud", infoLotacao.alteracao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.nrProcJud),
            new XElement(ns + "codSusp", infoLotacao.alteracao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro.codSusp))));

            infoLotacao.alteracao.dadosLotacao.fpasLotacao.infoProcJudTerceiros.procJudTerceiro = new sInfoLotacao.sIncAlt.sDadosLotacao.sFpasLotacao.sInfoProcJudTerceiros.sProcJudTerceiro();
        }
        #endregion

        #endregion

        #region ********************************************************************************************************************************************************* Structs

        public sInfoLotacao infoLotacao;
        public struct sInfoLotacao {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;
            public struct sIncAlt {

                public sIdeLotacao ideLotacao;
                public struct sIdeLotacao {
                    public string codLotacao, iniValid, fimValid;
                }

                public sDadosLotacao dadosLotacao;
                public struct sDadosLotacao {

                    public string tpLotacao, tpInsc, nrInsc;

                    public sFpasLotacao fpasLotacao;
                    public struct sFpasLotacao {

                        public string fPas, codTercs, codTercsSusp;

                        public sInfoProcJudTerceiros infoProcJudTerceiros;
                        public struct sInfoProcJudTerceiros {
                            public sProcJudTerceiro procJudTerceiro;
                            public struct sProcJudTerceiro { public string codTerc, nrProcJud, codSusp; }
                        }
                    }
                    public sInfoEmprParcial infoEmprParcial;
                    public struct sInfoEmprParcial { public string tpInscContrat, nrInscContrat, tpInscProp, nrInscProp; }

                    public sDadosOpPort dadosOpPort;
                    public struct sDadosOpPort { public string aliqRat, fap; }    
                }
                public sIdePeriodo novaValidade;
            }
            public struct sExclusao { public sIncAlt.sIdeLotacao ideEstab; }
        }
        #endregion
    }
}