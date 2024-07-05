using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static eSocial.Model.Eventos.XML.s2500.sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo;
using static eSocial.Model.Eventos.XML.s2500.sIdeTrab.sInfoContr.sIdeEstab;
using static eSocial.Model.Eventos.XML.s2500.sIdeTrab.sInfoContr.sInfoCompl;
using static eSocial.Model.Eventos.XML.s2500.sIdeTrab.sInfoContr;
using static eSocial.Model.Eventos.XML.s2500.sIdeTrab;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso.sDadosCompl;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso;
using static eSocial.cBase;

namespace eSocial.Model.Eventos.BD
{
   class s2500 : bEvento_BD
   {

      XML.s2500 s2500XML;

      public s2500() : base("2500", "Processo Trabalhista", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {
            List<string> lista2500 = new List<string>();
            List<string> lista2500Dep = new List<string>();
            List<string> lista2500Contrato = new List<string>();
            List<string> lista2500Remuneracao = new List<string>();
            List<string> lista2500Obs = new List<string>();
            List<string> lista2500Mudanca = new List<string>();
            List<string> lista2500Unic = new List<string>();
            List<string> lista2500Periodo = new List<string>();
            List<string> lista2500BaseCalculo = new List<string>();
            List<string> lista2500InfoFGTS = new List<string>();
            List<string> lista2500BaseMudaCateg = new List<string>();
            string sIDchave = "", sIDchave2="", sIDchave3="", sIDchave4="", sIDchave5="", sIDchave6="", sIDchave7="", sIDchave8="", sIDchave9="";

            foreach (DataRow row in tbEventos.Rows)
            {
               // Só executa 1x para cada funcionário
               if (!lista2500.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista2500.Add(row["id_funcionario"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2500XML = new XML.s2500(evento.id);

                  // ### Evento

                  // ideEvento
                  s2500XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2500XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2500XML.ideEvento.tpAmb = evento.tpAmb;
                  s2500XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2500XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2500XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2500XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideResp.. somente se houver imposição de responsabilidade indireta (avaliar)
                  //s2500XML.ideResp.tpInsc = row["tpInscResp"].ToString();
                  //s2500XML.ideResp.nrInsc = row["nrInscResp"].ToString();

                  // infoProcesso 1
                  s2500XML.infoProcesso.origem = row["origem_infoProcesso"].ToString();
                  s2500XML.infoProcesso.nrProcTrab = row["nrProcTrab_infoProcesso"].ToString();
                  s2500XML.infoProcesso.obsProcTrab = row["obsProcTrab_infoProcesso"].ToString();

                  // dadosCompl 1
                  //// infoProcJud 0 - 1
                  s2500XML.infoProcesso.dadosCompl.infoProcJud.dtSent = validadores.aaaa_mm_dd(row["dtSent_infoProcJud"].ToString());
                  s2500XML.infoProcesso.dadosCompl.infoProcJud.ufVara = row["ufVara_infoProcJud"].ToString();
                  s2500XML.infoProcesso.dadosCompl.infoProcJud.codMunic = row["codMunic_infoProcJud"].ToString();
                  s2500XML.infoProcesso.dadosCompl.infoProcJud.idVara = row["idVara_infoProcJud"].ToString();

                  //// infoCCP 0 - 1
                  s2500XML.infoProcesso.dadosCompl.infoCCP.dtCCP = validadores.aaaa_mm_dd(row["dtCCP_infoCCP"].ToString());
                  s2500XML.infoProcesso.dadosCompl.infoCCP.tpCCP = row["tpCCP_infoCCP"].ToString();
                  s2500XML.infoProcesso.dadosCompl.infoCCP.cnpjCCP = row["cnpjCCP_infoCCP"].ToString();

                  //// ideTrab 1
                  s2500XML.ideTrab.cpfTrab = row["cpfTrab_ideTrab"].ToString();
                  s2500XML.ideTrab.nmTrab = row["nmTrab_ideTrab"].ToString();
                  s2500XML.ideTrab.dtNascto = validadores.aaaa_mm_dd(row["dtNascto_dtNascto"].ToString());

                  //// dependente 0 - 99
                  gcl.setLevel("dependente");

                  var tbDep = from DataRow r in tbEventos.Rows
                                   where !string.IsNullOrEmpty(r[$"cpfDep{gcl.getLevel}"].ToString()) &&
                                   r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                   select r;

                  foreach (var dep in tbDep)
                  {
                     gcl.setLevel(row: dep);
                     sIDchave = gcl.getVal("id").ToString();

                     // Só executa 1x para cada funcionario
                     if (!lista2500Dep.Contains(sIDchave))
                     {
                        lista2500Dep.Add(sIDchave);
                        s2500XML.ideTrab.dependente.cpfDep = gcl.getVal("cpfDep");
                        s2500XML.ideTrab.dependente.tpDep = gcl.getVal("tpDep");
                        s2500XML.ideTrab.dependente.descDep = gcl.getVal("descDep");

                        s2500XML.add_dependente();
                     }
                  }

                  //// infoContr 1 - 99
                  gcl.setLevel("infoContr", clear: true);
                  //gcl.setLevel("infoContr");

                  var tbContrato = from DataRow r in tbEventos.Rows
                              where !string.IsNullOrEmpty(r[$"tpContr{gcl.getLevel}"].ToString()) &&
                              r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                              select r;

                  foreach (var infoContr in tbContrato)
                  {
                     gcl.setLevel(row: infoContr);
                     sIDchave = gcl.getVal("id").ToString();

                     // Só executa 1x para cada funcionario
                     if (!lista2500Contrato.Contains(sIDchave))
                     {
                        lista2500Contrato.Add(sIDchave);
                        s2500XML.ideTrab.infoContr.tpContr = gcl.getVal("tpContr");
                        s2500XML.ideTrab.infoContr.indContr = gcl.getVal("indContr");
                        s2500XML.ideTrab.infoContr.dtAdmOrig = validadores.aaaa_mm_dd(gcl.getVal("dtAdmOrig"));
                        s2500XML.ideTrab.infoContr.indReint = gcl.getVal("indReint");
                        s2500XML.ideTrab.infoContr.indCateg = gcl.getVal("indCateg");
                        s2500XML.ideTrab.infoContr.indNatAtiv = gcl.getVal("indNatAtiv");
                        s2500XML.ideTrab.infoContr.indMotDeslig = gcl.getVal("indMotDeslig");
                        s2500XML.ideTrab.infoContr.indUnic = gcl.getVal("indUnic");
                        s2500XML.ideTrab.infoContr.matricula = gcl.getVal("matricula");
                        s2500XML.ideTrab.infoContr.codCateg = gcl.getVal("codCateg");
                        s2500XML.ideTrab.infoContr.dtInicio = validadores.aaaa_mm_dd(gcl.getVal("dtInicio"));

                        // infoCompl 0 - 1
                        s2500XML.ideTrab.infoContr.infoCompl.codCBO = gcl.getVal("codCBO_infoCompl");
                        s2500XML.ideTrab.infoContr.infoCompl.natAtividade = gcl.getVal("natAtividade_infoCompl");

                        // remuneracao 0 - 99
                        gcl.setLevel("remuneracao", clear: true);

                        var tbRemuneracao = from DataRow r in tbEventos.Rows
                                         where !string.IsNullOrEmpty(r[$"undSalFixo{gcl.getLevel}"].ToString()) &&
                                         r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                         r["id_infoContr"].ToString().Equals(sIDchave)
                                         select r;

                        foreach (var remuneracao in tbRemuneracao)
                        {
                           gcl.setLevel(row: remuneracao);
                           sIDchave2 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada remuneracao
                           if (!lista2500Remuneracao.Contains(sIDchave2))
                           {
                              lista2500Remuneracao.Add(sIDchave2);
                              s2500XML.ideTrab.infoContr.infoCompl.remuneracao.dtRemun = validadores.aaaa_mm_dd(gcl.getVal("dtRemun"));
                              s2500XML.ideTrab.infoContr.infoCompl.remuneracao.vrSalFx = gcl.getVal("vrSalFx").Replace(",", ".");
                              s2500XML.ideTrab.infoContr.infoCompl.remuneracao.undSalFixo = gcl.getVal("undSalFixo");
                              s2500XML.ideTrab.infoContr.infoCompl.remuneracao.dscSalVar = gcl.getVal("dscSalVar");

                              s2500XML.add_remuneracao();
                           }
                        }

                        gcl.setLevel("infoVinc", clear: true);

                        //// infoVinc 0 - 1
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.tpRegTrab = gcl.getVal("tpRegTrab");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.tpRegPrev = gcl.getVal("tpRegPrev");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.dtAdm = validadores.aaaa_mm_dd(gcl.getVal("dtAdm"));
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.tmpParc = gcl.getVal("tmpParc");

                        gcl.setLevel("duracao", clear: true);

                        //// duracao 0 - 1
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.duracao.tpContr = gcl.getVal("tpContr");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.duracao.dtTerm = validadores.aaaa_mm_dd(gcl.getVal("dtTerm"));
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.duracao.clauAssec = gcl.getVal("clauAssec");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.duracao.objDet = gcl.getVal("objDet");

                        //// observacoes 0 - 99
                        gcl.setLevel("observacoes", clear: true);

                        var tbObs = from DataRow r in tbEventos.Rows
                                            where !string.IsNullOrEmpty(r[$"id{gcl.getLevel}"].ToString()) &&
                                            r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                            r["id_infoContr"].ToString().Equals(sIDchave)
                                            select r;

                        foreach (var observacoes in tbObs)
                        {
                           gcl.setLevel(row: observacoes);
                           sIDchave3 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada obs
                           if (!lista2500Obs.Contains(sIDchave3))
                           {
                              lista2500Obs.Add(sIDchave3);
                              s2500XML.ideTrab.infoContr.infoCompl.infoVinc.observacoes.observacao = gcl.getVal("observacao");
                              s2500XML.add_observacoes();
                           }
                        }

                        gcl.setLevel("sucessaoVinc", clear: true);

                        //// sucessaoVinc 0 - 1
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.tpInsc  = gcl.getVal("tpInsc");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.nrInsc = gcl.getVal("nrInsc");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.matricAnt = gcl.getVal("matricAnt");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.dtTransf = validadores.aaaa_mm_dd(gcl.getVal("dtTransf"));

                        gcl.setLevel("infoDeslig", clear: true);

                        //// infoDeslig 1
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.dtDeslig = validadores.aaaa_mm_dd(gcl.getVal("dtDeslig"));
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.mtvDeslig = gcl.getVal("mtvDeslig");
                        s2500XML.ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.dtProjFimAPI = validadores.aaaa_mm_dd(gcl.getVal("dtProjFimAPI"));


                        gcl.setLevel("infoTerm", clear: true);

                        //// infoTerm 0 - 1
                        s2500XML.ideTrab.infoContr.infoCompl.infoTerm.dtTerm = validadores.aaaa_mm_dd(gcl.getVal("dtTerm"));
                        s2500XML.ideTrab.infoContr.infoCompl.infoTerm.mtvDesligTSV = gcl.getVal("mtvDesligTSV");

                        //// mudCategAtiv 0 - 99
                        gcl.setLevel("mudCategAtiv", clear: true);

                        var tbMudanca = from DataRow r in tbEventos.Rows
                                            where !string.IsNullOrEmpty(r[$"codCateg{gcl.getLevel}"].ToString()) &&
                                            r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                            r["id_infoContr"].ToString().Equals(sIDchave)
                                            select r;

                        foreach (var mudCategAtiv in tbMudanca)
                        {
                           gcl.setLevel(row: mudCategAtiv);
                           sIDchave4 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada mudança
                           if (!lista2500Mudanca.Contains(sIDchave4))
                           {
                              lista2500Mudanca.Add(sIDchave4);
                              s2500XML.ideTrab.infoContr.mudCategAtiv.codCateg = gcl.getVal("codCateg");
                              s2500XML.ideTrab.infoContr.mudCategAtiv.natAtividade = gcl.getVal("natAtividade");
                              s2500XML.ideTrab.infoContr.mudCategAtiv.dtMudCategAtiv = validadores.aaaa_mm_dd(gcl.getVal("dtMudCategAtiv"));

                              s2500XML.add_mudaCategAtiv();
                           }
                        }


                        //// unicContr 0 - 99
                        gcl.setLevel("unicContr", clear:true);

                        var tbUnic = from DataRow r in tbEventos.Rows
                                        where !string.IsNullOrEmpty(r[$"matUnic{gcl.getLevel}"].ToString()) &&
                                        r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                        r["id_infoContr"].ToString().Equals(sIDchave)
                                        select r;

                        foreach (var unicContr in tbUnic)
                        {
                           gcl.setLevel(row: unicContr);
                           sIDchave5 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada mudança
                           if (!lista2500Unic.Contains(sIDchave5))
                           {
                              lista2500Unic.Add(sIDchave5);
                              s2500XML.ideTrab.infoContr.unicContr.matUnic = gcl.getVal("matUnic");
                              s2500XML.ideTrab.infoContr.unicContr.codCateg = gcl.getVal("codCateg");
                              s2500XML.ideTrab.infoContr.unicContr.dtInicio = validadores.aaaa_mm_dd(gcl.getVal("dtInicio"));

                              s2500XML.add_unicContr();
                           }
                        }

                        gcl.setLevel("ideEstab", clear: true);

                        //// ideEstab 1
                        s2500XML.ideTrab.infoContr.ideEstab.tpInsc = gcl.getVal("tpInsc");
                        s2500XML.ideTrab.infoContr.ideEstab.nrInsc = gcl.getVal("nrInsc");


                        gcl.setLevel("infoVlr", clear: true);

                        //// infoVlr 1
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.compIni = validadores.aaaa_mm(gcl.getVal("compIni"));
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.compFim = validadores.aaaa_mm(gcl.getVal("compFim"));
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.indReperc = gcl.getVal("indReperc");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.indenSD = gcl.getVal("indenSD");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.indenAbono = gcl.getVal("indenAbono");
                        //s2500XML.ideTrab.infoContr.ideEstab.infoVlr.repercProc = gcl.getVal("repercProc");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.vrRemun = gcl.getVal("vrRemun").Replace(",", ".");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.vrAPI= gcl.getVal("vrAPI").Replace(",", ".");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.vr13API= gcl.getVal("vr13API").Replace(",", ".");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.vrInden = gcl.getVal("vrInden").Replace(",", ".");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.vrBaseIndenFGTS= gcl.getVal("vrBaseIndenFGTS").Replace(",", ".");
                        s2500XML.ideTrab.infoContr.ideEstab.infoVlr.pagDiretoResc= gcl.getVal("pagDiretoResc");

                        //// idePeriodo 0 - 360
                        gcl.setLevel("idePeriodo", clear: true);

                        var tbPeriodo = from DataRow r in tbEventos.Rows
                                     where !string.IsNullOrEmpty(r[$"perRef{gcl.getLevel}"].ToString()) &&
                                     r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                     r["id_infoContr"].ToString().Equals(sIDchave)
                                     select r;

                        foreach (var idePeriodo in tbPeriodo)
                        {
                           gcl.setLevel(row: idePeriodo);
                           sIDchave6 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada periodo
                           if (!lista2500Periodo.Contains(sIDchave6))
                           {
                              lista2500Periodo.Add(sIDchave6);
                              s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.perRef = validadores.aaaa_mm(gcl.getVal("perRef"));

                              gcl.setLevel("baseCalculo", clear: true);

                              var tbPeriodo2 = from DataRow r in tbEventos.Rows
                                              where //!string.IsNullOrEmpty(r[$"perRef{gcl.getLevel}"].ToString()) &&
                                              r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                              r["id_infoContr"].ToString().Equals(sIDchave) &&
                                              r["id_idePeriodo"].ToString().Equals(sIDchave6)
                                               select r;

                              foreach (var idePeriodo2 in tbPeriodo2)
                              {
                                 gcl.setLevel(row: idePeriodo2);
                                 sIDchave7 = gcl.getVal("id").ToString();

                                 // Só executa 1x para cada periodo
                                 if (!lista2500BaseCalculo.Contains(sIDchave7))
                                 {
                                    lista2500BaseCalculo.Add(sIDchave7);

                                    //eCalculo 1
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCpMensal = gcl.getVal("vrBcCpMensal").Replace(",", ".");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCp13 = gcl.getVal("vrBcCp13").Replace(",", ".");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts = gcl.getVal("vrBcFgts").Replace(",", ".");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts13 = gcl.getVal("vrBcFgts13").Replace(",", ".");

                                    // infoAgNocivo 0 - 1
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.infoAgNocivo.grauExp = gcl.getVal("grauExp_infoAgNocivo");

                                    s2500XML.add_baseCalculo();
                                 }
                              }

                              gcl.setLevel("infoFGTS", clear: true);

                              var tbPeriodo3 = from DataRow r in tbEventos.Rows
                                               where //!string.IsNullOrEmpty(r[$"perRef{gcl.getLevel}"].ToString()) &&
                                               r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                               r["id_infoContr"].ToString().Equals(sIDchave) &&
                                               r["id_idePeriodo"].ToString().Equals(sIDchave6)
                                               select r;

                              foreach (var idePeriodo3 in tbPeriodo3)
                              {
                                 gcl.setLevel(row: idePeriodo3);

                                 sIDchave8 = gcl.getVal("id").ToString();

                                 // Só executa 1x para cada periodo
                                 if (!lista2500InfoFGTS.Contains(sIDchave8))
                                 {
                                    lista2500InfoFGTS.Add(sIDchave8);

                                    // infoFGTS 0 - 1
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.temInfoFGTS = gcl.getVal("temInfoFGTS");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSProcTrab = gcl.getVal("vrBcFGTSProcTrab").Replace(",", ".");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSSefip = gcl.getVal("vrBcFGTSSefip").Replace(",", ".");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSDecAnt = gcl.getVal("vrBcFGTSDecAnt").Replace(",", ".");

                                    s2500XML.add_infoFGTS();
                                 }
                              }

                              gcl.setLevel("baseMudCateg", clear: true);

                              var tbPeriodo4 = from DataRow r in tbEventos.Rows
                                               where //!string.IsNullOrEmpty(r[$"perRef{gcl.getLevel}"].ToString()) &&
                                               r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                               r["id_infoContr"].ToString().Equals(sIDchave) &&
                                               r["id_idePeriodo"].ToString().Equals(sIDchave6)
                                               select r;

                              foreach (var idePeriodo4 in tbPeriodo4)
                              {
                                 gcl.setLevel(row: idePeriodo4);

                                 sIDchave9 = gcl.getVal("id").ToString();

                                 // Só executa 1x para cada periodo
                                 if (!lista2500BaseMudaCateg.Contains(sIDchave9))
                                 {
                                    lista2500BaseMudaCateg.Add(sIDchave9);

                                    // baseMudCateg 0 - 1
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.codCateg = gcl.getVal("codCateg");
                                    s2500XML.ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.vrBcCPrev = gcl.getVal("vrBcCPrev").Replace(",", ".");

                                    s2500XML.add_baseMudCateg();
                                 }
                              }

                              s2500XML.add_periodo();
                              gcl.setLevel("idePeriodo", clear: true);
                           }
                        }

                        s2500XML.add_infoContr();
                        gcl.setLevel("infoContr", clear: true);
                     }

                  }

                  evento.eventoAssinadoXML = s2500XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2500", e.Message); }
         return lEventos;
      }
   }
}
