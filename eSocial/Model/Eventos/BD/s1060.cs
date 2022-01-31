using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD {
   public class s1060 : bEvento_BD {

      XML.s1060 s1060XML;

      public s1060() : base("1060", "Amb. Trabalho", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in (from DataRow r in tbEventos.Rows select r).Take(1)) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1060XML = new XML.s1060(evento.id);

               // ### Evento

               // ideEvento
               s1060XML.ideEvento.tpAmb = evento.tpAmb;
               s1060XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1060XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1060XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1060XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1060XML.infoAmbiente.exclusao.ideAmbiente.codAmb = row["codAmb"].ToString();
                  s1060XML.infoAmbiente.exclusao.ideAmbiente.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1060XML.infoAmbiente.exclusao.ideAmbiente.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1060.sInfoAmbiente.sIncAlt incAlt = new XML.s1060.sInfoAmbiente.sIncAlt();

                  // IdeHorContratual
                  incAlt.ideAmbiente.codAmb = row["codAmb"].ToString();
                  incAlt.ideAmbiente.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideAmbiente.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosHorContratual
                  incAlt.dadosAmbiente.nmAmb = row["nmAmb"].ToString();
                  incAlt.dadosAmbiente.dscAmb = row["dscAmb"].ToString();
                  incAlt.dadosAmbiente.localAmb = row["localAmb"].ToString();
                  incAlt.dadosAmbiente.tpInsc = row["tpInsc"].ToString();
                  incAlt.dadosAmbiente.nrInsc = row["nrInsc"].ToString();

                  // fatorRisco 1.99
                  foreach (var r in from DataRow r in tbEventos.Rows
                                    where r["id_funcionario"].ToString().Equals(row["id_funcionario"].ToString()) &&
                                    !string.IsNullOrEmpty(r["codFatRis"]?.ToString())
                                    select r) {

                     incAlt.dadosAmbiente.fatorRisco.codFatRis = r["codFatRis"].ToString();

                     if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) { s1060XML.add_fatorRisco_inclusao(); }
                     else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) { s1060XML.add_fatorRisco_alteracao(); }
                  }

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1060XML.infoAmbiente.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1060XML.infoAmbiente.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1060XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) {
            addError("model.eventos.BD.s1060", e.Message);
         }

         return lEventos;
      }
   }
}
