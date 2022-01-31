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
    public class s2200 : bEvento_XML {

        public s2200(string sID) : base("evtAdmissao") {

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

            vinculo = new sVinculo();
            vinculo.infoRegimeTrab = new sVinculo.sInfoRegimeTrab();
            vinculo.infoRegimeTrab.infoCeletista = new sVinculo.sInfoRegimeTrab.sInfoCeletista();
            vinculo.infoRegimeTrab.infoCeletista.FGTS = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sFGTS();
            vinculo.infoRegimeTrab.infoCeletista.trabTemporario = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sTrabTemporario();
            vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sTrabTemporario.sIdeTomadorServ();
            vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sTrabTemporario.sIdeTomadorServ.sIdeEstabVinc();
            vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTrabSubstituido = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sTrabTemporario.sIdeTrabSubstituido();
            vinculo.infoRegimeTrab.infoCeletista.aprend = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sAprend();

            vinculo.infoRegimeTrab.infoEstatutario = new sVinculo.sInfoRegimeTrab.sInfoEstatutario();
            vinculo.infoRegimeTrab.infoEstatutario.infoDecJud = new sVinculo.sInfoRegimeTrab.sInfoEstatutario.sInfoDecJud();

            vinculo.infoContrato = new sVinculo.sInfoContrato();
            vinculo.infoContrato.remuneracao = new sVinculo.sInfoContrato.sRemuneracao();
            vinculo.infoContrato.duracao = new sVinculo.sInfoContrato.sDuracao();

            vinculo.infoContrato.localTrabalho = new sVinculo.sInfoContrato.sLocalTrabalho();
            vinculo.infoContrato.localTrabalho.localTrabGeral = new sVinculo.sInfoContrato.sLocalTrabalho.sLocalTrabGeral();
            vinculo.infoContrato.localTrabalho.localTrabDom = new sVinculo.sInfoContrato.sLocalTrabalho.sLocalTrabDom();

            vinculo.infoContrato.horContratual = new sVinculo.sInfoContrato.sHorContratual();
            vinculo.infoContrato.horContratual.horario = new sVinculo.sInfoContrato.sHorContratual.sHorario();

            vinculo.infoContrato.filiacaoSindical = new sVinculo.sInfoContrato.sFiliacaoSindical();
            vinculo.infoContrato.alvaraJudicial = new sVinculo.sInfoContrato.sAlvaraJudicial();
            vinculo.infoContrato.observacoes = new sVinculo.sInfoContrato.sObservacoes();

            vinculo.sucessaoVinc = new sVinculo.sSucessaoVinc();
            vinculo.transfDom = new sVinculo.sTransfDom();
            vinculo.mudancaCPF = new sVinculo.sMudancaCPF();
            vinculo.afastamento = new sVinculo.sAfastamento();
            vinculo.desligamento = new sVinculo.sDesligamento();
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
            opTag("indPriEmpr", trabalhador.indPriEmpr),
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
            new XElement(ns + "infoCota", trabalhador.infoDeficiencia.infoCota),
            opTag("observacao", trabalhador.infoDeficiencia.observacao)),

            // dependente 0.99
            from e in lDependente
            select e,

            // aposentadoria 0.1
            opElement("aposentadoria", trabalhador.aposentadoria.trabAposent,
            new XElement(ns + "trabAposent", trabalhador.aposentadoria.trabAposent)),

            // contato 0.1
            from e in lContato
            select e

            )); // trabalhador

            // vinculo
            xml.Elements().ElementAt(0).Add(
            new XElement(ns + "vinculo",
            new XElement(ns + "matricula", vinculo.matricula),
            new XElement(ns + "tpRegTrab", vinculo.tpRegTrab),
            new XElement(ns + "tpRegPrev", vinculo.tpRegPrev),
            opTag("nrRecInfPrelim", vinculo.nrRecInfPrelim),
            new XElement(ns + "cadIni", vinculo.cadIni),

            // infoRegimeTrab
            new XElement(ns + "infoRegimeTrab",

            // infoCeletista 0.1
            opElement("infoCeletista", vinculo.infoRegimeTrab.infoCeletista.dtAdm,
            new XElement(ns + "dtAdm", vinculo.infoRegimeTrab.infoCeletista.dtAdm),
            new XElement(ns + "tpAdmissao", vinculo.infoRegimeTrab.infoCeletista.tpAdmissao),
            new XElement(ns + "indAdmissao", vinculo.infoRegimeTrab.infoCeletista.indAdmissao),
            new XElement(ns + "tpRegJor", vinculo.infoRegimeTrab.infoCeletista.tpRegJor),
            new XElement(ns + "natAtividade", vinculo.infoRegimeTrab.infoCeletista.natAtividade),
            opTag("dtBase", vinculo.infoRegimeTrab.infoCeletista.dtBase),
            new XElement(ns + "cnpjSindCategProf", vinculo.infoRegimeTrab.infoCeletista.cnpjSindCategProf),

            // FGTS
            new XElement(ns + "FGTS",
            new XElement(ns + "opcFGTS", vinculo.infoRegimeTrab.infoCeletista.FGTS.opcFGTS),
            opTag("dtOpcFGTS", vinculo.infoRegimeTrab.infoCeletista.FGTS.dtOpcFGTS)),

            // trabTemporario
            opElement("trabTemporario", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.hipLeg,
            new XElement(ns + "hipLeg", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.hipLeg),
            new XElement(ns + "justContr", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.justContr),
            new XElement(ns + "tpInclContr", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.tpInclContr),

            // ideTomadorServ
            new XElement(ns + "ideTomadorServ",
            new XElement(ns + "tpInsc", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.tpInsc),
            new XElement(ns + "nrInsc", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.nrInsc),

            // ideEstabVinc
            opElement("ideEstabVinc", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc.tpInsc,
            new XElement(ns + "tpInsc", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc.tpInsc),
            new XElement(ns + "nrInsc", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc.nrInsc)),

            // ideTrabSubstituido 0.9
            from e in lIdeTrabSubstituido
            select e,

            // aprend
            opElement("aprend", vinculo.infoRegimeTrab.infoCeletista.aprend.tpInsc,
            new XElement(ns + "tpInsc", vinculo.infoRegimeTrab.infoCeletista.aprend.tpInsc),
            new XElement(ns + "nrInsc", vinculo.infoRegimeTrab.infoCeletista.aprend.nrInsc))))

            ), // infoCeletista

            // infoEstatutario
            opElement("infoEstatutario", vinculo.infoRegimeTrab.infoEstatutario.indProvim,
            new XElement(ns + "indProvim", vinculo.infoRegimeTrab.infoEstatutario.indProvim),
            new XElement(ns + "tpProv", vinculo.infoRegimeTrab.infoEstatutario.tpProv),
            new XElement(ns + "dtNomeacao", vinculo.infoRegimeTrab.infoEstatutario.dtNomeacao),
            new XElement(ns + "dtPosse", vinculo.infoRegimeTrab.infoEstatutario.dtPosse),
            new XElement(ns + "dtExercicio", vinculo.infoRegimeTrab.infoEstatutario.dtExercicio),
            new XElement(ns + "tpPlanRP", vinculo.infoRegimeTrab.infoEstatutario.tpPlanRP),

            // infoDecJud 0.1
            opElement("infoDecJud", vinculo.infoRegimeTrab.infoEstatutario.infoDecJud.nrProcJud,
            new XElement(ns + "nrProcJud", vinculo.infoRegimeTrab.infoEstatutario.infoDecJud.nrProcJud)))

            ), // infoRegimeTrab

            // infoContrato
            new XElement(ns + "infoContrato",
            opTag("codCargo", vinculo.infoContrato.codCargo),
            opTag("codFuncao", vinculo.infoContrato.codFuncao),
            new XElement(ns + "codCateg", vinculo.infoContrato.codCateg),
            opTag("codCarreira", vinculo.infoContrato.codCarreira),
            opTag("dtIngrCarr", vinculo.infoContrato.dtIngrCarr),

            // remuneracao
            new XElement(ns + "remuneracao",
            new XElement(ns + "vrSalFx", vinculo.infoContrato.remuneracao.vrSalFx),
            new XElement(ns + "undSalFixo", vinculo.infoContrato.remuneracao.undSalFixo),
            opTag("dscSalVar", vinculo.infoContrato.remuneracao.dscSalVar)),

            // duracao
            new XElement(ns + "duracao",
            new XElement(ns + "tpContr", vinculo.infoContrato.duracao.tpContr),
            opTag("dtTerm", vinculo.infoContrato.duracao.dtTerm),
            opTag("clauAssec", vinculo.infoContrato.duracao.clauAssec),
            opTag("objDet", vinculo.infoContrato.duracao.objDet)),

            // localTrabalho
            new XElement(ns + "localTrabalho",

            // localTrabGeral 0.1
            opElement("localTrabGeral", vinculo.infoContrato.localTrabalho.localTrabGeral.tpInsc,
            new XElement(ns + "tpInsc", vinculo.infoContrato.localTrabalho.localTrabGeral.tpInsc),
            new XElement(ns + "nrInsc", vinculo.infoContrato.localTrabalho.localTrabGeral.nrInsc),
            opTag("descComp", vinculo.infoContrato.localTrabalho.localTrabGeral.descComp)),

            // localTrabDom 0.1
            opElement("localTrabDom", vinculo.infoContrato.localTrabalho.localTrabDom.tpLograd,
            new XElement(ns + "tpLograd", vinculo.infoContrato.localTrabalho.localTrabDom.tpLograd),
            new XElement(ns + "dscLograd", vinculo.infoContrato.localTrabalho.localTrabDom.dscLograd),
            new XElement(ns + "nrLograd", vinculo.infoContrato.localTrabalho.localTrabDom.nrLograd),
            opTag("complemento", vinculo.infoContrato.localTrabalho.localTrabDom.complemento),
            opTag("bairro", vinculo.infoContrato.localTrabalho.localTrabDom.bairro),
            new XElement(ns + "cep", vinculo.infoContrato.localTrabalho.localTrabDom.cep),
            new XElement(ns + "codMunic", vinculo.infoContrato.localTrabalho.localTrabDom.codMunic),
            new XElement(ns + "uf", vinculo.infoContrato.localTrabalho.localTrabDom.uf))

            ), // localTrabalho

            // horContratual 0.1
            opElement("horContratual", vinculo.infoContrato.horContratual.qtdHrsSem,
            opTag("qtdHrsSem", vinculo.infoContrato.horContratual.qtdHrsSem),
            new XElement(ns + "tpJornada", vinculo.infoContrato.horContratual.tpJornada),
            opTag("dscTpJorn", vinculo.infoContrato.horContratual.dscTpJorn),
            new XElement(ns + "tmpParc", vinculo.infoContrato.horContratual.tmpParc),

            // horario 0.99
            from e in lHorario
            select e),

            // filiacaoSindical 0.2
            from e in lFiliacaoSindical
            select e,

            // alvaraJudicial 0.1
            opElement("alvaraJudicial", vinculo.infoContrato.alvaraJudicial.nrProcJud,
            new XElement(ns + "nrProcJud", vinculo.infoContrato.alvaraJudicial.nrProcJud)),

            // observacoes 0.99
            from e in lObservacoes
            select e

            ),  // infoContrato

            // sucessaoVinc 0.1
            opElement("sucessaoVinc", vinculo.sucessaoVinc.cnpjEmpregAnt,
            new XElement(ns + "tpInscAnt", vinculo.sucessaoVinc.tpInscAnt),
            new XElement(ns + "cnpjEmpregAnt", vinculo.sucessaoVinc.cnpjEmpregAnt),
            opTag("matricAnt", vinculo.sucessaoVinc.matricAnt),
            new XElement(ns + "dtTransf", vinculo.sucessaoVinc.dtTransf),
            opTag("observacao", vinculo.sucessaoVinc.observacao)),

            // transfDom 0.1
            opElement("transfDom", vinculo.transfDom.cpfSubstituido,
            new XElement(ns + "cpfSubstituido", vinculo.transfDom.cpfSubstituido),
            opTag("matricAnt", vinculo.transfDom.matricAnt),
            new XElement(ns + "dtTransf", vinculo.transfDom.dtTransf)),

            // mudancaCPF 0.1
            opElement("mudancaCPF", vinculo.mudancaCPF.cpfAnt,
            new XElement(ns + "cpfAnt", vinculo.mudancaCPF.cpfAnt),
            new XElement(ns + "matricAnt", vinculo.mudancaCPF.matricAnt),
            new XElement(ns + "dtAltCPF", vinculo.mudancaCPF.dtAltCPF),
            opTag("observacao", vinculo.mudancaCPF.observacao)),

            // afastamento 0.1
            opElement("afastamento", vinculo.afastamento.dtIniAfast,
            new XElement(ns + "dtIniAfast", vinculo.afastamento.dtIniAfast),
            new XElement(ns + "codMotAfast", vinculo.afastamento.codMotAfast)),

            // desligamento 0.1
            opElement("desligamento", vinculo.desligamento.dtDeslig,
            new XElement(ns + "dtDeslig", vinculo.desligamento.dtDeslig))

            )); // vinculo

            return x509.signXMLSHA256(xml, cert);
        }

        #region ************************************************************************************************************** Tags com +1 ocorrência

        #region dependente   

        List<XElement> lDependente = new List<XElement>();
        public void add_dependente() {

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
        public void add_contato() {

            lContato.Add(
            new XElement(ns + "contato",
            opTag("fonePrinc", trabalhador.contato.fonePrinc),
            opTag("foneAlternat", trabalhador.contato.foneAlternat),
            opTag("emailPrinc", trabalhador.contato.emailPrinc),
            opTag("emailAlternat", trabalhador.contato.emailAlternat)));

            trabalhador.contato = new sTrabalhador.sContato();
        }
        #endregion

        #region ideTrabSubstituido 

        List<XElement> lIdeTrabSubstituido = new List<XElement>();
        public void add_ideTrabSubstituido() {

            lIdeTrabSubstituido.Add(
            new XElement(ns + "ideTrabSubstituido",
            new XElement(ns + "cpfTrabSubst", vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTrabSubstituido.cpfTrabSubst)));

            vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTrabSubstituido = new sVinculo.sInfoRegimeTrab.sInfoCeletista.sTrabTemporario.sIdeTrabSubstituido();
        }

        #endregion

        #region horario 

        List<XElement> lHorario = new List<XElement>();
        public void add_horario() {

            lHorario.Add(
            new XElement(ns + "horario",
            new XElement(ns + "dia", vinculo.infoContrato.horContratual.horario.dia),
            new XElement(ns + "codHorContrat", vinculo.infoContrato.horContratual.horario.codHorContrat)));

            vinculo.infoContrato.horContratual.horario = new sVinculo.sInfoContrato.sHorContratual.sHorario();
        }
        #endregion

        #region filiacaoSindical 

        List<XElement> lFiliacaoSindical = new List<XElement>();
        public void add_filiacaoSindical() {

            lFiliacaoSindical.Add(
            new XElement(ns + "filiacaoSindical", vinculo.infoContrato.filiacaoSindical.cnpjSindTrab,
            new XElement(ns + "cnpjSindTrab", vinculo.infoContrato.filiacaoSindical.cnpjSindTrab)));

            vinculo.infoContrato.filiacaoSindical = new sVinculo.sInfoContrato.sFiliacaoSindical();
        }

        #endregion

        #region observacoes 

        List<XElement> lObservacoes = new List<XElement>();
        public void add_observacoes() {

            lObservacoes.Add(
            new XElement(ns + "observacoes",
            new XElement(ns + "observacao", vinculo.infoContrato.observacoes.observacao)));

            vinculo.infoContrato.observacoes = new sVinculo.sInfoContrato.sObservacoes();
        }

        #endregion

        #endregion

        #region ****************************************************************************************************************************** Structs

        public new sIdeEvento ideEvento;
        public new struct sIdeEvento {
            public string indRetif, indApuracao, nrRecibo, perApur, verProc;
            public enTpAmb tpAmb;
            public enProcEmi procEmi;
        }

        public sTrabalhador trabalhador;
        public struct sTrabalhador {
            public string racaCor, estCiv;
            public string cpfTrab, nisTrab, nmTrab, sexo, grauInstr, indPriEmpr, nmSoc;

            public sNascimento nascimento;
            public struct sNascimento {
                public string codMunic;
                public string uf, paisNascto, paisNac, nmMae, nmPai;
                public string dtNascto;
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

        public sVinculo vinculo;
        public struct sVinculo {
            public string matricula, nrRecInfPrelim, cadIni;
            public string tpRegTrab, tpRegPrev;

            public sInfoRegimeTrab infoRegimeTrab;
            public struct sInfoRegimeTrab {

                public sInfoCeletista infoCeletista;
                public struct sInfoCeletista {
                    public string dtAdm, dtBase;
                    public string tpAdmissao, indAdmissao, tpRegJor, natAtividade;
                    public string cnpjSindCategProf;

                    public sFGTS FGTS;
                    public struct sFGTS {
                        public string dtOpcFGTS;
                        public string opcFGTS;
                    }

                    public sTrabTemporario trabTemporario;
                    public struct sTrabTemporario {
                        public string justContr;
                        public string hipLeg, tpInclContr;

                        public sIdeTomadorServ ideTomadorServ;
                        public struct sIdeTomadorServ {
                            public string nrInsc;
                            public string tpInsc;

                            public sIdeEstabVinc ideEstabVinc;
                            public struct sIdeEstabVinc {
                                public string nrInsc;
                                public string tpInsc;
                            }
                        }
                        public sIdeTrabSubstituido ideTrabSubstituido;
                        public struct sIdeTrabSubstituido { public string cpfTrabSubst; }
                    }
                    public sAprend aprend;
                    public struct sAprend {
                        public string nrInsc;
                        public string tpInsc;
                    }
                }
                public sInfoEstatutario infoEstatutario;
                public struct sInfoEstatutario {
                    public string indProvim, tpProv, tpPlanRP;
                    public string dtNomeacao, dtPosse, dtExercicio;

                    public sInfoDecJud infoDecJud;
                    public struct sInfoDecJud { public string nrProcJud; }
                }
            }
            public sInfoContrato infoContrato;
            public struct sInfoContrato {
                public string codCargo, codFuncao, codCarreira;
                public string codCateg;
                public string dtIngrCarr;

                public sRemuneracao remuneracao;
                public struct sRemuneracao {
                    public string dscSalVar;
                    public string vrSalFx;
                    public string undSalFixo;
                }

                public sDuracao duracao;
                public struct sDuracao {
                    public string dtTerm;
                    public string tpContr;
                    public string clauAssec;
                    public string objDet;
            }

                public sLocalTrabalho localTrabalho;
                public struct sLocalTrabalho {

                    public sLocalTrabGeral localTrabGeral;
                    public struct sLocalTrabGeral {
                        public string nrInsc, descComp;
                        public string tpInsc;
                    }

                    public sLocalTrabDom localTrabDom;
                    public struct sLocalTrabDom {
                        public string tpLograd, dscLograd, nrLograd, complemento, bairro, cep, uf;
                        public string codMunic;
                    }
                }
                public sHorContratual horContratual;
                public struct sHorContratual {
                    public string dscTpJorn, tmpParc;
                    public string qtdHrsSem;
                    public string tpJornada;

                    public sHorario horario;
                    public struct sHorario {
                        public string dia;
                        public string codHorContrat;
                    }
                }
                public sFiliacaoSindical filiacaoSindical;
                public struct sFiliacaoSindical { public string cnpjSindTrab; }

                public sAlvaraJudicial alvaraJudicial;
                public struct sAlvaraJudicial { public string nrProcJud; }

                public sObservacoes observacoes;
                public struct sObservacoes { public string observacao; }
            }

            public sSucessaoVinc sucessaoVinc;
            public struct sSucessaoVinc {
                public string tpInscAnt, cnpjEmpregAnt, matricAnt, observacao;
                public string dtTransf;
            }

            public sTransfDom transfDom;
            public struct sTransfDom {
                public string cpfSubstituido, matricAnt;
                public string dtTransf;
            }

            public sMudancaCPF mudancaCPF;
            public struct sMudancaCPF
            {
               public string cpfAnt, matricAnt, observacao;
               public string dtAltCPF;
            }

            public sAfastamento afastamento;
            public struct sAfastamento {
                public string codMotAfast;
                public string dtIniAfast;
            }

            public sDesligamento desligamento;
            public struct sDesligamento { public string dtDeslig; }

        }
        #endregion
    }
}