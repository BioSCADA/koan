/*M///////////////////////////////////////////////////////////////////////////////////////
//
//  IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING.
//
//  By downloading, copying, installing or using the software you agree to this license.
//  If you do not agree to this license, do not download, install,
//  copy or use the software.
//
//
//                           License Agreement
//                For Open Source Heart Rate SCADA Library  
//
// Copyright (C) 2011-2012, Diego Schmaedech, all rights reserved. 
//
							For Open Source Biosignal SCADA
//
// Copyright (C) 2012, Laboratório de Educação Cerebral, all rights reserved.
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

using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Threading;
using Koan.Sessions;
using BioSCADA;

namespace Koan.Blueteeth
{
    public class KoanDriver
    {
         
        public static SerialPort mySerialPort;
         
        public static string device = "";
        public static string port = "";
        public static int portsToTest = 0;
        public static Boolean inTest = false;
        static int iHeader = 0, iSize = 0, iCheck = 0, iIndex = 0, iStatus = 0, iBPM = 0, iRRI = 0;
        static int hxmBattery, hxmHeartRate;
        static int iBattery, iBeat;
        static DateTime dDate; 
        static Hashtable devices = new Hashtable();
        public static bool Connected = false;
        public static bool TestEnd = false;
        public static bool Error = false;
        public static DateTime ttOpened = DateTime.Now;
        public static DateTime ttEvent = DateTime.Now;
         
        public KoanDriver( )
        {
             
        }
         
        public static void Open(string port, string device)
        {
            mySerialPort = new SerialPort(port);
            KoanDriver.device = device;
            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.ReadTimeout = 2000;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            if (mySerialPort.IsOpen)
            {
                KoanDriver.Connected = false;
                //AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "Porta serial já está sendo usada por outro dispositivo.");
           
            } 
            try
            {
                mySerialPort.Open();
                if (mySerialPort.BytesToRead == 0)
                {
                    //AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "Não há dados na porta serial, verifique o dispositivo!");

                    KoanDriver.Connected = false;
                    Error = true;
                } 
            }             
            catch (Exception ex)
            {
                //AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "Não há dados na porta serial, verifique o dispositivo!");
           
                KoanDriver.Connected = false;
                Error = true;
            }
            
            string path;
            DateTime timestamp;
            timestamp = DateTime.Now;
            path = Application.ExecutablePath;

          

            dDate = timestamp;
            iBPM = -1;
            iRRI = 0;
            iSize = -1;
             
        }

        public static void Close()
        {
            mySerialPort.Close();
            
            Protocol.Battery = 0;
            Protocol.Heartrate = 0;
            Connected = false;
        }

        public DateTime Time()
        {
            return dDate;
        }

        public Boolean Beat()
        {
            if (iBeat > 0) 
                return true;
            else 
                return false;
        }

        public int Bat()
        {
            return iBattery;
        }

        public int BPM()
        {
            return iBPM;
        }

        public int Index()
        {
            return iIndex;
        }

        public string Header()
        {
            string s;
            s = iHeader.ToString() + "," + iStatus.ToString();
            return s;
        }

        public int RRI()
        {
            return iRRI;
        }
         
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                iRRI = 0;
                Connected = true;
                Error = false;
                SerialPort sp = (SerialPort)sender;
                
                int bytes = 0;
                try
                {
                    bytes = sp.BytesToRead;
                }
                catch (Exception ex)
                {
                     //AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "Não há dados na porta serial, verifique o dispositivo!");
           
                 //   MessageBox.Show(ex.Message, "erro ao ler porta serial!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    KoanDriver.Connected = false;
                    Error = true;
                }
            
                if (device.Equals("POLAR"))
                {
                    string sRow;
                    //Atleast 6 bytes
                    if (sp.BytesToRead > 5 && bytes < 60)
                    {
                        iHeader = sp.ReadByte(); //always 254
                        iSize = sp.ReadByte(); //size of block including bHeader, always even (8, 10, 12), different number of RRI
                        iCheck = sp.ReadByte(); //255-bSize
                        iIndex = sp.ReadByte(); //index: 0-15 (seconds?), first is 1
                        //2010-08-05 iBattery changed to iSttus
                        iStatus = sp.ReadByte(); //status bit 1BBP0001, thus 128+64+16+1=209 beats (P=16) detected (BA=2) , 193 no beats
                        iBeat = (iStatus >> 4) & 1;
                        iBattery = (iStatus >> 5) & 3;
                        iBPM = sp.ReadByte(); //beats per minutes, some averaging?
                        Protocol.Battery = iBattery;
                        dDate = DateTime.Now;
                        sRow = dDate.ToString("HH:mm:ss") + (char)9 + iHeader.ToString() + (char)9 + iSize.ToString() + (char)9 + iCheck.ToString() + (char)9 + iIndex.ToString() + (char)9 + iStatus.ToString() + (char)9 + iBPM.ToString();
                       // Console.WriteLine("iSize: " + iSize);
                        for (int i = 7; i < iSize; i = i + 2) //different number of RRI intervals
                        {
                            int tmpiRRI = sp.ReadByte() * 256 + sp.ReadByte(); //RRI (ms)  
                            Protocol.AddSample(dDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "" + iBattery, "" + iBPM, tmpiRRI, "" + Protocol.TAGs); 
                            Console.Write( tmpiRRI +"," );
                           // AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "RR " + tmpiRRI);
           
                        }
                         
                        sRow = sRow + (char)9 + iRRI.ToString();
                       
                        Console.WriteLine("");
                    }
                    else
                    {
                         

                    }

                    //Atleast 6 bytes for polar
                    // Console.WriteLine("bytes: " + bytes);
                    //if (bytes > 5 && bytes < 60)
                    //{
                    //    iHeader = sp.ReadByte(); //always 254
                    //    iSize = sp.ReadByte(); //size of block including bHeader, always even (8, 10, 12), different number of RRI
                    //    iCheck = sp.ReadByte(); //255-bSize
                    //    iIndex = sp.ReadByte(); //index: 0-15 (seconds?), first is 1    //2010-08-05 iBattery changed to iSttus
                    //    iStatus = sp.ReadByte(); //status bit 1BBP0001, thus 128+64+16+1=209 beats (P=16) detected (BA=2) , 193 no beats
                    //    iBeat = (iStatus >> 4) & 1;
                    //    iBattery = (iStatus >> 5) & 3;
                    //    iBPM = sp.ReadByte(); //beats per minutes, some averaging?

                    //    dDate = DateTime.Now;
                    //    int hxmRR = 0;
                    //    if (iBPM > 30 && iBPM < 160)
                    //    {
                    //        hxmRR = 60000 / iBPM;
                    //    }
                    //    Protocol.Battery = iBattery * 10;
                       
                    //    Console.WriteLine(" " + iBattery + "\t" + iBPM);
                    //    for (int i = 7; i < iSize; i = i + 2) //different number of RRI intervals
                    //    {
                    //        iRRI = sp.ReadByte() * 256 + sp.ReadByte(); //RRI (ms)  
                    //    }

                    //    if (iBPM > 30 && iBPM < 160)
                    //    {
                    //        Protocol.addSample(dDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "" + iBattery, iBPM.ToString(), hxmRR, "" + Protocol.lap);
                    //    }
                    //    else
                    //    {
                    //        Protocol.Battery = 0;
                    //        Protocol.addHeartRate(0);
                    //    }
                    //}
                    //else
                    //{
                    //    Protocol.Battery = 0;
                    //    Protocol.addHeartRate(0);

                    //}
                }
                else if (device.Equals("ZEPHYR"))
                {
                    //Atleast 6 bytes for polar
                  //   Console.WriteLine("bytes: " + bytes);
                    int tmp = 0;
                    if (bytes > 59 && bytes < 100)
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            tmp = sp.ReadByte();
                            if (i == 11)
                            {
                                hxmBattery = tmp;
                            }
                            if (i == 12)
                            {
                                hxmHeartRate = tmp;
                            }
                        }

                        dDate = DateTime.Now;
                        Protocol.Battery = hxmBattery;
                        int hxmRR = 0;
                        if (hxmHeartRate > 30 && hxmHeartRate < 150)
                        {
                            hxmRR = 60000 / hxmHeartRate;
                        }
                        
                        //   Console.WriteLine(" " + Protocol.getBattery() + "\t" + Protocol.getHeartRate());
                       // Console.WriteLine(" " + hxmBattery + "\t" + hxmRR);


                        if (hxmHeartRate > 30 && hxmHeartRate < 150)
                        {
                            Protocol.AddSample(dDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "" + hxmBattery, "" + hxmHeartRate, hxmRR, "" + Protocol.TAGs);
                       
                        }
                        else {
                            //Protocol.Battery = 0;
                            //Protocol.addHeartRate(0);
                        }
                    }

                    else {
                        //Protocol.Battery = 0;
                        //Protocol.addHeartRate(0);
                
                    }


                }

                 

            }
            catch (TimeoutException to)
            {
                 //AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "Não há dados na porta serial, verifique o dispositivo!");
           
            }
        }
          
    }
}
