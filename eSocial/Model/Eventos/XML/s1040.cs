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
    public class s1040 : bEvento_XML {

        public s1040(string sID) : base("evtTabFuncao", "infoFuncao") {

            id = sID;

            infoFuncao = new sInfoFuncao();
            infoFuncao.inclusao = new sInfoFuncao.sIncAlt();
            infoFuncao.inclusao.ideFuncao = new sInfoFuncao.sIncAlt.sIdeFuncao();
            infoFuncao.inclusao.dadosFuncao = new sInfoFuncao.sIncAlt.sDadosFuncao();

            infoFuncao.alteracao = new sInfoFuncao.sIncAlt();
            infoFuncao.alteracao.ideFuncao = new sInfoFuncao.sIncAlt.sIdeFuncao();
            infoFuncao.alteracao.dadosFuncao = new sInfoFuncao.sIncAlt.sDadosFuncao();

            infoFuncao.alteracao.novaValidade = new sIdePeriodo();

            infoFuncao.exclusao = new sInfoFuncao.sExclusao();
            infoFuncao.exclusao.ideFuncao = new sInfoFuncao.sIncAlt.sIdeFuncao();
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

            // infoFuncao
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1
            opElement("inclusao", infoFuncao.inclusao.ideFuncao.codFuncao,

            // ideFuncao
            new XElement(ns + "ideFuncao",
            new XElement(ns + "codFuncao", infoFuncao.inclusao.ideFuncao.codFuncao),
            new XElement(ns + "iniValid", infoFuncao.inclusao.ideFuncao.iniValid),
            opTag("fimValid", infoFuncao.inclusao.ideFuncao.fimValid)),

            // dadosFuncao
            new XElement(ns + "dadosFuncao",
            new XElement(ns + "dscFuncao", infoFuncao.inclusao.dadosFuncao.dscFuncao),
            new XElement(ns + "codCBO", infoFuncao.inclusao.dadosFuncao.codCBO))),

            // alteracao 0.1                  
            opElement("alteracao", infoFuncao.alteracao.ideFuncao.codFuncao,

            // ideFuncao
            new XElement(ns + "ideFuncao",
            new XElement(ns + "codFuncao", infoFuncao.alteracao.ideFuncao.codFuncao),
            new XElement(ns + "iniValid", infoFuncao.alteracao.ideFuncao.iniValid),
            opTag("fimValid", infoFuncao.alteracao.ideFuncao.fimValid)),

            // dadosFuncao
            new XElement(ns + "dadosFuncao",
            new XElement(ns + "dscFuncao", infoFuncao.alteracao.dadosFuncao.dscFuncao),
            new XElement(ns + "codCBO", infoFuncao.alteracao.dadosFuncao.codCBO)),

            // novaValidade 0.1
            opElement("novaValidade", infoFuncao.alteracao.novaValidade.iniValid,
            new XElement(ns + "iniValid", infoFuncao.alteracao.novaValidade.iniValid),
            opTag("fimValid", infoFuncao.alteracao.novaValidade.fimValid))

            ), // alteracao

            // exclusao 0.1
            opElement("exclusao", infoFuncao.exclusao.ideFuncao.codFuncao,

            new XElement(ns + "ideFuncao",

            new XElement(ns + "codFuncao", infoFuncao.exclusao.ideFuncao.codFuncao),
            new XElement(ns + "iniValid", infoFuncao.exclusao.ideFuncao.iniValid),
            opTag("fimValid", infoFuncao.exclusao.ideFuncao.fimValid))

            )); // exclusao

            return x509.signXMLSHA256(xml, cert);
        }

        #region ********************************************************************************************************************************************************* Structs

        public sInfoFuncao infoFuncao;
        public struct sInfoFuncao {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;
            public struct sIncAlt {

                public sIdeFuncao ideFuncao;
                public struct sIdeFuncao {
                    public string codFuncao, iniValid, fimValid;
                }
                public sDadosFuncao dadosFuncao;
                public struct sDadosFuncao {

                    public string dscFuncao, codCBO;
                }
                public sIdePeriodo novaValidade;
            }
            public struct sExclusao { public sIncAlt.sIdeFuncao ideFuncao; }
        }
        #endregion
    }
}