using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using eSocial.Controller;

# region Versão: v02_05_00 - substituida em 02/02/22
//namespace eSocial.Model.Eventos.XML {
//    public class s1000 : bEvento_XML {

//        public s1000(string sID) : base("evtInfoEmpregador", "infoEmpregador") {

//            id = sID;

//            infoEmpregador = new sInfoEmpregador();

//            infoEmpregador.inclusao = new sInfoEmpregador.sIncAlt();
//            infoEmpregador.inclusao.idePeriodo = new sIdePeriodo();
//            infoEmpregador.inclusao.infoCadastro = new sInfoCadastro();
//            infoEmpregador.inclusao.infoCadastro.dadosIsencao = new sInfoCadastro.sDadosIsencao();
//            infoEmpregador.inclusao.infoCadastro.contato = new sInfoCadastro.sContato();
//            infoEmpregador.inclusao.infoCadastro.infoOp = new sInfoCadastro.sInfoOp();
//            infoEmpregador.inclusao.infoCadastro.infoOp.infoEFR = new sInfoCadastro.sInfoOp.sInfoEFR();
//            infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte = new sInfoCadastro.sInfoOp.sInfoEnte();
//            infoEmpregador.inclusao.infoCadastro.infoOrgInternacional = new sInfoCadastro.sInfoOrgInternacional();
//            infoEmpregador.inclusao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
//            infoEmpregador.inclusao.infoCadastro.infoComplementares = new sInfoCadastro.sInfoComplementares();

//            infoEmpregador.alteracao = new sInfoEmpregador.sIncAlt();
//            infoEmpregador.alteracao.idePeriodo = new sIdePeriodo();
//            infoEmpregador.alteracao.infoCadastro = new sInfoCadastro();
//            infoEmpregador.alteracao.infoCadastro.dadosIsencao = new sInfoCadastro.sDadosIsencao();
//            infoEmpregador.alteracao.infoCadastro.contato = new sInfoCadastro.sContato();
//            infoEmpregador.alteracao.infoCadastro.infoOp = new sInfoCadastro.sInfoOp();
//            infoEmpregador.alteracao.infoCadastro.infoOp.infoEFR = new sInfoCadastro.sInfoOp.sInfoEFR();
//            infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte = new sInfoCadastro.sInfoOp.sInfoEnte();
//            infoEmpregador.alteracao.infoCadastro.infoOrgInternacional = new sInfoCadastro.sInfoOrgInternacional();
//            infoEmpregador.alteracao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
//            infoEmpregador.alteracao.infoCadastro.infoComplementares = new sInfoCadastro.sInfoComplementares();

//            infoEmpregador.alteracao.novaValidade = new sIdePeriodo();

//            infoEmpregador.exclusao = new sIdePeriodo();
//        }

//        public override XElement genSignedXML(X509Certificate2 cert) {

//            // ideEvento
//            xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
//            new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
//            new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
//            new XElement(ns + "verProc", ideEvento.verProc));

//            // ideEmpregador
//            xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
//            new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
//            new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

//            // infoEmpregador
//            xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

//            // inclusao 0.1
//            opElement("inclusao", infoEmpregador.inclusao.idePeriodo.iniValid,

//            // idePeriodo
//            new XElement(ns + "idePeriodo",
//            new XElement(ns + "iniValid", infoEmpregador.inclusao.idePeriodo.iniValid),
//            opTag("fimValid", infoEmpregador.inclusao.idePeriodo.fimValid)),

//            // infoCadastro
//            new XElement(ns + "infoCadastro",
//            new XElement(ns + "nmRazao", infoEmpregador.inclusao.infoCadastro.nmRazao),
//            new XElement(ns + "classTrib", infoEmpregador.inclusao.infoCadastro.classTrib),
//            opTag("natJurid", infoEmpregador.inclusao.infoCadastro.natJurid),
//            opTag("indCoop", infoEmpregador.inclusao.infoCadastro.indCoop),
//            opTag("indConstr", infoEmpregador.inclusao.infoCadastro.indConstr),
//            new XElement(ns + "indDesFolha", infoEmpregador.inclusao.infoCadastro.indDesFolha),
//            opTag("indOpcCP", infoEmpregador.inclusao.infoCadastro.indOpcCP),
//            new XElement(ns + "indOptRegEletron", infoEmpregador.inclusao.infoCadastro.indOptRegEletron),
//            opTag("indEntEd", infoEmpregador.inclusao.infoCadastro.indEntEd),
//            new XElement(ns + "indEtt", infoEmpregador.inclusao.infoCadastro.indEtt),
//            opTag("nrRegEtt", infoEmpregador.inclusao.infoCadastro.nrRegEtt),

