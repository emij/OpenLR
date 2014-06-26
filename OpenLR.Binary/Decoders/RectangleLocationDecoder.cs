﻿using OpenLR.Binary.Data;
using OpenLR.Locations;

namespace OpenLR.Binary.Decoders
{
    /// <summary>
    /// A decoder that decodes binary data into a rectangle location.
    /// </summary>
    public class RectangleLocationDecoder : BinaryLocationDecoder<RectangleLocation>
    {
        /// <summary>
        /// Decodes the given data into a location reference.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override RectangleLocation Decode(byte[] data)
        {
            var rectangleLocation = new RectangleLocation();
            rectangleLocation.LowerLeft = CoordinateConverter.Decode(data, 1);
            rectangleLocation.UpperRight = CoordinateConverter.DecodeRelative(rectangleLocation.LowerLeft, data, 7);
            return rectangleLocation;
        }

        /// <summary>
        /// Returns true if the given data can be decoded by this decoder.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override bool CanDecode(byte[] data)
        {
            // decode the header first.
            var header = HeaderConvertor.Decode(data, 0);

            // check header info.
            if (!header.ArF1 ||
                header.IsPoint ||
                header.ArF0 ||
                header.HasAttributes)
            { // header is incorrect.
                return false;
            }

            return data != null && (data.Length == 11 || data.Length == 13);
        }
    }
}