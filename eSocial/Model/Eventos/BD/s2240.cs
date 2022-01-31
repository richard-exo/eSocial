using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   class s2240 : bEvento_BD
	{
		XML.s2240 s2240XML;

		public s2240() : base("2240", "Condições Ambientais do Trabalho - Agentes Nocivos", enTipoEvento.eventosNaoPeriodicos_2) { }

		public override List<sEvento> getEventosPendentes()
		{

			base.getEventosPendentes();

			try
			{

				List<string> lista2240 = new List<string>();
				List<string> lista2240agNoc = new List<string>();
				List<string> lista2240epi = new List<string>();
				List<string> lista2240respReg = new List<string>();

				foreach (DataRow row in tbEventos.Rows)
				{
					// Só executa 1x para cada funcionário
					if (!lista2240.Contains(row["id_funcionario"].ToString()))
					{
						// Registra o funcionário
						lista2240.Add(row["id_funcionario"].ToString());

						sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

						s2240XML = new XML.s2240(evento.id);

						// ### Evento

						// ideEvento
						s2240XML.ideEvento.indRetif = row["indRetif"].ToString();
						s2240XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1

						s2240XML.ideEvento.tpAmb = evento.tpAmb; 
						s2240XML.ideEvento.procEmi = enProcEmi.appEmpregador_1; 
						s2240XML.ideEvento.verProc = versao;

						//// ideEmpregador
						s2240XML.ideEmpregador.tpInsc = evento.tpInsc;
						s2240XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

						//// ideVinculo
						s2240XML.ideVinculo.cpfTrab = row["cpfTrab_vinculo"].ToString();
						s2240XML.ideVinculo.matricula = row["matricula_vinculo"].ToString();
						s2240XML.ideVinculo.codCateg = row["codCateg_vinculo"].ToString();

						//// infoExpRisco
						s2240XML.infoExpRisco.dtIniCondicao = validadores.aaaa_mm_dd(row["dtIniCondicao"].ToString());
						
						//// infoAmb   
						s2240XML.infoExpRisco.infoAmb.localAmb = row["localAmb_infoAmb"].ToString();
						s2240XML.infoExpRisco.infoAmb.dscSetor = row["dscSetor_infoAmb"].ToString();
						s2240XML.infoExpRisco.infoAmb.tpInsc = row["tpInsc_infoAmb"].ToString();
						s2240XML.infoExpRisco.infoAmb.nrInsc = row["nrInsc_infoAmb"].ToString();

						//// infoAtiv
						s2240XML.infoExpRisco.infoAtiv.dscAtivDes = row["dscAtivDes_infoAtiv"].ToString();

						// agNoc 1.9999
						gcl.setLevel("agNoc", clear: true);

						var tbAgNoc = from DataRow r in tbEventos.Rows
										  where r["id_funcionario"].ToString().Equals(evento.id_funcionario)
										  select r;

						foreach (var agNoc in tbAgNoc)
						{
							gcl.setLevel(row: agNoc);

							string sChaveEpi1 = gcl.getVal("id").ToString();
							// Só executa 1x para cada dependente
							if (!lista2240agNoc.Contains(sChaveEpi1))
							{
								// Registra o AgNoc
								lista2240agNoc.Add(sChaveEpi1);

								s2240XML.infoExpRisco.agNoc.codAgNoc = gcl.getVal("codigo");
								s2240XML.infoExpRisco.agNoc.dscAgNoc = gcl.getVal("dsc");
								s2240XML.infoExpRisco.agNoc.tpAval = gcl.getVal("tpAval");
								s2240XML.infoExpRisco.agNoc.intConc = gcl.getVal("intConc").Replace(",", ".");
								s2240XML.infoExpRisco.agNoc.limTol = gcl.getVal("limTol").Replace(",", ".");
								s2240XML.infoExpRisco.agNoc.unMed = gcl.getVal("unMed");
								s2240XML.infoExpRisco.agNoc.tecMedicao = gcl.getVal("tecMedicao");

								// Registra epcEPI
								s2240XML.infoExpRisco.agNoc.epcEpi.utilizEpc = gcl.getVal("utilizEPC");
								s2240XML.infoExpRisco.agNoc.epcEpi.eficEpc = gcl.getVal("eficEpc");
								s2240XML.infoExpRisco.agNoc.epcEpi.utilizEPI = gcl.getVal("utilizEPI");
								s2240XML.infoExpRisco.agNoc.epcEpi.eficEpi = gcl.getVal("eficEpi");

								// epi 0.99
								gcl.setLevel("retEpi_detEpi", clear: true);

								var tbRetEpi_detEpi = from DataRow r in tbAgNoc
														 where !string.IsNullOrEmpty(r[$"docAval_retEpi_detEpi"].ToString()) &&
														 r["id_retEpi_detEpi"].ToString().Equals(sChaveEpi1)
														 select r;

								foreach (var retEpiFl in tbRetEpi_detEpi)
								{
									gcl.setLevel(row: retEpiFl);

									string sChaveEpi2 = gcl.getVal("item").ToString();
									if (!lista2240epi.Contains(sChaveEpi2))
									{
										lista2240epi.Add(sChaveEpi2);

										s2240XML.infoExpRisco.agNoc.epcEpi.epi.docAval = gcl.getVal("docAval");
										s2240XML.infoExpRisco.agNoc.epcEpi.epi.dscEPI = gcl.getVal("dscEPI");

										s2240XML.add_epi();
									}
								}
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.medProtecao = gcl.getVal("medProtecao");
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.condFuncto = gcl.getVal("condFuncto");
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.usoInint = gcl.getVal("usoInint");
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.przValid = gcl.getVal("przValid");
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.periodicTroca = gcl.getVal("periodicTroca");
								s2240XML.infoExpRisco.agNoc.epcEpi.epiCompl.higienizacao = gcl.getVal("higienizacao");

								s2240XML.add_agNoc();
							}
							gcl.setLevel("agNoc", clear: true);
						}

						////////
						// agNoc 1.9999
						gcl.setLevel("respReg", clear: true);

						var tbRespReg = from DataRow r in tbEventos.Rows
										  where r["id_funcionario"].ToString().Equals(evento.id_funcionario)
										  select r;

						foreach (var respReg in tbRespReg)
						{
							gcl.setLevel(row: respReg);

							string sChaveEpi3 = gcl.getVal("item").ToString();
							// Só executa 1x para cada dependente
							if (!lista2240respReg.Contains(sChaveEpi3))
							{
								// Registra o AgNoc
								lista2240respReg.Add(sChaveEpi3);

								s2240XML.infoExpRisco.respReg.cpfResp = gcl.getVal("cpfResp");
								s2240XML.infoExpRisco.respReg.ideOC = gcl.getVal("ideOC");
								s2240XML.infoExpRisco.respReg.dscOC = gcl.getVal("dscOC");
								s2240XML.infoExpRisco.respReg.nrOC = gcl.getVal("nrOC");
								s2240XML.infoExpRisco.respReg.ufOC = gcl.getVal("ufOC");
								
								s2240XML.add_respReg();
							}
							gcl.setLevel("respReg", clear: true);
						}

						//// obs
						s2240XML.infoExpRisco.obs.obsCompl= row["obsCompl"].ToString();

						evento.eventoAssinadoXML = s2240XML.genSignedXML(evento.certificado);
						lEventos.Add(evento);
					}
				}
			}
			catch (Exception e) { addError("model.eventos.BD.s2240", e.Message); }
			return lEventos;
		}
	}
}
