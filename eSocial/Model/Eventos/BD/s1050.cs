using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace eSocial.Model.Eventos.BD {
   public class s1050 : bEvento_BD {

      XML.s1050 s1050XML;

      public s1050() : base("1050", "Tab. Horários / Turnos", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1050XML = new XML.s1050(evento.id);

               // ### Evento

               // ideEvento
               s1050XML.ideEvento.tpAmb = evento.tpAmb;
               s1050XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1050XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1050XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1050XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1050XML.infoHorContratual.exclusao.IdeHorContratual.codHorContrat = row["codHorContrat"].ToString();
                  s1050XML.infoHorContratual.exclusao.IdeHorContratual.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1050XML.infoHorContratual.exclusao.IdeHorContratual.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1050.sInfoHorContratual.sIncAlt incAlt = new XML.s1050.sInfoHorContratual.sIncAlt();

                  // IdeHorContratual
                  incAlt.ideHorContratual.codHorContrat = row["codHorContrat"].ToString();
                  incAlt.ideHorContratual.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideHorContratual.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosHorContratual
                  incAlt.dadosHorContratual.hrEntr = row["hrEntr"].ToString();
                  incAlt.dadosHorContratual.hrSaida = row["hrSaida"].ToString();
                  incAlt.dadosHorContratual.durJornada = row["durJornada"].ToString();
                  incAlt.dadosHorContratual.perHorFlexivel = row["perHorFlexivel"].ToString();

                  // horarioIntervalo 0.99
                  var tbInterv = from DataRow r in tbEventos.Rows
                                 where r["codHorContrat"].ToString().Equals(row["codHorContrat"].ToString()) && !r["durInterv"].ToString().Equals("0")
                                 select r;

                  foreach (var i in tbInterv) {

                     incAlt.dadosHorContratual.horarioIntervalo.tpInterv = i["tpInterv"].ToString();
                     incAlt.dadosHorContratual.horarioIntervalo.durInterv = i["durInterv"].ToString();
                     incAlt.dadosHorContratual.horarioIntervalo.iniInterv = i["iniInterv"].ToString();
                     incAlt.dadosHorContratual.horarioIntervalo.termInterv = i["termInterv"].ToString();

                     if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                        s1050XML.infoHorContratual.inclusao = incAlt;
                        s1050XML.add_horarioIntervalo();
                     }
                     else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {
                        s1050XML.infoHorContratual.alteracao = incAlt;
                        s1050XML.add_horarioIntervalo_alteracao();
                     }

                  }

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1050XML.infoHorContratual.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1050XML.infoHorContratual.alteracao = incAlt;
                  }

                  evento.eventoAssinadoXML = s1050XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1050", e.Message); }

         return lEventos;
      }
   }
}
