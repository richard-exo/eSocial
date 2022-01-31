using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD {
   public class s2230 : bEvento_BD {

      XML.s2230 s2230XML;

      public s2230() : base("2230", "Afast. tempórário", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s2230XML = new XML.s2230(evento.id);

               // ### Evento

               // ideEvento
               s2230XML.ideEvento.indRetif = row["indRetif"].ToString();
               s2230XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
               s2230XML.ideEvento.tpAmb = evento.tpAmb;
               s2230XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s2230XML.ideEvento.verProc = versao;

               // ideEmpregador
               s2230XML.ideEmpregador.tpInsc = evento.tpInsc;
               s2230XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideVinculo
               s2230XML.ideVinculo.cpfTrab = row["cpfTrab"].ToString();
               s2230XML.ideVinculo.nisTrab = row["nisTrab"].ToString();     // 0.1
               s2230XML.ideVinculo.matricula = row["matricula"].ToString(); // 0.1
               s2230XML.ideVinculo.codCateg = row["codCateg"].ToString();   // 0.1

               // iniAfastamento 0.1
               s2230XML.infoAfastamento.iniAfastamento.dtIniAfast = validadores.aaaa_mm_dd(row["dtIniAfast"].ToString());
               s2230XML.infoAfastamento.iniAfastamento.codMotAfast = row["codMotAfast"].ToString();
               s2230XML.infoAfastamento.iniAfastamento.infoMesmoMtv = row["infoMesmoMtv"].ToString();     // 0.1
               s2230XML.infoAfastamento.iniAfastamento.tpAcidTransito = row["tpAcidTransito"].ToString(); // 0.1
               s2230XML.infoAfastamento.iniAfastamento.observacao = row["observacao"].ToString();         // 0.1

              
                // infoAtestado 0.9
                gcl.setLevel("infoAtestado", clear: true);

                var tbInfoAtestado = from DataRow r in tbEventos.Rows
                                    where !string.IsNullOrEmpty(r["qtdDiasAfast"].ToString()) &&
                                    r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                    select r;

                foreach (var infoAtestado in tbInfoAtestado)
                {
                    if (row["codMotAfast"].ToString() == "01" || (row["codMotAfast"].ToString() == "03") || (row["codMotAfast"].ToString() == "35"))
                    {
                        s2230XML.infoAfastamento.iniAfastamento.infoAtestado.codCID = row["codCID"].ToString();             // 0.1
                        s2230XML.infoAfastamento.iniAfastamento.infoAtestado.qtdDiasAfast = row["qtdDiasAfast"].ToString();

                        if (row["codMotAfast"].ToString() == "01")
                        {
                            s2230XML.infoAfastamento.iniAfastamento.infoAtestado.emitente.nmEmit = row["nmEmit"].ToString();
                            s2230XML.infoAfastamento.iniAfastamento.infoAtestado.emitente.ideOC = row["ideOC"].ToString();
                            s2230XML.infoAfastamento.iniAfastamento.infoAtestado.emitente.nrOc = row["nrOc"].ToString();
                            s2230XML.infoAfastamento.iniAfastamento.infoAtestado.emitente.ufOC = row["ufOC"].ToString();        // 0.1
                        }
                        s2230XML.add_infoAtestado();
                    }
                }
               

               // infoCessao 0.1
               s2230XML.infoAfastamento.iniAfastamento.infoCessao.cnpjCess = row["cnpjCess"].ToString();
               s2230XML.infoAfastamento.iniAfastamento.infoCessao.infOnus = row["infOnus"].ToString();

               // infoMandSind 0.1
               s2230XML.infoAfastamento.iniAfastamento.infoMandSind.cnpjSind = row["cnpjSind"].ToString();
               s2230XML.infoAfastamento.iniAfastamento.infoMandSind.infOnusRemun = row["infOnusRemun"].ToString();

               // infoRetif 0.1
               s2230XML.infoAfastamento.infoRetif.origRetif = row["origRetif"].ToString();
               s2230XML.infoAfastamento.infoRetif.tpProc = row["tpProc"].ToString();       // 0.1
               s2230XML.infoAfastamento.infoRetif.nrProc = row["nrProc"].ToString();

               // fimAfastamento 0.1
               s2230XML.infoAfastamento.fimAfastamento.dtTermAfast = validadores.aaaa_mm_dd(row["dtTermAfast"].ToString());

               evento.eventoAssinadoXML = s2230XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2230", e.Message); }
         return lEventos;
      }
   }
}
