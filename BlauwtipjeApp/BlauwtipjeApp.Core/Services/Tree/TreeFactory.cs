using System;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml.Impl;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Impl;

namespace BlauwtipjeApp.Core.Services.Tree
{
    public class TreeFactory<TResult> : ITreeFactory<TResult> where TResult : Result
    {
        private ILocalResourceDAO _dao;
        private XmlTreeConverter<TResult> converter;
        private TreeDecorator<TResult> decorator;

        public TreeFactory(ILocalResourceDAO dao)
        {
            this._dao = dao;
            this.converter = new XmlTreeConverter<TResult>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());
            this.decorator = new TreeDecorator<TResult>(dao);
        }

        /// <summary>
        /// Builds the tree using a xml file at the path specified in <see cref="Settings.XmlFileName"/>
        /// </summary>
        /// <returns>
        /// <see cref="DeterminationTree{TResult}"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">Cannot find xml file: " + Settings.XmlFileName</exception>
        public DeterminationTree<TResult> BuildTree()
        {
            var xml = _dao.Get(Settings.XmlFileName)?.GetContentAsString();
            if (xml == null) return null;
            var tree = converter.FromXml(xml);
            decorator.DecorateTree(tree);
            return tree;
        }
    }
}
