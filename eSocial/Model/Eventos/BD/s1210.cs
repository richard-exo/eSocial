using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD {
   public class s1210 : bEvento_BD {

      XML.s1210 s1210XML;

      public s1210() : base("1210", "Pag. de rendimentos do trab.", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            // Armazena os funcionários transmitidos
            List<string> lista1210 = new List<string>();
            List<string> lista1210info = new List<string>();
            List<string> lista1210detPgto = new List<string>();
            List<string> lista1210retPgtoTot = new List<string>();
            List<string> lista1210detPgtoFer = new List<string>();
            List<string> lista1210detPgtoFer_eventos = new List<string>();
            List<string> lista1210penAlim_detRubrFer_eventos = new List<string>();
            List<string> lista1210detPgtoAnt = new List<string>();
            List<string> lista1210infoPgtoAnt = new List<string>();
				List<string> lista1210penAlim = new List<string>();
            string sIDchave; double nLiquido;

				foreach (DataRow row in tbEventos.Rows) {

               // Só executa 1x para cada funcionário
               if (!lista1210.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista1210.Add(row["id_funcionario"].ToString());

                  string sTpPgto = "";
                  string sMesano = row["mesano"].ToString();

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s1210XML = new XML.s1210(evento.id);

                  // ### Evento

                  // ideEvento
                  s1210XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s1210XML.ideEvento.nrRecibo = row["nrRecibo"].ToString();
                  s1210XML.ideEvento.indApuracao = row["indApuracao"].ToString();
                  s1210XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString());
                  s1210XML.ideEvento.tpAmb = evento.tpAmb;
                  s1210XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s1210XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s1210XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s1210XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideBenef
                  s1210XML.ideBenef.cpfBenef = row["cpfBenef"].ToString();

                  // deps 0.1
                  if (row["vrDedDeps"].ToString()!="0")
                     s1210XML.ideBenef.deps.vrDedDep = row["vrDedDeps"].ToString();
                  
                  // infoPgto 1.60
                  gcl.setLevel("infoPgto");

                  var tbInfoPgto = from DataRow r in tbEventos.Rows
                                   where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                   !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString())
                                   select r;

                  foreach (var infoPgto in tbInfoPgto)
                  {

                     gcl.setLevel(row: infoPgto);
                     sIDchave = gcl.getVal("idData").ToString();
                     nLiquido = Convert.ToDouble(gcl.getVal("vrLiq_detPgtoFl"));

                     // Só executa 1x para cada funcionario
                     if (!lista1210info.Contains(sIDchave) && nLiquido>0.00)
                     {
                        // Registra o funcionário
                        lista1210info.Add(sIDchave);

                        sTpPgto = gcl.getVal("tpPgto");
                        s1210XML.ideBenef.infoPgto.dtPgto = validadores.aaaa_mm_dd(gcl.getVal("dtPgto"));
								s1210XML.ideBenef.infoPgto.tpPgto = gcl.getVal("tpPgto");
								s1210XML.ideBenef.infoPgto.perRef = validadores.aaaa_mm(gcl.getVal("perRef"));
                        s1210XML.ideBenef.infoPgto.ideDmDev = gcl.getVal("ideDmDev");
                        s1210XML.ideBenef.infoPgto.vrLiq = gcl.getVal("vrLiq").Replace(",", ".");

                        s1210XML.add_infoPgto();
                        gcl.setLevel("infoPgto", clear: true);
                     }
                  }

                  evento.eventoAssinadoXML = s1210XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }

         catch (Exception e) {
            addError("model.eventos.BD.s1210", e.Message);
         }

         return lEventos;
      }
   }
}