//            // dadosIsencao 0.1
//            opElement("dadosIsencao", infoEmpregador.inclusao.infoCadastro.dadosIsencao.ideMinLei,
//            new XElement(ns + "ideMinLei", infoEmpregador.inclusao.infoCadastro.dadosIsencao.ideMinLei),
//            new XElement(ns + "nrCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.nrCertif),
//            new XElement(ns + "dtEmisCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtEmisCertif),
//            new XElement(ns + "dtVencCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtVencCertif),
//            opTag("nrProtRenov", infoEmpregador.inclusao.infoCadastro.dadosIsencao.nrProtRenov),
//            opTag("dtProtRenov", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtProtRenov),
//            opTag("dtDou", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtDou),
//            opTag("pagDou", infoEmpregador.inclusao.infoCadastro.dadosIsencao.pagDou)),

//            // contato
//            new XElement(ns + "contato",
//            new XElement(ns + "nmCtt", infoEmpregador.inclusao.infoCadastro.contato.nmCtt),
//            new XElement(ns + "cpfCtt", infoEmpregador.inclusao.infoCadastro.contato.cpfCtt),
//            opTag("foneFixo", infoEmpregador.inclusao.infoCadastro.contato.foneFixo),
//            opTag("foneCel", infoEmpregador.inclusao.infoCadastro.contato.foneCel),
//            opTag("email", infoEmpregador.inclusao.infoCadastro.contato.email)),

//            // infoOp 0.1
//            opElement("infoOP", infoEmpregador.inclusao.infoCadastro.infoOp.nrSiafi,
//            new XElement(ns + "nrSiafi", infoEmpregador.inclusao.infoCadastro.infoOp.nrSiafi),

//             // infoEFR 0.1
//             opElement("infoEFR", infoEmpregador.inclusao.infoCadastro.infoOp.infoEFR.ideEFR,
//             new XElement(ns + "ideEFR", infoEmpregador.inclusao.infoCadastro.infoOp.infoEFR.ideEFR),
//             opTag("cnpjEFR", infoEmpregador.inclusao.infoCadastro.infoOp.infoEFR.cnpjEFR)),

//             // infoEnte
//             new XElement(ns + "infoEnte",
//             new XElement(ns + "nmEnte", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.nmEnte),
//             new XElement(ns + "uf", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.uf),
//             opTag("codMunic", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.codMunic),
//             new XElement(ns + "indRPPS", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.indRPPS),
//             new XElement(ns + "subteto", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.subteto),
//             new XElement(ns + "vrSubteto", infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte.vrSubteto))),

//            // infoOrgInternacional 0.1
//            opElement("indAcordoIsenMulta", infoEmpregador.inclusao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta,
//            new XElement(ns + "indAcordoIsenMulta", infoEmpregador.inclusao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta)),

//            // softwareHouse 0.99
//            from e in lSoftwareHouse_inclusao
//            select e,

//         // infoComplementares
//         new XElement(ns + "infoComplementares",

//         // situacaoPJ 0.1
//         opElement("situacaoPJ", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPJ.indSitPJ,
//         new XElement(ns + "indSitPJ", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPJ.indSitPJ)),

//          // situacaoPF 0.1
//          opElement("situacaoPF", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPF.indSitPF,
//          new XElement(ns + "indSitPF", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPF.indSitPF)))

//          )), // inclusao

//          // alteracao 0.1
//          opElement("alteracao", infoEmpregador.alteracao.idePeriodo.iniValid,

//          // idePeriodo
//          new XElement(ns + "idePeriodo",
//          new XElement(ns + "iniValid", infoEmpregador.alteracao.idePeriodo.iniValid),
//          opTag("fimValid", infoEmpregador.alteracao.idePeriodo.fimValid)),

