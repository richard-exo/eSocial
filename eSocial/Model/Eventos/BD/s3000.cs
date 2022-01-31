using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s3000 : bEvento_BD {

      XML.s3000 s3000XML;

      public s3000() : base("3000", "Exclusão eventos", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s3000XML = new XML.s3000(evento.id);

               // ### Evento

               // ideEvento
               s3000XML.ideEvento.tpAmb = evento.tpAmb;
               s3000XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s3000XML.ideEvento.verProc = versao;

               // ideEmpregador
               s3000XML.ideEmpregador.tpInsc = evento.tpInsc;
               s3000XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // infoExclusao
               s3000XML.infoExclusao.tpEvento = row["tpEvento"].ToString();
               s3000XML.infoExclusao.nrRecEvt = row["nrRecEvt"].ToString();

               // ideTrabalhador 0.1
               s3000XML.infoExclusao.ideTrabalhador.cpfTrab = row["cpfTrab"].ToString();
               s3000XML.infoExclusao.ideTrabalhador.nisTrab = row["nisTrab"].ToString(); // 0.1

               // ideFolhaPagto 0.1
               s3000XML.infoExclusao.ideFolhaPagto.indApuracao = row["indApuracao"].ToString();
               s3000XML.infoExclusao.ideFolhaPagto.perApur = validadores.aaaa_mm(row["perApur"].ToString());

               evento.eventoAssinadoXML = s3000XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s3000", e.Message); }
         return lEventos;
      }
   }
}
