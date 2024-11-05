using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using eSocial.Model.Eventos.BD;
using System.Configuration;
using eSocial.Model.Eventos.Retorno;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static eSocial.cBase;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using eSocial.Controller;

namespace eSocial.Model
{
   public sealed class model
   {
      string sFileConfig = "c:\\eSocial\\config.xml";

      // Primeira execução: controller chama model e ainda não existe enControl
      //eSocial.Controller.controller.controls.getControl(eSocial.Controller.controller.controls.enControls.chkTabela);
      //System.Windows.Forms.Control x = controller.controls.getControl(controller.controls.enControls.chkTabela);

      List<bEvento_BD> lEvento = new List<bEvento_BD>();

      public model()
      {
         if (System.IO.File.Exists(sFileConfig))
         {
            // Tabelas
            if (BuscaConfig("s1000") == "1") lEvento.Add(new s1000());
            if (BuscaConfig("s1005") == "1") lEvento.Add(new s1005());
            if (BuscaConfig("s1010") == "1") lEvento.Add(new s1010());
            if (BuscaConfig("s1020") == "1") lEvento.Add(new s1020());
            if (BuscaConfig("s1030") == "1") lEvento.Add(new s1030());
            if (BuscaConfig("s1040") == "1") lEvento.Add(new s1040());
            if (BuscaConfig("s1050") == "1") lEvento.Add(new s1050());
            if (BuscaConfig("s1060") == "1") lEvento.Add(new s1060());

            // Não periódicos
            if (BuscaConfig("s2190") == "1") lEvento.Add(new s2190());
            if (BuscaConfig("s2200") == "1") lEvento.Add(new s2200());
            if (BuscaConfig("s2205") == "1") lEvento.Add(new s2205());
            if (BuscaConfig("s2206") == "1") lEvento.Add(new s2206());
            if (BuscaConfig("s2210") == "1") lEvento.Add(new s2210());
            if (BuscaConfig("s2220") == "1") lEvento.Add(new s2220());
            if (BuscaConfig("s2230") == "1") lEvento.Add(new s2230());
            if (BuscaConfig("s2240") == "1") lEvento.Add(new s2240());
            if (BuscaConfig("s2250") == "1") lEvento.Add(new s2250());
            if (BuscaConfig("s2299") == "1") lEvento.Add(new s2299());
            if (BuscaConfig("s2300") == "1") lEvento.Add(new s2300());
            if (BuscaConfig("s2306") == "1") lEvento.Add(new s2306());
            if (BuscaConfig("s2399") == "1") lEvento.Add(new s2399());
            if (BuscaConfig("s2500") == "1") lEvento.Add(new s2500());
            if (BuscaConfig("s2501") == "1") lEvento.Add(new s2501());
            if (BuscaConfig("s2555") == "1") lEvento.Add(new s2555());
            if (BuscaConfig("s3000") == "1") lEvento.Add(new s3000());

            // Periódicos
            if (BuscaConfig("s1200") == "1") lEvento.Add(new s1200());
            if (BuscaConfig("s1210") == "1") lEvento.Add(new s1210());
            if (BuscaConfig("s1298") == "1") lEvento.Add(new s1298());
            if (BuscaConfig("s1299") == "1") lEvento.Add(new s1299());
            if (BuscaConfig("s1300") == "1") lEvento.Add(new s1300());

            // Bases
            if (BuscaConfig("s5001") == "1") lEvento.Add(new s5001());
            if (BuscaConfig("s5002") == "1") lEvento.Add(new s5002());
         }
         else
         {
            // Tabelas
            if (ConfigurationManager.AppSettings["evento_tabela"] == "1")
            {
               lEvento.Add(new s1000());
               lEvento.Add(new s1005());
               lEvento.Add(new s1010());
               lEvento.Add(new s1020());
               lEvento.Add(new s1030());
               lEvento.Add(new s1040());
               lEvento.Add(new s1050());
               lEvento.Add(new s1060());
            }

            // Não periódicos
            if (ConfigurationManager.AppSettings["evento_naoPeriodico"] == "1")
            {
               lEvento.Add(new s2190());
               lEvento.Add(new s2200());
               lEvento.Add(new s2205());
               lEvento.Add(new s2206());
               lEvento.Add(new s2210());
               lEvento.Add(new s2220());
               lEvento.Add(new s2230());
               lEvento.Add(new s2240());
               lEvento.Add(new s2250());
               lEvento.Add(new s2299());
               lEvento.Add(new s2300());
               lEvento.Add(new s2306());
               lEvento.Add(new s2399());
               lEvento.Add(new s2500());
               lEvento.Add(new s2501());
               lEvento.Add(new s2555());
               lEvento.Add(new s3000());
            }

            // Periódicos
            if (ConfigurationManager.AppSettings["evento_periodico"] == "1")
            {
               lEvento.Add(new s1200());
               lEvento.Add(new s1210());
               lEvento.Add(new s1298());
               lEvento.Add(new s1299());
               lEvento.Add(new s1300());
            }

            // Bases
            //lEvento.Add(new s5001());
            //lEvento.Add(new s5002());
         }
      }

