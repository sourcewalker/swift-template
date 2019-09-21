using umbraco.businesslogic;
using umbraco.interfaces;

namespace $safeprojectname$.App_Plugins.Tools.Section
{
    [Application(
        alias: "tools",
        name: "Tools",
        icon: "icon-wrench",
        sortOrder: 15)]
    public class ToolsSection : IApplication { }
}