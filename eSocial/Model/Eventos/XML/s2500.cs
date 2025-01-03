using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using eSocial.Controller;

namespace eSocial.Model.Eventos.XML
{
   public class s2500 : bEvento_XML
   {

      public s2500(string sID) : base("evtProcTrab", "infoProcesso", "v_S_01_03_00")
      {

         id = sID;

         ideEvento = new sIdeEvento();
         ideResp = new sIdeResp();

         infoProcesso = new sInfoProcesso();
         infoProcesso.dadosCompl = new sInfoProcesso.sDadosCompl();

         ideTrab = new sIdeTrab();
         ideTrab.dependente = new sIdeTrab.sDependente();
         ideTrab.infoContr = new sIdeTrab.sInfoContr();

         ideTrab.infoContr.infoCompl = new sIdeTrab.sInfoContr.sInfoCompl();
         ideTrab.infoContr.infoCompl.infoVinc = new sIdeTrab.sInfoContr.sInfoCompl.sInfoVinc();
         ideTrab.infoContr.infoCompl.infoVinc.duracao = new sIdeTrab.sInfoContr.sInfoCompl.sInfoVinc.sDuracao();
         ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc = new sIdeTrab.sInfoContr.sInfoCompl.sInfoVinc.sSucessaoVinc();
         ideTrab.infoContr.infoCompl.infoVinc.infoDeslig = new sIdeTrab.sInfoContr.sInfoCompl.sInfoVinc.sInfoDeslig();
         ideTrab.infoContr.infoCompl.infoTerm = new sIdeTrab.sInfoContr.sInfoCompl.sInfoTerm();

         ideTrab.infoContr.ideEstab = new sIdeTrab.sInfoContr.sIdeEstab();
         ideTrab.infoContr.ideEstab.infoVlr = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr();
         ideTrab.infoContr.ideEstab.infoVlr.abono = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sAbono();
         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo();
         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sBaseCalculo();
         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.infoAgNocivo = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sBaseCalculo.sInfoAgNocivo ();
         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sInfoFGTS();
         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sBaseMudCateg();
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

         //// ideEmpregador
         //xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         //new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         //new XElement(ns + "nrInsc", ideEmpregador.nrInsc),

         //opElement("ideResp", ideResp.tpInsc,
         //new XElement(ns + "tpInsc", ideResp.tpInsc.GetHashCode()),
         //new XElement(ns + "nrInsc", ideResp.nrInsc)));

         // ideEmpregador
         xml.Elements().ElementAt(0).Element(ns + "ideEmpregador").ReplaceNodes(
         new XElement(ns + "tpInsc", ideEmpregador.tpInsc.GetHashCode()),
         new XElement(ns + "nrInsc", ideEmpregador.nrInsc));

         // infoProcesso
         xml.Elements().ElementAt(0).Element(ns + tagInfo).ReplaceNodes(

         //new XElement(ns + "infoProcesso",
         new XElement(ns + "origem", infoProcesso.origem),
         new XElement(ns + "nrProcTrab", infoProcesso.nrProcTrab),
         opTag("obsProcTrab", infoProcesso.obsProcTrab),

         // dadosCompl 1
         new XElement(ns + "dadosCompl",
         opElement("infoProcJud", infoProcesso.dadosCompl.infoProcJud.dtSent,
         new XElement(ns + "dtSent", infoProcesso.dadosCompl.infoProcJud.dtSent),
         new XElement(ns + "ufVara", infoProcesso.dadosCompl.infoProcJud.ufVara),
         new XElement(ns + "codMunic", infoProcesso.dadosCompl.infoProcJud.codMunic),
         new XElement(ns + "idVara", infoProcesso.dadosCompl.infoProcJud.idVara)),
         opElement("infoCCP", infoProcesso.dadosCompl.infoCCP.dtCCP,
         new XElement(ns + "dtCCP", infoProcesso.dadosCompl.infoCCP.dtCCP),
         new XElement(ns + "tpCCP", infoProcesso.dadosCompl.infoCCP.tpCCP),
         opTag("cnpjCCP", infoProcesso.dadosCompl.infoCCP.cnpjCCP))));

         // ideTrab 
         xml.Elements().ElementAt(0).Add(
         new XElement(ns + "ideTrab",
         new XElement(ns + "cpfTrab", ideTrab.cpfTrab),
         opTag("nmTrab", ideTrab.nmTrab),
         opTag("dtNascto", ideTrab.dtNascto),

         //opElement("dependente", ideTrab.dependente.cpfDep,
         //new XElement(ns + "cpfDep", ideTrab.dependente.cpfDep),
         //new XElement(ns + "tpDep", ideTrab.dependente.tpDep),
         //opTag("descDep", ideTrab.dependente.descDep)),

         //from e in lDependente
         //select e,

         // infoContr 0.99
         from e in lInfoContr
         select e

         ));

         return x509.signXMLSHA256(xml, cert);
      }

