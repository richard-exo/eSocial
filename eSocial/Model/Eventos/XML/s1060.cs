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
    public class s1060 : bEvento_XML {

        public s1060(string sID) : base("evtTabAmbiente", "infoAmbiente") {

            id = sID;

            infoAmbiente = new sInfoAmbiente();
            infoAmbiente.inclusao = new sInfoAmbiente.sIncAlt();

            infoAmbiente.inclusao.ideAmbiente = new sInfoAmbiente.sIncAlt.sIdeAmbiente();
            infoAmbiente.inclusao.dadosAmbiente = new sInfoAmbiente.sIncAlt.sDadosAmbiente();
            infoAmbiente.inclusao.dadosAmbiente.fatorRisco = new sInfoAmbiente.sIncAlt.sDadosAmbiente.sFatorRisco();

            infoAmbiente.alteracao = new sInfoAmbiente.sIncAlt();

            infoAmbiente.alteracao.ideAmbiente = new sInfoAmbiente.sIncAlt.sIdeAmbiente();
            infoAmbiente.alteracao.dadosAmbiente = new sInfoAmbiente.sIncAlt.sDadosAmbiente();
            infoAmbiente.alteracao.dadosAmbiente.fatorRisco = new sInfoAmbiente.sIncAlt.sDadosAmbiente.sFatorRisco();

            infoAmbiente.alteracao.novaValidade = new sIdePeriodo();

            infoAmbiente.exclusao = new sInfoAmbiente.sExclusao();
            infoAmbiente.exclusao.ideAmbiente = new sInfoAmbiente.sIncAlt.sIdeAmbiente();
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

            // infoAmbiente
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1                  
            opElement("inclusao", infoAmbiente.inclusao.ideAmbiente.codAmb,

             // ideHorContratual
             new XElement(ns + "ideHorContratual",
             new XElement(ns + "codHorContrat", infoAmbiente.inclusao.ideAmbiente.codAmb),
             new XElement(ns + "iniValid", infoAmbiente.inclusao.ideAmbiente.iniValid),
             opTag("fimValid", infoAmbiente.inclusao.ideAmbiente.fimValid)),

             // dadosAmbiente
             new XElement(ns + "dadosAmbiente",
             new XElement(ns + "nmAmb", infoAmbiente.inclusao.dadosAmbiente.nmAmb),
             new XElement(ns + "dscAmb", infoAmbiente.inclusao.dadosAmbiente.dscAmb),
             new XElement(ns + "localAmb", infoAmbiente.inclusao.dadosAmbiente.localAmb),
             new XElement(ns + "tpInsc", infoAmbiente.inclusao.dadosAmbiente.tpInsc),
             new XElement(ns + "nrInsc", infoAmbiente.inclusao.dadosAmbiente.nrInsc),

             // fatorRisco 0.99
             from e in lfatorRisco_inclusao
             select e)

             ), // inclusao

             // alteracao 0.1                  
             opElement("alteracao", infoAmbiente.alteracao.ideAmbiente.codAmb,

             // ideHorContratual
             new XElement(ns + "ideHorContratual",
             new XElement(ns + "codHorContrat", infoAmbiente.alteracao.ideAmbiente.codAmb),
             new XElement(ns + "iniValid", infoAmbiente.alteracao.ideAmbiente.iniValid),
             opTag("fimValid", infoAmbiente.alteracao.ideAmbiente.fimValid)),

             // dadosAmbiente
             new XElement(ns + "dadosAmbiente",
             new XElement(ns + "nmAmb", infoAmbiente.alteracao.dadosAmbiente.nmAmb),
             new XElement(ns + "dscAmb", infoAmbiente.alteracao.dadosAmbiente.dscAmb),
             new XElement(ns + "localAmb", infoAmbiente.alteracao.dadosAmbiente.localAmb),
             new XElement(ns + "tpInsc", infoAmbiente.alteracao.dadosAmbiente.tpInsc),
             new XElement(ns + "nrInsc", infoAmbiente.alteracao.dadosAmbiente.nrInsc),

             // fatorRisco 0.99
             from e in lfatorRisco_alteracao
             select e),

             // novaValidade 0.1
             opElement("novaValidade", infoAmbiente.alteracao.novaValidade.iniValid,
             new XElement(ns + "iniValid", infoAmbiente.alteracao.novaValidade.iniValid),
             opTag("fimValid", infoAmbiente.alteracao.novaValidade.fimValid))

             ), // alteracao

             // exclusao 0.1
             opElement("exclusao", infoAmbiente.exclusao.ideAmbiente.codAmb,
             new XElement(ns + "ideHorContratual",
             new XElement(ns + "codHorContrat", infoAmbiente.exclusao.ideAmbiente.codAmb),
             new XElement(ns + "iniValid", infoAmbiente.exclusao.ideAmbiente.iniValid),
             opTag("fimValid", infoAmbiente.exclusao.ideAmbiente.fimValid))

          )); // exclusao

            return x509.signXMLSHA256(xml, cert);
        }
        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

        #region fatorRisco_inclusao   

        List<XElement> lfatorRisco_inclusao = new List<XElement>();
        public void add_fatorRisco_inclusao() {

            lfatorRisco_inclusao.Add(
            new XElement(ns + "fatorRisco",
            new XElement(ns + "codFatRis", infoAmbiente.inclusao.dadosAmbiente.fatorRisco.codFatRis)));

            infoAmbiente.inclusao.dadosAmbiente.fatorRisco = new sInfoAmbiente.sIncAlt.sDadosAmbiente.sFatorRisco();
        }
        #endregion

        #region fatorRisco_alteracao   

        List<XElement> lfatorRisco_alteracao = new List<XElement>();
        public void add_fatorRisco_alteracao() {

            lfatorRisco_inclusao.Add(
            opElement("fatorRisco", infoAmbiente.alteracao.dadosAmbiente.fatorRisco.codFatRis,
            new XElement(ns + "codFatRis", infoAmbiente.alteracao.dadosAmbiente.fatorRisco.codFatRis)));

            infoAmbiente.alteracao.dadosAmbiente.fatorRisco = new sInfoAmbiente.sIncAlt.sDadosAmbiente.sFatorRisco();
        }
        #endregion

        #endregion

        #region ********************************************************************************************************************************************************* Structs

        public sInfoAmbiente infoAmbiente;
        public struct sInfoAmbiente {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;
            public struct sIncAlt {

                public sIdeAmbiente ideAmbiente;
                public struct sIdeAmbiente {
                    public string codAmb, iniValid, fimValid;
                }
                public sDadosAmbiente dadosAmbiente;
                public struct sDadosAmbiente {
                    public string nmAmb, dscAmb, localAmb, tpInsc, nrInsc;

                    public sFatorRisco fatorRisco;
                    public struct sFatorRisco { public string codFatRis; }
                }
                public sIdePeriodo novaValidade;
            }
            public struct sExclusao { public sIncAlt.sIdeAmbiente ideAmbiente; }
        }
        #endregion
    }
}