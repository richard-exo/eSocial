using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace eSocial.Model.Eventos.XML {

    public abstract class bEvento_XML : cBase {

        public virtual XElement genSignedXML(X509Certificate2 cert) { return null; }

        private string xmlns = "http://www.esocial.gov.br/schema/evt/";
        public string tagEvento, verLayout, tagInfo;
        string _id;

        public sIdeEvento ideEvento = new sIdeEvento();
        public sIdeEmpTrans ideEmpregador = new sIdeEmpTrans();

        public string id { get { return _id; } set { if (value == null) { return; } _id = value; xml.Elements().ElementAt(0).SetAttributeValue("Id", value); } }

        public bEvento_XML(string tagEvento, string tagInfo = "", string versao = "") {

            this.tagEvento = tagEvento;
            this.tagInfo = tagInfo;
            this.verLayout = (!versao.Equals("") ? versao : ConfigurationManager.AppSettings["vLayoutEventos"]);
            ns = xmlns + tagEvento + "/" + this.verLayout;

            xml = new XElement(ns + "eSocial",

            new XElement(ns + tagEvento,

            // ideEvento
            new XElement(ns + "ideEvento", ""),

            // ideEmpregador
            new XElement(ns + "ideEmpregador", ""),

            // tagInfo
            (!tagInfo.Equals("") ? new XElement(ns + tagInfo, "") : null)));
        }

        public XElement opTag(string tag, object obj) {

            if (obj == null) { return null; }
            else if (string.IsNullOrEmpty(obj.ToString())) { return null; }

            return new XElement(ns + tag, obj);
        }
        public XElement opElement(string tag, object condObj, params object[] objs) {

            if (condObj == null) { return null; }

            else if (string.IsNullOrEmpty(condObj.ToString())) { return null; }

            else if (condObj.GetType().ToString().Equals(typeof(List<XElement>).ToString())) {
                List<XElement> lst = (List<XElement>)condObj;
                if (lst.Count.Equals(0)) { return null; }
            }

            return new XElement(ns + tag,
            from x in objs
            select x);
        }
    }
}
