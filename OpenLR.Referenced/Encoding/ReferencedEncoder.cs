﻿using OpenLR.Locations;
using OpenLR.Model;
using OpenLR.Referenced;
using OpenLR.Referenced.Encoding;
using OsmSharp.Collections.Tags;
using OsmSharp.Math.Geo.Simple;
using OsmSharp.Routing.Graph;
using OsmSharp.Routing.Graph.Routing;
using OsmSharp.Routing.Osm.Graphs;
using OsmSharp.Units.Angle;
using System;

namespace OpenLR.Referenced.Encoding
{
    /// <summary>
    /// Represents a dynamic graph encoder: Encodes a reference OpenLR location into an unreferenced location.
    /// </summary>
    public abstract class ReferencedEncoder<TReferencedLocation, TLocation> : ReferencedLocationEncoder<TReferencedLocation, TLocation>
        where TReferencedLocation : ReferencedLocation
        where TLocation : ILocation
    {
        /// <summary>
        /// Holds the main encoder.
        /// </summary>
        private ReferencedEncoderBase _mainEncoder;

        /// <summary>
        /// Creates a new dynamic graph encoder.
        /// </summary>
        /// <param name="mainEncoder"></param>
        /// <param name="rawEncoder"></param>
        public ReferencedEncoder(ReferencedEncoderBase mainEncoder, OpenLR.Encoding.LocationEncoder<TLocation> rawEncoder)
            : base(rawEncoder)
        {
            _mainEncoder = mainEncoder;
        }

        /// <summary>
        /// Returns the main encoder.
        /// </summary>
        public ReferencedEncoderBase MainEncoder
        {
            get
            {
                return _mainEncoder;
            }
        }

        /// <summary>
        /// Returns the coordinate of the given vertex.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        protected Coordinate GetVertexLocation(long vertex)
        {
            return _mainEncoder.GetVertexLocation(vertex);
        }

        /// <summary>
        /// Returns the tags for the given tags id.
        /// </summary>
        /// <param name="tagsId"></param>
        /// <returns></returns>
        protected TagsCollectionBase GetTags(uint tagsId)
        {
            return _mainEncoder.GetTags(tagsId);
        }

        /// <summary>
        /// Tries to match the given tags and figure out a corresponding frc and fow.
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="frc"></param>
        /// <param name="fow"></param>
        /// <returns>False if no matching was found.</returns>
        protected bool TryMatching(TagsCollectionBase tags, out FunctionalRoadClass frc, out FormOfWay fow)
        {
            return _mainEncoder.TryMatching(tags, out frc, out fow);
        }

        /// <summary>
        /// Returns a value if a oneway restriction is found.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>null: no restrictions, true: forward restriction, false: backward restriction.</returns>
        /// <returns></returns>
        protected bool? IsOneway(TagsCollectionBase tags)
        {
            return _mainEncoder.IsOneway(tags);
        }

        ///// <summary>
        ///// Returns the bearing calculate between two given vertices along the given edge.
        ///// </summary>
        ///// <param name="vertexFrom"></param>
        ///// <param name="edge"></param>
        ///// <param name="edgeShape"></param>
        ///// <param name="vertexTo"></param>
        ///// <param name="forward">When true the edge is forward relative to the vertices, false the edge is backward.</param>
        ///// <returns></returns>
        //protected Degree GetBearing(long vertexFrom, LiveEdge edge, GeoCoordinateSimple[] edgeShape, long vertexTo, bool forward)
        //{
        //    return _mainEncoder.GetBearing(vertexFrom, edge, edgeShape, vertexTo, forward);
        //}
    }
}