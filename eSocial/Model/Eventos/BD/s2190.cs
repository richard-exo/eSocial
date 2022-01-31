using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s2190 : bEvento_BD {

      XML.s2190 s2190XML;

      public s2190() : base("2190", "Adm. preliminar", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s2190XML = new XML.s2190(evento.id);

               // ### Evento

               // ideEvento
               s2190XML.ideEvento.tpAmb = evento.tpAmb;
               s2190XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s2190XML.ideEvento.verProc = versao;

               // ideEmpregador
               s2190XML.ideEmpregador.tpInsc = evento.tpInsc;
               s2190XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // infoRegPrelim
               s2190XML.infoRegPrelim.cpfTrab = row["cpfTrab"].ToString();
               s2190XML.infoRegPrelim.dtNascto = validadores.aaaa_mm_dd(row["dtNascto"].ToString());
               s2190XML.infoRegPrelim.dtAdm = validadores.aaaa_mm_dd(row["dtAdm"].ToString());

               evento.eventoAssinadoXML = s2190XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2190", e.Message); }
         return lEventos;
      }
   }
}
