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
   public class s2190 : bEvento_XML {

        public s2190(string sID) : base("evtAdmPrelim", "", "v_S_01_01_00")
        {
            id = sID;
            infoRegPrelim = new sInfoRegPrelim();
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

         // infoRegPrelim
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "infoRegPrelim",
         new XElement(ns + "cpfTrab", infoRegPrelim.cpfTrab),
         new XElement(ns + "dtNascto", infoRegPrelim.dtNascto),
         new XElement(ns + "dtAdm", infoRegPrelim.dtAdm),
         new XElement(ns + "matricula", infoRegPrelim.matricula),
         new XElement(ns + "codCateg", infoRegPrelim.codCateg),
         new XElement(ns + "natAtividade", infoRegPrelim.natAtividade)));

         return x509.signXMLSHA256(xml, cert);
      }

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento {
         public string verProc, indRetif, nrRecibo;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

      public sInfoRegPrelim infoRegPrelim;
      public struct sInfoRegPrelim {
         public string cpfTrab;
         public string dtNascto, dtAdm, matricula, codCateg, natAtividade;
      }

      #endregion
   }
}