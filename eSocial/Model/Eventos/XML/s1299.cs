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
    public class s1299 : bEvento_XML {

        public s1299(string sID) : base("evtFechaEvPer", "", "v_S_01_02_00") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideRespInf = new sIdeRespInf();
            infoFech = new sInfoFech();
        }

        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
            new XElement(ns + "indApuracao", ideEvento.indApuracao),
            new XElement(ns + "perApur", ideEvento.perApur),
            opTag("indGuia", ideEvento.indGuia),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // infoFech
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "infoFech",
            new XElement(ns + "evtRemun", infoFech.evtRemun),
            new XElement(ns + "evtPgtos", infoFech.evtPgtos),
            new XElement(ns + "evtComProd", infoFech.evtComProd),
            new XElement(ns + "evtContratAvNP", infoFech.evtContratAvNP),
            new XElement(ns + "evtInfoComplPer", infoFech.evtInfoComplPer),
            opTag("indExcApur1250", infoFech.indExcApur1250),
            opTag("transDCTFWeb", infoFech.transDCTFWeb),
            opTag("naoValid", infoFech.naoValid)

            ));

            return x509.signXMLSHA256(xml, cert);
        }

        #region ************************************************************************************************************************************************* Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indApuracao, perApur, indGuia, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sIdeRespInf ideRespInf;
        public struct sIdeRespInf { public string nmResp, cpfResp, telefone, email; }

        public sInfoFech infoFech;
        public struct sInfoFech {
            public string evtRemun, evtPgtos, evtAqProd, evtComProd, evtContratAvNP, evtInfoComplPer, compSemMovto, indExcApur1250, transDCTFWeb, naoValid;
        }

        #endregion
    }
}