      #region *************************************************************************************************************** Tags com +1 ocorrência


      #region dependente

      List<XElement> lDependente = new List<XElement>();
      public void add_dependente()
      {
         lDependente.Add(
         opElement("dependente", ideTrab.dependente.cpfDep,
         new XElement(ns + "cpfDep", ideTrab.dependente.cpfDep),
         new XElement(ns + "tpDep", ideTrab.dependente.tpDep),
         opTag("descDep", ideTrab.dependente.descDep)));

         ideTrab.dependente = new sIdeTrab.sDependente();
      }

      #endregion

      #region infoContr

      List<XElement> lInfoContr = new List<XElement>();
      public void add_infoContr()
      {

         lInfoContr.Add(
         new XElement(ns + "infoContr",
         new XElement(ns + "tpContr", ideTrab.infoContr.tpContr),
         new XElement(ns + "indContr", ideTrab.infoContr.indContr),
         opTag("dtAdmOrig", ideTrab.infoContr.dtAdmOrig),
         opTag("indReint", ideTrab.infoContr.indReint),
         new XElement(ns + "indCateg", ideTrab.infoContr.indCateg),
         new XElement(ns + "indNatAtiv", ideTrab.infoContr.indNatAtiv),
         new XElement(ns + "indMotDeslig", ideTrab.infoContr.indMotDeslig),
         //opTag("indUnic", ideTrab.infoContr.indUnic),
         opTag("matricula", ideTrab.infoContr.matricula),
         opTag("codCateg", ideTrab.infoContr.codCateg),
         opTag("dtInicio", ideTrab.infoContr.dtInicio),

         // infoCompl 0.1
         opElement("infoCompl", ideTrab.infoContr.infoCompl.codCBO,
         opTag("codCBO", ideTrab.infoContr.infoCompl.codCBO),
         opTag("natAtividade", ideTrab.infoContr.infoCompl.natAtividade),

         // remuneracao 0.99
         from e in lRemuneracao
         select e,

         // infoVinc 0.1
         opElement("infoVinc", ideTrab.infoContr.infoCompl.infoVinc.tpRegTrab,
         new XElement(ns + "tpRegTrab", ideTrab.infoContr.infoCompl.infoVinc.tpRegTrab),
         new XElement(ns + "tpRegPrev", ideTrab.infoContr.infoCompl.infoVinc.tpRegPrev),
         new XElement(ns + "dtAdm", ideTrab.infoContr.infoCompl.infoVinc.dtAdm),
         opTag("tmpParc", ideTrab.infoContr.infoCompl.infoVinc.tmpParc),

         // duracao 0.1
         opElement("duracao", ideTrab.infoContr.infoCompl.infoVinc.duracao.tpContr,
         new XElement(ns + "tpContr", ideTrab.infoContr.infoCompl.infoVinc.duracao.tpContr),
         opTag("dtTerm", ideTrab.infoContr.infoCompl.infoVinc.duracao.dtTerm),
         opTag("clauAssec", ideTrab.infoContr.infoCompl.infoVinc.duracao.clauAssec),
         opTag("objDet", ideTrab.infoContr.infoCompl.infoVinc.duracao.objDet)),

         // remuneracao 0.99
         from e in lObservacoes
         select e,

         // sucessoVinc 0.1
         opElement("sucessaoVinc", ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.tpInsc,
         new XElement(ns + "tpInsc", ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.tpInsc),
         new XElement(ns + "nrInsc", ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.nrInsc),
         opTag("matricAnt", ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.matricAnt),
         new XElement(ns + "dtTransf", ideTrab.infoContr.infoCompl.infoVinc.sucessaoVinc.dtTransf)),

         // infoDeslig
         new XElement(ns + "infoDeslig",
         new XElement(ns + "dtDeslig", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.dtDeslig),
         new XElement(ns + "mtvDeslig", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.mtvDeslig),
         opTag("dtProjFimAPI", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.dtProjFimAPI),
         opTag("pensAlim", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.pensAlim),
         opTag("percAliment", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.percAliment),
         opTag("vrAlim", ideTrab.infoContr.infoCompl.infoVinc.infoDeslig.vrAlim))

         ),

         // infoTerm 0.1
         opElement("infoTerm", ideTrab.infoContr.infoCompl.infoTerm.dtTerm,
         new XElement(ns + "dtDeslig", ideTrab.infoContr.infoCompl.infoTerm.dtTerm),
         opTag("mtvDesligTSV", ideTrab.infoContr.infoCompl.infoTerm.mtvDesligTSV))),

         // mudCategAtiv 0.99
         from e in lMudCategAtiv
         select e,

         // unicContr 0.99
         from e in lUnicContr
         select e,

         // ideEstab
         new XElement(ns + "ideEstab",
         new XElement(ns + "tpInsc", ideTrab.infoContr.ideEstab.tpInsc),
         new XElement(ns + "nrInsc", ideTrab.infoContr.ideEstab.nrInsc),

         //  infoVlr 
         new XElement(ns + "infoVlr",
         new XElement(ns + "compIni", ideTrab.infoContr.ideEstab.infoVlr.compIni),
         new XElement(ns + "compFim", ideTrab.infoContr.ideEstab.infoVlr.compFim),
         //new XElement(ns + "repercProc", ideTrab.infoContr.ideEstab.infoVlr.repercProc),
         //new XElement(ns + "vrRemun", ideTrab.infoContr.ideEstab.infoVlr.vrRemun),
         //new XElement(ns + "vrAPI", ideTrab.infoContr.ideEstab.infoVlr.vrAPI),
         //new XElement(ns + "vr13API", ideTrab.infoContr.ideEstab.infoVlr.vr13API),
         //new XElement(ns + "vrInden", ideTrab.infoContr.ideEstab.infoVlr.vrInden),
         //opTag("vrBaseIndenFGTS", ideTrab.infoContr.ideEstab.infoVlr.vrBaseIndenFGTS),
         //opTag("pagDiretoResc", ideTrab.infoContr.ideEstab.infoVlr.pagDiretoResc),
         new XElement(ns + "indReperc", ideTrab.infoContr.ideEstab.infoVlr.indReperc),
         opTag("indenSD", ideTrab.infoContr.ideEstab.infoVlr.indenSD),
         opTag("indenAbono", ideTrab.infoContr.ideEstab.infoVlr.indenAbono),

         //  idePeriodo 0-9
         from e in lAbono
         select e,

         //  idePeriodo 0-360
         from e in lPeriodo
         select e//,

         ////  baseCalculo 
         ////new XElement(ns + "idePeriodo",
         //new XElement(ns + "baseCalculo",
         //new XElement(ns + "vrBcCpMensal", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCpMensal),
         //new XElement(ns + "vrBcCp13", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCp13),
         //new XElement(ns + "vrBcFgts", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts),
         //new XElement(ns + "vrBcFgts13", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts13),

         ////  infoAgNocivo 
         //new XElement(ns + "infoAgNocivo",
         //new XElement(ns + "grauExp", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.infoAgNocivo.grauExp))),

         ////  infoFGTS 
         //opElement("infoFGTS", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFgtsGuia,
         //new XElement(ns + "vrBcFgtsGuia", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFgtsGuia),
         //new XElement(ns + "vrBcFgts13Guia", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFgts13Guia),
         //new XElement(ns + "vrBcFgts", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFgtsGuia),
         //new XElement(ns + "pagDireto", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.pagDireto)),


         ////  infoFGTS 0.1
         //opElement("baseMudCateg", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.codCateg,
         //new XElement(ns + "codCateg", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.codCateg),
         //new XElement(ns + "vrBcCPrev", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.vrBcCPrev))

         //)

         ))));

         lDependente = new List<XElement>();
         lRemuneracao = new List<XElement>();
         lObservacoes = new List<XElement>();
         lMudCategAtiv = new List<XElement>();
         lUnicContr = new List<XElement>();
         lAbono = new List<XElement>();
         lPeriodo = new List<XElement>();
         ideTrab.infoContr = new sIdeTrab.sInfoContr();
      }

