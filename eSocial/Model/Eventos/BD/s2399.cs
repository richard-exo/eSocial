using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eSocial.Model.Eventos.BD
{
    public class s2399 : bEvento_BD
    {

        XML.s2399 s2399XML;

        public s2399() : base("2399", "Trabalhador sem vinculo de emprego - Término", enTipoEvento.eventosNaoPeriodicos_2) { }

        public override List<sEvento> getEventosPendentes()
        {

            base.getEventosPendentes();

            try
            {

                List<string> lista2399 = new List<string>();

                foreach (DataRow row in tbEventos.Rows)
                {
                    // Só executa 1x para cada funcionário
                    if (!lista2399.Contains(row["id_autonomo"].ToString()))
                    {
                        // Registra o funcionário
                        lista2399.Add(row["id_autonomo"].ToString());

                        sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

                        s2399XML = new XML.s2399(evento.id);

                        // ### Evento

                        // ideEvento
                        s2399XML.ideEvento.indRetif = row["indRetif"].ToString();
                        s2399XML.ideEvento.nrRecibo = row["nrRecibo"].ToString(); // 0.1
                        s2399XML.ideEvento.tpAmb = evento.tpAmb;
                        s2399XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
                        s2399XML.ideEvento.verProc = versao;

                        // ideEmpregador
                        s2399XML.ideEmpregador.tpInsc = evento.tpInsc;
                        s2399XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

                        // trabalhador
                        gcl.setLevel("trabalhador", row);

                        s2399XML.trabalhador.cpfTrab = gcl.getVal("cpfTrab");
                        s2399XML.trabalhador.matricula = gcl.getVal("matricula");
                        s2399XML.trabalhador.codCateg = gcl.getVal("codCateg");                        

                        // infoTSVTermino
                        gcl.setLevel("infoTSVTermino", clear: true);                        
                        s2399XML.infoTSVTermino.dtTerm = validadores.aaaa_mm_dd(gcl.getVal("dtTerm"));
                        if (gcl.getVal("mtvDesligTSV").Trim().ToString()!="")
                            s2399XML.infoTSVTermino.mtvDesligTSV = gcl.getVal("mtvDesligTSV");
                        s2399XML.infoTSVTermino.pensAlim = gcl.getVal("pensAlim");
                        s2399XML.infoTSVTermino.percAliment = gcl.getVal("percAliment");
                        s2399XML.infoTSVTermino.vrAlim = gcl.getVal("vrAlim");                        

                        // mudancaCPF 0.1
                        gcl.setLevel("mudancaCPF", clear: true);

                        if (gcl.getVal("novoCPF") != "")
                        {
                            s2399XML.infoTSVTermino.mudancaCPF.novoCPF = gcl.getVal("novoCPF");
                        }

                        // quarentena 0.1
                        gcl.setLevel("quarentena", clear: true);
                        s2399XML.infoTSVTermino.quarentena.dtFimQuar = validadores.aaaa_mm_dd(gcl.getVal("dtFimQuar"));

                        evento.eventoAssinadoXML = s2399XML.genSignedXML(evento.certificado);
                        lEventos.Add(evento);
                    }
                }
            }
            catch (Exception e) { addError("model.eventos.BD.s2399", e.Message); }
            return lEventos;
        }
    }
}
