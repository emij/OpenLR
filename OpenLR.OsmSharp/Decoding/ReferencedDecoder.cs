﻿using OpenLR.Locations;
using OpenLR.Model;
using OpenLR.OsmSharp.Decoding.Candidates;
using OpenLR.OsmSharp.Locations;
using OpenLR.OsmSharp.Router;
using OpenLR.Referenced;
using OpenLR.Referenced.Decoding;
using OsmSharp.Collections.Tags;
using OsmSharp.Math.Geo;
using OsmSharp.Routing;
using OsmSharp.Routing.Graph;
using OsmSharp.Routing.Graph.Router;
using OsmSharp.Routing.Osm.Interpreter;
using OsmSharp.Units.Angle;
using OsmSharp.Units.Distance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenLR.OsmSharp.Decoding
{
    /// <summary>
    /// Represents a dynamic graph decoder: Decodes a raw OpenLR location into a location referenced to a dynamic graph.
    /// </summary>
    public abstract class ReferencedDecoder<TReferencedLocation, TLocation, TEdge> : ReferencedLocationDecoder<TReferencedLocation, TLocation>
        where TEdge : IDynamicGraphEdgeData
        where TReferencedLocation : ReferencedLocation
        where TLocation : ILocation
    {
        /// <summary>
        /// Holds a dynamic graph.
        /// </summary>
        private IBasicRouterDataSource<TEdge> _graph;

        /// <summary>
        /// Holds the main decoder.
        /// </summary>
        private ReferencedDecoderBase<TEdge> _mainDecoder;

        /// <summary>
        /// Holds a basic router.
        /// </summary>
        private BasicRouter _router;

        /// <summary>
        /// Creates a new dynamic graph decoder.
        /// </summary>
        /// <param name="mainDecoder"></param>
        /// <param name="rawDecoder"></param>
        /// <param name="graph"></param>
        /// <param name="router"></param>
        public ReferencedDecoder(ReferencedDecoderBase<TEdge> mainDecoder, OpenLR.Decoding.LocationDecoder<TLocation> rawDecoder, IBasicRouterDataSource<TEdge> graph,
            BasicRouter router)
            : base(rawDecoder)
        {
            _mainDecoder = mainDecoder;
            _graph = graph;
            _router = router;
        }

        /// <summary>
        /// Decodes an OpenLR-encoded unreferenced raw OpenLR location into a referenced Location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public override abstract TReferencedLocation Decode(TLocation location);

        /// <summary>
        /// Returns the max vertex distance.
        /// </summary>
        protected Meter MaxVertexDistance
        {
            get
            {
                return _mainDecoder.MaxVertexDistance;
            }
        }

        /// <summary>
        /// Finds all candidate vertex/edge pairs for a given location reference point.
        /// </summary>
        /// <param name="lrp"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        protected SortedSet<CandidateVertexEdge<TEdge>> FindCandidatesFor(LocationReferencePoint lrp, bool forward)
        {
            return _mainDecoder.FindCandidatesFor(lrp, forward);
        }

        /// <summary>
        /// Finds all candidate vertex/edge pairs for a given location reference point.
        /// </summary>
        /// <param name="lrp"></param>
        /// <param name="forward"></param>
        /// <param name="maxVertexDistance"></param>
        /// <returns></returns>
        protected SortedSet<CandidateVertexEdge<TEdge>> FindCandidatesFor(LocationReferencePoint lrp, bool forward, Meter maxVertexDistance)
        {
            return _mainDecoder.FindCandidatesFor(lrp, forward, maxVertexDistance);
        }

        /// <summary>
        /// Finds the closest candidate by finding the closest point on the network.
        /// </summary>
        /// <param name="locationReferencePoint"></param>
        /// <returns></returns>
        protected CandidateVertexEdge<TEdge> FindClosestCandidateFor(LocationReferencePoint locationReferencePoint)
        {
            return _mainDecoder.FindClosestCandidateFor(locationReferencePoint);
        }

        /// <summary>
        /// Finds candidate vertices for a location reference point.
        /// </summary>
        /// <param name="lrp"></param>
        /// <returns></returns>
        protected IEnumerable<ReferencedDecoderBase<TEdge>.CandidateVertex> FindCandidateVerticesFor(LocationReferencePoint lrp)
        {
            return _mainDecoder.FindCandidateVerticesFor(lrp);
        }

        /// <summary>
        /// Finds candidate edges for a vertex matching a given fow and frc.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="forward"></param>
        /// <param name="fow"></param>
        /// <param name="frc"></param>
        /// <returns></returns>
        protected IEnumerable<ReferencedDecoderBase<TEdge>.CandidateEdge> FindCandidateEdgesFor(uint vertex, bool forward, FormOfWay fow, FunctionalRoadClass frc)
        {
            return _mainDecoder.FindCandidateEdgesFor(vertex, forward, fow, frc);
        }

        /// <summary>
        /// Calculates a match between the tags collection and the properties of the OpenLR location reference.
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="fow"></param>
        /// <param name="frc"></param>
        /// <returns></returns>
        protected float MatchArc(TagsCollectionBase tags, FormOfWay fow, FunctionalRoadClass frc)
        {
            return _mainDecoder.MatchArc(tags, fow, frc);
        }

        /// <summary>
        /// Calculates a route between the two given vertices.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="minimum">The minimum FRC.</param>
        /// <returns></returns>
        protected CandidateRoute<TEdge> FindCandidateRoute(CandidateVertexEdge<TEdge> from, CandidateVertexEdge<TEdge> to, FunctionalRoadClass minimum)
        {
            return _mainDecoder.FindCandiateRoute(from, to, minimum);
        }

        /// <summary>
        /// Returns the coordinate of the given vertex.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        protected Coordinate GetCoordinate(long vertex)
        {
            return _mainDecoder.GetCoordinate(vertex);
        }

        /// <summary>
        /// Returns the list of coordinates the represent the given route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        protected List<GeoCoordinate> GetCoordinates(ReferencedLine<TEdge> route)
        {
            return _mainDecoder.GetCoordinates(route);
        }

        /// <summary>
        /// Returns the distance for the given route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        protected Meter GetDistance(ReferencedLine<TEdge> route)
        {
            return _mainDecoder.GetDistance(route);
        }

        /// <summary>
        /// Returns a value if a oneway restriction is found.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>null: no restrictions, true: forward restriction, false: backward restriction.</returns>
        /// <returns></returns>
        protected bool? IsOneway(TagsCollectionBase tags)
        {
            return _mainDecoder.IsOneway(tags);
        }

        /// <summary>
        /// Returns the bearing calculate between two given vertices along the given edge.
        /// </summary>
        /// <param name="vertexFrom"></param>
        /// <param name="edge"></param>
        /// <param name="vertexTo"></param>
        /// <param name="forward">When true the edge is forward relative to the vertices, false the edge is backward.</param>
        /// <returns></returns>
        protected Degree GetBearing(long vertexFrom, TEdge edge, long vertexTo, bool forward)
        {
            return _mainDecoder.GetBearing(vertexFrom, edge, vertexTo, forward);
        }
    }
}