      #endregion

      #region abono
      List<XElement> lAbono = new List<XElement>();
      public void add_abono()
      {
         lAbono.Add(
         opElement("infoFGTS", ideTrab.infoContr.ideEstab.infoVlr.abono.abono,
         new XElement(ns + "abono", ideTrab.infoContr.ideEstab.infoVlr.abono.abono)

         ));

         ideTrab.infoContr.ideEstab.infoVlr.abono = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sAbono();
      }
      #endregion

      #region periodo
      List<XElement> lPeriodo = new List<XElement>();
      public void add_periodo()
      {
         lPeriodo.Add(
         new XElement(ns + "idePeriodo",
         new XElement(ns + "perRef", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.perRef),

         from e in lBaseCalculo
         select e,

         from e in lInfoFGTS
         select e,

         from e in lBaseMudCateg
         select e
         
         ));

         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo();

         lBaseCalculo = new List<XElement>();
         lInfoFGTS = new List<XElement>();
         lBaseMudCateg = new List<XElement>();
      }
      #endregion

      #region baseCalculo
      List<XElement> lBaseCalculo = new List<XElement>();
      public void add_baseCalculo()
      {

         lBaseCalculo.Add(
         new XElement(ns + "baseCalculo",
         new XElement(ns + "vrBcCpMensal", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCpMensal),
         new XElement(ns + "vrBcCp13", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcCp13),
         //new XElement(ns + "vrBcFgts", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts),
         //new XElement(ns + "vrBcFgts13", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.vrBcFgts13),

         //  infoAgNocivo 
         new XElement(ns + "infoAgNocivo",
         new XElement(ns + "grauExp", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo.infoAgNocivo.grauExp))));

         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseCalculo = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sBaseCalculo();
      }
      #endregion

