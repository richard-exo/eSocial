using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static eSocial.Model.Eventos.XML.s1210.sIdeBenef;
using static eSocial.Model.Eventos.XML.s1210.sIdeBenef.sInfoIRComple;
using static eSocial.Model.Eventos.XML.s1210.sIdeBenef.sInfoPgto;

namespace eSocial.Model.Eventos.BD {
   public class s1210 : bEvento_BD {

      XML.s1210 s1210XML;

      public s1210() : base("1210", "Pag. de rendimentos do trab.", enTipoEvento.eventosPeriodicos_3) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            // Armazena os funcionários transmitidos
            List<string> lista1210 = new List<string>();
            List<string> lista1210info = new List<string>();
            //List<string> lista1210detPgto = new List<string>();
            //List<string> lista1210retPgtoTot = new List<string>();
            //List<string> lista1210detPgtoFer = new List<string>();
            //List<string> lista1210detPgtoFer_eventos = new List<string>();
            //List<string> lista1210penAlim_detRubrFer_eventos = new List<string>();
            //List<string> lista1210detPgtoAnt = new List<string>();
            //List<string> lista1210infoPgtoAnt = new List<string>();
				//List<string> lista1210penAlim = new List<string>();
            List<string> lista1210infoPgtoExt = new List<string>();
            List<string> lista1210infoPgtoExtEnd = new List<string>();
            List<string> lista1210infoIRComplem = new List<string>();
            List<string> lista1210infoDep = new List<string>();
            List<string> lista1210infoIRCR = new List<string>();
            List<string> lista1210infoIRCREnviado = new List<string>();

            List<string> lista1210DedPen = new List<string>();
            List<string> lista1210PenAlim = new List<string>();
            List<string> lista1210PrevidCompl = new List<string>();
            List<string> lista1210InfoProcRet = new List<string>();
            List<string> lista1210InfoValores = new List<string>();
            List<string> lista1210planSaude = new List<string>();
            List<string> lista1210infoDepSau = new List<string>();
            List<string> lista1210reembMed = new List<string>();

            string sIDchave; double nLiquido;

