using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1299 : bEvento_BD {

      XML.s1299 s1299XML;

      public s1299() : base("1299", "Fechamento dos eventos per.", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1299XML = new XML.s1299(evento.id);

               // ### Evento

               // ideEvento
               s1299XML.ideEvento.indApuracao = row["indApuracao"].ToString();
               s1299XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString());
               s1299XML.ideEvento.tpAmb = evento.tpAmb;
               s1299XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1299XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1299XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1299XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideRespInf
               s1299XML.ideRespInf.nmResp = row["nmResp"].ToString();
               s1299XML.ideRespInf.cpfResp = row["cpfResp"].ToString();
               s1299XML.ideRespInf.telefone = row["telefone"].ToString();
               s1299XML.ideRespInf.email = row["email"].ToString();       // 0.1

               // infoFech
               s1299XML.infoFech.evtRemun = row["evtRemun"].ToString();
               s1299XML.infoFech.evtPgtos = row["evtPgtos"].ToString();
               s1299XML.infoFech.evtAqProd = row["evtAqProd"].ToString();
               s1299XML.infoFech.evtComProd = row["evtComProd"].ToString();
               s1299XML.infoFech.evtContratAvNP = row["evtContratAvNP"].ToString();
               s1299XML.infoFech.evtInfoComplPer = row["evtInfoComplPer"].ToString();
               //s1299XML.infoFech.compSemMovto = row["compSemMovto"].ToString();       
               s1299XML.infoFech.compSemMovto = validadores.aaaa_mm(row["compSemMovto"].ToString()); // 0.1

               evento.eventoAssinadoXML = s1299XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1299", e.Message); }
         return lEventos;
      }
   }
}
