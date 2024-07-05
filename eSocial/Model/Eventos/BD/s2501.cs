using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso.sDadosCompl;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso;
using static eSocial.Model.Eventos.XML.s2501;
using static eSocial.Model.Eventos.XML.s2240.sInfoExpRisco;

namespace eSocial.Model.Eventos.BD
{
   class s2501 : bEvento_BD
   {
      XML.s2501 s2501XML;

      public s2501() : base("2501", "Informações de contribuições do Processo Trabalhista", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {
            List<string> lista2501 = new List<string>();
            List<string> lista2501CalcTrib = new List<string>();
            List<string> lista2501CalcTribInfo = new List<string>();
            List<string> lista2501Info = new List<string>();
            List<string> lista2501Adv = new List<string>();            
            string sIDchave = "", sIDchave2="";

            foreach (DataRow row in tbEventos.Rows)
            {
               // Só executa 1x para cada funcionário
               if (!lista2501.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista2501.Add(row["id_funcionario"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2501XML = new XML.s2501(evento.id);

                  // ### Evento

                  // ideEvento
                  s2501XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2501XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2501XML.ideEvento.tpAmb = evento.tpAmb;
                  s2501XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2501XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2501XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2501XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideProc 1
                  s2501XML.ideProc.nrProcTrab = row["nrProcTrab_ideProc_ideProc"].ToString();
                  s2501XML.ideProc.perApurPgto = validadores.aaaa_mm(row["perApurPgto_ideProc"].ToString());
                  s2501XML.ideProc.obs = row["obs_ideProc"].ToString();

                  // ideTrab 1
                  s2501XML.ideTrab.cpfTrab = row["cpfTrab_ideTrab"].ToString() ;

                  // calcTrib 1 - 360
                  gcl.setLevel("calcTrib");

                  var tbCalcTrib = from DataRow r in tbEventos.Rows
                                   where !string.IsNullOrEmpty(r[$"id{gcl.getLevel}"].ToString()) && 
                                   r["id_funcionario"].ToString().Equals(evento.id_funcionario) 
                                   select r;

                  foreach (var calcTrib in tbCalcTrib)
                  {
                     gcl.setLevel(row: calcTrib);
                     sIDchave = gcl.getVal("id").ToString();

                     // Só executa 1x para cada funcionario
                     if (!lista2501CalcTrib.Contains(sIDchave))
                     {
                        lista2501CalcTrib.Add(sIDchave);

                        s2501XML.ideTrab.calcTrib.perRef = validadores.aaaa_mm(gcl.getVal("perRef"));
                        s2501XML.ideTrab.calcTrib.vrBcCpMensal = gcl.getVal("vrBcCpMensal").Replace(",", ".");
                        s2501XML.ideTrab.calcTrib.vrBcCp13 = gcl.getVal("vrBcCp13").Replace(",", ".");

                        // infoCRContrib 0 - 99
                        gcl.setLevel("infoCRContrib", clear: true);

                        var tbCalcTribInfo = from DataRow r in tbEventos.Rows
                                             where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                             r["id_calcTrib"].ToString().Equals(sIDchave)
                                             select r;

                        foreach (var calcTribInfo in tbCalcTribInfo)
                        {
                           gcl.setLevel(row: calcTribInfo);
                           sIDchave2 = gcl.getVal("id").ToString();

                           // Só executa 1x para cada funcionario
                           if (!lista2501CalcTribInfo.Contains(sIDchave2))
                           {
                              lista2501CalcTribInfo.Add(sIDchave2);

                              s2501XML.ideTrab.calcTrib.infoCRContrib.tpCR = gcl.getVal("tpCR");
                              s2501XML.ideTrab.calcTrib.infoCRContrib.vrCR = gcl.getVal("vrCR").Replace(",", ".");
                              s2501XML.add_calcTribInfo();
                           }
                        }
                        s2501XML.add_calcTrib();
                        gcl.setLevel("calcTrib", clear:true);
                     }     
                  }

                  // infoCRIRRF 0 - 99
                  gcl.setLevel("infoCRIRRF", clear: true);

                  var tbInfo = from DataRow r in tbEventos.Rows
                               where r["id_funcionario"].ToString().Equals(evento.id_funcionario)                               
                               select r;

                  foreach (var info in tbInfo)
                  {
                     gcl.setLevel(row: info);
                     sIDchave = gcl.getVal("id").ToString();

                     // Só executa 1x para cada funcionario
                     if (!lista2501Info.Contains(sIDchave))
                     {
                        lista2501Info.Add(sIDchave);

                        s2501XML.ideTrab.infoCRIRRF.tpCR = gcl.getVal("tpCR");
                        s2501XML.ideTrab.infoCRIRRF.vrCR = gcl.getVal("vrCR").Replace(",", ".");

                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrRendTrib = gcl.getVal("vrRendTrib").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrRendTrib13 = gcl.getVal("vrRendTrib13").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrRendMoleGrave = gcl.getVal("vrRendMoleGrave").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrRendIsen65 = gcl.getVal("vrRendIsen65").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrJurosMora = gcl.getVal("vrJurosMora").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrRendIsenNTrib = gcl.getVal("vrRendIsenNTrib").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.descIsenNTrib = gcl.getVal("descIsenNTrib");
                        s2501XML.ideTrab.infoCRIRRF.infoIR.vrPrevOficial = gcl.getVal("vrPrevOficial").Replace(",", ".");

                        s2501XML.ideTrab.infoCRIRRF.infoRRA.descRRA = gcl.getVal("descRRA");
                        s2501XML.ideTrab.infoCRIRRF.infoRRA.qtdMesesRRA = gcl.getVal("qtdMesesRRA");

                        s2501XML.ideTrab.infoCRIRRF.infoRRA.despProcJud.vlrDespCustas = gcl.getVal("vlrDespCustas").Replace(",", ".");
                        s2501XML.ideTrab.infoCRIRRF.infoRRA.despProcJud.vlrDespAdvogados = gcl.getVal("vlrDespAdvogados").Replace(",", ".");

                        // ideAdv 0.99
                        gcl.setLevel("ideAdv", clear: true);

                        var tbAdv = from DataRow r in tbEventos.Rows
                                             where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                             r["id_infocrirrf"].ToString().Equals(sIDchave)
                                             select r;

                        foreach (var adv in tbAdv)
                        {
                           gcl.setLevel(row: adv);

                           string sChaveAdv = gcl.getVal("id").ToString();
                           if (!lista2501Adv.Contains(sChaveAdv) && sChaveAdv!="")
                           {
                              lista2501Adv.Add(sChaveAdv);

                              s2501XML.ideTrab.infoCRIRRF.infoRRA.ideAdv.tpInsc = gcl.getVal("tpInsc");
                              s2501XML.ideTrab.infoCRIRRF.infoRRA.ideAdv.nrInsc = gcl.getVal("nrInsc");
                              s2501XML.ideTrab.infoCRIRRF.infoRRA.ideAdv.vlrAdv = gcl.getVal("vlrAdv").Replace(",", ".");

                              s2501XML.add_infoAdv();
                           }
                        }                        
                        s2501XML.add_infoCRIRRF();
                     }
                     gcl.setLevel("infoCRIRRF", clear: true);
                  }

                  evento.eventoAssinadoXML = s2501XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2501", e.Message); }
         return lEventos;
      }
   }
}