//          // infoCadastro
//          new XElement(ns + "infoCadastro",
//          new XElement(ns + "nmRazao", infoEmpregador.alteracao.infoCadastro.nmRazao),
//          new XElement(ns + "classTrib", infoEmpregador.alteracao.infoCadastro.classTrib),
//          opTag("natJurid", infoEmpregador.alteracao.infoCadastro.natJurid),
//          opTag("indCoop", infoEmpregador.alteracao.infoCadastro.indCoop),
//          opTag("indConstr", infoEmpregador.alteracao.infoCadastro.indConstr), // novo 
//          new XElement(ns + "indDesFolha", infoEmpregador.alteracao.infoCadastro.indDesFolha),
//          opTag("indOpcCP", infoEmpregador.alteracao.infoCadastro.indOpcCP),
//          new XElement(ns + "indOptRegEletron", infoEmpregador.alteracao.infoCadastro.indOptRegEletron),
//          opTag("indEntEd", infoEmpregador.alteracao.infoCadastro.indEntEd),
//          new XElement(ns + "indEtt", infoEmpregador.alteracao.infoCadastro.indEtt),
//          opTag("nrRegEtt", infoEmpregador.alteracao.infoCadastro.nrRegEtt),

//          // dadosIsencao 0.1
//          opElement("dadosIsencao", infoEmpregador.alteracao.infoCadastro.dadosIsencao.ideMinLei,
//          new XElement(ns + "ideMinLei", infoEmpregador.alteracao.infoCadastro.dadosIsencao.ideMinLei),
//          new XElement(ns + "nrCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.nrCertif),
//          new XElement(ns + "dtEmisCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtEmisCertif),
//          new XElement(ns + "dtVencCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtVencCertif),
//          opTag("nrProtRenov", infoEmpregador.alteracao.infoCadastro.dadosIsencao.nrProtRenov),
//          opTag("dtProtRenov", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtProtRenov),
//          opTag("dtDou", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtDou),
//          opTag("pagDou", infoEmpregador.alteracao.infoCadastro.dadosIsencao.pagDou)),

//          // contato
//          new XElement(ns + "contato",
//          new XElement(ns + "nmCtt", infoEmpregador.alteracao.infoCadastro.contato.nmCtt),
//          new XElement(ns + "cpfCtt", infoEmpregador.alteracao.infoCadastro.contato.cpfCtt),
//          opTag("foneFixo", infoEmpregador.alteracao.infoCadastro.contato.foneFixo),
//          opTag("foneCel", infoEmpregador.alteracao.infoCadastro.contato.foneCel),
//          opTag("email", infoEmpregador.alteracao.infoCadastro.contato.email)),

//          // infoOp 0.1
//          opElement("infoOP", infoEmpregador.alteracao.infoCadastro.infoOp.nrSiafi,
//          new XElement(ns + "nrSiafi", infoEmpregador.alteracao.infoCadastro.infoOp.nrSiafi),

//          // infoEFR 0.1
//          opElement("infoEFR", infoEmpregador.alteracao.infoCadastro.infoOp.infoEFR.ideEFR,
//          new XElement(ns + "ideEFR", infoEmpregador.alteracao.infoCadastro.infoOp.infoEFR.ideEFR),
//          opTag("cnpjEFR", infoEmpregador.alteracao.infoCadastro.infoOp.infoEFR.cnpjEFR)),

//          // infoEnte 0.1
//          opElement("infoEnte", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.nmEnte,
//          new XElement(ns + "nmEnte", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.nmEnte),
//          new XElement(ns + "uf", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.uf),
//          opTag("codMunic", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.codMunic),
//          new XElement(ns + "indRPPS", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.indRPPS),
//          new XElement(ns + "subteto", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.subteto),
//          new XElement(ns + "vrSubteto", infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte.vrSubteto))),

//          // infoOrgInternacional 0.1
//          opElement("infoOrgInternacional", infoEmpregador.alteracao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta,
//          new XElement(ns + "indAcordoIsenMulta", infoEmpregador.alteracao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta)),

//          // softwareHouse 0.99
//          from e in lSoftwareHouse_alteracao
//          select e,

//          // infoComplementares 1.1
//          new XElement(ns + "infoComplementares",

//          // situacaoPJ 0.1
//          opElement("situacaoPJ", infoEmpregador.alteracao.infoCadastro.infoComplementares.situacaoPJ.indSitPJ,
//          new XElement(ns + "indSitPJ", infoEmpregador.alteracao.infoCadastro.infoComplementares.situacaoPJ.indSitPJ)),

