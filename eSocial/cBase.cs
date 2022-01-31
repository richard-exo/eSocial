using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using eSocial.Controller;
using eSocial.Model;
using eSocial.Model.Eventos.Retorno;

namespace eSocial
{

   public abstract class cBase
   {

      static bool bHasError;
      static List<Tuple<string, string>> lastErrors = new List<Tuple<string, string>>();

      static int _seqID = 0, _grupoEvento = 0;

      public XElement xml;
      public XNamespace ns;

      static Dictionary<int, string> lCdResposta;

      public static int qtdEvtLote { get { return int.Parse(ConfigurationManager.AppSettings["qtdEvtLote"]); } }
      public static string versao { get { return ConfigurationManager.AppSettings["versao"]; } }
      //public static string versao2 { get { return ConfigurationManager.AppSettings["versao2"]; } } // layout 2020
      public static bool debugMode { get { return ConfigurationManager.AppSettings["debug"].Equals("1"); } }
      static public string seqID { get { if (_seqID >= (99999)) { _seqID = 0; } _seqID++; return _seqID.ToString().PadLeft(5, '0'); } }
      static public int grupoEvento { get { if (_grupoEvento >= (int.MaxValue)) { _grupoEvento = 0; } _grupoEvento++; return _grupoEvento; } }

      public bool hasError { get { return bHasError; } }

      public static void addError(string caller, string message)
      {
         bHasError = true;
         lastErrors.Add(new Tuple<string, string>(caller, message));
      }
      public static List<Tuple<string, string>> getErrors()
      {

         var _lastErrors = lastErrors.ToList();
         lastErrors.Clear();
         bHasError = false;

         return _lastErrors;
      }

      public static Dictionary<int, string> getServerResponses
      {
         get
         {
            if (lCdResposta == null)
            {
               lCdResposta = new Dictionary<int, string>();
               lCdResposta.Add(enCdResposta.proc_101.GetHashCode(), "PROC");
               lCdResposta.Add(enCdResposta.filaProc_203.GetHashCode(), "FPRO");
               lCdResposta.Add(enCdResposta.sucesso_201.GetHashCode(), "OK");
               lCdResposta.Add(enCdResposta.advertencia_202.GetHashCode(), "ADVR");
               lCdResposta.Add(enCdResposta.aguardando_999.GetHashCode(), "AGUA");
            }
            return lCdResposta;
         }
      }

      public string onlyNumbers(string input) { return Regex.Replace(input, "[^0-9.]", ""); }
      public enum enTipoEvento { eventosIniciais_1 = 1, eventosNaoPeriodicos_2 = 2, eventosPeriodicos_3 = 3, eventosParciais_4 = 4 }
      public enum enModoEnvio { inclusao = 0, alteracao = 1, exclusao = 2 }
      public enum enCdResposta { proc_101 = 101, sucesso_201 = 201, advertencia_202 = 202, filaProc_203 = 203, aguardando_999 = 999 }
      public enum enTpAmb
      {
         producao_1 = 1,
         producaoRestrita_2 = 2
      }

      public enum enProcEmi
      {
         appEmpregador_1 = 1,
         appAppWeb_2 = 2
      }
      public struct sIdeEvento
      {
         public enProcEmi procEmi;
         public enTpAmb tpAmb;
         public string verProc;
      }

      public enum enTpInsc { cnpj_1 = 1, cpf_2 = 2 }

      public class sEvento
      {
         public string nomeEmpresa, id_empresa, id_cliente, id_funcionario;
         public string id, nomeEvento, descricaoEvento, id_evento, id_arquivo;
         public enTpInsc tpInsc;

         protected enTpAmb _tpAmb = enTpAmb.producaoRestrita_2; // Inicia no modo de teste
         public enTpAmb tpAmb { get { return _tpAmb; } internal set { _tpAmb = value; } }

         string _natJurid, _nrInsc;
         public string natJurid { get { return _natJurid; } set { _natJurid = value; } }
         public string nrInsc
         {
            get { return _nrInsc; }
            set { _nrInsc = validadores.nrInsc(tpInsc, value); }
         }

         XElement _eventoAssinadoXML;
         protected X509Certificate2 _certificado;

         public XElement eventoAssinadoXML { get { return _eventoAssinadoXML; } set { _eventoAssinadoXML = value; } }

         int _grupoEvento = 0;
         public int grupoEvento { get { return _grupoEvento; } set { _grupoEvento = value; } }

         public sDadosConsulta dadosConsulta = new sDadosConsulta();
         public struct sDadosConsulta { public string status, protocolo, proximaConsulta; }

         public sIdeEmpTrans ideTransmissor = new sIdeEmpTrans();
         public sIdeEmpTrans ideEmpregador = new sIdeEmpTrans();

         public X509Certificate2 certificado { get { return _certificado; } set { _certificado = value; } }
      }

      public struct sIdeEmpTrans
      {

         public enTpInsc tpInsc;
         string _nrInsc;
         public string nrInsc
         {
            get { return _nrInsc; }
            set { _nrInsc = validadores.nrInsc(tpInsc, value); }
         }
      }
      public struct sIdePeriodo { public string iniValid, fimValid; }

