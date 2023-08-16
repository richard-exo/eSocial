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
      public s2300(string sID) : base("evtTSVInicio", "", "v_S_01_01_00")
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
         trabalhador.trabImig = new sTrabalhador.sTrabImig();
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
         infoTSVInicio.infoComplementares.infoMandElet = new sInfoTSVInicio.sInfoComplementares.sInfoMandElet();
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
         new XElement(ns + "nmTrab", trabalhador.nmTrab),
         new XElement(ns + "sexo", trabalhador.sexo),
         new XElement(ns + "racaCor", trabalhador.racaCor),
         opTag("estCiv", trabalhador.estCiv),
         new XElement(ns + "grauInstr", trabalhador.grauInstr),
         opTag("nmSoc", trabalhador.nmSoc),

         // nascimento
         new XElement(ns + "nascimento",
         new XElement(ns + "dtNascto", trabalhador.nascimento.dtNascto),
         new XElement(ns + "paisNascto", trabalhador.nascimento.paisNascto),
         new XElement(ns + "paisNac", trabalhador.nascimento.paisNac)),

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

         // trabImig 0.1
         opElement("trabImig", trabalhador.trabImig.condIng,
         opTag("tmpResid", trabalhador.trabImig.tmpResid),
         new XElement(ns + "condIng", trabalhador.trabImig.condIng)),

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
         opTag("matricula", infoTSVInicio.matricula),
         new XElement(ns + "codCateg", infoTSVInicio.codCateg),
         new XElement(ns + "dtInicio", infoTSVInicio.dtInicio),
         opTag("nrProcTrab", infoTSVInicio.nrProcTrab),
         opTag("natAtividade", infoTSVInicio.natAtividade),

         // infoComplementares
         new XElement(ns + "infoComplementares",
         opElement("cargoFuncao", infoTSVInicio.infoComplementares.cargoFuncao.nmCargo,
         opTag("nmCargo", infoTSVInicio.infoComplementares.cargoFuncao.nmCargo),
         opTag("CBOCargo", infoTSVInicio.infoComplementares.cargoFuncao.CBOCargo),
         opTag("nmFuncao", infoTSVInicio.infoComplementares.cargoFuncao.nmFuncao),
         opTag("CBOFuncao", infoTSVInicio.infoComplementares.cargoFuncao.CBOFuncao)),

         new XElement(ns + "remuneracao",
         new XElement(ns + "vrSalFx", infoTSVInicio.infoComplementares.remuneracao.vrSalFx),
         new XElement(ns + "undSalFixo", infoTSVInicio.infoComplementares.remuneracao.undSalFixo),
         opTag("descSalVar", infoTSVInicio.infoComplementares.remuneracao.dscSalVar)),

         // FGTS
         opElement("fgts",
         opTag("dtOpcFGTS", infoTSVInicio.infoComplementares.FGTS.dtOpcFGTS)),

         // infoDirigenteSindical
         opElement("infoDirigenteSindical",
         opTag("categOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.categOrig),
         opTag("tpInsc", infoTSVInicio.infoComplementares.infoDirigenteSindical.tpInsc),
         opTag("nrInsc", infoTSVInicio.infoComplementares.infoDirigenteSindical.nrInsc),
         opTag("dtAdmOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.dtAdmOrig),
         opTag("matricOrig", infoTSVInicio.infoComplementares.infoDirigenteSindical.matricOrig),
         opTag("tpRegTrab", infoTSVInicio.infoComplementares.infoDirigenteSindical.tpRegTrab),
         new XElement(ns + "tpRegPrev", infoTSVInicio.infoComplementares.infoDirigenteSindical.tpRegPrev)),

         // infoTrabCedido
         opElement("infoTrabCedido",
         opTag("categOrig", infoTSVInicio.infoComplementares.infoTrabCedido.categOrig),
         opTag("cnpjCednt", infoTSVInicio.infoComplementares.infoTrabCedido.cnpCednt),
         opTag("matricCed", infoTSVInicio.infoComplementares.infoTrabCedido.matricCed),
         opTag("dtAdmCed", infoTSVInicio.infoComplementares.infoTrabCedido.dtAdmCed),
         opTag("tpRegTrab", infoTSVInicio.infoComplementares.infoTrabCedido.tpRegTrab),
         opTag("tpRegPrev", infoTSVInicio.infoComplementares.infoTrabCedido.tpRegPrev)),

         // infoMandElet
         opElement("infoMandElet", infoTSVInicio.infoComplementares.infoMandElet.categOrig,
         new XElement(ns + "categOrig", infoTSVInicio.infoComplementares.infoMandElet.categOrig),
         new XElement(ns + "cnpjOrig", infoTSVInicio.infoComplementares.infoMandElet.cnpjOrig),
         new XElement(ns + "matricOrig", infoTSVInicio.infoComplementares.infoMandElet.matricOrig),
         new XElement(ns + "dtExercOrig", infoTSVInicio.infoComplementares.infoMandElet.tpRegPrev),
         opTag("indRemunCargo", infoTSVInicio.infoComplementares.infoMandElet.indRemunCargo),
         new XElement(ns + "tpRegTrab", infoTSVInicio.infoComplementares.infoMandElet.tpRegTrab),
         new XElement(ns + "tpRegPrev", infoTSVInicio.infoComplementares.infoMandElet.tpRegPrev)),

         // infoEstagiario
         opElement("infoEstagiario",
         opTag("natEstagio", infoTSVInicio.infoComplementares.infoEstagiario.natEstagio),
         opTag("nivEstagio", infoTSVInicio.infoComplementares.infoEstagiario.nivEstagio),
         opTag("areaAtuacao", infoTSVInicio.infoComplementares.infoEstagiario.areaAtuacao),
         opTag("nrApol", infoTSVInicio.infoComplementares.infoEstagiario.nrApol),
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
         opTag("cnpjAgntInteg", infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg)),

         // supervisorEstagio
         opElement("supervisorEstagio",
         opTag("cpfSupervisor", infoTSVInicio.infoComplementares.infoEstagiario.supervisorEstagiario.cpfSupervisor)),

         // mudancaCPF 
         opElement("mudancaCPF", infoTSVInicio.mudancaCPF.cpfAnt,
         new XElement(ns + "cnpjOrig", infoTSVInicio.mudancaCPF.cpfAnt),
         opTag("matricAnt", infoTSVInicio.mudancaCPF.matricAnt),
         new XElement(ns + "dtAltCPF", infoTSVInicio.mudancaCPF.dtAltCPF),
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
         opTag("emailPrinc", trabalhador.contato.emailPrinc)));

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

         public sTrabImig trabImig;
         public struct sTrabImig
         {
            public string tmpResid;
            public string condIng;
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
         public string cadIni, matricula, codCateg, dtInicio, natAtividade, nrProcTrab;

         public sInfoComplementares infoComplementares;
         public struct sInfoComplementares
         {
            public sCargoFuncao cargoFuncao;
            public struct sCargoFuncao
            {
               public string codCargo, codFuncao, nmCargo, CBOCargo, nmFuncao, CBOFuncao;
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
               public string categOrig, cnpjOrigem, dtAdmOrig, matricOrig, tpInsc, nrInsc, tpRegTrab, tpRegPrev;
            }
            public sInfoTrabCedido infoTrabCedido;
            public struct sInfoTrabCedido
            {
               public string categOrig, cnpCednt, matricCed, dtAdmCed, tpRegTrab, tpRegPrev, infOnus;
            }
            public sInfoMandElet infoMandElet;
            public struct sInfoMandElet
            {
               public string categOrig, cnpjOrig, matricOrig, dtExercOrig, indRemunCargo, tpRegTrab, tpRegPrev;
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
            public string cpfAnt, dtAltCPF, matricAnt, observacao;
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