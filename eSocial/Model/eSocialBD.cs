using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSocial.Model
{

   public enum enModo { envioWS = 0, consultaWS = 1, consultaEventoWS = 2, eventosPendentesConsulta = 3, eventosPendentesEnvio = 4, inserirLoteParcial = 6, inserirEventosEnviados = 61 }

   public enum enParams
   {
      id_arquivo, id_empresa, id_cliente, id_funcionario, evento, id_esocial,
      evtDupl, protocolo, recibo, dhRecepcao, proxConsulta,
      cdResposta, descResposta, ocorrencia_tipo, ocorrencia_localizacao,
      totTipo, xmlTot, xmlContrato, xmlEnvio, xmlRetorno, id_evento,
      modoOcorrencia
   };

   public class eSocialBD
   {

      public eSocialBD(enModo? modo = null, Dictionary<enParams, string> cmdParams = null)
      {
         if (cmdParams != null) { lParams = cmdParams; };
         if (modo != null) { _modo = modo; }
      }

      Dictionary<enParams, string> lParams = new Dictionary<enParams, string>();

      fSQL SQL = new fSQL();
      StringBuilder _sSQL = new StringBuilder();

      enModo? _modo;

      public void setModo(enModo? modo) { _modo = modo; }

      public void setParam(enParams param, string value)
      {
         if (value == null) { return; }
         if (lParams.ContainsKey(param)) { lParams.Remove(param); }
         lParams.Add(param, value);
      }

      public void clearParams() { lParams.Clear(); _sSQL.Clear(); }

      public DataSet exec(bool clearParams = true)
      {

         string sSQL;

         _sSQL.Append(ConfigurationManager.AppSettings["SQLProc"]).Append(" ");

         foreach (var p in lParams.Where(x => !x.Value.Trim().ToString().Equals("")))
         {
            try
            {
               if (p.Value.All(char.IsDigit))
               {
                  _sSQL.Append("@" + Enum.GetName(typeof(enParams), p.Key) + "=" + p.Value).Append(", ");
               }
               else
               {
                  _sSQL.Append("@" + Enum.GetName(typeof(enParams), p.Key) + "='" + p.Value.Replace("'", "''")).Append("', ");
               }
            }
            catch { }
         }

         // _sSQL.Append("@desenv=1,");
         _sSQL.Append("@server="+ ConfigurationManager.AppSettings["evento_server"].ToString()+",");
         _sSQL.Append("@modo=" + _modo.GetHashCode());
         sSQL = _sSQL.ToString();

         if (clearParams) { lParams.Clear(); _sSQL.Clear(); }

         return SQL.exec(sSQL);
      }

      public Dictionary<enParams, string> getParams() { return new Dictionary<enParams, string>(lParams); }
   }
}
