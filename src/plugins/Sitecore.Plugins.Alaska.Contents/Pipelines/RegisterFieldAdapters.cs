using Sitecore.Exceptions;
using Sitecore.Pipelines;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using Sitecore.Plugins.Alaska.Contents.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sitecore.Plugins.Alaska.Contents.Pipelines
{
    public class RegisterFieldAdapters
    {
        public void Process(PipelineArgs args)
        { }

        public void SetDefaultAdapter(XmlNode node)
        {
            var adapter = GetAdapterInstance(node.Attributes["type"].Value);
            FieldAdaptersCollection.Current.SetDefaultFieldAdapter(adapter);
        }

        public void AddAdapter(XmlNode node)
        {
            try
            {
                var adapter = GetAdapterInstance(node.Attributes["type"].Value);
                var fieldTypes = GetAdapterFieldTypes(node).ToList();
                fieldTypes.ForEach(x => FieldAdaptersCollection.Current.Add(x, adapter));
            }
            catch (Exception e)
            {
                throw new ConfigurationException($"{nameof(RegisterFieldAdapters)} adapter initialization error -> {node.OuterXml} -> {node.InnerXml}", e);
            }
        }

        private IEnumerable<string> GetAdapterFieldTypes(XmlNode node)
        {
            return node.ChildNodes
                .Cast<XmlNode>()
                .Select(x => x.Attributes["typeKey"].Value)
                .ToList();
        }

        private IFieldAdapter GetAdapterInstance(string type)
        {
            var typeObj = Type.GetType(type);
            if (typeObj == null)
                throw new InvalidOperationException($"Cannot find adapter type {type}");

            return (IFieldAdapter)Activator.CreateInstance(typeObj);
        }
    }
}
