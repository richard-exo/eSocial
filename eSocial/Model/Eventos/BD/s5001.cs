using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD
{
   public class s5001 : bEvento_BD
   {

      XML.s5001 s5001XML;

      public s5001() : base("5001", "Info. Contrib. Trabalhador", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {

            foreach (DataRow row in tbEventos.Rows)
            {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s5001XML = new XML.s5001(evento.id);

               // ### Evento

               // ideEvento
               s5001XML.ideEvento.nrRecArqBase = row["nrRecArqBase"].ToString();
               s5001XML.ideEvento.indApuracao = row["indApuracao"].ToString();
               s5001XML.ideEvento.perApur = row["perApur"].ToString();
               
               // ideEmpregador
               s5001XML.ideEmpregador.tpInsc = evento.tpInsc;
               s5001XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideTrabalhador
               s5001XML.ideTrabalhador.cpfTrab = row["cpfTrab"].ToString();
               s5001XML.ideTrabalhador.procJudTrab.codSusp= row["codSusp"].ToString();
               s5001XML.ideTrabalhador.procJudTrab.nrProcJud = row["nrProcJud"].ToString();

               // infoCpCalc
               s5001XML.infoCpCalc.tpCR = row["tpCR"].ToString();
               s5001XML.infoCpCalc.vrCpSeg = row["vrCpSeg"].ToString();
               s5001XML.infoCpCalc.vrDescSeg = row["vrDescSeg"].ToString();

               // infoCp
               s5001XML.infoCp.ideEstabLot.tpInsc = row["tpInsc"].ToString();
               s5001XML.infoCp.ideEstabLot.nrInsc = row["nrInsc"].ToString();
               s5001XML.infoCp.ideEstabLot.codLotacao = row["codLotacao_ideEstabLot"].ToString();
               s5001XML.infoCp.ideEstabLot.infoCategIncid.matricula = row["matricula_infoCategIncid"].ToString();
               s5001XML.infoCp.ideEstabLot.infoCategIncid.codCateg = row["codCateg_infoCategIncid"].ToString();
               s5001XML.infoCp.ideEstabLot.infoCategIncid.indSimples = row["indSimples_infoCategIncid"].ToString();
               //s5001XML.infoCp.ideEstabLot.calcTerc.tpCR = row["tpCR"].ToString();
               //s5001XML.infoCp.ideEstabLot.calcTerc.vrCsSegTerc = row["vrCsSegTerc"].ToString();
               //s5001XML.infoCp.ideEstabLot.calcTerc.vrDescTerc = row["vrDescTerc"].ToString();


               s5001XML.add_infoCategIncid();
               s5001XML.add_ideEstabLot();
               evento.eventoAssinadoXML = s5001XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s5001XML", e.Message); }

         return lEventos;
      }
   }
}
