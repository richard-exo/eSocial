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
    public class s1298 : bEvento_XML {

        public s1298(string sID) : base("evtReabreEvPer","", "v_S_01_03_00") {

            id = sID;
            ideEvento = new sIdeEvento();
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

            return x509.signXMLSHA256(xml, cert);
        }

        #region ***************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indApuracao, perApur, indGuia, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }
        #endregion
    }
}