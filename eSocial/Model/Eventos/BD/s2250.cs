using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD
{
   class s2250 : bEvento_BD
   {
      XML.s2250 s2250XML;

      public s2250() : base("2250", "Aviso Prévio", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {
         base.getEventosPendentes();

         try
         {
            foreach (DataRow row in tbEventos.Rows)
            {
               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s2250XML = new XML.s2250(evento.id);

               // ### Evento
               // ideEvento
               s2250XML.ideEvento.indRetif = row["indRetif"].ToString();
               s2250XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
               s2250XML.ideEvento.tpAmb = evento.tpAmb;
               s2250XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s2250XML.ideEvento.verProc = versao;

               // ideEmpregador
               s2250XML.ideEmpregador.tpInsc = evento.tpInsc;
               s2250XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideVinculo
               s2250XML.ideVinculo.cpfTrab = row["cpfTrab"].ToString();
               s2250XML.ideVinculo.nisTrab = row["nisTrab"].ToString();
               s2250XML.ideVinculo.matricula = row["matricula"].ToString();

               // infoAvPrevio
               // > detAvPrevio
               s2250XML.infoAvPrevio.detAvPrevio.dtAvPrv = validadores.aaaa_mm_dd(row["dtAvPrv"].ToString());
               s2250XML.infoAvPrevio.detAvPrevio.dtPrevDeslig = validadores.aaaa_mm_dd(row["dtPrevDeslig"].ToString());
               s2250XML.infoAvPrevio.detAvPrevio.tpAvPrevio = row["tpAvPrevio"].ToString();
               s2250XML.infoAvPrevio.detAvPrevio.observacao = row["observacao"].ToString();

               // > cancAvPrevio
               s2250XML.infoAvPrevio.cancAvPrevio.dtCancAvPrv = validadores.aaaa_mm_dd(row["dtCancAvPrv"].ToString());
               s2250XML.infoAvPrevio.cancAvPrevio.observacao = row["observacaoCancAvPrv"].ToString();
               s2250XML.infoAvPrevio.cancAvPrevio.mtvCancAvPrevio = row["mtvCancAvPrevio"].ToString();

               evento.eventoAssinadoXML = s2250XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2250", e.Message); }
         return lEventos;
      }
   }
}
