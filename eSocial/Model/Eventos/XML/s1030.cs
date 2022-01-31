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
    public class s1030 : bEvento_XML {

        public s1030(string sID) : base("evtTabCargo", "infoCargo") {

            id = sID;

            infoCargo = new sInfoCargo();

            infoCargo.inclusao = new sInfoCargo.sIncAlt();
            infoCargo.inclusao.ideCargo = new sInfoCargo.sIdeCargo();
            infoCargo.inclusao.dadosCargo = new sInfoCargo.sDadosCargo();
            infoCargo.inclusao.dadosCargo.cargoPublico = new sInfoCargo.sDadosCargo.sCargoPublico();
            infoCargo.inclusao.dadosCargo.cargoPublico.leiCargo = new sInfoCargo.sDadosCargo.sCargoPublico.sLeiCargo();

            infoCargo.alteracao = new sInfoCargo.sIncAlt();
            infoCargo.alteracao.ideCargo = new sInfoCargo.sIdeCargo();
            infoCargo.alteracao.dadosCargo = new sInfoCargo.sDadosCargo();
            infoCargo.alteracao.dadosCargo.cargoPublico = new sInfoCargo.sDadosCargo.sCargoPublico();
            infoCargo.alteracao.dadosCargo.cargoPublico.leiCargo = new sInfoCargo.sDadosCargo.sCargoPublico.sLeiCargo();

            infoCargo.alteracao.novaValidade = new sIdePeriodo();

            infoCargo.exclusao = new sInfoCargo.sExclusao();
            infoCargo.exclusao.ideCargo = new sInfoCargo.sIdeCargo();

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

            // infoCargo
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1
            opElement("inclusao", infoCargo.inclusao.ideCargo.codCargo,

            // ideEstab
            new XElement(ns + "ideCargo",
            new XElement(ns + "codCargo", infoCargo.inclusao.ideCargo.codCargo),
            new XElement(ns + "iniValid", infoCargo.inclusao.ideCargo.iniValid),
            opTag("fimValid", infoCargo.inclusao.ideCargo.fimValid)),

            // dadosCargo
            new XElement(ns + "dadosCargo",
            new XElement(ns + "nmCargo", infoCargo.inclusao.dadosCargo.nmCargo),
            new XElement(ns + "codCBO", infoCargo.inclusao.dadosCargo.codCBO),

            // cargoPublico 0.1
            opElement("cargoPublico", infoCargo.inclusao.dadosCargo.cargoPublico.acumCargo,
            new XElement(ns + "acumCargo", infoCargo.inclusao.dadosCargo.cargoPublico.acumCargo),
            new XElement(ns + "contagemEsp", infoCargo.inclusao.dadosCargo.cargoPublico.contagemEsp),
            new XElement(ns + "dedicExcel", infoCargo.inclusao.dadosCargo.cargoPublico.dedicExcel),

            // leiCargo
            new XElement(ns + "leiCargo", infoCargo.inclusao.dadosCargo.cargoPublico.leiCargo.nrLei,
            new XElement(ns + "nrLei", infoCargo.inclusao.dadosCargo.cargoPublico.leiCargo.nrLei),
            new XElement(ns + "dtLei", infoCargo.inclusao.dadosCargo.cargoPublico.leiCargo.dtLei),
            new XElement(ns + "sitCargo", infoCargo.inclusao.dadosCargo.cargoPublico.leiCargo.sitCargo))))

            ), // inclusao

            // alteracao 0.1
            opElement("alteracao", infoCargo.alteracao.ideCargo.codCargo,

            // ideEstab
            new XElement(ns + "ideCargo",
            new XElement(ns + "codCargo", infoCargo.alteracao.ideCargo.codCargo),
            new XElement(ns + "iniValid", infoCargo.alteracao.ideCargo.iniValid),
            opTag("fimValid", infoCargo.alteracao.ideCargo.fimValid)),

            // dadosCargo
            new XElement(ns + "dadosCargo",
            new XElement(ns + "nmCargo", infoCargo.alteracao.dadosCargo.nmCargo),
            new XElement(ns + "codCBO", infoCargo.alteracao.dadosCargo.codCBO),

            // cargoPublico 0.1
            opElement("cargoPublico", infoCargo.alteracao.dadosCargo.cargoPublico.acumCargo,
            new XElement(ns + "acumCargo", infoCargo.alteracao.dadosCargo.cargoPublico.acumCargo),
            new XElement(ns + "contagemEsp", infoCargo.alteracao.dadosCargo.cargoPublico.contagemEsp),
            new XElement(ns + "dedicExcel", infoCargo.alteracao.dadosCargo.cargoPublico.dedicExcel),

            // leiCargo
            new XElement(ns + "leiCargo", infoCargo.alteracao.dadosCargo.cargoPublico.leiCargo.nrLei,
            new XElement(ns + "nrLei", infoCargo.alteracao.dadosCargo.cargoPublico.leiCargo.nrLei),
            new XElement(ns + "dtLei", infoCargo.alteracao.dadosCargo.cargoPublico.leiCargo.dtLei),
            new XElement(ns + "sitCargo", infoCargo.alteracao.dadosCargo.cargoPublico.leiCargo.sitCargo))))

            ), // alteracao

            // exclusao
            opElement("exclusao", infoCargo.exclusao.ideCargo.codCargo,

            // ideCargo
            new XElement(ns + "ideCargo",
            new XElement(ns + "tpInsc", infoCargo.exclusao.ideCargo.codCargo),
            new XElement(ns + "iniValid", infoCargo.exclusao.ideCargo.iniValid),
            opTag("fimValid", infoCargo.exclusao.ideCargo.fimValid)))

            );

            return x509.signXMLSHA256(xml, cert);
        }

        #region ********************************************************************************************************************************************************* Structs

        public sInfoCargo infoCargo;
        public struct sInfoCargo {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;

            public struct sIncAlt {
                public sIdePeriodo novaValidade;
                public sIdeCargo ideCargo;
                public sDadosCargo dadosCargo;
            }

            public struct sIdeCargo { public string codCargo, iniValid, fimValid; }

            public struct sDadosCargo {

                public string nmCargo, codCBO;
                public sCargoPublico cargoPublico;

                public struct sCargoPublico {

                    public string acumCargo, contagemEsp, dedicExcel;
                    public sLeiCargo leiCargo;

                    public struct sLeiCargo { public string nrLei, dtLei, sitCargo; }
                }
            }
            public struct sExclusao { public sIdeCargo ideCargo; }
        }
    }

    #endregion
}