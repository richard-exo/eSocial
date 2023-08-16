using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD {
   public class s2200 : bEvento_BD {

      XML.s2200 s2200XML;

      public s2200() : base("2200", "Cad. inicial do vínculo", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            List<string> lista2200 = new List<string>();

            foreach (DataRow row in tbEventos.Rows) {
               // Só executa 1x para cada funcionário
               if (!lista2200.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista2200.Add(row["id_funcionario"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2200XML = new XML.s2200(evento.id);

                  // ### Evento

                  // ideEvento
                  s2200XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2200XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2200XML.ideEvento.tpAmb = evento.tpAmb;
                  s2200XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2200XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2200XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2200XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // trabalhador
                  gcl.setLevel("trabalhador", row);

                  s2200XML.trabalhador.cpfTrab = gcl.getVal("cpfTrab");
                  s2200XML.trabalhador.nisTrab = gcl.getVal("nisTrab");
                  s2200XML.trabalhador.nmTrab = gcl.getVal("nmTrab");
                  s2200XML.trabalhador.sexo = gcl.getVal("sexo");
                  s2200XML.trabalhador.racaCor = gcl.getVal("racaCor");
                  s2200XML.trabalhador.estCiv = gcl.getVal("estCiv");         // 0.1
                  s2200XML.trabalhador.grauInstr = gcl.getVal("grauInstr");
                  s2200XML.trabalhador.indPriEmpr = gcl.getVal("indPriEmpr"); // 0.1
                  s2200XML.trabalhador.nmSoc = gcl.getVal("nmSoc");

                  // nascimento
                  gcl.setLevel("nascimento", clear: true);

                  s2200XML.trabalhador.nascimento.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                  s2200XML.trabalhador.nascimento.codMunic = gcl.getVal("codMunic"); // 0.1
                  s2200XML.trabalhador.nascimento.uf = gcl.getVal("uf");             // 0.1
                  s2200XML.trabalhador.nascimento.paisNascto = gcl.getVal("paisNascto");
                  s2200XML.trabalhador.nascimento.paisNac = gcl.getVal("paisNac");
                  s2200XML.trabalhador.nascimento.nmMae = gcl.getVal("nmMae");
                  s2200XML.trabalhador.nascimento.nmPai = gcl.getVal("nmPai");

                  // CTPS 0.1
                  gcl.setLevel("ctps", clear: true);

                  s2200XML.trabalhador.documentos.CTPS.nrCtps = gcl.getVal("nrCtps");
                  s2200XML.trabalhador.documentos.CTPS.serieCtps = gcl.getVal("serieCtps");
                  s2200XML.trabalhador.documentos.CTPS.ufCtps = gcl.getVal("ufCtps");

                  // RIC 0.1
                  gcl.setLevel("ric", clear: true);

                  s2200XML.trabalhador.documentos.RIC.nrRic = gcl.getVal("nrRic");
                  s2200XML.trabalhador.documentos.RIC.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2200XML.trabalhador.documentos.RIC.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // RG 0.1
                  gcl.setLevel("rg", clear: true);

                  s2200XML.trabalhador.documentos.RG.nrRg = gcl.getVal("nrRg");
                  s2200XML.trabalhador.documentos.RG.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2200XML.trabalhador.documentos.RG.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // RNE 0.1
                  gcl.setLevel("rne", clear: true);

                  s2200XML.trabalhador.documentos.RNE.nrRne = gcl.getVal("nrRne");
                  s2200XML.trabalhador.documentos.RNE.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2200XML.trabalhador.documentos.RNE.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // OC 0.1
                  gcl.setLevel("oc", clear: true);

                  s2200XML.trabalhador.documentos.OC.nrOc = gcl.getVal("nrOc");
                  s2200XML.trabalhador.documentos.OC.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2200XML.trabalhador.documentos.OC.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1
                  s2200XML.trabalhador.documentos.OC.dtValid = validadores.aaaa_mm_dd(gcl.getVal("dtValid")); // 0.1

                  // CNH 0.1
                  gcl.setLevel("cnh", clear: true);

                  s2200XML.trabalhador.documentos.CNH.nrRegCnh = gcl.getVal("nrRegCnh");
                  s2200XML.trabalhador.documentos.CNH.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped"));   // 0.1
                  s2200XML.trabalhador.documentos.CNH.ufCnh = gcl.getVal("ufCnh");
                  s2200XML.trabalhador.documentos.CNH.dtValid = validadores.aaaa_mm_dd(gcl.getVal("dtValid"));
                  s2200XML.trabalhador.documentos.CNH.dtPriHab = validadores.aaaa_mm_dd(gcl.getVal("dtPriHab")); // 0.1
                  s2200XML.trabalhador.documentos.CNH.categoriaCnh = gcl.getVal("categoriaCnh");

                  // brasil 0.1
                  gcl.setLevel("brasil", clear: true);

                  s2200XML.trabalhador.endereco.brasil.tpLograd = gcl.getVal("tpLograd");
                  s2200XML.trabalhador.endereco.brasil.dscLograd = gcl.getVal("dscLograd");
                  s2200XML.trabalhador.endereco.brasil.nrLograd = gcl.getVal("nrLograd");
                  s2200XML.trabalhador.endereco.brasil.complemento = gcl.getVal("complemento"); // 0.1
                  s2200XML.trabalhador.endereco.brasil.bairro = gcl.getVal("bairro");           // 0.1
                  s2200XML.trabalhador.endereco.brasil.cep = gcl.getVal("cep");
                  s2200XML.trabalhador.endereco.brasil.codMunic = gcl.getVal("codMunic");
                  s2200XML.trabalhador.endereco.brasil.uf = gcl.getVal("uf");

                  // exterior 0.1
                  gcl.setLevel("exterior", clear: true);

                  s2200XML.trabalhador.endereco.exterior.paisResid = gcl.getVal("paisResid");
                  s2200XML.trabalhador.endereco.exterior.dscLograd = gcl.getVal("dscLograd");
                  s2200XML.trabalhador.endereco.exterior.nrLograd = gcl.getVal("nrLograd");
                  s2200XML.trabalhador.endereco.exterior.complemento = gcl.getVal("complemento"); // 0.1
                  s2200XML.trabalhador.endereco.exterior.bairro = gcl.getVal("bairro");           // 0.1
                  s2200XML.trabalhador.endereco.exterior.nmCid = gcl.getVal("nmCid");
                  s2200XML.trabalhador.endereco.exterior.codPostal = gcl.getVal("codPostal");     // 0.1

                  // trabEstrangeiro 0.1
                  gcl.setLevel("trabEstrangeiro", clear: true);

                  s2200XML.trabalhador.trabImig.tmpResid = gcl.getVal("tmpResid");
                  s2200XML.trabalhador.trabImig.condIng = gcl.getVal("condIng");

                  // infoDeficiencia 0.1
                  gcl.setLevel("infoDeficiencia", clear: true);

                  s2200XML.trabalhador.infoDeficiencia.defFisica = gcl.getVal("defFisica");
                  s2200XML.trabalhador.infoDeficiencia.defVisual = gcl.getVal("defVisual");
                  s2200XML.trabalhador.infoDeficiencia.defAuditiva = gcl.getVal("defAuditiva");
                  s2200XML.trabalhador.infoDeficiencia.defMental = gcl.getVal("defMental");
                  s2200XML.trabalhador.infoDeficiencia.defIntelectual = gcl.getVal("defIntelectual");
                  s2200XML.trabalhador.infoDeficiencia.reabReadap = gcl.getVal("reabReadap");
                  s2200XML.trabalhador.infoDeficiencia.infoCota = gcl.getVal("infoCota");
                  s2200XML.trabalhador.infoDeficiencia.observacao = gcl.getVal("observacao");          // 0.1

                  // dependente 0.99
                  List<string> lista2200dependente = new List<string>();

                  gcl.setLevel("dependente", clear: true);

                  var tbDependente = from DataRow r in tbEventos.Rows
                                     where !string.IsNullOrEmpty(r[$"tpDep{gcl.getLevel}"].ToString()) &&
                                     r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                     select r;

                  foreach (var dependente in tbDependente) {

                     gcl.setLevel(row: dependente);

                     // Só executa 1x para cada dependente
                     if (!lista2200dependente.Contains(gcl.getVal("nmDep").ToString()))
                     {
                        // Registra o funcionário
                        lista2200dependente.Add(gcl.getVal("nmDep").ToString());

                        if (gcl.getVal("nmDep").ToString() != "")
                        {
                           s2200XML.trabalhador.dependente.tpDep = gcl.getVal("tpDep");
                           s2200XML.trabalhador.dependente.nmDep = gcl.getVal("nmDep");
                           s2200XML.trabalhador.dependente.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                           s2200XML.trabalhador.dependente.cpfDep = gcl.getVal("cpfDep");     // 0.1
                           s2200XML.trabalhador.dependente.depIRRF = gcl.getVal("depIRRF");
                           s2200XML.trabalhador.dependente.depSF = gcl.getVal("depSF");
                           s2200XML.trabalhador.dependente.incTrab = gcl.getVal("incTrab");

                           s2200XML.add_dependente();
                        }
                     }
                  }

                  // aposentadoria 0.1
                  gcl.setLevel("aposentadoria", row, clear: true);

                  s2200XML.trabalhador.aposentadoria.trabAposent = gcl.getVal("trabAposent");

                  // contato 0.1
                  gcl.setLevel("contato", clear: true);

                  s2200XML.trabalhador.contato.fonePrinc = gcl.getVal("fonePrinc");         // 0.1
                  s2200XML.trabalhador.contato.foneAlternat = gcl.getVal("foneAlternat");   // 0.1
                  s2200XML.trabalhador.contato.emailPrinc = gcl.getVal("emailPrinc");       // 0.1
                  s2200XML.trabalhador.contato.emailAlternat = gcl.getVal("emailAlternat"); // 0.1

                  // vinculo 0.1
                  gcl.setLevel("vinculo", clear: true);

                  s2200XML.vinculo.matricula = gcl.getVal("matricula");
                  s2200XML.vinculo.tpRegTrab = gcl.getVal("tpRegTrab");
                  s2200XML.vinculo.tpRegPrev = gcl.getVal("tpRegPrev");
                  s2200XML.vinculo.nrRecInfPrelim = gcl.getVal("nrRecInfPrelim"); // 0.1
                  s2200XML.vinculo.cadIni = gcl.getVal("cadIni");

                  // infoCeletista 0.1
                  gcl.setLevel("infoCeletista", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.dtAdm = validadores.aaaa_mm_dd(gcl.getVal("dtAdm"));
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.tpAdmissao = gcl.getVal("tpAdmissao");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.indAdmissao = gcl.getVal("indAdmissao");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.tpRegJor = gcl.getVal("tpRegJor");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.natAtividade = gcl.getVal("natAtividade");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.dtBase = gcl.getVal("dtBase");                       // 0.1
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.cnpjSindCategProf = gcl.getVal("cnpjSindCategProf");

                  // FGTS 0.1
                  gcl.setLevel("fgts", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.FGTS.opcFGTS = gcl.getVal("opcFGTS");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.FGTS.dtOpcFGTS = validadores.aaaa_mm_dd(gcl.getVal("dtOpcFGTS")); // 0.1

                  // trabTemporario 0.1
                  gcl.setLevel("trabTemporario", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.hipLeg = gcl.getVal("hipLeg");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.justContr = gcl.getVal("justContr");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.tpInclContr = gcl.getVal("tpInclContr");

                  // ideTomadorServ
                  gcl.setLevel("ideTomadorServ", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.tpInsc = gcl.getVal("tpInsc");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.nrInsc = gcl.getVal("nrInsc");

                  // ideEstabVinc 0.1
                  gcl.setLevel("ideEstabVinc", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc.tpInsc = gcl.getVal("tpInsc");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTomadorServ.ideEstabVinc.nrInsc = gcl.getVal("nrInsc");

                  // ideTrabSubstituido 0.9
                  gcl.setLevel("ideTrabSubstituido", clear: true);

                  var tbIdeTrabSubstituido = from DataRow r in tbEventos.Rows
                                             where !string.IsNullOrEmpty(r[$"cpfTrabSubst{gcl.getLevel}"].ToString()) &&
                                             r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                             select r;

                  foreach (var ideTrabSubstituido in tbIdeTrabSubstituido) {

                     gcl.setLevel(row: ideTrabSubstituido);

                     s2200XML.vinculo.infoRegimeTrab.infoCeletista.trabTemporario.ideTrabSubstituido.cpfTrabSubst = gcl.getVal("cpfTrabSubst");

                     s2200XML.add_ideTrabSubstituido();
                  }

                  // aprend 0.1
                  gcl.setLevel("aprend", row, clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.aprend.tpInsc = gcl.getVal("tpInsc");
                  s2200XML.vinculo.infoRegimeTrab.infoCeletista.aprend.nrInsc = gcl.getVal("nrInsc");

                  // infoEstatutario 0.1
                  gcl.setLevel("infoEstatutario", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.indProvim = gcl.getVal("indProvim");
                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.tpProv = gcl.getVal("tpProv");
                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.dtNomeacao = validadores.aaaa_mm_dd(gcl.getVal("dtNomeacao"));
                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.dtPosse = validadores.aaaa_mm_dd(gcl.getVal("dtPosse"));
                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.dtExercicio = validadores.aaaa_mm_dd(gcl.getVal("dtExercicio"));
                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.tpPlanRP = gcl.getVal("tpPlanRP");       // 0.1

                  // infoDecJud 0.1
                  gcl.setLevel("infoDecJud", clear: true);

                  s2200XML.vinculo.infoRegimeTrab.infoEstatutario.infoDecJud.nrProcJud = gcl.getVal("nrProcJud");

                  // infoContrato 0.1
                  gcl.setLevel("infoContrato", clear: true);

                  s2200XML.vinculo.infoContrato.nmCargo = gcl.getVal("nmCargo");                                  // 0.1
                  s2200XML.vinculo.infoContrato.CBOCargo = gcl.getVal("CBOCargo");                                // 0.1
                  s2200XML.vinculo.infoContrato.nmFuncao = gcl.getVal("nmFuncao");                                // 0.1
                  s2200XML.vinculo.infoContrato.CBOfuncao = gcl.getVal("CBOfuncao");                              // 0.1
                  s2200XML.vinculo.infoContrato.acumCargo = gcl.getVal("acumCargo");                              // 0.1
                  s2200XML.vinculo.infoContrato.dtIngrCarr = validadores.aaaa_mm_dd(gcl.getVal("dtIngrCarr"));    // 0.1
                  s2200XML.vinculo.infoContrato.codCateg = gcl.getVal("codCateg");

                  // remuneracao
                  gcl.setLevel("remuneracao", clear: true);

                  s2200XML.vinculo.infoContrato.remuneracao.vrSalFx = gcl.getVal("vrSalFx").Replace(",", ".");
                  s2200XML.vinculo.infoContrato.remuneracao.undSalFixo = gcl.getVal("undSalFixo");
                  s2200XML.vinculo.infoContrato.remuneracao.dscSalVar = gcl.getVal("dscSalVar");   // 0.1

                  // duracao
                  gcl.setLevel("duracao", clear: true);

                  s2200XML.vinculo.infoContrato.duracao.tpContr = gcl.getVal("tpContr");
                  s2200XML.vinculo.infoContrato.duracao.dtTerm = validadores.aaaa_mm_dd(gcl.getVal("dtTerm"));       // 0.1
                  s2200XML.vinculo.infoContrato.duracao.clauAssec = gcl.getVal("clauAssec"); // 0.1
                  s2200XML.vinculo.infoContrato.duracao.objDet = gcl.getVal("objDet"); // 0.1

                  // localTrabGeral 0.1
                  gcl.setLevel("localTrabGeral", clear: true);

                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabGeral.tpInsc = gcl.getVal("tpInsc");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabGeral.nrInsc = gcl.getVal("nrInsc");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabGeral.descComp = gcl.getVal("descComp"); // 0.1

                  // localTrabDom 0.1
                  gcl.setLevel("localTrabDom", clear: true);

                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.tpLograd = gcl.getVal("tpLograd");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.dscLograd = gcl.getVal("dscLograd");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.nrLograd = gcl.getVal("nrLograd");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.complemento = gcl.getVal("complemento"); // 0.1
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.bairro = gcl.getVal("bairro");           // 0.1
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.cep = gcl.getVal("cep");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.codMunic = gcl.getVal("codMunic");
                  s2200XML.vinculo.infoContrato.localTrabalho.localTrabDom.uf = gcl.getVal("uf");

                  // horContratual 0.1
                  gcl.setLevel("horContratual", clear: true);

                  s2200XML.vinculo.infoContrato.horContratual.qtdHrsSem = gcl.getVal("qtdHrsSem");    // 0.1
                  s2200XML.vinculo.infoContrato.horContratual.tpJornada = gcl.getVal("tpJornada");
                  s2200XML.vinculo.infoContrato.horContratual.horNoturno = gcl.getVal("horNoturno");  // 0.1
                  s2200XML.vinculo.infoContrato.horContratual.dscJorn = gcl.getVal("dscJorn");        // 0.1
                  s2200XML.vinculo.infoContrato.horContratual.tmpParc = gcl.getVal("tmpParc");

                  // horario 0.9
                  List<string> lista2200horario = new List<string>();

                  gcl.setLevel("horario", clear: true);

                  var tbHorario = from DataRow r in tbEventos.Rows
                                  where !string.IsNullOrEmpty(r[$"dia{gcl.getLevel}"].ToString()) &&
                                  r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                  select r;

                  foreach (var horario in tbHorario) {
                     gcl.setLevel(row: horario);

                     if (!lista2200horario.Contains(gcl.getVal("codHorContrat").ToString()))
                     {
                        // Registra codHorContrat
                        lista2200horario.Add(gcl.getVal("codHorContrat").ToString());

                        s2200XML.vinculo.infoContrato.horContratual.horario.dia = gcl.getVal("dia");
                        s2200XML.vinculo.infoContrato.horContratual.horario.codHorContrat = gcl.getVal("codHorContrat");
                        s2200XML.add_horario();
                     }
                  }

                  // filiacaoSindical 0.2
                  gcl.setLevel("filiacaoSindical", clear: true);

                  var tbFiliacaoSindical = from DataRow r in tbEventos.Rows
                                           where !string.IsNullOrEmpty(r[$"cnpjSindTrab{gcl.getLevel}"].ToString()) &&
                                           r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                           select r;

                  foreach (var filiacaoSindical in tbFiliacaoSindical) {

                     gcl.setLevel(row: filiacaoSindical);

                     s2200XML.vinculo.infoContrato.filiacaoSindical.cnpjSindTrab = gcl.getVal("cnpjSindTrab");
                     s2200XML.add_filiacaoSindical();
                  }

                  // alvaraJudicial 0.1
                  gcl.setLevel("alvaraJudicial", row, clear: true);

                  s2200XML.vinculo.infoContrato.alvaraJudicial.nrProcJud = gcl.getVal("nrProcJud");

                  // observacoes 0.99
                  gcl.setLevel("observacoes", clear: true);

                  var tbObservacoes = from DataRow r in tbEventos.Rows
                                      where !string.IsNullOrEmpty(r[$"observacao{gcl.getLevel}"].ToString()) &&
                                      r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                      select r;

                  foreach (var observacoes in tbObservacoes) {

                     gcl.setLevel(row: observacoes);

                     s2200XML.vinculo.infoContrato.observacoes.observacao = gcl.getVal("observacao");
                     s2200XML.add_observacoes();
                  }

                  // sucessaoVinc 0.1
                  gcl.setLevel("sucessaoVinc", row, clear: true);

                  s2200XML.vinculo.sucessaoVinc.tpInscAnt = gcl.getVal("tpInscAnt");
                  s2200XML.vinculo.sucessaoVinc.cnpjEmpregAnt = gcl.getVal("cnpjEmpregAnt");
                  s2200XML.vinculo.sucessaoVinc.matricAnt = gcl.getVal("matricAnt");         // 0.1
                  s2200XML.vinculo.sucessaoVinc.dtTransf = validadores.aaaa_mm_dd(gcl.getVal("dtTransf"));
                  s2200XML.vinculo.sucessaoVinc.observacao = gcl.getVal("observacao");       // 0.1

                  // transfDom 0.1
                  gcl.setLevel("transfDom", clear: true);

                  s2200XML.vinculo.transfDom.cpfSubstituido = gcl.getVal("cpfSubstituido");
                  s2200XML.vinculo.transfDom.matricAnt = gcl.getVal("matricAnt");           // 0.1
                  s2200XML.vinculo.transfDom.dtTransf = validadores.aaaa_mm_dd(gcl.getVal("dtTransf"));

                  // mudancaCPF 0.1
                  gcl.setLevel("mudancaCPF", clear: true);

                  s2200XML.vinculo.mudancaCPF.cpfAnt= gcl.getVal("cpfAnt");
                  s2200XML.vinculo.mudancaCPF.matricAnt = gcl.getVal("matricAnt");           // 0.1
                  s2200XML.vinculo.mudancaCPF.dtAltCPF= validadores.aaaa_mm_dd(gcl.getVal("dtAltCPF"));
                  s2200XML.vinculo.mudancaCPF.observacao = gcl.getVal("observacao");

                  // afastamento 0.1
                  gcl.setLevel("afastamento", clear: true);

                  s2200XML.vinculo.afastamento.dtIniAfast = validadores.aaaa_mm_dd(gcl.getVal("dtIniAfast"));
                  s2200XML.vinculo.afastamento.codMotAfast = gcl.getVal("codMotAfast");

                  // desligamento 0.1
                  gcl.setLevel("desligamento", clear: true);

                  s2200XML.vinculo.desligamento.dtDeslig = validadores.aaaa_mm_dd(gcl.getVal("dtDeslig"));

                  evento.eventoAssinadoXML = s2200XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2200", e.Message); }
         return lEventos;
      }
   }
}
