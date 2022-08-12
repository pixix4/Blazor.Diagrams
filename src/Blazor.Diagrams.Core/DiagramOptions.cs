﻿using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Blazor.Diagrams.Core
{
    public class DiagramOptions
    {
        [Description("The grid size (grid-based snaping")]
        public int? GridSize { get; set; }
        [Description("Whether to allow users to select multiple nodes at once using CTRL or not")]
        public bool AllowMultiSelection { get; set; } = true;
        [Description("Whether to allow panning or not")]
        public bool AllowPanning { get; set; } = true;
        [Description("Only render visible nodes")]
        public bool EnableVirtualization { get; set; } = true;
        [Description("Links layer order")]
        public int LinksLayerOrder { get; set; } = 0;
        [Description("Nodes layer order")]
        public int NodesLayerOrder { get; set; } = 0;

        public DiagramZoomOptions Zoom { get; set; } = new DiagramZoomOptions();
        public DiagramLinkOptions Links { get; set; } = new DiagramLinkOptions();
        public DiagramGroupOptions Groups { get; set; } = new DiagramGroupOptions();
        public DiagramConstraintsOptions Constraints { get; set; } = new DiagramConstraintsOptions();
    }

    /// <summary>
    /// All the options regarding links.
    /// </summary>
    public class DiagramLinkOptions
    {
        [Description("The default color for links")]
        public string DefaultColor { get; set; } = "black";
        [Description("The default color for selected links")]
        public string DefaultSelectedColor { get; set; } = "rgb(110, 159, 212)";
        [Description("Default Router for links")]
        public Router DefaultRouter { get; set; } = Routers.Normal;
        [Description("Default PathGenerator for links")]
        public PathGenerator DefaultPathGenerator { get; set; } = PathGenerators.Smooth;
        [Description("Whether to enable link snapping")]
        public bool EnableSnapping { get; set; } = false;
        [Description("Link snapping radius")]
        public double SnappingRadius { get; set; } = 50;
        [Description("Link model factory")]
        public LinkFactory Factory { get; set; } = (diagram, sourcePort) => new LinkModel(sourcePort);
    }

    /// <summary>
    /// All the options regarding zooming.
    /// </summary>
    public class DiagramZoomOptions
    {
        [Description("Whether to allow zooming or not")]
        public bool Enabled { get; set; } = true;
        [Description("Whether to inverse the zoom direction or not")]
        public bool Inverse { get; set; }
        [Description("Minimum value allowed")]
        public double Minimum { get { return _minimum; } set { SetMinimum(value); } }
        private double _minimum = 0.1;
        [Description("Maximum value allowed")]
        public double Maximum { get; set; } = 2;
        [Description("Zoom Scale Factor. Should be between 1.01 and 2.  Default is 1.05.")]
        public double ScaleFactor { get; set; } = 1.05;

        private void SetMinimum(double minValue)
        {
            if (minValue <= 0)
                throw new ArgumentException($"(Zoom Options) =>{nameof(Minimum)} cannot be equal or lower than 0");
            _minimum = minValue;
        }
    }

    /// <summary>
    /// All the options regarding groups.
    /// </summary>
    public class DiagramGroupOptions
    {
        [Description("Whether to allow users to group/ungroup nodes")]
        public bool Enabled { get; set; }
        [Description("Group model factory")]
        public GroupFactory Factory { get; set; } = (diagram, children) => new GroupModel(children);
    }

    /// <summary>
    /// All the options regarding diagram constraints, such as deciding whether to delete a node or not.
    /// </summary>
    public class DiagramConstraintsOptions
    {
        [Description("Decide if a node can/should be deleted")]
        public Func<NodeModel, ValueTask<bool>> ShouldDeleteNode { get; set; } = _ => ValueTask.FromResult(true);
        [Description("Decide if a link can/should be deleted")]
        public Func<BaseLinkModel, ValueTask<bool>> ShouldDeleteLink { get; set; } = _ => ValueTask.FromResult(true);
        [Description("Decide if a group can/should be deleted")]
        public Func<GroupModel, ValueTask<bool>> ShouldDeleteGroup { get; set; } = _ => ValueTask.FromResult(true);
    }
}
