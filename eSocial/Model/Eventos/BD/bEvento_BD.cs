using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eSocial.Model.Eventos.BD {
   public abstract class bEvento_BD : cBase, IEnumerable<cBase.sEvento> {

      public bEvento_BD(string nomeEvento, string descricao, enTipoEvento tipoEvento) { _nomeEvento = nomeEvento; _descricao = descricao; _tipoEvento = tipoEvento; }

      protected string _nomeEvento, _descricao, _id_evento, _id_arquivo;

      public sIdeEmpTrans ideTransmissor, ideEmpregador;

      protected DataTable tbInfo, tbEventos;
      protected List<sEvento> lEventos;
      protected eSocialBD eSocialBD = new eSocialBD();

      public enTipoEvento _tipoEvento;
      public enTipoEvento tipoEvento { get { return _tipoEvento; } }
      public string nomeEvento { get { return _nomeEvento; } }
      public string descricaoEvento { get { return _descricao; } }
      public bool hasElements { get { return (lEventos == null ? false : !lEventos.Count.Equals(0)); } }

      protected sEvento initEvento(string evtTpAmb, string id_arquivo, string id_evento, string id_empresa, string id_cliente, string id_funcionario) {

         sEvento retEvento = new sEvento();
         gcl = new sGetColLevel();

         var info = (from DataRow t in tbInfo.Rows where t["id_cliente"].ToString().Equals(id_cliente) select t).FirstOrDefault();

         if (info["certificado"].ToString().Equals("")) { throw new Exception("A empresa não possui um certificado cadastrado."); }

         retEvento.nomeEmpresa = info["nmRazao"].ToString();
         retEvento.nomeEvento = nomeEvento;
         retEvento.descricaoEvento = descricaoEvento;

         retEvento.id_empresa = id_empresa;
         retEvento.id_cliente = id_cliente;
         retEvento.id_funcionario = id_funcionario;

         retEvento.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), info["tpInsc_empregador"].ToString());
         retEvento.nrInsc = info["nrInsc_empregador"].ToString();
         retEvento.natJurid = info["natJurid"].ToString();

         retEvento.ideTransmissor.tpInsc = (enTpInsc)Enum.Parse(typeof(enTpInsc), info["tpInsc_transmissor"].ToString());
         retEvento.ideTransmissor.nrInsc = info["nrInsc_transmissor"].ToString();

         retEvento.ideEmpregador.tpInsc = retEvento.tpInsc;
         retEvento.ideEmpregador.nrInsc = validadores.nrInsc(retEvento.tpInsc, retEvento.nrInsc, retEvento.natJurid);

         ideTransmissor = retEvento.ideTransmissor;
         ideEmpregador = retEvento.ideEmpregador;

         retEvento.id = validadores.ID(retEvento.tpInsc.GetHashCode().ToString(), retEvento.nrInsc, retEvento.natJurid);

         retEvento.tpAmb = (enTpAmb)Enum.Parse(typeof(enTpAmb), evtTpAmb);

         retEvento.id_evento = id_evento;
         retEvento.id_arquivo = id_arquivo;

         retEvento.certificado = new X509Certificate2((byte[])info["certificado"], info["senha"].ToString());

         return retEvento;
      }

      public virtual List<sEvento> getEventosPendentes() {

         lEventos = new List<sEvento>();

         eSocialBD.setModo(enModo.eventosPendentesEnvio);
         eSocialBD.setParam(enParams.evento, _nomeEvento);

         DataSet dt = eSocialBD.exec();
         tbInfo = dt.Tables[0];

         try { tbEventos = dt.Tables[1]; } catch { tbEventos = new DataTable(); }

         return null;
      }

      IEnumerator<sEvento> IEnumerable<sEvento>.GetEnumerator() { if (lEventos == null) { getEventosPendentes(); } return lEventos?.GetEnumerator(); }
      IEnumerator IEnumerable.GetEnumerator() { if ((lEventos == null)) { getEventosPendentes(); } getEventosPendentes(); return lEventos?.GetEnumerator(); }

      public sGetColLevel gcl;
      public class sGetColLevel {
         DataRow _row;

         List<string> lColLevel = new List<string>();
         public void setLevel(string level = "", DataRow row = null, bool clear = false) {
            if (clear) { lColLevel.Clear(); }
            if (!level.Equals("")) { lColLevel.Add(level); }
            if (row != null) { _row = row; }
         }
         public string getVal(string element) { return _row[element + getLevel].ToString(); }
         public string getLevel { get { return "_" + string.Join("_", lColLevel.ToArray().Reverse()); } }
         public void clear() { lColLevel.Clear(); }
      }

   }
}