//          // situacaoPF 0.1
//          opElement("situacaoPF", infoEmpregador.alteracao.infoCadastro.infoComplementares.situacaoPF.indSitPF,
//          new XElement(ns + "indSitPF", infoEmpregador.alteracao.infoCadastro.infoComplementares.situacaoPF.indSitPF)))),

//          // novaValidade 0.1
//          opElement("novaValidade", infoEmpregador.alteracao.novaValidade.iniValid,
//          new XElement(ns + "iniValid", infoEmpregador.alteracao.novaValidade.iniValid),
//          opTag("fimValid", infoEmpregador.alteracao.novaValidade.fimValid))

//          ), // alteracao

//          // exclusao 0.1
//          opElement("exclusao", infoEmpregador.exclusao.iniValid,

//          // idePeriodo
//          new XElement(ns + "idePeriodo",
//          new XElement(ns + "iniValid", infoEmpregador.exclusao.iniValid),
//          opTag("fimValid", infoEmpregador.inclusao.idePeriodo.fimValid)))

//          );
//            return x509.signXMLSHA256(xml, cert);
//        }

//        #region ******************************************************************************************************************************************* Tags com +1 ocorrência

//        #region softwareHouse_inclusao   

//        List<XElement> lSoftwareHouse_inclusao = new List<XElement>();
//        public void add_softwareHouse_inclusao() {

//            lSoftwareHouse_inclusao.Add(
//            new XElement(ns + "softwareHouse",
//            new XElement(ns + "cnpjSoftHouse", infoEmpregador.inclusao.infoCadastro.softwareHouse.cnpjSoftHouse),
//            new XElement(ns + "nmRazao", infoEmpregador.inclusao.infoCadastro.softwareHouse.nmRazao),
//            new XElement(ns + "nmCont", infoEmpregador.inclusao.infoCadastro.softwareHouse.nmCont),
//            new XElement(ns + "telefone", infoEmpregador.inclusao.infoCadastro.softwareHouse.telefone),
//            opTag("email", infoEmpregador.inclusao.infoCadastro.softwareHouse.email)));

//            infoEmpregador.inclusao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
//        }
//        #endregion

//        #region softwareHouse_alteracao 

//        List<XElement> lSoftwareHouse_alteracao = new List<XElement>();
//        public void add_softwareHouse_alteracao() {

//            lSoftwareHouse_alteracao.Add(
//            new XElement(ns + "softwareHouse",
//            new XElement(ns + "cnpjSoftHouse", infoEmpregador.alteracao.infoCadastro.softwareHouse.cnpjSoftHouse),
//            new XElement(ns + "nmRazao", infoEmpregador.alteracao.infoCadastro.softwareHouse.nmRazao),
//            new XElement(ns + "nmCont", infoEmpregador.alteracao.infoCadastro.softwareHouse.nmCont),
//            new XElement(ns + "telefone", infoEmpregador.alteracao.infoCadastro.softwareHouse.telefone),
//            opTag("email", infoEmpregador.alteracao.infoCadastro.softwareHouse.email)));

//            infoEmpregador.alteracao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
//        }
//        #endregion

//        #endregion

//        #region ********************************************************************************************************************************************************* Structs

//        public sInfoEmpregador infoEmpregador;
//        public struct sInfoEmpregador {

//            public sIncAlt inclusao, alteracao;
//            public sIdePeriodo exclusao;
//            public struct sIncAlt {
//                public sIdePeriodo idePeriodo, novaValidade;
//                public sInfoCadastro infoCadastro;
//            }
//        }

//        public struct sInfoCadastro {
//            public string nmRazao, classTrib, multTabRubricas, indEntEd, indEtt, natJurid;
//            public string indCoop, indConstr, indDesFolha, indOpcCP, indOptRegEletron, nrRegEtt;

//            public sDadosIsencao dadosIsencao;
//            public struct sDadosIsencao {
//                public string ideMinLei, nrCertif, nrProtRenov;
//                public string dtEmisCertif, dtVencCertif, dtProtRenov, dtDou;
//                public string pagDou;
//            }

