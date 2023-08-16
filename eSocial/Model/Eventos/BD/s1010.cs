using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1010 : bEvento_BD {

      XML.s1010 s1010XML;

      public s1010() : base("1010", "Rubricas", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1010XML = new XML.s1010(evento.id);

               // ### Evento

               // ideEvento
               s1010XML.ideEvento.tpAmb = evento.tpAmb;
               s1010XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1010XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1010XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1010XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  s1010XML.infoRubrica.exclusao.ideRubrica.codRubr = row["codRubr"].ToString();
                  s1010XML.infoRubrica.exclusao.ideRubrica.ideTabRubr = validadores.ideTabRubr(row["ideTabRubr"].ToString(), evento.nomeEmpresa, evento.nrInsc);
                  s1010XML.infoRubrica.exclusao.ideRubrica.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  s1010XML.infoRubrica.exclusao.ideRubrica.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
               }

               // inclusão / alteração
               else {

                  XML.s1010.sInfoRubrica.sIncAlt incAlt = new XML.s1010.sInfoRubrica.sIncAlt();

                  // ideRubrica
                  incAlt.ideRubrica.codRubr = row["codRubr"].ToString();
                  incAlt.ideRubrica.ideTabRubr = validadores.ideTabRubr(row["ideTabRubr"].ToString(), evento.nomeEmpresa, evento.nrInsc);
                  incAlt.ideRubrica.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.ideRubrica.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // dadosRubrica
                  incAlt.dadosRubrica.dscRubr = row["dscRubr"].ToString();
                  incAlt.dadosRubrica.natRubr = row["natRubr"].ToString();
                  incAlt.dadosRubrica.tpRubr = row["tpRubr"].ToString(); //validadores.tpRubr(row["tpRubr"].ToString());
                  incAlt.dadosRubrica.codIncCP = row["codIncCP"].ToString();
                  incAlt.dadosRubrica.codIncIRRF = row["codIncIRRF"].ToString();
                  incAlt.dadosRubrica.codIncFGTS = row["codIncFGTS"].ToString();
                  incAlt.dadosRubrica.codIncCPRP = row["codIncCPRP"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1010XML.infoRubrica.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1010XML.infoRubrica.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1010XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1010", e.Message); }

         return lEventos;
      }
   }
}
