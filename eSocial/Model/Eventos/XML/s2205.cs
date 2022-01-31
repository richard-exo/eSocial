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
   public class s2205 : bEvento_XML {

      public s2205(string sID) : base("evtAltCadastral") {

         id = sID;

         //ideEvento = new sIdeEvento();
         ideTrabalhador = new sIdeTrabalhador();

         alteracao = new sAlteracao();

         alteracao.dadosTrabalhador.nascimento = new sAlteracao.sDadosTrabalhador.sNascimento();

         alteracao.dadosTrabalhador.documentos = new sAlteracao.sDadosTrabalhador.sDocumentos();
         alteracao.dadosTrabalhador.documentos.CTPS = new sAlteracao.sDadosTrabalhador.sDocumentos.sCTPS();
         alteracao.dadosTrabalhador.documentos.RIC = new sAlteracao.sDadosTrabalhador.sDocumentos.sRIC();
         alteracao.dadosTrabalhador.documentos.RG = new sAlteracao.sDadosTrabalhador.sDocumentos.sRG();
         alteracao.dadosTrabalhador.documentos.RNE = new sAlteracao.sDadosTrabalhador.sDocumentos.sRNE();
         alteracao.dadosTrabalhador.documentos.OC = new sAlteracao.sDadosTrabalhador.sDocumentos.sOC();
         alteracao.dadosTrabalhador.documentos.CNH = new sAlteracao.sDadosTrabalhador.sDocumentos.sCNH();

         alteracao.dadosTrabalhador.endereco = new sAlteracao.sDadosTrabalhador.sEndereco();
         alteracao.dadosTrabalhador.endereco.brasil = new sAlteracao.sDadosTrabalhador.sEndereco.sBrasil();
         alteracao.dadosTrabalhador.endereco.exterior = new sAlteracao.sDadosTrabalhador.sEndereco.sExterior();
         alteracao.dadosTrabalhador.trabEstrangeiro = new sAlteracao.sDadosTrabalhador.sTrabEstrangeiro();
         alteracao.dadosTrabalhador.infoDeficiencia = new sAlteracao.sDadosTrabalhador.sInfoDeficiencia();
         alteracao.dadosTrabalhador.dependente = new sAlteracao.sDadosTrabalhador.sDependente();
         alteracao.dadosTrabalhador.aposentadoria = new sAlteracao.sDadosTrabalhador.sAposentadoria();
         alteracao.dadosTrabalhador.contato = new sAlteracao.sDadosTrabalhador.sContato();
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

         // ideTrabalhador
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideTrabalhador",
         new XElement(ns + "cpfTrab", ideTrabalhador.cpfTrab)));

         // alteracao
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "alteracao",
         new XElement(ns + "dtAlteracao", alteracao.dtAlteracao),

         // dadosTrabalhador   
         new XElement(ns + "dadosTrabalhador",
         opTag("nisTrab", alteracao.dadosTrabalhador.nisTrab),
         new XElement(ns + "nmTrab", alteracao.dadosTrabalhador.nmTrab),
         new XElement(ns + "sexo", alteracao.dadosTrabalhador.sexo),
         new XElement(ns + "racaCor", alteracao.dadosTrabalhador.racaCor),
         opTag("estCiv", alteracao.dadosTrabalhador.estCiv),
         new XElement(ns + "grauInstr", alteracao.dadosTrabalhador.grauInstr),
         opTag("nmSoc", alteracao.dadosTrabalhador.nmSoc),

         // nascimento   
         new XElement(ns + "nascimento",
         new XElement(ns + "dtNascto", alteracao.dadosTrabalhador.nascimento.dtNascto),
         new XElement(ns + "codMunic", alteracao.dadosTrabalhador.nascimento.codMunic),
         new XElement(ns + "uf", alteracao.dadosTrabalhador.nascimento.uf),
         new XElement(ns + "paisNascto", alteracao.dadosTrabalhador.nascimento.paisNascto),
         new XElement(ns + "paisNac", alteracao.dadosTrabalhador.nascimento.paisNac),
         new XElement(ns + "nmMae", alteracao.dadosTrabalhador.nascimento.nmMae),
         opTag("nmPai", alteracao.dadosTrabalhador.nascimento.nmPai)),

         // documentos 0.1
         opElement("documentos", alteracao.dadosTrabalhador.documentos.CTPS.nrCtps,

         // CTPS 0.1
         opElement("CTPS", alteracao.dadosTrabalhador.documentos.CTPS.nrCtps,
         new XElement(ns + "nrCtps", alteracao.dadosTrabalhador.documentos.CTPS.nrCtps),
         new XElement(ns + "serieCtps", alteracao.dadosTrabalhador.documentos.CTPS.serieCtps),
         new XElement(ns + "ufCtps", alteracao.dadosTrabalhador.documentos.CTPS.ufCtps)),

         // RIC 0.1
         opElement("RIC", alteracao.dadosTrabalhador.documentos.RIC.nrRic,
         new XElement(ns + "nrRic", alteracao.dadosTrabalhador.documentos.RIC.nrRic),
         new XElement(ns + "orgaoEmissor", alteracao.dadosTrabalhador.documentos.RIC.orgaoEmissor),
         opTag("dtExped", alteracao.dadosTrabalhador.documentos.RIC.dtExped)),

         // RG 0.1
         opElement("RG", alteracao.dadosTrabalhador.documentos.RG.nrRg,
         new XElement(ns + "nrRg", alteracao.dadosTrabalhador.documentos.RG.nrRg),
         new XElement(ns + "orgaoEmissor", alteracao.dadosTrabalhador.documentos.RG.orgaoEmissor),
         opTag("dtExped", alteracao.dadosTrabalhador.documentos.RG.dtExped)),

         // RNE 0.1
         opElement("RNE", alteracao.dadosTrabalhador.documentos.RNE.nrRne,
         new XElement(ns + "nrRne", alteracao.dadosTrabalhador.documentos.RNE.nrRne),
         new XElement(ns + "orgaoEmissor", alteracao.dadosTrabalhador.documentos.RNE.orgaoEmissor),
         opTag("dtExped", alteracao.dadosTrabalhador.documentos.RNE.dtExped)),

         // OC 0.1
         opElement("OC", alteracao.dadosTrabalhador.documentos.OC.nrOc,
         new XElement(ns + "nrOc", alteracao.dadosTrabalhador.documentos.OC.nrOc),
         new XElement(ns + "orgaoEmissor", alteracao.dadosTrabalhador.documentos.OC.orgaoEmissor),
         opTag("dtExped", alteracao.dadosTrabalhador.documentos.OC.dtExped),
         opTag("dtValid", alteracao.dadosTrabalhador.documentos.OC.dtValid)),

         // CNH 0.1
         opElement("CNH", alteracao.dadosTrabalhador.documentos.CNH.nrRegCnh,
         new XElement(ns + "nrRegCnh", alteracao.dadosTrabalhador.documentos.CNH.nrRegCnh),
         opTag("dtExped", alteracao.dadosTrabalhador.documentos.CNH.dtExped),
         new XElement(ns + "ufCnh", alteracao.dadosTrabalhador.documentos.CNH.ufCnh),
         new XElement(ns + "dtValid", alteracao.dadosTrabalhador.documentos.CNH.dtValid),
         opTag("dtPriHab", alteracao.dadosTrabalhador.documentos.CNH.dtPriHab),
         new XElement(ns + "categoriaCnh", alteracao.dadosTrabalhador.documentos.CNH.categoriaCnh))

         ), //documentos

         // endereco
         new XElement(ns + "endereco",

         // brasil 0.1
         opElement("brasil", alteracao.dadosTrabalhador.endereco.brasil.tpLograd,
         new XElement(ns + "tpLograd", alteracao.dadosTrabalhador.endereco.brasil.tpLograd),
         new XElement(ns + "dscLograd", alteracao.dadosTrabalhador.endereco.brasil.dscLograd),
         new XElement(ns + "nrLograd", alteracao.dadosTrabalhador.endereco.brasil.nrLograd),
         opTag("complemento", alteracao.dadosTrabalhador.endereco.brasil.complemento),
         opTag("bairro", alteracao.dadosTrabalhador.endereco.brasil.bairro),
         new XElement(ns + "cep", alteracao.dadosTrabalhador.endereco.brasil.cep),
         new XElement(ns + "codMunic", alteracao.dadosTrabalhador.endereco.brasil.codMunic),
         new XElement(ns + "uf", alteracao.dadosTrabalhador.endereco.brasil.uf)),

         // exterior 0.1
         opElement("exterior", alteracao.dadosTrabalhador.endereco.exterior.paisResid,
         new XElement(ns + "paisResid", alteracao.dadosTrabalhador.endereco.exterior.paisResid),
         new XElement(ns + "dscLograd", alteracao.dadosTrabalhador.endereco.exterior.dscLograd),
         new XElement(ns + "nrLograd", alteracao.dadosTrabalhador.endereco.exterior.nrLograd),
         opTag("complemento", alteracao.dadosTrabalhador.endereco.exterior.complemento),
         opTag("bairro", alteracao.dadosTrabalhador.endereco.exterior.bairro),
         new XElement(ns + "nmCid", alteracao.dadosTrabalhador.endereco.exterior.nmCid),
         opTag("codPostal", alteracao.dadosTrabalhador.endereco.exterior.codPostal))

         ), // endereco

         // trabEstrangeiro 0.1
         opElement("trabEstrangeiro", alteracao.dadosTrabalhador.trabEstrangeiro.dtChegada,
         new XElement(ns + "dtChegada", alteracao.dadosTrabalhador.trabEstrangeiro.dtChegada),
         new XElement(ns + "classTrabEstrang", alteracao.dadosTrabalhador.trabEstrangeiro.classTrabEstrang),
         new XElement(ns + "casadoBr", alteracao.dadosTrabalhador.trabEstrangeiro.casadoBr),
         new XElement(ns + "filhosBr", alteracao.dadosTrabalhador.trabEstrangeiro.filhosBr)),

         // infoDeficiencia 0.1
         opElement("infoDeficiencia", alteracao.dadosTrabalhador.infoDeficiencia.defFisica,
         new XElement(ns + "defFisica", alteracao.dadosTrabalhador.infoDeficiencia.defFisica),
         new XElement(ns + "defVisual", alteracao.dadosTrabalhador.infoDeficiencia.defVisual),
         new XElement(ns + "defAuditiva", alteracao.dadosTrabalhador.infoDeficiencia.defAuditiva),
         new XElement(ns + "defMental", alteracao.dadosTrabalhador.infoDeficiencia.defMental),
         new XElement(ns + "defIntelectual", alteracao.dadosTrabalhador.infoDeficiencia.defIntelectual),
         new XElement(ns + "reabReadap", alteracao.dadosTrabalhador.infoDeficiencia.reabReadap),
         opTag("infoCota", alteracao.dadosTrabalhador.infoDeficiencia.infoCota),
         opTag("observacao", alteracao.dadosTrabalhador.infoDeficiencia.observacao)),

         // dependente 0.99
         from e in lDependente
         select e,

         // aposentadoria 0.1
         opElement("aposentadoria", alteracao.dadosTrabalhador.aposentadoria.trabAposent,
         new XElement(ns + "trabAposent", alteracao.dadosTrabalhador.aposentadoria.trabAposent)),

         // contato 0.1
         from e in lContato
         select e

         ))); // alteracao

         return x509.signXMLSHA256(xml, cert);

      }

      #region ************************************************************************************************************** Tags com +1 ocorrência

      #region dependente   

      List<XElement> lDependente = new List<XElement>();
      public void add_dependente() {

         lDependente.Add(
         new XElement(ns + "dependente",
         new XElement(ns + "tpDep", alteracao.dadosTrabalhador.dependente.tpDep),
         new XElement(ns + "nmDep", alteracao.dadosTrabalhador.dependente.nmDep),
         new XElement(ns + "dtNascto", alteracao.dadosTrabalhador.dependente.dtNascto),
         opTag("cpfDep", alteracao.dadosTrabalhador.dependente.cpfDep),
         new XElement(ns + "depIRRF", alteracao.dadosTrabalhador.dependente.depIRRF),
         new XElement(ns + "depSF", alteracao.dadosTrabalhador.dependente.depSF),
         new XElement(ns + "incTrab", alteracao.dadosTrabalhador.dependente.incTrab)));

         alteracao.dadosTrabalhador.dependente = new sAlteracao.sDadosTrabalhador.sDependente();
      }
      #endregion

      #region contato   

      List<XElement> lContato = new List<XElement>();
      public void add_contato() {

         lContato.Add(
         new XElement(ns + "contato",
         opTag("fonePrinc", alteracao.dadosTrabalhador.contato.fonePrinc),
         opTag("foneAlternat", alteracao.dadosTrabalhador.contato.foneAlternat),
         opTag("emailPrinc", alteracao.dadosTrabalhador.contato.emailPrinc),
         opTag("emailAlternat", alteracao.dadosTrabalhador.contato.emailAlternat)));

         alteracao.dadosTrabalhador.contato = new sAlteracao.sDadosTrabalhador.sContato();
      }
      #endregion

      #endregion

      #region **************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento {
         public string indRetif, nrRecibo, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeTrabalhador ideTrabalhador;
      public struct sIdeTrabalhador { public string cpfTrab; }

      public sAlteracao alteracao;

      public struct sAlteracao {
         public string dtAlteracao;

         public sDadosTrabalhador dadosTrabalhador;
         public struct sDadosTrabalhador {

            public string nisTrab, nmTrab, sexo, grauInstr, nmSoc;
            public string racaCor, estCiv;

            public sNascimento nascimento;
            public struct sNascimento {
               public string dtNascto;
               public string codMunic;
               public string uf, paisNascto, paisNac, nmMae, nmPai;
            }

            public sDocumentos documentos;
            public struct sDocumentos {

               public sCTPS CTPS;
               public struct sCTPS {
                  public string nrCtps, serieCtps, ufCtps;
               }

               public sRIC RIC;
               public struct sRIC {
                  public string nrRic, orgaoEmissor;
                  public string dtExped;
               }

               public sRG RG;
               public struct sRG {
                  public string nrRg, orgaoEmissor;
                  public string dtExped;
               }

               public sRNE RNE;
               public struct sRNE {
                  public string nrRne, orgaoEmissor;
                  public string dtExped;
               }

               public sOC OC;
               public struct sOC {
                  public string nrOc, orgaoEmissor;
                  public string dtExped, dtValid;
               }

               public sCNH CNH;
               public struct sCNH {
                  public string nrRegCnh, ufCnh, categoriaCnh;
                  public string dtExped, dtValid, dtPriHab;
               }
            }

            public sEndereco endereco;
            public struct sEndereco {

               public sBrasil brasil;
               public struct sBrasil {
                  public string tpLograd, dscLograd, nrLograd, complemento, bairro, cep, uf;
                  public string codMunic;
               }
               public sExterior exterior;
               public struct sExterior { public string paisResid, dscLograd, nrLograd, complemento, bairro, nmCid, codPostal; }
            }

            public sTrabEstrangeiro trabEstrangeiro;
            public struct sTrabEstrangeiro {
               public string classTrabEstrang;
               public string casadoBr, filhosBr;
               public string dtChegada;
            }
            public sInfoDeficiencia infoDeficiencia;
            public struct sInfoDeficiencia { public string defFisica, defVisual, defAuditiva, defMental, defIntelectual, reabReadap, infoCota, observacao; }

            public sDependente dependente;
            public struct sDependente {
               public string tpDep, nmDep, cpfDep, depIRRF, depSF, incTrab;
               public string dtNascto;
            }
            public sAposentadoria aposentadoria;
            public struct sAposentadoria { public string trabAposent; }

            public sContato contato;
            public struct sContato { public string fonePrinc, foneAlternat, emailPrinc, emailAlternat; }
         }
      }
      #endregion
   }
}