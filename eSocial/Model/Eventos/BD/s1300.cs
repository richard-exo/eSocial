using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD
{
   class s1300 : bEvento_BD
   {
      XML.s1300 s1300XML;

      public s1300() : base("1300", "Contribuição Sindical Patronal", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes()
      {
         base.getEventosPendentes();

         try
         {
            foreach (DataRow row in tbEventos.Rows)
            {
               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1300XML = new XML.s1300(evento.id);

               // ### Evento
               // ideEvento
               s1300XML.ideEvento.indRetif = row["indRetif"].ToString();
               s1300XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
               s1300XML.ideEvento.indApuracao = row["indApuracao"].ToString(); // 0.1
               s1300XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString()); // 0.1
               s1300XML.ideEvento.tpAmb = evento.tpAmb;
               s1300XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1300XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1300XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1300XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // contribSind
               s1300XML.contribSind.cnpjSindic = row["cnpjSindic"].ToString();
               s1300XML.contribSind.tpContribSind = row["tpContribSind"].ToString();
               s1300XML.contribSind.vlrContribSind = row["vlrContribSind"].ToString().Replace(",", "."); 

               evento.eventoAssinadoXML = s1300XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1300", e.Message); }
         return lEventos;
      }
   }
}
