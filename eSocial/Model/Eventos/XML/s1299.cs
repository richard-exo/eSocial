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

        public s1299(string sID) : base("evtFechaEvPer") {

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
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // ideRespInf 0.1
            xml.Elements().ElementAt(0).Add(
            opElement("ideRespInf", ideRespInf.nmResp,
            new XElement(ns + "nmResp", ideRespInf.nmResp),
            new XElement(ns + "cpfResp", ideRespInf.cpfResp),
            new XElement(ns + "telefone", ideRespInf.telefone),
            opTag("email", ideRespInf.email)));

            // infoFech
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "infoFech",
            new XElement(ns + "evtRemun", infoFech.evtRemun),
            new XElement(ns + "evtPgtos", infoFech.evtPgtos),
            new XElement(ns + "evtAqProd", infoFech.evtAqProd),
            new XElement(ns + "evtComProd", infoFech.evtComProd),
            new XElement(ns + "evtContratAvNP", infoFech.evtContratAvNP),
            new XElement(ns + "evtInfoComplPer", infoFech.evtInfoComplPer),
            opTag("compSemMovto", infoFech.compSemMovto)

            ));

            return x509.signXMLSHA256(xml, cert);
        }

        #region ************************************************************************************************************************************************* Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indApuracao, perApur, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sIdeRespInf ideRespInf;
        public struct sIdeRespInf { public string nmResp, cpfResp, telefone, email; }

        public sInfoFech infoFech;
        public struct sInfoFech {
            public string evtRemun, evtPgtos, evtAqProd, evtComProd, evtContratAvNP, evtInfoComplPer, compSemMovto;
        }

        #endregion
    }
}