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
    public class s2399 : bEvento_XML
    {
        public s2399(string sID) : base("evtTSVTermino", "", "v_S_01_03_00")
        {
            id = sID;
            ideEvento = new sIdeEvento();
            trabalhador = new sTrabalhador();

            infoTSVTermino = new sInfoTSVTermino();
            infoTSVTermino.mudancaCPF = new sInfoTSVTermino.sMudancaCPF();
            infoTSVTermino.quarentena = new sInfoTSVTermino.sQuarentena();

            verbasResc = new sVerbasResc();
            verbasResc.dmDev = new sVerbasResc.sDmDev();
            verbasResc.dmDev.ideEstabLot = new sVerbasResc.sDmDev.sIdeEstabLot();
            verbasResc.dmDev.ideEstabLot.detVerbas = new sVerbasResc.sDmDev.sIdeEstabLot.sDetVerbas();
            verbasResc.dmDev.ideEstabLot.infoAgNocivo = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoAgNocivo();
            verbasResc.dmDev.ideEstabLot.infoSaudeColet = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSaudeColet();
            verbasResc.dmDev.ideEstabLot.infoSimples = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSimples();

            verbasResc.infoMV = new sVerbasResc.sInfoMV();
            verbasResc.procJudTrab = new sVerbasResc.sProcJudTrab();
        }

        public override XElement genSignedXML(X509Certificate2 cert)
        {
            // ideEvento
            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
            new XElement(ns + "indRetif", ideEvento.indRetif),
            opTag("nrRecibo", ideEvento.nrRecibo),
            opTag("indGuia", ideEvento.indGuia),
            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
            new XElement(ns + "verProc", ideEvento.verProc));

            // ideEmpregador
            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

            // trabalhador
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "ideTrabSemVinculo",
            new XElement(ns + "cpfTrab", trabalhador.cpfTrab),
            opTag("matricula", trabalhador.matricula),
            //new XElement(ns + "codCateg", trabalhador.codCateg)
            opTag("codCateg", trabalhador.codCateg)));

            // infoTSVTermino
            xml.Elements().ElementAt(0).Add(
            opElement("infoTSVTermino", infoTSVTermino.dtTerm,
            new XElement(ns + "dtTerm", infoTSVTermino.dtTerm),
            opTag("mtvDesligTSV", infoTSVTermino.mtvDesligTSV),
            opTag("pensAlim", infoTSVTermino.pensAlim),
            opTag("percAliment", infoTSVTermino.percAliment),
            opTag("vrAlim", infoTSVTermino.vrAlim),
            opTag("nrProcTrab", infoTSVTermino.nrProcTrab),

            // mudancaCPF 
            opElement("mudancaCPF",
            opTag("novoCPF", infoTSVTermino.mudancaCPF.novoCPF)),

            // dmDev 1.50
            from e in lDmDev
            select e,

            // infoInterm 0.1
            opElement("quarentena", infoTSVTermino.quarentena.dtFimQuar,
            new XElement(ns + "qtdDiasInterm", infoTSVTermino.quarentena.dtFimQuar))

            )); // infoTSVInicio

            return x509.signXMLSHA256(xml, cert);
        }

        #region dmDev
        List<XElement> lDmDev = new List<XElement>();
        public void add_dmDev()
        {
            lDmDev.Add(

            new XElement(ns + "dmDev",
            new XElement(ns + "ideDmDev", verbasResc.dmDev.ideDmDev),

            // ideEstabLot 1.99
            from e in lIdeEstabLot
            select e

            ));

            verbasResc.dmDev = new sVerbasResc.sDmDev();
            verbasResc.dmDev.ideEstabLot = new sVerbasResc.sDmDev.sIdeEstabLot();  
            lIdeEstabLot = new List<XElement>();
        }
        #endregion


        #region ideEstabLot

        List<XElement> lIdeEstabLot = new List<XElement>();
        public void add_ideEstabLot()
        {
            lIdeEstabLot.Add(
            new XElement(ns + "ideEstabLot",
            new XElement(ns + "tpInsc", verbasResc.dmDev.ideEstabLot.tpInsc),
            new XElement(ns + "nrInsc", verbasResc.dmDev.ideEstabLot.nrInsc),
            new XElement(ns + "codLotacao", verbasResc.dmDev.ideEstabLot.codLotacao),

            // remunPerApur 1.8
            from e in lDetVerbas
            select e));

            verbasResc.dmDev.ideEstabLot.detVerbas = new sVerbasResc.sDmDev.sIdeEstabLot.sDetVerbas();
            lDetVerbas = new List<XElement>();
        }
        #endregion

        #region remunPerApur

        List<XElement> lDetVerbas = new List<XElement>();
        public void add_detVerbas()
        {
            lDetVerbas.Add(
            new XElement(ns + "detVerbas",
            new XElement(ns + "codRubr", verbasResc.dmDev.ideEstabLot.detVerbas.codRubr),
            new XElement(ns + "ideTabRubr", verbasResc.dmDev.ideEstabLot.detVerbas.ideTabRubr),
            opTag("qtdRubr", verbasResc.dmDev.ideEstabLot.detVerbas.qtdRubr),
            opTag("fatorRubr", verbasResc.dmDev.ideEstabLot.detVerbas.fatorRubr),
            new XElement(ns + "vrRubr", verbasResc.dmDev.ideEstabLot.detVerbas.vrRubr),
            opTag("indApurIR", verbasResc.dmDev.ideEstabLot.detVerbas.indApurIR),

            //// infoSaudeColet 
            //from e in lItensInfoSaudeColet
            //select e,

            //// infoAgNocivo 0.1
            //opElement("infoAgNocivo", verbasResc.dmDev.ideEstabLot.infoAgNocivo.grauExp,
            //new XElement(ns + "grauExp", verbasResc.dmDev.ideEstabLot.infoAgNocivo.grauExp)),

            // infoSimples 0.1
            opElement("infoSimples", verbasResc.dmDev.ideEstabLot.infoSimples.indSimples,
            new XElement(ns + "indSimples", verbasResc.dmDev.ideEstabLot.infoSimples.indSimples)),

            // procJudTrab 
            from e in lProcJudTrab
            select e,

            // infoMV 
            from e in lInfoMV
            select e,

            // quarentena 0.1
            opElement("quarentena", infoTSVTermino.quarentena,
            new XElement(ns + "dtFimQuar", infoTSVTermino.quarentena))

            ));

            verbasResc.dmDev.ideEstabLot.infoSaudeColet = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSaudeColet();
            verbasResc.dmDev.ideEstabLot.infoAgNocivo = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoAgNocivo();
            verbasResc.dmDev.ideEstabLot.infoSimples = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSimples();
    
            lItensInfoSaudeColet = new List<XElement>();
            lProcJudTrab = new List<XElement>();
        }
        #endregion

        List<XElement> lItensInfoSaudeColet = new List<XElement>();
        public void add_infoSaudeColet()
        {
            lItensInfoSaudeColet.Add(
            new XElement(ns + "detOper",
            new XElement(ns + "cnpjOper", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.cnpjOper),
            new XElement(ns + "regANS", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.regANS),
            new XElement(ns + "vrPgTit", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.vrPgTit),

            // detPlano 
            from e in lDetPlano
            select e));

            verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSaudeColet.sDetOper();
            lDetPlano = new List<XElement>();
        }

        List<XElement> lDetPlano = new List<XElement>();
        public void add_detPlano()
        {
            lDetPlano.Add(
            new XElement(ns + "detPlano",
            new XElement(ns + "tpDep", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano.tpDep),
            opTag("cpfDep", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano.cpfDep),
            new XElement(ns + "nmDep", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano.nmDep),
            new XElement(ns + "dtNascto", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano.dtNascto),
            new XElement(ns + "vlrPgDep", verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano.vlrPgDep)));

            verbasResc.dmDev.ideEstabLot.infoSaudeColet.detOper.detPlano = new sVerbasResc.sDmDev.sIdeEstabLot.sInfoSaudeColet.sDetOper.sDetPlano();
        }
               
        List<XElement> lProcJudTrab = new List<XElement>();
        public void add_procJudTrab()
        {
            lProcJudTrab.Add(
            new XElement(ns + "procJudTrab",
            new XElement(ns + "tpTrib", verbasResc.procJudTrab.tpTrib),
            new XElement(ns + "nrProcJud", verbasResc.procJudTrab.nrProcJud),
            opTag("codSusp", verbasResc.procJudTrab.codSusp)));
            
            verbasResc.procJudTrab = new sVerbasResc.sProcJudTrab();
        }

        List<XElement> lInfoMV = new List<XElement>();
        public void add_infoMV()
        {
            lProcJudTrab.Add(
            new XElement(ns + "infoMV",
            opTag("indMV", verbasResc.infoMV.indMV),

            // remunOutrEmpr 
            from e in lRemunOutrEmpr
            select e));

            verbasResc.infoMV = new sVerbasResc.sInfoMV();
        }

        List<XElement> lRemunOutrEmpr = new List<XElement>();
        public void add_remunOutrEmpr()
        {
            lProcJudTrab.Add(
            new XElement(ns + "remunOutrEmpr",
            new XElement(ns + "tpInsc", verbasResc.infoMV.remunOutrEmpr.tpInsc),
            new XElement(ns + "nrInsc", verbasResc.infoMV.remunOutrEmpr.nrInsc),
            new XElement(ns + "codCateg", verbasResc.infoMV.remunOutrEmpr.codCateg),
            new XElement(ns + "vlrRemunOE", verbasResc.infoMV.remunOutrEmpr.vlrRemunOE)));

            verbasResc.infoMV.remunOutrEmpr = new sVerbasResc.sInfoMV.sRemunOutrEmpr();
        }

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento
        {
            public string indRetif, indApuracao, nrRecibo, indGuia, perApur, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sTrabalhador trabalhador;
        public struct sTrabalhador
        {
            public string cpfTrab, matricula, codCateg;

        }

        public sInfoTSVTermino infoTSVTermino;
        public struct sInfoTSVTermino
        {
            public string dtTerm, mtvDesligTSV, pensAlim, percAliment, vrAlim, nrProcTrab;

            public sMudancaCPF mudancaCPF;
            public struct sMudancaCPF
            {
                public string novoCPF;
            }

            public sQuarentena quarentena;
            public struct sQuarentena
            {
                public string dtFimQuar;
            }
        }

        public sVerbasResc verbasResc;
        public struct sVerbasResc
        {
            public sDmDev dmDev;
            public struct sDmDev
            {
                public string ideDmDev;

                public sIdeEstabLot ideEstabLot;
                public struct sIdeEstabLot
                {
                    public string tpInsc, nrInsc, codLotacao;

                    public sDetVerbas detVerbas;
                    public struct sDetVerbas
                    {
                        public string codRubr, ideTabRubr, qtdRubr, fatorRubr, vrUnit, vrRubr, indApurIR;
                    }

                    public sInfoSaudeColet infoSaudeColet;
                    public struct sInfoSaudeColet
                    {
                        public sDetOper detOper;
                        public struct sDetOper
                        {
                            public string cnpjOper, regANS, vrPgTit;

                            public sDetPlano detPlano;
                            public struct sDetPlano
                            {
                                public string tpDep, cpfDep, nmDep, dtNascto, vlrPgDep;
                            }
                        }
                    }

                    public sInfoAgNocivo infoAgNocivo;
                    public struct sInfoAgNocivo
                    {
                        public string grauExp;
                    }

                    public sInfoSimples infoSimples;
                    public struct sInfoSimples
                    {
                        public string indSimples;
                    }
                }
            }

            public sProcJudTrab procJudTrab;
            public struct sProcJudTrab
            {
                public string tpTrib, nrProcJud, codSusp;
            }

            public sInfoMV infoMV;
            public struct sInfoMV
            {
                public string indMV;

                public sRemunOutrEmpr remunOutrEmpr;
                public struct sRemunOutrEmpr
                {
                    public string tpInsc, nrInsc, codCateg, vlrRemunOE;
                }
            }
        }
        #endregion
    }
}
