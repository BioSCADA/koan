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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using Koan.Blueteeth;
using BioSCADA;
 

namespace Koan
{
    public partial class CharPanel : UserControl
    {

        public bool ChartPlay = true;

        GraphPane gpRRCO;
        GraphPane gpFFT;
        BoxObj box3 = new BoxObj(0, 100, 0.015, 100, Color.Empty, Color.FromArgb(150, Color.Red));
        public bool IsFFT { get; set; }

        public CharPanel()
        {
            InitializeComponent();
            InitializeChart();
            //toolStrip1.Renderer = new ToolStripOverride();

            //tbAmplitude.SendToBack();
            //this.toolTip1.SetToolTip(tbAmplitude, "A = " + tbAmplitude.Value);
            //tbFrequency.SendToBack();
            //this.toolTip1.SetToolTip(tbFrequency, "f = " + tbFrequency.Value);
        }

        private void InitializeChart()
        {
            // Get a reference to the GraphPane instance in the ZedGraphControl
            gpRRCO = zgRRCO.GraphPane;
            gpRRCO.Border.IsVisible = false;
            // myPane.Fill = new Fill(Color.FromArgb(90, 110, 220), Color.FromArgb(255, 255, 255), 90);
            gpRRCO.Fill = new Fill(Color.FromArgb(250, 250, 250), Color.FromArgb(187, 84, 39), 90F);
            
            // Set the titles and axis labels
            gpRRCO.Title.Text = "RR e CO";
            gpRRCO.Title.FontSpec.Size = 16;
            gpRRCO.Title.IsVisible = false;
            gpRRCO.XAxis.Title.Text = "tempo";
            gpRRCO.XAxis.Title.FontSpec.Size = 14;

            gpRRCO.YAxis.Title.Text = "RR";
            gpRRCO.YAxis.Title.FontSpec.Size = 14;
            gpRRCO.Y2Axis.Title.Text = "coerência";
            gpRRCO.Y2Axis.Title.FontSpec.Size = 14;
            // Make up some data points based on the Sine function
           
            //for (int i = 0; i < 36; i++)
            //{
            //    double x = (double)i ;
            //    double y = 40 + Math.Sin((double)i * Math.PI / 15.0) * 16.0;
            //    double y2 = y * 13.5;
            //    rrList.Add(x, y);
            //    coherenceList.Add(x, y2);
            //}

            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem myCurve = gpRRCO.AddCurve("RR", Protocol.rrPairList, Color.FromArgb(95, 150, 95), SymbolType.Diamond);
            // Fill the symbols with white
           // myCurve.Symbol.Fill = new Fill(Color.White);

            // Generate a blue curve with circle symbols, and "Beta" in the legend
            myCurve = gpRRCO.AddCurve("coerência", Protocol.coherencePairList, Color.CornflowerBlue, SymbolType.Circle);
            // Fill the symbols with white
         //   myCurve.Symbol.Fill = new Fill(Color.White);
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = true;

            // Show the x axis grid
            gpRRCO.XAxis.MajorGrid.IsVisible = true;

            // Make the Y axis scale red
            gpRRCO.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(95, 150, 95);
            gpRRCO.YAxis.Title.FontSpec.FontColor = Color.FromArgb(95, 150, 95);
            gpRRCO.YAxis.Title.FontSpec.Size = 14;
            gpRRCO.YAxis.Title.FontSpec.Family = "Roboto";
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            gpRRCO.YAxis.MajorTic.IsOpposite = false;
            gpRRCO.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            gpRRCO.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            gpRRCO.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            //   myPane.YAxis.Scale.Min = 40;
            // myPane.YAxis.Scale.Max = 150;

            // Enable the Y2 axis display
            gpRRCO.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue gpRRCO.Y2Axis.Title.FontSpec.FontColor = Color.CornflowerBlue;
            gpRRCO.Y2Axis.Scale.FontSpec.FontColor = Color.SteelBlue;
            gpRRCO.Y2Axis.Title.FontSpec.FontColor = Color.SteelBlue;
            gpRRCO.Y2Axis.Title.FontSpec.Family = "Roboto";
            gpRRCO.Y2Axis.Title.FontSpec.Size = 14;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            gpRRCO.Y2Axis.MajorTic.IsOpposite = false;
            gpRRCO.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            gpRRCO.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            gpRRCO.Y2Axis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            gpRRCO.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);
       // gpRRCO.Chart.Fill = new Fill(Color.Transparent);
            gpRRCO.Chart.Fill.IsVisible = false;
            gpRRCO.Legend.IsVisible = false;
            // Add a text box with instructions
            //TextObj text = new TextObj("Use o mouse para: \n  Zoom, Drag & Pan e Menu ", 0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
            //text.FontSpec.StringAlignment = StringAlignment.Near;
            //graphPanel.GraphObjList.Add(text);

