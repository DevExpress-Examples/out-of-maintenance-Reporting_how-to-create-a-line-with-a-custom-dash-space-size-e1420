Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports System.Drawing

Namespace CustomXRLine
	Public Class CustomLine
		Inherits XRLine
		Private dash_Renamed As Integer
		Private space_Renamed As Integer
		Public Sub New()
			MyBase.New()
			Me.dash_Renamed =1
			Me.space_Renamed = 0
		End Sub
		<DisplayName("Dash size"), Browsable(True), DefaultValue(1)> _
		Public Property Dash() As Integer
			Get
				Return Me.dash_Renamed
			End Get
			Set(ByVal value As Integer)
				Me.dash_Renamed = value
			End Set
		End Property
		<DisplayName("Space size"), Browsable(True), DefaultValue(0)> _
		Public Property Space() As Integer
			Get
				Return Me.space_Renamed
			End Get
			Set(ByVal value As Integer)
				Me.space_Renamed = value
			End Set
		End Property
		Protected Overrides Function CreateBrick(ByVal childrenBricks() As VisualBrick) As VisualBrick
			If Me.LineStyle <> System.Drawing.Drawing2D.DashStyle.Custom Then
				Return MyBase.CreateBrick(childrenBricks)
			Else
				Return New PanelBrick(Me)
			End If
		End Function

		Protected Overrides Sub PutStateToBrick(ByVal brick As VisualBrick, ByVal ps As PrintingSystem)
			If Me.LineStyle <> System.Drawing.Drawing2D.DashStyle.Custom Then
				MyBase.PutStateToBrick(brick, ps)
			Else
				Dim panel As PanelBrick = CType(brick, PanelBrick)
				panel.BackColor = Color.Transparent
				panel.Sides = BorderSide.None
				Dim i As Integer
				If Me.LineDirection = LineDirection.Horizontal Then
					For i = 0 To Convert.ToInt32(panel.Rect.Width / (Me.Dash + Me.Space)) - 1
						Dim dashBrick As New LineBrick()
						dashBrick.Sides = BorderSide.None
						dashBrick.BackColor = panel.Style.ForeColor
						dashBrick.Rect = New RectangleF(i * (Me.Dash + Me.Space), panel.Rect.Height / 2 - Me.LineWidth \ 2, Me.dash_Renamed, Me.LineWidth)
						panel.Bricks.Add(dashBrick)
					Next i
					Dim endBrick As New LineBrick()
					endBrick.Sides = BorderSide.None
					endBrick.BackColor = panel.Style.ForeColor
					endBrick.Rect = New RectangleF(i * (Me.Dash + Me.Space), panel.Rect.Height / 2 - Me.LineWidth \ 2, panel.Rect.Width - i * (Me.Dash + Me.Space), Me.LineWidth)
					panel.Bricks.Add(endBrick)
				ElseIf Me.LineDirection = LineDirection.Vertical Then
					For i = 0 To Convert.ToInt32(panel.Rect.Height / (Me.Dash + Me.Space)) - 1
						Dim dashBrick As New LineBrick()
						dashBrick.Sides = BorderSide.None
						dashBrick.BackColor = panel.Style.ForeColor
						dashBrick.Rect = New RectangleF(panel.Rect.Width / 2 - Me.LineWidth \ 2, i * (Me.Dash + Me.Space), Me.LineWidth, Me.Dash)
						panel.Bricks.Add(dashBrick)
					Next i
					Dim endBrick As New LineBrick()
					endBrick.Sides = BorderSide.None
					endBrick.BackColor = panel.Style.ForeColor
					endBrick.Rect = New RectangleF(panel.Rect.Width / 2 - Me.LineWidth \ 2, i * (Me.Dash + Me.Space), Me.LineWidth, panel.Rect.Height - i*(Me.Dash+Me.Space))
					panel.Bricks.Add(endBrick)
				End If
			End If
		End Sub
	End Class
End Namespace
