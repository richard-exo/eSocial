using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   public class s2210 : bEvento_BD
   {
      XML.s2210 s2210XML;

      public s2210() : base("2210", "Comunicação de Acidente de Trabalho", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {

            foreach (DataRow row in tbEventos.Rows)
            {
               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s2210XML = new XML.s2210(evento.id);

               // ### Evento

               // ideEvento
               s2210XML.ideEvento.indRetif = row["indRetif"].ToString();
               s2210XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
               s2210XML.ideEvento.tpAmb = evento.tpAmb;
               s2210XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s2210XML.ideEvento.verProc = versao;

               //// ideRegistrador
               //s2210XML.ideRegistrador.tpRegistrador = row["tpRegistrador"].ToString();
               //s2210XML.ideRegistrador.tpInsc = row["tpInsc"].ToString();
               //s2210XML.ideRegistrador.nrInsc = row["nrInsc"].ToString();

               // ideEmpregador
               s2210XML.ideEmpregador.tpInsc = evento.tpInsc;
               s2210XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               //// ideTrabalhador
               s2210XML.ideVinculo.cpfTrab = row["cpfTrab"].ToString();
               s2210XML.ideVinculo.matricula = row["matricula"].ToString();   // 1.1
               s2210XML.ideVinculo.codCateg = row["codCateg"].ToString();     // 1.1

               //// iniAfastamento 0.1
               s2210XML.cat.dtAcid = validadores.aaaa_mm_dd(row["dtAcid"].ToString());
               s2210XML.cat.tpAcid = row["tpAcid"].ToString();
               s2210XML.cat.hrAcid = row["hrAcid"].ToString();
               s2210XML.cat.hrsTrabAntesAcid = row["hrsTrabAntesAcid"].ToString();
               s2210XML.cat.tpCat = row["tpCat"].ToString();
               s2210XML.cat.indCatObito = row["indCatObito"].ToString();
               s2210XML.cat.dtObito = validadores.aaaa_mm_dd(row["dtObito"].ToString());
               s2210XML.cat.indComunPolicia = row["indComunPolicia"].ToString();
               s2210XML.cat.codSitGeradora = row["codSitGeradora"].ToString();
               s2210XML.cat.iniciatCAT = row["iniciatCAT"].ToString();
               s2210XML.cat.obsCAT = row["obsCAT"].ToString();

               //// localAcidente
               s2210XML.cat.localAcidente.tpLocal = row["tpLocal"].ToString();
               s2210XML.cat.localAcidente.dscLocal = row["dscLocal"].ToString();
               s2210XML.cat.localAcidente.tpLograd = row["tpLograd"].ToString();
               s2210XML.cat.localAcidente.dscLograd = row["dscLograd"].ToString();
               s2210XML.cat.localAcidente.nrLograd = row["nrLograd"].ToString();
               s2210XML.cat.localAcidente.complemento = row["complemento"].ToString();
               s2210XML.cat.localAcidente.bairro = row["bairro"].ToString();
               s2210XML.cat.localAcidente.complemento = row["cep"].ToString();
               s2210XML.cat.localAcidente.codMunic = row["codMunic"].ToString();
               s2210XML.cat.localAcidente.uf = row["uf"].ToString();
               s2210XML.cat.localAcidente.pais = row["pais"].ToString();
               s2210XML.cat.localAcidente.codPostal = row["codPostal"].ToString();

               //// parteAtingida
               s2210XML.cat.parteAtingida.codParteAting = row["codParteAting"].ToString();
               s2210XML.cat.parteAtingida.lateralidade = row["lateralidade"].ToString();

               //// agenteCausador
               s2210XML.cat.agenteCausador.codAgntCausador = row["codAgntCausador"].ToString();

               // atestado
               // infoAtestado 0.9
               gcl.setLevel("atestado", clear: true);

               var tbInfoAtestado = from DataRow r in tbEventos.Rows
                                    where !string.IsNullOrEmpty(r["durTrat"].ToString()) &&
                                    r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                                    select r;

               foreach (var infoAtestado in tbInfoAtestado)
               {
       
                  s2210XML.cat.atestado.dtAtendimento = validadores.aaaa_mm_dd(row["dtAtendimento"].ToString());
                  s2210XML.cat.atestado.hrAtendimento = row["hrAtendimento"].ToString();
                  s2210XML.cat.atestado.indInternacao = row["indInternacao"].ToString();
                  s2210XML.cat.atestado.durTrat = row["durTrat"].ToString();
                  s2210XML.cat.atestado.indAfast = row["indAfast"].ToString();
                  s2210XML.cat.atestado.dscLesao = row["dscLesao"].ToString();
                  s2210XML.cat.atestado.dscCompLesao = row["dscCompLesao"].ToString();
                  s2210XML.cat.atestado.diagProvavel = row["diagProvavel"].ToString();
                  s2210XML.cat.atestado.codCID = row["codCID"].ToString();
                  s2210XML.cat.atestado.observacao = row["observacaoAtestado"].ToString();

                  s2210XML.cat.atestado.emitente.nmEmit = row["nmEmit"].ToString();
                  s2210XML.cat.atestado.emitente.ideOC = row["ideOC"].ToString();
                  s2210XML.cat.atestado.emitente.nrOC = row["nrOC"].ToString().Trim();
                  s2210XML.cat.atestado.emitente.ufOC = row["ufOC"].ToString();

                  s2210XML.add_atestado();
               }

               evento.eventoAssinadoXML = s2210XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2210", e.Message); }
         return lEventos;
      }
   }
}
