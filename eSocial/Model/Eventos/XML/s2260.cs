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

namespace eSocial.Model.Eventos.XML
{
   public class s2260 : bEvento_XML
   {

      public s2260(string sID) : base("evtConvInterm", "infoConvInterm")
      {

         id = sID;

         ideEvento = new sIdeEvento();
         ideVinculo = new sIdeVinculo();

         infoConvInterm = new sInfoConvInterm();
         infoConvInterm.jornada = new sInfoConvInterm.sJornada();
         infoConvInterm.localTrab = new sInfoConvInterm.sLocalTrab();
         infoConvInterm.localTrabInterm = new sInfoConvInterm.sLocalTrabInterm();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {

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

         // infoConvInterm
         xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(
         //new XElement(ns + "infoConvInterm",

         opElement("codConv", infoConvInterm.codConv,
         new XElement(ns + "dtInicio", infoConvInterm.dtInicio),
         new XElement(ns + "dtFim", infoConvInterm.dtFim),
         new XElement(ns + "dtPrevPgto", infoConvInterm.dtPrevPgto)),

         // jornada 1.1
         opElement("codHorContrat", infoConvInterm.jornada.codHorContrat,
         opTag("dscJornada", infoConvInterm.jornada.dscJornada)),

         // localTrab 1.1
         opElement("indLocal", infoConvInterm.localTrab.indLocal),

         // localTrabInterm 0.1
         opElement("tpLograd", infoConvInterm.localTrabInterm.tpLograd,
         new XElement(ns + "dscLograd", infoConvInterm.localTrabInterm.dscLograd),
         new XElement(ns + "nrLograd", infoConvInterm.localTrabInterm.nrLograd),
         opTag("complem", infoConvInterm.localTrabInterm.complem),
         opTag("bairro", infoConvInterm.localTrabInterm.bairro),
         new XElement(ns + "cep", infoConvInterm.localTrabInterm.cep),
         new XElement(ns + "codMunic", infoConvInterm.localTrabInterm.codMunic),
         new XElement(ns + "uf", infoConvInterm.localTrabInterm.uf)));

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
      public struct sIdeVinculo
      {
         public string cpfTrab, nisTrab, matricula;
      }

      public sInfoConvInterm infoConvInterm;
      public struct sInfoConvInterm
      {
         public string dtInicio, dtFim, dtPrevPgto, codConv;
        
         public sJornada jornada;
         public struct sJornada
         {
            public string codHorContrat;
            public string dscJornada;
         }

         public sLocalTrab localTrab;
         public struct sLocalTrab
         {
            public string indLocal;
         }

         public sLocalTrabInterm localTrabInterm;
         public struct sLocalTrabInterm
         {
            public string tpLograd, dscLograd, nrLograd, complem, bairro, cep, codMunic, uf;
         }
      }
      #endregion
   }
}