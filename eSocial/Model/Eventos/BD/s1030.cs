using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace eSocial.Model.Eventos.BD {
   public class s1030 : bEvento_BD {

      XML.s1030 s1030XML;

      public s1030() : base("1030", "Tab. de cargos /Empr. públicos", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1030XML = new XML.s1030(evento.id);

               // ### Evento

               // ideEvento
               s1030XML.ideEvento.tpAmb = evento.tpAmb;
               s1030XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1030XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1030XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1030XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1030XML.infoCargo.exclusao.ideCargo.codCargo = row["codCargo"].ToString();
                  s1030XML.infoCargo.exclusao.ideCargo.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1030XML.infoCargo.exclusao.ideCargo.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1030.sInfoCargo.sIncAlt incAlt = new XML.s1030.sInfoCargo.sIncAlt();

                  // ideCargo
                  incAlt.ideCargo.codCargo = row["codCargo"].ToString();
                  incAlt.ideCargo.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideCargo.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosCargo
                  string sNmCargo = "";
                  foreach (var c in row["nmCargo"].ToString()) { if (c >= 32 && c <= 126) { sNmCargo += c.ToString(); } } // Remove carácteres ilegais.

                  incAlt.dadosCargo.nmCargo = sNmCargo;
                  incAlt.dadosCargo.codCBO = row["codCBO"].ToString();

                  // cargoPublico 0.1
                  incAlt.dadosCargo.cargoPublico.acumCargo = row["acumCargo"].ToString();
                  incAlt.dadosCargo.cargoPublico.contagemEsp = row["contagemEsp"].ToString();
                  incAlt.dadosCargo.cargoPublico.dedicExcel = row["dedicExcel"].ToString();

                  // leiCargo
                  incAlt.dadosCargo.cargoPublico.leiCargo.nrLei = row["nrLei"].ToString();
                  incAlt.dadosCargo.cargoPublico.leiCargo.dtLei = row["dtLei"].ToString();
                  incAlt.dadosCargo.cargoPublico.leiCargo.sitCargo = row["sitCargo"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1030XML.infoCargo.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1030XML.infoCargo.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1030XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1030", e.Message); }

         return lEventos;
      }
   }
}
