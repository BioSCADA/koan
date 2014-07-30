/*M///////////////////////////////////////////////////////////////////////////////////////
//
//  IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING.
//
//  By downloading, copying, installing or using the software you agree to this license.
//  If you do not agree to this license, do not download, install,
//  copy or use the software.
//
//
//                           BioSCADA® License Agreement
//                For Open Source Human SCADA Library  
//
// Copyright (C) 2011-2014, Diego Schmaedech for this and Many Others Developers around the worlds for all, all rights reserved. 
//
//							For Open Source Human SCADA aplications
//
// Copyright (C) 2011-2014, Prof. Dr. Emílio Takase, Laboratório de Educação Cerebral, all rights reserved.
//
// Third party copyrights are property of their respective owners.
// Third party copyrights are property of their respective owners.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
//   * Redistribution's of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//
//   * Redistribution's in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//
//   * The name of the copyright holders may not be used to endorse or promote products
//     derived from this software without specific prior written permission.
//
// This software is provided by the copyright holders and contributors "as is" and
// any express or implied warranties, including, but not limited to, the implied
// warranties of merchantability and fitness for a particular purpose are disclaimed.
// In no event shall the Intel Corporation or contributors be liable for any direct,
// indirect, incidental, special, exemplary, or consequential damages
// (including, but not limited to, procurement of substitute goods or services;
// loss of use, data, or profits; or business interruption) however caused
// and on any theory of liability, whether in contract, strict liability,
// or tort (including negligence or otherwise) arising in any way out of
// the use of this software, even if advised of the possibility of such damage.
//
//M*/
using Koan;
using Koan.Blueteeth;
using Koan.Sessions;
using MathLibrary;
using System; 
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ZedGraph;
namespace BioSCADA
{
    public class Protocol
    {
        private static Protocol instance;
        public static Samples samples = new Samples();
        public static Acquisitions acquisitions = new Acquisitions();
        public static Acquisition acquisition;
        public static PointPairList rrPairList = new PointPairList();
        public static PointPairList coherencePairList = new PointPairList();
        public static PointPairList fftPowerPairList = new PointPairList();

        public static float Battery = 0;
        public static float SimFrequency = 1;
        public static float SimAmplitude = 15;
        public static float Heartrate = 0;
        public static float LastHeartrate = 0;
        public static float Coherence = 0;
        public static float PeakFreq = 0;
        public static string filename = Path.GetDirectoryName(Application.ExecutablePath);
        public static ArrayList rrlist = new ArrayList(); 
        public static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static string TAGs = "";
        public static float[] rrArray = (float[])rrlist.ToArray(typeof(float));
        public static bool IsSimuletedRAN = false;
        public static DateTime newAquisitionDate = DateTime.Now;
        public static bool IsPlay = false;
        public static bool IsConected = false;
        public static string Type = "KOAN";
        public static string RAWFILENAME = "[KOAN]" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
       
        public static void RunPhyfft()
        {

            rrArray = (float[])rrlist.ToArray(typeof(float));
            if (rrArray.Length > 4)
            {
                rrArray = Interpolation.Interpolation4(rrArray, 4);


                PhyFFT phyfft = new PhyFFT(rrArray, 4f/*freq*/, 128/*samples*/, 0/*K*/, 128 / 2/*L*/, 64 / 2/*D*/, false, 1/*smooth*/, 1/*decimation*/, "Welch", true, true);

                Coherence = phyfft.getCoherence();
                double x = (double)Protocol.rrlist.Count;
                rrPairList.Add(x, Heartrate);
                coherencePairList.Add(x, Coherence);
            }
        }
         

        public static void Clear()
        {
            rrlist.Clear();
            rrPairList.Clear();
            coherencePairList.Clear();
            fftPowerPairList.Clear();
            samples = new Samples();
        }


        public static void AddRAN()
        {

            float fs = SimFrequency / 12f;

            float a = SimAmplitude;
            float y = 800 + a * (float)Math.Sin(((float)Protocol.rrlist.Count / 1) * ((2f * (float)Math.PI) * fs));
            //  Console.WriteLine(Protocol.rrlist.Count + "\n");
            DateTime dDate = DateTime.Now;
            Protocol.AddSample(dDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "0", "0",(int) y, TAGs);
            //Console.WriteLine(Convert.ToInt32(y) + "\n");
        }