				foreach (DataRow row in tbEventos.Rows) {

               // Só executa 1x para cada funcionário
               if (!lista1210.Contains(row["id_funcionario"].ToString()))
               {
                  // Registra o funcionário
                  lista1210.Add(row["id_funcionario"].ToString());

                  string sTpPgto = "";
                  string sMesano = row["mesano"].ToString();

                  sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                  s1210XML = new XML.s1210(evento.id);

                  // ### Evento

                  // ideEvento
                  s1210XML.ideEvento.indRetif = row["indRetif"].ToString();
                  s1210XML.ideEvento.nrRecibo = row["nrRecibo"].ToString();
                  s1210XML.ideEvento.indApuracao = row["indApuracao"].ToString();
                  s1210XML.ideEvento.perApur = validadores.aaaa_mm(row["perApur"].ToString());
                  s1210XML.ideEvento.tpAmb = evento.tpAmb;
                  s1210XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                  s1210XML.ideEvento.verProc = versao;

                  // ideEmpregador
                  s1210XML.ideEmpregador.tpInsc = evento.tpInsc;
                  s1210XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                  // ideBenef
                  s1210XML.ideBenef.cpfBenef = row["cpfBenef"].ToString();

                  // deps 0.1
                  if (row["vrDedDeps"].ToString()!="0")
                     s1210XML.ideBenef.deps.vrDedDep = row["vrDedDeps"].ToString();
                  
                  // infoPgto 1.60
                  gcl.setLevel("infoPgto");

                  var tbInfoPgto = from DataRow r in tbEventos.Rows
                                   where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                   !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString())
                                   select r;

                  foreach (var infoPgto in tbInfoPgto)
                  {

                     gcl.setLevel(row: infoPgto);
                     sIDchave = gcl.getVal("idData").ToString();
                     nLiquido = Convert.ToDouble(gcl.getVal("vrLiq_detPgtoFl"));

                     // Só executa 1x para cada funcionario
                     if (!lista1210info.Contains(sIDchave) && nLiquido>0.00)
                     {
                        lista1210info.Add(sIDchave);

                        sTpPgto = gcl.getVal("tpPgto");
                        s1210XML.ideBenef.infoPgto.dtPgto = validadores.aaaa_mm_dd(gcl.getVal("dtPgto"));
								s1210XML.ideBenef.infoPgto.tpPgto = gcl.getVal("tpPgto");
								s1210XML.ideBenef.infoPgto.perRef = validadores.aaaa_mm(gcl.getVal("perRef"));
                        s1210XML.ideBenef.infoPgto.ideDmDev = gcl.getVal("ideDmDev");
                        s1210XML.ideBenef.infoPgto.vrLiq = gcl.getVal("vrLiq").Replace(",", ".");

                        #region estrutura nova 02/2024
                        s1210XML.ideBenef.infoPgto.paisResidExt = gcl.getVal("paisResidExt");

                        // infoPgtoExt 0.1
                        gcl.setLevel("infoPgtoExt");
                        var tbInfoPgtoExt = from DataRow r in tbEventos.Rows
                                            where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                            !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                            r["idData_infoPgtoExt"].ToString().Equals(sIDchave)
                                            select r;

                        foreach (var infoPgtoExt in tbInfoPgtoExt)
                        {
                           gcl.setLevel(row: infoPgtoExt);
                           string sIDchaveExt = gcl.getVal("id").ToString();

                           if (!lista1210infoPgtoExt.Contains(sIDchaveExt))
                           {
                              lista1210infoPgtoExt.Add(sIDchaveExt);

                              s1210XML.ideBenef.infoPgto.infoPgtoExt.indNIF = gcl.getVal("indNIF");
                              s1210XML.ideBenef.infoPgto.infoPgtoExt.nifBenef = gcl.getVal("nifBenef");
                              s1210XML.ideBenef.infoPgto.infoPgtoExt.frmTribut = gcl.getVal("frmTribut");

                              // endExt
                              gcl.setLevel("infoPgtoExtEnd");
                              var tbInfoPgtoExtEnd = from DataRow r in tbEventos.Rows
                                                     where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                     !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                                     r["idData_infoPgtoExt"].ToString().Equals(sIDchave)
                                                     select r;

                              foreach (var infoPgtoExtEnd in tbInfoPgtoExtEnd)
                              {
                                 gcl.setLevel(row: infoPgtoExtEnd);
                                 string sIDchaveExtEnd = gcl.getVal("id").ToString();

                                 if (!lista1210infoPgtoExtEnd.Contains(sIDchaveExtEnd))
                                 {
                                    lista1210infoPgtoExtEnd.Add(sIDchaveExtEnd);

                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endDscLograd = gcl.getVal("endDscLograd");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endNrLograd = gcl.getVal("endNrLograd");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endComplem = gcl.getVal("endComplem");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endBairro = gcl.getVal("endBairro");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endCidade = gcl.getVal("endCidade");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endEstado = gcl.getVal("endEstado");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.endCodPostal = gcl.getVal("endCodPostal");
                                    s1210XML.ideBenef.infoPgto.infoPgtoExt.endExt.telef = gcl.getVal("telef");
                                 }
                              }

                              s1210XML.add_infoPgtoExt();
                              gcl.setLevel("infoPgtoExt", clear: true);
                           }
                        }
                        #endregion

                        s1210XML.add_infoPgto();
                        gcl.setLevel("infoPgto", clear: true);
                     }
                  }

                  #region retrutura nova 02/2024
                  // infoIRComplem 0.1
                  gcl.setLevel("infoIRComplem", clear: true);

                  var tbInfoIRComplem = from DataRow r in tbEventos.Rows
                                        where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                        !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString())
                                        select r;

