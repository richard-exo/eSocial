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
    public class s2230 : bEvento_XML {

        public s2230(string sID) : base("evtAfastTemp", "infoAfastamento", "v_S_01_02_00") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideVinculo = new sIdeVinculo();

            infoAfastamento = new sInfoAfastamento();
            infoAfastamento.iniAfastamento = new sInfoAfastamento.sIniAfastamento();

            infoAfastamento.iniAfastamento.infoAtestado = new sInfoAfastamento.sIniAfastamento.sInfoAtestado();
            infoAfastamento.iniAfastamento.infoAtestado.emitente = new sInfoAfastamento.sIniAfastamento.sInfoAtestado.sEmitente();
            infoAfastamento.iniAfastamento.perAquis = new sInfoAfastamento.sIniAfastamento.sPerAquis();
            infoAfastamento.iniAfastamento.infoCessao = new sInfoAfastamento.sIniAfastamento.sInfoCessao();
            infoAfastamento.iniAfastamento.infoMandSind = new sInfoAfastamento.sIniAfastamento.sInfoMandSind();
            infoAfastamento.iniAfastamento.infoMandElet = new sInfoAfastamento.sIniAfastamento.sInfoMandElet();

            infoAfastamento.infoRetif = new sInfoAfastamento.sInfoRetif();
            infoAfastamento.fimAfastamento = new sInfoAfastamento.sFimAfastamento();

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
            opTag("matricula", ideVinculo.matricula),
            opTag("codCateg", ideVinculo.codCateg)));

            // infoAfastamento
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // iniAfastamento 0.1
            opElement("iniAfastamento", infoAfastamento.iniAfastamento.dtIniAfast,
            new XElement(ns + "dtIniAfast", infoAfastamento.iniAfastamento.dtIniAfast),
            new XElement(ns + "codMotAfast", infoAfastamento.iniAfastamento.codMotAfast),
            opTag("infoMesmoMtv", infoAfastamento.iniAfastamento.infoMesmoMtv),
            opTag("tpAcidTransito", infoAfastamento.iniAfastamento.tpAcidTransito),
            opTag("observacao", infoAfastamento.iniAfastamento.observacao),

            //// infoAtestado 0.9
            //from e in lInfoAtestado
            //select e,

            // perAquis 0.1
            opElement("perAquis", infoAfastamento.iniAfastamento.perAquis.dtInicio,
            new XElement(ns + "dtInicio", infoAfastamento.iniAfastamento.perAquis.dtInicio),
            opTag("dtFim", infoAfastamento.iniAfastamento.perAquis.dtFim)),
  
            // infoCessao 0.1
            opElement("infoCessao", infoAfastamento.iniAfastamento.infoCessao.cnpjCess,
            new XElement(ns + "cnpjCess", infoAfastamento.iniAfastamento.infoCessao.cnpjCess),
            new XElement(ns + "infOnus", infoAfastamento.iniAfastamento.infoCessao.infOnus)),

            // infoMandSind 0.1
            opElement("infoMandSind", infoAfastamento.iniAfastamento.infoMandSind.cnpjSind,
            new XElement(ns + "cnpjSind", infoAfastamento.iniAfastamento.infoMandSind.cnpjSind),
            new XElement(ns + "infOnusRemun", infoAfastamento.iniAfastamento.infoMandSind.infOnusRemun)),

            // infoMandElet 0.1
            opElement("infoMandElet", infoAfastamento.iniAfastamento.infoMandElet.cnpjMandElet,
            new XElement(ns + "cnpjMandElet", infoAfastamento.iniAfastamento.infoMandElet.cnpjMandElet),
            opTag("indRemunCargo", infoAfastamento.iniAfastamento.infoMandElet.indRemunCargo))

            ), // iniAfastamento

            // infoRetif 0.1
            opElement("infoRetif", infoAfastamento.infoRetif.origRetif,
            new XElement(ns + "origRetif", infoAfastamento.infoRetif.origRetif),
            opTag("tpProc", infoAfastamento.infoRetif.tpProc),
            opTag("nrProc", infoAfastamento.infoRetif.nrProc)),

            // fimAfastamento 0.1
            opElement("fimAfastamento", infoAfastamento.fimAfastamento.dtTermAfast,
            new XElement(ns + "dtTermAfast", infoAfastamento.fimAfastamento.dtTermAfast)));

            return x509.signXMLSHA256(xml, cert);
        }

        #region *************************************************************************************************************** Tags com +1 ocorrência

        #region infoAtestado   

        List<XElement> lInfoAtestado = new List<XElement>();
        public void add_infoAtestado() {

            lInfoAtestado.Add(
            new XElement(ns + "infoAtestado",
            opTag("codCID", infoAfastamento.iniAfastamento.infoAtestado.codCID),
            new XElement(ns + "qtdDiasAfast", infoAfastamento.iniAfastamento.infoAtestado.qtdDiasAfast),

            // emitente
            opElement("emitente", infoAfastamento.iniAfastamento.infoAtestado.emitente.nmEmit,
            new XElement(ns + "nmEmit", infoAfastamento.iniAfastamento.infoAtestado.emitente.nmEmit),
            new XElement(ns + "ideOC", infoAfastamento.iniAfastamento.infoAtestado.emitente.ideOC),
            new XElement(ns + "nrOc", infoAfastamento.iniAfastamento.infoAtestado.emitente.nrOc),
            opTag("ufOC", infoAfastamento.iniAfastamento.infoAtestado.emitente.ufOC))));

            infoAfastamento.iniAfastamento.infoAtestado = new sInfoAfastamento.sIniAfastamento.sInfoAtestado();
            infoAfastamento.iniAfastamento.infoAtestado.emitente = new sInfoAfastamento.sIniAfastamento.sInfoAtestado.sEmitente();
        }
        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, nrRecibo, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sIdeVinculo ideVinculo;
        public struct sIdeVinculo {
            public string cpfTrab, nisTrab, matricula;
            public string codCateg;
        }

        public sInfoAfastamento infoAfastamento;
        public struct sInfoAfastamento {

            public sIniAfastamento iniAfastamento;
            public struct sIniAfastamento {
                public string dtIniAfast;
                public string codMotAfast, infoMesmoMtv, observacao;
                public string tpAcidTransito;

                public sInfoAtestado infoAtestado;
                public struct sInfoAtestado {
                    public string codCID;
                    public string qtdDiasAfast;

                    public sEmitente emitente;
                    public struct sEmitente {
                        public string nmEmit, nrOc, ufOC;
                        public string ideOC;
                    }
                }
                public sPerAquis perAquis;
                public struct sPerAquis
                {
                    public string dtInicio, dtFim; 
                }
                public sInfoCessao infoCessao;
                public struct sInfoCessao {
                    public string cnpjCess;
                    public string infOnus;
                }
                public sInfoMandSind infoMandSind;
                public struct sInfoMandSind {
                    public string cnpjSind;
                    public string infOnusRemun;
                }
                public sInfoMandElet infoMandElet;
                public struct sInfoMandElet
                {
                    public string cnpjMandElet, indRemunCargo;
                }
            }
            public sFimAfastamento fimAfastamento;
            public struct sFimAfastamento { public string dtTermAfast; }

            public sInfoRetif infoRetif;
            public struct sInfoRetif {
                public string nrProc;
                public string origRetif, tpProc;
            }

        }
        #endregion
    }
}