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
    public class s2250 : bEvento_XML {

        public s2250(string sID) : base("evtAvPrevio", "infoAvPrevio") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideVinculo = new sIdeVinculo();

            infoAvPrevio = new sInfoAvPrevio();
            infoAvPrevio.detAvPrevio = new sInfoAvPrevio.sDetAvPrevio();
            infoAvPrevio.cancAvPrevio = new sInfoAvPrevio.sCancAvPrevio();
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

            // infoAvPrevio
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(
            //new XElement(ns + "infoAvPrevio",

            // detAvPrevio 0.1
            opElement("detAvPrevio", infoAvPrevio.detAvPrevio.dtAvPrv,
            new XElement(ns + "dtAvPrv", infoAvPrevio.detAvPrevio.dtAvPrv),
            new XElement(ns + "dtPrevDeslig", infoAvPrevio.detAvPrevio.dtPrevDeslig),
            new XElement(ns + "tpAvPrevio", infoAvPrevio.detAvPrevio.tpAvPrevio),
            opTag("observacao", infoAvPrevio.detAvPrevio.observacao)),

            // cancAvPrevio 0.1
            opElement("cancAvPrevio", infoAvPrevio.cancAvPrevio.dtCancAvPrv,
            new XElement(ns + "dtCancAvPrv", infoAvPrevio.cancAvPrevio.dtCancAvPrv),
            opTag("observacao", infoAvPrevio.cancAvPrevio.observacao),
            new XElement(ns + "mtvCancAvPrevio", infoAvPrevio.cancAvPrevio.mtvCancAvPrevio)));//)

            return x509.signXMLSHA256(xml, cert);
        }

      #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento
        {
           public string indRetif, nrRecibo, verProc;
           public enTpAmb tpAmb;
           public enProcEmi procEmi;
        }

        public sIdeVinculo ideVinculo;
        public struct sIdeVinculo {
            public string cpfTrab, nisTrab, matricula;
        }

        public sInfoAvPrevio infoAvPrevio;
        public struct sInfoAvPrevio {

            public sDetAvPrevio detAvPrevio;
            public struct sDetAvPrevio {
                public string dtAvPrv, dtPrevDeslig;
                public string tpAvPrevio;
                public string observacao;
            }

            public sCancAvPrevio cancAvPrevio;
            public struct sCancAvPrevio {
                public string dtCancAvPrv;
                public string mtvCancAvPrevio;
                public string observacao;
            }
        }
        #endregion
    }
}