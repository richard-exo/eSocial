using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD
{
   class s2260 : bEvento_BD
   {
      XML.s2260 s2260XML;

      public s2260() : base("2260", "Convocação para Trabalho Intermitente", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {
         base.getEventosPendentes();

         try
         {
            foreach (DataRow row in tbEventos.Rows)
            {
               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s2260XML = new XML.s2260(evento.id);

               // ### Evento
               // ideEvento
               s2260XML.ideEvento.indRetif = row["indRetif"].ToString();
               s2260XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
               s2260XML.ideEvento.tpAmb = evento.tpAmb;
               s2260XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s2260XML.ideEvento.verProc = versao;

               // ideEmpregador
               s2260XML.ideEmpregador.tpInsc = evento.tpInsc;
               s2260XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideVinculo
               s2260XML.ideVinculo.cpfTrab = row["cpfTrab"].ToString();
               s2260XML.ideVinculo.nisTrab = row["nisTrab"].ToString();
               s2260XML.ideVinculo.matricula = row["matricula"].ToString();

               // infoConvInterm
               s2260XML.infoConvInterm.codConv = row["codConv"].ToString();
               s2260XML.infoConvInterm.dtInicio = validadores.aaaa_mm_dd(row["dtInicio"].ToString());
               s2260XML.infoConvInterm.dtFim = validadores.aaaa_mm_dd(row["dtFim"].ToString());
               s2260XML.infoConvInterm.dtPrevPgto = validadores.aaaa_mm_dd(row["dtPrevPgto"].ToString());

               // > jornada
               s2260XML.infoConvInterm.jornada.codHorContrat = row["codHorContrat"].ToString();
               s2260XML.infoConvInterm.jornada.dscJornada = row["dscJornada"].ToString();

               // > localTrab
               s2260XML.infoConvInterm.localTrab.indLocal = row["indLocal"].ToString();

               // > localTrabInterm
               s2260XML.infoConvInterm.localTrabInterm.tpLograd = row["tpLograd"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.dscLograd = row["dscLograd"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.nrLograd = row["nrLograd"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.complem = row["complem"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.bairro = row["bairro"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.cep = row["cep"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.codMunic = row["codMunic"].ToString();
               s2260XML.infoConvInterm.localTrabInterm.uf = row["uf"].ToString();

               evento.eventoAssinadoXML = s2260XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2260", e.Message); }
         return lEventos;
      }
   }
}
