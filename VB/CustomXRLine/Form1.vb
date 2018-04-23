Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Design

Namespace CustomXRLine
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim report As New XtraReport1()
			AddHandler report.DesignerLoaded, AddressOf report_DesignerLoaded
			report.ShowDesigner()
		End Sub

		Private Sub report_DesignerLoaded(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs)
			Dim tb As IToolboxService = TryCast(e.DesignerHost.GetService(GetType(IToolboxService)), IToolboxService)
			tb.AddToolboxItem(New ToolboxItem(GetType(CustomLine)))
		End Sub
	End Class
End Namespace