                  foreach (var infoIRComplem in tbInfoIRComplem)
                  {

                     gcl.setLevel(row: infoIRComplem);
                     string sIDchaveIRcomplem = gcl.getVal("id").ToString();

                     if (!lista1210infoIRComplem.Contains(sIDchaveIRcomplem) && sIDchaveIRcomplem!="0")
                     {
                        lista1210infoIRComplem.Add(sIDchaveIRcomplem);
                        s1210XML.ideBenef.infoIRComplem.temInfoIRComplem = gcl.getVal("temInfoIRComplem");
                        s1210XML.ideBenef.infoIRComplem.dtLaudo = validadores.aaaa_mm_dd(gcl.getVal("dtLaudo"));

                        // infoDep
                        gcl.setLevel("infoDep", clear: true);

                        var tbInfoDep = from DataRow r in tbEventos.Rows
                                        where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                        !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                        r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem)
                                        select r;

                        foreach (var infoDep in tbInfoDep)
                        {
                           gcl.setLevel(row: infoDep);
                           string sIDchaveInfoDep = gcl.getVal("id").ToString();

                           if (!lista1210infoDep.Contains(sIDchaveInfoDep))
                           {
                              lista1210infoDep.Add(sIDchaveInfoDep);
                              s1210XML.ideBenef.infoIRComplem.infoDep.cpfDep = gcl.getVal("cpfDep");
                              s1210XML.ideBenef.infoIRComplem.infoDep.dtNascto = validadores.aaaa_mm_dd(gcl.getVal("dtNascto"));
                              s1210XML.ideBenef.infoIRComplem.infoDep.nome = gcl.getVal("nome");
                              s1210XML.ideBenef.infoIRComplem.infoDep.depIRRF = gcl.getVal("depIRRF");
                              s1210XML.ideBenef.infoIRComplem.infoDep.tpDep = gcl.getVal("tpDep");
                              s1210XML.ideBenef.infoIRComplem.infoDep.descrDep = gcl.getVal("descrDep");

                              s1210XML.add_infoDep();
                              gcl.setLevel("infoDep", clear: true);
                           }
                        }

                        // infoIRCR
                        gcl.setLevel("infoIRCR", clear: true);

                        var tbInfoIRCR = from DataRow r in tbEventos.Rows
                                         where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                         !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                         r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem)
                                         select r;

