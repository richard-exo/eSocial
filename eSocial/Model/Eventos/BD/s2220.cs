using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
   class s2220 : bEvento_BD
   {

		XML.s2220 s2220XML;

		public s2220() : base("2220", "Monitoramento da Saúde do Trabalhador", enTipoEvento.eventosNaoPeriodicos_2) { }

		public override List<sEvento> getEventosPendentes()
		{

			base.getEventosPendentes();

			try
			{

				List<string> lista2220 = new List<string>();
				List<string> lista2220exame = new List<string>();

				foreach (DataRow row in tbEventos.Rows)
				{
					// Só executa 1x para cada funcionário
					if (!lista2220.Contains(row["id_funcionario"].ToString()))
					{
						// Registra o funcionário
						lista2220.Add(row["id_funcionario"].ToString());

						sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

						s2220XML = new XML.s2220(evento.id);

						// ### Evento

						// ideEvento
						s2220XML.ideEvento.indRetif = row["indRetif"].ToString();
						s2220XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1

						s2220XML.ideEvento.tpAmb = evento.tpAmb;
						s2220XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
						s2220XML.ideEvento.verProc = versao;

						//// ideEmpregador
						s2220XML.ideEmpregador.tpInsc = evento.tpInsc;
						s2220XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

						//// ideVinculo
						s2220XML.ideVinculo.cpfTrab = row["cpfTrab_vinculo"].ToString();
						s2220XML.ideVinculo.matricula = row["matricula_vinculo"].ToString();
						s2220XML.ideVinculo.codCateg = row["codCateg_vinculo"].ToString();

						//// exMedOcup
						s2220XML.exMedOcup.tpExameOcup = row["tpExameOcup"].ToString();

						//// aso   
						s2220XML.exMedOcup.aso.dtAso = validadores.aaaa_mm_dd(row["dtAso"].ToString());
						s2220XML.exMedOcup.aso.resAso = row["resAso"].ToString();

						// exame 1.99
						gcl.setLevel("exame", clear: true);

						var tbExame = from DataRow r in tbEventos.Rows
										  where r["id_funcionario"].ToString().Equals(evento.id_funcionario)
										  select r;

						foreach (var exame in tbExame)
						{
							gcl.setLevel(row: exame);

							string sChaveEpi1 = gcl.getVal("id").ToString();
							// Só executa 1x para exame
							if (!lista2220exame.Contains(sChaveEpi1))
							{
								// Registra o exame
								lista2220exame.Add(sChaveEpi1);

								// epi 0.99
								gcl.setLevel("exame", clear: true);

								var tbRetExame_detExame = from DataRow r in tbExame
															 where !string.IsNullOrEmpty(r[$"dtExm_exame"].ToString()) &&
															 r["id_exame"].ToString().Equals(sChaveEpi1)
															 select r;

								foreach (var retExameFl in tbRetExame_detExame)
								{
									gcl.setLevel(row: retExameFl);

									s2220XML.exMedOcup.aso.exame.dtExm = validadores.aaaa_mm_dd(gcl.getVal("dtExm")); 
									s2220XML.exMedOcup.aso.exame.procRealizado = gcl.getVal("procRealizado");
									s2220XML.exMedOcup.aso.exame.obsProc = gcl.getVal("obsProc");
									s2220XML.exMedOcup.aso.exame.ordExame = gcl.getVal("ordExame");
									s2220XML.exMedOcup.aso.exame.indResult = gcl.getVal("indResult");
									s2220XML.add_exame();
								}

								s2220XML.exMedOcup.aso.medico.nmMed= gcl.getVal("nmMed");
								s2220XML.exMedOcup.aso.medico.nrCRM= gcl.getVal("nrCRM");
								s2220XML.exMedOcup.aso.medico.ufCRM= gcl.getVal("ufCRM");
							}
							gcl.setLevel("exame", clear: true);
						}

						//// respMonit
						s2220XML.exMedOcup.respMonit.cpfResp = row["cpfResp"].ToString();
						s2220XML.exMedOcup.respMonit.nmResp = row["nmResp"].ToString();
						s2220XML.exMedOcup.respMonit.nrCRM = row["nrCRM"].ToString();
						s2220XML.exMedOcup.respMonit.ufCRM = row["ufCRM"].ToString();

						evento.eventoAssinadoXML = s2220XML.genSignedXML(evento.certificado);
						lEventos.Add(evento);
					}
				}
			}
			catch (Exception e) { addError("model.eventos.BD.s2220", e.Message); }
			return lEventos;
		}

	}
}
