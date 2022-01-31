using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD
{
   public class s5002 : bEvento_BD
   {

      XML.s5002 s5002XML;

      public s5002() : base("5002", "Imposto de Renda", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {

            foreach (DataRow row in tbEventos.Rows)
            {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s5002XML = new XML.s5002(evento.id);

               // ### Evento

               // ideEvento
               s5002XML.ideEvento.nrRecArqBase = row["nrRecArqBase"].ToString();
               s5002XML.ideEvento.perApur = row["perApur"].ToString();

               // ideEmpregador
               s5002XML.ideEmpregador.tpInsc = evento.tpInsc;
               s5002XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // ideTrabalhador
               s5002XML.ideTrabalhador.cpfTrab = row["cpfTrab"].ToString();

               // infoDep
               s5002XML.infoDep.vrDedDep = row["vrDedDep"].ToString();

               // infoIrrf
               s5002XML.infoIrrf.codCateg = row["codCateg"].ToString();
               s5002XML.infoIrrf.indResBr = row["indResBr"].ToString();

               // infoIrrf > basesIrrf
               s5002XML.infoIrrf.basesIrrf.tpValor = row["tpValor"].ToString();
               s5002XML.infoIrrf.basesIrrf.valor = row["valor"].ToString();

               // infoIrrf > irrf
               s5002XML.infoIrrf.irrf.tpCR = row["tpCR"].ToString();
               s5002XML.infoIrrf.irrf.vrIrrfDesc = row["vrIrrfDesc"].ToString();

               // infoIrrf > 
               s5002XML.infoIrrf.idePgtoExt.idePais.codPais = row["codPais"].ToString();
               s5002XML.infoIrrf.idePgtoExt.idePais.indNIF = row["indNIF"].ToString();
               s5002XML.infoIrrf.idePgtoExt.idePais.nifBenef = row["nifBenef"].ToString();

               // infoIrrf > idePgtoExt > endExt
               s5002XML.infoIrrf.idePgtoExt.endExt.dscLograd = row["dscLograd"].ToString();
               s5002XML.infoIrrf.idePgtoExt.endExt.nrLograd = row["nrLograd"].ToString();
               s5002XML.infoIrrf.idePgtoExt.endExt.complem = row["complem"].ToString();
               s5002XML.infoIrrf.idePgtoExt.endExt.bairro = row["bairro"].ToString();
               s5002XML.infoIrrf.idePgtoExt.endExt.nmCid = row["nmCid"].ToString();
               s5002XML.infoIrrf.idePgtoExt.endExt.codPostal = row["codPostal"].ToString();

               evento.eventoAssinadoXML = s5002XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s5002XML", e.Message); }

         return lEventos;
      }
   }
}
