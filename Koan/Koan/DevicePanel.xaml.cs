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
using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
 
using Koan.Blueteeth;

namespace BioSCADA
{
    /// <summary>
    /// Interaction logic for DevicePanel.xaml
    /// </summary>
    public partial class DevicePanel : UserControl
    {
        int com_index = 0;

        public DevicePanel()
        {
            InitializeComponent();
            get_ports();
            lb_devices.SelectedIndex = 0;
            lb_com_port.SelectedIndex = 0;

        }
        public void Connect()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
         
            if (lb_com_port.SelectedIndex > -1 && lb_devices.SelectedIndex > -1)
            {
               
                ListBoxItem lbi = (ListBoxItem)lb_devices.SelectedItem;
                KoanDriver.device = lbi.Tag.ToString();
                KoanDriver.port = lb_com_port.SelectedItem.ToString().Trim();
              
                     AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "conectando com o dispositivo..." + KoanDriver.port+":"+ KoanDriver.device);
         
                new KoanConnect().DoSend();

            } 

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }
        private void bt_connect_Click(object sender, RoutedEventArgs e)
        {
             AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "conectando com o dispositivo...");
         
            Connect();
            AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "dispositivo conectado");
         
            get_ports();
        }

        public void get_ports() {
            com_index = lb_com_port.SelectedIndex;
            lb_com_port.Items.Clear();
            if (SerialPort.GetPortNames().Length > 0)
            {
                foreach (string s in SerialPort.GetPortNames())
                { 
                    lb_com_port.Items.Add(s); 
                }
                lb_com_port.SelectedIndex = com_index;  
            }
            else {
                AlarmMessageBus.log((System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4682b4"), "não há dispositivos conectados!");
         
            }
        }
    }
}