      /* Ex.:
       Evento: S1200
               > Empresa X
                 > Evento [0] S1200 da empresa X
               > Empresa Y
                 > Evento [0] S1200 da empresa Y
                 > Evento [1] S1200 da empresa Y
      */

      public List<sLote> getEventosPendentes(enTipoEvento tipoEvento)
      {
         List<sLote> lRet = new List<sLote>();
         List<string> lista1210 = new List<string>();

         // Orderna e itera os eventos do tipo selecionado
         // Ex.: S1000, S1200
         foreach (var eventos in lEvento
                  .Where(t => t.tipoEvento.Equals(tipoEvento))
                  .OrderBy(o => o.nomeEvento))
         {

            try
            {
               // Agrupa por empresa os eventos dentro do evento selecionado.
               // Ex.: S1200 => Empresa X => S1200 [0], S1200 [1] | Empresa Y => S1200 [0]
               foreach (var lote in eventos.getEventosPendentes()
                        .GroupBy(g => new { g.nomeEmpresa, g.id_arquivo, g.id_cliente, g.id_funcionario }))
               {

                  // Retorna as informações da empresa (Os eventos da mesma empresa estão agrupados, 
                  // portanto as informações serão iguais para todos os eventos da mesma)
                  var infoEmpresa = lote.ToList().FirstOrDefault();

                  // Cria lotes com o limite estabelecido em 'qtdEvtLote'
                  int iCurrEvento = 0;
                  var ieEventos = lote.ToList().GetEnumerator();

                  List<Tuple<string, string, string, string, string>> lId_evento = new List<Tuple<string, string, string, string, string>>();
                  List<sEvento> lEvento = new List<sEvento>();

                  for (int e = 0; e <= qtdEvtLote - 1; e++)
                  {
                     if (iCurrEvento >= lote.ToList().Count) { break; }

                     iCurrEvento++;
                     ieEventos.MoveNext(); lEvento.Add(ieEventos.Current);

                     Boolean bSegue = false;
                     // Só executa 1x para cada arquivo
                     if (ieEventos.Current.nomeEvento == "1210")
                     {
                        if (!lista1210.Contains(ieEventos.Current.id_arquivo))
                        {
                           lista1210.Add(ieEventos.Current.id_arquivo);
                           bSegue = true;
                        }
                     }
                     else
                        bSegue = true;

                     if (bSegue)
                        lId_evento.Add(new Tuple<string, string, string, string, string>(ieEventos.Current.id_empresa, ieEventos.Current.id_arquivo, ieEventos.Current.nomeEvento, ieEventos.Current.id, ieEventos.Current.id_evento));
                  }

                  lRet.Add(new sLote(

                      infoEmpresa.nomeEmpresa,
                      eventos.nomeEvento,
                      eventos.descricaoEvento,

                      infoEmpresa.id_empresa,
                      infoEmpresa.id_cliente,
                      infoEmpresa.id_funcionario,

                      infoEmpresa.ideTransmissor,
                      infoEmpresa.ideEmpregador,
                      lEvento,

                      infoEmpresa.id_arquivo,
                      lId_evento,
                      lEvento.ToList().First().certificado));

                  // Caso o lote seja maior que o limite, insere um parcial para envio posterior.
                  if (lote.Count() > qtdEvtLote) { lRet.Add(new sLote(eventos.nomeEvento, lote.Key.nomeEmpresa, eventos.descricaoEvento, infoEmpresa.id_arquivo)); }
               }
            }

            catch (Exception e) { addError("model.getEventosPendentes", e.Message); }
         }

         return lRet;
      }