      public static class validadores
      {
         public static string nrInsc(enTpInsc tpInsc, string cpf_cnpj, string natJurid = "")
         {  // 1=CNPJ, 2=CPF

            string _cpf_cnpj = cpf_cnpj.Trim().Replace(".", "").Replace("/", "").Replace("-", "");
            string _natJurid = natJurid.Replace("-", "");

            if (tpInsc. ToString()!= "cpf_2") // se for CPF, retorna o bloco todo
            {
               switch (natJurid)
               {
                  case "": break;
                  case "1015": break;
                  case "1040": break;
                  case "1074": break;
                  case "1163": break;
                  default: _cpf_cnpj = _cpf_cnpj.Substring(0, 8); break;
               }
            }
            return _cpf_cnpj;
         }

         public static string ID(string tpInsc, string cpf_cnpj, string natJurid)
         {

            StringBuilder ID = new StringBuilder();

            ID.Append("ID");
            ID.Append(tpInsc);
            ID.Append(nrInsc( (tpInsc=="2" ? enTpInsc.cpf_2 : enTpInsc.cnpj_1), cpf_cnpj, natJurid).PadRight(14, '0'));
            ID.Append(DateTime.Now.ToString("yyyyMMddhhmmss"));
            ID.Append(seqID);

            return ID.ToString();
         }

         // Nome personalizado dado para a tabela de rubricas de cada filial da empresa
         // Pegamos o primeiro nome da empresa e os dois digitos do CNPJ da mesma
         // http://blog.bluetax.com.br/profiles/blogs/no-esocial-um-empregador-podera-adotar-uma-tabela-de-rubricas-dif
         public static string ideTabRubr(string ideTabRubr = "", string nomeEmpresa = "", string cnpj = "")
         {
            return ideTabRubr;

            // # DEPRECATED: Retornar o ideTabRubr do BD.
            /*if (!string.IsNullOrEmpty(ideTabRubr)) { return ideTabRubr; }

            // Nome da empresa + final CNPJ
            else if (debugMode) { return nomeEmpresa.Split(' ')[0].Substring(0, 6).ToUpper() + cnpj.Substring(cnpj.Length - 2, 2); }
            else { throw new Exception("Informar um código único - ideTabRubr"); }*/
         }

         public static string tpRubr(string tpRubr)
         {

            // Conversão do tipo de rubrica
            switch (tpRubr.Substring(0, 1))
            {
               case "0": return "1";
               case "3": return "1";
               case "1": return "2";
               case "9": return "3";
               default: return null;
            }
         }

         public static string aaaa_mm(string iniValid)
         {
            try 
            {
               if (iniValid.Length == 4) // mês 13 retorna apenas o ano
                  return iniValid;
               else 
                  return iniValid.Substring(0, 4) + "-" + iniValid.Substring(4, 2); 
            } 
            catch 
            { 
               return null; 
            }
         }
         public static string aaaa_mm_dd(string data)
         {
            try { return data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2); } catch { return ""; }
         }
            public static string getHashSHA1(string input)
         {
            byte[] hash = SHA1.Create().ComputeHash(Encoding.Unicode.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
         }
      }

      public class sLote
      {

         public sLote(string nomeEvento, string nomeEmpresa, string descricaoEvento, string id_arquivo)
         {
            _nomeEvento = nomeEvento;
            _nomeEmpresa = nomeEmpresa;
            _descricaoEvento = descricaoEvento;
            _id_arquivo = id_arquivo;
            _bIsParcial = true;
         }

         public sLote(

             string nomeEmpresa,
             string nomeEvento,
             string descricaoEvento,

             string id_empresa,
             string id_cliente,
             string id_funcionario,

             sIdeEmpTrans ideTransmissor,
             sIdeEmpTrans ideEmpregador,

             List<sEvento> eventos,

             string id_arquivo,
             List<Tuple<string, string, string, string, string>> lId_enviado,
             X509Certificate2 certificado)
         {

            _nomeEmpresa = nomeEmpresa;
            _nomeEvento = nomeEvento;
            _descricaoEvento = descricaoEvento;

            _id_empresa = id_empresa;
            _id_cliente = id_cliente;
            _id_funcionario = id_funcionario;

            _ideTransmissor = ideTransmissor;
            _ideEmpregador = ideEmpregador;

            _eventos = eventos;

            _id_arquivo = id_arquivo;
            _bIsParcial = false;
            _lId_enviado = lId_enviado;

            _certificado = certificado;
         }

         string _nomeEmpresa, _nomeEvento, _descricaoEvento, _id_empresa, _id_cliente, _id_funcionario, _id_arquivo;
         bool _bIsParcial;
         List<Tuple<string, string, string, string, string>> _lId_enviado;
         sIdeEmpTrans _ideTransmissor, _ideEmpregador;
         X509Certificate2 _certificado;

         public string nomeEmpresa { get { return _nomeEmpresa; } }
         public string nomeEvento { get { return _nomeEvento; } }
         public string descricaoEvento { get { return _descricaoEvento; } }
         public string id_empresa { get { return _id_empresa; } }
         public string id_cliente { get { return _id_cliente; } }
         public string id_funcionario { get { return _id_funcionario; } }

         public sIdeEmpTrans ideTransmissor { get { return _ideTransmissor; } }
         public sIdeEmpTrans ideEmpregador { get { return _ideEmpregador; } }

         List<sEvento> _eventos;
         public List<sEvento> eventos { get { return _eventos; } }
         public List<Tuple<string, string, string, string, string>> lId_enviado { get { return _lId_enviado; } }
         public string id_arquivo { get { return _id_arquivo; } }
         public bool isParcial { get { return _bIsParcial; } }
         public X509Certificate2 certificado { get { return _certificado; } }
      }
   }
}
