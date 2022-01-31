using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD{
   public class s2205 : bEvento_BD
   {
      XML.s2205 s2205XML;

      public s2205() : base("2205", "Alteração de Dados Cadastrais do Trabalhador", enTipoEvento.eventosNaoPeriodicos_2) { }

      public override List<sEvento> getEventosPendentes(){

         base.getEventosPendentes();

         try {

				List<string> lista2205 = new List<string>();

				foreach (DataRow row in tbEventos.Rows)
				{
					// Só executa 1x para cada funcionário
					if (!lista2205.Contains(row["id_funcionario"].ToString()))
					{
						// Registra o funcionário
						lista2205.Add(row["id_funcionario"].ToString());

						sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

						s2205XML = new XML.s2205(evento.id);

						// ### Evento

						// ideEvento
						s2205XML.ideEvento.indRetif = row["indRetif"].ToString();
						s2205XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1

						s2205XML.ideEvento.tpAmb = evento.tpAmb; //.GetHashCode().ToString();
						s2205XML.ideEvento.procEmi = enProcEmi.appEmpregador_1; //.GetHashCode().ToString();
						s2205XML.ideEvento.verProc = versao;

						//// ideEmpregador
						s2205XML.ideEmpregador.tpInsc = evento.tpInsc;
						s2205XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

						//// ideTrabalhador
						s2205XML.ideTrabalhador.cpfTrab = row["cpfTrab_trabalhador"].ToString();

						//// alteracao
						s2205XML.alteracao.dtAlteracao = validadores.aaaa_mm_dd(row["dtAlteracao"].ToString());

						//// dadosTrabalhador   
						s2205XML.alteracao.dadosTrabalhador.nisTrab = row["nisTrab_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nmTrab = row["nmTrab_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.sexo = row["sexo_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.racaCor = row["racaCor_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.estCiv = row["estCiv_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.grauInstr = row["grauInstr_trabalhador"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nmSoc = row["nmSoc_trabalhador"].ToString();

						//// nascimento
						s2205XML.alteracao.dadosTrabalhador.nascimento.dtNascto = validadores.aaaa_mm_dd(row["dtNascto_nascimento"].ToString());
						s2205XML.alteracao.dadosTrabalhador.nascimento.codMunic = row["codMunic_nascimento"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nascimento.uf = row["uf_nascimento"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nascimento.paisNascto = row["paisNascto_nascimento"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nascimento.paisNac = row["paisNac_nascimento"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nascimento.nmMae = row["nmMae_nascimento"].ToString();
						s2205XML.alteracao.dadosTrabalhador.nascimento.nmPai = row["nmPai_nascimento"].ToString();

						//// documentos
						s2205XML.alteracao.dadosTrabalhador.documentos.CTPS.nrCtps = row["nrCtps_ctps"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.CTPS.serieCtps = row["serieCtps_ctps"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.CTPS.ufCtps = row["ufCtps_ctps"].ToString();

						s2205XML.alteracao.dadosTrabalhador.documentos.RIC.nrRic = row["nrRic_ric"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RIC.orgaoEmissor = row["orgaoEmissor_ric"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RIC.dtExped = validadores.aaaa_mm_dd(row["dtExped_ric"].ToString());

						s2205XML.alteracao.dadosTrabalhador.documentos.RG.nrRg = row["nrRg_rg"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RG.orgaoEmissor = row["orgaoEmissor_rg"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RG.dtExped = validadores.aaaa_mm_dd(row["dtExped_rg"].ToString());

						s2205XML.alteracao.dadosTrabalhador.documentos.RNE.nrRne = row["nrRne_rne"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RNE.orgaoEmissor = row["orgaoEmissor_rne"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.RNE.dtExped = validadores.aaaa_mm_dd(row["dtExped_rne"].ToString());

						s2205XML.alteracao.dadosTrabalhador.documentos.OC.nrOc = row["nrOc_oc"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.OC.orgaoEmissor = row["orgaoEmissor_oc"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.OC.dtExped = validadores.aaaa_mm_dd(row["dtExped_oc"].ToString());
						s2205XML.alteracao.dadosTrabalhador.documentos.OC.dtValid = validadores.aaaa_mm_dd(row["dtValid_oc"].ToString());

						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.nrRegCnh = row["nrRegCnh_cnh"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.dtExped = validadores.aaaa_mm_dd(row["dtExped_cnh"].ToString());
						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.ufCnh = row["ufCnh_cnh"].ToString();
						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.dtValid = validadores.aaaa_mm_dd(row["dtValid_cnh"].ToString());
						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.dtPriHab = validadores.aaaa_mm_dd(row["dtPriHab_cnh"].ToString());
						s2205XML.alteracao.dadosTrabalhador.documentos.CNH.categoriaCnh = row["categoriaCnh_cnh"].ToString();

						//// endereco brasil
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.tpLograd = row["tpLograd_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.dscLograd = row["dscLograd_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.nrLograd = row["nrLograd_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.complemento = row["complemento_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.bairro = row["bairro_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.cep = row["cep_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.codMunic = row["codMunic_brasil"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.brasil.uf = row["uf_brasil"].ToString();

						//// endereco exterior
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.paisResid = row["paisResid_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.dscLograd = row["dscLograd_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.nrLograd = row["nrLograd_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.complemento = row["complemento_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.bairro = row["bairro_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.nmCid = row["nmCid_exterior"].ToString();
						s2205XML.alteracao.dadosTrabalhador.endereco.exterior.codPostal = row["codPostal_exterior"].ToString();

						//// trabEstrangeiro
						s2205XML.alteracao.dadosTrabalhador.trabEstrangeiro.dtChegada = validadores.aaaa_mm_dd(row["dtChegada_trabEstrangeiro"].ToString());
						s2205XML.alteracao.dadosTrabalhador.trabEstrangeiro.classTrabEstrang = row["classTrabEstrang_trabEstrangeiro"].ToString();
						s2205XML.alteracao.dadosTrabalhador.trabEstrangeiro.casadoBr = row["casadoBr_trabEstrangeiro"].ToString();
						s2205XML.alteracao.dadosTrabalhador.trabEstrangeiro.filhosBr = row["filhosBr_trabEstrangeiro"].ToString();

						//// infoDeficiencia
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.defFisica = row["defFisica_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.defVisual = row["defVisual_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.defAuditiva = row["defAuditiva_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.defMental = row["defMental_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.defIntelectual = row["defIntelectual_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.reabReadap = row["reabReadap_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.infoCota = row["infoCota_infoDeficiencia"].ToString();
						s2205XML.alteracao.dadosTrabalhador.infoDeficiencia.observacao = row["observacao_infoDeficiencia"].ToString();

						// dependente 0.99
						List<string> lista2205dependente = new List<string>();

						gcl.setLevel("dependente", clear: true);

						var tbDependente = from DataRow r in tbEventos.Rows
												 where !string.IsNullOrEmpty(r[$"tpDep{gcl.getLevel}"].ToString()) &&
												 r["id_funcionario"].ToString().Equals(evento.id_funcionario)
												 select r;

						foreach (var dependente in tbDependente)
						{

							gcl.setLevel(row: dependente);

							// Só executa 1x para cada dependente
							if (!lista2205dependente.Contains(gcl.getVal("nmDep").ToString()))
							{
								// Registra o funcionário
								lista2205dependente.Add(gcl.getVal("nmDep").ToString());

								if (gcl.getVal("nmDep").ToString() != "")
								{
									s2205XML.alteracao.dadosTrabalhador.dependente.tpDep = gcl.getVal("tpDep");
									s2205XML.alteracao.dadosTrabalhador.dependente.nmDep = gcl.getVal("nmDep");
									s2205XML.alteracao.dadosTrabalhador.dependente.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
									s2205XML.alteracao.dadosTrabalhador.dependente.cpfDep = gcl.getVal("cpfDep");     // 0.1
									s2205XML.alteracao.dadosTrabalhador.dependente.depIRRF = gcl.getVal("depIRRF");
									s2205XML.alteracao.dadosTrabalhador.dependente.depSF = gcl.getVal("depSF");
									s2205XML.alteracao.dadosTrabalhador.dependente.incTrab = gcl.getVal("incTrab");

									s2205XML.add_dependente();
								}
							}
						}

						//// aposentadoria
						s2205XML.alteracao.dadosTrabalhador.aposentadoria.trabAposent = row["trabAposent_aposentadoria"].ToString();

						//// contato
						s2205XML.alteracao.dadosTrabalhador.contato.fonePrinc = row["fonePrinc_contato"].ToString();
						s2205XML.alteracao.dadosTrabalhador.contato.foneAlternat = row["foneAlternat_contato"].ToString();
						s2205XML.alteracao.dadosTrabalhador.contato.emailPrinc = row["emailPrinc_contato"].ToString();
						s2205XML.alteracao.dadosTrabalhador.contato.emailAlternat = row["emailAlternat_contato"].ToString();

						evento.eventoAssinadoXML = s2205XML.genSignedXML(evento.certificado);
						lEventos.Add(evento);
					}
				}
         }
         catch (Exception e) { addError("model.eventos.BD.s2205", e.Message); }
         return lEventos;
      }
   }
}
