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
    public class s1300 : bEvento_XML {

        public s1300(string sID) : base("evtContrSindPatr") {

            id = sID;

            ideEvento = new sIdeEvento();
            contribSind = new sContribSind();
        }

        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
            new XElement(ns + "indRetif", ideEvento.indRetif),
            opTag("nrRecibo", ideEvento.nrRecibo),
            new XElement(ns + "indApuracao", ideEvento.indApuracao),
            new XElement(ns + "perApur", ideEvento.perApur),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // contribSind 1.999
         //xml.Elements().ElementAt(0).Add(
         //from e in lContribSind
         //select e);

            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "contribSind",
            new XElement(ns + "cnpjSindic", contribSind.cnpjSindic),
            new XElement(ns + "tpContribSind", contribSind.tpContribSind),
            new XElement(ns + "vlrContribSind", contribSind.vlrContribSind)));

         return x509.signXMLSHA256(xml, cert);
        }

        #region ************************************************************************************************************** Tags com +1 ocorrência

        #region contribSind   

        List<XElement> lContribSind = new List<XElement>();
        public void add_contribSind() {

            lContribSind.Add(
            new XElement(ns + "contribSind",
            new XElement(ns + "cnpjSindic", contribSind.cnpjSindic),
            new XElement(ns + "tpContribSind", contribSind.tpContribSind),
            new XElement(ns + "vlrContribSind", contribSind.vlrContribSind)));

            contribSind = new sContribSind();
        }
        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao, nrRecibo, perApur, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sContribSind contribSind;
        public struct sContribSind {
            public string cnpjSindic;
            public string tpContribSind;
            public string vlrContribSind;
        }

    }
    #endregion
}