            // Enable scrollbars if needed
            zgRRCO.IsShowHScrollBar = true;
            zgRRCO.IsShowVScrollBar = true;
            zgRRCO.IsAutoScrollRange = true;
            zgRRCO.IsScrollY2 = true;

            // OPTIONAL: Show tooltips when the mouse hovers over a point
            zgRRCO.IsShowPointValues = true;
            zgRRCO.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            // OPTIONAL: Add a custom context menu item
            zgRRCO.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(MyContextMenuBuilder);

            // OPTIONAL: Handle the Zoom Event
            zgRRCO.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            // zg1.Size = new Size(this.Width, this.Height);
            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zgRRCO.AxisChange();
            // Make sure the Graph gets redrawn
            zgRRCO.Invalidate();

            gpFFT = zgFFT.GraphPane;
            // myPane.Fill = new Fill(Color.FromArgb(90, 110, 220), Color.FromArgb(95, 150, 95), 90);
            gpFFT.Fill = new Fill(Color.White, Color.FromArgb(95, 150, 95), 90F);
            gpFFT.Border.IsVisible = false;
            // Set the titles and axis labels
            gpFFT.Title.Text = "análise espectral";
            gpFFT.Title.FontSpec.Size = 14;
            gpFFT.Title.FontSpec.Family = "Roboto";
            gpFFT.Title.IsVisible = false;
            gpFFT.XAxis.Title.Text = "frequência";
            gpFFT.XAxis.Title.FontSpec.Size = 14;
            gpFFT.YAxis.Title.FontSpec.FontColor = Color.FromArgb(51, 51, 51);

            gpFFT.YAxis.Title.FontSpec.FontColor = Color.FromArgb(51, 51, 51);
            gpFFT.YAxis.Title.Text = "potência";
            gpFFT.YAxis.Title.FontSpec.Size = 14;
            gpFFT.YAxis.Title.FontSpec.Family = "Roboto";
            gpFFT.YAxis.Scale.Align = AlignP.Inside;

            gpFFT.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            gpFFT.Y2Axis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            gpFFT.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            zgFFT.IsShowHScrollBar = true;
            zgFFT.IsShowVScrollBar = true;
            zgFFT.IsAutoScrollRange = true;
           zgFFT.IsScrollY2 = true;
            LineItem myCurve2 = gpFFT.AddCurve("espectro de frequência", Protocol.fftPowerPairList, Color.FromArgb(95, 150, 95), SymbolType.None);
            // Fill the area under the curve with a white-red gradient at 45 degrees 
           // myCurve2.Line.Fill = new Fill(Color.White, Color.Red, 45F);
            // Make the symbols opaque by filling them with white 
            //myCurve2.Symbol.Fill = new Fill(System.Drawing.SystemColors.GradientActiveCaption);
            
            // range do maior pico
            BoxObj box = new BoxObj(.04, 1000, (0.26 - 0.04), 1000, Color.Empty, Color.FromArgb(150, Color.Blue));
            box.Fill = new Fill(  Color.FromArgb(100, Color.Blue));
            // Use the BehindAxis zorder to draw the highlight beneath the grid lines
            box.ZOrder = ZOrder.A_InFront;
            // Make sure that the boxObj does not extend outside the chart rect if the chart is zoomed
            box.IsClippedToChartRect = true;
            // Use a hybrid coordinate system so the X axis always covers the full x range
            // from chart fraction 0.0 to 1.0
        //    box.Location.CoordinateFrame = CoordType.XChartFractionYScale;
            gpFFT.GraphObjList.Add(box);

