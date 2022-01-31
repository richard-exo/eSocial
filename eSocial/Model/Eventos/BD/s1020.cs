using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1020 : bEvento_BD {

      XML.s1020 s1020XML;

      public s1020() : base("1020", "Tab. Lot. Tributárias", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1020XML = new XML.s1020(evento.id);

               // ### Evento

               // ideEvento
               s1020XML.ideEvento.tpAmb = evento.tpAmb;
               s1020XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1020XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1020XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1020XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1020XML.infoLotacao.exclusao.ideEstab.codLotacao = row["codFuncao"].ToString();
                  s1020XML.infoLotacao.exclusao.ideEstab.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1020XML.infoLotacao.exclusao.ideEstab.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1020.sInfoLotacao.sIncAlt incAlt = new XML.s1020.sInfoLotacao.sIncAlt();

                  // ideLotacao
                  incAlt.ideLotacao.codLotacao = row["codLotacao"].ToString();
                  incAlt.ideLotacao.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideLotacao.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosLotacao
                  incAlt.dadosLotacao.tpLotacao = row["tpLotacao"].ToString();
                  try { incAlt.dadosLotacao.tpInsc = row["tpInsc"].ToString(); } catch { }
                  try { incAlt.dadosLotacao.nrInsc = row["nrInsc"].ToString(); } catch { }

                  // dadosLotacao
                  incAlt.dadosLotacao.fpasLotacao.fPas = row["fPas"].ToString();
                  incAlt.dadosLotacao.fpasLotacao.codTercs = row["codTercs"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1020XML.infoLotacao.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1020XML.infoLotacao.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1020XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1020", e.Message); }

         return lEventos;
      }
   }
}