        public static void AddSample(string p2, string p3, string p4, float rr, string p7)
        {
            //if (rr <= 1500 && rr >= 300 )
            //{

            //    rrTemplist.Add(rr);
            //    if (rrTemplist.Count > 10)
            //    {
            //        float[] rrTmpArray = (float[])rrTemplist.ToArray(typeof(float));
            //        if (Math.Abs(rr - Statistics.mean(rrTmpArray)) < 370)
            //        {
            //            Heartrate = rr;
            //            rrlist.Add(rr);
            //            runPhyfft();
            //            Sample sampling = new Sample(User.Login, p2, p3, p4, rr.ToString(), Coherence.ToString(), p7);
            //            samples.Add(sampling);

            //        }
            //        else
            //        {
            //            //float[]  rrTmpArray = (float[])rrTemplist.ToArray(typeof(float));
            //            //Heartrate = (Statistics.mean(rrTmpArray) + rr )/2;
            //            //rrlist.Add(rr);
            //            //runPhyfft();
            //            //Sample sampling = new Sample(User.Login, p2, p3, p4, rr.ToString(), Coherence.ToString(), p7);
            //            //samples.Add(sampling);
            //            Console.WriteLine("Math.Abs(rr - (float)rrTemplist[rrTemplist.Count - 2]) < 370 " + rr);
            //        }


            //    }



            //}
            //else {
            //    Console.WriteLine("rr <= 1500 && rr >= 300 " + rr);
            //}

            if (rr <= 1500 && rr >= 300)
            {
                Heartrate = rr;
                rrlist.Add(rr);
                RunPhyfft();
                Sample sampling = new Sample(User.ID.ToString(), p2, p3, p4, rr.ToString(), Coherence.ToString(), p7);
                samples.Add(sampling);
                LastHeartrate = Heartrate;
                
            }


        }

        public static void WriteTxT(DataTable dt, string filePath)
        {
            int i = 0;
            StreamWriter sw = null;

            try
            {

                sw = new StreamWriter(filePath, false);

                for (i = 0; i < dt.Columns.Count - 1; i++)
                {

                    sw.Write(dt.Columns[i].ColumnName + (char)9);

                }
                sw.Write(dt.Columns[i].ColumnName);
                sw.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    object[] array = row.ItemArray;

                    for (i = 0; i < array.Length - 1; i++)
                    {
                        sw.Write(array[i].ToString() + (char)9);
                    }
                    sw.Write(array[i].ToString());
                    sw.WriteLine();

                }

                sw.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Protocol:Operação invalida: \n" + ex.ToString());
            }
        }

        public static void Stop()
        {
            SaveAcquisitionXML();
            SaveSamplesXML();
            IsPlay = false;
        }

        public static void Play()
        { 
            CreateAcquisitionXML();
            CreateSamplesXML();
            IsPlay = true;
        }

        public static string CreateAcquisitionXML()
        {
            newAquisitionDate = DateTime.Now;
            Protocol.filename = "[" + User.ID + "]" + newAquisitionDate.ToString("yyyy-MM-dd_HH-mm-ss-fff") + ".txt";

            acquisition = new Acquisition(Int32.Parse(User.ID), newAquisitionDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), Protocol.filename, 0, Protocol.Type);

            Protocol.acquisitions.Add(acquisition);
            SaveAcquisitionXML();


            return Protocol.filename;
        }

        public static string SaveAcquisitionXML()
        {

            Protocol.acquisitions.CollectionName = "Acquisitions";
            XmlRootAttribute root = new XmlRootAttribute("Acquisitions");
            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xml = new XmlSerializer(typeof(Acquisitions), root);
            try
            {
                TextWriter writer = new StreamWriter("Data/Report.xml");
                xml.Serialize(writer, Protocol.acquisitions, ns);
                writer.Close();
                App app = (App)App.Current;
                XmlDataProvider xdp = app.TryFindResource("ReportProvider") as XmlDataProvider;
                if (xdp != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Data/Report.xml");
                    xdp.Document = doc;
                    xdp.XPath = "/Acquisitions/Acquisition";
                    xdp.Refresh();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Protocol.filename;
        }

        public static string CreateSamplesXML()
        { 
            Clear();
            SaveSamplesXML(); 
            return Protocol.filename;
        }

        public static string SaveSamplesXML()
        {

            Protocol.samples.CollectionName = "Samples";
            XmlRootAttribute root = new XmlRootAttribute("Samples");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xml = new XmlSerializer(typeof(Samples), root);

            try
            {
                TextWriter writer = new StreamWriter(Protocol.filename);
                Console.WriteLine(Protocol.filename);
                xml.Serialize(writer, Protocol.samples, ns);
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Protocol.filename;
        }


        private Protocol()
        {

        }

        public static Protocol Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Protocol();
                }
                return instance;
            }
        }
    }
}
