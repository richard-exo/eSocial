using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   public class s2306 : bEvento_BD
   {

      XML.s2306 s2306XML;

      public s2306() : base("2306", "Trabalhador sem vinculo de emprego - Alteração", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes()
      {

         base.getEventosPendentes();

         try
         {

            List<string> lista2306 = new List<string>();

            foreach (DataRow row in tbEventos.Rows)
            {
               // Só executa 1x para cada funcionário
               if (!lista2306.Contains(row["id_autonomo"].ToString()))
               {
                  // Registra o funcionário
                  lista2306.Add(row["id_autonomo"].ToString());

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s2306XML = new XML.s2306(evento.id);

                  // ### Evento

                  // ideEvento
                  s2306XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s2306XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                  s2306XML.ideEvento.tpAmb = evento.tpAmb;
                  s2306XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s2306XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s2306XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s2306XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // trabalhador
                  gcl.setLevel("ideTrabSemVinculo", row);

                  s2306XML.ideTrabSemVinculo.cpfTrab = gcl.getVal("cpfTrab");
                  s2306XML.ideTrabSemVinculo.matricula = gcl.getVal("matricula");
                  s2306XML.ideTrabSemVinculo.codCateg = gcl.getVal("codCateg");

                  // vinculo 0.1
                  gcl.setLevel("infoTSVAlteracao", clear: true);

                  s2306XML.infoTSVAlteracao.dtAlteracao = validadores.aaaa_mm_dd(gcl.getVal("dtAlteracao"));

                  if (gcl.getVal("natAtividade") != "0")
                     s2306XML.infoTSVAlteracao.natAtividade = gcl.getVal("natAtividade"); // 0.1

                  // cargoFuncao 0.1
                  gcl.setLevel("cargoFuncao", clear: true);

                  s2306XML.infoTSVAlteracao.infoComplementares.cargoFuncao.nmCargo = gcl.getVal("nmCargo");
                  s2306XML.infoTSVAlteracao.infoComplementares.cargoFuncao.CBOCargo = gcl.getVal("CBOCargo");
                  s2306XML.infoTSVAlteracao.infoComplementares.cargoFuncao.codCargo = gcl.getVal("codCargo");
                  s2306XML.infoTSVAlteracao.infoComplementares.cargoFuncao.codFuncao = gcl.getVal("codFuncao");

                  // remuneracao 0.1
                  gcl.setLevel("remuneracao", clear: true);

                  s2306XML.infoTSVAlteracao.infoComplementares.remuneracao.vrSalFx = gcl.getVal("vrSalFx").Replace(",", ".");
                  s2306XML.infoTSVAlteracao.infoComplementares.remuneracao.undSalFixo = gcl.getVal("undSalFixo");
                  s2306XML.infoTSVAlteracao.infoComplementares.remuneracao.dscSalVar = gcl.getVal("dscSalVar");   // 0.1

                  // infoDirigenteSindical 0.1
                  gcl.setLevel("infoDirigenteSindical", clear: true);

                  s2306XML.infoTSVAlteracao.infoComplementares.infoDirigenteSindical.tpRegPrev = gcl.getVal("tpRegPrev");


                  // infoTrabCedido 0.1
                  gcl.setLevel("infoTrabCedido", clear: true);

                  s2306XML.infoTSVAlteracao.infoComplementares.infoTrabCedido.tpRegPrev = gcl.getVal("tpRegPrev");

                  // infoEstagiario 0.1
                  gcl.setLevel("infoEstagiario", clear: true);

                  if (gcl.getVal("natEstagio") != "")
                  {
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.natEstagio = gcl.getVal("natEstagio");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.nivEstagio = gcl.getVal("nivEstagio");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.areaAtuacao = gcl.getVal("areaAtuacao");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.nrApol = gcl.getVal("nrApol");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.dtPrevTeam = validadores.aaaa_mm_dd(gcl.getVal("dtPrevTeam"));
                  }

                  // instEnsino 
                  gcl.setLevel("instEnsino", row, clear: true);

                  if (gcl.getVal("cnpjInstEnsino") != "")
                  {
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.cnpjInstEnsino = gcl.getVal("cnpjInstEnsino");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.nmRazao = gcl.getVal("nmRazao");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.dscLograd = gcl.getVal("dscLograd");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.nrLograd = gcl.getVal("nrLograd");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.bairro = gcl.getVal("bairro");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.cep = gcl.getVal("cep");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.codMunic = gcl.getVal("codMunic");
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.uf = gcl.getVal("uf");
                  }

                  // ageIntegracao 0.1
                  gcl.setLevel("ageIntegracao", clear: true);

                  if (gcl.getVal("cnpjAgntInteg") != "")
                  {
                     s2306XML.infoTSVAlteracao.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg = gcl.getVal("cnpjAgntInteg");
                  }

                  evento.eventoAssinadoXML = s2306XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s2306", e.Message); }
         return lEventos;
      }
   }
}