            //total power
            BoxObj box2 = new BoxObj(0.0033, 1000, (0.4), 1000, Color.Empty, Color.FromArgb(150, Color.Green));
            box2.Fill = new Fill(  Color.FromArgb(100, Color.LightGreen));
            // Use the BehindAxis zorder to draw the highlight beneath the grid lines
            box2.ZOrder = ZOrder.A_InFront;
            // Make sure that the boxObj does not extend outside the chart rect if the chart is zoomed
            box2.IsClippedToChartRect = true;
            // Use a hybrid coordinate system so the X axis always covers the full x range
            // from chart fraction 0.0 to 1.0
        //    box2.Location.CoordinateFrame = CoordType.XChartFractionYScale;
            gpFFT.GraphObjList.Add(box2);

            gpFFT.Chart.Fill.IsVisible = false;
            gpFFT.Legend.IsVisible = false;

            // Add a text item to label the highlighted range
            //TextObj text = new TextObj("Optimal\nRange", 0.95f, 85, CoordType.AxisXYScale,  AlignH.Right, AlignV.Center);
            //text.FontSpec.Fill.IsVisible = false;
            //text.FontSpec.Border.IsVisible = false;
            //text.FontSpec.IsBold = true;
            //text.FontSpec.IsItalic = true;
            //text.Location.CoordinateFrame = CoordType.XChartFractionYScale;
            //text.IsClippedToChartRect = true;
            //gpFFT.GraphObjList.Add(text);

            zgFFT.AxisChange();
            // Make sure the Graph gets redrawn
            zgFFT.Invalidate();
            zgFFT.Refresh();

            zgRRCO.BringToFront();
           
             
        }


