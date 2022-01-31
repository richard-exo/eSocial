using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Model.Eventos.XML;

namespace eSocial.Model.Eventos.Retorno {
    public sealed class retEvento : cBase {

        public enum enTipo {
            erro_1 = 1,
            advertencia_2 = 2
        }

        public new enum enCdResposta {
            loteRecebidoSucesso_201 = 201,
            loteRecebidoAdvertencias_202 = 202,
            erroServidor_301 = 301,
            erroConteudo_401 = 401,
            schemaInvalido_402 = 402,
            layoutInvalido_403 = 403,
            erroCertificado_404 = 404,
            erroAssinatura_405 = 405,
            eventoNaoPertenceAoGrupoDoLote_406 = 406
        }

        public retEvento(XElement retXML) {

            xml = retXML;

            #region ############################################################################################################################################### Structs

            _retornoEvento = new sRetornoEvento();
            _retornoEvento.ideEmpregador = new sIdeEmpTrans();

            _retornoEvento.recepcao = new sRetornoEvento.sRecepcao();

            _retornoEvento.processamento = new sRetornoEvento.sProcessamento();
            _retornoEvento.processamento.ocorrencias = new sRetornoEvento.sProcessamento.sOcorrencias();

            _retornoEvento.recibo = new sRetornoEvento.sRecibo();
            _retornoEvento.recibo.contrato = new sRetornoEvento.sRecibo.sContrato();
            _retornoEvento.recibo.contrato.trabalhador = new sRetornoEvento.sRecibo.sContrato.sTrabalhador();
            _retornoEvento.recibo.contrato.infoDeficiencia = new sRetornoEvento.sRecibo.sContrato.sInfoDeficiencia();
            _retornoEvento.recibo.contrato.vinculo = new sRetornoEvento.sRecibo.sContrato.sVinculo();
            _retornoEvento.recibo.contrato.infoCeletista = new sRetornoEvento.sRecibo.sContrato.sInfoCeletista();
            _retornoEvento.recibo.contrato.infoEstatutario = new sRetornoEvento.sRecibo.sContrato.sInfoEstatutario();

            _retornoEvento.recibo.contrato.infoContrato = new sRetornoEvento.sRecibo.sContrato.sInfoContrato();
            _retornoEvento.recibo.contrato.infoContrato.cargo = new sRetornoEvento.sRecibo.sContrato.sInfoContrato.sCargo();
            _retornoEvento.recibo.contrato.infoContrato.funcao = new sRetornoEvento.sRecibo.sContrato.sInfoContrato.sFuncao();
            _retornoEvento.recibo.contrato.remuneracao = new sRetornoEvento.sRecibo.sContrato.sRemuneracao();
            _retornoEvento.recibo.contrato.duracao = new sRetornoEvento.sRecibo.sContrato.sDuracao();
            _retornoEvento.recibo.contrato.localTrabGeral = new sRetornoEvento.sRecibo.sContrato.sLocalTrabGeral();

            _retornoEvento.recibo.contrato.horContratual = new sRetornoEvento.sRecibo.sContrato.sHorContratual();

            #endregion

            // retornoEvento            
            XElement _xml = xml.Elements().ElementAt(0);
            XNamespace ns = xml.Attribute("xmlns").Value;
            //XNamespace ns = "http://www.esocial.gov.br/schema/evt/retornoEvento/" + ConfigurationManager.AppSettings["vLayoutRetEvento"];

            // Id
            _retornoEvento.Id = _xml.Attribute("Id").Value;
           
            // ideEmpregador
            _retornoEvento.ideEmpregador.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), _xml.Element(ns + "ideEmpregador").Element(ns + "tpInsc").Value);
            _retornoEvento.ideEmpregador.nrInsc = _xml.Element(ns + "ideEmpregador").Element(ns + "nrInsc").Value;

            // recepcao
            _retornoEvento.recepcao.tpAmb = (enTpAmb)Enum.Parse(typeof(enTpAmb), _xml.Element(ns + "recepcao").Element(ns + "tpAmb").Value);
            _retornoEvento.recepcao.dhRecepcao = _xml.Element(ns + "recepcao").Element(ns + "dhRecepcao").Value;
            _retornoEvento.recepcao.versaoAppRecepcao = _xml.Element(ns + "recepcao").Element(ns + "versaoAppRecepcao").Value;
            _retornoEvento.recepcao.protocoloEnvioLote = _xml.Element(ns + "recepcao").Element(ns + "protocoloEnvioLote")?.Value; // 0.1

