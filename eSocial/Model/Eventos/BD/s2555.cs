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
   class s2555 : bEvento_BD
   {
      XML.s2555 s2555XML;

      public s2555() : base("2555", "Solicitação de Consolidação das Informações de Tributos Decorrentes de Processo Trabalhista", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {
            List<string> lista2555 = new List<string>();

            foreach (DataRow row in tbEventos.Rows)
            {
               // Só executa 1x para cada funcionário
               if (!lista2555.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista2555.Add(row["id_funcionario"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2555XML = new XML.s2555(evento.id);

                  // ### Evento

                  // ideEvento
                  //s2555XML.ideEvento.indRetif = row["indRetif"].ToString();
                  //s2555XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2555XML.ideEvento.tpAmb = evento.tpAmb;
                  s2555XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2555XML.ideEvento.verProc =  versao;

                  // ideEmpregador
                  s2555XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2555XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideProc 1
                  s2555XML.ideProc.nrProcTrab = row["nrProcTrab_ideProc_ideProc"].ToString();
                  s2555XML.ideProc.perApurPgto = validadores.aaaa_mm(row["perApurPgto_ideProc"].ToString());

                  evento.eventoAssinadoXML = s2555XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2555", e.Message); }
         return lEventos;
      }
   }
}
