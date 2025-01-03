using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML
{
   internal class s2306 : bEvento_XML
   {
      public s2306(string sID) : base("evtTSVAltContr", "", "v_S_01_03_00")
      {
         id = sID;
         ideEvento = new sIdeEvento();
         ideTrabSemVinculo = new sIdeTrabSemVinculo();

         infoTSVAlteracao = new sInfoTSVAlteracao();
         infoTSVAlteracao.infoComplementares = new sInfoTSVAlteracao.sInfoComplementares();
         infoTSVAlteracao.infoComplementares.cargoFuncao = new sInfoTSVAlteracao.sInfoComplementares.sCargoFuncao();
         infoTSVAlteracao.infoComplementares.remuneracao = new sInfoTSVAlteracao.sInfoComplementares.sRemuneracao();
         infoTSVAlteracao.infoComplementares.infoDirigenteSindical = new sInfoTSVAlteracao.sInfoComplementares.sInfoDirigenteSindical();
         infoTSVAlteracao.infoComplementares.infoTrabCedido = new sInfoTSVAlteracao.sInfoComplementares.sInfoTrabCedido();
         infoTSVAlteracao.infoComplementares.infoMandElet = new sInfoTSVAlteracao.sInfoComplementares.sInfoMandElet();
         infoTSVAlteracao.infoComplementares.infoEstagiario = new sInfoTSVAlteracao.sInfoComplementares.sInfoEstagiario();
         infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino = new sInfoTSVAlteracao.sInfoComplementares.sInfoEstagiario.sInstEnsino();
         infoTSVAlteracao.infoComplementares.infoEstagiario.ageIntegracao = new sInfoTSVAlteracao.sInfoComplementares.sInfoEstagiario.sAgeIntegracao();
         infoTSVAlteracao.infoComplementares.infoEstagiario.supervisorEstagiario = new sInfoTSVAlteracao.sInfoComplementares.sInfoEstagiario.sSupervisorEstagiario();
      }

      public override XElement genSignedXML(X509Certificate2 cert)
      {
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

         // ideTrabSemVinculo
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideTrabSemVinculo",
         new XElement(ns + "cpfTrab", ideTrabSemVinculo.cpfTrab),
         opTag("matricula", ideTrabSemVinculo.matricula),
         opTag("codCateg", ideTrabSemVinculo.codCateg)

         )); // ideTrabSemVinculo

         // infoTSVAlteracao 
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "infoTSVAlteracao",
         new XElement(ns + "dtAlteracao", infoTSVAlteracao.dtAlteracao),
         opTag("natAtividade", infoTSVAlteracao.natAtividade),

         // infoComplementares
         new XElement(ns + "infoComplementares",
         opElement("cargoFuncao", infoTSVAlteracao.infoComplementares.cargoFuncao.nmCargo,
         opTag("nmCargo", infoTSVAlteracao.infoComplementares.cargoFuncao.nmCargo),
         opTag("CBOCargo", infoTSVAlteracao.infoComplementares.cargoFuncao.CBOCargo),
         opTag("nmFuncao", infoTSVAlteracao.infoComplementares.cargoFuncao.nmFuncao),
         opTag("CBOFuncao", infoTSVAlteracao.infoComplementares.cargoFuncao.CBOFuncao)),

         new XElement(ns + "remuneracao",
         new XElement(ns + "vrSalFx", infoTSVAlteracao.infoComplementares.remuneracao.vrSalFx),
         new XElement(ns + "undSalFixo", infoTSVAlteracao.infoComplementares.remuneracao.undSalFixo),
         opTag("descSalVar", infoTSVAlteracao.infoComplementares.remuneracao.dscSalVar)),

         // infoDirigenteSindical
         opElement("infoDirigenteSindical", infoTSVAlteracao.infoComplementares.infoDirigenteSindical.tpRegPrev,
         new XElement(ns + "tpRegPrev", infoTSVAlteracao.infoComplementares.infoDirigenteSindical.tpRegPrev)),

         // infoTrabCedido
         opElement("infoTrabCedido", infoTSVAlteracao.infoComplementares.infoTrabCedido.tpRegPrev,
         opTag("tpRegPrev", infoTSVAlteracao.infoComplementares.infoTrabCedido.tpRegPrev)),

         // infoMandElet
         opElement("infoMandElet", infoTSVAlteracao.infoComplementares.infoMandElet.tpRegPrev,
         opTag("indRemunCargo", infoTSVAlteracao.infoComplementares.infoMandElet.indRemunCargo),
         new XElement(ns + "tpRegPrev", infoTSVAlteracao.infoComplementares.infoMandElet.tpRegPrev)),

         // infoEstagiario
         opElement("infoEstagiario", infoTSVAlteracao.infoComplementares.infoEstagiario.natEstagio,
         opTag("natEstagio", infoTSVAlteracao.infoComplementares.infoEstagiario.natEstagio),
         opTag("nivEstagio", infoTSVAlteracao.infoComplementares.infoEstagiario.nivEstagio),
         opTag("areaAtuacao", infoTSVAlteracao.infoComplementares.infoEstagiario.areaAtuacao),
         opTag("nrApol", infoTSVAlteracao.infoComplementares.infoEstagiario.nrApol),
         opTag("dtPrevTerm", infoTSVAlteracao.infoComplementares.infoEstagiario.dtPrevTeam)),

         // intEnsino
         opElement("instEnsino",
         opTag("cnpjInstEnsino", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.cnpjInstEnsino),
         opTag("nmRazao", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.nmRazao),
         opTag("dscLograd", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.dscLograd),
         opTag("nrLograd", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.nrLograd),
         opTag("bairro", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.bairro),
         opTag("cep", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.cep),
         opTag("codMunic", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.codMunic),
         opTag("uf", infoTSVAlteracao.infoComplementares.infoEstagiario.instEnsino.uf)),

         // ageIntegracao
         opElement("ageIntegracao", infoTSVAlteracao.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg,
         opTag("cnpjAgntInteg", infoTSVAlteracao.infoComplementares.infoEstagiario.ageIntegracao.cnpjAgntInteg)),

         // supervisorEstagio
         opElement("supervisorEstagio", infoTSVAlteracao.infoComplementares.infoEstagiario.supervisorEstagiario.cpfSupervisor,
         opTag("cpfSupervisor", infoTSVAlteracao.infoComplementares.infoEstagiario.supervisorEstagiario.cpfSupervisor))


         ))); // infoTSVAlteracao

         return x509.signXMLSHA256(xml, cert);
      }


      #region ****************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, indApuracao, nrRecibo, perApur, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeTrabSemVinculo ideTrabSemVinculo;
      public struct sIdeTrabSemVinculo
      {
         public string cpfTrab, matricula, codCateg;
      }

      public sInfoTSVAlteracao infoTSVAlteracao;
      public struct sInfoTSVAlteracao
      {
         public string dtAlteracao, natAtividade;

         public sInfoComplementares infoComplementares;
         public struct sInfoComplementares
         {
            public sCargoFuncao cargoFuncao;
            public struct sCargoFuncao
            {
               public string codCargo, codFuncao, nmCargo, CBOCargo, nmFuncao, CBOFuncao;
            }
            public sRemuneracao remuneracao;
            public struct sRemuneracao
            {
               public string undSalFixo;
               public string dscSalVar;
               public string vrSalFx;
            }
            public sInfoDirigenteSindical infoDirigenteSindical;
            public struct sInfoDirigenteSindical
            {
               public string tpRegPrev;
            }
            public sInfoTrabCedido infoTrabCedido;
            public struct sInfoTrabCedido
            {
               public string tpRegPrev;
            }
            public sInfoMandElet infoMandElet;
            public struct sInfoMandElet
            {
               public string indRemunCargo, tpRegPrev;
            }
            public sInfoEstagiario infoEstagiario;
            public struct sInfoEstagiario
            {
               public string natEstagio, nivEstagio, areaAtuacao, nrApol, dtPrevTeam;

               public sInstEnsino instEnsino;
               public struct sInstEnsino
               {
                  public string cnpjInstEnsino, nmRazao, dscLograd, nrLograd, bairro, cep, codMunic, uf;
               }
               public sAgeIntegracao ageIntegracao;
               public struct sAgeIntegracao
               {
                  public string cnpjAgntInteg;
               }
               public sSupervisorEstagiario supervisorEstagiario;
               public struct sSupervisorEstagiario
               {
                  public string cpfSupervisor;
               }
            }
         }

         #endregion
      }
   }
}