      public Dictionary<string, List<sEvento>> getRecibosConsulta()
      {

         List<sEvento> lRet = new List<sEvento>();
         eSocialBD eSBD = new eSocialBD();

         eSBD.setModo(enModo.eventosPendentesConsulta);

         try
         {

            var tbEmpresas =
            from e in eSBD.exec().Tables[0].Select()
            select new
            {
               id_empresa = e["id_empresa"].ToString(),
               id_cliente = e["id_cliente"].ToString(),
               nomeEmpresa = e["nomeEmpresa"].ToString(),
               descricaoEvento = e["descricaoEvento"].ToString(),
               status = e["status"].ToString(),
               protocolo = e["protocolo"].ToString(),
               proximaConsulta = e["proximaConsulta"].ToString(),
               certificado = e["certificado"],
               certPass = e["certPass"].ToString(),
               tpAmb=e["tpAmb"].ToString()
            };

            foreach (var empresa in tbEmpresas
                                    .GroupBy(e => e.nomeEmpresa))
            {

               foreach (var evento in empresa.OrderBy(o => o.descricaoEvento))
               {

                  sEvento _evento = new sEvento
                  {
                     id_empresa = evento.id_empresa,
                     nomeEmpresa = evento.nomeEmpresa,
                     id_cliente = evento.id_cliente,
                     nomeEvento = evento.descricaoEvento,
                     certificado = new X509Certificate2((byte[])evento.certificado, evento.certPass),
                     tpAmb=(enTpAmb)int.Parse(evento.tpAmb)
                  };

                  _evento.dadosConsulta.status = evento.status;
                  _evento.dadosConsulta.protocolo = evento.protocolo;
                  _evento.dadosConsulta.proximaConsulta = evento.proximaConsulta;

                  lRet.Add(_evento);
               }
            }
         }
         catch (Exception e)
         {
            addError("model.getRecibosConsulta", e.Message);
         }

         return lRet.GroupBy(p => p.dadosConsulta.protocolo).ToDictionary(k => k.Key, v => v.ToList());
      }

      public void inserirParcial(sLote lote)
      {

         eSocialBD eSBD;

         eSBD = new eSocialBD(enModo.inserirLoteParcial);

         eSBD.setParam(enParams.id_arquivo, lote.id_arquivo);
         eSBD.exec();
      }

      public void gravar_retornoWS(retEnvioLoteEventos retorno) { _gravar_retornoWS(retorno, 0); }
      public void gravar_retornoWS(retProcessamentoLote retorno) { _gravar_retornoWS(retorno, 1); }

