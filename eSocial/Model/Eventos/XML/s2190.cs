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
        //public s2190(string sID) : base("evtAdmPrelim", "infoRegPrelim") {

        //   id = sID;
        //   infoRegPrelim = new sInfoRegPrelim();
        //}

        public s2190(string sID) : base("evtAdmPrelim")
        {
            id = sID;
            infoRegPrelim = new sInfoRegPrelim();
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

         // infoRegPrelim
         //xml.Elements().ElementAt(0).Element(ns + tagEvento).ReplaceNodes(
         //new XElement(ns + "cpfTrab", infoRegPrelim.cpfTrab),
         //new XElement(ns + "dtNascto", infoRegPrelim.dtNascto),
         //new XElement(ns + "dtAdm", infoRegPrelim.dtAdm));

         // infoRegPrelim
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "infoRegPrelim",
         new XElement(ns + "cpfTrab", infoRegPrelim.cpfTrab),
         new XElement(ns + "dtNascto", infoRegPrelim.dtNascto),
         new XElement(ns + "dtAdm", infoRegPrelim.dtAdm)));

         return x509.signXMLSHA256(xml, cert);
      }

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento {
         public string verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

      public sInfoRegPrelim infoRegPrelim;
      public struct sInfoRegPrelim {
         public string cpfTrab;
         public string dtNascto, dtAdm;
      }

      #endregion
   }
}