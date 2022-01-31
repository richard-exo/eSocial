using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML {
   class s2206 : bEvento_XML {

      public s2206(string sID) : base("evtAltContratual") {

         id = sID;

         ideEvento = new sIdeEvento();

         ideVinculo = new sIdeVinculo();

         altContratual = new sAltContratual();
         altContratual.vinculo = new sAltContratual.sVinculo();

         altContratual.infoRegimeTrab = new sAltContratual.sInfoRegimeTrab();

         altContratual.infoRegimeTrab.infoCeletista = new sAltContratual.sInfoRegimeTrab.sInfoCeletista();
         altContratual.infoRegimeTrab.infoCeletista.trabTemp = new sAltContratual.sInfoRegimeTrab.sInfoCeletista.sTrabTemp();
         altContratual.infoRegimeTrab.infoCeletista.aprend = new sAltContratual.sInfoRegimeTrab.sInfoCeletista.sAprend();

         altContratual.infoRegimeTrab.infoEstatutario = new sAltContratual.sInfoRegimeTrab.sInfoEstatutario();

         altContratual.infoContrato = new sAltContratual.sInfoContrato();

         altContratual.infoContrato.remuneracao = new sAltContratual.sInfoContrato.sRemuneracao();
         altContratual.infoContrato.duracao = new sAltContratual.sInfoContrato.sDuracao();

         altContratual.infoContrato.localTrabalho = new sAltContratual.sInfoContrato.sLocalTrabalho();
         altContratual.infoContrato.localTrabalho.localTrabGeral = new sAltContratual.sInfoContrato.sLocalTrabalho.sLocalTrabGeral();
         altContratual.infoContrato.localTrabalho.localTrabDom = new sAltContratual.sInfoContrato.sLocalTrabalho.sLocalTrabDom();

         altContratual.infoContrato.horContratual = new sAltContratual.sInfoContrato.sHorContratual();
         altContratual.infoContrato.horContratual.horario = new sAltContratual.sInfoContrato.sHorContratual.sHorario();

         altContratual.infoContrato.filiacaoSindical = new sAltContratual.sInfoContrato.sFiliacaoSindical();
         altContratual.infoContrato.alvaraJudicial = new sAltContratual.sInfoContrato.sAlvaraJudicial();
         altContratual.infoContrato.observacoes = new sAltContratual.sInfoContrato.sObservacoes();
         altContratual.infoContrato.servPubl = new sAltContratual.sInfoContrato.sServPubl();
      }

      public override XElement genSignedXML(X509Certificate2 cert) {

         // ideEvento
         xml.Elements().ElementAt(0).Element(ns + "ideEvento").ReplaceNodes(

         new XElement(ns + "indRetif", ideEvento.indRetif),
         opTag("nrRecibo", ideEvento.nrRecibo),
         new XElement(ns + "tpAmb", ideEvento.tpAmb.GetHashCode()),
         new XElement(ns + "procEmi", ideEvento.procEmi.GetHashCode()),
         new XElement(ns + "verProc", ideEvento.verProc));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // ideVinculo
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideVinculo",
         new XElement(ns + "cpfTrab", ideVinculo.cpfTrab),
         new XElement(ns + "nisTrab", ideVinculo.nisTrab),
         new XElement(ns + "matricula", ideVinculo.matricula)));

         // altContratual
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "altContratual",
         new XElement(ns + "dtAlteracao", altContratual.dtAlteracao),
         opTag("dtEf", altContratual.dtEf),
         opTag("dscAlt", altContratual.dscAlt),

         // vinculo
         new XElement(ns + "vinculo",
         new XElement(ns + "tpRegPrev", altContratual.vinculo.tpRegPrev)),

         // infoRegimeTrab
         new XElement(ns + "infoRegimeTrab",

         // infoCeletista 0.1
         opElement("infoCeletista", altContratual.infoRegimeTrab.infoCeletista.tpRegJor,
         new XElement(ns + "tpRegJor", altContratual.infoRegimeTrab.infoCeletista.tpRegJor),
         new XElement(ns + "natAtividade", altContratual.infoRegimeTrab.infoCeletista.natAtividade),
         opTag("dtBase", altContratual.infoRegimeTrab.infoCeletista.dtBase),
         new XElement(ns + "cnpjSindCategProf", altContratual.infoRegimeTrab.infoCeletista.cnpjSindCategProf),

         // trabTemp 0.1
         opElement("trabTemp", altContratual.infoRegimeTrab.infoCeletista.trabTemp.justProrr,
         new XElement(ns + "justProrr", altContratual.infoRegimeTrab.infoCeletista.trabTemp.justProrr)),

         // aprend 0.1
         opElement("aprend", altContratual.infoRegimeTrab.infoCeletista.aprend.tpInsc,
         new XElement(ns + "tpInsc", altContratual.infoRegimeTrab.infoCeletista.aprend.tpInsc),
         new XElement(ns + "nrInsc", altContratual.infoRegimeTrab.infoCeletista.aprend.nrInsc)))),

         // infoContrato
         new XElement(ns + "infoContrato",
         opTag("codCargo", altContratual.infoContrato.codCargo),
         opTag("codFuncao", altContratual.infoContrato.codFuncao),
         new XElement(ns + "codCateg", altContratual.infoContrato.codCateg),
         opTag("codCarreira", altContratual.infoContrato.codCarreira),
         opTag("dtIngrCarr", altContratual.infoContrato.dtIngrCarr),

         // remuneracao
         new XElement(ns + "remuneracao",
         new XElement(ns + "vrSalFx", altContratual.infoContrato.remuneracao.vrSalFx),
         new XElement(ns + "undSalFixo", altContratual.infoContrato.remuneracao.undSalFixo),
         opTag("codCateg", altContratual.infoContrato.remuneracao.dscSalVar)),

         // duracao
         new XElement(ns + "duracao",
         new XElement(ns + "tpContr", altContratual.infoContrato.duracao.tpContr),
         //new XElement(ns + "dtTerm", altContratual.infoContrato.duracao.dtTerm),
         //new XElement(ns + "objDet", altContratual.infoContrato.duracao.objDet)),
         opTag("dtTerm", altContratual.infoContrato.duracao.dtTerm),
         opTag("objDet", altContratual.infoContrato.duracao.objDet)),

         // localTrabalho
         new XElement(ns + "localTrabalho",

         // localTrabGeral 0.1
         opElement("localTrabGeral", altContratual.infoContrato.localTrabalho.localTrabGeral.tpInsc,
         new XElement(ns + "tpInsc", altContratual.infoContrato.localTrabalho.localTrabGeral.tpInsc),
         new XElement(ns + "nrInsc", altContratual.infoContrato.localTrabalho.localTrabGeral.nrInsc),
         opTag("descComp", altContratual.infoContrato.localTrabalho.localTrabGeral.descComp)),

         // localTrabDom 0.1
         opElement("localTrabDom", altContratual.infoContrato.localTrabalho.localTrabDom.tpLograd,
         new XElement(ns + "tpLograd", altContratual.infoContrato.localTrabalho.localTrabDom.tpLograd),
         new XElement(ns + "dscLograd", altContratual.infoContrato.localTrabalho.localTrabDom.dscLograd),
         new XElement(ns + "nrLograd", altContratual.infoContrato.localTrabalho.localTrabDom.nrLograd),
         opTag("complemento", altContratual.infoContrato.localTrabalho.localTrabDom.complemento),
         opTag("bairro", altContratual.infoContrato.localTrabalho.localTrabDom.bairro),
         new XElement(ns + "cep", altContratual.infoContrato.localTrabalho.localTrabDom.cep),
         new XElement(ns + "codMunic", altContratual.infoContrato.localTrabalho.localTrabDom.codMunic),
         new XElement(ns + "uf", altContratual.infoContrato.localTrabalho.localTrabDom.uf))),

         // horContratual 0.1
         opElement("horContratual", altContratual.infoContrato.horContratual.tpJornada,
         opTag("qtdHrsSem", altContratual.infoContrato.horContratual.qtdHrsSem),
         new XElement(ns + "tpJornada", altContratual.infoContrato.horContratual.tpJornada),
         opTag("dscTpJorn", altContratual.infoContrato.horContratual.dscTpJorn),
         new XElement(ns + "tmpParc", altContratual.infoContrato.horContratual.tmpParc),

         // horario 0.1
         from e in lHorario
         select e),

         // filiacaoSindical 0.2
         from e in lFiliacaoSindical
         select e,

         // alvaraJudicial 0.1
         opElement("alvaraJudicial", altContratual.infoContrato.alvaraJudicial.nrProcJud,
         new XElement(ns + "nrProcJud", altContratual.infoContrato.alvaraJudicial.nrProcJud)),

         // observacoes 0.99
         //from e in lObservacoes
         //select e,

         opElement("observacoes", altContratual.infoContrato.observacoes.observacao,
         new XElement(ns + "observacao", altContratual.infoContrato.observacoes.observacao)),

         // servPubl 0.1
         opElement("servPubl", altContratual.infoContrato.servPubl.mtvAlter,
         new XElement(ns + "mtvAlter", altContratual.infoContrato.servPubl.mtvAlter))

         )));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência

      #region filiacaoSindical

      List<XElement> lFiliacaoSindical = new List<XElement>();
      public void add_filiacaoSindical() {

         lFiliacaoSindical.Add(

         new XElement(ns + "filiacaoSindical",
         new XElement(ns + "cnpjSindTrab", altContratual.infoContrato.filiacaoSindical.cnpjSindTrab)));

         altContratual.infoContrato.filiacaoSindical = new sAltContratual.sInfoContrato.sFiliacaoSindical();

      }
      #endregion

      #region horario

      List<XElement> lHorario = new List<XElement>();
      public void add_horario() {

         lHorario.Add(

         new XElement(ns + "horario",
         new XElement(ns + "dia", altContratual.infoContrato.horContratual.horario.dia),
         new XElement(ns + "codHorContrat", altContratual.infoContrato.horContratual.horario.codHorContrat)));

         altContratual.infoContrato.horContratual.horario = new sAltContratual.sInfoContrato.sHorContratual.sHorario();

      }
      #endregion


      #region observacoes

      List<XElement> lObservacoes = new List<XElement>();
      public void add_observacoes() {

         lObservacoes.Add(

         new XElement(ns + "observacoes",
         new XElement(ns + "observacao", altContratual.infoContrato.observacoes.observacao)));

         altContratual.infoContrato.observacoes = new sAltContratual.sInfoContrato.sObservacoes();

      }
      #endregion

      #endregion

      #region ****************************************************************************************************************************** Structs
      
      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, nrRecibo, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeVinculo ideVinculo;
      public struct sIdeVinculo { public string cpfTrab, nisTrab, matricula; }

      public sAltContratual altContratual;
      public struct sAltContratual {
         public string dtAlteracao, dtEf;
         public string dscAlt;

         public sVinculo vinculo;
         public struct sVinculo { public string tpRegPrev; }

         public sInfoRegimeTrab infoRegimeTrab;
         public struct sInfoRegimeTrab {

            public sInfoCeletista infoCeletista;
            public struct sInfoCeletista {
               public string tpRegJor, natAtividade, dtBase;
               public string cnpjSindCategProf;

               public sTrabTemp trabTemp;
               public struct sTrabTemp { public string justProrr; }

               public sAprend aprend;
               public struct sAprend {
                  public string tpInsc;
                  public string nrInsc;
               }
            }
            public sInfoEstatutario infoEstatutario;
            public struct sInfoEstatutario { public string tpPlanRP; }
         }
         public sInfoContrato infoContrato;
         public struct sInfoContrato {

            public string codCargo, codFuncao, codCarreira;
            public string codCateg;
            public string dtIngrCarr;

            public sRemuneracao remuneracao;
            public struct sRemuneracao {
               public string dscSalVar;
               public string vrSalFx;
               public string undSalFixo;
            }

            public sDuracao duracao;
            public struct sDuracao {
               public string tpContr;
               public string dtTerm;
               public string objDet;
            }

            public sLocalTrabalho localTrabalho;
            public struct sLocalTrabalho {

               public sLocalTrabGeral localTrabGeral;
               public struct sLocalTrabGeral {
                  public string tpInsc;
                  public string nrInsc, descComp;
               }

               public sLocalTrabDom localTrabDom;
               public struct sLocalTrabDom {
                  public string tpLograd, dscLograd, nrLograd, complemento, bairro, cep, uf;
                  public string codMunic;
               }
            }
            public sHorContratual horContratual;
            public struct sHorContratual {
               public string qtdHrsSem;
               public string tpJornada, tmpParc;
               public string dscTpJorn;

               public sHorario horario;
               public struct sHorario {
                  public string dia;
                  public string codHorContrat;
               }
            }
            public sFiliacaoSindical filiacaoSindical;
            public struct sFiliacaoSindical { public string cnpjSindTrab; }

            public sAlvaraJudicial alvaraJudicial;
            public struct sAlvaraJudicial { public string nrProcJud; }

            public sObservacoes observacoes;
            public struct sObservacoes { public string observacao; }

            public sServPubl servPubl;
            public struct sServPubl { public string mtvAlter; }
         }
      }
   }
   #endregion
}
