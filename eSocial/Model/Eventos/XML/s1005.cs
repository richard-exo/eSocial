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
    public class s1005 : bEvento_XML {

        public s1005(string sID) : base("evtTabEstab", "infoEstab") {

            id = sID;

            infoEstab = new sInfoEstab();

            infoEstab.inclusao = new sInfoEstab.sIncAlt();
            infoEstab.inclusao.ideEstab = new sInfoEstab.sIdeEstab();
            infoEstab.inclusao.dadosEstab = new sInfoEstab.sDadosEstab();
            infoEstab.inclusao.dadosEstab.aliqGilrat = new sInfoEstab.sDadosEstab.sAliqGilrat();
            infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudRat = new sInfoEstab.sDadosEstab.sAliqGilrat.sProcAdmJudRat();
            infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudFap = new sInfoEstab.sDadosEstab.sAliqGilrat.sProcAdmJudFap();
            infoEstab.inclusao.dadosEstab.infoCaepf = new sInfoEstab.sDadosEstab.sInfoCaepf();
            infoEstab.inclusao.dadosEstab.infoObra = new sInfoEstab.sDadosEstab.sInfoObra();
            infoEstab.inclusao.dadosEstab.infoTrab = new sInfoEstab.sDadosEstab.sInfoTrab();
            infoEstab.inclusao.dadosEstab.infoTrab.infoApr = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoApr();
            infoEstab.inclusao.dadosEstab.infoTrab.infoPCD = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoPCD();

            infoEstab.alteracao = new sInfoEstab.sIncAlt();
            infoEstab.alteracao.ideEstab = new sInfoEstab.sIdeEstab();
            infoEstab.alteracao.dadosEstab = new sInfoEstab.sDadosEstab();
            infoEstab.alteracao.dadosEstab.aliqGilrat = new sInfoEstab.sDadosEstab.sAliqGilrat();
            infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudRat = new sInfoEstab.sDadosEstab.sAliqGilrat.sProcAdmJudRat();
            infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudFap = new sInfoEstab.sDadosEstab.sAliqGilrat.sProcAdmJudFap();
            infoEstab.alteracao.dadosEstab.infoCaepf = new sInfoEstab.sDadosEstab.sInfoCaepf();
            infoEstab.alteracao.dadosEstab.infoObra = new sInfoEstab.sDadosEstab.sInfoObra();
            infoEstab.alteracao.dadosEstab.infoTrab = new sInfoEstab.sDadosEstab.sInfoTrab();
            infoEstab.alteracao.dadosEstab.infoTrab.infoApr = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoApr();
            infoEstab.alteracao.dadosEstab.infoTrab.infoPCD = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoPCD();

            infoEstab.alteracao.novaValidade = new sIdePeriodo();

            infoEstab.exclusao = new sInfoEstab.sExclusao();
            infoEstab.exclusao.ideEstab = new sInfoEstab.sIdeEstab();

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

            // infoEstab
            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

            // inclusao 0.1
            opElement("inclusao", infoEstab.inclusao.ideEstab.tpInsc,

            // ideEstab
            new XElement(ns + "ideEstab",
            new XElement(ns + "tpInsc", infoEstab.inclusao.ideEstab.tpInsc),
            new XElement(ns + "nrInsc", infoEstab.inclusao.ideEstab.nrInsc),
            new XElement(ns + "iniValid", infoEstab.inclusao.ideEstab.iniValid),
            opTag("fimValid", infoEstab.inclusao.ideEstab.fimValid)),

            // dadosEstab
            new XElement(ns + "dadosEstab",
            new XElement(ns + "cnaePrep", infoEstab.inclusao.dadosEstab.cnaePrep),

            // aliqGilrat
            new XElement(ns + "aliqGilrat",
            new XElement(ns + "aliqRat", infoEstab.inclusao.dadosEstab.aliqGilrat.aliqRat),
            opTag("fap", infoEstab.inclusao.dadosEstab.aliqGilrat.fap),
            opTag("aliqRatAjust", infoEstab.inclusao.dadosEstab.aliqGilrat.aliqRatAjust),

            //opElement("aliqGilrat", infoEstab.inclusao.dadosEstab.aliqGilrat.aliqRat,
            //new XElement(ns + "aliqRat", infoEstab.inclusao.dadosEstab.aliqGilrat.aliqRat),
            //new XElement(ns + "fap", infoEstab.inclusao.dadosEstab.aliqGilrat.fap),
            //new XElement(ns + "aliqRatAjust", infoEstab.inclusao.dadosEstab.aliqGilrat.aliqRatAjust)),

            // procAdmJudRat 0.1
            opElement("procAdmJudRat", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudRat.tpProc,
            new XElement(ns + "tpProc", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudRat.tpProc),
            new XElement(ns + "nrProc", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudRat.nrProc),
            new XElement(ns + "codSusp", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudRat.codSusp)),

            // procAdmJudFap 0.1
            opElement("procAdmJudFap", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudFap.tpProc,
            new XElement(ns + "tpProc", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudFap.tpProc),
            new XElement(ns + "nrProc", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudFap.nrProc),
            new XElement(ns + "codSusp", infoEstab.inclusao.dadosEstab.aliqGilrat.procAdmJudFap.codSusp))),

            //// infoCaepf 0.1
            //opElement("procAdmJudFap", infoEstab.inclusao.dadosEstab.infoCaepf.tpCaepf,
            //new XElement(ns + "tpCaepf", infoEstab.inclusao.dadosEstab.infoCaepf.tpCaepf)),

            // infoCaepf 0.1
            opElement("infoCaepf", infoEstab.inclusao.dadosEstab.infoCaepf.tpCaepf,
            new XElement(ns + "tpCaepf", infoEstab.inclusao.dadosEstab.infoCaepf.tpCaepf)),

            // infoObra 0.1
            opElement("infoObra", infoEstab.inclusao.dadosEstab.infoObra.indSubstPatrObra,
            new XElement(ns + "indSubstPatrObra", infoEstab.inclusao.dadosEstab.infoObra.indSubstPatrObra)),

            // infoTrab
            new XElement(ns + "infoTrab",
            new XElement(ns + "regPt", infoEstab.inclusao.dadosEstab.infoTrab.regPt),

            // infoApr
            new XElement(ns + "infoApr",
            new XElement(ns + "contApr", infoEstab.inclusao.dadosEstab.infoTrab.infoApr.contApr),
            opTag("nrProcJud", infoEstab.inclusao.dadosEstab.infoTrab.infoApr.nrProcJud),
            opTag("contEntEd", infoEstab.inclusao.dadosEstab.infoTrab.infoApr.contEndEd),

            // infoEntEduc_inclusao 0.99
            from e in lInfoEntEduc_inclusao
            select e),

            // infoPCD 0.1
            opElement("infoPCD", infoEstab.inclusao.dadosEstab.infoTrab.infoPCD.contPCD,
            new XElement(ns + "contPCD", infoEstab.inclusao.dadosEstab.infoTrab.infoPCD.contPCD),
            opTag("nrProcJud", infoEstab.inclusao.dadosEstab.infoTrab.infoPCD.nrProcJud))))

            ), // inclusao

            // alteracao 0.1
            opElement("alteracao", infoEstab.alteracao.ideEstab.tpInsc,

            // ideEstab
            new XElement(ns + "ideEstab",
            new XElement(ns + "tpInsc", infoEstab.alteracao.ideEstab.tpInsc),
            new XElement(ns + "nrInsc", infoEstab.alteracao.ideEstab.nrInsc),
            new XElement(ns + "iniValid", infoEstab.alteracao.ideEstab.iniValid),
            opTag("fimValid", infoEstab.alteracao.ideEstab.fimValid)),

            // dadosEstab
            new XElement(ns + "dadosEstab",
            new XElement(ns + "cnaePrep", infoEstab.alteracao.dadosEstab.cnaePrep),

            // aliqGilrat
            new XElement(ns + "aliqGilrat",
            new XElement(ns + "aliqRat", infoEstab.alteracao.dadosEstab.aliqGilrat.aliqRat),
            opTag("fap", infoEstab.alteracao.dadosEstab.aliqGilrat.fap),
            opTag("aliqRatAjust", infoEstab.alteracao.dadosEstab.aliqGilrat.aliqRatAjust),

            // procAdmJudRat 0.1
            opElement("procAdmJudRat", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudRat.tpProc,
            new XElement(ns + "tpProc", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudRat.tpProc),
            new XElement(ns + "nrProc", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudRat.nrProc),
            new XElement(ns + "codSusp", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudRat.codSusp)),

            // procAdmJudFap 0.1
            opElement("procAdmJudFap", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudFap.tpProc,
            new XElement(ns + "tpProc", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudFap.tpProc),
            new XElement(ns + "nrProc", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudFap.nrProc),
            new XElement(ns + "codSusp", infoEstab.alteracao.dadosEstab.aliqGilrat.procAdmJudFap.codSusp))),

            // infoCaepf 0.1
            opElement("procAdmJudFap", infoEstab.alteracao.dadosEstab.infoCaepf.tpCaepf,
            new XElement(ns + "tpCaepf", infoEstab.alteracao.dadosEstab.infoCaepf.tpCaepf)),

            // infoCaepf 0.1
            opElement("procAdmJudFap", infoEstab.alteracao.dadosEstab.infoCaepf.tpCaepf,
            new XElement(ns + "tpCaepf", infoEstab.alteracao.dadosEstab.infoCaepf.tpCaepf)),

            // infoObra 0.1
            opElement("infoObra", infoEstab.alteracao.dadosEstab.infoObra.indSubstPatrObra,
            new XElement(ns + "indSubstPatrObra", infoEstab.alteracao.dadosEstab.infoObra.indSubstPatrObra)),

            // infoTrab
            new XElement(ns + "infoTrab",
            new XElement(ns + "regPt", infoEstab.alteracao.dadosEstab.infoTrab.regPt),

            // infoApr
            new XElement(ns + "infoApr",
            new XElement(ns + "contApr", infoEstab.alteracao.dadosEstab.infoTrab.infoApr.contApr),
            opTag("nrProcJud", infoEstab.alteracao.dadosEstab.infoTrab.infoApr.nrProcJud),
            opTag("contEntEd", infoEstab.alteracao.dadosEstab.infoTrab.infoApr.contEndEd),

            // infoEntEduc_alteracao 0.99
            from e in lInfoEntEduc_alteracao
            select e),

            // infoPCD 0.1
            opElement("infoPCD", infoEstab.alteracao.dadosEstab.infoTrab.infoPCD.contPCD,
            new XElement(ns + "contPCD", infoEstab.alteracao.dadosEstab.infoTrab.infoPCD.contPCD),
            opTag("nrProcJud", infoEstab.alteracao.dadosEstab.infoTrab.infoPCD.nrProcJud)))),

            // novaValidade 0.1
            opElement("novaValidade", infoEstab.alteracao.novaValidade.iniValid,
            new XElement(ns + "iniValid", infoEstab.alteracao.novaValidade.iniValid),
            opTag("fimValid", infoEstab.alteracao.novaValidade.fimValid))

            ), // alteracao

            // exclusao
            opElement("exclusao", infoEstab.exclusao.ideEstab.tpInsc,

            // ideEstab
            new XElement(ns + "ideEstab",
            new XElement(ns + "tpInsc", infoEstab.exclusao.ideEstab.tpInsc),
            new XElement(ns + "nrInsc", infoEstab.exclusao.ideEstab.nrInsc),
            new XElement(ns + "iniValid", infoEstab.exclusao.ideEstab.iniValid),
            opTag("fimValid", infoEstab.exclusao.ideEstab.fimValid)))

            );

            return x509.signXMLSHA256(xml, cert);
        }

        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

        #region infoEntEduc_inclusao   

        List<XElement> lInfoEntEduc_inclusao = new List<XElement>();
        public void add_infoEntEduc_inclusao() {

            lInfoEntEduc_inclusao.Add(
            new XElement(ns + "infoEntEduc",
            new XElement(ns + "nrInsc", infoEstab.inclusao.dadosEstab.infoTrab.infoApr.infoEntEduc)));

            infoEstab.inclusao.dadosEstab.infoTrab.infoApr.infoEntEduc = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoApr.sInfoEntEduc();
        }
        #endregion

        #region infoEntEduc_alteracao

        List<XElement> lInfoEntEduc_alteracao = new List<XElement>();
        public void add_infoEntEduc_alteracao() {

            lInfoEntEduc_alteracao.Add(
            new XElement(ns + "infoEntEduc",
            new XElement(ns + "nrInsc", infoEstab.alteracao.dadosEstab.infoTrab.infoApr.infoEntEduc)));

            infoEstab.alteracao.dadosEstab.infoTrab.infoApr.infoEntEduc = new sInfoEstab.sDadosEstab.sInfoTrab.sInfoApr.sInfoEntEduc();
        }
        #endregion

        #endregion

        #region ********************************************************************************************************************************************************* Structs

        public sInfoEstab infoEstab;
        public struct sInfoEstab {

            public sIncAlt inclusao, alteracao;
            public sExclusao exclusao;

            public struct sIncAlt {
                public sIdePeriodo novaValidade;
                public sIdeEstab ideEstab;
                public sDadosEstab dadosEstab;
            }

            public struct sIdeEstab {
                public string tpInsc;
                public string nrInsc, iniValid, fimValid;
            }

            public struct sDadosEstab {

                public string cnaePrep;
                public sAliqGilrat aliqGilrat;

                public struct sAliqGilrat {

                    public string aliqRat;
                    public string aliqRatAjust;
                    public string fap;
                    public sProcAdmJudFap procAdmJudFap;
                    public sProcAdmJudRat procAdmJudRat;

                    public struct sProcAdmJudRat {
                        public string tpProc, codSusp;
                        public string nrProc;
                    }

                    public struct sProcAdmJudFap {
                        public string tpProc, codSusp;
                        public string nrProc;
                    }
                }

                public sInfoCaepf infoCaepf;
                public struct sInfoCaepf { public string tpCaepf; }

                public sInfoObra infoObra;
                public struct sInfoObra { public string indSubstPatrObra; }

                public sInfoTrab infoTrab;
                public struct sInfoTrab {
                    public string regPt;

                    public sInfoApr infoApr;
                    public struct sInfoApr {
                        public string contApr;
                        public string nrProcJud, contEndEd;

                        public sInfoEntEduc infoEntEduc;
                        public struct sInfoEntEduc { public string nrInsc; }
                    }
                    public sInfoPCD infoPCD;
                    public struct sInfoPCD {
                        public string contPCD;
                        public string nrProcJud;
                    }
                }
            }
            public struct sExclusao { public sIdeEstab ideEstab; }
        }
    }

    #endregion
}