using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Deployment.Internal.CodeSigning;
using System.Collections.Generic;
using System.Linq;

namespace eSocial.Controller {

   public static class x509 {

      public static XElement signXMLSHA256(XElement xml, X509Certificate2 cert, string uri = "") {

         CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
         
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(xml.CreateReader());

         XmlElement xmlToSign = xmlDoc.DocumentElement;

         SignedXml signedXML = new SignedXml(xmlToSign);
         signedXML.SigningKey = cert.GetRSAPrivateKey();

         signedXML.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

         Reference reference = new Reference();

         if (uri.Equals("")) { reference.Uri = ""; }
         else {
            foreach (XmlAttribute _att in xmlToSign.GetElementsByTagName(uri).Item(0).Attributes) {
               if (_att.Name == "Id") { reference.Uri = "#" + _att.InnerText; }
            }
         }

         reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
         reference.AddTransform(new XmlDsigC14NTransform());

         reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

         signedXML.AddReference(reference);

         KeyInfo keyInfo = new KeyInfo();
         keyInfo.AddClause(new KeyInfoX509Data(cert));
         signedXML.KeyInfo = keyInfo;

         signedXML.ComputeSignature();

         xmlToSign.AppendChild(signedXML.GetXml());

         return XElement.Parse(xmlToSign.OuterXml);
      }

      public static XElement signXMLSHA1(XElement xml, X509Certificate2 cert, string uri = "") {

         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(xml.CreateReader());

         XmlElement xmlToSign = xmlDoc.DocumentElement;

         SignedXml signedXML = new SignedXml(xmlToSign);
         signedXML.SigningKey = cert.PrivateKey;

         Reference reference = new Reference();

         if (!uri.Equals("")) {
            foreach (XmlAttribute _att in xmlToSign.GetElementsByTagName(uri).Item(0).Attributes) {
               if (_att.Name == "Id") { reference.Uri = "#" + _att.InnerText; }
            }
         }

         reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
         reference.AddTransform(new XmlDsigC14NTransform());

         signedXML.AddReference(reference);

         KeyInfo keyInfo = new KeyInfo();
         keyInfo.AddClause(new KeyInfoX509Data(cert));
         signedXML.KeyInfo = keyInfo;

         signedXML.ComputeSignature();

         xmlToSign.AppendChild(signedXML.GetXml());

         return XElement.Parse(xmlToSign.OuterXml);
      }
   }
}