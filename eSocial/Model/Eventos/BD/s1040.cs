using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1040 : bEvento_BD {

      XML.s1040 s1040XML;

      public s1040() : base("1040", "Funções / Cargos em comic.", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1040XML = new XML.s1040(evento.id);

               // ### Evento

               // ideEvento
               s1040XML.ideEvento.tpAmb = evento.tpAmb;
               s1040XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1040XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1040XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1040XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1040XML.infoFuncao.exclusao.ideFuncao.codFuncao = row["codFuncao"].ToString();
                  s1040XML.infoFuncao.exclusao.ideFuncao.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1040XML.infoFuncao.exclusao.ideFuncao.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1040.sInfoFuncao.sIncAlt incAlt = new XML.s1040.sInfoFuncao.sIncAlt();

                  // ideFuncao
                  incAlt.ideFuncao.codFuncao = row["codFuncao"].ToString();
                  incAlt.ideFuncao.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideFuncao.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosFuncao
                  incAlt.dadosFuncao.dscFuncao = row["dscFuncao"].ToString();
                  incAlt.dadosFuncao.codCBO = row["codCBO"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1040XML.infoFuncao.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1040XML.infoFuncao.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1040XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1040", e.Message); }

         return lEventos;
      }
   }
}