        public void updatePanel()
        {

         //   Protocol.SimAmplitude = tbAmplitude.Value;
         //   Protocol.SimFrequency = tbFrequency.Value;
            //if (Protocol.rrTemplist.Count < 10)
            //{
            //    imgCalib.Visible = true;
            //    imgCalib.BringToFront();
            //    this.Invalidate();
            //    this.Refresh();
            //    label1.Text = "calibrando.....";
            //    label1.BringToFront();
            //    Console.WriteLine(Protocol.rrTemplist.Count);
            //}
            //else
            //{
            //    label1.Text = "";
            //    imgCalib.Visible = false;
            //    this.Invalidate();
            //    this.Refresh();
            //    Console.WriteLine(Protocol.rrTemplist.Count);
            //}
            if (ChartPlay)
            {

                double x = (double)Protocol.rrlist.Count; 

                if (x < 120)
                {
                    gpRRCO.XAxis.Scale.Min = 0;
                    gpRRCO.XAxis.Scale.Max = x + 10;
                    zgRRCO.RestoreScale(gpRRCO);
                }
                else
                {
                    gpRRCO.XAxis.Scale.Min = x - 64;
                    gpRRCO.XAxis.Scale.Max = x + 10;

                }
                gpFFT.GraphObjList.Remove(box3);
                //peak power
                box3 = new BoxObj((Protocol.PeakFreq - 0.0075), 1000, 0.015, 1000, Color.Empty, Color.FromArgb(100, Color.Red));
               // Console.WriteLine(Protocol.PeakFreq);
                box3.Fill = new Fill(Color.FromArgb(150, Color.Red));
                // Use the BehindAxis zorder to draw the highlight beneath the grid lines
                box3.ZOrder = ZOrder.A_InFront;
                // Make sure that the boxObj does not extend outside the chart rect if the chart is zoomed
                box3.IsClippedToChartRect = true;
                // Use a hybrid coordinate system so the X axis always covers the full x range
                // from chart fraction 0.0 to 1.0
                // box3.Location.CoordinateFrame = CoordType.XChartFractionYScale;
                gpFFT.GraphObjList.Add(box3);
                 

                gpFFT.XAxis.Scale.Max = 0.5;
             
            }

            zgRRCO.AxisChange();
            // Make sure the Graph gets redrawn
            zgRRCO.Invalidate();
            zgRRCO.Refresh();

            zgFFT.AxisChange();
            // Make sure the Graph gets redrawn
            zgFFT.Invalidate();
            zgFFT.Refresh();
            
        }

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];

            return curve.Label.Text + "  " + pt.Y.ToString("f2") + "  " + pt.X.ToString("f1") + " ";
        }

        /// <summary>
        /// Customize the context menu by adding a new item to the end of the menu
        /// </summary>
        private void MyContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            //ToolStripMenuItem item = new ToolStripMenuItem();
            //item.Name = "add-beta";
            //item.Tag = "add-beta";
            //item.Text = "Add a new Beta Point";
            //item.Click += new System.EventHandler(AddBetaPoint);


            // menuStrip.Items.Add(itemMaximize); 
        }


        /// <summary>
        /// Handle the "Add New Beta Point" context menu item.  This finds the curve with
        /// the CurveItem.Label = "Beta", and adds a new point to it.
        /// </summary>
        //private void AddBetaPoint(object sender, EventArgs args)
        //{
        //    // Get a reference to the "Beta" curve IPointListEdit
        //    IPointListEdit ip = zg1.GraphPane.CurveList["Beta"].Points as IPointListEdit;
        //    if (ip != null)
        //    {
        //        double x = ip.Count * 5.0;
        //        double y = Math.Sin(ip.Count * Math.PI / 15.0) * 16.0 * 13.5;
        //        ip.Add(x, y);
        //        zg1.AxisChange();
        //        zg1.Refresh();
        //    }
        //}

        // Respond to a Zoom Event
        private void MyZoomEvent(ZedGraphControl control, ZoomState oldState, ZoomState newState)
        {
            // Here we get notification everytime the user zooms
        }


        private void CharPanel_Resize(object sender, EventArgs e)
        {
            zgRRCO.Size = new Size(pDockChart.Width, pDockChart.Height);
        }

        private void zg1_MouseClick(object sender, MouseEventArgs e)
        {

            if (this.ChartPlay)
            {
                this.ChartPlay = false;

            }
            else
            {

                this.ChartPlay = true;
            }
        }

        
        private void tsbSimulated_Click(object sender, EventArgs e)
        {
            if (Protocol.IsSimuletedRAN) {
              //  tbAmplitude.Visible = false;

             //   tbFrequency.Visible = false;
                Protocol.IsSimuletedRAN = false;
             //   this.tsbSimulated.Image = global::Koan.Properties.Resources.Loop1;
            } else {
             //   tbAmplitude.Visible = true;
            //    this.tsbSimulated.Image = global::Koan.Properties.Resources.Loop2;
             //   tbFrequency.Visible = true;
                Protocol.IsSimuletedRAN = true; 
            }

         //   tbAmplitude.SendToBack();

          //  tbFrequency.SendToBack();
        }

        private void tbAmplitude_ValueChanged(object sender, EventArgs e)
        {
           // Protocol.SimAmplitude = tbAmplitude.Value;
           // this.toolTip1.SetToolTip(tbAmplitude, "A = " + tbAmplitude.Value);
        }

        private void tbFrequency_ValueChanged(object sender, EventArgs e)
        {
           // Protocol.SimFrequency = tbFrequency.Value;
           // this.toolTip1.SetToolTip(this.tbFrequency, "f = " + tbFrequency.Value);
        }
 

        private void zgRRCO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IsFFT)
            {
                zgRRCO.BringToFront();
                IsFFT = false;
            }
            else
            {

                zgFFT.BringToFront();
                IsFFT = true;
            }
        }

        private void zgFFT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IsFFT)
            {
                zgRRCO.BringToFront();
                IsFFT = false;
            }
            else
            {

                zgFFT.BringToFront();
                IsFFT = true;
            }
        }
    }
}