//            public sContato contato;
//            public struct sContato {
//                public string nmCtt, cpfCtt, foneFixo, foneCel, email;
//            }

//            public sInfoOp infoOp;
//            public struct sInfoOp {

//                public string nrSiafi;

//                public sInfoEFR infoEFR;
//                public struct sInfoEFR { public string ideEFR, cnpjEFR; }

//                public sInfoEnte infoEnte;
//                public struct sInfoEnte {
//                    public string nmEnte, uf, indRPPS, subteto, codMunic;
//                    public string vrSubteto;
//                }
//            }

//            public sInfoOrgInternacional infoOrgInternacional;
//            public struct sInfoOrgInternacional { public string indAcordoIsenMulta; }

//            public sSoftwareHouse softwareHouse;
//            public struct sSoftwareHouse { public string cnpjSoftHouse, nmRazao, nmCont, telefone, email; }

//            public sInfoComplementares infoComplementares;
//            public struct sInfoComplementares {

//                public sSituacaoPJ situacaoPJ;
//                public struct sSituacaoPJ { public string indSitPJ; }
//                public sSituacaoPF situacaoPF;
//                public struct sSituacaoPF { public string indSitPF; }
//            }
//        }
//        #endregion
//    }
//}

#endregion

namespace eSocial.Model.Eventos.XML
{
   public class s1000 : bEvento_XML
   {