            // processamento
            _retornoEvento.processamento.cdResposta = (enCdResposta)Enum.Parse(typeof(enCdResposta), _xml.Element(ns + "processamento").Element(ns + "cdResposta").Value);
            _retornoEvento.processamento.descResposta = _xml.Element(ns + "processamento").Element(ns + "descResposta").Value;
            _retornoEvento.processamento.versaoAppProcessamento = _xml.Element(ns + "processamento").Element(ns + "versaoAppProcessamento").Value;
            _retornoEvento.processamento.dhProcessamento = _xml.Element(ns + "processamento").Element(ns + "dhProcessamento").Value;

            // ocorrencias 0.1 
            if (_xml.Element(ns + "processamento").Element(ns + "ocorrencias") != null) {

                _retornoEvento.processamento.ocorrencias.ocorrencia = new List<sRetornoEvento.sProcessamento.sOcorrencias.sOcorrencia>();

                // > ocorrencia 1.*
                foreach (var o in _xml.Element(ns + "processamento").Element(ns + "ocorrencias").Descendants(ns + "ocorrencia")) {

                    sRetornoEvento.sProcessamento.sOcorrencias.sOcorrencia ocorrencia = new sRetornoEvento.sProcessamento.sOcorrencias.sOcorrencia();

                    ocorrencia.tipo = (enTipo)Enum.Parse(typeof(enTipo), o.Element(ns + "tipo").Value);
                    ocorrencia.codigo = o.Element(ns + "codigo").Value;
                    ocorrencia.descricao = o.Element(ns + "descricao").Value;
                    ocorrencia.localizacao = o.Element(ns + "localizacao")?.Value; // 0.1

                    _retornoEvento.processamento.ocorrencias.ocorrencia.Add(ocorrencia);
                }
            }

            // recibo 0.1
            if (_xml.Element(ns + "recibo") != null) {

                _retornoEvento.recibo.nrRecibo = _xml.Element(ns + "recibo").Element(ns + "nrRecibo").Value;
                _retornoEvento.recibo.hash = _xml.Element(ns + "recibo").Element(ns + "hash").Value;

                // contrato 0.1
                if (_xml.Element(ns + "contrato") != null) {

                    _retornoEvento.recibo.xmlContrato = _xml.Element(ns + "recibo").Element(ns + "contrato");
                    XElement xmlContrato = _retornoEvento.recibo.xmlContrato;

                    // ideEmpregador
                    _retornoEvento.recibo.contrato.ideEmpregador.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), xmlContrato.Element(ns + "ideEmpregador").Element(ns + "tpInsc").Value);
                    _retornoEvento.recibo.contrato.ideEmpregador.nrInsc = xmlContrato.Element(ns + "ideEmpregador").Element(ns + "nrInsc").Value;

                    // trabalhador
                    _retornoEvento.recibo.contrato.trabalhador.cpfTrab = xmlContrato.Element(ns + "trabalhador").Element(ns + "cpfTrab").Value;
                    _retornoEvento.recibo.contrato.trabalhador.nisTrab = xmlContrato.Element(ns + "trabalhador").Element(ns + "nisTrab").Value;
                    _retornoEvento.recibo.contrato.trabalhador.nmTrab = xmlContrato.Element(ns + "trabalhador").Element(ns + "nmTrab").Value;

                    // infoDeficiencia 0.1
                    if (_xml.Element(ns + "contrato") != null) {
                        _retornoEvento.recibo.contrato.infoDeficiencia.infoCota = xmlContrato.Element(ns + "infoDeficiencia").Element(ns + "infoCota").Value;
                    }

                    // vinculo 0.1
                    if (_xml.Element(ns + "vinculo") != null) {
                        _retornoEvento.recibo.contrato.vinculo.matricula = xmlContrato.Element(ns + "vinculo").Element(ns + "matricula").Value;
                    }

                    // infoCeletista 0.1
                    if (xmlContrato.Element(ns + "infoCeletista") != null) {
                        _retornoEvento.recibo.contrato.infoCeletista.dtAdm = xmlContrato.Element(ns + "infoCeletista").Element(ns + "dtAdm").Value;
                        _retornoEvento.recibo.contrato.infoCeletista.tpRegJor = xmlContrato.Element(ns + "infoCeletista").Element(ns + "tpRegJor").Value;
                        _retornoEvento.recibo.contrato.infoCeletista.dtBase = xmlContrato.Element(ns + "infoCeletista").Element(ns + "dtBase")?.Value; // 0.1
                        _retornoEvento.recibo.contrato.infoCeletista.cnpjSindCategProf = xmlContrato.Element(ns + "infoCeletista").Element(ns + "cnpjSindCategProf").Value;
                    }

