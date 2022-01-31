using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            List<string> lista1210detPgto = new List<string>();
            List<string> lista1210retPgtoTot = new List<string>();
            List<string> lista1210detPgtoFer = new List<string>();
            List<string> lista1210detPgtoFer_eventos = new List<string>();
            List<string> lista1210penAlim_detRubrFer_eventos = new List<string>();
            List<string> lista1210detPgtoAnt = new List<string>();
            List<string> lista1210infoPgtoAnt = new List<string>();
				List<string> lista1210penAlim = new List<string>();
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
                        // Registra o funcionário
                        lista1210info.Add(sIDchave);

                        sTpPgto = gcl.getVal("tpPgto");
                        s1210XML.ideBenef.infoPgto.dtPgto = validadores.aaaa_mm_dd(gcl.getVal("dtPgto"));
								s1210XML.ideBenef.infoPgto.tpPgto = gcl.getVal("tpPgto");
								s1210XML.ideBenef.infoPgto.indResBr = gcl.getVal("indResBr");

                        Boolean bSegue_detPgtoFl = true;
                        if (sMesano == "201812") // || sMesano == "201906")
                           bSegue_detPgtoFl = false;

                        if (bSegue_detPgtoFl)
                        {
                           // detPgtoFl 0.200
                           gcl.setLevel("detPgtoFl");

                           //var tbDetPgtoFl = from DataRow r in tbInfoPgto
                           //                  where !string.IsNullOrEmpty(r[$"ideDmDev_detPgtoFl_infoPgto"].ToString()) &&
                           //                  r["id_detPgtoFl_infoPgto"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                           //                  select r;

                           // Agrupamento por data pgto
                           var tbDetPgtoFl = from DataRow r in tbInfoPgto
                                             where !string.IsNullOrEmpty(r[$"ideDmDev_detPgtoFl_infoPgto"].ToString()) &&
                                             r["idData_infoPgto"].ToString().Equals(sIDchave)
                                             select r;

                           foreach (var detPgtoFl in tbDetPgtoFl)
                           {
                              gcl.setLevel(row: detPgtoFl);

                              string sChaveDetPgtoFl = gcl.getVal("id").ToString();

                              // Só executa 1x para cada funcionario
                              if (!lista1210detPgto.Contains(sChaveDetPgtoFl))
                              {
                                 //if (gcl.getVal("vrLiq").ToString() != "0,00")
                                 //{
                                    // Registra o funcionário
                                    lista1210detPgto.Add(sChaveDetPgtoFl);

                                    if (sTpPgto == "1" || sTpPgto == "2" || sTpPgto == "3" || sTpPgto == "5")
                                    {
													 if (sTpPgto == "1" || sTpPgto == "5")
														 s1210XML.ideBenef.infoPgto.detPgtoFl.perRef = validadores.aaaa_mm(gcl.getVal("perRef"));       // 0.1

													 s1210XML.ideBenef.infoPgto.detPgtoFl.ideDmDev = gcl.getVal("ideDmDev");
                                        s1210XML.ideBenef.infoPgto.detPgtoFl.indPgtoTt = gcl.getVal("indPgtoTt");
                                        s1210XML.ideBenef.infoPgto.detPgtoFl.vrLiq = gcl.getVal("vrLiq").Replace(",", ".");
                                        s1210XML.ideBenef.infoPgto.detPgtoFl.nrRecArq = gcl.getVal("nrRecArq");   // 0.1

                                        // retPgtoTot 0.99
                                        gcl.setLevel("retPgtoTot_detPgtoFl", clear: true);

                                        var tbRetPgtoTot = from DataRow r in tbDetPgtoFl
                                                            where !string.IsNullOrEmpty(r[$"codRubr_retPgtoTot_detPgtoFl"].ToString()) &&
                                                            r["id_retPgtoTot_detPgtoFl"].ToString().Equals(sChaveDetPgtoFl)
                                                            select r;

                                        foreach (var retPgtoTot in tbRetPgtoTot)
                                        {

                                            gcl.setLevel(row: retPgtoTot);

                                          string sCodRubr = gcl.getVal("codRubr").ToString();
                                          string sChaveRetPgtoTot = gcl.getVal("chave").ToString();

                                          if (!lista1210retPgtoTot.Contains(sChaveRetPgtoTot))
                                            {

                                                if (gcl.getVal("vrRubr").ToString() != "0,00")
                                                {
                                                   lista1210retPgtoTot.Add(sChaveRetPgtoTot);

                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.codRubr = gcl.getVal("codRubr");
                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.qtdRubr = gcl.getVal("qtdRubr");     // 0.1
                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.fatorRubr = gcl.getVal("fatorRubr"); // 0.1
                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");       // 0.1
                                                   s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                                                   // penAlim 0.99
                                                   gcl.setLevel("penAlim_retPgtoTot", clear: true);

                                                   var tbPenAlim = from DataRow r in tbRetPgtoTot
                                                                   where r["codRubr_penAlim_retPgtoTot"].ToString().Equals(sCodRubr.ToString()) &&
                                                                         !string.IsNullOrEmpty(r[$"chave_penAlim_retPgtoTot"].ToString()) &&
                                                                         !string.IsNullOrEmpty(r[$"cpfBenef_penAlim_retPgtoTot"].ToString())
                                                                   select r;

                                                   foreach (var penAlim in tbPenAlim)
                                                   {

                                                      gcl.setLevel(row: penAlim);

                                                      if (!lista1210penAlim.Contains(gcl.getVal("chave").ToString()) && gcl.getVal("vlrPensao").ToString() != "0,00")
                                                      {
                                                         lista1210penAlim.Add(gcl.getVal("chave").ToString());

                                                         s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.cpfBenef = gcl.getVal("cpfBenef");
                                                         s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.dtNasctoBenef = validadores.aaaa_mm_dd(gcl.getVal("dtNasctoBenef")); // 0.1
                                                         s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.nmBenefic = gcl.getVal("nmBenefic");
                                                         s1210XML.ideBenef.infoPgto.detPgtoFl.retPgtoTot.penAlim.vlrPensao = gcl.getVal("vlrPensao").Replace(",", ".");

                                                         s1210XML.add_penAlim_detPgtoFl();
                                                      }
                                                   }
                                                   s1210XML.add_retPgtoTot_detPgtoFl();
                                                   gcl.setLevel("retPgtoTot_detPgtoFl", clear: true);
                                                }

                                            }
                                            
                                        }

                                        // infoPgtoParc 0.99
                                        gcl.setLevel("infoPgtoParc_detPgtoFl", clear: true);

                                        var tbInfoPgtoParc = from DataRow r in tbDetPgtoFl
                                                                where !string.IsNullOrEmpty(r[$"codRubr_infoPgtoParc_detPgtoFl"].ToString()) &&
                                                                r["id_infoPgtoParc_detPgtoFl"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                                                select r;

                                        foreach (var infoPgtoParc in tbInfoPgtoParc)
                                        {

                                            gcl.setLevel(row: infoPgtoParc);

                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.matricula = gcl.getVal("matricula");    // 0.1
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.codRubr = gcl.getVal("codRubr");
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.ideTabRubr = gcl.getVal("ideTabRubr");
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.qtdRubr = gcl.getVal("qtdRubr");        // 0.1
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.fatorRubr = gcl.getVal("fatorRubr");    // 0.1
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");          // 0.1
                                            s1210XML.ideBenef.infoPgto.detPgtoFl.infoPgtoParc.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                                            s1210XML.add_infoPgtoParc_detPgtoFl();
                                        }
                                        s1210XML.add_detPgtoFl();
                                        gcl.setLevel("detPgtoFl_infoPgto", clear: true);
                                    }
                                 //}
                              }
                           }
                        }

                        // detPgtoBenPr 0.1
                        gcl.setLevel("detPgtoBenPr_infoPgto", infoPgto, true);

                        s1210XML.ideBenef.infoPgto.detPgtoBenPr.perRef = gcl.getVal("perRef");
                        s1210XML.ideBenef.infoPgto.detPgtoBenPr.ideDmDev = gcl.getVal("ideDmDev");
                        s1210XML.ideBenef.infoPgto.detPgtoBenPr.indPgtoTt = gcl.getVal("indPgtoTt");
                        s1210XML.ideBenef.infoPgto.detPgtoBenPr.vrLiq = gcl.getVal("vrLiq").Replace(",", ".");

                        // retPgtoTot 0.99
                        gcl.setLevel("retPgtoTot_detPgtoBenPr", clear: true);

                        var tbRetPgtoTot_detPgtoBenPr = from DataRow r in tbInfoPgto
                                                        where !string.IsNullOrEmpty(r[$"codRubr_retPgtoTot_detPgtoBenPr"].ToString()) &&
                                                        r["id_retPgtoTot_detPgtoBenPr"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                                        select r;

                        foreach (var retPgtoTot_detPgtoBenPr in tbRetPgtoTot_detPgtoBenPr)
                        {

                           gcl.setLevel(row: retPgtoTot_detPgtoBenPr);

                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.codRubr = gcl.getVal("codRubr");
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.qtdRubr = gcl.getVal("qtdRubr");      // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.fatorRubr = gcl.getVal("fatorRubr");  // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");        // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.retPgtoTot.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                           s1210XML.add_retPgtoTot_detPgtoBenPr();
                        }

                        // infoPgtoParc 0.99
                        gcl.setLevel("infoPgtoParc_detPgtoBenPr", clear: true);

                        var tbInfoPgtoParc_detPgtoBenPr = from DataRow r in tbInfoPgto
                                                          where !string.IsNullOrEmpty(r[$"codRubr_infoPgtoParc_detPgtoBenPr"].ToString()) &&
                                                          r["id_infoPgtoParc_detPgtoBenPr"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                                          select r;

                        foreach (var infoPgtoParc_detPgtoBenPr in tbInfoPgtoParc_detPgtoBenPr)
                        {

                           gcl.setLevel(row: infoPgtoParc_detPgtoBenPr);

                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.codRubr = gcl.getVal("codRubr");
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.qtdRubr = gcl.getVal("qtdRubr");      // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.fatorRubr = gcl.getVal("fatorRubr");  // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");        // 0.1
                           s1210XML.ideBenef.infoPgto.detPgtoBenPr.infoPgtoParc.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                           s1210XML.add_infoPgtoParc_detPgtoBenPr();
                        }

                        // detPgtoFer 0.5
                        gcl.setLevel("detPgtoFer_infoPgto", clear: true);

                        var tbDetPgtoFer = from DataRow r in tbInfoPgto
                                           where !string.IsNullOrEmpty(r[$"codCateg_detPgtoFer_infoPgto"].ToString()) &&
                                           r["id_detPgtoFer_infoPgto"].ToString().Equals(infoPgto["id_infoPgto"].ToString()) &&
                                           r[$"tpPgto_infoPgto"].ToString().Equals("7")
                                           select r;

                        foreach (var detPgtoFer in tbDetPgtoFer)
                        {
                           gcl.setLevel(row: detPgtoFer);

                            // Só executa 1x para cada funcionario
                            if (!lista1210detPgtoFer.Contains(gcl.getVal("id").ToString()))
                            {
                                // Registra
                                lista1210detPgtoFer.Add(gcl.getVal("id").ToString());

                                if (gcl.getVal("qtDias").ToString() != "")
                                {

                                    s1210XML.ideBenef.infoPgto.detPgtoFer.codCateg = gcl.getVal("codCateg");
                                    s1210XML.ideBenef.infoPgto.detPgtoFer.matricula = gcl.getVal("matricula");
                                    s1210XML.ideBenef.infoPgto.detPgtoFer.dtIniGoz = validadores.aaaa_mm_dd(gcl.getVal("dtIniGoz"));
                                    s1210XML.ideBenef.infoPgto.detPgtoFer.qtDias = gcl.getVal("qtDias");
                                    s1210XML.ideBenef.infoPgto.detPgtoFer.vrLiq = gcl.getVal("vrLiq").Replace(",", ".");

                                    // detRubrFer 0.99
                                    gcl.setLevel("detRubrFer_detPgtoFer", clear: true);

                                    var tbDetRubrFer = from DataRow r in tbDetPgtoFer
                                                        where !string.IsNullOrEmpty(r[$"codRubr_detRubrFer_detPgtoFer"].ToString()) &&
                                                        r["id_detRubrFer_detPgtoFer"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                                        select r;

                                    foreach (var detRubrFer in tbDetRubrFer)
                                    {
                                       gcl.setLevel(row: detRubrFer);
                                       string sID_vincdep = gcl.getVal("id_vincdep").ToString();
                                       string sChaveDetRubrFer = gcl.getVal("chave").ToString();    // cobRubr

                                       if (!lista1210detPgtoFer_eventos.Contains(sChaveDetRubrFer))
                                       {
                                          lista1210detPgtoFer_eventos.Add(sChaveDetRubrFer);  

                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.codRubr = gcl.getVal("codRubr");
                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.ideTabRubr = validadores.ideTabRubr(gcl.getVal("ideTabRubr"), evento.nomeEmpresa, evento.nrInsc);
                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.qtdRubr = gcl.getVal("qtdRubr");      // 0.1
                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.fatorRubr = gcl.getVal("fatorRubr");  // 0.1
                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.vrUnit = gcl.getVal("vrUnit").Replace(",", ".");        // 0.1
                                          s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.vrRubr = gcl.getVal("vrRubr").Replace(",", ".");

                                          if (gcl.getVal("isPensao").ToString() == "1")
                                          {
                                             // penAlim 0.99
                                             gcl.setLevel("penAlim_detRubrFer", clear: true);

                                             var tbPenAlim = from DataRow r in tbDetRubrFer
                                                             where !string.IsNullOrEmpty(r[$"cpfBenef_penAlim_detRubrFer"].ToString()) &&
                                                                r["id_penAlim_detRubrFer"].ToString().Equals(infoPgto["id_infoPgto"].ToString()) &&
                                                                r["chave_penAlim_detRubrFer"].ToString().Equals(sChaveDetRubrFer.ToString())
                                                             select r;

                                             foreach (var penAlim in tbPenAlim)
                                             {

                                                gcl.setLevel(row: penAlim);
                                                
                                                if (!lista1210penAlim_detRubrFer_eventos.Contains(gcl.getVal("id_ficha").ToString()) && gcl.getVal("vlrPensao").ToString() != "0,00")
                                                {
                                                   lista1210penAlim_detRubrFer_eventos.Add(gcl.getVal("id_ficha").ToString());

                                                   s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.cpfBenef = gcl.getVal("cpfBenef");
                                                   s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.dtNasctoBenef = validadores.aaaa_mm_dd(gcl.getVal("dtNasctoBenef")); // 0.1
                                                   s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.nmBenefic = gcl.getVal("nmBenefic");
                                                   s1210XML.ideBenef.infoPgto.detPgtoFer.detRubrFer.penAlim.vlrPensao = gcl.getVal("vlrPensao").Replace(",", ".");

                                                   s1210XML.add_penAlim_detRubrFer();
                                                }
                                             }
                                          }

                                          s1210XML.add_detRubrFer();
                                          gcl.setLevel("detRubrFer_detPgtoFer", clear: true);
                                       }

                                    }

                                    s1210XML.add_detPgtoFer();
                                    gcl.setLevel("detPgtoFer_infoPgto", clear: true);
                                }
                            }
                        }

                        // detPgtoAnt 0.99
                        gcl.setLevel("detPgtoAnt", clear: true);

                        var tbDetPgtoAnt = from DataRow r in tbInfoPgto
                                           where !string.IsNullOrEmpty(r[$"codCateg_detPgtoAnt"].ToString()) &&
                                           r["id_detPgtoAnt"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                           select r;

                        foreach (var detPgtoAnt in tbDetPgtoAnt)
                        {

                           gcl.setLevel(row: detPgtoAnt);

                           // Só executa 1x para cada funcionario
                           if (!lista1210detPgtoAnt.Contains(gcl.getVal("id").ToString()))
                           {
                              // Registra 
                              lista1210detPgtoAnt.Add(gcl.getVal("id").ToString());

                              s1210XML.ideBenef.infoPgto.detPgtoAnt.codCateg = gcl.getVal("codCateg");

                              // infoPgtoAnt 1.99
                              gcl.setLevel("infoPgtoAnt", clear: true);

                              var tbInfoPgtoAnt = from DataRow r in tbDetPgtoAnt
                                                  where !string.IsNullOrEmpty(r["tpBcIRRF_infoPgtoAnt"].ToString()) &&
                                                  r["id_detPgtoAnt"].ToString().Equals(infoPgto["id_infoPgto"].ToString())
                                                  select r;

                              foreach (var infoPgtoAnt in tbInfoPgtoAnt)
                              {

                                 gcl.setLevel(row: infoPgtoAnt);

                                 // Só executa 1x para cada funcionario
                                 if (!lista1210infoPgtoAnt.Contains(gcl.getVal("id").ToString()))
                                 {
                                    // Registra 
                                    lista1210infoPgtoAnt.Add(gcl.getVal("id").ToString());

                                    s1210XML.ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt.tpBcIRRF = gcl.getVal("tpBcIRRF");
                                    s1210XML.ideBenef.infoPgto.detPgtoAnt.infoPgtoAnt.vrBcIRRF = gcl.getVal("vrBcIRRF").Replace(",", ".");

                                    s1210XML.add_infoPgtoAnt();
                                 }
                              }
                              s1210XML.add_detPgtoAnt();
                              gcl.setLevel("detPgtoAnt", clear: true);
                           }
                        }
                        s1210XML.add_infoPgto();
                        gcl.setLevel("infoPgto", clear: true);
                     }
                  }

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
