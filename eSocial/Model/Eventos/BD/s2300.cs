using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   public class s2300 : bEvento_BD
   {

      XML.s2300 s2300XML;

      public s2300() : base("2300", "Trabalhador sem vinculo de emprego", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {

            List<string> lista2300 = new List<string>();

            foreach (DataRow row in tbEventos.Rows)
            {
               // Só executa 1x para cada funcionário
               if (!lista2300.Contains(row["id_autonomo"].ToString()))
               {
                  // Registra o funcionário
                  lista2300.Add(row["id_autonomo"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2300XML = new XML.s2300(evento.id);

                  // ### Evento

                  // ideEvento
                  s2300XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2300XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2300XML.ideEvento.tpAmb = evento.tpAmb;
                  s2300XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2300XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2300XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2300XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // trabalhador
                  gcl.setLevel("trabalhador", row);

                  s2300XML.trabalhador.cpfTrab = gcl.getVal("cpfTrab");
                  s2300XML.trabalhador.nisTrab = gcl.getVal("nisTrab");
                  s2300XML.trabalhador.nmTrab = gcl.getVal("nmTrab");
                  s2300XML.trabalhador.sexo = gcl.getVal("sexo");
                  s2300XML.trabalhador.racaCor = gcl.getVal("racaCor");
                  s2300XML.trabalhador.estCiv = gcl.getVal("estCiv");         // 0.1
                  s2300XML.trabalhador.grauInstr = gcl.getVal("grauInstr");
                  s2300XML.trabalhador.nmSoc = gcl.getVal("nmSoc");

                  // nascimento
                  gcl.setLevel("nascimento", clear: true);

                  s2300XML.trabalhador.nascimento.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                  s2300XML.trabalhador.nascimento.codMunic = gcl.getVal("codMunic"); // 0.1
                  s2300XML.trabalhador.nascimento.uf = gcl.getVal("uf");             // 0.1
                  s2300XML.trabalhador.nascimento.paisNascto = gcl.getVal("paisNascto");
                  s2300XML.trabalhador.nascimento.paisNac = gcl.getVal("paisNac");
                  s2300XML.trabalhador.nascimento.nmMae = gcl.getVal("nmMae");
                  s2300XML.trabalhador.nascimento.nmPai = gcl.getVal("nmPai");

                  // CTPS 0.1
                  gcl.setLevel("ctps", clear: true);

                  s2300XML.trabalhador.documentos.CTPS.nrCtps = gcl.getVal("nrCtps");
                  s2300XML.trabalhador.documentos.CTPS.serieCtps = gcl.getVal("serieCtps");
                  s2300XML.trabalhador.documentos.CTPS.ufCtps = gcl.getVal("ufCtps");

                  // RIC 0.1
                  gcl.setLevel("ric", clear: true);

                  s2300XML.trabalhador.documentos.RIC.nrRic = gcl.getVal("nrRic");
                  s2300XML.trabalhador.documentos.RIC.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2300XML.trabalhador.documentos.RIC.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // RG 0.1
                  gcl.setLevel("rg", clear: true);

                  s2300XML.trabalhador.documentos.RG.nrRg = gcl.getVal("nrRg");
                  s2300XML.trabalhador.documentos.RG.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2300XML.trabalhador.documentos.RG.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // RNE 0.1
                  gcl.setLevel("rne", clear: true);

                  s2300XML.trabalhador.documentos.RNE.nrRne = gcl.getVal("nrRne");
                  s2300XML.trabalhador.documentos.RNE.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2300XML.trabalhador.documentos.RNE.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1

                  // OC 0.1
                  gcl.setLevel("oc", clear: true);

                  s2300XML.trabalhador.documentos.OC.nrOc = gcl.getVal("nrOc");
                  s2300XML.trabalhador.documentos.OC.orgaoEmissor = gcl.getVal("orgaoEmissor");
                  s2300XML.trabalhador.documentos.OC.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped")); // 0.1
                  s2300XML.trabalhador.documentos.OC.dtValid = validadores.aaaa_mm_dd(gcl.getVal("dtValid")); // 0.1

                  // CNH 0.1
                  gcl.setLevel("cnh", clear: true);

                  s2300XML.trabalhador.documentos.CNH.nrRegCnh = gcl.getVal("nrRegCnh");
                  s2300XML.trabalhador.documentos.CNH.dtExped = validadores.aaaa_mm_dd(gcl.getVal("dtExped"));   // 0.1
                  s2300XML.trabalhador.documentos.CNH.ufCnh = gcl.getVal("ufCnh");
                  s2300XML.trabalhador.documentos.CNH.dtValid = validadores.aaaa_mm_dd(gcl.getVal("dtValid"));
                  s2300XML.trabalhador.documentos.CNH.dtPriHab = validadores.aaaa_mm_dd(gcl.getVal("dtPriHab")); // 0.1
                  s2300XML.trabalhador.documentos.CNH.categoriaCnh = gcl.getVal("categoriaCnh");

                  // brasil 0.1
                  gcl.setLevel("brasil", clear: true);

                  s2300XML.trabalhador.endereco.brasil.tpLograd = gcl.getVal("tpLograd");
                  s2300XML.trabalhador.endereco.brasil.dscLograd = gcl.getVal("dscLograd");
                  s2300XML.trabalhador.endereco.brasil.nrLograd = gcl.getVal("nrLograd");
                  s2300XML.trabalhador.endereco.brasil.complemento = gcl.getVal("complemento"); // 0.1
                  s2300XML.trabalhador.endereco.brasil.bairro = gcl.getVal("bairro");           // 0.1
                  s2300XML.trabalhador.endereco.brasil.cep = gcl.getVal("cep");
                  s2300XML.trabalhador.endereco.brasil.codMunic = gcl.getVal("codMunic");
                  s2300XML.trabalhador.endereco.brasil.uf = gcl.getVal("uf");

                  // exterior 0.1
                  gcl.setLevel("exterior", clear: true);

                  s2300XML.trabalhador.endereco.exterior.paisResid = gcl.getVal("paisResid");
                  s2300XML.trabalhador.endereco.exterior.dscLograd = gcl.getVal("dscLograd");
                  s2300XML.trabalhador.endereco.exterior.nrLograd = gcl.getVal("nrLograd");
                  s2300XML.trabalhador.endereco.exterior.complemento = gcl.getVal("complemento"); // 0.1
                  s2300XML.trabalhador.endereco.exterior.bairro = gcl.getVal("bairro");           // 0.1
                  s2300XML.trabalhador.endereco.exterior.nmCid = gcl.getVal("nmCid");
                  s2300XML.trabalhador.endereco.exterior.codPostal = gcl.getVal("codPostal");     // 0.1

                  // trabEstrangeiro 0.1
                  gcl.setLevel("trabEstrangeiro", clear: true);

                  if (gcl.getVal("tmpResid") != "0")
                     s2300XML.trabalhador.trabImig.tmpResid = gcl.getVal("tmpResid");

                  if (gcl.getVal("condIng") != "0")
                     s2300XML.trabalhador.trabImig.condIng = gcl.getVal("condIng");

                  // infoDeficiencia 0.1
                  gcl.setLevel("infoDeficiencia", clear: true);

                  s2300XML.trabalhador.infoDeficiencia.defFisica = gcl.getVal("defFisica");
                  s2300XML.trabalhador.infoDeficiencia.defVisual = gcl.getVal("defVisual");
                  s2300XML.trabalhador.infoDeficiencia.defAuditiva = gcl.getVal("defAuditiva");
                  s2300XML.trabalhador.infoDeficiencia.defMental = gcl.getVal("defMental");
                  s2300XML.trabalhador.infoDeficiencia.defIntelectual = gcl.getVal("defIntelectual");
                  s2300XML.trabalhador.infoDeficiencia.reabReadap = gcl.getVal("reabReadap");
                  s2300XML.trabalhador.infoDeficiencia.infoCota = gcl.getVal("infoCota");
                  s2300XML.trabalhador.infoDeficiencia.observacao = gcl.getVal("observacao");          // 0.1

                  // dependente 0.99
                  List<string> lista2300dependente = new List<string>();

                  gcl.setLevel("dependente", clear: true);

                  var tbDependente = from DataRow r in tbEventos.Rows
                                     where !string.IsNullOrEmpty(r[$"tpDep{gcl.getLevel}"].ToString()) &&
                                     r["id_autonomo"].ToString().Equals(evento.id_funcionario)
                                     select r;

                  foreach (var dependente in tbDependente)
                  {

                     gcl.setLevel(row: dependente);

                     // Só executa 1x para cada dependente
                     if (!lista2300dependente.Contains(gcl.getVal("nmDep").ToString()))
                     {
                        // Registra o funcionário
                        lista2300dependente.Add(gcl.getVal("nmDep").ToString());

                        if (gcl.getVal("nmDep").ToString() != "")
                        {
                           s2300XML.trabalhador.dependente.tpDep = gcl.getVal("tpDep");
                           s2300XML.trabalhador.dependente.nmDep = gcl.getVal("nmDep");
                           s2300XML.trabalhador.dependente.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                           s2300XML.trabalhador.dependente.cpfDep = gcl.getVal("cpfDep");     // 0.1
                           s2300XML.trabalhador.dependente.depIRRF = gcl.getVal("depIRRF");
                           s2300XML.trabalhador.dependente.depSF = gcl.getVal("depSF");
                           s2300XML.trabalhador.dependente.incTrab = gcl.getVal("incTrab");

                           s2300XML.add_dependente();
                        }
                     }
                  }

                  // contato 0.1
                  gcl.setLevel("contato", clear: true);

                  s2300XML.trabalhador.contato.fonePrinc = gcl.getVal("fonePrinc");         // 0.1
                  s2300XML.trabalhador.contato.foneAlternat = gcl.getVal("foneAlternat");   // 0.1
                  s2300XML.trabalhador.contato.emailPrinc = gcl.getVal("emailPrinc");       // 0.1
                  s2300XML.trabalhador.contato.emailAlternat = gcl.getVal("emailAlternat"); // 0.1

                  // vinculo 0.1
                  gcl.setLevel("infoTSVInicio", clear: true);
                  string sCodCateg = gcl.getVal("codCateg"); ;

                  s2300XML.infoTSVInicio.cadIni = gcl.getVal("cadIni");
                  s2300XML.infoTSVInicio.codCateg = gcl.getVal("codCateg");
                  s2300XML.infoTSVInicio.dtInicio = validadores.aaaa_mm_dd(gcl.getVal("dtInicio"));
                  s2300XML.infoTSVInicio.matricula = gcl.getVal("matricula").ToString().Trim();

                  if (gcl.getVal("natAtividade")!="0")
                    s2300XML.infoTSVInicio.natAtividade = gcl.getVal("natAtividade"); // 0.1

                  // cargoFuncao 0.1
                  gcl.setLevel("cargoFuncao", clear: true);

                  s2300XML.infoTSVInicio.infoComplementares.cargoFuncao.nmCargo = gcl.getVal("nmCargo");
                  s2300XML.infoTSVInicio.infoComplementares.cargoFuncao.CBOCargo = gcl.getVal("CBOCargo");
                  s2300XML.infoTSVInicio.infoComplementares.cargoFuncao.codCargo = gcl.getVal("codCargo");
                  s2300XML.infoTSVInicio.infoComplementares.cargoFuncao.codFuncao = gcl.getVal("codFuncao");

                  // remuneracao 0.1
                  gcl.setLevel("remuneracao", clear: true);

                  s2300XML.infoTSVInicio.infoComplementares.remuneracao.vrSalFx = gcl.getVal("vrSalFx").Replace(",", ".");
                  s2300XML.infoTSVInicio.infoComplementares.remuneracao.undSalFixo = gcl.getVal("undSalFixo");
                  s2300XML.infoTSVInicio.infoComplementares.remuneracao.dscSalVar = gcl.getVal("dscSalVar");   // 0.1

                  // FGTS 0.1
                  gcl.setLevel("fgts", clear: true);

                  if (sCodCateg == "721")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.FGTS.opcFGTS = gcl.getVal("opcFGTS");
                     s2300XML.infoTSVInicio.infoComplementares.FGTS.dtOpcFGTS = validadores.aaaa_mm_dd(gcl.getVal("dtOpcFGTS")); // 0.1
                  }

                  // infoDirigenteSindical 0.1
                  gcl.setLevel("infoDirigenteSindical", clear: true);

                  if (gcl.getVal("categOrig") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.infoDirigenteSindical.categOrig = gcl.getVal("categOrig");
                     s2300XML.infoTSVInicio.infoComplementares.infoDirigenteSindical.cnpjOrigem = gcl.getVal("cnpjOrigem");
                     s2300XML.infoTSVInicio.infoComplementares.infoDirigenteSindical.dtAdmOrig = validadores.aaaa_mm_dd(gcl.getVal("dtAdmOrig"));
                     s2300XML.infoTSVInicio.infoComplementares.infoDirigenteSindical.matricOrig = gcl.getVal("matricOrig");
                  }

                  // infoTrabCedido 0.1
                  gcl.setLevel("infoTrabCedido", clear: true);

                  if (gcl.getVal("categOrig") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.categOrig = gcl.getVal("categOrig");
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.cnpCednt = gcl.getVal("cnpCednt");
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.matricCed = gcl.getVal("matricCed");
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.dtAdmCed = validadores.aaaa_mm_dd(gcl.getVal("dtAdmCed"));
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.tpRegTrab = gcl.getVal("tpRegTrab");
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.tpRegPrev = gcl.getVal("tpRegPrev");
                     s2300XML.infoTSVInicio.infoComplementares.infoTrabCedido.infOnus = gcl.getVal("infOnus");
                  }

                  // infoEstagiario 0.1
                  gcl.setLevel("infoEstagiario", clear: true);

                  if (gcl.getVal("natEstagio") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.natEstagio = gcl.getVal("natEstagio");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.nivEstagio = gcl.getVal("nivEstagio");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.areaAtuacao = gcl.getVal("areaAtuacao");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.nrApol = gcl.getVal("nrApol");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.vlrBolsa = gcl.getVal("vlrBolsa");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.dtPrevTeam = validadores.aaaa_mm_dd(gcl.getVal("dtPrevTeam"));
                  }

                  // instEnsino 
                  gcl.setLevel("instEnsino", row, clear: true);

                  if (gcl.getVal("cnpjInstEnsino") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.cnpjInstEnsino = gcl.getVal("cnpjInstEnsino");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.nmRazao = gcl.getVal("nmRazao");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.dscLograd = gcl.getVal("dscLograd");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.nrLograd = gcl.getVal("nrLograd");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.bairro = gcl.getVal("bairro");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.cep = gcl.getVal("cep");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.codMunic = gcl.getVal("codMunic");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.instEnsino.uf = gcl.getVal("uf");
                  }

                  // ageIntegracao 0.1
                  gcl.setLevel("ageIntegracao", clear: true);

                  if (gcl.getVal("cnpjAgntInteg") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg = gcl.getVal("cnpjAgntInteg");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.nrRazao = gcl.getVal("nrRazao");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.dscLograd = gcl.getVal("dscLograd");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.nrLograd = gcl.getVal("nrLograd");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.bairro = gcl.getVal("bairro");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.cep = gcl.getVal("cep");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.codMunic = gcl.getVal("codMunic");
                     s2300XML.infoTSVInicio.infoComplementares.infoEstagiario.ageIntegracao.uf = gcl.getVal("uf");
                  }

                  // localTrabGeral 0.1
                  gcl.setLevel("localTrabGeral", clear: true);

                  if (gcl.getVal("nrInsc") != "")
                  {
                     s2300XML.infoTSVInicio.infoComplementares.localTrabGeral.tpInsc = gcl.getVal("tpInsc");
                     s2300XML.infoTSVInicio.infoComplementares.localTrabGeral.nrInsc = gcl.getVal("nrInsc");
                     s2300XML.infoTSVInicio.infoComplementares.localTrabGeral.descComp = gcl.getVal("descComp");
                  }

                  //// mudancaCPF 0.1
                  //gcl.setLevel("mudancaCPF", clear: true);

                  //if (gcl.getVal("cpfAnt") != "")
                  //{
                  //   s2300XML.infoTSVInicio.mudancaCPF.cpfAnt = gcl.getVal("cpfAnt");
                  //   s2300XML.infoTSVInicio.mudancaCPF.dtAltCPF = validadores.aaaa_mm_dd(gcl.getVal("dtAltCPF"));
                  //   s2300XML.infoTSVInicio.mudancaCPF.observacao = gcl.getVal("observacao");
                  //}

                  //// afastamento 0.1
                  //gcl.setLevel("afastamento", clear: true);

                  //s2300XML.infoTSVInicio.afastamento.dtIniAfast = validadores.aaaa_mm_dd(gcl.getVal("dtIniAfast"));
                  //s2300XML.infoTSVInicio.afastamento.codMotAfast = gcl.getVal("codMotAfast");

                  //// afastamento 0.1
                  //gcl.setLevel("termino", clear: true);

                  //s2300XML.infoTSVInicio.termino.dtTerm = validadores.aaaa_mm_dd(gcl.getVal("dtTerm"));

                  evento.eventoAssinadoXML = s2300XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2300", e.Message); }
         return lEventos;
      }
   }
}
