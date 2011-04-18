﻿using System;
using System.Globalization;

using CloudObserver.Clients.Auxiliaries.Tools;

namespace CloudObserver.Clients.Auxiliaries.Formats.Audio
{
    /// <summary>
    /// A managed representation of the multimedia WAVEFORMATEX structure
    /// declared in mmreg.h.
    /// </summary>
    /// <remarks>
    /// This was designed for usage in an environment where PInvokes are not
    /// allowed.
    /// </remarks>
    public class WaveFormatEx
    {
        #region Fields
        /// <summary>
        /// Gets or sets the audio format type. A complete list of format tags can be
        /// found in the Mmreg.h header file.
        /// </summary>
        /// <remarks>
        /// Silverlight 2 supports:
        /// WMA 7,8,9
        /// WMA 10 Pro
        /// Mp3
        /// WAVE_FORMAT_MPEGLAYER3 = 0x0055
        /// </remarks>
        public short FormatTag { get; set; }

        /// <summary>
        /// Gets or sets the number of channels in the data. 
        /// Mono            1
        /// Stereo          2
        /// Dual            2 (2 Mono channels)
        /// </summary>
        /// <remarks>
        /// Silverlight 2 only supports stereo output and folds down higher
        /// numbers of channels to stereo.
        /// </remarks>
        public short Channels { get; set; }

        /// <summary>
        /// Gets or sets the sampling rate in hertz (samples per second)
        /// </summary>
        public int SamplesPerSec { get; set; }

        /// <summary>
        /// Gets or sets the average data-transfer rate, in bytes per second, for the format.
        /// </summary>
        public int AverageBytesPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of a unit of data for the given format in Bytes.
        /// </summary>
        public short BlockAlign { get; set; }

        /// <summary>
        /// Gets or sets the number of bits in a single sample of the format's data.
        /// </summary>
        public short BitsPerSample { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes of any extra format data added to the end of the
        /// WAVEFORMATEX structure.
        /// </summary>
        public short Size { get; set; }
        
        public byte[] ext { get; set; }
        #endregion

        #region Constants
        public const uint SizeOf = 18;
        #endregion

        public WaveFormatEx() { }

        public WaveFormatEx(int samplesPerSec, short bitsPerSample, short channels)
        {
            FormatTag = (short)WaveFormats.Pcm;
            Channels = channels;
            SamplesPerSec = samplesPerSec;
            BitsPerSample = bitsPerSample;
            Size = 0;
            ext = null;

            BlockAlign = (short)(Channels * (BitsPerSample / 8));
            AverageBytesPerSecond = SamplesPerSec * BlockAlign;
        }

        /// <summary>
        /// Set the data from a byte array (usually read from a file).
        /// </summary>
        /// <param name="byteArray"></param>
        public WaveFormatEx(byte[] byteArray)
        {
            if ((byteArray.Length + 2) < SizeOf)
            {
                throw new ArgumentException("Byte array is too small");
            }

            FormatTag = BitConverter.ToInt16(byteArray, 0);
            Channels = BitConverter.ToInt16(byteArray, 2);
            SamplesPerSec = BitConverter.ToInt32(byteArray, 4);
            AverageBytesPerSecond = BitConverter.ToInt32(byteArray, 8);
            BlockAlign = BitConverter.ToInt16(byteArray, 12);
            BitsPerSample = BitConverter.ToInt16(byteArray, 14);
            if (byteArray.Length >= SizeOf)
            {
                Size = BitConverter.ToInt16(byteArray, 16);
            }
            else
            {
                Size = 0;
            }

            if (byteArray.Length > WaveFormatEx.SizeOf)
            {
                ext = new byte[byteArray.Length - WaveFormatEx.SizeOf];
                Array.Copy(byteArray, (int)WaveFormatEx.SizeOf, ext, 0, ext.Length);
            }
            else
            {
                ext = null;
            }
        }

        /// <summary>
        /// Returns a string representing the structure in little-endian 
        /// hexadecimal format.
        /// </summary>
        /// <remarks>
        /// The string generated here is intended to be passed as 
        /// CodecPrivateData for Silverlight 2's MediaStreamSource
        /// </remarks>
        /// <returns>
        /// A string representing the structure in little-endia hexadecimal
        /// format.
        /// </returns>
        public string ToHexString()
        {
            string s = "";
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X4}", FormatTag)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X4}", Channels)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X8}", SamplesPerSec)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X8}", AverageBytesPerSecond)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X4}", BlockAlign)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X4}", BitsPerSample)).ToLittleEndian();
            s += (string.Format(CultureInfo.InvariantCulture, "{0:X4}", Size)).ToLittleEndian();
            return s;
        }

        public byte[] ToByteArray()
        {
            int length = (int)SizeOf - 2;
            if (Size != 0) length += Size + 2;
            byte[] byteArray = new byte[length];
            BitConverter.GetBytes(FormatTag).CopyTo(byteArray, 0);
            BitConverter.GetBytes(Channels).CopyTo(byteArray, 2);
            BitConverter.GetBytes(SamplesPerSec).CopyTo(byteArray, 4);
            BitConverter.GetBytes(AverageBytesPerSecond).CopyTo(byteArray, 8);
            BitConverter.GetBytes(BlockAlign).CopyTo(byteArray, 12);
            BitConverter.GetBytes(BitsPerSample).CopyTo(byteArray, 14);
            if (Size != 0)
            {
                BitConverter.GetBytes(Size).CopyTo(byteArray, 16);
                ext.CopyTo(byteArray, (int)SizeOf);
            }
            return byteArray;
        }        

        /// <summary>
        /// Returns a string representing all of the fields in the object.
        /// </summary>
        /// <returns>
        /// A string representing all of the fields in the object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "WAVEFORMATEX FormatTag: {0}, Channels: {1}, SamplesPerSec: {2}, AvgBytesPerSec: {3}, BlockAlign: {4}, BitsPerSample: {5}, Size: {6} ",
                this.FormatTag,
                this.Channels,
                this.SamplesPerSec,
                this.AverageBytesPerSecond,
                this.BlockAlign,
                this.BitsPerSample,
                this.Size);
        }

        /// <summary>
        /// Calculate the duration of audio based on the size of the buffer.
        /// </summary>
        /// <param name="cbAudioDataSize"></param>
        /// <returns></returns>
        public Int64 AudioDurationFromBufferSize(UInt32 cbAudioDataSize)
        {
            if (AverageBytesPerSecond == 0)
            {
                return 0;
            }

            return (Int64)(cbAudioDataSize * 10000000 / AverageBytesPerSecond);
        }

        /// <summary>
        /// Calculate the buffer size necessary for a duration of audio.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public Int64 BufferSizeFromAudioDuration(Int64 duration)
        {
            Int64 size = duration * AverageBytesPerSecond / 10000000;
            UInt32 remainder = (UInt32)(size % BlockAlign);
            if (remainder != 0)
            {
                size += BlockAlign - remainder;
            }

            return size;
        }
    }
}