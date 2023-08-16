using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1005 : bEvento_BD {

      XML.s1005 s1005XML;

      public s1005() : base("1005", "Estab. / Obras ou unidades", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1005XML = new XML.s1005(evento.id);

               // ### Evento

               // ideEvento
               s1005XML.ideEvento.tpAmb = evento.tpAmb;
               s1005XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1005XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1005XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1005XML.ideEmpregador.nrInsc = (row["caepf"].ToString()=="1" ? row["nrInsc"].ToString() : validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid));

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1005XML.infoEstab.exclusao.ideEstab.tpInsc = row["tpInscIdeEstab"].ToString();
                  s1005XML.infoEstab.exclusao.ideEstab.nrInsc = row["nrInscIdeEstab"].ToString();
                  s1005XML.infoEstab.exclusao.ideEstab.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1005XML.infoEstab.exclusao.ideEstab.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1005.sInfoEstab.sIncAlt incAlt = new XML.s1005.sInfoEstab.sIncAlt();

                  // ideEstab
                  incAlt.ideEstab.tpInsc = row["tpInscIdeEstab"].ToString();
                  incAlt.ideEstab.nrInsc = row["nrInscIdeEstab"].ToString();
                  incAlt.ideEstab.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideEstab.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosEstab
                  incAlt.dadosEstab.cnaePrep = row["cnaePrep"].ToString();

                  // aliqGilrat
                  //if (row["aliqRat"].ToString()!="0,00")
                  //{
                     if (row["aliqRat"].ToString()!="0")
                        incAlt.dadosEstab.aliqGilrat.aliqRat = row["aliqRat"].ToString();

                     if (row["tpInsc"].ToString() != "2") // Se não for CPF
                     {
                        if (row["tpInsc"].ToString() == "3") // Se CAEPF
                        {
                           incAlt.dadosEstab.infoCaepf.tpCaepf = row["tpCaepf"].ToString();
                        }
                        else
                        {
                           incAlt.dadosEstab.aliqGilrat.fap = row["fap"].ToString();
                           incAlt.dadosEstab.aliqGilrat.aliqRatAjust = row["aliqRatAjust"].ToString();
                        }
                     }
                  //}

                  // infoTrab
                  incAlt.dadosEstab.infoTrab.regPt = row["regPt"].ToString();

                  // infoApr
                  incAlt.dadosEstab.infoTrab.infoApr.contApr = row["contApr"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1005XML.infoEstab.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1005XML.infoEstab.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1005XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1005", e.Message); }
         return lEventos;
      }
   }
}
