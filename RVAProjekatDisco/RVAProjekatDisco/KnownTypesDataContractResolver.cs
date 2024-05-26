using Common.Model;
using System;
using System.Runtime.Serialization;
using System.Xml;

public class KnownTypesDataContractResolver : DataContractResolver
{
    public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
    {
        if (typeName == "PesmaMP3" && typeNamespace == "http://schemas.datacontract.org/2004/07/Common.Model")
        {
            return typeof(PesmaMP3);
        }
        return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
    }

    public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
    {
        if (type == typeof(PesmaMP3))
        {
            typeName = new XmlDictionaryString(XmlDictionary.Empty, "PesmaMP3", 0);
            typeNamespace = new XmlDictionaryString(XmlDictionary.Empty, "http://schemas.datacontract.org/2004/07/Common.Model", 0);
            return true;
        }
        return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
    }
}
