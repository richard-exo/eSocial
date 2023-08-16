using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso.sDadosCompl;
using static eSocial.Model.Eventos.XML.s2500.sInfoProcesso;
using static eSocial.Model.Eventos.XML.s2501;

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
                        s2501XML.ideTrab.calcTrib.vrRendIRRF = gcl.getVal("vrRendIRRF").Replace(",", ".");
                        s2501XML.ideTrab.calcTrib.vrRendIRRF13 = gcl.getVal("vrRendIRRF13").Replace(",", ".");

                        // infoCRContrib 0 - 99
                       // gcl.setLevel("infoCRContrib");
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
                              s2501XML.ideTrab.calcTrib.infoCRContrib.vrCR = gcl.getVal("vrCR");    
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
                        s2501XML.ideTrab.infoCRIRRF.vrCR = gcl.getVal("vrCR");
                        s2501XML.add_infoCRIRRF();
                     }
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
