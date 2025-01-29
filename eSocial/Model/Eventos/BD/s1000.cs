using System;
using System.Collections.Generic;
using System.Data;

namespace eSocial.Model.Eventos.BD {
   public class s1000 : bEvento_BD {

      XML.s1000 s1000XML;

      public s1000() : base("1000", "Info. empregador", enTipoEvento.eventosIniciais_1) { }

      public override List<sEvento> getEventosPendentes() {

         base.getEventosPendentes();

         try {

            foreach (DataRow row in tbEventos.Rows) {

               sEvento evento = initEvento(row["tpAmb"].ToString(), row["id_arquivo"].ToString(), row["id_evento"].ToString(), row["id_empresa"].ToString(), row["id_cliente"].ToString(), row["id_funcionario"].ToString());

               s1000XML = new XML.s1000(evento.id);
               //s1000XML = new XML.s1000((row["id_cliente"].ToString()=="380837" ? "123456789101234567890123456789" : evento.id));

               // ### Evento

               // ideEvento
               s1000XML.ideEvento.tpAmb = evento.tpAmb;
               s1000XML.ideEvento.procEmi = enProcEmi.appEmpregador_1;
               s1000XML.ideEvento.verProc = versao;

               // ideEmpregador
               s1000XML.ideEmpregador.tpInsc = evento.tpInsc;
               s1000XML.ideEmpregador.nrInsc = validadores.nrInsc(evento.tpInsc, evento.nrInsc, evento.natJurid);

               // exclusão
               if (row["modoEnvio"].ToString().Equals(enModoEnvio.exclusao.GetHashCode().ToString())) {

                  // Remove a base inteira do ambiente de testes
                  if (evento.tpAmb.Equals(enTpAmb.producaoRestrita_2)) {

                     s1000XML.infoEmpregador.inclusao.idePeriodo.iniValid = "2016-01";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.nmRazao = "RemoverEmpregadorDaBaseDeDadosDaProducaoRestrita";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.classTrib = "00";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.natJurid = "2240";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indCoop = "0";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indConstr = "0";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indDesFolha = "0";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indOptRegEletron = "1";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indEntEd = "N";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.indEtt = "N";

                     s1000XML.infoEmpregador.inclusao.infoCadastro.contato.nmCtt = "...";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.contato.cpfCtt = "12345678909";
                     s1000XML.infoEmpregador.inclusao.infoCadastro.contato.foneFixo = "1312345678";
                     
                     if (evento.tpInsc.ToString() != "cpf_2")
                        s1000XML.infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPJ.indSitPJ = "0";
                     else
                        s1000XML.infoEmpregador.inclusao.infoCadastro.infoComplementares.situacaoPF.indSitPF = "0";
                  }
                  else {
                     s1000XML.infoEmpregador.exclusao.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                     s1000XML.infoEmpregador.exclusao.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());
                  }
               }

               // inclusão / alteração
               else {

                  XML.s1000.sInfoEmpregador.sIncAlt incAlt = new XML.s1000.sInfoEmpregador.sIncAlt();

                  // idePeriodo
                  incAlt.idePeriodo.iniValid = validadores.aaaa_mm(row["iniValid"].ToString());
                  incAlt.idePeriodo.fimValid = validadores.aaaa_mm(row["fimValid"].ToString());

                  // infoCadastro
                  incAlt.infoCadastro.nmRazao = evento.nomeEmpresa;
                  incAlt.infoCadastro.classTrib = row["classTrib"].ToString();                  
                  incAlt.infoCadastro.indDesFolha = row["indDesFolha"].ToString();
                  if (row["indOpcCP"].ToString()!="0")
                     incAlt.infoCadastro.indOpcCP = row["indOpcCP"].ToString();
                  incAlt.infoCadastro.indOptRegEletron = row["indOptRegEletron"].ToString();                  
                  incAlt.infoCadastro.indEtt = row["indEtt"].ToString();
                  incAlt.infoCadastro.indTribFolhaPisPasep = row["indTribFolhaPisPasep"].ToString();

                  // contato
                  incAlt.infoCadastro.contato.nmCtt = row["nmCtt"].ToString();
                  incAlt.infoCadastro.contato.cpfCtt = onlyNumbers(row["cpfCtt"].ToString());
                  incAlt.infoCadastro.contato.foneFixo = onlyNumbers(row["foneFixo"].ToString());

                  // infoComplementares
                  if (evento.tpInsc.ToString() != "cpf_2")
                  {
                     incAlt.infoCadastro.natJurid = evento.natJurid;
                     incAlt.infoCadastro.indCoop = row["indCoop"].ToString();
                     incAlt.infoCadastro.indConstr = row["indConstr"].ToString();
                     incAlt.infoCadastro.indEntEd = row["indEntEd"].ToString();
                     incAlt.infoCadastro.infoComplementares.situacaoPJ.indSitPJ = row["indSitPJ"].ToString();
                  }
                  else
                     incAlt.infoCadastro.infoComplementares.situacaoPF.indSitPF = row["indSitPF"].ToString();

                  if (row["modoEnvio"].ToString().Equals(enModoEnvio.inclusao.GetHashCode().ToString())) {
                     s1000XML.infoEmpregador.inclusao = incAlt;
                  }
                  else if (row["modoEnvio"].ToString().Equals(enModoEnvio.alteracao.GetHashCode().ToString())) {

                     incAlt.novaValidade.iniValid = validadores.aaaa_mm(row["iniValid_novaValidade"].ToString());
                     incAlt.novaValidade.fimValid = validadores.aaaa_mm(row["fimValid_novaValidade"].ToString());

                     s1000XML.infoEmpregador.alteracao = incAlt;
                  }
               }

               evento.eventoAssinadoXML = s1000XML.genSignedXML(evento.certificado);
               lEventos.Add(evento);
            }
         }
         catch (Exception e) { addError("model.eventos.BD.s1000", e.Message); }

         return lEventos;
      }
   }
}
