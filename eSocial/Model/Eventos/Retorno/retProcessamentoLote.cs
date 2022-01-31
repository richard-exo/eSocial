using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eSocial.Model.Eventos.Retorno {
    public sealed class retProcessamentoLote : cBase {

        string _protocolo;
        public string protocolo { get { return _protocolo; } }

        public new enum enCdResposta {
            aguardandoProcessamento_101 = 101,
            processadoComSucesso_201 = 201,
            processadoComAdvertencias_202 = 202,
            erroServidorSocial_301 = 301,
            erroPreenchimento_401 = 401,
            schemaInválido_402 = 402,
            versaoSchemaNaoPermitida_403 = 403,
            erroCertificado_404 = 404,
            loteNuloInválido_405 = 405,
            consultaIncorreta_erroPreenchimento_501 = 501,
            consultaIncorreta_schemaInvalido_502 = 502,
            consultaIncorreta_versaoSchemaNaoPermitida_503 = 503,
            consultaIncorreta_erroCertificado_504 = 504,
            consultaIncorreta_consultaNulaOuVazia_505 = 505
        }

        public enum enTipo {
            erro_1 = 1,
            advertencia_2 = 2
        }

        public retProcessamentoLote(XElement retXML, string protocolo) {

            xml = retXML;
            _protocolo = protocolo;

            #region ############################################################################################################################################### Structs

            _retornoProcessamentoLoteEventos = new sRetornoProcessamentoLoteEventos();
            _retornoProcessamentoLoteEventos.ideEmpregador = new sIdeEmpTrans();
            _retornoProcessamentoLoteEventos.ideTransmissor = new sIdeEmpTrans();

            _retornoProcessamentoLoteEventos.status = new sRetornoProcessamentoLoteEventos.sStatus();
            _retornoProcessamentoLoteEventos.status.ocorrencias = new sRetornoProcessamentoLoteEventos.sStatus.sOcorrencias();

            _retornoProcessamentoLoteEventos.dadosRecepcaoLote = new sRetornoProcessamentoLoteEventos.sDadosRecepcaoLote();
            _retornoProcessamentoLoteEventos.dadosProcessamentoLote = new sRetornoProcessamentoLoteEventos.sDadosProcessamentoLote();

            _retornoProcessamentoLoteEventos.retornoEventos = new sRetornoProcessamentoLoteEventos.sRetornoEventos();

            #endregion

            // retornoProcessamentoLoteEventos
            XNamespace ns = "http://www.esocial.gov.br/schema/lote/eventos/envio/retornoProcessamento/" + ConfigurationManager.AppSettings["vLayoutRetProc"];
            XElement _xml = xml.Elements().ElementAt(0);

            // ideEmpregador 0.1
            if (_xml.Element(ns + "ideEmpregador") != null) {
                _retornoProcessamentoLoteEventos.ideEmpregador.nrInsc = _xml.Element(ns + "ideEmpregador").Element(ns + "nrInsc").Value;
                _retornoProcessamentoLoteEventos.ideEmpregador.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), _xml.Element(ns + "ideEmpregador").Element(ns + "tpInsc").Value);
            }

            // ideTransmissor 0.1
            if (_xml.Element(ns + "ideTransmissor") != null) {
                _retornoProcessamentoLoteEventos.ideTransmissor.nrInsc = _xml.Element(ns + "ideTransmissor").Element(ns + "nrInsc").Value;
                _retornoProcessamentoLoteEventos.ideTransmissor.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), _xml.Element(ns + "ideTransmissor").Element(ns + "tpInsc").Value);
            }

            // status
            _retornoProcessamentoLoteEventos.status.cdResposta = (enCdResposta)Enum.Parse(typeof(enCdResposta), _xml.Element(ns + "status").Element(ns + "cdResposta").Value);
            _retornoProcessamentoLoteEventos.status.descResposta = _xml.Element(ns + "status").Element(ns + "descResposta").Value;
            _retornoProcessamentoLoteEventos.status.tempoEstimadoConclusao = _xml.Element(ns + "status").Element(ns + "tempoEstimadoConclusao")?.Value; // 0.1

            // ocorrencias 0.1 
            if (_xml.Element(ns + "status").Element(ns + "ocorrencias") != null) {

                _retornoProcessamentoLoteEventos.status.ocorrencias.ocorrencia = new List<sRetornoProcessamentoLoteEventos.sStatus.sOcorrencias.sOcorrencia>();

                // > ocorrencia 1.*
                foreach (var o in _xml.Element(ns + "status").Element(ns + "ocorrencias").Descendants(ns + "ocorrencia")) {

                    sRetornoProcessamentoLoteEventos.sStatus.sOcorrencias.sOcorrencia ocorrencia = new sRetornoProcessamentoLoteEventos.sStatus.sOcorrencias.sOcorrencia();

                    ocorrencia.codigo = o.Element(ns + "codigo").Value;
                    ocorrencia.descricao = o.Element(ns + "descricao").Value;
                    ocorrencia.tipo = (enTipo)Enum.Parse(typeof(enTipo), o.Element(ns + "tipo").Value);
                    ocorrencia.localizacao = o.Element(ns + "localizacao")?.Value; // 0.1

                    _retornoProcessamentoLoteEventos.status.ocorrencias.ocorrencia.Add(ocorrencia);
                }
            }

            // dadosRecepcaoLote 0.1
            if (_xml.Element(ns + "dadosRecepcaoLote") != null) {
                _retornoProcessamentoLoteEventos.dadosRecepcaoLote.dhRecepcao = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "dhRecepcao").Value;
                _retornoProcessamentoLoteEventos.dadosRecepcaoLote.versaoAplicativoRecepcao = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "versaoAplicativoRecepcao").Value;
                _retornoProcessamentoLoteEventos.dadosRecepcaoLote.protocoloEnvio = _xml.Element(ns + "dadosRecepcaoLote").Element(ns + "protocoloEnvio").Value;
            }

            // dadosProcessamentoLote 0.1
            if (_xml.Element(ns + "dadosProcessamentoLote") != null) {
                _retornoProcessamentoLoteEventos.dadosProcessamentoLote.versaoAplicativoProcessamentoLote = _xml.Element(ns + "dadosProcessamentoLote").Element(ns + "versaoAplicativoProcessamentoLote").Value;
            }

            // retornoEventos 0.1
            if (_xml.Element(ns + "retornoEventos") != null) {

                _retornoProcessamentoLoteEventos.retornoEventos.evento = new List<sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento>();

                // > evento 1.50
                foreach (var o in _xml.Element(ns + "retornoEventos").Descendants(ns + "evento")) {

                    sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento evento = new sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento();

                    evento.Id = o.Attribute("Id").Value;
                    evento.evtDupl = o.Attribute("evtDupl")?.Value;

                    evento.retornoEvento = new retEvento(o.Element(ns + "retornoEvento").Elements().ElementAt(0));

                    // tot 0.*
                    if (o.Element(ns + "tot") != null) {

                        evento.tot = new List<sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento.sTot>();

                        foreach (var t in o.Descendants(ns + "tot")) {

                            sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento.sTot tot = new sRetornoProcessamentoLoteEventos.sRetornoEventos.sEvento.sTot();

                            tot.tipo = t.Attribute("tipo").Value;
                            foreach (var ta in o.Element(ns + "tot").Elements().Where(x => !x.Name.Equals("tipo"))) { tot.xmlTot = ta.ToString(); }

                            evento.tot.Add(tot);
                        }
                    }
                    _retornoProcessamentoLoteEventos.retornoEventos.evento.Add(evento);
                }
            }
        }

        public sRetornoProcessamentoLoteEventos retornoProcessamentoLoteEventos { get { return _retornoProcessamentoLoteEventos; } }

        sRetornoProcessamentoLoteEventos _retornoProcessamentoLoteEventos;
        public struct sRetornoProcessamentoLoteEventos {

            public sIdeEmpTrans ideEmpregador, ideTransmissor;

            public sStatus status;
            public struct sStatus {

                public enCdResposta cdResposta;
                public string descResposta;
                public string tempoEstimadoConclusao;

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

            public sDadosProcessamentoLote dadosProcessamentoLote;
            public struct sDadosProcessamentoLote {
                public string versaoAplicativoProcessamentoLote;
            }
            public sRetornoEventos retornoEventos;
            public struct sRetornoEventos {

                public List<sEvento> evento;

                public struct sEvento {
                    public string Id, evtDupl;

                    public retEvento retornoEvento;

                    public List<sTot> tot;
                    public struct sTot {
                        public string tipo;
                        public string xmlTot;
                    }
                }
            }
        }
    }
}