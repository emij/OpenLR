﻿using OsmSharp.Math.Geo;
using OsmSharp.Routing.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLR.OsmSharp.Decoding.Candidates
{
    /// <summary>
    /// Represents a candidate vertex/edge pair and associated score.
    /// </summary>
    public class CandidateVertexEdge<TEdge>
        where TEdge : IDynamicGraphEdgeData
    {
        /// <summary>
        /// The combined score of vertex and edge.
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// Gets or sets the candidate vertex.
        /// </summary>
        public uint Vertex { get; set; }

        /// <summary>
        /// Gets or sets the candidate vertex coordinate.
        /// </summary>
        public GeoCoordinate VertexCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the candidate edge.
        /// </summary>
        public TEdge Edge { get; set; }

        /// <summary>
        /// Gets or sets the vertex this edge leads to.
        /// </summary>
        public uint TargetVertex { get; set; }

        /// <summary>
        /// Gets or sets the coordinate of the vertex this edge leads to.
        /// </summary>
        public GeoCoordinate TargetVertexCoordinate { get; set; }

        /// <summary>
        /// Determines whether this object is equal to the given object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = (obj as CandidateVertexEdge<TEdge>);
            return other != null && other.Vertex == this.Vertex && other.TargetVertex == this.TargetVertex && other.Edge.Equals(this.Edge) && other.Score == this.Score;
        }

        /// <summary>
        /// Serves as a hashfunction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Score.GetHashCode() ^
                this.Edge.GetHashCode() ^
                this.Vertex.GetHashCode() ^
                this.TargetVertex.GetHashCode();
        }
    }
}
