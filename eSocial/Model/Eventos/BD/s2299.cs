using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD{
   class s2299 : bEvento_BD {

      XML.s2299 s2299XML;

      public s2299() : base("2299", "Desligamento", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try
         {
            List<string> lista2299 = new List<string>();
            List<string> lista2299info = new List<string>();
            string sIDchave;

            foreach (DataRow row in tbEventos.Rows)
            {
                // Só executa 1x para cada funcionário
                if (!lista2299.Contains(row["id_funcionario"].ToString()))
                {
                    // Registra o funcionário
                    lista2299.Add(row["id_funcionario"].ToString());

                    sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                    s2299XML = new XML.s2299(evento.id);

                    // ### Evento

                    // ideEvento
                    s2299XML.ideEvento.indRetif = row["indRetif"].ToString();
                    s2299XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                    s2299XML.ideEvento.tpAmb = evento.tpAmb;
                    s2299XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                    s2299XML.ideEvento.verProc = versao;

                    // ideEmpregador
                    s2299XML.ideEmpregador.tpInsc = evento.tpInsc;
                    s2299XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                    // ideVinculo
                    s2299XML.ideVinculo.cpfTrab = row["cpfTrab"].ToString();
                    s2299XML.ideVinculo.nisTrab = row["nisTrab"].ToString();
                    s2299XML.ideVinculo.matricula = row["matricula"].ToString();

                    // infoDeslig
                    s2299XML.infoDeslig.mtvDeslig = row["mtvDeslig"].ToString();
                    s2299XML.infoDeslig.dtDeslig = validadores.aaaa_mm_dd(row["dtDeslig"].ToString());
                    s2299XML.infoDeslig.dtAvPrv = validadores.aaaa_mm_dd(row["dtAvPrv"].ToString());
                    s2299XML.infoDeslig.indPagtoAPI = row["indPagtoAPI"].ToString();
                    s2299XML.infoDeslig.dtProjFimAPI = validadores.aaaa_mm_dd(row["dtProjFimAPI"].ToString());
                    s2299XML.infoDeslig.pensAlim = row["pensAlim"].ToString();

                    if (row["pensAlim"].ToString() == "1")
                        s2299XML.infoDeslig.percAliment = row["percAliment"].ToString().Replace(",", ".");
                    else if (row["pensAlim"].ToString() == "2")
                        s2299XML.infoDeslig.vrAlim = row["vrAlim"].ToString().Replace(",", ".");
                    else if (row["pensAlim"].ToString() == "3")
                    {
                        s2299XML.infoDeslig.percAliment = row["percAliment"].ToString().Replace(",", ".");
                        s2299XML.infoDeslig.vrAlim = row["vrAlim"].ToString().Replace(",", ".");
                    }

                    s2299XML.infoDeslig.nrCertObito = row["nrCertObito"].ToString();
                    s2299XML.infoDeslig.nrProcTrab = row["nrProcTrab"].ToString();
                    s2299XML.infoDeslig.indCumprParc = row["indCumprParc"].ToString();
                    s2299XML.infoDeslig.qtdDiasInterm = row["qtdDiasInterm"].ToString();

                    // infoDeslig
                    // > observacoes
                    s2299XML.infoDeslig.observacoes.observacao = row["observacao"].ToString();

                   // infoDeslig
                   // > sucessaoVinc
                   s2299XML.infoDeslig.sucessaoVinc.tpInscSuc = row["tpInscSuc"].ToString();
                   s2299XML.infoDeslig.sucessaoVinc.cnpjSucessora = row["cnpjSucessora"].ToString();

                    // infoDeslig
                    // > transfTit
                    s2299XML.infoDeslig.transfTit.cpfSubstituto = row["cpfSubstituto"].ToString();
                    s2299XML.infoDeslig.transfTit.dtNascto = validadores.aaaa_mm_dd(row["dtNascto"].ToString());

                  // infoDeslig
                  // > mudancaCPF
                  s2299XML.infoDeslig.mudancaCPF.novoCPF = row["novoCPF"].ToString();


                  // infoPgto 1.60
                  gcl.setLevel("infoPgto");

                  var tbInfoPgto = from DataRow r in tbEventos.Rows
                                   where r["id_funcionario"].ToString().Equals(evento.id_funcionario) 
                                   select r;

                  foreach (var infoPgto in tbInfoPgto)
                  {
                     gcl.setLevel(row: infoPgto);
                     sIDchave = gcl.getVal("id").ToString();

                     // Só executa 1x para cada funcionario
                     if (!lista2299info.Contains(sIDchave))
                     {
                        // Registra o DEM1, DEM5..
                        lista2299info.Add(sIDchave);
                        Boolean bTemVerbas = false;

                        // >>>>> detVerbas
                        gcl.setLevel("detVerbas", clear: true);

                        var tpVerbas = from DataRow r in tbEventos.Rows
                                       where !string.IsNullOrEmpty(r[$"codRubr{gcl.getLevel}"].ToString()) &&
                                       r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                       r["id_infoPgto"].ToString().Equals(sIDchave)
                                       select r;

                        foreach (var detVerbas in tpVerbas)
                        {
                           gcl.setLevel(row: detVerbas);


                           // infoDeslig
                           // > verbasResc
                           // >> dmDev
                           s2299XML.infoDeslig.verbasResc.dmDev.ideDmDev = gcl.getVal("ideDmDev");

                           // >>> infoPerApur
                           // >>>> ideEstabLot
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.tpInsc = row["tpInsc"].ToString();
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.nrInsc = row["nrInsc"].ToString();
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.codLotacao = row["codLotacao"].ToString();

                           if (gcl.getVal("codRubr").ToString() != "")
                           {
                              bTemVerbas = true;
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.codRubr = gcl.getVal("codRubr");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.ideTabRubr = gcl.getVal("ideTabRubr");
                              if (gcl.getVal("qtdRubr")!=null && gcl.getVal("qtdRubr").ToString()!="000.00")
                                 s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.qtdRubr = gcl.getVal("qtdRubr").Replace(":", "");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.fatorRubr = gcl.getVal("fatorRubr");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.vrUnit = gcl.getVal("vrUnit");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.detVerbas.indApurIR = gcl.getVal("indApurIR");

                              s2299XML.add_detVerbas_infoPerApur();
                           }
                        }

                        // >>>>> infoSaudeColet
                        // > detOper
                        gcl.setLevel("detOper", clear: true);
                        var tpOper = from DataRow r in tbEventos.Rows
                                     where !string.IsNullOrEmpty(r[$"cnpjOper{gcl.getLevel}"].ToString()) &&
                                     r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                     r["id_infoPgto"].ToString().Equals(sIDchave)
                                     select r;

                        foreach (var detOper in tpOper)
                        {
                           gcl.setLevel(row: detOper);

                           if (gcl.getVal("cnpjOper").ToString() != "" && gcl.getVal("vrPgTit").ToString() != "0,00")
                           {
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.cnpjOper = gcl.getVal("cnpjOper");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.regANS = gcl.getVal("regANS");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.vrPgTit = gcl.getVal("vrPgTit").Replace(",", ".");

                              s2299XML.add_detOper();
                           }
                        }

                        // > detPlano
                        gcl.setLevel("detPlano", clear: true);
                        var tpPlano = from DataRow r in tbEventos.Rows
                                      where !string.IsNullOrEmpty(r[$"nmDep{gcl.getLevel}"].ToString()) &&
                                      r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                      r["id_infoPgto"].ToString().Equals(sIDchave)
                                      select r;

                        foreach (var detPlano in tpPlano)
                        {
                           gcl.setLevel(row: detPlano);

                           if (gcl.getVal("nmDep").ToString() != "")
                           {  // detPlano
                              bTemVerbas = true;
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.tpDep = gcl.getVal("tpDep");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.cpfDep = gcl.getVal("cpfDep");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.nmDep = gcl.getVal("nmDep");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSaudeColet.detOper.detPlano.vlrPgDep = gcl.getVal("vlrPgDep").Replace(",", ".");

                              s2299XML.add_detPlano();
                           }
                        }

                        // >>>> ideEstabLot
                        // >>>>> infoAgNocivo
                        s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoAgNocivo.grauExp = row["grauExp"].ToString();

                        // >>>>> infoSimples
                        s2299XML.infoDeslig.verbasResc.dmDev.infoPerApur.ideEstabLot.infoSimples.indSimples = row["indSimples"].ToString();

                        if (bTemVerbas)
                        {
                           s2299XML.add_ideEstabLot_infoPerApur();
                           s2299XML.add_dmDev();
                        }

                        // >>>> ideEstabLot
                        // >>>>> infoPerAnt
                        // >>>>>> ideADC
                        gcl.setLevel("ideADC", clear: true);

                        var tpADC = from DataRow r in tbEventos.Rows
                                    where !string.IsNullOrEmpty(r[$"dtAcConv{gcl.getLevel}"].ToString()) &&
                                    r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                    r["id_infoPgto"].ToString().Equals(sIDchave)
                                    select r;

                        foreach (var ideADC in tpADC)
                        {
                           gcl.setLevel(row: ideADC);

                           if (gcl.getVal("dtAcConv").ToString() != "")
                           {
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dtAcConv = validadores.aaaa_mm_dd(gcl.getVal("dtAcConv"));
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.tpAcConv = gcl.getVal("tpAcConv");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.compAcConv = gcl.getVal("compAcConv");
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dtEfAcConv = validadores.aaaa_mm_dd(gcl.getVal("dtEfAcConv"));
                              s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.dsc = gcl.getVal("dsc");

                              s2299XML.add_ideADC();
                           }
                        }

                        // idePeriodo
                        gcl.setLevel("idePeriodo", clear: true);

                        var tpPeriodo = from DataRow r in tbEventos.Rows
                                        where !string.IsNullOrEmpty(r[$"perRef{gcl.getLevel}"].ToString()) &&
                                        r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                        r["id_infoPgto"].ToString().Equals(sIDchave)
                                        select r;

                        foreach (var idePeriodo in tpPeriodo)
                        {
                           gcl.setLevel(row: idePeriodo);


                           // > idePeriodo
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.perRef = gcl.getVal("perRef");

                           s2299XML.add_idePeriodo();
                        }

                        // ideEstabLot
                        gcl.setLevel("ideEstabLot", clear: true);

                        var tpEstabLot = from DataRow r in tbEventos.Rows
                                         where !string.IsNullOrEmpty(r[$"nrInsc{gcl.getLevel}"].ToString()) &&
                                         r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                         r["id_infoPgto"].ToString().Equals(sIDchave)
                                         select r;

                        foreach (var ideEstabLot in tpEstabLot)
                        {
                           gcl.setLevel(row: ideEstabLot);

                           // >> ideEstabLot
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.tpInsc = gcl.getVal("tpInsc");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.nrInsc = gcl.getVal("nrInsc");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.codLotacao = gcl.getVal("codLotacao");

                           s2299XML.add_ideEstabLot_infoPerAnt();
                        }

                        // detVerbas
                        gcl.setLevel("detVerbas2", clear: true);

                        var tpVerbas2 = from DataRow r in tbEventos.Rows
                                        where !string.IsNullOrEmpty(r[$"codRubr{gcl.getLevel}"].ToString()) &&
                                        r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                        r["id_infoPgto"].ToString().Equals(sIDchave)
                                        select r;

                        foreach (var detVerbas2 in tpVerbas2)
                        {
                           gcl.setLevel(row: detVerbas2);

                           // >>> detVerbas
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.codRubr = gcl.getVal("codRubr");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.ideTabRubr = gcl.getVal("ideTabRubr");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.qtdRubr = gcl.getVal("qtdRubr").Replace(":", "");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.fatorRubr = gcl.getVal("fatorRubr");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.vrUnit = gcl.getVal("vrUnit");
                           s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.detVerbas.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                           s2299XML.add_detVerbas_infoPerAnt();
                        }

                        // infoAgNocivo
                        // >>>> ideEstabLot
                        s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoAgNocivo.grauExp = row["infoAgNocivo_grauExp"].ToString();

                        // > infoSimples
                        s2299XML.infoDeslig.verbasResc.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.infoSimples.indSimples = row["infoSimples_indSimples"].ToString();

                        // infoTrabInterm
                        gcl.setLevel("infoTrabInterm", clear: true);

                        var tpinfoTrabInterm = from DataRow r in tbEventos.Rows
                                               where !string.IsNullOrEmpty(r[$"codConv{gcl.getLevel}"].ToString()) &&
                                               r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                               r["id_infoPgto"].ToString().Equals(sIDchave)
                                               select r;

                        foreach (var infoTrabInterm in tpinfoTrabInterm)
                        {
                           gcl.setLevel(row: infoTrabInterm);
                           s2299XML.infoDeslig.verbasResc.dmDev.infoTrabInterm.codConv = gcl.getVal("codConv");
                           s2299XML.add_infoTrabInterm();
                        }

                        // procJurTrab
                        gcl.setLevel("procJurTrab", clear: true);

                        var tpprocJurTrab = from DataRow r in tbEventos.Rows
                                            where !string.IsNullOrEmpty(r[$"nrProcJud{gcl.getLevel}"].ToString()) &&
                                            r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                            r["id_infoPgto"].ToString().Equals(sIDchave)
                                            select r;

                        foreach (var procJurTrab in tpprocJurTrab)
                        {
                           gcl.setLevel(row: procJurTrab);
                           s2299XML.infoDeslig.verbasResc.procJudTrab.tpTrib = gcl.getVal("tpTrib");
                           s2299XML.infoDeslig.verbasResc.procJudTrab.nrProcJud = gcl.getVal("nrProcJud");
                           s2299XML.infoDeslig.verbasResc.procJudTrab.codSusp = gcl.getVal("codSusp");

                           s2299XML.add_procJudTrab();
                        }

                        // infoMV
                        s2299XML.infoDeslig.verbasResc.infoMV.indMV = row["indMV"].ToString();

                        // remunOutrEmpr
                        gcl.setLevel("remunOutrEmpr", clear: true);

                        var tpremunOutrEmpr = from DataRow r in tbEventos.Rows
                                              where !string.IsNullOrEmpty(r[$"nrInsc{gcl.getLevel}"].ToString()) &&
                                              r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                              r["id_infoPgto"].ToString().Equals(sIDchave)
                                              select r;

                        foreach (var remunOutrEmpr in tpremunOutrEmpr)
                        {
                           gcl.setLevel(row: remunOutrEmpr);
                           s2299XML.infoDeslig.verbasResc.infoMV.remunOutrEmpr.tpInsc = gcl.getVal("tpInsc");
                           s2299XML.infoDeslig.verbasResc.infoMV.remunOutrEmpr.nrInsc = gcl.getVal("nrInsc");
                           s2299XML.infoDeslig.verbasResc.infoMV.remunOutrEmpr.codCateg = gcl.getVal("codCateg");
                           s2299XML.infoDeslig.verbasResc.infoMV.remunOutrEmpr.vlrRemunOE = gcl.getVal("vlrRemunOE").Replace(",", ".");

                           s2299XML.add_remunOutrEmpr();
                        }

                        // procCS
                        s2299XML.infoDeslig.verbasResc.procCS.nrProcJud = row["nrProcJud"].ToString();


                        ///////////////////////////////////////
                        ////// encerrar verbasResc
                        ///

                        //s2299XML.add_detVerbas();
                        ///////////////////////////////////////
                     }

                     gcl.setLevel("infoPgto", clear: true);
                  }

                  // quarentena
                  s2299XML.infoDeslig.quarentena.dtFimQuar = validadores.aaaa_mm_dd(row["dtFimQuar"].ToString());

                  // consigFGTS
                  gcl.setLevel("consigFGTS", clear: true);
                  var tpconsigFGTS = from DataRow r in tbEventos.Rows
                                       where !string.IsNullOrEmpty(r[$"nrContr{gcl.getLevel}"].ToString()) &&
                                       r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                       select r;

                  foreach (var consigFGTS in tpconsigFGTS)
                  {
                     gcl.setLevel(row: consigFGTS);
                     s2299XML.infoDeslig.consigFGTS.insConsig = gcl.getVal("insConsig");
                     s2299XML.infoDeslig.consigFGTS.nrContr = gcl.getVal("nrContr");
                     s2299XML.add_remunOutrEmpr();
                  }              
                  
                  evento.eventoAssinadoXML = s2299XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2299", e.Message); }
         return lEventos;
      }
   }
}
