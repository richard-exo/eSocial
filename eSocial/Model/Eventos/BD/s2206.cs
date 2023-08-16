using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   public class s2206 : bEvento_BD
   {
      XML.s2206 s2206XML;

      public s2206() : base("2206", "Alteração de Contrato de Trabalho", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {
            List<string> lista2206 = new List<string>();

            foreach (DataRow row in tbEventos.Rows)
            {
               if (!lista2206.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista2206.Add(row["id_funcionario"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2206XML = new XML.s2206(evento.id);

                  // ### Evento

                  // ideEvento
                  s2206XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2206XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1

                  s2206XML.ideEvento.tpAmb = evento.tpAmb; //.GetHashCode().ToString();
                  s2206XML.ideEvento.procEmi = enProcEmi.appEmpregador_1; //.GetHashCode().ToString();
                  s2206XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2206XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2206XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideVinculo
                  s2206XML.ideVinculo.cpfTrab = row["cpfTrab_trabalhador"].ToString();
                  s2206XML.ideVinculo.nisTrab = row["nisTrab_trabalhador"].ToString();
                  s2206XML.ideVinculo.matricula = row["matricula_trabalhador"].ToString();

                  // altContratual
                  s2206XML.altContratual.dtAlteracao = validadores.aaaa_mm_dd(row["dtAlteracao"].ToString());
                  s2206XML.altContratual.dtEf = validadores.aaaa_mm_dd(row["dtEf"].ToString());
                  s2206XML.altContratual.dscAlt = row["dscAlt"].ToString();

                  // vinculo
                  s2206XML.altContratual.vinculo.tpRegPrev = row["tpRegPrev"].ToString();

                  // inforRegimeTrab\\infoCeletista
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.tpRegJor = row["tpRegJor"].ToString();
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.natAtividade = row["natAtividade"].ToString();
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.dtBase = validadores.aaaa_mm_dd(row["dtBase"].ToString());
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.cnpjSindCategProf = row["cnpjSindCategProf"].ToString();

                  // trabTemp
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.trabTemp.justProrr = row["justProrr"].ToString();

                  // aprend
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.aprend.tpInsc = row["tpInsc_aprend"].ToString();
                  s2206XML.altContratual.infoRegimeTrab.infoCeletista.aprend.nrInsc = row["nrInsc_aprend"].ToString();

                  // infoEstatutario
                  s2206XML.altContratual.infoRegimeTrab.infoEstatutario.tpPlanRP = row["tpPlanRP"].ToString();

                  // infoContrato
                  s2206XML.altContratual.infoContrato.nmCargo = row["nmCargo"].ToString();
                  s2206XML.altContratual.infoContrato.CBOCargo = row["CBOCargo"].ToString();
                  s2206XML.altContratual.infoContrato.nmFuncao = row["nmFuncao"].ToString();
                  s2206XML.altContratual.infoContrato.CBOFuncao = row["CBOFuncao"].ToString();
                  s2206XML.altContratual.infoContrato.acumCargo = row["acumCargo"].ToString();
                  s2206XML.altContratual.infoContrato.codCateg = row["codCateg"].ToString();

                  // remuneracao
                  s2206XML.altContratual.infoContrato.remuneracao.vrSalFx = row["vrSalFx"].ToString().Replace(",", ".");
                  s2206XML.altContratual.infoContrato.remuneracao.undSalFixo = row["undSalFixo"].ToString();
                  s2206XML.altContratual.infoContrato.remuneracao.dscSalVar = row["dscSalVar"].ToString();

                  // duracao
                  s2206XML.altContratual.infoContrato.duracao.tpContr = row["tpContr"].ToString();

                  if (row["tpContr"].ToString() == "2")
                     s2206XML.altContratual.infoContrato.duracao.dtTerm = validadores.aaaa_mm_dd(row["dtTerm"].ToString());

                  if (row["tpContr"].ToString() == "3")
                     s2206XML.altContratual.infoContrato.duracao.objDet = row["objDet"].ToString();

                  // localTrabGeral
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabGeral.tpInsc = row["tpInsc_localTrabGeral"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabGeral.nrInsc = row["nrInsc_localTrabGeral"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabGeral.descComp = row["descComp_localTrabGeral"].ToString();

                  // localTrabDom
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.tpLograd = row["tpLograd"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.dscLograd = row["dscLograd"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.nrLograd = row["nrLograd"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.complemento = row["complemento"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.bairro = row["bairro"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.cep = row["cep"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.codMunic = row["codMunic"].ToString();
                  s2206XML.altContratual.infoContrato.localTrabalho.localTrabDom.uf = row["uf"].ToString();

                  // horContratual
                  s2206XML.altContratual.infoContrato.horContratual.qtdHrsSem  = row["qtdHrsSem"].ToString();
                  s2206XML.altContratual.infoContrato.horContratual.tpJornada  = row["tpJornada"].ToString();
                  s2206XML.altContratual.infoContrato.horContratual.dscTpJorn  = row["dscTpJorn"].ToString();
                  s2206XML.altContratual.infoContrato.horContratual.horNoturno = row["horNoturno"].ToString(); 
                  s2206XML.altContratual.infoContrato.horContratual.dscJorn    = row["dscJorn"].ToString();
                  s2206XML.altContratual.infoContrato.horContratual.tmpParc    = row["tmpParc"].ToString();

                  // filiacaoSindical
                  s2206XML.altContratual.infoContrato.filiacaoSindical.cnpjSindTrab = row["cnpjSindTrab"].ToString();

                  // alvaraJudicial
                  s2206XML.altContratual.infoContrato.alvaraJudicial.nrProcJud = row["nrProcJud"].ToString();

                  // observacao 0.9
                  //List<string> lista2206Observacao = new List<string>();

                  //gcl.setLevel("observacoes", clear: true);

                  //var tbObservacao = from DataRow r in tbEventos.Rows
                  //                where !string.IsNullOrEmpty(r[$"observacao{gcl.getLevel}"].ToString()) &&
                  //                r["id_funcionario"].ToString().Equals(evento.id_funcionario)
                  //                select r;

                  //foreach (var observacoes in tbObservacao)
                  //{
                  //   gcl.setLevel(row: observacoes);

                  //   if (!lista2206Observacao.Contains(gcl.getVal("seq").ToString()))
                  //   {
                  //      // Registra observacao
                  //      lista2206Observacao.Add(gcl.getVal("seq").ToString());

                  //      // observacoes
                  //      s2206XML.altContratual.infoContrato.observacoes.observacao = gcl.getVal("observacao");  
                  //      s2206XML.add_observacoes();
                  //   }
                  //}                             

                  // observacao
                  s2206XML.altContratual.infoContrato.observacoes.observacao = row["observacao"].ToString();

                  // servPubl
                  s2206XML.altContratual.infoContrato.treiCap.codTreiCap = row["codTreiCap"].ToString();

                  evento.eventoAssinadoXML = s2206XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2206", e.Message); }
         return lEventos;
      }
   }
}
