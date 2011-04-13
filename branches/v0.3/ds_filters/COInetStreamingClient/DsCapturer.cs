﻿using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using DirectShowLib;
using CloudDirectShowLib;



namespace CloudObserverWriterClient
{
    public class DsCapturer
    {
        private IGraphBuilder graph;
        private IMediaControl mediaControl;

        private Guid CLSID_LAMEAudioEncoder    = new Guid("{B8D27088-FF5F-4B7C-98DC-0E91A1696286}"); // lame.ax
        private Guid CLSID_MatroskaMuxer       = new Guid("{1E1299A2-9D42-4F12-8791-D79E376F4143}"); // MatroskaMuxer.ax
        private Guid CLSID_CloudStreamRenderer = new Guid("{37AC047C-BED1-49EF-AB43-BB906A158DD6}"); // cloud_stream_renderer.ax
        public DsCapturer(DsDevice videoInputDevice, DsDevice audioInputDevice, string targetAddress, int targetPort)
        {
            int hr = 0;

            graph = (IGraphBuilder)new FilterGraph();
            ICaptureGraphBuilder2 pBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            hr = pBuilder.SetFiltergraph(graph);
            checkHR(hr, "Can't set filtergraph.");

            
            //  ADDING FILTERS
            
            // add video input device
            IBaseFilter pVideoInputDevice = CreateDeviceFilter(videoInputDevice);
            hr = graph.AddFilter(pVideoInputDevice, "Video Input Device");
            checkHR(hr, "Can't add " + videoInputDevice.Name + " to graph.");

            // add audio input device
            IBaseFilter pAudioInputDevice = CreateDeviceFilter(audioInputDevice);
            hr = graph.AddFilter(pAudioInputDevice, "Audio Input Device");
            checkHR(hr, "Can't add " + audioInputDevice.Name + " to graph.");

            // add x264vfw - H.264/MPEG-4 AVC codec
            IBaseFilter px264vfwH264MPEG4AVCcodec = CreateFilter(@"@device:cm:{33D9A760-90C8-11D0-BD43-00A0C911CE86}\x264", @"x264vfw - H.264/MPEG-4 AVC codec");
            hr = graph.AddFilter(px264vfwH264MPEG4AVCcodec, "x264vfw - H.264/MPEG-4 AVC codec");
            checkHR(hr, "Can't add x264vfw - H.264/MPEG-4 AVC codec to graph.");

            // add LAME Audio Encoder
            IBaseFilter pLAMEAudioEncoder = (IBaseFilter)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_LAMEAudioEncoder));
            hr = graph.AddFilter(pLAMEAudioEncoder, "LAME Audio Encoder");
            checkHR(hr, "Can't add LAME Audio Encoder to graph.");

