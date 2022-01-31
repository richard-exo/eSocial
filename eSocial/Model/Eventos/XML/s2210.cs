using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
   class s2210 : bEvento_XML {

      public s2210(string sID) : base("evtCAT", "", "v_S_01_00_00") {

         id = sID;

         ideEvento = new sIdeEvento();
         ideRegistrador = new sIdeRegistrador();
         ideVinculo = new sIdeVinculo();

         cat = new sCat();
         cat.localAcidente = new sCat.sLocalAcidente();
         cat.parteAtingida = new sCat.sParteAtingida();
         cat.agenteCausador = new sCat.sAgenteCausador();

         cat.atestado = new sCat.sAtestado();
         cat.atestado.emitente = new sCat.sAtestado.sEmitente();

         cat.catOrigem = new sCat.sCatOrigem();
      }

      public override XElement genSignedXML(X509Certificate2 cert) {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

         new XElement(ns + "indRetif", ideEvento.indRetif),
         opTag("nrRecibo", ideEvento.nrRecibo),
         new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
         new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
         new XElement(ns + "verProc", ideEvento.verProc));

         //// ideRegistrador
         //xml.Elements().ElementAt(0).Element(ns + "ideEvento").AddAfterSelf(
         //new XElement(ns + "ideRegistrador",
         //new XElement(ns + "tpRegistrador", ideRegistrador.tpRegistrador),
         //opTag("tpInsc", ideRegistrador.tpInsc),
         //opTag("nrInsc", ideRegistrador.nrInsc)));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // ideVinculo
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideVinculo",
         new XElement(ns + "cpfTrab", ideVinculo.cpfTrab),
         opTag("matricula", ideVinculo.matricula),
         opTag("codCateg", ideVinculo.codCateg)));

         // cat
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "cat",
         new XElement(ns + "dtAcid", cat.dtAcid),
         new XElement(ns + "tpAcid", cat.tpAcid),
         new XElement(ns + "hrAcid", cat.hrAcid),
         new XElement(ns + "hrsTrabAntesAcid", cat.hrsTrabAntesAcid),
         new XElement(ns + "tpCat", cat.tpCat),
         new XElement(ns + "indCatObito", cat.indCatObito),
         opTag("dtObito", cat.dtObito),
         new XElement(ns + "indComunPolicia", cat.indComunPolicia),
         opTag("codSitGeradora", cat.codSitGeradora),
         new XElement(ns + "iniciatCAT", cat.iniciatCAT),
         opTag("obsCAT", cat.obsCAT),

         // localAcidente
         new XElement(ns + "localAcidente",
         new XElement(ns + "tpLocal", cat.localAcidente.tpLocal),
         opTag("dscLocal", cat.localAcidente.dscLocal),
         new XElement(ns + "tpLograd", cat.localAcidente.tpLograd),
         opTag("dscLograd", cat.localAcidente.dscLograd),
         opTag("nrLograd", cat.localAcidente.nrLograd),
         opTag("complemento", cat.localAcidente.complemento),
         opTag("bairro", cat.localAcidente.bairro),
         opTag("cep", cat.localAcidente.cep),
         opTag("codMunic", cat.localAcidente.codMunic),
         opTag("uf", cat.localAcidente.uf),
         opTag("pais", cat.localAcidente.pais),
         opTag("codPostal", cat.localAcidente.codPostal)),

         //// parteAtingida 0.99
         //from e in lParteAtingida
         //select e,

         new XElement(ns + "parteAtingida",
         new XElement(ns + "codParteAting", cat.parteAtingida.codParteAting),
         new XElement(ns + "lateralidade", cat.parteAtingida.lateralidade)),

         //// agenteCausador 0.99
         //from e in lAgenteCausador
         //select e,

         new XElement(ns + "agenteCausador",
         new XElement(ns + "codAgntCausador", cat.agenteCausador.codAgntCausador)),

         // atestado 0.1
         opElement("atestado", cat.atestado.dtAtendimento,
         new XElement(ns + "dtAtendimento", cat.atestado.dtAtendimento),
         new XElement(ns + "hrAtendimento", cat.atestado.hrAtendimento),
         new XElement(ns + "indInternacao", cat.atestado.indInternacao),
         new XElement(ns + "durTrat", cat.atestado.durTrat),
         new XElement(ns + "indAfast", cat.atestado.indAfast),
         opTag("dscLesao", cat.atestado.dscLesao),
         opTag("dscCompLesao", cat.atestado.dscCompLesao),
         opTag("diagProvavel", cat.atestado.diagProvavel),
         new XElement(ns + "codCID", cat.atestado.codCID),
         opTag("observacao", cat.atestado.observacao),

         // emitente
         new XElement(ns + "emitente",
         new XElement(ns + "nmEmit", cat.atestado.emitente.nmEmit),
         new XElement(ns + "ideOC", cat.atestado.emitente.ideOC),
         new XElement(ns + "nrOC", cat.atestado.emitente.nrOC),
         opTag("ufOC", cat.atestado.emitente.ufOC))),

         // catOrigem 0.1
         opElement("catOrigem", cat.catOrigem.nrRecCatOrig,
         new XElement(ns + "nrRecCatOrig", cat.catOrigem.nrRecCatOrig))

         ) // cat

         );

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      //#region parteAtingida

      //List<XElement> lParteAtingida = new List<XElement>();
      //public void add_parteAtingida() {

      //   lParteAtingida.Add(

      //   new XElement(ns + "parteAtingida",
      //   new XElement(ns + "codParteAting", cat.parteAtingida.codParteAting),
      //   new XElement(ns + "lateralidade", cat.parteAtingida.lateralidade)));

      //   cat.parteAtingida = new sCat.sParteAtingida();
      //}
      //#endregion

      //#region agenteCausador

      //List<XElement> lAgenteCausador = new List<XElement>();
      //public void add_agenteCausador() {

      //   lAgenteCausador.Add(

      //   new XElement(ns + "agenteCausador",
      //   new XElement(ns + "codAgntCausador", cat.parteAtingida.codParteAting)));

      //   cat.agenteCausador = new sCat.sAgenteCausador();
      //}
      //#endregion


      #region atestado   

      List<XElement> lInfoAtestado = new List<XElement>();
      public void add_atestado()
      {
         //lInfoAtestado.Add(
         //new XElement(ns + "atestado",
         //opTag("codCNES", cat.atestado.codCNES),
         //new XElement(ns + "dtAtendimento", cat.atestado.dtAtendimento),
         //new XElement(ns + "hrAtendimento", cat.atestado.hrAtendimento),
         //new XElement(ns + "indInternacao", cat.atestado.indInternacao),
         //new XElement(ns + "durTrat", cat.atestado.durTrat),
         //new XElement(ns + "indAfast", cat.atestado.indAfast),
         //new XElement(ns + "dscLesao", cat.atestado.dscLesao),
         //new XElement(ns + "dscCompLesao", cat.atestado.dscCompLesao),
         //new XElement(ns + "diagProvavel", cat.atestado.diagProvavel),
         //new XElement(ns + "codCID", cat.atestado.codCID),
         //new XElement(ns + "observacao", cat.atestado.observacao),

         lInfoAtestado.Add(
         new XElement(ns + "atestado",
         new XElement(ns + "dtAtendimento", cat.atestado.dtAtendimento),
         new XElement(ns + "hrAtendimento", cat.atestado.hrAtendimento),
         new XElement(ns + "indInternacao", cat.atestado.indInternacao),
         new XElement(ns + "durTrat", cat.atestado.durTrat),
         new XElement(ns + "indAfast", cat.atestado.indAfast),
         opTag("dscLesao", cat.atestado.dscLesao),
         opTag("dscCompLesao", cat.atestado.dscCompLesao),
         opTag("diagProvavel", cat.atestado.diagProvavel), 
         new XElement(ns + "codCID", cat.atestado.codCID),
         opTag("observacao", cat.atestado.observacao), 

         opElement("emitente", cat.atestado.emitente.nmEmit,
         new XElement(ns + "nmEmit", cat.atestado.emitente.nmEmit),
         new XElement(ns + "ideOC", cat.atestado.emitente.ideOC),
         new XElement(ns + "nrOC", cat.atestado.emitente.nrOC),
         opTag("ufOC", cat.atestado.emitente.ufOC))));
      }
      #endregion

      #endregion

      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, nrRecibo, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeRegistrador ideRegistrador;
      public struct sIdeRegistrador {
         public string tpRegistrador, tpInsc;
         public string nrInsc;
      }

      public sIdeVinculo ideVinculo;
      public struct sIdeVinculo { public string cpfTrab, matricula, codCateg; }

      public sCat cat;
      public struct sCat {
         public string tpCat, iniciatCAT, codSitGeradora;
         public string hrAcid, hrsTrabAntesAcid, indCatObito, indComunPolicia, obsCAT;
         public string dtAcid, tpAcid, dtObito;

         public sLocalAcidente localAcidente;
         public struct sLocalAcidente {
            public string tpLocal, codMunic;
            public string dscLocal, tpLograd, dscLograd, nrLograd, complemento, bairro, cep,  uf, pais, codPostal;
         }

         public sParteAtingida parteAtingida;
         public struct sParteAtingida { public string codParteAting, lateralidade; }

         public sAgenteCausador agenteCausador;
         public struct sAgenteCausador { public string codAgntCausador; }

         public sAtestado atestado;
         public struct sAtestado {
            public string hrAtendimento, indInternacao, indAfast, dscCompLesao, diagProvavel, codCID, observacao;
            public string dtAtendimento;
            public string durTrat, dscLesao;

            public sEmitente emitente;
            public struct sEmitente {
               public string nmEmit, nrOC, ufOC;
               public string ideOC;
            }
         }
         public sCatOrigem catOrigem;
         public struct sCatOrigem {
            public string nrRecCatOrig;
         }
      }

      #endregion

   }
}
