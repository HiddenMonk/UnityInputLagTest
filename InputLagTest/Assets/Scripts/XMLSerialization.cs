using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class XMLSerialization
{
	public static void ToXMLFile<T>(T xmlObj, string filePath)
	{
		if(xmlObj == null) return;

		File.WriteAllText(filePath, ToXMLString<T>(xmlObj));
	}
		
	public static T FileToObject<T>(string filePath) where T : new()
	{
		if(!File.Exists(filePath)) throw new FileNotFoundException();
		
		return StringToObject<T>(File.ReadAllText(filePath));
	}
		
	public static string ToXMLString<T>(T xmlObj)
	{
		if(xmlObj == null) return String.Empty;

		XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
		using(StringWriter stringWriter = new StringWriter())
		{
            using(XmlWriter writer = XmlWriter.Create(stringWriter, new XmlWriterSettings(){Indent = true}))
            {
                xmlserializer.Serialize(writer, xmlObj);
                return stringWriter.ToString();
            }
		}
	}
		
	public static T StringToObject<T>(string xml) where T : new()
	{
		if(String.IsNullOrEmpty(xml)) return new T();

		XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
		using(StringReader stringReader = new StringReader(xml))
		{
            using(XmlReader reader = XmlReader.Create(stringReader))
            {
                if(xmlserializer.CanDeserialize(reader))
				{
					return (T)(xmlserializer.Deserialize(reader));
				}else{
					return new T();
				}
            }
		}
	}
}