using System;
using System.Net.Http.Formatting;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace $safeprojectname$.Controllers.Backoffice
{
    [Tree(appAlias: "tools",
        alias: "instantwin",
        title: "Instant Win",
        iconClosed: "icon-logout",
        iconOpen: "icon-arrow-right",
        initialize: true)]
    [PluginController("Tools")]
    public class InstantWinTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {

            if (id == Constants.System.Root.ToInvariantString())
            {
                var nodes = new TreeNodeCollection();
                //var productImportNode = CreateTreeNode("1", Constants.System.Root.ToInvariantString(), 
                //                                  queryStrings, "Products", "icon-shipping");
                //productImportNode.NodeType = string.Empty;
                //productImportNode.HasChildren = false;
                //nodes.Add(productImportNode);
                return nodes;
            }

            throw new NotSupportedException();

        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {

            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                //menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias));
            }
            else
            {
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias));
            }

            return menu;

        }
    }
}