using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
    class s1070 : bEvento_XML {

        public s1070(string sID) : base("evtTabProcesso", "infoProcesso", "v_S_01_02_00") {

            id = sID;

            infoProcesso = new sInfoProcesso();

            infoProcesso.inclusao = new sInfoProcesso.sInclusao();
            infoProcesso.inclusao.ideProcesso = new sInfoProcesso.sInclusao.sIdeProcesso();
            infoProcesso.inclusao.dadosProc = new sInfoProcesso.sInclusao.sDadosProc();
            infoProcesso.inclusao.dadosProc.dadosProcJud = new sInfoProcesso.sInclusao.sDadosProc.sDadosProcJud();
            infoProcesso.inclusao.dadosProc.infoSusp = new sInfoProcesso.sInclusao.sDadosProc.sInfoSusp();

            infoProcesso.alteracao = new sInfoProcesso.sAlteracao();
            infoProcesso.alteracao.ideProcesso = new sInfoProcesso.sAlteracao.sIdeProcesso();
            infoProcesso.alteracao.dadosProc = new sInfoProcesso.sAlteracao.sDadosProc();
            infoProcesso.alteracao.dadosProc.dadosProcJud = new sInfoProcesso.sAlteracao.sDadosProc.sDadosProcJud();
            infoProcesso.alteracao.dadosProc.infoSusp = new sInfoProcesso.sAlteracao.sDadosProc.sInfoSusp();

            infoProcesso.alteracao.novaValidade = new sInfoProcesso.sAlteracao.sNovaValidade();

            infoProcesso.exclusao = new sInfoProcesso.sExclusao();
            infoProcesso.exclusao.ideProcesso = new sInfoProcesso.sExclusao.sIdeProcesso();
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

            // infoProcesso
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1
            opElement("inclusao", infoProcesso.inclusao.ideProcesso.tpProc,
            new XElement(ns + "ideProcesso",
            new XElement(ns + "tpProc", infoProcesso.inclusao.ideProcesso.tpProc),
            new XElement(ns + "nrProc", infoProcesso.inclusao.ideProcesso.nrProc),
            new XElement(ns + "iniValid", infoProcesso.inclusao.ideProcesso.iniValid),
            opTag("fimValid", infoProcesso.inclusao.ideProcesso.fimValid)),

            // dadosProc
            new XElement(ns + "dadosProc",
            opTag("indAutoria", infoProcesso.inclusao.dadosProc.indAutoria),
            new XElement(ns + "indMatProc", infoProcesso.inclusao.dadosProc.indMatProc),
            opTag("observacao", infoProcesso.inclusao.dadosProc.observacao),

            // dadosProcJud
            opElement("dadosProcJud", infoProcesso.inclusao.dadosProc.dadosProcJud.ufVara,
            new XElement(ns + "ufVara", infoProcesso.inclusao.dadosProc.dadosProcJud.ufVara),
            new XElement(ns + "codMunic", infoProcesso.inclusao.dadosProc.dadosProcJud.codMunic),
            new XElement(ns + "idVara", infoProcesso.inclusao.dadosProc.dadosProcJud.idVara)),

            // infoSusp 0.99
            from e in lInfoSusp_inclusao
            select e

            )), // inclusao

            // alteracao 0.1
            opElement("alteracao", infoProcesso.alteracao.ideProcesso.tpProc,
            new XElement(ns + "ideProcesso",
            new XElement(ns + "tpProc", infoProcesso.alteracao.ideProcesso.tpProc),
            new XElement(ns + "nrProc", infoProcesso.alteracao.ideProcesso.nrProc),
            new XElement(ns + "iniValid", infoProcesso.alteracao.ideProcesso.iniValid),
            opTag("fimValid", infoProcesso.alteracao.ideProcesso.fimValid)),

            // dadosProc
            new XElement(ns + "dadosProc",
            opTag("indAutoria", infoProcesso.alteracao.dadosProc.indAutoria),
            new XElement(ns + "indMatProc", infoProcesso.alteracao.dadosProc.indMatProc),
            opTag("observacao", infoProcesso.alteracao.dadosProc.observacao),

            // novaValidade 0.1
            opElement("novaValidade", infoProcesso.alteracao.novaValidade.iniValid,
            new XElement(ns + "iniValid", infoProcesso.alteracao.novaValidade.iniValid),
            opTag("fimValid", infoProcesso.alteracao.novaValidade.fimValid))

            )), // alteracao

            // exclusao 0.1
            opElement("exclusao", infoProcesso.exclusao.ideProcesso.tpProc,

            // ideProcesso
            new XElement(ns + "ideProcesso",
            new XElement(ns + "tpProc", infoProcesso.alteracao.ideProcesso.tpProc),
            new XElement(ns + "nrProc", infoProcesso.alteracao.ideProcesso.nrProc),
            new XElement(ns + "iniValid", infoProcesso.alteracao.ideProcesso.iniValid),
            opTag("fimValid", infoProcesso.alteracao.ideProcesso.fimValid)))

            );

            return x509.signXMLSHA256(xml, cert);
        }

        #region *************************************************************************************************************** Tags com +1 ocorrência

        #region infoSusp_inclusao

        List<XElement> lInfoSusp_inclusao = new List<XElement>();
        public void add_infoSusp_inclusao() {

            lInfoSusp_inclusao.Add(

            new XElement(ns + "infoSusp",
            new XElement(ns + "codSusp", infoProcesso.inclusao.dadosProc.infoSusp.codSusp),
            new XElement(ns + "indSusp", infoProcesso.inclusao.dadosProc.infoSusp.indSusp),
            new XElement(ns + "dtDecisao", infoProcesso.inclusao.dadosProc.infoSusp.dtDecisao),
            new XElement(ns + "indDeposito", infoProcesso.inclusao.dadosProc.infoSusp.indDeposito)));

            infoProcesso.inclusao.dadosProc.infoSusp = new sInfoProcesso.sInclusao.sDadosProc.sInfoSusp();
        }
        #endregion

        #region infoSusp_alteracao

        List<XElement> lInfoSusp_alteracao = new List<XElement>();
        public void add_infoSusp_alteracao() {

            lInfoSusp_alteracao.Add(

            new XElement(ns + "infoSusp",
            new XElement(ns + "codSusp", infoProcesso.inclusao.dadosProc.infoSusp.codSusp),
            new XElement(ns + "indSusp", infoProcesso.inclusao.dadosProc.infoSusp.indSusp),
            new XElement(ns + "dtDecisao", infoProcesso.inclusao.dadosProc.infoSusp.dtDecisao),
            new XElement(ns + "indDeposito", infoProcesso.inclusao.dadosProc.infoSusp.indDeposito)));

            infoProcesso.alteracao.dadosProc.infoSusp = new sInfoProcesso.sAlteracao.sDadosProc.sInfoSusp();
        }
        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public sInfoProcesso infoProcesso;
        public struct sInfoProcesso {

            public sInclusao inclusao;
            public struct sInclusao {

                public sIdeProcesso ideProcesso;
                public struct sIdeProcesso {
                    public string tpProc;
                    public string nrProc, iniValid, fimValid;
                }

                public sDadosProc dadosProc;
                public struct sDadosProc {
                    public string indAutoria, indMatProc;
                    public string observacao;

                    public sDadosProcJud dadosProcJud;
                    public struct sDadosProcJud {
                        public string codMunic, idVara;
                        public string ufVara;
                    }

                    public sInfoSusp infoSusp;
                    public struct sInfoSusp {
                        public string codSusp;
                        public string indSusp, indDeposito;
                        public string dtDecisao;
                    }
                }
            }

            public sAlteracao alteracao;
            public struct sAlteracao {

                public sIdeProcesso ideProcesso;
                public struct sIdeProcesso {
                    public string tpProc;
                    public string nrProc, iniValid, fimValid;
                }

                public sDadosProc dadosProc;
                public struct sDadosProc {
                    public string indAutoria, indMatProc;
                    public string observacao;

                    public sDadosProcJud dadosProcJud;
                    public struct sDadosProcJud {
                        public string codMunic, idVara;
                        public string ufVara;
                    }

                    public sInfoSusp infoSusp;
                    public struct sInfoSusp {
                        public string codSusp;
                        public string indSusp, indDeposito;
                        public string dtDecisao;
                    }
                }
                public sNovaValidade novaValidade;
                public struct sNovaValidade { public string iniValid, fimValid; }
            }

            public sExclusao exclusao;
            public struct sExclusao {

                public sIdeProcesso ideProcesso;
                public struct sIdeProcesso {
                    public string tpProc;
                    public string nrProc, iniValid, fimValid;
                }
            }
        }

        #endregion

    }
}
