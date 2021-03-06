﻿/*M///////////////////////////////////////////////////////////////////////////////////////
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
namespace Koan
{
    partial class CharPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.zgFFT = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pDockChart = new System.Windows.Forms.Panel();
            this.zgRRCO = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.pDockChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // zgFFT
            // 
            this.zgFFT.BackColor = System.Drawing.Color.Transparent; 
            this.zgFFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zgFFT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgFFT.Font = new System.Drawing.Font("Maiandra GD", 11.25F);
            this.zgFFT.ForeColor = System.Drawing.Color.Transparent;
            this.zgFFT.IsAntiAlias = true;
            this.zgFFT.Location = new System.Drawing.Point(0, 0);
            this.zgFFT.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.zgFFT.Name = "zgFFT";
            this.zgFFT.ScrollGrace = 0D;
            this.zgFFT.ScrollMaxX = 0D;
            this.zgFFT.ScrollMaxY = 0D;
            this.zgFFT.ScrollMaxY2 = 0D;
            this.zgFFT.ScrollMinX = 0D;
            this.zgFFT.ScrollMinY = 0D;
            this.zgFFT.ScrollMinY2 = 0D;
            this.zgFFT.Size = new System.Drawing.Size(760, 324);
            this.zgFFT.TabIndex = 2;
            this.toolTip1.SetToolTip(this.zgFFT, "gráfico de frequência cardíaca e coerência");
            this.zgFFT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zg1_MouseClick);
            this.zgFFT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zgFFT_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pDockChart, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(770, 334);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // pDockChart
            // 
            this.pDockChart.BackColor = System.Drawing.Color.Transparent;
            this.pDockChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pDockChart.Controls.Add(this.zgRRCO);
            this.pDockChart.Controls.Add(this.zgFFT);
            this.pDockChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDockChart.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pDockChart.Location = new System.Drawing.Point(5, 5);
            this.pDockChart.Margin = new System.Windows.Forms.Padding(5);
            this.pDockChart.Name = "pDockChart";
            this.pDockChart.Size = new System.Drawing.Size(760, 324);
            this.pDockChart.TabIndex = 4;
            
            // 
            // zgRRCO
            // 
            this.zgRRCO.BackColor = System.Drawing.Color.Transparent; 
            this.zgRRCO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zgRRCO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgRRCO.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zgRRCO.ForeColor = System.Drawing.Color.Transparent;
            this.zgRRCO.IsAntiAlias = true;
            this.zgRRCO.Location = new System.Drawing.Point(0, 0);
            this.zgRRCO.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.zgRRCO.Name = "zgRRCO";
            this.zgRRCO.ScrollGrace = 0D;
            this.zgRRCO.ScrollMaxX = 0D;
            this.zgRRCO.ScrollMaxY = 0D;
            this.zgRRCO.ScrollMaxY2 = 0D;
            this.zgRRCO.ScrollMinX = 0D;
            this.zgRRCO.ScrollMinY = 0D;
            this.zgRRCO.ScrollMinY2 = 0D;
            this.zgRRCO.Size = new System.Drawing.Size(760, 324);
            this.zgRRCO.TabIndex = 0;
            this.zgRRCO.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zg1_MouseClick);
            this.zgRRCO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zgRRCO_MouseDoubleClick);
            // 
            // CharPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Myriad Web Pro", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "CharPanel";
            this.Size = new System.Drawing.Size(770, 334);
           
            this.Resize += new System.EventHandler(this.CharPanel_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pDockChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgRRCO;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pDockChart;
        private ZedGraph.ZedGraphControl zgFFT;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
