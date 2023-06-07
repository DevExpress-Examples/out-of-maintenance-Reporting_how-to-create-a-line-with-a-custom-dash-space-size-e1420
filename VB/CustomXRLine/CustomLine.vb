Imports System
Imports System.ComponentModel
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports System.Drawing
Imports DevExpress.Drawing

Namespace CustomXRLine

    Public Class CustomLine
        Inherits XRLine

        Private dashField As Integer

        Private spaceField As Integer

        Public Sub New()
            MyBase.New()
            dashField = 1
            spaceField = 0
        End Sub

        <DisplayName("Dash size"), Browsable(True), DefaultValue(1)>
        Public Property Dash As Integer
            Get
                Return dashField
            End Get

            Set(ByVal value As Integer)
                dashField = value
            End Set
        End Property

        <DisplayName("Space size"), Browsable(True), DefaultValue(0)>
        Public Property Space As Integer
            Get
                Return spaceField
            End Get

            Set(ByVal value As Integer)
                spaceField = value
            End Set
        End Property

        Protected Overrides Function CreateBrick(ByVal childrenBricks As VisualBrick()) As VisualBrick
            If LineStyle <> DXDashStyle.Custom Then
                Return MyBase.CreateBrick(childrenBricks)
            Else
                Return New PanelBrick(Me)
            End If
        End Function

        Protected Overrides Sub PutStateToBrick(ByVal brick As VisualBrick, ByVal ps As PrintingSystemBase)
            If LineStyle <> DXDashStyle.Custom Then
                MyBase.PutStateToBrick(brick, ps)
            Else
                Dim panel As PanelBrick = CType(brick, PanelBrick)
                panel.BackColor = Color.Transparent
                panel.Sides = BorderSide.None
                Dim i As Integer
                If LineDirection = LineDirection.Horizontal Then
                    For i = 0 To Convert.ToInt32(panel.Rect.Width / (Dash + Space)) - 1
                        Dim dashBrick As LineBrick = New LineBrick()
                        dashBrick.Sides = BorderSide.None
                        dashBrick.BackColor = panel.Style.ForeColor
                        dashBrick.Rect = New RectangleF(i * (Dash + Space), panel.Rect.Height / 2 - LineWidth / 2, dashField, LineWidth)
                        panel.Bricks.Add(dashBrick)
                    Next

                    Dim endBrick As LineBrick = New LineBrick()
                    endBrick.Sides = BorderSide.None
                    endBrick.BackColor = panel.Style.ForeColor
                    endBrick.Rect = New RectangleF(i * (Dash + Space), panel.Rect.Height / 2 - LineWidth / 2, panel.Rect.Width - i * (Dash + Space), LineWidth)
                    panel.Bricks.Add(endBrick)
                ElseIf LineDirection = LineDirection.Vertical Then
                    For i = 0 To Convert.ToInt32(panel.Rect.Height / (Dash + Space)) - 1
                        Dim dashBrick As LineBrick = New LineBrick()
                        dashBrick.Sides = BorderSide.None
                        dashBrick.BackColor = panel.Style.ForeColor
                        dashBrick.Rect = New RectangleF(panel.Rect.Width / 2 - LineWidth / 2, i * (Dash + Space), LineWidth, Dash)
                        panel.Bricks.Add(dashBrick)
                    Next

                    Dim endBrick As LineBrick = New LineBrick()
                    endBrick.Sides = BorderSide.None
                    endBrick.BackColor = panel.Style.ForeColor
                    endBrick.Rect = New RectangleF(panel.Rect.Width / 2 - LineWidth / 2, i * (Dash + Space), LineWidth, panel.Rect.Height - i * (Dash + Space))
                    panel.Bricks.Add(endBrick)
                End If
            End If
        End Sub
    End Class
End Namespace