      #region infoFGTS
      List<XElement> lInfoFGTS = new List<XElement>();
      public void add_infoFGTS()
      {
         lInfoFGTS.Add(
         opElement("infoFGTS", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.temInfoFGTS,
         new XElement(ns + "vrBcFGTSProcTrab", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSProcTrab),
         new XElement(ns + "vrBcFGTSSefip", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSSefip),
         new XElement(ns + "vrBcFGTSDecAnt", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS.vrBcFGTSDecAnt)));

         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.infoFGTS = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sInfoFGTS();
      }
      #endregion

      #region baseMudCateg
      List<XElement> lBaseMudCateg = new List<XElement>();
      public void add_baseMudCateg()
      {
         lBaseMudCateg.Add(
         opElement("baseMudCateg", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.codCateg,
         new XElement(ns + "codCateg", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.codCateg),
         new XElement(ns + "vrBcCPrev", ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg.vrBcCPrev)));

         ideTrab.infoContr.ideEstab.infoVlr.idePeriodo.baseMudCateg = new sIdeTrab.sInfoContr.sIdeEstab.sInfoVlr.sIdePeriodo.sBaseMudCateg();
      }
      #endregion

      #region remuneracao
      List<XElement> lRemuneracao = new List<XElement>();
      public void add_remuneracao()
      {

         lRemuneracao.Add(
         opElement("remuneracao", ideTrab.infoContr.infoCompl.remuneracao.dtRemun,
         new XElement(ns + "dtRemun", ideTrab.infoContr.infoCompl.remuneracao.dtRemun),
         new XElement(ns + "vrSalFx", ideTrab.infoContr.infoCompl.remuneracao.vrSalFx),
         new XElement(ns + "undSalFixo", ideTrab.infoContr.infoCompl.remuneracao.undSalFixo),
         opTag("dscSalVar", ideTrab.infoContr.infoCompl.remuneracao.dscSalVar)));

         ideTrab.infoContr.infoCompl.remuneracao = new sIdeTrab.sInfoContr.sInfoCompl.sRemuneracao();
            
      }
      #endregion

      #region observacoes

      List<XElement> lObservacoes = new List<XElement>();
      public void add_observacoes()
      {

         lObservacoes.Add(
         opElement("observacoes", ideTrab.infoContr.infoCompl.infoVinc.observacoes.observacao,
         new XElement(ns + "observacao", ideTrab.infoContr.infoCompl.infoVinc.observacoes.observacao)));

         ideTrab.infoContr.infoCompl.infoVinc.observacoes = new sIdeTrab.sInfoContr.sInfoCompl.sInfoVinc.sObservacoes();
      }
      #endregion

      #region mudCategAtiv

      List<XElement> lMudCategAtiv = new List<XElement>();
      public void add_mudaCategAtiv()
      {

         lMudCategAtiv.Add(
         opElement("mudCategAtiv", ideTrab.infoContr.mudCategAtiv.codCateg,
         new XElement(ns + "codCateg", ideTrab.infoContr.mudCategAtiv.codCateg),
         opTag("natAtividade", ideTrab.infoContr.mudCategAtiv.natAtividade),
         new XElement(ns + "dtMudCategAtiv", ideTrab.infoContr.mudCategAtiv.dtMudCategAtiv)));

         ideTrab.infoContr.mudCategAtiv = new sIdeTrab.sInfoContr.sMudCategAtiv();
      }
      #endregion

      #region unicContr

