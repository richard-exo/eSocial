using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
    class s1210 : bEvento_XML {

        public s1210(string sID) : base("evtPgtos", "", "v_S_01_01_00") {

            id = sID;

            ideEvento = new sIdeEvento();
            ideBenef = new sIdeBenef();
            ideBenef.deps = new sIdeBenef.sDeps();
            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();
        }

        public override XElement genSignedXML(X509Certificate2 cert) {

            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

            new XElement(ns + "indRetif", ideEvento.indRetif),
            opTag("nrRecibo", ideEvento.nrRecibo),
            new XElement(ns + "perApur", ideEvento.perApur),
            opTag("indGuia", ideEvento.indGuia),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // ideBenef
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "ideBenef",
            new XElement(ns + "cpfBenef", ideBenef.cpfBenef),

            // infoPgto 1.60
            from e in lInfoPgto
            select e));

            return x509.signXMLSHA256(xml, cert);
        }

        #region infoPgto

        List<XElement> lInfoPgto = new List<XElement>();
        public void add_infoPgto() {

            lInfoPgto.Add(

            new XElement(ns + "infoPgto",
            new XElement(ns + "dtPgto", ideBenef.infoPgto.dtPgto),
            new XElement(ns + "tpPgto", ideBenef.infoPgto.tpPgto),
            new XElement(ns + "perRef", ideBenef.infoPgto.perRef),
            new XElement(ns + "ideDmDev", ideBenef.infoPgto.ideDmDev),
            new XElement(ns + "vrLiq", ideBenef.infoPgto.vrLiq)));

            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();
        }

        #endregion
       
        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao, perApur, indGuia, verProc, nrRecibo;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sIdeBenef ideBenef;
        public struct sIdeBenef {
            public string cpfBenef;

            public sDeps deps;
            public struct sDeps { public string vrDedDep; }

            public sInfoPgto infoPgto;
            public struct sInfoPgto {
                public string dtPgto;
                public string tpPgto;
                public string perRef;
                public string ideDmDev;
                public string vrLiq;
            }
        }
        #endregion
    }
}
