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
using static eSocial.Model.Eventos.XML.s1210.sIdeBenef.sInfoIRComple.sPlanSaude;
using static eSocial.Model.Eventos.XML.s1210.sIdeBenef.sInfoIRComple;
using static eSocial.Model.Eventos.XML.s1210;
using static eSocial.Model.Eventos.XML.s2190.sInfoRegPrelim;

namespace eSocial.Model.Eventos.XML {
   public class s2190 : bEvento_XML {

        public s2190(string sID) : base("evtAdmPrelim", "", "v_S_01_03_00")
        {
            id = sID;
            infoRegPrelim = new sInfoRegPrelim();
            infoRegPrelim.infoRegCTPS = new sInfoRegPrelim.sInfoRegCTPS();
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
         new XElement(ns + "natAtividade", infoRegPrelim.natAtividade),

         // infoRegCTPS 0.1
         from e in lInfoRegCTPS
         select e

         ));

         lInfoRegCTPS = new List<XElement>();

         return x509.signXMLSHA256(xml, cert);
      }

      List<XElement> lInfoRegCTPS = new List<XElement>();
      public void add_infoRegCTPS()
      {

         lInfoRegCTPS.Add(

         opElement("infoRegCTPS", infoRegPrelim.infoRegCTPS.CBOCargo,
         new XElement(ns + "CBOCargo", infoRegPrelim.infoRegCTPS.CBOCargo),
         new XElement(ns + "vrSalFx", infoRegPrelim.infoRegCTPS.vrSalFx),
         new XElement(ns + "undSalFixo", infoRegPrelim.infoRegCTPS.undSalFixo),
         new XElement(ns + "tpContr", infoRegPrelim.infoRegCTPS.tpContr),
         //new XElement(ns + "dtTerm", infoRegPrelim.infoRegCTPS.dtTerm )));
         opTag("dtTerm", infoRegPrelim.infoRegCTPS.dtTerm)));

         infoRegPrelim.infoRegCTPS = new sInfoRegPrelim.sInfoRegCTPS();
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


         public sInfoRegCTPS infoRegCTPS;
         public struct sInfoRegCTPS
         {
            public string CBOCargo, vrSalFx, undSalFixo, tpContr, dtTerm;
         }
      }
      #endregion
   }
}