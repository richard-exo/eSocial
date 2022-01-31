using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Model.Eventos.XML;

namespace eSocial.Model.Eventos.Retorno {
   public sealed class retEnvioLoteEventos : cBase {

      XElement _xmlEnvio;
      sLote _lote;

      public new enum enCdResposta {
         loteRecebidoSucesso_201 = 201,
         loteRecebidoAdvertencias_202 = 202,
         erroServidor_301 = 301,
         erroPreenchimento_401 = 401,
         schemaInvalido_402 = 402,
         versaoSchema_403 = 403,
         erroCertificado_404 = 404,
         loteNuloVazio_405 = 405
      }

      public enum enTipo {
         erro_1 = 1,
         advertencia_2 = 2
      }

      public XElement xmlEnvio { get { return _xmlEnvio; } }
      public sLote lote { get { return _lote; } }

      public retEnvioLoteEventos(XElement xmlEnvio, XElement xmlRetorno, sLote empresa) {

         xml = xmlRetorno;
         _xmlEnvio = xmlEnvio;
         _lote = empresa;

         #region ############################################################################################################################################### Structs

         _retornoEnvioLoteEventos = new sRetornoEnvioLoteEventos();
         _retornoEnvioLoteEventos.ideEmpregador = new sIdeEmpTrans();
         _retornoEnvioLoteEventos.ideTransmissor = new sIdeEmpTrans();

         _retornoEnvioLoteEventos.status = new sRetornoEnvioLoteEventos.sStatus();
         _retornoEnvioLoteEventos.status.ocorrencias = new sRetornoEnvioLoteEventos.sStatus.sOcorrencias();

         _retornoEnvioLoteEventos.dadosRecepcaoLote = new sRetornoEnvioLoteEventos.sDadosRecepcaoLote();

         #endregion

         // retornoEnvioLoteEventos
         XNamespace ns = "http://www.esocial.gov.br/schema/lote/eventos/envio/retornoEnvio/" + ConfigurationManager.AppSettings["vLayoutRetEnvio"];
         XElement _xml = xml.Elements().ElementAt(0);

         // ideEmpregador 0.1
         if (_xml.Element(ns + "ideEmpregador") != null) {
            _retornoEnvioLoteEventos.ideEmpregador.nrInsc = _xml.Element(ns + "ideEmpregador").Element(ns + "nrInsc").Value;
            _retornoEnvioLoteEventos.ideEmpregador.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), _xml.Element(ns + "ideEmpregador").Element(ns + "tpInsc").Value);
         }

         // ideTransmissor 0.1
         if (_xml.Element(ns + "ideTransmissor") != null) {
            _retornoEnvioLoteEventos.ideTransmissor.nrInsc = _xml.Element(ns + "ideTransmissor").Element(ns + "nrInsc").Value;
            _retornoEnvioLoteEventos.ideTransmissor.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), _xml.Element(ns + "ideTransmissor").Element(ns + "tpInsc").Value);
         }

         // status
         _retornoEnvioLoteEventos.status.cdResposta = (enCdResposta)Enum.Parse(typeof(enCdResposta), _xml.Element(ns + "status").Element(ns + "cdResposta").Value);
         _retornoEnvioLoteEventos.status.descResposta = _xml.Element(ns + "status").Element(ns + "descResposta").Value;

         // ocorrencias 0.1 
         if (_xml.Element(ns + "status").Element(ns + "ocorrencias") != null) {

            _retornoEnvioLoteEventos.status.ocorrencias.ocorrencia = new List<sRetornoEnvioLoteEventos.sStatus.sOcorrencias.sOcorrencia>();

            // ocorrencia 1.*
            foreach (var o in _xml.Element(ns + "status").Element(ns + "ocorrencias").Descendants(ns + "ocorrencia")) {

               sRetornoEnvioLoteEventos.sStatus.sOcorrencias.sOcorrencia ocorrencia = new sRetornoEnvioLoteEventos.sStatus.sOcorrencias.sOcorrencia();

               ocorrencia.codigo = o.Element(ns + "codigo").Value;
               ocorrencia.descricao = o.Element(ns + "descricao").Value;
               ocorrencia.tipo = (enTipo)Enum.Parse(typeof(enTipo), o.Element(ns + "tipo").Value);
               ocorrencia.localizacao = o.Element(ns + "localizacao")?.Value; // 0.1

               _retornoEnvioLoteEventos.status.ocorrencias.ocorrencia.Add(ocorrencia);
            }
         }

         // dadosRecepcaoLote 0.1
         if (_xml.Element(ns + "dadosRecepcaoLote") != null) {
            _retornoEnvioLoteEventos.dadosRecepcaoLote.dhRecepcao = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "dhRecepcao").Value;
            _retornoEnvioLoteEventos.dadosRecepcaoLote.versaoAplicativoRecepcao = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "versaoAplicativoRecepcao").Value;
            _retornoEnvioLoteEventos.dadosRecepcaoLote.protocoloEnvio = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "protocoloEnvio").Value;
         }
      }

      public sRetornoEnvioLoteEventos retornoEnvioLoteEventos { get { return _retornoEnvioLoteEventos; } }

      sRetornoEnvioLoteEventos _retornoEnvioLoteEventos;
      public struct sRetornoEnvioLoteEventos {

         public sIdeEmpTrans ideEmpregador, ideTransmissor;

         public sStatus status;
         public struct sStatus {

            public enCdResposta cdResposta;
            public string descResposta;

            public sOcorrencias ocorrencias;
            public struct sOcorrencias {

               public List<sOcorrencia> ocorrencia;
               public struct sOcorrencia {
                  public string codigo;
                  public enTipo tipo;
                  public string descricao, localizacao;
               }
            }
         }
         public sDadosRecepcaoLote dadosRecepcaoLote;
         public struct sDadosRecepcaoLote {
            public string dhRecepcao;
            public string versaoAplicativoRecepcao, protocoloEnvio;
         }
      }
   }
}