      public s1000(string sID) : base("evtInfoEmpregador", "infoEmpregador", "v_S_01_02_00")
      {

         id = sID;

         infoEmpregador = new sInfoEmpregador();

         infoEmpregador.inclusao = new sInfoEmpregador.sIncAlt();
         infoEmpregador.inclusao.idePeriodo = new sIdePeriodo();
         infoEmpregador.inclusao.infoCadastro = new sInfoCadastro();
         infoEmpregador.inclusao.infoCadastro.dadosIsencao = new sInfoCadastro.sDadosIsencao();
         infoEmpregador.inclusao.infoCadastro.contato = new sInfoCadastro.sContato();
         infoEmpregador.inclusao.infoCadastro.infoOp = new sInfoCadastro.sInfoOp();
         infoEmpregador.inclusao.infoCadastro.infoOp.infoEFR = new sInfoCadastro.sInfoOp.sInfoEFR();
         infoEmpregador.inclusao.infoCadastro.infoOp.infoEnte = new sInfoCadastro.sInfoOp.sInfoEnte();
         infoEmpregador.inclusao.infoCadastro.infoOrgInternacional = new sInfoCadastro.sInfoOrgInternacional();
         infoEmpregador.inclusao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
         infoEmpregador.inclusao.infoCadastro.infoComplementares = new sInfoCadastro.sInfoComplementares();

         infoEmpregador.alteracao = new sInfoEmpregador.sIncAlt();
         infoEmpregador.alteracao.idePeriodo = new sIdePeriodo();
         infoEmpregador.alteracao.infoCadastro = new sInfoCadastro();
         infoEmpregador.alteracao.infoCadastro.dadosIsencao = new sInfoCadastro.sDadosIsencao();
         infoEmpregador.alteracao.infoCadastro.contato = new sInfoCadastro.sContato();
         infoEmpregador.alteracao.infoCadastro.infoOp = new sInfoCadastro.sInfoOp();
         infoEmpregador.alteracao.infoCadastro.infoOp.infoEFR = new sInfoCadastro.sInfoOp.sInfoEFR();
         infoEmpregador.alteracao.infoCadastro.infoOp.infoEnte = new sInfoCadastro.sInfoOp.sInfoEnte();
         infoEmpregador.alteracao.infoCadastro.infoOrgInternacional = new sInfoCadastro.sInfoOrgInternacional();
         infoEmpregador.alteracao.infoCadastro.softwareHouse = new sInfoCadastro.sSoftwareHouse();
         infoEmpregador.alteracao.infoCadastro.infoComplementares = new sInfoCadastro.sInfoComplementares();

         infoEmpregador.alteracao.novaValidade = new sIdePeriodo();

         infoEmpregador.exclusao = new sIdePeriodo();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {
         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(
         new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
         new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
         new XElement(ns + "verProc", ideEvento.verProc));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // infoEmpregador
         xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

         // inclusao 0.1
         opElement("inclusao", infoEmpregador.inclusao.infoCadastro.classTrib,

         // idePeriodo
         //new XElement(ns + "idePeriodo",
         opElement("idePeriodo", infoEmpregador.inclusao.idePeriodo.iniValid,
         new XElement(ns + "iniValid", infoEmpregador.inclusao.idePeriodo.iniValid),
         opTag("fimValid", infoEmpregador.inclusao.idePeriodo.fimValid)),

         // infoCadastro
         new XElement(ns + "infoCadastro",
         new XElement(ns + "classTrib", infoEmpregador.inclusao.infoCadastro.classTrib),
         opTag("indCoop", infoEmpregador.inclusao.infoCadastro.indCoop),
         opTag("indConstr", infoEmpregador.inclusao.infoCadastro.indConstr),
         new XElement(ns + "indDesFolha", infoEmpregador.inclusao.infoCadastro.indDesFolha),
         opTag("indOpcCP", infoEmpregador.inclusao.infoCadastro.indOpcCP),
         opTag("indPorte", infoEmpregador.inclusao.infoCadastro.indPorte),
         new XElement(ns + "indOptRegEletron", infoEmpregador.inclusao.infoCadastro.indOptRegEletron),
         opTag("cnpjEFR", infoEmpregador.inclusao.infoCadastro.cnpjEFR),
         opTag("dtTrans11096", infoEmpregador.inclusao.infoCadastro.dtTrans11096),
         opTag("indTribFolhaPisCofins", infoEmpregador.inclusao.infoCadastro.indTribFolhaPisCofins),

         // dadosIsencao 0.1
         opElement("dadosIsencao", infoEmpregador.inclusao.infoCadastro.dadosIsencao.ideMinLei,
         new XElement(ns + "ideMinLei", infoEmpregador.inclusao.infoCadastro.dadosIsencao.ideMinLei),
         new XElement(ns + "nrCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.nrCertif),
         new XElement(ns + "dtEmisCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtEmisCertif),
         new XElement(ns + "dtVencCertif", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtVencCertif),
         opTag("nrProtRenov", infoEmpregador.inclusao.infoCadastro.dadosIsencao.nrProtRenov),
         opTag("dtProtRenov", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtProtRenov),
         opTag("dtDou", infoEmpregador.inclusao.infoCadastro.dadosIsencao.dtDou),
         opTag("pagDou", infoEmpregador.inclusao.infoCadastro.dadosIsencao.pagDou)),

         // infoOrgInternacional 0.1
         opElement("indAcordoIsenMulta", infoEmpregador.inclusao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta,
         new XElement(ns + "indAcordoIsenMulta", infoEmpregador.inclusao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta)),

         // situacaoPF 0.1
         opElement("situacaoPF", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPF.indSitPF,
         new XElement(ns + "indSitPF", infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPF.indSitPF)))

         ), // inclusao

         // alteracao 0.1
         opElement("alteracao", infoEmpregador.alteracao.infoCadastro.classTrib,

          // idePeriodo
          //new XElement(ns + "idePeriodo",
          opElement("idePeriodo", infoEmpregador.alteracao.idePeriodo.iniValid,
          new XElement(ns + "iniValid", infoEmpregador.alteracao.idePeriodo.iniValid),
          opTag("fimValid", infoEmpregador.alteracao.idePeriodo.fimValid)),

          // infoCadastro
          new XElement(ns + "infoCadastro",
          new XElement(ns + "classTrib", infoEmpregador.alteracao.infoCadastro.classTrib),
          opTag("indCoop", infoEmpregador.alteracao.infoCadastro.indCoop),
          opTag("indConstr", infoEmpregador.alteracao.infoCadastro.indConstr), // novo 
          new XElement(ns + "indDesFolha", infoEmpregador.alteracao.infoCadastro.indDesFolha),
          opTag("indOpcCP", infoEmpregador.alteracao.infoCadastro.indOpcCP),
          opTag("indPorte", infoEmpregador.alteracao.infoCadastro.indPorte),
          new XElement(ns + "indOptRegEletron", infoEmpregador.alteracao.infoCadastro.indOptRegEletron),
          opTag("cnpjEFR", infoEmpregador.alteracao.infoCadastro.cnpjEFR),
          opTag("dtTrans11096", infoEmpregador.alteracao.infoCadastro.dtTrans11096),
          opTag("indTribFolhaPisCofins", infoEmpregador.alteracao.infoCadastro.indTribFolhaPisCofins),

          // dadosIsencao 0.1
          opElement("dadosIsencao", infoEmpregador.alteracao.infoCadastro.dadosIsencao.ideMinLei,
          new XElement(ns + "ideMinLei", infoEmpregador.alteracao.infoCadastro.dadosIsencao.ideMinLei),
          new XElement(ns + "nrCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.nrCertif),
          new XElement(ns + "dtEmisCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtEmisCertif),
          new XElement(ns + "dtVencCertif", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtVencCertif),
          opTag("nrProtRenov", infoEmpregador.alteracao.infoCadastro.dadosIsencao.nrProtRenov),
          opTag("dtProtRenov", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtProtRenov),
          opTag("dtDou", infoEmpregador.alteracao.infoCadastro.dadosIsencao.dtDou),
          opTag("pagDou", infoEmpregador.alteracao.infoCadastro.dadosIsencao.pagDou)),

          // infoOrgInternacional 0.1
          opElement("infoOrgInternacional", infoEmpregador.alteracao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta,
          new XElement(ns + "indAcordoIsenMulta", infoEmpregador.alteracao.infoCadastro.infoOrgInternacional.indAcordoIsenMulta))),

          // novaValidade 0.1
          opElement("novaValidade", infoEmpregador.alteracao.novaValidade.iniValid,
          new XElement(ns + "iniValid", infoEmpregador.alteracao.novaValidade.iniValid),
          opTag("fimValid", infoEmpregador.alteracao.novaValidade.fimValid))

          ), // alteracao

          // exclusao 0.1
          opElement("exclusao", infoEmpregador.exclusao.iniValid,

          // idePeriodo
          new XElement(ns + "idePeriodo",
          new XElement(ns + "iniValid", infoEmpregador.exclusao.iniValid),
          opTag("fimValid", infoEmpregador.inclusao.idePeriodo.fimValid)))

          );
       
          return x509.signXMLSHA256(xml, cert);
      }
            
      #region ********************************************************************************************************************************************************* Structs

      public sInfoEmpregador infoEmpregador;
      public struct sInfoEmpregador
      {

         public sIncAlt inclusao, alteracao;
         public sIdePeriodo exclusao;
         public struct sIncAlt
         {
            public sIdePeriodo idePeriodo, novaValidade;
            public sInfoCadastro infoCadastro;
         }
      }

      public struct sInfoCadastro
      {
         public string nmRazao, classTrib, multTabRubricas, indEntEd, indEtt, natJurid;
         public string indCoop, indConstr, indDesFolha, indOpcCP, indOptRegEletron, nrRegEtt;
         public string indPorte, cnpjEFR, dtTrans11096, indTribFolhaPisCofins;

         public sDadosIsencao dadosIsencao;
         public struct sDadosIsencao
         {
            public string ideMinLei, nrCertif, nrProtRenov;
            public string dtEmisCertif, dtVencCertif, dtProtRenov, dtDou;
            public string pagDou;
         }

         public sContato contato;
         public struct sContato
         {
            public string nmCtt, cpfCtt, foneFixo, foneCel, email;
         }

         public sInfoOp infoOp;
         public struct sInfoOp
         {

            public string nrSiafi;

            public sInfoEFR infoEFR;
            public struct sInfoEFR { public string ideEFR, cnpjEFR; }

            public sInfoEnte infoEnte;
            public struct sInfoEnte
            {
               public string nmEnte, uf, indRPPS, subteto, codMunic;
               public string vrSubteto;
            }
         }

         public sInfoOrgInternacional infoOrgInternacional;
         public struct sInfoOrgInternacional { public string indAcordoIsenMulta; }

         public sSoftwareHouse softwareHouse;
         public struct sSoftwareHouse { public string cnpjSoftHouse, nmRazao, nmCont, telefone, email; }

         public sInfoComplementares infoComplementares;
         public struct sInfoComplementares
         {

            public sSituacaoPJ situacaoPJ;
            public struct sSituacaoPJ { public string indSitPJ; }
            public sSituacaoPF situacaoPF;
            public struct sSituacaoPF { public string indSitPF; }
         }
      }
      #endregion
   }
}