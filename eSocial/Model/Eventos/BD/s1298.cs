using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1298 : bEvento_BD {

      XML.s1298 s1298XML;

      public s1298() : base("1298", "Reabertura dos eventos per.", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1298XML = new XML.s1298(evento.id);

               // ### Evento

               // ideEvento
               s1298XML.ideEvento.indApuracao = row["indApuracao"].ToString();
               s1298XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString());
               s1298XML.ideEvento.tpAmb = evento.tpAmb;
               s1298XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1298XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1298XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1298XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               evento.eventoAssinadoXML = s1298XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1298", e.Message); }
         return lEventos;
      }
   }
}