                    // infoEstatutario 0.1
                    if (xmlContrato.Element(ns + "infoEstatutario") != null) {
                        _retornoEvento.recibo.contrato.infoEstatutario.dtPosse = xmlContrato.Element(ns + "infoEstatutario").Element(ns + "dtPosse").Value;
                        _retornoEvento.recibo.contrato.infoEstatutario.dtExercicio = xmlContrato.Element(ns + "infoEstatutario").Element(ns + "dtExercicio").Value;
                    }

                    // infoContrato
                    // > cargo 0.1
                    if (xmlContrato.Element(ns + "infoContrato").Element(ns + "cargo") != null) {
                        _retornoEvento.recibo.contrato.infoContrato.cargo.codCargo = xmlContrato.Element(ns + "infoContrato").Element(ns + "cargo").Element(ns + "codCargo").Value;
                        _retornoEvento.recibo.contrato.infoContrato.cargo.nmCargo = xmlContrato.Element(ns + "infoContrato").Element(ns + "cargo").Element(ns + "nmCargo").Value;
                        _retornoEvento.recibo.contrato.infoContrato.cargo.codCBO = xmlContrato.Element(ns + "infoContrato").Element(ns + "cargo").Element(ns + "codCBO").Value;
                    }

                    // > funcao 0.1
                    if (xmlContrato.Element(ns + "infoContrato").Element(ns + "funcao") != null) {
                        _retornoEvento.recibo.contrato.infoContrato.funcao.codFuncao = xmlContrato.Element(ns + "infoContrato").Element(ns + "funcao").Element(ns + "codFuncao").Value;
                        _retornoEvento.recibo.contrato.infoContrato.funcao.dscFuncao = xmlContrato.Element(ns + "infoContrato").Element(ns + "funcao").Element(ns + "dscFuncao").Value;
                        _retornoEvento.recibo.contrato.infoContrato.funcao.codCBO = xmlContrato.Element(ns + "infoContrato").Element(ns + "funcao").Element(ns + "codCBO").Value;
                    }

                    // > codCateg
                    _retornoEvento.recibo.contrato.infoContrato.codCateg = xmlContrato.Element(ns + "infoContrato").Element(ns + "codCateg").Value;

                    // remuneracao
                    _retornoEvento.recibo.contrato.remuneracao.vrSalFx = xmlContrato.Element(ns + "remuneracao").Element(ns + "vrSalFx").Value;
                    _retornoEvento.recibo.contrato.remuneracao.undSalFixo = xmlContrato.Element(ns + "remuneracao").Element(ns + "undSalFixo").Value;
                    _retornoEvento.recibo.contrato.remuneracao.dscSalVar = xmlContrato.Element(ns + "remuneracao").Element(ns + "dscSalVar")?.Value; // 0.1

                    // duracao
                    _retornoEvento.recibo.contrato.duracao.tpContr = xmlContrato.Element(ns + "duracao").Element(ns + "tpContr").Value;
                    _retornoEvento.recibo.contrato.duracao.dtTerm = xmlContrato.Element(ns + "duracao").Element(ns + "dtTerm")?.Value; // 0.1
                    _retornoEvento.recibo.contrato.duracao.clauAsseg = xmlContrato.Element(ns + "duracao").Element(ns + "clauAsseg")?.Value; // 0.1

                    // localTrabGeral 0.1
                    if (xmlContrato.Element(ns + "localTrabGeral") != null) {
                        _retornoEvento.recibo.contrato.localTrabGeral.tpInsc = xmlContrato.Element(ns + "localTrabGeral").Element(ns + "tpInsc").Value;
                        _retornoEvento.recibo.contrato.localTrabGeral.nrInsc = xmlContrato.Element(ns + "localTrabGeral").Element(ns + "nrInsc").Value;
                        _retornoEvento.recibo.contrato.localTrabGeral.cnae = xmlContrato.Element(ns + "localTrabGeral").Element(ns + "cnae").Value;
                    }

                    // horContratual 0.1
                    if (xmlContrato.Element(ns + "horContratual") != null) {
                        _retornoEvento.recibo.contrato.horContratual.qtdHrsSem = xmlContrato.Element(ns + "horContratual").Element(ns + "qtdHrsSem").Value;
                        _retornoEvento.recibo.contrato.horContratual.tpJornada = xmlContrato.Element(ns + "horContratual").Element(ns + "tpJornada").Value;
                        _retornoEvento.recibo.contrato.horContratual.dscTpJorn = xmlContrato.Element(ns + "horContratual").Element(ns + "dscTpJorn").Value;
                        _retornoEvento.recibo.contrato.horContratual.tmpParc = xmlContrato.Element(ns + "horContratual").Element(ns + "tmpParc").Value;
                    }

