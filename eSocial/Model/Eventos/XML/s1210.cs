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

        public s1210(string sID) : base("evtPgtos") {

            id = sID;

            ideEvento = new sIdeEvento();

            ideBenef = new sIdeBenef();
            ideBenef.deps = new sIdeBenef.sDeps();

            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();

            ideBenef.infoPgto.detPgtoFl = new sIdeBenef.sInfoPgto.sDetPgtoFl();
            ideBenef.infoPgto.detPgtoFl.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot();
            ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot.sPenAlim();
            ideBenef.infoPgto.detPgtoFl.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoFl.sInfoPgtoParc();

            ideBenef.infoPgto.detPgtoBenPr = new sIdeBenef.sInfoPgto.sDetPgtoBenPr();
            ideBenef.infoPgto.detPgtoBenPr.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sRetPgtoTot();
            ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sInfoPgtoParc();

            ideBenef.infoPgto.detPgtoFer = new sIdeBenef.sInfoPgto.sDetPgtoFer();
            ideBenef.infoPgto.detPgtoFer.detRubrFer = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer();
            ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer.sPenAlim();

            ideBenef.infoPgto.detPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt();
            ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt.sInfoPgtoAnt();

            ideBenef.infoPgto.idePgtoExt = new sIdeBenef.sInfoPgto.sIdePgtoExt();
            ideBenef.infoPgto.idePgtoExt.idePais = new sIdeBenef.sInfoPgto.sIdePgtoExt.sIdePais();
            ideBenef.infoPgto.idePgtoExt.endExt = new sIdeBenef.sInfoPgto.sIdePgtoExt.sEndExt();
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

            // ideBenef
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "ideBenef",
            new XElement(ns + "cpfBenef", ideBenef.cpfBenef),

            // deps 0.1
            opElement("deps", ideBenef.deps.vrDedDep,
            new XElement("vrDedDep", ideBenef.deps.vrDedDep)),

            // infoPgto 1.60
            from e in lInfoPgto
            select e));

            return x509.signXMLSHA256(xml, cert);
        }

        #region *************************************************************************************************************** Tags com +1 ocorrência

        #region infoPgto

        List<XElement> lInfoPgto = new List<XElement>();
        public void add_infoPgto() {

            lInfoPgto.Add(

            new XElement(ns + "infoPgto",
            new XElement(ns + "dtPgto", ideBenef.infoPgto.dtPgto),
            new XElement(ns + "tpPgto", ideBenef.infoPgto.tpPgto),
            new XElement(ns + "indResBr", ideBenef.infoPgto.indResBr),

            // detPgtoFl 0.200
            from e in lDetPgtoFl
            select e,

            // detPgtoBenPr 0.1
            opElement("detPgtoBenPr", ideBenef.infoPgto.detPgtoBenPr.perRef,
            new XElement("perRef", ideBenef.infoPgto.detPgtoBenPr.perRef),
            new XElement("ideDmDev", ideBenef.infoPgto.detPgtoBenPr.ideDmDev),
            new XElement("indPgtoTt", ideBenef.infoPgto.detPgtoBenPr.indPgtoTt),
            new XElement("vrLiq", ideBenef.infoPgto.detPgtoBenPr.vrLiq),

            // retPgtoTot 0.99
            from e in lRetPgtoTot_detPgtoBenPr
            select e,

            // infoPgtoParc 0.99
            from e in lInfoPgtoParc_detPgtoBenPr
            select e),

            // detPgtoFer 0.5
            from e in lDetPgtoFer
            select e,

            // detPgtoAnt 0.99
            from e in lDetPgtoAnt
            select e,

            // idePgtoExt 0.1
            opElement("idePgtoExt", ideBenef.infoPgto.idePgtoExt.idePais.codPais,
            new XElement(ns + "idePais",
            new XElement(ns + "codPais", ideBenef.infoPgto.idePgtoExt.idePais.codPais),
            new XElement(ns + "indNIF", ideBenef.infoPgto.idePgtoExt.idePais.indNIF),
            opTag("nifBenef", ideBenef.infoPgto.idePgtoExt.idePais.nifBenef)),

            // endExt
            new XElement(ns + "endExt",
            new XElement(ns + "dscLograd", ideBenef.infoPgto.idePgtoExt.endExt.dscLograd),
            opTag("nrLograd", ideBenef.infoPgto.idePgtoExt.endExt.nrLograd),
            opTag("complem", ideBenef.infoPgto.idePgtoExt.endExt.complem),
            opTag("bairro", ideBenef.infoPgto.idePgtoExt.endExt.bairro),
            new XElement(ns + "nmCid", ideBenef.infoPgto.idePgtoExt.endExt.nmCid),
            opTag("codPostal", ideBenef.infoPgto.idePgtoExt.endExt.codPostal)))

            ));

            ideBenef.infoPgto = new sIdeBenef.sInfoPgto();

            ideBenef.infoPgto.detPgtoFl = new sIdeBenef.sInfoPgto.sDetPgtoFl();
            ideBenef.infoPgto.detPgtoFl.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot();
            ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot.sPenAlim();
            ideBenef.infoPgto.detPgtoFl.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoFl.sInfoPgtoParc();

            ideBenef.infoPgto.detPgtoBenPr = new sIdeBenef.sInfoPgto.sDetPgtoBenPr();
            ideBenef.infoPgto.detPgtoBenPr.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sRetPgtoTot();
            ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sInfoPgtoParc();

            ideBenef.infoPgto.detPgtoFer = new sIdeBenef.sInfoPgto.sDetPgtoFer();
            ideBenef.infoPgto.detPgtoFer.detRubrFer = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer();
            ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer.sPenAlim();

            ideBenef.infoPgto.detPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt();
            ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt.sInfoPgtoAnt();

            ideBenef.infoPgto.idePgtoExt = new sIdeBenef.sInfoPgto.sIdePgtoExt();
            ideBenef.infoPgto.idePgtoExt.idePais = new sIdeBenef.sInfoPgto.sIdePgtoExt.sIdePais();
            ideBenef.infoPgto.idePgtoExt.endExt = new sIdeBenef.sInfoPgto.sIdePgtoExt.sEndExt();

            // new new
            lDetPgtoFl = new List<XElement>();
        }

        #endregion

        #region detPgtoFl

        List<XElement> lDetPgtoFl = new List<XElement>();
        public void add_detPgtoFl() {

            lDetPgtoFl.Add(

            new XElement(ns + "detPgtoFl",
            opTag("perRef", ideBenef.infoPgto.detPgtoFl.perRef),
            new XElement(ns + "ideDmDev", ideBenef.infoPgto.detPgtoFl.ideDmDev),
            new XElement(ns + "indPgtoTt", ideBenef.infoPgto.detPgtoFl.indPgtoTt),
            new XElement(ns + "vrLiq", ideBenef.infoPgto.detPgtoFl.vrLiq),
            opTag("nrRecArq", ideBenef.infoPgto.detPgtoFl.nrRecArq),

            // retPgtoTot 0.99
            from e in lRetPgtoTot_detPgtoFl
            select e,

            // infoPgtoParc 0.99
            from e in lInfoPgtoParc_detPgtoFl
            select e

            ));

            ideBenef.infoPgto.detPgtoFl = new sIdeBenef.sInfoPgto.sDetPgtoFl();

            // new new
            lRetPgtoTot_detPgtoFl = new List<XElement>();
            lRetPgtoTot_detPgtoBenPr = new List<XElement>();
            //lPenAlim_detPgtoFl = new List<XElement>();
            //lPenAlim_detRubrFer = new List<XElement>();

            // inserido em 14/05/19
            lDetPgtoFer = new List<XElement>(); 
            lDetRubrFer = new List<XElement>();
            lDetPgtoAnt = new List<XElement>();
            lInfoPgtoAnt = new List<XElement>();
        }

        #endregion

        #region lRetPgtoTot_detPgtoFl

        List<XElement> lRetPgtoTot_detPgtoFl = new List<XElement>();
        public void add_retPgtoTot_detPgtoFl() {

            lRetPgtoTot_detPgtoFl.Add(

            new XElement(ns + "retPgtoTot",
            new XElement(ns + "codRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.codRubr),
            new XElement(ns + "ideTabRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.ideTabRubr),
            opTag("qtdRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.qtdRubr),
            opTag("fatorRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.fatorRubr),
            opTag("vrUnit", ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrUnit),
            new XElement(ns + "vrRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrRubr),

            // penAlim 0.99
            from e in lPenAlim_detPgtoFl
            select e

            ));

				ideBenef.infoPgto.detPgtoFl.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot();
			   lPenAlim_detPgtoFl = new List<XElement>();
		  }

        #endregion

        #region retPgtoTot_detPgtoBenPr

        List<XElement> lRetPgtoTot_detPgtoBenPr = new List<XElement>();
        public void add_retPgtoTot_detPgtoBenPr() {

            lRetPgtoTot_detPgtoBenPr.Add(

            new XElement(ns + "retPgtoTot",
            new XElement(ns + "codRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.codRubr),
            new XElement(ns + "ideTabRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.ideTabRubr),
            opTag("qtdRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.qtdRubr),
            opTag("fatorRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.fatorRubr),
            opTag("vrUnit", ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrUnit),
            new XElement(ns + "vrRubr", ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrRubr)));

            ideBenef.infoPgto.detPgtoBenPr.retPgtoTot = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sRetPgtoTot();
        }

        #endregion

        #region penAlim_detPgtoFl

        List<XElement> lPenAlim_detPgtoFl = new List<XElement>();
        public void add_penAlim_detPgtoFl() {

            lPenAlim_detPgtoFl.Add(

            new XElement(ns + "penAlim",
            new XElement(ns + "cpfBenef", ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.cpfBenef),
            opTag("dtNasctoBenef", ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.dtNasctoBenef),
            new XElement(ns + "nmBenefic", ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.nmBenefic),
            new XElement(ns + "vlrPensao", ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.vlrPensao)));

            ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFl.sRetPgtoTot.sPenAlim();
        }

        #endregion

        #region penAlim_detRubrFer

        List<XElement> lPenAlim_detRubrFer = new List<XElement>();
        public void add_penAlim_detRubrFer() {

            lPenAlim_detRubrFer.Add(

            new XElement(ns + "penAlim",
            new XElement(ns + "cpfBenef", ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.cpfBenef),
            opTag("dtNasctoBenef", ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.dtNasctoBenef),
            new XElement(ns + "nmBenefic", ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.nmBenefic),
            new XElement(ns + "vlrPensao", ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.vlrPensao)));

            ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer.sPenAlim();
        }

        #endregion

        #region infoPgtoParc_detPgtoFl

        List<XElement> lInfoPgtoParc_detPgtoFl = new List<XElement>();
        public void add_infoPgtoParc_detPgtoFl() {

            lInfoPgtoParc_detPgtoFl.Add(

           new XElement(ns + "infoPgtoParc",
           opTag("matricula", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.matricula),
           new XElement(ns + "codRubr", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.codRubr),
           new XElement(ns + "ideTabRubr", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.ideTabRubr),
           opTag("qtdRubr", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.qtdRubr),
           opTag("fatorRubr", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.fatorRubr),
           opTag("vrUnit", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.vrUnit),
           new XElement(ns + "vrRubr", ideBenef.infoPgto.detPgtoFl.infoPgtoParc.vrRubr)));

            ideBenef.infoPgto.detPgtoFl.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoFl.sInfoPgtoParc();

         // new new
         lInfoPgtoParc_detPgtoBenPr = new List<XElement>();
         //lDetPgtoFer = new List<XElement>();
         //lDetRubrFer = new List<XElement>();
      }

        #endregion

        #region infoPgtoParc_detPgtoBenPr

        List<XElement> lInfoPgtoParc_detPgtoBenPr = new List<XElement>();
        public void add_infoPgtoParc_detPgtoBenPr() {

            lInfoPgtoParc_detPgtoBenPr.Add(

           new XElement(ns + "infoPgtoParc",
           new XElement(ns + "codRubr", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.codRubr),
           new XElement(ns + "ideTabRubr", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.ideTabRubr),
           opTag("qtdRubr", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.qtdRubr),
           opTag("fatorRubr", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.fatorRubr),
           opTag("vrUnit", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.vrUnit),
           new XElement(ns + "vrRubr", ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.vrRubr)));

            ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc = new sIdeBenef.sInfoPgto.sDetPgtoBenPr.sInfoPgtoParc();
        }

        #endregion

        #region detPgtoFer

        List<XElement> lDetPgtoFer = new List<XElement>();
        public void add_detPgtoFer() {

            lDetPgtoFer.Add(

           new XElement(ns + "detPgtoFer",
           new XElement(ns + "codCateg", ideBenef.infoPgto.detPgtoFer.codCateg),
           new XElement(ns + "matricula", ideBenef.infoPgto.detPgtoFer.matricula),
           new XElement(ns + "dtIniGoz", ideBenef.infoPgto.detPgtoFer.dtIniGoz),
           new XElement(ns + "qtDias", ideBenef.infoPgto.detPgtoFer.qtDias),
           new XElement(ns + "vrLiq", ideBenef.infoPgto.detPgtoFer.vrLiq),

           // detRubrFer 1.99
           from e in lDetRubrFer
           select e

           ));

            ideBenef.infoPgto.detPgtoFer = new sIdeBenef.sInfoPgto.sDetPgtoFer();
        }

        #endregion

        #region detRubrFer

        List<XElement> lDetRubrFer = new List<XElement>();
        public void add_detRubrFer() {

            lDetRubrFer.Add(

           new XElement(ns + "detRubrFer",
           new XElement(ns + "codRubr", ideBenef.infoPgto.detPgtoFer.detRubrFer.codRubr),
           new XElement(ns + "ideTabRubr", ideBenef.infoPgto.detPgtoFer.detRubrFer.ideTabRubr),
           opTag("qtdRubr", ideBenef.infoPgto.detPgtoFer.detRubrFer.qtdRubr),
           opTag("fatorRubr", ideBenef.infoPgto.detPgtoFer.detRubrFer.fatorRubr),
           opTag("vrUnit", ideBenef.infoPgto.detPgtoFer.detRubrFer.vrUnit),
           new XElement(ns + "vrRubr", ideBenef.infoPgto.detPgtoFer.detRubrFer.vrRubr),

            // penAlim 0.99
            from e in lPenAlim_detRubrFer
            select e

           ));

            ideBenef.infoPgto.detPgtoFer.detRubrFer = new sIdeBenef.sInfoPgto.sDetPgtoFer.sDetRubrFer();
				lPenAlim_detRubrFer = new List<XElement>();
		}

		#endregion

		#region detPgtoAnt

		List<XElement> lDetPgtoAnt = new List<XElement>();
        public void add_detPgtoAnt() {

            lDetPgtoAnt.Add(

           new XElement(ns + "detPgtoAnt",
           new XElement(ns + "codCateg", ideBenef.infoPgto.detPgtoAnt.codCateg),

           // detRubrFer 1.99
           from e in lInfoPgtoAnt
           select e

           ));

            ideBenef.infoPgto.detPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt();

         // new new
         lInfoPgtoAnt = new List<XElement>();
        }

        #endregion

        #region infoPgtoAnt

        List<XElement> lInfoPgtoAnt = new List<XElement>();
        public void add_infoPgtoAnt() {

            lInfoPgtoAnt.Add(

           new XElement(ns + "infoPgtoAnt",
           new XElement(ns + "tpBcIRRF", ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt.tpBcIRRF),
           new XElement(ns + "vrBcIRRF", ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt.vrBcIRRF))

           );

            ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt = new sIdeBenef.sInfoPgto.sDetPgtoAnt.sInfoPgtoAnt();
        }

        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao, perApur, verProc, nrRecibo;
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
                public string indResBr;

                public sDetPgtoFl detPgtoFl;
                public struct sDetPgtoFl {
                    public string perRef, ideDmDev, indPgtoTt, nrRecArq;
                    public string vrLiq;

                    public sRetPgtoTot retPgtoTot;
                    public struct sRetPgtoTot {
                        public string codRubr, ideTabRubr;
                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;

                        public sPenAlim penAlim;
                        public struct sPenAlim {
                            public string cpfBenef, nmBenefic;
                            public string dtNasctoBenef;
                            public string vlrPensao;
                        }
                    }
                    public sInfoPgtoParc infoPgtoParc;
                    public struct sInfoPgtoParc {
                        public string matricula, codRubr, ideTabRubr;
                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;
                    }
                }
                public sDetPgtoBenPr detPgtoBenPr;
                public struct sDetPgtoBenPr {
                    public string perRef, ideDmDev, indPgtoTt;
                    public string vrLiq;

                    public sRetPgtoTot retPgtoTot;
                    public struct sRetPgtoTot {
                        public string codRubr, ideTabRubr;
                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;
                    }

                    public sInfoPgtoParc infoPgtoParc;
                    public struct sInfoPgtoParc {
                        public string codRubr, ideTabRubr;
                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;
                    }
                }
                public sDetPgtoFer detPgtoFer;
                public struct sDetPgtoFer {
                    public string matricula;
                    public string codCateg, qtDias;
                    public string vrLiq;
                    public string dtIniGoz;

                    public sDetRubrFer detRubrFer;
                    public struct sDetRubrFer {
                        public string codRubr, ideTabRubr;
                        public string qtdRubr, fatorRubr, vrUnit, vrRubr;

                        public sPenAlim penAlim;
                        public struct sPenAlim {
                            public string cpfBenef, nmBenefic;
                            public string dtNasctoBenef;
                            public string vlrPensao;
                        }
                    }
                }
                public sDetPgtoAnt detPgtoAnt;
                public struct sDetPgtoAnt {
                    public string codCateg;

                    public sInfoPgtoAnt infoPgtoAnt;
                    public struct sInfoPgtoAnt {
                        public string tpBcIRRF;
                        public string vrBcIRRF;
                    }
                }
                public sIdePgtoExt idePgtoExt;
                public struct sIdePgtoExt {
                    public sIdePais idePais;
                    public struct sIdePais {
                        public string codPais, nifBenef;
                        public string indNIF;
                    }
                    public sEndExt endExt;
                    public struct sEndExt { public string dscLograd, nrLograd, complem, bairro, nmCid, codPostal; }
                }
            }
        }

        #endregion
    }
}
