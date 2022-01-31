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
    public class s1050 : bEvento_XML {

        public s1050(string sID) : base("evtTabHorTur", "infoHorContratual") {

            id = sID;

            infoHorContratual = new sInfoHorContratual();
            infoHorContratual.inclusao = new sInfoHorContratual.sIncAlt();
            infoHorContratual.inclusao.ideHorContratual = new sInfoHorContratual.sIncAlt.sIdeHorContratual();

            infoHorContratual.inclusao.dadosHorContratual = new sInfoHorContratual.sIncAlt.sDadosHorContratual();
            infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo = new sInfoHorContratual.sIncAlt.sDadosHorContratual.sHorarioIntervalo();

            infoHorContratual.alteracao = new sInfoHorContratual.sIncAlt();
            infoHorContratual.alteracao.ideHorContratual = new sInfoHorContratual.sIncAlt.sIdeHorContratual();

            infoHorContratual.alteracao.dadosHorContratual = new sInfoHorContratual.sIncAlt.sDadosHorContratual();
            infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo = new sInfoHorContratual.sIncAlt.sDadosHorContratual.sHorarioIntervalo();

            infoHorContratual.alteracao.novaValidade = new sIdePeriodo();

            infoHorContratual.exclusao = new sInfoHorContratual.sExclusao();
            infoHorContratual.exclusao.IdeHorContratual = new sInfoHorContratual.sIncAlt.sIdeHorContratual();
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

            // infoHorContratual
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1                  
            opElement("inclusao", infoHorContratual.inclusao.ideHorContratual.codHorContrat,

             // ideHorContratual
             new XElement(ns + "ideHorContratual",
             new XElement(ns + "codHorContrat", infoHorContratual.inclusao.ideHorContratual.codHorContrat),
             new XElement(ns + "iniValid", infoHorContratual.inclusao.ideHorContratual.iniValid),
             opTag("fimValid", infoHorContratual.inclusao.ideHorContratual.fimValid)),

             // dadosHorContratual
             new XElement(ns + "dadosHorContratual",
             new XElement(ns + "hrEntr", infoHorContratual.inclusao.dadosHorContratual.hrEntr),
             new XElement(ns + "hrSaida", infoHorContratual.inclusao.dadosHorContratual.hrSaida),
             new XElement(ns + "durJornada", infoHorContratual.inclusao.dadosHorContratual.durJornada),
             new XElement(ns + "perHorFlexivel", infoHorContratual.inclusao.dadosHorContratual.perHorFlexivel),

             // horarioIntervalo 0.99
             from e in lHorarioIntervalo
             select e)

             ), // inclusao

             // alteracao 0.1                  
             opElement("alteracao", infoHorContratual.alteracao.ideHorContratual.codHorContrat,

             // ideHorContratual
             new XElement(ns + "ideHorContratual",
             new XElement(ns + "codHorContrat", infoHorContratual.alteracao.ideHorContratual.codHorContrat),
             new XElement(ns + "iniValid", infoHorContratual.alteracao.ideHorContratual.iniValid),
             opTag("fimValid", infoHorContratual.alteracao.ideHorContratual.fimValid)),

             // dadosHorContratual
             new XElement(ns + "dadosHorContratual",
             new XElement(ns + "hrEntr", infoHorContratual.alteracao.dadosHorContratual.hrEntr),
             new XElement(ns + "hrSaida", infoHorContratual.alteracao.dadosHorContratual.hrSaida),
             new XElement(ns + "durJornada", infoHorContratual.alteracao.dadosHorContratual.durJornada),
             new XElement(ns + "perHorFlexivel", infoHorContratual.alteracao.dadosHorContratual.perHorFlexivel),

             // horarioIntervalo 0.99
             from e in lHorarioIntervalo_alteracao
             select e),

             // novaValidade 0.1
             opElement("novaValidade", infoHorContratual.alteracao.novaValidade.iniValid,
             new XElement(ns + "iniValid", infoHorContratual.alteracao.novaValidade.iniValid),
             opTag("fimValid", infoHorContratual.alteracao.novaValidade.fimValid))

             ), // alteracao

             // exclusao 0.1
             opElement("exclusao", infoHorContratual.exclusao.IdeHorContratual.codHorContrat,
          new XElement(ns + "ideHorContratual",
          new XElement(ns + "codHorContrat", infoHorContratual.exclusao.IdeHorContratual.codHorContrat),
          new XElement(ns + "iniValid", infoHorContratual.exclusao.IdeHorContratual.iniValid),
          opTag("fimValid", infoHorContratual.exclusao.IdeHorContratual.fimValid))

          )); // exclusao

            return x509.signXMLSHA256(xml, cert);
        }
        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

        #region horarioIntervalo   

        List<XElement> lHorarioIntervalo = new List<XElement>();
        public void add_horarioIntervalo() {

            lHorarioIntervalo.Add(
            new XElement(ns + "horarioIntervalo",
            new XElement(ns + "tpInterv", infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo.tpInterv),
            new XElement(ns + "durInterv", infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo.durInterv),
            opTag("iniInterv", infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo.iniInterv),
            opTag("termInterv", infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo.termInterv)));

            infoHorContratual.inclusao.dadosHorContratual.horarioIntervalo = new sInfoHorContratual.sIncAlt.sDadosHorContratual.sHorarioIntervalo();
        }
        #endregion

        #region horarioIntervalo_alteracao   

        List<XElement> lHorarioIntervalo_alteracao = new List<XElement>();
        public void add_horarioIntervalo_alteracao() {

            lHorarioIntervalo_alteracao.Add(
            new XElement(ns + "horarioIntervalo",
            new XElement(ns + "tpInterv", infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo.tpInterv),
            new XElement(ns + "durInterv", infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo.durInterv),
            opTag("iniInterv", infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo.iniInterv),
            opTag("termInterv", infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo.termInterv)));

            infoHorContratual.alteracao.dadosHorContratual.horarioIntervalo = new sInfoHorContratual.sIncAlt.sDadosHorContratual.sHorarioIntervalo();
        }
        #endregion

        #endregion

        #region ********************************************************************************************************************************************************* Structs

        public sInfoHorContratual infoHorContratual;
        public struct sInfoHorContratual {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;
            public struct sIncAlt {

                public sIdeHorContratual ideHorContratual;
                public struct sIdeHorContratual {
                    public string codHorContrat, iniValid, fimValid;
                }
                public sDadosHorContratual dadosHorContratual;
                public struct sDadosHorContratual {
                    public string hrEntr, hrSaida, perHorFlexivel, durJornada;
                    public sHorarioIntervalo horarioIntervalo;
                    public struct sHorarioIntervalo { public string tpInterv, durInterv, iniInterv, termInterv; }
                }
                public sIdePeriodo novaValidade;
            }
            public struct sExclusao { public sIncAlt.sIdeHorContratual IdeHorContratual; }
        }
        #endregion
    }
}