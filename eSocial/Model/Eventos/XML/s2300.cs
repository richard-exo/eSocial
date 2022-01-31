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
   public class s2300 : bEvento_XML
   {
      public s2300(string sID) : base("evtTSVInicio")
      {
         id = sID;
         ideEvento = new sIdeEvento();
         trabalhador = new sTrabalhador();
         trabalhador.nascimento = new sTrabalhador.sNascimento();
         trabalhador.documentos = new sTrabalhador.sDocumentos();
         trabalhador.documentos.CTPS = new sTrabalhador.sDocumentos.sCTPS();
         trabalhador.documentos.RIC = new sTrabalhador.sDocumentos.sRIC();
         trabalhador.documentos.RG = new sTrabalhador.sDocumentos.sRG();
         trabalhador.documentos.RNE = new sTrabalhador.sDocumentos.sRNE();
         trabalhador.documentos.OC = new sTrabalhador.sDocumentos.sOC();
         trabalhador.documentos.CNH = new sTrabalhador.sDocumentos.sCNH();

         trabalhador.endereco = new sTrabalhador.sEndereco();
         trabalhador.endereco.brasil = new sTrabalhador.sEndereco.sBrasil();
         trabalhador.endereco.exterior = new sTrabalhador.sEndereco.sExterior();
         trabalhador.trabEstrangeiro = new sTrabalhador.sTrabEstrangeiro();
         trabalhador.infoDeficiencia = new sTrabalhador.sInfoDeficiencia();
         trabalhador.dependente = new sTrabalhador.sDependente();
         trabalhador.aposentadoria = new sTrabalhador.sAposentadoria();
         trabalhador.contato = new sTrabalhador.sContato();

         infoTSVInicio = new sInfoTSVInicio();
         infoTSVInicio.infoComplementares = new sInfoTSVInicio.sInfoComplementares();
         infoTSVInicio.infoComplementares.cargoFuncao = new sInfoTSVInicio.sInfoComplementares.sCargoFuncao();
         infoTSVInicio.infoComplementares.remuneracao = new sInfoTSVInicio.sInfoComplementares.sRemuneracao();
         infoTSVInicio.infoComplementares.FGTS = new sInfoTSVInicio.sInfoComplementares.sFGTS();
         infoTSVInicio.infoComplementares.infoDirigenteSindical = new sInfoTSVInicio.sInfoComplementares.sInfoDirigenteSindical();
         infoTSVInicio.infoComplementares.infoTrabCedido = new sInfoTSVInicio.sInfoComplementares.sInfoTrabCedido();
         infoTSVInicio.infoComplementares.infoEstagiario = new sInfoTSVInicio.sInfoComplementares.sInfoEstagiario();
         infoTSVInicio.infoComplementares.infoEstagiario.instEnsino = new sInfoTSVInicio.sInfoComplementares.sInfoEstagiario.sInstEnsino();
         infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao = new sInfoTSVInicio.sInfoComplementares.sInfoEstagiario.sAgeIntegracao();
         infoTSVInicio.infoComplementares.infoEstagiario.supervisorEstagiario = new sInfoTSVInicio.sInfoComplementares.sInfoEstagiario.sSupervisorEstagiario();
         infoTSVInicio.mudancaCPF = new sInfoTSVInicio.sMudancaCPF();
         infoTSVInicio.afastamento = new sInfoTSVInicio.sAfastamento();
         infoTSVInicio.termino = new sInfoTSVInicio.sTermino();
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

         // trabalhador
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "trabalhador",
         new XElement(ns + "cpfTrab", trabalhador.cpfTrab),
         new XElement(ns + "nisTrab", trabalhador.nisTrab),
         new XElement(ns + "nmTrab", trabalhador.nmTrab),
         new XElement(ns + "sexo", trabalhador.sexo),
         new XElement(ns + "racaCor", trabalhador.racaCor),
         opTag("estCiv", trabalhador.estCiv),
         new XElement(ns + "grauInstr", trabalhador.grauInstr),
         opTag("nmSoc", trabalhador.nmSoc),

         // nascimento
         new XElement(ns + "nascimento",
         new XElement(ns + "dtNascto", trabalhador.nascimento.dtNascto),
         opTag("codMunic", trabalhador.nascimento.codMunic),
         opTag("uf", trabalhador.nascimento.uf),
         new XElement(ns + "paisNascto", trabalhador.nascimento.paisNascto),
         new XElement(ns + "paisNac", trabalhador.nascimento.paisNac),
         opTag("nmMae", trabalhador.nascimento.nmMae),
         opTag("nmPai", trabalhador.nascimento.nmPai)),

         // documentos 0.1
         opElement("documentos", trabalhador.documentos.CTPS.nrCtps,

         // CTPS 0.1
         opElement("CTPS", trabalhador.documentos.CTPS.nrCtps,
         new XElement(ns + "nrCtps", trabalhador.documentos.CTPS.nrCtps),
         new XElement(ns + "serieCtps", trabalhador.documentos.CTPS.serieCtps),
         new XElement(ns + "ufCtps", trabalhador.documentos.CTPS.ufCtps)),

         // RIC 0.1
         opElement("RIC", trabalhador.documentos.RIC.nrRic,
         new XElement(ns + "nrRic", trabalhador.documentos.RIC.nrRic),
         new XElement(ns + "orgaoEmissor", trabalhador.documentos.RIC.orgaoEmissor),
         opTag("dtExped", trabalhador.documentos.RIC.dtExped)),

         // RG 0.1
         opElement("RG", trabalhador.documentos.RG.nrRg,
         new XElement(ns + "nrRg", trabalhador.documentos.RG.nrRg),
         new XElement(ns + "orgaoEmissor", trabalhador.documentos.RG.orgaoEmissor),
         opTag("dtExped", trabalhador.documentos.RG.dtExped)),

         // RNE 0.1
         opElement("RNE", trabalhador.documentos.RNE.nrRne,
         new XElement(ns + "nrRne", trabalhador.documentos.RNE.nrRne),
         new XElement(ns + "orgaoEmissor", trabalhador.documentos.RNE.orgaoEmissor),
         opTag("dtExped", trabalhador.documentos.RNE.dtExped)),

         // OC 0.1
         opElement("OC", trabalhador.documentos.OC.nrOc,
         new XElement(ns + "nrOc", trabalhador.documentos.OC.nrOc),
         new XElement(ns + "orgaoEmissor", trabalhador.documentos.OC.orgaoEmissor),
         opTag("dtExped", trabalhador.documentos.OC.dtExped),
         opTag("dtValid", trabalhador.documentos.OC.dtValid)),

         // CNH 0.1
         opElement("CNH", trabalhador.documentos.CNH.nrRegCnh,
         new XElement(ns + "nrRegCnh", trabalhador.documentos.CNH.nrRegCnh),
         opTag("dtExped", trabalhador.documentos.CNH.dtExped),
         new XElement(ns + "ufCnh", trabalhador.documentos.CNH.ufCnh),
         new XElement(ns + "dtValid", trabalhador.documentos.CNH.dtValid),
         opTag("dtPriHab", trabalhador.documentos.CNH.dtPriHab),
         new XElement(ns + "categoriaCnh", trabalhador.documentos.CNH.categoriaCnh))

         ), //documentos

         // endereco
         new XElement(ns + "endereco",

         // brasil 0.1
         opElement("brasil", trabalhador.endereco.brasil.tpLograd,
         new XElement(ns + "tpLograd", trabalhador.endereco.brasil.tpLograd),
         new XElement(ns + "dscLograd", trabalhador.endereco.brasil.dscLograd),
         new XElement(ns + "nrLograd", trabalhador.endereco.brasil.nrLograd),
         opTag("complemento", trabalhador.endereco.brasil.complemento),
         opTag("bairro", trabalhador.endereco.brasil.bairro),
         new XElement(ns + "cep", trabalhador.endereco.brasil.cep),
         new XElement(ns + "codMunic", trabalhador.endereco.brasil.codMunic),
         new XElement(ns + "uf", trabalhador.endereco.brasil.uf)),

         // exterior 0.1
         opElement("exterior", trabalhador.endereco.exterior.paisResid,
         new XElement(ns + "paisResid", trabalhador.endereco.exterior.paisResid),
         new XElement(ns + "dscLograd", trabalhador.endereco.exterior.dscLograd),
         new XElement(ns + "nrLograd", trabalhador.endereco.exterior.nrLograd),
         opTag("complemento", trabalhador.endereco.exterior.complemento),
         opTag("bairro", trabalhador.endereco.exterior.bairro),
         new XElement(ns + "nmCid", trabalhador.endereco.exterior.nmCid),
         opTag("codPostal", trabalhador.endereco.exterior.codPostal))

         ), // endereco

         // trabEstrangeiro 0.1
         opElement("trabEstrangeiro", trabalhador.trabEstrangeiro.dtChegada,
         new XElement(ns + "dtChegada", trabalhador.trabEstrangeiro.dtChegada),
         new XElement(ns + "classTrabEstrang", trabalhador.trabEstrangeiro.classTrabEstrang),
         new XElement(ns + "casadoBr", trabalhador.trabEstrangeiro.casadoBr),
         new XElement(ns + "filhosBr", trabalhador.trabEstrangeiro.filhosBr)),

         // infoDeficiencia 0.1
         opElement("infoDeficiencia", trabalhador.infoDeficiencia.defFisica,
         new XElement(ns + "defFisica", trabalhador.infoDeficiencia.defFisica),
         new XElement(ns + "defVisual", trabalhador.infoDeficiencia.defVisual),
         new XElement(ns + "defAuditiva", trabalhador.infoDeficiencia.defAuditiva),
         new XElement(ns + "defMental", trabalhador.infoDeficiencia.defMental),
         new XElement(ns + "defIntelectual", trabalhador.infoDeficiencia.defIntelectual),
         new XElement(ns + "reabReadap", trabalhador.infoDeficiencia.reabReadap),
         opTag("observacao", trabalhador.infoDeficiencia.observacao)),

         // dependente 0.99
         from e in lDependente
         select e,

         // contato 0.1
         from e in lContato
         select e

         )); // trabalhador

         // infoTSVInicio
         xml.Elements().ElementAt(0).Add(
         opElement("infoTSVInicio", infoTSVInicio.cadIni,
         new XElement(ns + "cadIni", infoTSVInicio.cadIni),
         new XElement(ns + "codCateg", infoTSVInicio.codCateg),
         new XElement(ns + "dtInicio", infoTSVInicio.dtInicio),
         opTag("natAtividade", infoTSVInicio.natAtividade),

         // infoComplementares
         new XElement(ns + "infoComplementares",
         opElement("cargoFuncao", infoTSVInicio.infoComplementares.cargoFuncao.codCargo,
         new XElement(ns + "codCargo", infoTSVInicio.infoComplementares.cargoFuncao.codCargo),
         opTag("codFuncao", infoTSVInicio.infoComplementares.cargoFuncao.codFuncao)),

         new XElement(ns + "remuneracao",
         new XElement(ns + "vrSalFx", infoTSVInicio.infoComplementares.remuneracao.vrSalFx),
         new XElement(ns + "undSalFixo", infoTSVInicio.infoComplementares.remuneracao.undSalFixo),
         opTag("descSalVar", infoTSVInicio.infoComplementares.remuneracao.dscSalVar)),

         // FGTS
         opElement("fgts",
         opTag("opcFGTS", infoTSVInicio.infoComplementares.FGTS.opcFGTS),
         opTag("dtOpcFGTS", infoTSVInicio.infoComplementares.FGTS.dtOpcFGTS)),

         // infoDirigenteSindical
         opElement("infoDirigenteSindical",
         opTag("categOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.categOrig),
         opTag("cnpjOrigem", infoTSVInicio.infoComplementares.infoDirigenteSindical.cnpjOrigem),
         opTag("dtAdmOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.dtAdmOrig),
         opTag("matricOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.matricOrig)),

         // infoTrabCedido
         opElement("infoTrabCedido",
         opTag("categOrig", infoTSVInicio.infoComplementares.infoTrabCedido.categOrig),
         opTag("cnpjCednt", infoTSVInicio.infoComplementares.infoTrabCedido.cnpCednt),
         opTag("matricCed", infoTSVInicio.infoComplementares.infoTrabCedido.matricCed),
         opTag("dtAdmCed", infoTSVInicio.infoComplementares.infoTrabCedido.dtAdmCed),
         opTag("tpRegTrab", infoTSVInicio.infoComplementares.infoTrabCedido.tpRegTrab),
         opTag("tpRegPrev", infoTSVInicio.infoComplementares.infoTrabCedido.tpRegPrev),
         opTag("infOnus", infoTSVInicio.infoComplementares.infoTrabCedido.infOnus)),

         // infoEstagiario
         opElement("localTrabalho",
         opTag("natEstagio", infoTSVInicio.infoComplementares.infoEstagiario.natEstagio),
         opTag("nivEstagio", infoTSVInicio.infoComplementares.infoEstagiario.nivEstagio),
         opTag("areaAtuacao", infoTSVInicio.infoComplementares.infoEstagiario.areaAtuacao),
         opTag("nrApol", infoTSVInicio.infoComplementares.infoEstagiario.nrApol),
         opTag("vlrBolsa", infoTSVInicio.infoComplementares.infoEstagiario.vlrBolsa),
         opTag("dtPrevTerm", infoTSVInicio.infoComplementares.infoEstagiario.dtPrevTeam)),

         // intEnsino
         opElement("instEnsino",
         opTag("cnpjInstEnsino", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.cnpjInstEnsino),
         opTag("nmRazao", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.nmRazao),
         opTag("dscLograd", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.dscLograd),
         opTag("nrLograd", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.nrLograd),
         opTag("bairro", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.bairro),
         opTag("cep", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.cep),
         opTag("codMunic", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.codMunic),
         opTag("uf", infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.uf)),

         // ageIntegracao
         opElement("ageIntegracao",
         opTag("cnpjAgntInteg", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg),
         opTag("nrRazao", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.nrRazao),
         opTag("dscLograd", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.dscLograd),
         opTag("nrLograd", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.nrLograd),
         opTag("bairro", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.bairro),
         opTag("cep", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.cep),
         opTag("codMunic", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.codMunic),
         opTag("uf", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.uf)),

         // supervisorEstagio
         opElement("supervisorEstagio",
         opTag("cpfSupervisor", infoTSVInicio.infoComplementares.infoEstagiario.supervisorEstagiario.cpfSupervisor),
         opTag("nmSuperv", infoTSVInicio.infoComplementares.infoEstagiario.supervisorEstagiario.nmSuperv)),

         // mudancaCPF 
         opElement("mudancaCPF",
         opTag("cpfAnt", infoTSVInicio.mudancaCPF.cpfAnt),
         opTag("dtAltCPF", infoTSVInicio.mudancaCPF.dtAltCPF),
         opTag("observacao", infoTSVInicio.mudancaCPF.observacao)),
         
         // afastamento
         opElement("afastamento",
         opTag("dtIniAfast", infoTSVInicio.afastamento.dtIniAfast),
         opTag("codMotAfast", infoTSVInicio.afastamento.codMotAfast)),

         // termino
         opElement("termino",
         opTag("dtTerm", infoTSVInicio.termino.dtTerm))

         ))); // infoTSVInicio

         return x509.signXMLSHA256(xml, cert);
      }

      #region dependente   

      List<XElement> lDependente = new List<XElement>();
      public void add_dependente()
      {

         lDependente.Add(
         new XElement(ns + "dependente",
         new XElement(ns + "tpDep", trabalhador.dependente.tpDep),
         new XElement(ns + "nmDep", trabalhador.dependente.nmDep),
         new XElement(ns + "dtNascto", trabalhador.dependente.dtNascto),
         opTag("cpfDep", trabalhador.dependente.cpfDep),
         new XElement(ns + "depIRRF", trabalhador.dependente.depIRRF),
         new XElement(ns + "depSF", trabalhador.dependente.depSF),
         new XElement(ns + "incTrab", trabalhador.dependente.incTrab)));

         trabalhador.dependente = new sTrabalhador.sDependente();
      }
      #endregion

      #region contato   

      List<XElement> lContato = new List<XElement>();
      public void add_contato()
      {

         lContato.Add(
         new XElement(ns + "contato",
         opTag("fonePrinc", trabalhador.contato.fonePrinc),
         opTag("foneAlternat", trabalhador.contato.foneAlternat),
         opTag("emailPrinc", trabalhador.contato.emailPrinc),
         opTag("emailAlternat", trabalhador.contato.emailAlternat)));

         trabalhador.contato = new sTrabalhador.sContato();
      }
      #endregion


      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, indApuracao, nrRecibo, perApur, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sTrabalhador trabalhador;
      public struct sTrabalhador
      {
         public string racaCor, estCiv;
         public string cpfTrab, nisTrab, nmTrab, sexo, grauInstr, nmSoc;

         public sNascimento nascimento;
         public struct sNascimento
         {
            public string codMunic;
            public string uf, paisNascto, paisNac, nmMae, nmPai;
            public string dtNascto;
         }

         public sDocumentos documentos;
         public struct sDocumentos
         {

            public sCTPS CTPS;
            public struct sCTPS
            {
               public string nrCtps, serieCtps, ufCtps;
            }

            public sRIC RIC;
            public struct sRIC
            {
               public string nrRic, orgaoEmissor;
               public string dtExped;
            }

            public sRG RG;
            public struct sRG
            {
               public string nrRg, orgaoEmissor;
               public string dtExped;
            }

            public sRNE RNE;
            public struct sRNE
            {
               public string nrRne, orgaoEmissor;
               public string dtExped;
            }

            public sOC OC;
            public struct sOC
            {
               public string nrOc, orgaoEmissor;
               public string dtExped, dtValid;
            }

            public sCNH CNH;
            public struct sCNH
            {
               public string nrRegCnh, ufCnh, categoriaCnh;
               public string dtExped, dtValid, dtPriHab;
            }
         }

         public sEndereco endereco;
         public struct sEndereco
         {

            public sBrasil brasil;
            public struct sBrasil
            {
               public string tpLograd, dscLograd, nrLograd, complemento, bairro, cep, uf;
               public string codMunic;
            }
            public sExterior exterior;
            public struct sExterior { public string paisResid, dscLograd, nrLograd, complemento, bairro, nmCid, codPostal; }
         }

         public sTrabEstrangeiro trabEstrangeiro;
         public struct sTrabEstrangeiro
         {
            public string classTrabEstrang;
            public string casadoBr, filhosBr;
            public string dtChegada;
         }
         public sInfoDeficiencia infoDeficiencia;
         public struct sInfoDeficiencia { public string defFisica, defVisual, defAuditiva, defMental, defIntelectual, reabReadap, infoCota, observacao; }

         public sDependente dependente;
         public struct sDependente
         {
            public string tpDep, nmDep, cpfDep, depIRRF, depSF, incTrab;
            public string dtNascto;
         }
         public sAposentadoria aposentadoria;
         public struct sAposentadoria { public string trabAposent; }

         public sContato contato;
         public struct sContato { public string fonePrinc, foneAlternat, emailPrinc, emailAlternat; }
      }

      public sInfoTSVInicio infoTSVInicio;
      public struct sInfoTSVInicio
      {
         public string cadIni, codCateg, dtInicio, natAtividade;

         public sInfoComplementares infoComplementares;
         public struct sInfoComplementares
         {
            public sCargoFuncao cargoFuncao;
            public struct sCargoFuncao
            {
               public string codCargo, codFuncao;
            }
            public sRemuneracao remuneracao;
            public struct sRemuneracao
            {
               public string undSalFixo;
               public string dscSalVar;
               public string vrSalFx;
            }
            public sFGTS FGTS;
            public struct sFGTS
            {
               public string dtOpcFGTS;
               public string opcFGTS;
            }
            public sInfoDirigenteSindical infoDirigenteSindical;
            public struct sInfoDirigenteSindical
            {
               public string categOrig, cnpjOrigem, dtAdmOrig, matricOrig;
            }
            public sInfoTrabCedido infoTrabCedido;
            public struct sInfoTrabCedido
            {
               public string categOrig, cnpCednt, matricCed, dtAdmCed, tpRegTrab, tpRegPrev, infOnus;
            }
            public sInfoEstagiario infoEstagiario;
            public struct sInfoEstagiario
            {
               public string natEstagio, nivEstagio, areaAtuacao, nrApol, vlrBolsa, dtPrevTeam;

               public sInstEnsino instEnsino;
               public struct sInstEnsino
               {
                  public string cnpjInstEnsino, nmRazao, dscLograd, nrLograd, bairro, cep, codMunic, uf;
               }
               public sAgeIntegracao ageIntegracao;
               public struct sAgeIntegracao
               {
                  public string cnpjAgntInteg, nrRazao, dscLograd, nrLograd, bairro, cep, codMunic, uf;
               }
               public sSupervisorEstagiario supervisorEstagiario;
               public struct sSupervisorEstagiario
               {
                  public string cpfSupervisor, nmSuperv;
               }
            }
         }
         public sMudancaCPF mudancaCPF;
         public struct sMudancaCPF
         {
            public string cpfAnt, dtAltCPF, observacao;
         }
         public sAfastamento afastamento;
         public struct sAfastamento
         {
            public string dtIniAfast, codMotAfast;
         }
         public sTermino termino;
         public struct sTermino
         {
            public string dtTerm;
         }
      }
      #endregion
   }
}