                        foreach (var infoIRCR in tbInfoIRCR)
                        {
                           gcl.setLevel(row: infoIRCR);
                           string sIDchaveinfoIRCR = gcl.getVal("id").ToString();

                           if (!lista1210infoIRCR.Contains(sIDchaveinfoIRCR) && sIDchaveinfoIRCR!="0")
                           {
                              lista1210infoIRCR.Add(sIDchaveinfoIRCR);
                              s1210XML.ideBenef.infoIRComplem.infoIRCR.tpCR = gcl.getVal("tpCR");

                              // depPen
                              gcl.setLevel("depPen", clear: true);

                              var tbDedPen = from DataRow r in tbEventos.Rows
                                             where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                             !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                             r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                             r["id_infoIRCR"].ToString().Equals(sIDchaveinfoIRCR) &&
                                             !r["id_depPen"].ToString().Equals("0")
                                             select r;

                              foreach (var depPen in tbDedPen)
                              {
                                 gcl.setLevel(row: depPen);
                                 string sIDchaveDedPen = gcl.getVal("id").ToString();

                                 if (!lista1210DedPen.Contains(sIDchaveDedPen))
                                 {
                                    lista1210DedPen.Add(sIDchaveDedPen);
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.dedDepen.tpRend = gcl.getVal("tpRend");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.dedDepen.cpfDep = gcl.getVal("cpfDep");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.dedDepen.vlrDedDep = gcl.getVal("vlrDedDep").Replace(",", ".");

                                    s1210XML.add_dedPen();
                                    gcl.setLevel("depPen", clear: true);
                                 }
                              }

                              // PenAlim
                              gcl.setLevel("penAlim", clear: true);

                              var tbPenAlim = from DataRow r in tbEventos.Rows
                                              where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                              !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                              r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                              r["id_infoIRCR"].ToString().Equals(sIDchaveinfoIRCR) &&
                                              !r["id_vincdep_penAlim"].ToString().Equals("0")
                                              select r;

                              foreach (var penAlim in tbPenAlim)
                              {
                                 gcl.setLevel(row: penAlim);
                                 string sIDchavePenAlim = gcl.getVal("chave").ToString();

                                 if (!lista1210PenAlim.Contains(sIDchavePenAlim) && sIDchavePenAlim!="0")
                                 {
                                    lista1210PenAlim.Add(sIDchavePenAlim);
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.penAlim.tpRend = gcl.getVal("tpRend");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.penAlim.cpfDep = gcl.getVal("cpfDep");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.penAlim.vlrDedPenAlim = gcl.getVal("vlrDedPenAlim").Replace(",", ".");

                                    s1210XML.add_penAlim();
                                    gcl.setLevel("penAlim", clear: true);
                                 }
                              }

                              // lPrevidCompl
                              gcl.setLevel("previdCompl", clear: true);

                              var tbPrevidCompl = from DataRow r in tbEventos.Rows
                                                  where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                  !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                                  r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                                  r["id_infoIRCR"].ToString().Equals(sIDchaveinfoIRCR) &&
                                                  !r["id_previdCompl"].ToString().Equals("0")
                                                  select r;

                              foreach (var previdCompl in tbPrevidCompl)
                              {
                                 gcl.setLevel(row: previdCompl);
                                 string sIDchavePrevidCompl = gcl.getVal("id").ToString();

                                 if (!lista1210PrevidCompl.Contains(sIDchavePrevidCompl))
                                 {
                                    lista1210PrevidCompl.Add(sIDchavePrevidCompl);
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.previdCompl.tpPrev = gcl.getVal("tpPrev");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.previdCompl.cnpjEntidPC = gcl.getVal("cnpjEntidPC");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.previdCompl.vlrDedPC = gcl.getVal("vlrDedPC").Replace(",", ".");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.previdCompl.vlrPatrocFunp = gcl.getVal("vlrPatrocFunp").Replace(",", ".");

                                    //s1210XML.add_previdCompl();
                                    gcl.setLevel("previdCompl", clear: true);
                                 }
                              }

                              // InfoProcRet
                              gcl.setLevel("infoProcRet", clear: true);

                              var tbInfoProcRet = from DataRow r in tbEventos.Rows
                                                  where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                  !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                                  r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                                  r["id_infoIRCR"].ToString().Equals(sIDchaveinfoIRCR) &&
                                                  !r["id_infoProcRet"].ToString().Equals("0")
                                                  select r;

                              foreach (var infoProcRet in tbInfoProcRet)
                              {
                                 gcl.setLevel(row: infoProcRet);
                                 string sIDchaveInfoProcRet = gcl.getVal("id").ToString();

                                 if (!lista1210InfoProcRet.Contains(sIDchaveInfoProcRet))
                                 {
                                    lista1210InfoProcRet.Add(sIDchaveInfoProcRet);
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.tpProcRet = gcl.getVal("tpProcRet");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.nrProcRet = gcl.getVal("nrProcRet");
                                    s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.codSusp = gcl.getVal("codSusp");

                                    // lInfoValores
                                    gcl.setLevel("infoValores", clear: true);

                                    var tbInfoValores = from DataRow r in tbEventos.Rows
                                                        where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                        !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                                        r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                                        r["id_infoIRCR"].ToString().Equals(sIDchaveinfoIRCR) &&
                                                        r["id_infoProcRet"].ToString().Equals(sIDchaveInfoProcRet) &&
                                                        !r["id_infoValores"].ToString().Equals("0")
                                                        select r;

                                    foreach (var infoValores in tbInfoValores)
                                    {
                                       gcl.setLevel(row: infoValores);
                                       string sIDchaveInfoValores = gcl.getVal("id").ToString();

                                       if (!lista1210InfoValores.Contains(sIDchaveInfoValores))
                                       {
                                          lista1210InfoValores.Add(sIDchaveInfoValores);
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.indApuracao = gcl.getVal("indApuracao");
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrNRetido = gcl.getVal("vlrNRetido").Replace(",", ".");
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrDepJud = gcl.getVal("vlrDepJud").Replace(",", ".");
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrCmpAnoCal = gcl.getVal("vlrCmpAnoCal").Replace(",", ".");
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrCmpAnoAnt = gcl.getVal("vlrCmpAnoAnt").Replace(",", ".");
                                          s1210XML.ideBenef.infoIRComplem.infoIRCR.infoProcRet.infoValores.vlrRendSusp = gcl.getVal("vlrRendSusp").Replace(",", ".");

                                          //s1210XML.add_infoValores();
                                          gcl.setLevel("infoValores", clear: true);
                                       }
                                    }

                                    //s1210XML.add_infoProcRet();
                                    gcl.setLevel("infoProcRet", clear: true);
                                 }
                              }

                              // Para cada chave só executa 1x
                              if (!lista1210infoIRCREnviado.Contains(sIDchaveinfoIRCR))
                              {
                                 lista1210infoIRCREnviado.Add(sIDchaveinfoIRCR);
                                 s1210XML.add_infoIRCR();
                              }
                              gcl.setLevel("infoIRCR", clear: true);
                           }
                        }