            // add Matroska Muxer
            IBaseFilter pMatroskaMuxer = (IBaseFilter)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_MatroskaMuxer));
            hr = graph.AddFilter(pMatroskaMuxer, "Matroska Muxer");
            checkHR(hr, "Can't add Matroska Muxer to graph.");

            // add CloudStreamRenderer Filter
            IBaseFilter pCloudStreamRenderer = (IBaseFilter)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_CloudStreamRenderer));
            hr = graph.AddFilter(pCloudStreamRenderer, "Cloud Stream Renderer");
            checkHR(hr,"Can't add Cloud Stream Renderer to graph");

            // set target address, inheriting interface's methods
            ICloudInetControl COInetControl = pCloudStreamRenderer as ICloudInetControl;
            if (COInetControl == null)
                checkHR(unchecked((int)0x80004002), "Can't add ICloudInetControl Interface.");
            COInetControl.SetAddress( targetAddress,targetPort);

            /*// add File writer
            IBaseFilter pFileWriter = (IBaseFilter)new FileWriter();
            hr = graph.AddFilter(pFileWriter, "File Writer");
            checkHR(hr, "Can't add File Writer to graph.");

            // set target file
            IFileSinkFilter pFileWriter_sink = pFileWriter as IFileSinkFilter;
            if (pFileWriter_sink == null)
                checkHR(unchecked((int)0x80004002), "Can't get IFileSinkFilter.");
            hr = pFileWriter_sink.SetFileName(targetFile, null);
            checkHR(hr, "Can't set target file.");*/

            
            //  CONNECTING FILTERS

            // connect Audio Input Device and LAME Audio Encoder
            hr = graph.ConnectDirect(GetFirstOutputPin(pAudioInputDevice), GetPin(pLAMEAudioEncoder, "XForm In"), null);
            checkHR(hr, "Can't connect " + audioInputDevice.Name + " and LAME Audio Encoder.");  
            
            // connect Video Input Device and x264vfw - H.264/MPEG-4 AVC codec
            hr = graph.ConnectDirect(GetFirstOutputPin(pVideoInputDevice), GetPin(px264vfwH264MPEG4AVCcodec, "Input"), null);
            checkHR(hr, "Can't connect " + videoInputDevice.Name + " and x264vfw - H264/MPEG-4 AVC codec.");

            // connect x264vfw - H.264/MPEG-4 AVC codec and Matroska Muxer
            hr = graph.ConnectDirect(GetPin(px264vfwH264MPEG4AVCcodec, "Output"), GetPin(pMatroskaMuxer, "Track 1"), null);
            checkHR(hr, "Can't connect x264vfw - H264/MPEG-4 AVC codec and Matroska Muxer.");

            // connect LAME Audio Encoder and Matroska Muxer
            hr = graph.ConnectDirect(GetPin(pLAMEAudioEncoder, "XForm Out"), GetPin(pMatroskaMuxer, "Track 2"), null);
            checkHR(hr, "Can't connect LAME Audio Encoder and Matroska Muxer.");

            // connect Matroska Muxer and CloudStreamRenderer Filter
            hr = graph.ConnectDirect(GetPin(pMatroskaMuxer, "Output"), GetPin(pCloudStreamRenderer, "In"), null);
            checkHR(hr,"Can't connect Matroska Muxer and CloudStreamRenderer Filter.");
            
            // connect Matroska Muxer and File Writer
            // hr = graph.ConnectDirect(GetPin(pMatroskaMuxer, "Output"), GetPin(pFileWriter, "in"), null);
            // checkHR(hr, "Can't connect Matroska Muxer and File Writer.");

            mediaControl = (IMediaControl)graph;
        }

        public void StartCapture()
        {
            int hr = mediaControl.Run();
            checkHR(hr, "Can't run the graph.");
        }

        public void StopCapture()
        {
            mediaControl.Pause();
            mediaControl.StopWhenReady();
        }

        private void checkHR(int hr, string message)
        {
            if (hr < 0)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DsError.ThrowExceptionForHR(hr);
            }
        }

        private IBaseFilter CreateFilter(string displayName, string friendlyName)
        {
            int hr = 0;
            IBaseFilter filter = null;
            IBindCtx bindCtx = null;
            IMoniker moniker = null;

            try
            {
                hr = CreateBindCtx(0, out bindCtx);
                checkHR(hr, "Can't create " + friendlyName + ".");
                Marshal.ThrowExceptionForHR(hr);

                int eaten;
                hr = MkParseDisplayName(bindCtx, displayName, out eaten, out moniker);
                checkHR(hr, "Can't create " + friendlyName + ".");
                Marshal.ThrowExceptionForHR(hr);

                Guid guid = typeof(IBaseFilter).GUID;
                object obj;
                moniker.BindToObject(bindCtx, null, ref guid, out obj);
                filter = (IBaseFilter)obj;
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (bindCtx != null) Marshal.ReleaseComObject(bindCtx);
                if (moniker != null) Marshal.ReleaseComObject(moniker);
            }

            return filter;
        }

        private IBaseFilter CreateDeviceFilter(DsDevice device)
        {
            Guid guid = typeof(IBaseFilter).GUID;
            object obj;
            device.Mon.BindToObject(null, null, ref guid, out obj);
            return (IBaseFilter)obj;
        }

        private IPin GetPin(IBaseFilter filter, string pinname)
        {
            IEnumPins epins;
            int hr = filter.EnumPins(out epins);
            checkHR(hr, "Can't enumerate pins.");
            IntPtr fetched = Marshal.AllocCoTaskMem(4);
            IPin[] pins = new IPin[1];
            while (epins.Next(1, pins, fetched) == 0)
            {
                PinInfo pinfo;
                pins[0].QueryPinInfo(out pinfo);
                bool found = (pinfo.name == pinname);
                DsUtils.FreePinInfo(pinfo);
                if (found) return pins[0];
            }
            checkHR(-1, "Pin not found.");
            return null;
        }

        private IPin GetFirstOutputPin(IBaseFilter filter)
        {
            IEnumPins epins;
            int hr = filter.EnumPins(out epins);
            checkHR(hr, "Can't enumerate pins.");
            IntPtr fetched = Marshal.AllocCoTaskMem(4);
            IPin[] pins = new IPin[1];
            while (epins.Next(1, pins, fetched) == 0)
            {
                PinInfo pinfo;
                pins[0].QueryPinInfo(out pinfo);
                bool found = (pinfo.dir == PinDirection.Output);
                DsUtils.FreePinInfo(pinfo);
                if (found) return pins[0];
            }
            checkHR(-1, "First output Pin not found.");
            return null;
        }

        [DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        [DllImport("ole32.dll")]
        public static extern int MkParseDisplayName(IBindCtx pcb, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out int pchEaten, out IMoniker ppmk);
    }
}