      List<XElement> lUnicContr = new List<XElement>();
      public void add_unicContr()
      {

         lUnicContr.Add(
         opElement("unicContr", ideTrab.infoContr.unicContr.matUnic,
         opTag("matUnic", ideTrab.infoContr.unicContr.matUnic),
         opTag("codCateg", ideTrab.infoContr.unicContr.codCateg),
         opTag("dtInicio", ideTrab.infoContr.unicContr.dtInicio)));

         ideTrab.infoContr.unicContr = new sIdeTrab.sInfoContr.sUniContr();
      }
      #endregion

      #region ***************************************************************************************************************************** Structs

      public new sIdeEvento ideEvento;
      public new struct sIdeEvento
      {
         public string indRetif, indApuracao, nrRecibo, perApur, verProc;
         public enTpAmb tpAmb;
         public enProcEmi procEmi;
      }

      public sIdeResp ideResp;
      public struct sIdeResp { public string tpInsc, nrInsc; }

      public sInfoProcesso infoProcesso;
      public struct sInfoProcesso
      {
         public string origem, nrProcTrab, obsProcTrab;

         public sDadosCompl dadosCompl;
         public struct sDadosCompl
         {

            public sInfoProcJud infoProcJud;
            public struct sInfoProcJud { public string dtSent, ufVara, codMunic, idVara; }

            public sInfoCCP infoCCP;
            public struct sInfoCCP { public string dtCCP, tpCCP, cnpjCCP; }
         }
      }

      public sIdeTrab ideTrab;
      public struct sIdeTrab
      {
         public string cpfTrab, nmTrab, dtNascto;

         public sDependente dependente;
         public struct sDependente { 
            public string cpfDep, tpDep, descDep; }

         public sInfoContr infoContr;
         public struct sInfoContr { 
            public string tpContr, indContr, dtAdmOrig, indReint, indCateg, indNatAtiv, indMotDeslig, indUnic, matricula, codCateg, dtInicio;

            public sInfoCompl infoCompl;
            public struct sInfoCompl { 
               public string codCBO, natAtividade;

               public sRemuneracao remuneracao;
               public struct sRemuneracao { 
                  public string dtRemun, vrSalFx, undSalFixo, dscSalVar; }

               public sInfoVinc infoVinc;
               public struct sInfoVinc { 
                  public string tpRegTrab, tpRegPrev, dtAdm, tmpParc;

                  public sDuracao duracao;
                  public struct sDuracao { 
                     public string tpContr, dtTerm, clauAssec, objDet; }

                  public sObservacoes observacoes;
                  public struct sObservacoes { 
                     public string observacao; }

                  public sSucessaoVinc sucessaoVinc;
                  public struct sSucessaoVinc { 
                     public string tpInsc, nrInsc, matricAnt, dtTransf; }

                  public sInfoDeslig infoDeslig;
                  public struct sInfoDeslig { 
                     public string dtDeslig, mtvDeslig, dtProjFimAPI, pensAlim, percAliment, vrAlim; }
               }

               public sInfoTerm infoTerm;
               public struct sInfoTerm { 
                  public string dtTerm, mtvDesligTSV; }
            }

            public sMudCategAtiv mudCategAtiv;
            public struct sMudCategAtiv { 
               public string codCateg, natAtividade, dtMudCategAtiv; }

            public sUniContr unicContr;
            public struct sUniContr { 
               public string matUnic, codCateg, dtInicio; }

            public sIdeEstab ideEstab;
            public struct sIdeEstab { 
               public string tpInsc, nrInsc;

               public sInfoVlr infoVlr;
               public struct sInfoVlr { 
                  public string compIni, compFim, repercProc, vrRemun, vrAPI, vr13API, vrInden, vrBaseIndenFGTS, pagDiretoResc, indReperc, indenSD, indenAbono;

                  public sAbono abono;
                  public struct sAbono
                  {
                     public string abono;
                  }

                  public sIdePeriodo idePeriodo;
                  public struct sIdePeriodo { 
                     public string perRef;

                     public sBaseCalculo baseCalculo;
                     public struct sBaseCalculo { 
                        public string vrBcCpMensal, vrBcCp13, vrBcFgts, vrBcFgts13;

                        public sInfoAgNocivo infoAgNocivo;
                        public struct sInfoAgNocivo { 
                           public string grauExp; }
                     }

                     public sInfoFGTS infoFGTS;
                     public struct sInfoFGTS { 
                        public string temInfoFGTS, vrBcFGTSProcTrab, vrBcFGTSSefip, vrBcFGTSDecAnt; }

                     public sBaseMudCateg baseMudCateg;
                     public struct sBaseMudCateg { 
                        public string codCateg, vrBcCPrev; }
                  }
               }
            }
         }
      }
      #endregion

      #endregion
   }
}