      void _gravar_retornoWS(object retorno, int modo)
      {

         string cdResposta;

         retEnvioLoteEventos envioWS = null;
         retProcessamentoLote consultaWS = null;
         eSocialBD eSBDP, eSBD;

         // Retorno do WS
         switch (modo)
         {

            // ###################################################################################################################################################### Envio
            case 0:

               envioWS = (retEnvioLoteEventos)retorno;
               cdResposta = envioWS.retornoEnvioLoteEventos.status.cdResposta.GetHashCode().ToString();

               // Parâmetros base
               eSBDP = new eSocialBD(enModo.envioWS);

               eSBDP.setParam(enParams.id_arquivo, envioWS.lote.id_arquivo);
               eSBDP.setParam(enParams.cdResposta, cdResposta);

               eSBD = new eSocialBD(enModo.envioWS, eSBDP.getParams());
               eSBD.exec();

               // XML de envio do lote (Remove as assinaturas x509 para diminuir o tamanho no BD)
               eSBD = new eSocialBD(enModo.envioWS, eSBDP.getParams());

               envioWS.xmlEnvio.Descendants().Where(e => e.Name.LocalName.Equals("Signature")).Remove();
               eSBD.setParam(enParams.xmlEnvio, envioWS.xmlEnvio.ToString());
               eSBD.exec();

               // Status OK
               if (cdResposta.Equals(enCdResposta.sucesso_201.GetHashCode().ToString()))
               {

                  // Insere o ID dos eventos que foram enviados
                  foreach (var evento in envioWS.lote.lId_enviado)
                  {

                     eSBD = new eSocialBD(enModo.inserirEventosEnviados);

                     eSBD.setParam(enParams.id_empresa, evento.Item1);
                     eSBD.setParam(enParams.id_arquivo, evento.Item2);
                     eSBD.setParam(enParams.evento, evento.Item3);
                     eSBD.setParam(enParams.id_esocial, evento.Item4);
                     eSBD.setParam(enParams.id_evento, evento.Item5);

                     eSBD.exec();
                  }

                  // Status do evento
                  eSBD = new eSocialBD(enModo.envioWS, eSBDP.getParams());
                  eSBD.setParam(enParams.protocolo, envioWS.retornoEnvioLoteEventos.dadosRecepcaoLote.protocoloEnvio);
                  eSBD.setParam(enParams.dhRecepcao, envioWS.retornoEnvioLoteEventos.dadosRecepcaoLote.dhRecepcao);
                  eSBD.exec();
               }

               // Status Erro | !Processamento (203)
               else if (!cdResposta.Equals(enCdResposta.filaProc_203.GetHashCode().ToString()))
               {

                  // XML de retorno do WS
                  eSBD = new eSocialBD(enModo.envioWS, eSBDP.getParams());
                  eSBD.setParam(enParams.xmlRetorno, envioWS.xml.ToString());
                  eSBD.exec();

                  // Ocorrências de erro
                  if (envioWS.retornoEnvioLoteEventos.status.ocorrencias.ocorrencia != null)
                  {
                     foreach (var ocorrencia in envioWS.retornoEnvioLoteEventos.status.ocorrencias.ocorrencia)
                     {

                        eSBD = new eSocialBD(enModo.envioWS, eSBDP.getParams());

                        eSBD.setParam(enParams.modoOcorrencia, "1");

                        eSBD.setParam(enParams.cdResposta, ocorrencia.codigo);
                        eSBD.setParam(enParams.descResposta, ocorrencia.descricao);

                        eSBD.setParam(enParams.ocorrencia_tipo, ocorrencia.tipo.GetHashCode().ToString());
                        eSBD.setParam(enParams.ocorrencia_localizacao, ocorrencia.localizacao);

                        eSBD.exec();
                     }
                  }
               }

               break;

            // ################################################################################################################################################### Consulta
            case 1:

               consultaWS = (retProcessamentoLote)retorno;
               cdResposta = consultaWS.retornoProcessamentoLoteEventos.status.cdResposta.GetHashCode().ToString();
               consultaWS.xml.Descendants().Where(e => e.Name.LocalName.Equals("Signature")).Remove();

               // Parâmetros base
               eSBDP = new eSocialBD(enModo.consultaWS);
               eSBDP.setParam(enParams.cdResposta, cdResposta);
               eSBDP.setParam(enParams.protocolo, consultaWS.protocolo);

               // 101 - Em processamento
               if (cdResposta.Equals(enCdResposta.proc_101.GetHashCode().ToString()))
               {

                  eSBD = new eSocialBD(enModo.consultaWS, eSBDP.getParams());
                  eSBD.setParam(enParams.proxConsulta, consultaWS.retornoProcessamentoLoteEventos.status.tempoEstimadoConclusao);

                  eSBD.exec();

                  break;
               }

               eSBD = new eSocialBD(enModo.consultaWS, eSBDP.getParams());
               eSBD.setParam(enParams.xmlRetorno, consultaWS.xml.ToString());

               // 201 - OK
               if (cdResposta.Equals(enCdResposta.sucesso_201.GetHashCode().ToString()))
               {

                  // Caso exista ao menos um evento com erro no lote
                  if (consultaWS.retornoProcessamentoLoteEventos.retornoEventos.evento != null)
                  {

                     try
                     {
                        cdResposta = consultaWS.retornoProcessamentoLoteEventos.retornoEventos.evento
                                     .Where(e => !e.retornoEvento.retornoEvento.processamento.cdResposta.GetHashCode().Equals(enCdResposta.sucesso_201.GetHashCode()))
                                     .First().retornoEvento.retornoEvento.processamento.cdResposta.GetHashCode().ToString();

                        eSBD.setParam(enParams.cdResposta, cdResposta);
                     }
                     catch { }
                  }

                  eSBD.exec();
               }

               // Status Erro
               else
               {

                  eSBD.exec();

                  // Ocorrências de erro
                  if (consultaWS.retornoProcessamentoLoteEventos.status.ocorrencias.ocorrencia != null)
                  {
                     foreach (var ocorrencia in consultaWS.retornoProcessamentoLoteEventos.status.ocorrencias.ocorrencia)
                     {

                        eSBD = new eSocialBD(enModo.consultaWS, eSBDP.getParams());

                        eSBD.setParam(enParams.modoOcorrencia, "1");

                        eSBD.setParam(enParams.cdResposta, ocorrencia.codigo);
                        eSBD.setParam(enParams.descResposta, ocorrencia.descricao);

                        eSBD.setParam(enParams.ocorrencia_tipo, ocorrencia.tipo.GetHashCode().ToString());
                        eSBD.setParam(enParams.ocorrencia_localizacao, ocorrencia.localizacao);

                        eSBD.exec();
                     }
                  }
               }

               // ######################################################################################################################################### Consulta - Eventos

               if (consultaWS.retornoProcessamentoLoteEventos.retornoEventos.evento != null)
               {

                  foreach (var evento in consultaWS.retornoProcessamentoLoteEventos.retornoEventos.evento)
                  {

                     cdResposta = evento.retornoEvento.retornoEvento.processamento.cdResposta.GetHashCode().ToString();

                     // XML dos eventos totalizadores 1.*
                     if (evento.tot != null)
                     {
                        foreach (var tot in evento.tot)
                        {
                           eSBD = new eSocialBD(enModo.consultaEventoWS, eSBDP.getParams());
                           eSBD.setParam(enParams.id_esocial, evento.Id);
                           eSBD.setParam(enParams.xmlTot, tot.xmlTot);
                           eSBD.setParam(enParams.totTipo, tot.tipo);
                           eSBD.exec();
                        }
                     }

                     // XML contrato 0.1
                     if (evento.retornoEvento.retornoEvento.recibo.xmlContrato != null)
                     {
                        eSBD = new eSocialBD(enModo.consultaEventoWS, eSBDP.getParams());
                        eSBD.setParam(enParams.id_esocial, evento.Id);
                        eSBD.setParam(enParams.xmlContrato, evento.retornoEvento.retornoEvento.recibo.xmlContrato.ToString());
                        eSBD.exec();
                     }

                     // Atualiza o status do evento
                     eSBD = new eSocialBD(enModo.consultaEventoWS, eSBDP.getParams());
                     eSBD.setParam(enParams.id_esocial, evento.Id);
                     eSBD.setParam(enParams.recibo, evento.retornoEvento.retornoEvento.recibo.nrRecibo);
                     eSBD.setParam(enParams.cdResposta, cdResposta);
                     eSBD.setParam(enParams.descResposta, evento.retornoEvento.retornoEvento.processamento.descResposta);
                     eSBD.exec();

                     // Ocorrências de erro
                     if (evento.retornoEvento.retornoEvento.processamento.ocorrencias.ocorrencia != null)
                     {
                        foreach (var ocorrencia in evento.retornoEvento.retornoEvento.processamento.ocorrencias.ocorrencia)
                        {

                           eSBD = new eSocialBD(enModo.consultaEventoWS, eSBDP.getParams());

                           eSBD.setParam(enParams.modoOcorrencia, "1");

                           eSBD.setParam(enParams.id_esocial, evento.Id);
                           eSBD.setParam(enParams.cdResposta, ocorrencia.codigo);
                           eSBD.setParam(enParams.descResposta, ocorrencia.descricao);

                           eSBD.setParam(enParams.ocorrencia_tipo, ocorrencia.tipo.GetHashCode().ToString());
                           eSBD.setParam(enParams.ocorrencia_localizacao, ocorrencia.localizacao);

                           eSBD.exec();
                        }
                     }
                  }

               }

               break;
         }
      }

      private string BuscaConfig(string sCampo)
      {
         string sRetorno = "";
         if (File.Exists(sFileConfig))
         {
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load(sFileConfig);
            XmlNodeList nl = xmlConfig.GetElementsByTagName("CONFIG");

            if (xmlConfig.GetElementsByTagName(sCampo).Item(0) != null)
               sRetorno = xmlConfig.GetElementsByTagName(sCampo).Item(0).InnerText;
         }
         return sRetorno;
      }
   }
}
