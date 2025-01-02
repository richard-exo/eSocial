using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD {
   public class s1200 : bEvento_BD {

      XML.s1200 s1200XML;

      public s1200() : base("1200", "Rem. de trabalhador vinc.", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {
            List<string> lista1200 = new List<string>();
            List<string> lista1200DmDev = new List<string>();
            List<string> lista1200infoPerApur = new List<string>();
            List<string> lista1200infoComplCont = new List<string>();
            List<string> lista1200remunOutrEmpr = new List<string>();                       

            foreach (DataRow row in tbEventos.Rows) {
               // Só executa 1x para cada funcionário
               if (!lista1200.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista1200.Add(row["id_funcionario"].ToString());
                  
                  string sMesano = row["mesano"].ToString();

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s1200XML = new XML.s1200(evento.id);

                  // ### Evento

                  // ideEvento
                  s1200XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s1200XML.ideEvento.nrRecibo = row["nrRecibo"].ToString();
                  s1200XML.ideEvento.indApuracao = row["indApuracao"].ToString();
                  s1200XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString());
                  s1200XML.ideEvento.tpAmb = evento.tpAmb;
                  s1200XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s1200XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s1200XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s1200XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideTrabalhador
                  s1200XML.ideTrabalhador.cpfTrab = row["cpfTrab"].ToString();
                  s1200XML.ideTrabalhador.nisTrab = row["nisTrab"].ToString();

                  // infoMV 0.1
                  s1200XML.ideTrabalhador.infoMV.indMV = row["indMV"].ToString();

                  // remunOutrEmpr 1.10
                  foreach (var remunOutrEmpr in from DataRow r in tbEventos.Rows
                                                where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                !string.IsNullOrEmpty(r["tpInsc"].ToString())
                                                select r)
                  {
                     if (!lista1200remunOutrEmpr.Contains(remunOutrEmpr["id_remunOutras"].ToString()))
                     {
                        lista1200remunOutrEmpr.Add(remunOutrEmpr["id_remunOutras"].ToString());

                        s1200XML.ideTrabalhador.infoMV.remunOutrEmpr.tpInsc = remunOutrEmpr["tpInsc"].ToString();
                        s1200XML.ideTrabalhador.infoMV.remunOutrEmpr.nrInsc = remunOutrEmpr["nrInsc"].ToString();
                        s1200XML.ideTrabalhador.infoMV.remunOutrEmpr.codCateg = remunOutrEmpr["codCateg"].ToString();
                        s1200XML.ideTrabalhador.infoMV.remunOutrEmpr.vlrRemunOE = remunOutrEmpr["vlrRemunOE"].ToString().Replace(",", ".");
                        s1200XML.add_remunOutrEmpr();
                     }
                  }

                  // infoComplem 0.1
                  s1200XML.ideTrabalhador.infoComplem.nmTrab = row["nmTrab"].ToString();
                  s1200XML.ideTrabalhador.infoComplem.dtNascto = validadores.aaaa_mm_dd(row["dtNascto"].ToString());

                  // sucessaoVinc 0.1
                  s1200XML.ideTrabalhador.infoComplem.sucessaoVinc.tpInsc = row["tpInscAnt"].ToString();
                  s1200XML.ideTrabalhador.infoComplem.sucessaoVinc.nrInsc = row["cnpjEmpregAnt"].ToString();
                  s1200XML.ideTrabalhador.infoComplem.sucessaoVinc.matricAnt = row["matricAnt"].ToString();         // 0.1
                  s1200XML.ideTrabalhador.infoComplem.sucessaoVinc.dtAdm = validadores.aaaa_mm_dd(row["dtAdm"].ToString());
                  s1200XML.ideTrabalhador.infoComplem.sucessaoVinc.observacao = row["observacao"].ToString();       // 0.1

                  // dmDev 1.99
                  gcl.setLevel("dmDev");

                  string sIdDmDev = "";
                  var tbDmDev = from DataRow r in tbEventos.Rows
                                where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                !string.IsNullOrEmpty(r["id_dmDev"].ToString())
                                select r;

                  foreach (var dmDev in tbDmDev)
                  {
                     //gcl.setLevel("dmDev", dmDev);
                     gcl.setLevel(row: dmDev);

                     sIdDmDev = gcl.getVal("id");
                     s1200XML.dmDev.ideDmDev = gcl.getVal("ideDmDev");
                     s1200XML.dmDev.codCateg = gcl.getVal("codCateg");

                     // Só executa 1x para cada dmDev
                     if (!lista1200DmDev.Contains(sIdDmDev))
                     {
                        // Registra o dmDev
                        lista1200DmDev.Add(sIdDmDev);

                        // infoPerApur 0.1
                        gcl.setLevel("infoPerApur", clear: true);

                        // ideEstabLot 1.500
                        var tbIdeEstabLot_infoPerApur = from DataRow r in tbDmDev
                                                        where !string.IsNullOrEmpty(r["tpinsc_infoPerApur"].ToString()) &&
                                                        r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                        select r;

                        foreach (var ideEstabLot in tbIdeEstabLot_infoPerApur)
                        {
                           gcl.setLevel(row: ideEstabLot);

                           s1200XML.dmDev.infoPerApur.ideEstabLot.tpInsc = gcl.getVal("tpInsc");
                           s1200XML.dmDev.infoPerApur.ideEstabLot.nrInsc = gcl.getVal("nrInsc");
                           s1200XML.dmDev.infoPerApur.ideEstabLot.codLotacao = gcl.getVal("codLotacao");

                           if (gcl.getVal("qtdDiasAv").ToString() != "0")
                              s1200XML.dmDev.infoPerApur.ideEstabLot.qtdDiasAv = gcl.getVal("qtdDiasAv");   // 0.1

                           // remunPerApur 1.8
                           var tbRemunPerApur = from DataRow r in tbIdeEstabLot_infoPerApur
                                                where  r[$"id_ideEstabLot_infoPerApur"].ToString().Equals(gcl.getVal("id_ideEstabLot")) &&
                                                       r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                select r;

                           foreach (var remunPerApur in tbRemunPerApur)
                           {

                              gcl.setLevel(row: remunPerApur);

                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.matricula = gcl.getVal("matricula");   // 0.1
                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.indSimples = gcl.getVal("indSimples"); // 0.1

                              // descFolha 0.1
                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.descFolha.tpDesc = gcl.getVal("eConsignado");
                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.descFolha.instFinanc = gcl.getVal("instFinanc");
                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.descFolha.nrContrato = gcl.getVal("nrContrato");

                              // infoAgNocivo 0.1
                              s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.infoAgNocivo.grauExp = gcl.getVal("grauExp");

                              // itensRemun 1.200
                              var tbItensRemun = from DataRow r in tbIdeEstabLot_infoPerApur
                                                 where !string.IsNullOrEmpty(r[$"codRubr_infoPerApur"].ToString()) &&
                                                      r[$"id_remunPerApur_infoPerApur"].ToString().Equals(gcl.getVal("id_remunPerApur")) &&
                                                      r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                 select r;

                              foreach (var itensRemun in tbItensRemun)
                              {

                                 gcl.setLevel(row: itensRemun);

                                 string sChave = gcl.getVal("chave");

                                 // Só executa 1x para evento+tipo
                                 if (!lista1200infoPerApur.Contains(sChave))
                                 {
                                    // Registra o infoPerApur
                                    lista1200infoPerApur.Add(sChave);

                                    if (gcl.getVal("vrRubr").ToString() != "0,00")
                                    {
                                       s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.codRubr = gcl.getVal("codRubr");
                                       s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                                       if (gcl.getVal("qtdRubr") != null && gcl.getVal("qtdRubr").ToString() != "000.00")
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.qtdRubr = gcl.getVal("qtdRubr");     // 0.1
                                       s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.fatorRubr = gcl.getVal("fatorRubr"); // 0.1
                                                                                                                                           //s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");       // 0.1
                                       s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                                       if (int.Parse(sMesano) >= 202107)
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.itensRemun.indApurIR = gcl.getVal("indApurIR"); ; // 0=normal 1=especial

                                       s1200XML.add_itensRemun_infoPerApur();
                                    }
                                 }
                              }

                              // Implantado em 23/03/20 - Testar assim que tiver movimento
                              //// infoSaudeColet 0.1
                              //var tbinfoSaudeColet = from DataRow r in tbIdeEstabLot_infoPerApur
                              //                   where !string.IsNullOrEmpty(r[$"cnpjOper_infoPerApur"].ToString()) &&
                              //                        r[$"id_remunPerApur_infoPerApur"].ToString().Equals(gcl.getVal("id_remunPerApur")) &&
                              //                        r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                              //                   select r;

                              //foreach (var infoSaudeColet in tbinfoSaudeColet)
                              //{

                              //   gcl.setLevel(row: infoSaudeColet);

                              //   s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.cnpjOper = gcl.getVal("cnpjOper");
                              //   s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.regANS = validadores.ideTabRubr(gcl.getVal("regANS"), evento.nomeEmpresa, evento.nrInsc);
                              //   s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerApur.infoSaudeColet.detOper.vrPgTit = gcl.getVal("vrPgTit").Replace(",", ".");       // 0.1
                                 
                              //   s1200XML.add_detOper_infoPerApur();
                              //}

                              s1200XML.add_remunPerApur();
                              break;
                           }

                           s1200XML.add_ideEstabLot_infoPerApur();

                           break;
                        }

                        // infoPerAnt 0.1
                        gcl.setLevel("infoPerAnt", clear: true);

                        // ideADC 1.8
                        var tbIdeADC = from DataRow r in tbDmDev
                                       where !string.IsNullOrEmpty(r[$"tpAcConv_infoPerAnt"].ToString()) &&
                                                        r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                       select r;

                        foreach (var ideADC in tbIdeADC)
                        {

                           gcl.setLevel(row: ideADC);

                           s1200XML.dmDev.infoPerAnt.ideADC.dtAcConv = gcl.getVal("dtAcConv");     // 0.1
                           s1200XML.dmDev.infoPerAnt.ideADC.tpAcConv = gcl.getVal("tpAcConv");
                           s1200XML.dmDev.infoPerAnt.ideADC.compAcConv = gcl.getVal("compAcConv"); // 0.1
                           s1200XML.dmDev.infoPerAnt.ideADC.dtEfAcConv = gcl.getVal("dtEfAcConv"); // 0.1
                           s1200XML.dmDev.infoPerAnt.ideADC.dsc = gcl.getVal("dsc");
                           s1200XML.dmDev.infoPerAnt.ideADC.remunSuc = gcl.getVal("remunSuc");

                           // idePeriodo 1.180
                           var tbIdePeriodo = from DataRow r in tbIdeADC
                                              where !string.IsNullOrEmpty(r[$"perRef_infoPerAnt"].ToString()) &&
                                                       r[$"id_ideADC_infoPerAnt"].ToString().Equals(gcl.getVal("id_ideADC")) &&
                                                       r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                              select r;

                           foreach (var idePeriodo in tbIdePeriodo)
                           {

                              gcl.setLevel(row: idePeriodo);

                              s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.perRef = gcl.getVal("perRef");

                              // ideEstabLot 1.500
                              var tbIdeEstabLot_infoPerAnt = from DataRow r in tbIdeADC
                                                             where !string.IsNullOrEmpty(r[$"tpInsc_infoPerAnt"].ToString()) &&
                                                             r[$"id_idePeriodo_infoPerAnt"].ToString().Equals(gcl.getVal("id_idePeriodo")) &&
                                                             r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                             select r;

                              foreach (var ideEstabLot in tbIdeEstabLot_infoPerAnt)
                              {

                                 gcl.setLevel(row: ideEstabLot);

                                 s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.tpInsc = gcl.getVal("tpInsc");
                                 s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.nrInsc = gcl.getVal("nrInsc");
                                 s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.codLotacao = gcl.getVal("codLotacao");

                                 // remunPerAnt 1.8
                                 var tbRemunPerAnt = from DataRow r in tbIdeEstabLot_infoPerAnt
                                                     where (
                                                     !string.IsNullOrEmpty(r[$"matricula_infoPerAnt"].ToString()) ||
                                                     !string.IsNullOrEmpty(r[$"indSimples_infoPerAnt"].ToString())) &&
                                                     r[$"id_ideEstabLot_infoPerAnt"].ToString().Equals(gcl.getVal("id_ideEstabLot")) &&
                                                     r["id_dmDev"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                     select r;

                                 foreach (var remunPerAnt in tbRemunPerAnt)
                                 {

                                    gcl.setLevel(row: remunPerAnt);

                                    s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.matricula = gcl.getVal("matricula");   // 0.1
                                    s1200XML.dmDev.infoPerAnt.ideADC.idePeriodo.ideEstabLot.remunPerAnt.indSimples = gcl.getVal("indSimples"); // 0.1

                                    // itensRemun 1.200
                                    var tbItensRemun = from DataRow r in tbRemunPerAnt
                                                       where !string.IsNullOrEmpty(r[$"codRubr_infoPerAnt"].ToString()) &&
                                                       r[$"id_remunPerAnt_infoPerAnt"].ToString().Equals(gcl.getVal("id_remunPerAnt"))
                                                       select r;

                                    foreach (var itensRemun in tbItensRemun)
                                    {

                                       gcl.setLevel(row: itensRemun);

                                       if (gcl.getVal("vrRubr").ToString() != "0,00")
                                       {
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.codRubr = gcl.getVal("codRubr");
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                                          if (gcl.getVal("qtdRubr") != null && gcl.getVal("qtdRubr").ToString() != "000.00")
                                             s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.qtdRubr = gcl.getVal("qtdRubr");     // 0.1
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.fatorRubr = gcl.getVal("fatorRubr"); // 0.1
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");       // 0.1
                                          s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.itensRemun.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");       // 0.1

                                          s1200XML.add_itensRemun_infoPerAnt();
                                       }
                                    }

                                    s1200XML.dmDev.infoPerApur.ideEstabLot.remunPerAnt.infoAgNocivo.grauExp = gcl.getVal("grauExp");

                                    s1200XML.add_remunPerAnt_infoPerAnt();
                                    break;
                                 }

                                 s1200XML.add_ideEstabLot_infoPerAnt();
                                 break;
                              }

                              s1200XML.add_idePeriodo();
                              break;
                           }

                           s1200XML.add_ideAdc();
                           break;
                        }

                        // infoComplCont 0.1
                        gcl.setLevel("infoComplCont", clear: true);

                        var tbIdeDmDev_infoComplCont = from DataRow r in tbDmDev
                                                       where r["id_infoComplCont"].ToString().Equals(dmDev["id_dmDev"].ToString())
                                                       select r;

                        foreach (var ideComplCont in tbIdeDmDev_infoComplCont)
                        {
                           gcl.setLevel(row: ideComplCont);

                           // Só executa 1x para cada idDmDev
                           if (!lista1200infoComplCont.Contains(sIdDmDev))
                           {
                              // Registra o infoComplCont
                              lista1200infoComplCont.Add(sIdDmDev);
                              {
                                 s1200XML.dmDev.infoComplCont.codCBO = gcl.getVal("codCBO");
                                 s1200XML.dmDev.infoComplCont.natAtividade = gcl.getVal("natAtividade");
                                 s1200XML.dmDev.infoComplCont.qtdDiasTrab = gcl.getVal("qtdDiasTrab");
                              }
                           }
                        }

                        s1200XML.add_dmDev();
                        gcl.setLevel("dmDev", clear: true);
                        //break;
                     }
                  }

                  evento.eventoAssinadoXML = s1200XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }

         catch (Exception e) {
            addError("model.eventos.BD.s1200", e.Message);
         }

         return lEventos;
      }
   }
}