                    // > horario 0.99
                    if (xmlContrato.Element(ns + "horContratual").Element(ns + "horario") != null) {

                        _retornoEvento.recibo.contrato.horContratual.horario = new List<sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario>();

                        foreach (var h in xmlContrato.Element(ns + "horContratual").Descendants(ns + "horario")) {

                            sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario horario = new sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario();
                            horario.horarioIntervalo = new List<sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario.sHorarioIntervalo>();

                            horario.dia = h.Element(ns + "dia").Value;
                            horario.codHorContrat = h.Element(ns + "codHorContrat").Value;
                            horario.hrEntr = h.Element(ns + "hrEntr").Value;
                            horario.hrSaida = h.Element(ns + "hrSaida").Value;
                            horario.durJornada = h.Element(ns + "durJornada").Value;
                            horario.perHorFlexivel = h.Element(ns + "perHorFlexivel").Value;

                            // >> horarioIntervalo 0.99
                            foreach (var hi in h.Descendants(ns + "horarioIntervalo")) {

                                sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario.sHorarioIntervalo horarioIntervalo = new sRetornoEvento.sRecibo.sContrato.sHorContratual.sHorario.sHorarioIntervalo();

                                horarioIntervalo.tpInterv = h.Element(ns + "tpInterv").Value;
                                horarioIntervalo.durInterv = h.Element(ns + "durInterv").Value;
                                horarioIntervalo.iniInterv = h.Element(ns + "iniInterv")?.Value; // 0.1
                                horarioIntervalo.termInterv = h.Element(ns + "termInterv")?.Value; // 0.1

                                horario.horarioIntervalo.Add(horarioIntervalo);
                            }

                            _retornoEvento.recibo.contrato.horContratual.horario.Add(horario);
                        }
                    }
                }
            }
        }

        public sRetornoEvento retornoEvento { get { return _retornoEvento; } }

        sRetornoEvento _retornoEvento;

        public struct sRetornoEvento {

            public string Id;

            public sIdeEmpTrans ideEmpregador;

            public sRecepcao recepcao;
            public struct sRecepcao {
                public enTpAmb tpAmb;
                public string dhRecepcao;
                public string versaoAppRecepcao, protocoloEnvioLote;
            }

            public sProcessamento processamento;
            public struct sProcessamento {
                public enCdResposta cdResposta;
                public string descResposta;
                public string versaoAppProcessamento;
                public string dhProcessamento;

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

            public sRecibo recibo;
            public struct sRecibo {
                public string nrRecibo, hash;

                public XElement xmlContrato;
                public sContrato contrato;
                public struct sContrato {

                    public sIdeEmpTrans ideEmpregador;

                    public sTrabalhador trabalhador;
                    public struct sTrabalhador {
                        public string cpfTrab, nisTrab;
                        public string nmTrab;
                    }

                    public sInfoDeficiencia infoDeficiencia;
                    public struct sInfoDeficiencia { public string infoCota; }

                    public sVinculo vinculo;
                    public struct sVinculo { public string matricula; }

                    public sInfoCeletista infoCeletista;
                    public struct sInfoCeletista {
                        public string dtBase;
                        public string dtAdm;
                        public string tpRegJor, cnpjSindCategProf;
                    }

                    public sInfoEstatutario infoEstatutario;
                    public struct sInfoEstatutario { public string dtPosse, dtExercicio; }

                    public sInfoContrato infoContrato;
                    public struct sInfoContrato {

                        public sCargo cargo;
                        public struct sCargo {
                            public string codCargo, nmCargo;
                            public string codCBO;
                        }

                        public sFuncao funcao;
                        public struct sFuncao {
                            public string codFuncao, dscFuncao;
                            public string codCBO;
                        }

                        public string codCateg;
                    }

                    public sRemuneracao remuneracao;
                    public struct sRemuneracao {
                        public string vrSalFx;
                        public string undSalFixo;
                        public string dscSalVar;
                    }

                    public sDuracao duracao;
                    public struct sDuracao {
                        public string tpContr;
                        public string dtTerm;
                        public string clauAsseg;
                    }

                    public sLocalTrabGeral localTrabGeral;
                    public struct sLocalTrabGeral { public string tpInsc, nrInsc, cnae; }

                    public sHorContratual horContratual;
                    public struct sHorContratual {
                        public string qtdHrsSem;
                        public string tpJornada;
                        public string dscTpJorn, tmpParc;

                        public List<sHorario> horario;
                        public struct sHorario {
                            public string dia, durJornada;
                            public string codHorContrat, hrEntr, hrSaida, perHorFlexivel;

                            public List<sHorarioIntervalo> horarioIntervalo;
                            public struct sHorarioIntervalo {
                                public string tpInterv, durInterv;
                                public string iniInterv, termInterv;
                            }
                        }
                    }
                }
            }
        }
    }
}