                        gcl.setLevel("planSaude", clear: true);

                        var tbPlanSaude = from DataRow r in tbEventos.Rows
                                          where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                          !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                          r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                          !r["id_planSaude"].ToString().Equals("0")
                                          select r;

                        foreach (var planSaude in tbPlanSaude)
                        {
                           gcl.setLevel(row: planSaude);
                           string sIDchavePlanSaude = gcl.getVal("id").ToString();

                           if (!lista1210planSaude.Contains(sIDchavePlanSaude))
                           {
                              lista1210planSaude.Add(sIDchavePlanSaude);
                              s1210XML.ideBenef.infoIRComplem.planSaude.cnpjOper = gcl.getVal("cnpjOper");
                              s1210XML.ideBenef.infoIRComplem.planSaude.regANS = gcl.getVal("regANS");
                              s1210XML.ideBenef.infoIRComplem.planSaude.vlrSaudeTit = gcl.getVal("vlrSaudeTit").Replace(",", ".");

                              gcl.setLevel("infoDepSau", clear: true);

                              var tbInfoDepSau = from DataRow r in tbEventos.Rows
                                                 where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                                 !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                                 r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                                 r["id_planSaude"].ToString().Equals(sIDchavePlanSaude) &&
                                                 !r["id_infoDepSau"].ToString().Equals("0")
                                                 select r;

                              foreach (var infoDepSau in tbInfoDepSau)
                              {
                                 gcl.setLevel(row: infoDepSau);
                                 string sIDchaveInfoDepSau = gcl.getVal("id").ToString();

                                 if (!lista1210infoDepSau.Contains(sIDchaveInfoDepSau))
                                 {
                                    lista1210infoDepSau.Add(sIDchaveInfoDepSau);
                                    s1210XML.ideBenef.infoIRComplem.planSaude.infoDepSau.cpfDep = gcl.getVal("cpfDep");
                                    s1210XML.ideBenef.infoIRComplem.planSaude.infoDepSau.vlrSaudeDep = gcl.getVal("vlrSaudeDep").Replace(",", ".");

                                    s1210XML.add_infoDepSau();
                                    gcl.setLevel("infoDepSau", clear: true);
                                 }
                              }

                              s1210XML.add_planSaude();
                              gcl.setLevel("planSaude", clear: true);
                           }
                        }

                        //lInfoReembMed
                        gcl.setLevel("infoReembMed", clear: true);

                        var tbInfoReembMed = from DataRow r in tbEventos.Rows
                                             where r["id_funcionario"].ToString().Equals(evento.id_funcionario) &&
                                             !string.IsNullOrEmpty(r[$"dtPgto_infoPgto"].ToString()) &&
                                             r["id_infoIRComplem"].ToString().Equals(sIDchaveIRcomplem) &&
                                             !r["id_infoReembMed"].ToString().Equals("0")
                                             select r;

                        foreach (var infoReembMed in tbInfoReembMed)
                        {
                           gcl.setLevel(row: infoReembMed);
                           string sIDchaveReembMed = gcl.getVal("id").ToString();

                           if (!lista1210reembMed.Contains(sIDchaveReembMed))
                           {
                              lista1210reembMed.Add(sIDchaveReembMed);
                              s1210XML.ideBenef.infoIRComplem.infoReembMed.indOrgReemb = gcl.getVal("indOrgReemb");
                              s1210XML.ideBenef.infoIRComplem.infoReembMed.cnpjOper = gcl.getVal("cnpjOper");

                              s1210XML.add_infoReembMed();
                              gcl.setLevel("infoReembMed", clear: true);
                           }
                        }

                        s1210XML.add_infoIRComplem();
                        gcl.setLevel("infoIRComplem", clear: true);
                     }
                  }
                  #endregion

                  evento.eventoAssinadoXML = s1210XML.genSignedXML(evento.certificado);
                  lEventos.Add(evento);
               }
            }
         }

         catch (Exception e) {
            addError("model.eventos.BD.s1210", e.Message);
         }

         return lEventos;
      }
   }
}
